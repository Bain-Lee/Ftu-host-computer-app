namespace DtuTestManager
{
    partial class CSoePicForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CSoePicForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.ComBox_X = new System.Windows.Forms.ToolStripComboBox();
            this.YHeightSet = new System.Windows.Forms.ToolStripLabel();
            this.LineSel = new System.Windows.Forms.ToolStripComboBox();
            this.ComBox_Y = new System.Windows.Forms.ToolStripComboBox();
            this.BtnReset = new System.Windows.Forms.ToolStripButton();
            this.BtnOpenFile = new System.Windows.Forms.ToolStripButton();
            this.StartTime = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.EndTime = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.LeftTime = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.RightTime = new System.Windows.Forms.ToolStripLabel();
            this.PicMainPanel = new System.Windows.Forms.Panel();
            this.CurrentPicPanel = new System.Windows.Forms.Panel();
            this.CurrentPicBox = new System.Windows.Forms.PictureBox();
            this.SoeNamePanel = new System.Windows.Forms.Panel();
            this.FreshTimer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ShowMsg = new System.Windows.Forms.ToolStripMenuItem();
            this.HideMsg = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1.SuspendLayout();
            this.PicMainPanel.SuspendLayout();
            this.CurrentPicPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentPicBox)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.ComBox_X,
            this.YHeightSet,
            this.LineSel,
            this.ComBox_Y,
            this.BtnReset,
            this.BtnOpenFile,
            this.StartTime,
            this.toolStripSeparator1,
            this.EndTime,
            this.toolStripSeparator2,
            this.LeftTime,
            this.toolStripSeparator3,
            this.RightTime});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(885, 40);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(68, 37);
            this.toolStripLabel1.Text = "横向比例：";
            // 
            // ComBox_X
            // 
            this.ComBox_X.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComBox_X.Name = "ComBox_X";
            this.ComBox_X.Size = new System.Drawing.Size(80, 40);
            this.ComBox_X.SelectedIndexChanged += new System.EventHandler(this.ComBox_X_SelectedIndexChanged);
            // 
            // YHeightSet
            // 
            this.YHeightSet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.YHeightSet.Image = ((System.Drawing.Image)(resources.GetObject("YHeightSet.Image")));
            this.YHeightSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.YHeightSet.Name = "YHeightSet";
            this.YHeightSet.Size = new System.Drawing.Size(68, 37);
            this.YHeightSet.Text = "纵向比例：";
            // 
            // LineSel
            // 
            this.LineSel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LineSel.Name = "LineSel";
            this.LineSel.Size = new System.Drawing.Size(121, 40);
            this.LineSel.SelectedIndexChanged += new System.EventHandler(this.LineSel_SelectedIndexChanged);
            // 
            // ComBox_Y
            // 
            this.ComBox_Y.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComBox_Y.Name = "ComBox_Y";
            this.ComBox_Y.Size = new System.Drawing.Size(80, 40);
            this.ComBox_Y.SelectedIndexChanged += new System.EventHandler(this.ComBox_Y_SelectedIndexChanged);
            // 
            // BtnReset
            // 
            this.BtnReset.Image = ((System.Drawing.Image)(resources.GetObject("BtnReset.Image")));
            this.BtnReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(60, 37);
            this.BtnReset.Text = "全部还原";
            this.BtnReset.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // BtnOpenFile
            // 
            this.BtnOpenFile.Image = ((System.Drawing.Image)(resources.GetObject("BtnOpenFile.Image")));
            this.BtnOpenFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnOpenFile.Name = "BtnOpenFile";
            this.BtnOpenFile.Size = new System.Drawing.Size(60, 37);
            this.BtnOpenFile.Text = "打开文件";
            this.BtnOpenFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.BtnOpenFile.Click += new System.EventHandler(this.BtnOpenFile_Click);
            // 
            // StartTime
            // 
            this.StartTime.Name = "StartTime";
            this.StartTime.Size = new System.Drawing.Size(56, 37);
            this.StartTime.Text = "开始时间";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // EndTime
            // 
            this.EndTime.Name = "EndTime";
            this.EndTime.Size = new System.Drawing.Size(56, 37);
            this.EndTime.Text = "结束时间";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
            // 
            // LeftTime
            // 
            this.LeftTime.Name = "LeftTime";
            this.LeftTime.Size = new System.Drawing.Size(44, 37);
            this.LeftTime.Text = "左时间";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 40);
            // 
            // RightTime
            // 
            this.RightTime.Name = "RightTime";
            this.RightTime.Size = new System.Drawing.Size(44, 37);
            this.RightTime.Text = "右时间";
            // 
            // PicMainPanel
            // 
            this.PicMainPanel.AutoScroll = true;
            this.PicMainPanel.BackColor = System.Drawing.Color.CornflowerBlue;
            this.PicMainPanel.Controls.Add(this.CurrentPicPanel);
            this.PicMainPanel.Controls.Add(this.SoeNamePanel);
            this.PicMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PicMainPanel.Location = new System.Drawing.Point(0, 40);
            this.PicMainPanel.Name = "PicMainPanel";
            this.PicMainPanel.Size = new System.Drawing.Size(885, 323);
            this.PicMainPanel.TabIndex = 2;
            // 
            // CurrentPicPanel
            // 
            this.CurrentPicPanel.Controls.Add(this.CurrentPicBox);
            this.CurrentPicPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CurrentPicPanel.Location = new System.Drawing.Point(151, 0);
            this.CurrentPicPanel.Name = "CurrentPicPanel";
            this.CurrentPicPanel.Size = new System.Drawing.Size(734, 323);
            this.CurrentPicPanel.TabIndex = 1;
            // 
            // CurrentPicBox
            // 
            this.CurrentPicBox.BackColor = System.Drawing.Color.Black;
            this.CurrentPicBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CurrentPicBox.Location = new System.Drawing.Point(0, 0);
            this.CurrentPicBox.Name = "CurrentPicBox";
            this.CurrentPicBox.Size = new System.Drawing.Size(734, 323);
            this.CurrentPicBox.TabIndex = 0;
            this.CurrentPicBox.TabStop = false;
            this.CurrentPicBox.Paint += new System.Windows.Forms.PaintEventHandler(this.CurrentPicBox_Paint);
            this.CurrentPicBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CurrentPicBox_MouseDown);
            this.CurrentPicBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CurrentPicBox_MouseMove);
            this.CurrentPicBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CurrentPicBox_MouseUp);
            // 
            // SoeNamePanel
            // 
            this.SoeNamePanel.BackColor = System.Drawing.Color.LightSteelBlue;
            this.SoeNamePanel.ContextMenuStrip = this.contextMenuStrip1;
            this.SoeNamePanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.SoeNamePanel.Location = new System.Drawing.Point(0, 0);
            this.SoeNamePanel.Name = "SoeNamePanel";
            this.SoeNamePanel.Size = new System.Drawing.Size(151, 323);
            this.SoeNamePanel.TabIndex = 0;
            // 
            // FreshTimer
            // 
            this.FreshTimer.Interval = 50;
            this.FreshTimer.Tick += new System.EventHandler(this.FreshTimer_Tick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ShowMsg,
            this.HideMsg});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 70);
            // 
            // ShowMsg
            // 
            this.ShowMsg.Name = "ShowMsg";
            this.ShowMsg.Size = new System.Drawing.Size(152, 22);
            this.ShowMsg.Text = "显示数值";
            this.ShowMsg.Click += new System.EventHandler(this.ShowMsg_Click);
            // 
            // HideMsg
            // 
            this.HideMsg.Name = "HideMsg";
            this.HideMsg.Size = new System.Drawing.Size(152, 22);
            this.HideMsg.Text = "隐藏数值";
            this.HideMsg.Click += new System.EventHandler(this.HideMsg_Click);
            // 
            // CSoePicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(885, 363);
            this.Controls.Add(this.PicMainPanel);
            this.Controls.Add(this.toolStrip1);
            this.Name = "CSoePicForm";
            this.Text = "录波图形分析";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CSoePicForm_Load);
            this.Resize += new System.EventHandler(this.CSoePicForm_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.PicMainPanel.ResumeLayout(false);
            this.CurrentPicPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CurrentPicBox)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel PicMainPanel;
        private System.Windows.Forms.Panel SoeNamePanel;
        private System.Windows.Forms.Panel CurrentPicPanel;
        private System.Windows.Forms.PictureBox CurrentPicBox;
        private System.Windows.Forms.ToolStripComboBox ComBox_X;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox LineSel;
        private System.Windows.Forms.ToolStripComboBox ComBox_Y;
        private System.Windows.Forms.ToolStripButton BtnReset;
        private System.Windows.Forms.Timer FreshTimer;
        private System.Windows.Forms.ToolStripLabel YHeightSet;
        private System.Windows.Forms.ToolStripButton BtnOpenFile;
        private System.Windows.Forms.ToolStripLabel StartTime;
        private System.Windows.Forms.ToolStripLabel EndTime;
        private System.Windows.Forms.ToolStripLabel LeftTime;
        private System.Windows.Forms.ToolStripLabel RightTime;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ShowMsg;
        private System.Windows.Forms.ToolStripMenuItem HideMsg;
       
    }
}