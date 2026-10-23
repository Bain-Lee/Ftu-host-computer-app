namespace DtuTestManager
{
    partial class CHisRecordForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CHisRecordForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_CallDir = new System.Windows.Forms.ToolStripButton();
            this.btn_CallAllFile = new System.Windows.Forms.ToolStripButton();
            this.btn_raed_SelFile = new System.Windows.Forms.ToolStripButton();
            this.btn_diff_read = new System.Windows.Forms.ToolStripButton();
            this.Btn_StopReadFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnCancelAll = new System.Windows.Forms.ToolStripButton();
            this.BtnCopySave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnCurrent = new System.Windows.Forms.ToolStripButton();
            this.CurrentNameList = new System.Windows.Forms.ToolStripComboBox();
            this.BtnLeft = new System.Windows.Forms.ToolStripButton();
            this.BtnRight = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.FileTreeViewList = new System.Windows.Forms.TreeView();
            this.FileView_Panel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.FileView = new System.Windows.Forms.ListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_ID = new System.Windows.Forms.Label();
            this.label_Version = new System.Windows.Forms.Label();
            this.label_FileName = new System.Windows.Forms.Label();
            this.HisDataView_Panel = new System.Windows.Forms.Panel();
            this.HisDataView = new System.Windows.Forms.ListView();
            this.LV_File_Panel = new System.Windows.Forms.Panel();
            this.LV_File = new DtuTestManager.MyListView(this.components);
            this.Time_FreshView = new System.Windows.Forms.Timer(this.components);
            this.BtnEnableTime = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.FileView_Panel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.HisDataView_Panel.SuspendLayout();
            this.LV_File_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.PowderBlue;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_CallDir,
            this.btn_CallAllFile,
            this.btn_raed_SelFile,
            this.btn_diff_read,
            this.Btn_StopReadFile,
            this.toolStripSeparator1,
            this.BtnCancelAll,
            this.BtnCopySave,
            this.toolStripSeparator2,
            this.BtnCurrent,
            this.CurrentNameList,
            this.BtnLeft,
            this.BtnRight});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(998, 56);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn_CallDir
            // 
            this.btn_CallDir.Image = ((System.Drawing.Image)(resources.GetObject("btn_CallDir.Image")));
            this.btn_CallDir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_CallDir.Name = "btn_CallDir";
            this.btn_CallDir.Size = new System.Drawing.Size(60, 53);
            this.btn_CallDir.Text = "召唤目录";
            this.btn_CallDir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_CallDir.Click += new System.EventHandler(this.btn_CallDir_Click);
            // 
            // btn_CallAllFile
            // 
            this.btn_CallAllFile.Image = ((System.Drawing.Image)(resources.GetObject("btn_CallAllFile.Image")));
            this.btn_CallAllFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_CallAllFile.Name = "btn_CallAllFile";
            this.btn_CallAllFile.Size = new System.Drawing.Size(60, 53);
            this.btn_CallAllFile.Text = "全部读取";
            this.btn_CallAllFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_CallAllFile.Click += new System.EventHandler(this.btn_CallAllFile_Click);
            // 
            // btn_raed_SelFile
            // 
            this.btn_raed_SelFile.Image = ((System.Drawing.Image)(resources.GetObject("btn_raed_SelFile.Image")));
            this.btn_raed_SelFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_raed_SelFile.Name = "btn_raed_SelFile";
            this.btn_raed_SelFile.Size = new System.Drawing.Size(60, 53);
            this.btn_raed_SelFile.Text = "选择读取";
            this.btn_raed_SelFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_raed_SelFile.Click += new System.EventHandler(this.btn_raed_SelFile_Click);
            // 
            // btn_diff_read
            // 
            this.btn_diff_read.Image = ((System.Drawing.Image)(resources.GetObject("btn_diff_read.Image")));
            this.btn_diff_read.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_diff_read.Name = "btn_diff_read";
            this.btn_diff_read.Size = new System.Drawing.Size(60, 53);
            this.btn_diff_read.Text = "差异读取";
            this.btn_diff_read.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_diff_read.Click += new System.EventHandler(this.btn_diff_read_Click);
            // 
            // Btn_StopReadFile
            // 
            this.Btn_StopReadFile.Image = ((System.Drawing.Image)(resources.GetObject("Btn_StopReadFile.Image")));
            this.Btn_StopReadFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_StopReadFile.Name = "Btn_StopReadFile";
            this.Btn_StopReadFile.Size = new System.Drawing.Size(60, 53);
            this.Btn_StopReadFile.Text = "停止读取";
            this.Btn_StopReadFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.Btn_StopReadFile.Click += new System.EventHandler(this.Btn_StopReadFile_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 56);
            // 
            // BtnCancelAll
            // 
            this.BtnCancelAll.Image = ((System.Drawing.Image)(resources.GetObject("BtnCancelAll.Image")));
            this.BtnCancelAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnCancelAll.Name = "BtnCancelAll";
            this.BtnCancelAll.Size = new System.Drawing.Size(60, 53);
            this.BtnCancelAll.Text = "取消选择";
            this.BtnCancelAll.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnCancelAll.ToolTipText = "取消当前已经选择的文件";
            this.BtnCancelAll.Click += new System.EventHandler(this.BtnCancelAll_Click);
            // 
            // BtnCopySave
            // 
            this.BtnCopySave.Image = ((System.Drawing.Image)(resources.GetObject("BtnCopySave.Image")));
            this.BtnCopySave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnCopySave.Name = "BtnCopySave";
            this.BtnCopySave.Size = new System.Drawing.Size(60, 53);
            this.BtnCopySave.Text = "合并文件";
            this.BtnCopySave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnCopySave.ToolTipText = "将实时读取的文件拷贝至历史文件目录下";
            this.BtnCopySave.Click += new System.EventHandler(this.BtnCopySave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 56);
            // 
            // BtnCurrent
            // 
            this.BtnCurrent.Image = ((System.Drawing.Image)(resources.GetObject("BtnCurrent.Image")));
            this.BtnCurrent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnCurrent.Name = "BtnCurrent";
            this.BtnCurrent.Size = new System.Drawing.Size(60, 53);
            this.BtnCurrent.Text = "数据曲线";
            this.BtnCurrent.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnCurrent.Click += new System.EventHandler(this.BtnCurrent_Click);
            // 
            // CurrentNameList
            // 
            this.CurrentNameList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CurrentNameList.Name = "CurrentNameList";
            this.CurrentNameList.Size = new System.Drawing.Size(200, 56);
            // 
            // BtnLeft
            // 
            this.BtnLeft.Image = ((System.Drawing.Image)(resources.GetObject("BtnLeft.Image")));
            this.BtnLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnLeft.Name = "BtnLeft";
            this.BtnLeft.Size = new System.Drawing.Size(48, 53);
            this.BtnLeft.Text = "上一组";
            this.BtnLeft.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnLeft.Click += new System.EventHandler(this.BtnLeft_Click);
            // 
            // BtnRight
            // 
            this.BtnRight.Image = ((System.Drawing.Image)(resources.GetObject("BtnRight.Image")));
            this.BtnRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnRight.Name = "BtnRight";
            this.BtnRight.Size = new System.Drawing.Size(48, 53);
            this.BtnRight.Text = "下一组";
            this.BtnRight.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnRight.Click += new System.EventHandler(this.BtnRight_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 56);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.FileTreeViewList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.FileView_Panel);
            this.splitContainer1.Panel2.Controls.Add(this.HisDataView_Panel);
            this.splitContainer1.Panel2.Controls.Add(this.LV_File_Panel);
            this.splitContainer1.Size = new System.Drawing.Size(998, 430);
            this.splitContainer1.SplitterDistance = 202;
            this.splitContainer1.TabIndex = 1;
            // 
            // FileTreeViewList
            // 
            this.FileTreeViewList.BackColor = System.Drawing.Color.Azure;
            this.FileTreeViewList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileTreeViewList.Location = new System.Drawing.Point(0, 0);
            this.FileTreeViewList.Name = "FileTreeViewList";
            this.FileTreeViewList.Size = new System.Drawing.Size(202, 430);
            this.FileTreeViewList.TabIndex = 0;
            this.FileTreeViewList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.FileTreeViewList_AfterSelect);
            // 
            // FileView_Panel
            // 
            this.FileView_Panel.Controls.Add(this.tableLayoutPanel1);
            this.FileView_Panel.Location = new System.Drawing.Point(3, 76);
            this.FileView_Panel.Name = "FileView_Panel";
            this.FileView_Panel.Size = new System.Drawing.Size(545, 229);
            this.FileView_Panel.TabIndex = 6;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.FileView, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 61F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(545, 229);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // FileView
            // 
            this.FileView.BackColor = System.Drawing.Color.Azure;
            this.FileView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileView.Location = new System.Drawing.Point(3, 64);
            this.FileView.Name = "FileView";
            this.FileView.Size = new System.Drawing.Size(539, 162);
            this.FileView.TabIndex = 2;
            this.FileView.UseCompatibleStateImageBehavior = false;
            this.FileView.DoubleClick += new System.EventHandler(this.FileView_DoubleClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.Controls.Add(this.label_ID);
            this.panel1.Controls.Add(this.label_Version);
            this.panel1.Controls.Add(this.label_FileName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(539, 55);
            this.panel1.TabIndex = 3;
            // 
            // label_ID
            // 
            this.label_ID.AutoSize = true;
            this.label_ID.Location = new System.Drawing.Point(390, 23);
            this.label_ID.Name = "label_ID";
            this.label_ID.Size = new System.Drawing.Size(41, 12);
            this.label_ID.TabIndex = 0;
            this.label_ID.Text = "label1";
            // 
            // label_Version
            // 
            this.label_Version.AutoSize = true;
            this.label_Version.Location = new System.Drawing.Point(250, 23);
            this.label_Version.Name = "label_Version";
            this.label_Version.Size = new System.Drawing.Size(41, 12);
            this.label_Version.TabIndex = 0;
            this.label_Version.Text = "label1";
            // 
            // label_FileName
            // 
            this.label_FileName.AutoSize = true;
            this.label_FileName.Location = new System.Drawing.Point(29, 23);
            this.label_FileName.Name = "label_FileName";
            this.label_FileName.Size = new System.Drawing.Size(41, 12);
            this.label_FileName.TabIndex = 0;
            this.label_FileName.Text = "label1";
            // 
            // HisDataView_Panel
            // 
            this.HisDataView_Panel.Controls.Add(this.HisDataView);
            this.HisDataView_Panel.Location = new System.Drawing.Point(114, 3);
            this.HisDataView_Panel.Name = "HisDataView_Panel";
            this.HisDataView_Panel.Size = new System.Drawing.Size(121, 58);
            this.HisDataView_Panel.TabIndex = 5;
            // 
            // HisDataView
            // 
            this.HisDataView.BackColor = System.Drawing.Color.Azure;
            this.HisDataView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HisDataView.Location = new System.Drawing.Point(0, 0);
            this.HisDataView.Name = "HisDataView";
            this.HisDataView.Size = new System.Drawing.Size(121, 58);
            this.HisDataView.TabIndex = 1;
            this.HisDataView.UseCompatibleStateImageBehavior = false;
            // 
            // LV_File_Panel
            // 
            this.LV_File_Panel.Controls.Add(this.LV_File);
            this.LV_File_Panel.Location = new System.Drawing.Point(2, 3);
            this.LV_File_Panel.Name = "LV_File_Panel";
            this.LV_File_Panel.Size = new System.Drawing.Size(106, 58);
            this.LV_File_Panel.TabIndex = 4;
            // 
            // LV_File
            // 
            this.LV_File.BackColor = System.Drawing.Color.Azure;
            this.LV_File.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LV_File.FullRowSelect = true;
            this.LV_File.GridLines = true;
            this.LV_File.Location = new System.Drawing.Point(0, 0);
            this.LV_File.Name = "LV_File";
            this.LV_File.OwnerDraw = true;
            this.LV_File.ProgressColumnIndex = -1;
            this.LV_File.Size = new System.Drawing.Size(106, 58);
            this.LV_File.TabIndex = 0;
            this.LV_File.UseCompatibleStateImageBehavior = false;
            this.LV_File.View = System.Windows.Forms.View.Details;
            // 
            // Time_FreshView
            // 
            this.Time_FreshView.Interval = 50;
            this.Time_FreshView.Tick += new System.EventHandler(this.Time_FreshView_Tick);
            // 
            // BtnEnableTime
            // 
            this.BtnEnableTime.Tick += new System.EventHandler(this.BtnEnableTime_Tick);
            // 
            // CHisRecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 486);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CHisRecordForm";
            this.Text = "历史记录";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CHisRecordForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CHisRecordForm_FormClosed);
            this.Load += new System.EventHandler(this.CHisRecordForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.FileView_Panel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.HisDataView_Panel.ResumeLayout(false);
            this.LV_File_Panel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btn_CallDir;
        private System.Windows.Forms.ToolStripButton btn_CallAllFile;
        private System.Windows.Forms.ToolStripButton btn_raed_SelFile;
        private System.Windows.Forms.ToolStripButton btn_diff_read;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView FileTreeViewList;
        private MyListView LV_File;
        private System.Windows.Forms.ToolStripButton Btn_StopReadFile;
        private System.Windows.Forms.ListView HisDataView;
        private System.Windows.Forms.Timer Time_FreshView;
        private System.Windows.Forms.ListView FileView;
        private System.Windows.Forms.Timer BtnEnableTime;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton BtnCancelAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton BtnCopySave;
        private System.Windows.Forms.Panel FileView_Panel;
        private System.Windows.Forms.Panel HisDataView_Panel;
        private System.Windows.Forms.Panel LV_File_Panel;
        private System.Windows.Forms.ToolStripButton BtnCurrent;
        private System.Windows.Forms.ToolStripComboBox CurrentNameList;
        private System.Windows.Forms.ToolStripButton BtnLeft;
        private System.Windows.Forms.ToolStripButton BtnRight;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_ID;
        private System.Windows.Forms.Label label_Version;
        private System.Windows.Forms.Label label_FileName;
    }
}