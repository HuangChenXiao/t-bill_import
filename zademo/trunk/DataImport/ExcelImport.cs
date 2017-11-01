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
            bindCbox();
        }
        public class aa_project
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
        private void bindCbox()
        {
            IList<aa_project> infoList = new List<aa_project>();
            DataTable dt = db.ExecuteSelect("select * from AA_Project").Tables[0];
            aa_project info1 = new aa_project() { Id = "0", Name = "--请选择--" };
            infoList.Add(info1);
            foreach (DataRow item in dt.Rows)
            {
                info1 = new aa_project() { Id = item["id"].ToString(), Name = item["Name"].ToString() };
                infoList.Add(info1);
            }
            this.cbx_project.DataSource = infoList;
            this.cbx_project.ValueMember = "Id";
            this.cbx_project.DisplayMember = "Name";
            db.Close();
            db.Dispose();
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

            if (Convert.ToInt32(this.cbx_project.SelectedIndex) > 0)
            {
                projectname = this.cbx_project.Text;
            }
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



    }
}
