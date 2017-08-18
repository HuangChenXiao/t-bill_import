using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Data.SqlClient;
using DBHelper.MSSQL;
using DataImport.model;

namespace DataImport
{
    public partial class ExcelImport : Form
    {
        string str;
        ImportEntity iEntity;
        DbOperate db;

        List<Sync_Table_PurchaseOrder> pu_order;//采购订单
        public ExcelImport(ImportEntity entity)
        {
            InitializeComponent();
            iEntity = entity;
            str = string.Format("server={0};database={1};uid={2};pwd={3}", entity.Server, entity.Database, entity.Uid, entity.Pwd, entity.AddUser);
            db = new DbOperate(str);
            this.treeView1.ExpandAll();
        }

        string sqlText = "";
        private void excel导入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            string fileName = "";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                fileName = dialog.FileName;
            }
            else
            {
                return;
            }
            string fileType = Path.GetExtension(fileName);
            string connStr;

            if (fileType == ".xls")
                connStr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + fileName + ";" + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
            else
                connStr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + fileName + ";" + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";

            OleDbConnection conn = new OleDbConnection(connStr);
            conn.Open();
            string strExcel = "";
            OleDbDataAdapter myCommand = null;
            DataTable dt = null;
            strExcel = "select * from [sheet1$]";
            myCommand = new OleDbDataAdapter(strExcel, connStr);
            dt = new DataTable();
            myCommand.Fill(dt);

            string name = this.treeView1.SelectedNode.Name;//节点名称 pu_order.采购订单
            if (name == "pu_order")
            {  
                //采购订单
                pu_order = new List<Sync_Table_PurchaseOrder>();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Sync_Table_PurchaseOrder dto = new Sync_Table_PurchaseOrder();
                    //表头
                    dto.voucherdate = dt.Rows[i]["单据日期"].ToString();
                    dto.clerk_name = dt.Rows[i]["采购员"].ToString();
                    dto.partner_name = dt.Rows[i]["供应商"].ToString();
                    //表体
                    dto.pubuserdefdecm2 = dt.Rows[i]["整票吨数"].ToDecimalIfNull();
                    dto.cinvname = dt.Rows[i]["石材品种"].ToString();
                    dto.cinvcode = dt.Rows[i]["荒料编号"].ToString();
                    dto.priuserdefnvc1 = dt.Rows[i]["等级"].ToString();

                    dto.freeItem1 = dt.Rows[i]["长"].ToString();
                    dto.freeItem2 = dt.Rows[i]["宽"].ToString();
                    dto.freeItem3 = dt.Rows[i]["厚"].ToString();

                    dto.quantity = dt.Rows[i]["方数"].ToDecimalIfNull();
                    dto.pubuserdefdecm1 = dt.Rows[i]["吨数"].ToDecimalIfNull();
                    dto.quantity2 = dt.Rows[i]["实重"].ToDecimalIfNull();
                    dto.price = dt.Rows[i]["单价"].ToDecimalIfNull();
                    dto.amount = dt.Rows[i]["金额"].ToDecimalIfNull();


                    pu_order.Add(dto);
                }
                CreateInventory();//不存在存货则添加
                truncateTable(name);//truncate
                foreach (Sync_Table_PurchaseOrder item in pu_order)
                {
                    sqlText += string.Format(@"INSERT INTO Sync_Table_PurchaseOrder
                               (voucherdate, clerk_name, partner_name, pubuserdefdecm2, cinvname, cinvcode, priuserdefnvc1, freeItem1, freeItem2, freeItem3, quantity, pubuserdefdecm1, quantity2, price,amount)
                               VALUES
                               ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')",
                                item.voucherdate, item.clerk_name, item.partner_name, item.pubuserdefdecm2, item.cinvname, item.cinvcode, item.priuserdefnvc1, item.freeItem1, item.freeItem2, item.freeItem3, item.quantity, item.pubuserdefdecm1, item.quantity2, item.price, item.amount);
                                }
                db.ExecuteUpdate(sqlText);
                MessageBox.Show("导入成功！");
            }
            conn.Close();//关闭excel

            Refreshdgv();
        }

        private void ExcelImport_Load(object sender, EventArgs e)
        {
            truncateTable();
        }

        private void truncateTable(string type= "all")
        {
            if (type == "pu_order")//采购订单
            {
                db.ExecuteUpdate("truncate table Sync_Table_PurchaseOrder");
            }
            else {//清除所有
                db.ExecuteUpdate("truncate table Sync_Table_PurchaseOrder");
            }
        }
        private void Refreshdgv()
        {
            this.dgvImport.DataSource = pu_order;
        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            truncateTable();
            Refreshdgv();
        }

        private void 同步ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvImport.DataSource == null)
            {
                MessageBox.Show("请先导入Excel数据，在进行同步操作！");
                return;
            }
            string sql = "";
            string TextMsg = "";
            string msg = "";
            int succeed = 0;
            int defeated = 0;
            sql = "select distinct code from HM_DataImportInfo";
            DataSet ds = db.ExecuteSelect(sql);

            foreach (DataRow item in ds.Tables[0].Rows)
            {
                //添加存储过程参数
                SqlParameter[] parms =
            {
                new SqlParameter("@ResultStr",SqlDbType.NVarChar, 300),
                new SqlParameter("@code",SqlDbType.NVarChar, 50),
            };
                parms[0].Direction = ParameterDirection.Output;
                parms[1].Value = item["code"];
                DataSet proc_ds = db.DB_Query("", parms);
                msg = (String)parms[0].Value;
                if (msg.Contains("同步成功"))
                {
                    succeed++;
                }
                else
                {
                    defeated++;
                    TextMsg += msg + "\r\n";
                }
            }
            MessageBox.Show("成功：" + succeed + "次，失败：" + defeated + "次\r\n" + TextMsg);
            if (defeated == 0)
            {
                truncateTable();
                Refreshdgv();
            }
        }
        /// <summary>
        /// 生成存货档案
        /// </summary>
        private void CreateInventory()
        {
            string sql = "";
            foreach (var item in pu_order)
            {
                sql = "select id from AA_Inventory where code='" + item.cinvcode + "'";
                int count = Convert.ToInt32(db.getValue(sql));
                if (count <= 0)
                {
                    //查询存货分类id
                    string str_class = "select id from dbo.AA_InventoryClass where name='荒料'";
                    int idinventoryclass = Convert.ToInt32(db.getValue(str_class));
                    StringBuilder sqlText = new StringBuilder();
                    ImportEntity entity = new ImportEntity();
                    sqlText.Append(" insert AA_Inventory(code,name,shorthand,isLimitedWithdraw,isBatch,isQualityPeriod,isSale,isMadeSelf,isPurchase,isMaterial,");
                    sqlText.Append(" disabled, isQualityCheck, isMadeRequest, isSingleUnit,Userfreeitem7, Userfreeitem6, Userfreeitem2, Userfreeitem1, Userfreeitem9, Userfreeitem0,");
                    sqlText.Append(" Userfreeitem8, Userfreeitem5, Userfreeitem4,Userfreeitem3, MustInputFreeitem7, MustInputFreeitem2, MustInputFreeitem6, MustInputFreeitem3, MustInputFreeitem5, MustInputFreeitem4, ");
                    sqlText.Append(" MustInputFreeitem9, MustInputFreeitem1, MustInputFreeitem8, MustInputFreeitem0,HasEverChanged,isphantom, ControlRangeFreeitem0,ControlRangeFreeitem1, ControlRangeFreeitem2,ControlRangeFreeitem3, ");
                    sqlText.Append(" ControlRangeFreeitem4, ControlRangeFreeitem5, ControlRangeFreeitem6, ControlRangeFreeitem7, ControlRangeFreeitem8,ControlRangeFreeitem9,IsLaborCost,IsNew,MadeRecordDate,IsSuite, ");
                    sqlText.Append(" IsWeigh,idinventoryclass,idMarketingOrgan,idunit,idunitbymanufacture,idUnitByPurchase, idUnitByRetail, idUnitBySale, idUnitByStock,taxRate, ");
                    sqlText.Append(" unittype, valueType,madeDate, updated, createdTime, Creater,priuserdefnvc1,IsModifiedCode,WithOutBargain)");
                    sqlText.AppendFormat(" values ('{0}','{1}',dbo.FB_GetChineseSpell('{2}'),0,1,0,0,1,1,0,", item.cinvcode, item.cinvname, item.cinvname);
                    sqlText.Append(" 0,0,0,0,0,0,1,1,0,1,");
                    sqlText.Append(" 0,0,0,0,0,0,0,0,0,0,");
                    sqlText.Append(" 0,0,0,0,1,0,0,0,0,0,");
                    sqlText.Append(" 0,0,0,0,0,0,0,1,getdate(),0,");
                    sqlText.AppendFormat(" 0,'{0}',1,1,1,1,1,1,1,108,", idinventoryclass);
                    sqlText.AppendFormat(" 595,199,getdate(),getdate(),getdate(),'demo','{0}',1,0)",item.priuserdefnvc1);
                    db.ExecuteUpdate(sqlText.ToString());
                }
            }

        }

        private void dgvImport_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //设置显示的列名
            dgvImport.Columns[0].Visible = false;
            dgvImport.Columns["voucherdate"].HeaderText = "单据日期";
            dgvImport.Columns["clerk_name"].HeaderText = "采购员";
            dgvImport.Columns["partner_name"].HeaderText = "供应商";
            dgvImport.Columns["pubuserdefdecm2"].HeaderText = "整票吨数";
            dgvImport.Columns["cinvcode"].HeaderText = "石材品种";
            dgvImport.Columns["cinvname"].HeaderText = "荒料编号";
            dgvImport.Columns["priuserdefnvc1"].HeaderText = "等级";
            dgvImport.Columns["freeItem1"].HeaderText = "长";
            dgvImport.Columns["freeItem2"].HeaderText = "宽";
            dgvImport.Columns["freeItem3"].HeaderText = "厚";
            dgvImport.Columns["quantity"].HeaderText = "方数";
            dgvImport.Columns["pubuserdefdecm1"].HeaderText = "吨数";
            dgvImport.Columns["quantity2"].HeaderText = "实重";
            dgvImport.Columns["price"].HeaderText = "单价";
            dgvImport.Columns["amount"].HeaderText = "金额";
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //if (treeView1.SelectedNode.Name == "pu_order")
            //{
            //    MessageBox.Show("11");
            //}
        }

    }
}
