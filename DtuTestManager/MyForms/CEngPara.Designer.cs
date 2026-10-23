namespace DtuTestManager
{
    partial class CEngPara
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CEngPara));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.BtnAllDown = new System.Windows.Forms.ToolStripButton();
            this.BtnAllRead = new System.Windows.Forms.ToolStripButton();
            this.BtnNewFile = new System.Windows.Forms.ToolStripButton();
            this.BtnEdit = new System.Windows.Forms.ToolStripButton();
            this.BtnModPara = new System.Windows.Forms.ToolStripButton();
            this.BtnSavePara = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnSelectAll = new System.Windows.Forms.ToolStripButton();
            this.BtnCancel = new System.Windows.Forms.ToolStripButton();
            this.BtnSelectNo = new System.Windows.Forms.ToolStripButton();
            this.ParaFileTree = new System.Windows.Forms.TreeView();
            this.ParaLV = new System.Windows.Forms.ListView();
            this.TaskTime = new System.Windows.Forms.Timer(this.components);
            this.FreshTime = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Azure;
            this.toolStrip1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnAllDown,
            this.BtnAllRead,
            this.BtnNewFile,
            this.BtnEdit,
            this.BtnModPara,
            this.BtnSavePara,
            this.toolStripSeparator1,
            this.BtnSelectAll,
            this.BtnCancel,
            this.BtnSelectNo});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1020, 28);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // BtnAllDown
            // 
            this.BtnAllDown.Image = ((System.Drawing.Image)(resources.GetObject("BtnAllDown.Image")));
            this.BtnAllDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnAllDown.Name = "BtnAllDown";
            this.BtnAllDown.Size = new System.Drawing.Size(94, 25);
            this.BtnAllDown.Text = "全部下发";
            this.BtnAllDown.Click += new System.EventHandler(this.BtnAllDown_Click);
            // 
            // BtnAllRead
            // 
            this.BtnAllRead.Image = ((System.Drawing.Image)(resources.GetObject("BtnAllRead.Image")));
            this.BtnAllRead.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnAllRead.Name = "BtnAllRead";
            this.BtnAllRead.Size = new System.Drawing.Size(94, 25);
            this.BtnAllRead.Text = "全部召唤";
            this.BtnAllRead.Click += new System.EventHandler(this.BtnAllRead_Click);
            // 
            // BtnNewFile
            // 
            this.BtnNewFile.Image = ((System.Drawing.Image)(resources.GetObject("BtnNewFile.Image")));
            this.BtnNewFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnNewFile.Name = "BtnNewFile";
            this.BtnNewFile.Size = new System.Drawing.Size(94, 25);
            this.BtnNewFile.Text = "新建文件";
            this.BtnNewFile.Click += new System.EventHandler(this.BtnNewFile_Click);
            // 
            // BtnEdit
            // 
            this.BtnEdit.Image = ((System.Drawing.Image)(resources.GetObject("BtnEdit.Image")));
            this.BtnEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnEdit.Name = "BtnEdit";
            this.BtnEdit.Size = new System.Drawing.Size(94, 25);
            this.BtnEdit.Text = "编辑文件";
            this.BtnEdit.Click += new System.EventHandler(this.BtnEdit_Click);
            // 
            // BtnModPara
            // 
            this.BtnModPara.Image = ((System.Drawing.Image)(resources.GetObject("BtnModPara.Image")));
            this.BtnModPara.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnModPara.Name = "BtnModPara";
            this.BtnModPara.Size = new System.Drawing.Size(94, 25);
            this.BtnModPara.Text = "修改参数";
            this.BtnModPara.Click += new System.EventHandler(this.BtnModPara_Click);
            // 
            // BtnSavePara
            // 
            this.BtnSavePara.Image = ((System.Drawing.Image)(resources.GetObject("BtnSavePara.Image")));
            this.BtnSavePara.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnSavePara.Name = "BtnSavePara";
            this.BtnSavePara.Size = new System.Drawing.Size(94, 25);
            this.BtnSavePara.Text = "保存参数";
            this.BtnSavePara.Click += new System.EventHandler(this.BtnSavePara_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // BtnSelectAll
            // 
            this.BtnSelectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BtnSelectAll.Image = ((System.Drawing.Image)(resources.GetObject("BtnSelectAll.Image")));
            this.BtnSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnSelectAll.Name = "BtnSelectAll";
            this.BtnSelectAll.Size = new System.Drawing.Size(78, 25);
            this.BtnSelectAll.Text = "全部选中";
            this.BtnSelectAll.Click += new System.EventHandler(this.BtnSelectAll_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BtnCancel.Image = ((System.Drawing.Image)(resources.GetObject("BtnCancel.Image")));
            this.BtnCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(78, 25);
            this.BtnCancel.Text = "全部取消";
            this.BtnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // BtnSelectNo
            // 
            this.BtnSelectNo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.BtnSelectNo.Image = ((System.Drawing.Image)(resources.GetObject("BtnSelectNo.Image")));
            this.BtnSelectNo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnSelectNo.Name = "BtnSelectNo";
            this.BtnSelectNo.Size = new System.Drawing.Size(78, 25);
            this.BtnSelectNo.Text = "反向选择";
            this.BtnSelectNo.Click += new System.EventHandler(this.BtnSelectNo_Click);
            // 
            // ParaFileTree
            // 
            this.ParaFileTree.BackColor = System.Drawing.Color.Lavender;
            this.ParaFileTree.Dock = System.Windows.Forms.DockStyle.Left;
            this.ParaFileTree.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ParaFileTree.Location = new System.Drawing.Point(0, 28);
            this.ParaFileTree.Name = "ParaFileTree";
            this.ParaFileTree.Size = new System.Drawing.Size(271, 478);
            this.ParaFileTree.TabIndex = 1;
            this.ParaFileTree.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.ParaFileTree_DrawNode);
            this.ParaFileTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ParaFileTree_AfterSelect);
            // 
            // ParaLV
            // 
            this.ParaLV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParaLV.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ParaLV.Location = new System.Drawing.Point(271, 28);
            this.ParaLV.Name = "ParaLV";
            this.ParaLV.Size = new System.Drawing.Size(749, 478);
            this.ParaLV.TabIndex = 2;
            this.ParaLV.UseCompatibleStateImageBehavior = false;
            // 
            // TaskTime
            // 
            this.TaskTime.Interval = 50;
            this.TaskTime.Tick += new System.EventHandler(this.TaskTime_Tick);
            // 
            // FreshTime
            // 
            this.FreshTime.Interval = 50;
            this.FreshTime.Tick += new System.EventHandler(this.FreshTime_Tick);
            // 
            // CEngPara
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 506);
            this.Controls.Add(this.ParaLV);
            this.Controls.Add(this.ParaFileTree);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CEngPara";
            this.Text = "工程参数";
            this.Load += new System.EventHandler(this.CEngPara_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton BtnAllDown;
        private System.Windows.Forms.ToolStripButton BtnAllRead;
        private System.Windows.Forms.ToolStripButton BtnNewFile;
        private System.Windows.Forms.ToolStripButton BtnEdit;
        private System.Windows.Forms.TreeView ParaFileTree;
        private System.Windows.Forms.ListView ParaLV;
        private System.Windows.Forms.Timer TaskTime;
        private System.Windows.Forms.ToolStripButton BtnModPara;
        private System.Windows.Forms.ToolStripButton BtnSavePara;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton BtnSelectAll;
        private System.Windows.Forms.ToolStripButton BtnCancel;
        private System.Windows.Forms.ToolStripButton BtnSelectNo;
        private System.Windows.Forms.Timer FreshTime;
    }
}