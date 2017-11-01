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
            this.treeView1.ExpandAll();
        }


        private void ExcelImport_Load(object sender, EventArgs e)
        {
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

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var name = this.treeView1.SelectedNode.Name;
            if (name == "aa_project")
            {
                this.dgvImport.Visible = true;
            }
        }

        private void btn_serach_Click(object sender, EventArgs e)
        {
            string projectname = "";

            if (!string.IsNullOrEmpty(this.txt_project.Text))
            {
                projectname = this.txt_project.Text;
            }
            ProjectAmountInfo(projectname);
        }

        private void ProjectAmountInfo(string projectname)
        {
            string sqlText = string.Format(@"select AuxiliaryItems,summary,sum(convert(float,origamountdr)) as amount from GL_Entry
                                where (AuxiliaryItems like '%{0}%' or '{0}'='' and AuxiliaryItems is not null)
                                group by AuxiliaryItems,summary", projectname);
            DataTable dt = db.ExecuteSelect(sqlText).Tables[0];
            List<Report_GL_Entry_View> clist = new List<Report_GL_Entry_View>();
            Report_GL_Entry_View cl = new Report_GL_Entry_View();
            foreach (DataRow item in dt.Rows)
            {
                string summary = item["summary"].ToString();
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
            clist.Add(cl);
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

    }
}
