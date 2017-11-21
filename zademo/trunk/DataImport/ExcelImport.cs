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

        public ExcelImport(ImportEntity entity)
        {
            InitializeComponent();
            iEntity = entity;
            str = string.Format("server={0};database={1};uid={2};pwd={3}", entity.Server, entity.Database, entity.Uid, entity.Pwd, entity.AddUser);
            db = new DbOperate(str);
        }


        private void ExcelImport_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime d1 = new DateTime(now.Year, now.Month, 1);
            DateTime d2 = d1.AddMonths(1).AddDays(-1);
            this.start_Picker.Value = Convert.ToDateTime(d1.ToString("yyyy-MM-dd"));
            this.end_Picker.Value = Convert.ToDateTime(d2.ToString("yyyy-MM-dd"));
            this.start_hetong_date.Value = Convert.ToDateTime(d1.ToString("yyyy-MM-dd"));
            this.end_hetong_date.Value = Convert.ToDateTime(d2.ToString("yyyy-MM-dd"));
        }


        private void dgvImport_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //设置显示的列名
            dgvImport.Columns["projectname"].HeaderText = "项目名称";
            dgvImport.Columns["yunfei"].HeaderText = "运费";
            dgvImport.Columns["shigongfei"].HeaderText = "施工费（人工）";
            dgvImport.Columns["gongzi"].HeaderText = "工资（人工）";
            dgvImport.Columns["zhaodaifei"].HeaderText = "业务招待费";
            dgvImport.Columns["jiaotongfei"].HeaderText = "交通费";
            dgvImport.Columns["xinxinfei"].HeaderText = "信息费";
            dgvImport.Columns["guanlifei"].HeaderText = "管理费";
            dgvImport.Columns["chailvfei"].HeaderText = "差旅费";
            dgvImport.Columns["shuijin"].HeaderText = "税金";
            dgvImport.Columns["shejifei"].HeaderText = "设计费";
            dgvImport.Columns["xiangmufei"].HeaderText = "项目费";
            dgvImport.Columns["qita"].HeaderText = "其他";

        }


        private void btn_serach_Click(object sender, EventArgs e)
        {
            string projectname = "";
            string start_date = "";
            string end_date = "";
            if (!string.IsNullOrEmpty(this.txt_project.Text))
            {
                projectname = this.txt_project.Text;
            }
            if (!string.IsNullOrEmpty(this.start_Picker.Value.ToString()))
            {
                start_date = DateTime.Now.ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrEmpty(this.end_Picker.Value.ToString()))
            {
                end_date = DateTime.Now.ToString("yyyy-MM-dd");
            }

            ProjectAmountInfo(projectname);
        }

        private void ProjectAmountInfo(string projectname)
        {
            string start_date = "";
            string end_date = "";
            if (!string.IsNullOrEmpty(this.start_Picker.Value.ToString()))
            {
                start_date = this.start_Picker.Value.ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrEmpty(this.end_Picker.Value.ToString()))
            {
                end_date = this.end_Picker.Value.ToString("yyyy-MM-dd");
            }
            string sqlText = string.Format(@"select b.AuxiliaryItems,b.summary,sum(convert(float,b.origamountdr)) as amount 
                                            from GL_doc a inner join GL_Entry b on a.id=b.idDocDTO
                                            where (b.AuxiliaryItems like '%{0}%' or '{0}'='' and b.AuxiliaryItems is not null)
                                            and   (a.voucherdate>='{1}' and a.voucherdate<='{2}')
                                            group by b.AuxiliaryItems,b.summary", projectname, start_date, end_date);
            DataTable dt = db.ExecuteSelect(sqlText).Tables[0];
            List<Report_GL_Entry_View> clist = new List<Report_GL_Entry_View>();
            Report_GL_Entry_View cl = new Report_GL_Entry_View();
            var name = "";
            foreach (DataRow item in dt.Rows)
            {
                string summary = item["summary"].ToString();
                if (string.IsNullOrEmpty(name))
                {
                    name = item["AuxiliaryItems"].ToString();
                    cl.projectname = name;
                    clist.Add(cl);
                }
                if (name == item["AuxiliaryItems"].ToString())
                {
                    switch (summary)
                    {
                        case "运费":
                            cl.yunfei = item["amount"].ToString();
                            break;
                        case "施工费（人工）":
                            cl.shigongfei = item["amount"].ToString();
                            break;
                        case "工资（人工）":
                            cl.gongzi = item["amount"].ToString();
                            break;
                        case "业务招待费":
                            cl.zhaodaifei = item["amount"].ToString();
                            break;
                        case "交通费":
                            cl.jiaotongfei = item["amount"].ToString();
                            break;
                        case "信息费":
                            cl.xinxinfei = item["amount"].ToString();
                            break;
                        case "管理费":
                            cl.guanlifei = item["amount"].ToString();
                            break;
                        case "差旅费":
                            cl.chailvfei = item["amount"].ToString();
                            break;
                        case "税金":
                            cl.shuijin = item["amount"].ToString();
                            break;
                        case "设计费":
                            cl.shejifei = item["amount"].ToString();
                            break;
                        case "项目费":
                            cl.xiangmufei = item["amount"].ToString();
                            break;
                        case "其他":
                            cl.qita = item["amount"].ToString();
                            break;
                    }
                }
                else {
                    cl.projectname = name;
                    clist.Add(cl);
                    name = item["AuxiliaryItems"].ToString();
                }
            }
            this.dgvImport.DataSource = clist;
            db.Close();
            db.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string where_name = "";
            //Project_Frm pro = new Project_Frm(db, where_name);
            //pro.ShowDialog();
            using (Project_Frm pro = new Project_Frm(db, where_name))
            {
                if (pro.ShowDialog() == DialogResult.OK)
                {
                    this.txt_project.Text = pro.where_name;
                    ProjectAmountInfo(pro.where_name);
                }
            }
        }
        private void ProjectAmountHeTong(string projectname)
        {
            string start_date = "";
            string end_date = "";
            if (!string.IsNullOrEmpty(this.start_hetong_date.Value.ToString()))
            {
                start_date = this.start_hetong_date.Value.ToString("yyyy-MM-dd");
            }
            if (!string.IsNullOrEmpty(this.end_hetong_date.Value.ToString()))
            {
                end_date = this.end_hetong_date.Value.ToString("yyyy-MM-dd");
            }
            string sqlText = string.Format(@"exec PROC_getProjectAmount '{0}','{1}','{2}'", projectname, start_date, end_date);
            DataTable dt = db.ExecuteSelect(sqlText).Tables[0];
            List<Report_ProjectAmount> clist = new List<Report_ProjectAmount>();
            foreach (DataRow item in dt.Rows)
            {
                Report_ProjectAmount cl = new Report_ProjectAmount();
                cl.name = item["name"].ToString();
                cl.xinhetong = item["xinhetong"].ToString();
                cl.buhetong = item["buhetong"].ToString();
                cl.SAInvoicetaxAmount = item["SAInvoicetaxAmount"].ToString();
                cl.SA_origSettleAmount = item["SA_origSettleAmount"].ToString();
                cl.hetongAmount = item["hetongAmount"].ToString();
                cl.CaiLiaoAmount = item["CaiLiaoAmount"].ToString();
                cl.JieKuanAmount = item["JieKuanAmount"].ToString();
                cl.PUInvoicetaxAmount = item["PUInvoicetaxAmount"].ToString();
                cl.PU_Amount = item["PU_Amount"].ToString();
                clist.Add(cl);
            }
            this.dataGridView1.DataSource = clist;
            db.Close();
            db.Dispose();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            string projectname = "";
            if (!string.IsNullOrEmpty(this.txt_project_hetong.Text))
            {
                projectname = this.txt_project_hetong.Text;
            }
            ProjectAmountHeTong(projectname);
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //设置显示的列名
            dataGridView1.Columns["name"].HeaderText = "项目名称";
            dataGridView1.Columns["xinhetong"].HeaderText = "新项目合同";
            dataGridView1.Columns["buhetong"].HeaderText = "增补合同";
            dataGridView1.Columns["SAInvoicetaxAmount"].HeaderText = "已开票金额";
            dataGridView1.Columns["SA_origSettleAmount"].HeaderText = "收款";
            dataGridView1.Columns["hetongAmount"].HeaderText = "应收金额";
            dataGridView1.Columns["CaiLiaoAmount"].HeaderText = "材料成本";
            dataGridView1.Columns["JieKuanAmount"].HeaderText = "付款";
            dataGridView1.Columns["PUInvoicetaxAmount"].HeaderText = "到票金额";
            dataGridView1.Columns["PU_Amount"].HeaderText = "应付金额";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string where_name = "";
            //Project_Frm pro = new Project_Frm(db, where_name);
            //pro.ShowDialog();
            using (Project_Frm pro = new Project_Frm(db, where_name))
            {
                if (pro.ShowDialog() == DialogResult.OK)
                {
                    this.txt_project_hetong.Text = pro.where_name;
                    ProjectAmountHeTong(pro.where_name);
                }
            }
        }
    }
}
