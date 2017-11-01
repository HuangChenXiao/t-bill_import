namespace DataImport
{
    partial class ExcelImport
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("项目费用分析报表");
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExcelImport));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.excel导入ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.同步ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.dgvImport = new System.Windows.Forms.DataGridView();
            this.lbl_project = new System.Windows.Forms.Label();
            this.cbx_project = new System.Windows.Forms.ComboBox();
            this.btn_serach = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImport)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excel导入ToolStripMenuItem,
            this.同步ToolStripMenuItem,
            this.刷新ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(893, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.Visible = false;
            // 
            // excel导入ToolStripMenuItem
            // 
            this.excel导入ToolStripMenuItem.Name = "excel导入ToolStripMenuItem";
            this.excel导入ToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.excel导入ToolStripMenuItem.Text = "Excel导入";
            // 
            // 同步ToolStripMenuItem
            // 
            this.同步ToolStripMenuItem.Name = "同步ToolStripMenuItem";
            this.同步ToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.同步ToolStripMenuItem.Text = "同步";
            // 
            // 刷新ToolStripMenuItem
            // 
            this.刷新ToolStripMenuItem.Name = "刷新ToolStripMenuItem";
            this.刷新ToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.刷新ToolStripMenuItem.Text = "清空数据";
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(0, 3);
            this.treeView1.Name = "treeView1";
            treeNode1.Name = "aa_project";
            treeNode1.Text = "项目费用分析报表";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeView1.Size = new System.Drawing.Size(133, 465);
            this.treeView1.TabIndex = 4;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // dgvImport
            // 
            this.dgvImport.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvImport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvImport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvImport.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvImport.Location = new System.Drawing.Point(6, 33);
            this.dgvImport.Name = "dgvImport";
            this.dgvImport.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvImport.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvImport.RowTemplate.Height = 23;
            this.dgvImport.Size = new System.Drawing.Size(742, 425);
            this.dgvImport.TabIndex = 1;
            this.dgvImport.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvImport_DataBindingComplete);
            // 
            // lbl_project
            // 
            this.lbl_project.AutoSize = true;
            this.lbl_project.Location = new System.Drawing.Point(49, 10);
            this.lbl_project.Name = "lbl_project";
            this.lbl_project.Size = new System.Drawing.Size(59, 12);
            this.lbl_project.TabIndex = 5;
            this.lbl_project.Text = "项目名称:";
            // 
            // cbx_project
            // 
            this.cbx_project.FormattingEnabled = true;
            this.cbx_project.Location = new System.Drawing.Point(113, 5);
            this.cbx_project.Name = "cbx_project";
            this.cbx_project.Size = new System.Drawing.Size(121, 20);
            this.cbx_project.TabIndex = 6;
            // 
            // btn_serach
            // 
            this.btn_serach.Location = new System.Drawing.Point(240, 3);
            this.btn_serach.Name = "btn_serach";
            this.btn_serach.Size = new System.Drawing.Size(75, 23);
            this.btn_serach.TabIndex = 7;
            this.btn_serach.Text = "查询";
            this.btn_serach.UseVisualStyleBackColor = true;
            this.btn_serach.Click += new System.EventHandler(this.btn_serach_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvImport);
            this.panel1.Controls.Add(this.btn_serach);
            this.panel1.Controls.Add(this.cbx_project);
            this.panel1.Controls.Add(this.lbl_project);
            this.panel1.Location = new System.Drawing.Point(139, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(755, 465);
            this.panel1.TabIndex = 8;
            // 
            // ExcelImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 471);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "ExcelImport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据同步";
            this.Load += new System.EventHandler(this.ExcelImport_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImport)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem excel导入ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 同步ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 刷新ToolStripMenuItem;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.DataGridView dgvImport;
        private System.Windows.Forms.Label lbl_project;
        private System.Windows.Forms.ComboBox cbx_project;
        private System.Windows.Forms.Button btn_serach;
        private System.Windows.Forms.Panel panel1;
    }
}

