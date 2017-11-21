using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataImport
{
    public partial class LoginWin : Form
    {

        public LoginWin()
        {
            InitializeComponent();
            skinEngine1.SkinFile = "Skins/Carlmness/Calmness.ssk";
            skinEngine1.SkinAllForm = true;//所有窗体均应用此皮肤
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ImportEntity entity = new ImportEntity();
            entity.Server = this.Servertbx.Text.Trim();
            entity.Database = this.Databasetbx.Text.Trim();
            entity.Uid = this.Uidtbx.Text.Trim();
            entity.Pwd = this.Pwdtbx.Text.Trim();
            entity.AddUser = this.Addusertxt.Text.Trim();
            Serialization();
            ExcelImport import = new ExcelImport(entity);
            import.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoginWin login = new LoginWin();
            this.Close();
        }
        string path = Environment.CurrentDirectory + @"\\Programmers.dat";
        private void LoginWin_Load(object sender, EventArgs e)
        {
            if (File.Exists(path))
            {
                //c#文件流读文件 
                using (FileStream fsRead = new FileStream(path, FileMode.Open))
                {
                    BinaryFormatter binFormat = new BinaryFormatter();//创建二进制序列化器

                    fsRead.Position = 0;//重置流位置

                    list = (List<ImportEntity>)binFormat.Deserialize(fsRead);//反序列化对象

                    foreach (ImportEntity entity in list)
                    {
                        this.Servertbx.Text = entity.Server.Trim();
                        this.Databasetbx.Text = entity.Database.Trim();
                        this.Uidtbx.Text = entity.Uid.Trim();
                        this.Pwdtbx.Text = entity.Pwd.Trim();
                        this.Addusertxt.Text = entity.AddUser.ToStringIfNull();
                    }

                    //Console.Read();
                }
            }
        }
        List<ImportEntity> list;
        private void Serialization()
        {
            //创建Programmer列表，并添加对象

            list = new List<ImportEntity>();

            list.Add(new ImportEntity { Server = this.Servertbx.Text.Trim(), Database = this.Databasetbx.Text.Trim(), Uid = this.Uidtbx.Text.Trim(), Pwd = this.Pwdtbx.Text.Trim(), AddUser = this.Addusertxt.Text.ToStringIfNull().Trim() });

            //使用二进制序列化对象

            Stream fStream = new FileStream(path, FileMode.Create, FileAccess.ReadWrite);

            BinaryFormatter binFormat = new BinaryFormatter();//创建二进制序列化器

            binFormat.Serialize(fStream, list);
            fStream.Close();
        }
    }
}
