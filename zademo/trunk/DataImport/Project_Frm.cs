using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBHelper.MSSQL;

namespace DataImport
{
    public partial class Project_Frm : Form
    {
        DbOperate db;
        public string where_name = "";
        public Project_Frm(DbOperate db, string where_name)
        {
            this.db = db;
            this.where_name = where_name;
            InitializeComponent();
        }

        private void Project_Frm_Load(object sender, EventArgs e)
        {
            bindCbox("");
        }
        public class aa_project
        {
            public string Code { get; set; }
            public string Name { get; set; }
        }
        private void bindCbox(string projectname)
        {
            IList<aa_project> infoList = new List<aa_project>();
            DataTable dt = db.ExecuteSelect(string.Format("select * from AA_Project where (name like '%{0}%' or code like '%{0}%' or '{0}'='')", projectname)).Tables[0];
            foreach (DataRow item in dt.Rows)
            {
                aa_project project = new aa_project();
                project.Code = item["Code"].ToString();
                project.Name = item["Name"].ToString();
                infoList.Add(project);
            }
            this.dataGridView1.DataSource = infoList;
            db.Close();
            db.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var projectname = this.textBox1.Text;
            bindCbox(projectname);
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dataGridView1.Columns["Code"].HeaderText = "项目编码";
            dataGridView1.Columns["Name"].HeaderText = "项目名称";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //where_name = dataGridView1.CurrentCell.Value.ToString();
            where_name = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
            if (!string.IsNullOrEmpty(where_name))
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

    }
}
