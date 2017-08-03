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

namespace DataImport
{
    public partial class ExcelImport : Form
    {
        string str;
        SqlConnection sqlconn;
        ImportEntity iEntity;
        public ExcelImport(ImportEntity entity)
        {
            InitializeComponent();
            iEntity = entity;
            str = string.Format("server={0};database={1};uid={2};pwd={3}", entity.Server, entity.Database, entity.Uid, entity.Pwd, entity.AddUser);
            sqlconn = new SqlConnection(str.ToString());
        }


        //`1q2w3e4r  UFTData624139_000001
        //SqlConnection sqlconn = new SqlConnection("server=.;database=UFTData29408_000001;uid=sa;pwd=sa");
        SqlCommand cmd;
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
            dtoList = new List<HM_DataImportInfo>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                HM_DataImportInfo dto = new HM_DataImportInfo();
                //表头
                dto.priuserdefnvc1 = dt.Rows[i]["销售属性"].ToString();
                dto.priuserdefnvc2 = dt.Rows[i]["物流公司运单号"].ToString();
                dto.code = dt.Rows[i]["单据号"].ToString();
                dto.warehouse = dt.Rows[i]["仓库"].ToString();
                dto.ccusname = dt.Rows[i]["买家昵称"].ToString();
                dto.linkman = dt.Rows[i]["收货人姓名"].ToString();
                dto.address = dt.Rows[i]["收货人详细地址"].ToString();
                dto.contactphone = dt.Rows[i]["收货人电话"].ToString();
                dto.memo = dt.Rows[i]["买家留言"].ToString();
                dto.priuserdefnvc3 = dt.Rows[i]["卖家备注"].ToString();
                dto.saleInvoiceNo = dt.Rows[i]["发票"].ToString();
                //表体
                dto.inventoryname = dt.Rows[i]["商品名称"].ToString();
                dto.idinventory = dt.Rows[i]["商品编码"].ToString();
                dto.quantity = dt.Rows[i]["数量"].ToDecimalIfNull();
                dto.priuserdefdecm1 = dt.Rows[i]["运费"].ToDecimalIfNull();
                dto.taxamount = dt.Rows[i]["订单金额"].ToDecimalIfNull();
                dtoList.Add(dto);
            }

            if (sqlconn.State == ConnectionState.Closed)
            {
                sqlconn.Open();
            }
            foreach (HM_DataImportInfo item in dtoList)
            {
                sqlText += string.Format(@"INSERT INTO [HM_DataImportInfo]
               (code,ccusname,linkman, address,contactphone, priuserdefnvc1, priuserdefnvc2, saleInvoiceNo, memo,idinventory,inventoryname,retailprice, quantity, priuserdefdecm1, taxamount,maker,priuserdefnvc3,warehouse)
               VALUES
               ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')",
               item.code, item.ccusname, item.linkman, item.address, item.contactphone, item.priuserdefnvc1, item.priuserdefnvc2, item.saleInvoiceNo, item.memo, item.idinventory, item.inventoryname, item.retailprice, item.quantity, item.priuserdefdecm1, item.taxamount, iEntity.AddUser, item.priuserdefnvc3, item.warehouse);
            }
            OleDbTransaction tran = conn.BeginTransaction();
            try
            {
                cmd = new SqlCommand(sqlText, sqlconn);
                cmd.ExecuteNonQuery();
                tran.Commit();
                MessageBox.Show("导入成功！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                tran.Rollback();
            }
            Refreshdgv();
            sqlconn.Close();
            conn.Close();
        }

        private void ExcelImport_Load(object sender, EventArgs e)
        {
            truncateTable();
        }

        private void truncateTable()
        {
            if (sqlconn.State == ConnectionState.Closed)
            {
                sqlconn.Open();
            }
            dtoList = null;
            sqlText = "truncate table [HM_DataImportInfo]";
            cmd = new SqlCommand(sqlText, sqlconn);
            cmd.ExecuteNonQuery();
        }
        List<HM_DataImportInfo> dtoList;
        private void Refreshdgv()
        {
            //string sqlText = "select * from HM_DataImportInfo ";
            //cmd = new SqlCommand(sqlText, sqlconn);
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //DataSet ds = new DataSet();
            //adapter.Fill(ds);
            //dtoList = new List<HM_DataImportInfo>();
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            //    {
            //        HM_DataImportInfo dto = new HM_DataImportInfo();
            //        dto.code = ds.Tables[0].Rows[i]["code"].ToString();
            //        dto.ccusname = ds.Tables[0].Rows[i]["ccusname"].ToString();
            //        dto.address = ds.Tables[0].Rows[i]["address"].ToString();
            //        dto.contactphone = ds.Tables[0].Rows[i]["contactphone"].ToString();
            //        dto.priuserdefnvc1 = ds.Tables[0].Rows[i]["priuserdefnvc1"].ToString();
            //        dto.priuserdefnvc2 = ds.Tables[0].Rows[i]["priuserdefnvc2"].ToString();
            //        dto.priuserdefnvc3 = ds.Tables[0].Rows[i]["priuserdefnvc3"].ToString();
            //        dto.saleInvoiceNo = ds.Tables[0].Rows[i]["saleInvoiceNo"].ToString();
            //        dto.memo = ds.Tables[0].Rows[i]["memo"].ToString();

            //        //dto.idinventory = ds.Tables[0].Rows[i]["idinventory"].ToString();
            //        //dto.inventoryname = ds.Tables[0].Rows[i]["inventoryname"].ToString();
            //        //dto.quantity = ds.Tables[0].Rows[i]["quantity"].ToDecimalIfNull();
            //        //dto.priuserdefdecm1 = ds.Tables[0].Rows[i]["priuserdefdecm1"].ToDecimalIfNull();
            //        //dto.taxamount = ds.Tables[0].Rows[i]["taxamount"].ToDecimalIfNull();

            //        dtoList.Add(dto);
            //    }
            //}
            Console.Write(dtoList);
            this.dgvImport.DataSource = dtoList;
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
            int succeed = 0;
            int defeated = 0;
            sql = "select distinct code from HM_DataImportInfo";
            cmd = new SqlCommand(sql, sqlconn);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                if (sqlconn.State != ConnectionState.Open)
                    sqlconn.Open();
                //添加存储过程参数
                SqlParameter[] parms = 
            {
                new SqlParameter("@ResultStr",SqlDbType.NVarChar, 300),
                new SqlParameter("@code",SqlDbType.NVarChar, 50),
            };
                parms[0].Direction = ParameterDirection.Output;
                parms[1].Value = item["code"];

                SqlCommand sqlcomm = new SqlCommand("Sync_SA_SaleDelivery", sqlconn);
                sqlcomm.CommandType = CommandType.StoredProcedure;
                sqlcomm.Parameters.AddRange(parms);
                sqlcomm.ExecuteReader();
                if (sqlconn.State != ConnectionState.Closed)
                {
                    sqlconn.Close();
                }
                string msg = (String)parms[0].Value;
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
        private void CreateInventory(int idclass)
        {
            string sql = "";
            foreach (var item in dtoList)
            {
                sql = "select id from AA_Inventory where code='" + item.idinventory + "'";
                cmd = new SqlCommand(sql, sqlconn);
                int count = cmd.ExecuteScalar() == null ? 0 : Convert.ToInt32(cmd.ExecuteScalar());
                if (count <= 0)
                {
                    StringBuilder sqlText = new StringBuilder();
                    ImportEntity entity = new ImportEntity();
                    sqlText.Append(" insert AA_Inventory(code,name,shorthand,isLimitedWithdraw,isBatch,isQualityPeriod,isSale,isMadeSelf,isPurchase,isMaterial,");
                    sqlText.Append(" disabled, isQualityCheck, isMadeRequest, isSingleUnit,Userfreeitem7, Userfreeitem6, Userfreeitem2, Userfreeitem1, Userfreeitem9, Userfreeitem0,");
                    sqlText.Append(" Userfreeitem8, Userfreeitem5, Userfreeitem4,Userfreeitem3, MustInputFreeitem7, MustInputFreeitem2, MustInputFreeitem6, MustInputFreeitem3, MustInputFreeitem5, MustInputFreeitem4, ");
                    sqlText.Append(" MustInputFreeitem9, MustInputFreeitem1, MustInputFreeitem8, MustInputFreeitem0,HasEverChanged,isphantom, ControlRangeFreeitem0,ControlRangeFreeitem1, ControlRangeFreeitem2,ControlRangeFreeitem3, ");
                    sqlText.Append(" ControlRangeFreeitem4, ControlRangeFreeitem5, ControlRangeFreeitem6, ControlRangeFreeitem7, ControlRangeFreeitem8,ControlRangeFreeitem9,IsLaborCost,IsNew,MadeRecordDate,IsSuite, ");
                    sqlText.Append(" IsWeigh,idinventoryclass,idMarketingOrgan,idunit,idunitbymanufacture,idUnitByPurchase, idUnitByRetail, idUnitBySale, idUnitByStock,taxRate, ");
                    sqlText.Append(" unittype, valueType,madeDate, updated, createdTime, Creater)");
                    sqlText.AppendFormat(" values ('{0}','{1}',dbo.fnpbGetPYFirstLetter('{2}'),0,0,0,1,1,1,1,", item.idinventory, item.inventoryname, item.inventoryname);
                    sqlText.Append(" 0,0,0,1,0,0,0,0,0,0,");
                    sqlText.Append(" 0,0,0,0,0,0,0,0,0,0,");
                    sqlText.Append(" 0,0,0,0,1,0,0,0,0,0,");
                    sqlText.Append(" 0,0,0,0,0,0,0,1,getdate(),0,");
                    sqlText.AppendFormat(" 0,'{0}',1,3,3,3,3,3,3,179,", idclass);
                    sqlText.Append(" 595,360,getdate(),getdate(),getdate(),'demo')");
                    cmd = new SqlCommand(sqlText.ToString(), sqlconn);
                    cmd.ExecuteNonQuery();
                }
            }

        }

        private void dgvImport_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //设置显示的列名
            dgvImport.Columns[0].Visible = false;
            dgvImport.Columns[1].Visible = false;
            dgvImport.Columns["code"].HeaderText = "单据号";
            dgvImport.Columns["warehouse"].HeaderText = "仓库";
            dgvImport.Columns["ccusname"].HeaderText = "买家昵称";
            dgvImport.Columns["linkman"].HeaderText = "收货人姓名";
            dgvImport.Columns["address"].HeaderText = "收货人详细地址";
            dgvImport.Columns["contactphone"].HeaderText = "收货人电话";
            dgvImport.Columns["inventoryname"].HeaderText = "商品名称";
            dgvImport.Columns["idinventory"].HeaderText = "商品编码";
            dgvImport.Columns["priuserdefnvc1"].HeaderText = "销售属性";
            dgvImport.Columns["quantity"].HeaderText = "数量";
            dgvImport.Columns["priuserdefdecm1"].HeaderText = "运费";
            dgvImport.Columns["taxamount"].HeaderText = "订单金额";
            dgvImport.Columns["memo"].HeaderText = "买家留言";
            dgvImport.Columns["priuserdefnvc3"].HeaderText = "卖家备注";
            dgvImport.Columns["priuserdefnvc2"].HeaderText = "物流公司运单号";
            dgvImport.Columns["saleInvoiceNo"].HeaderText = "发票";
        }

    }
}
