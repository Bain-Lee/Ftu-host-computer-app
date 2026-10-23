namespace DtuTestManager
{
    partial class CDataCheckForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CDataCheckForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.Btn_Check = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Btn_DC_Check = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnCallPhase = new System.Windows.Forms.ToolStripButton();
            this.CtrlPanel = new System.Windows.Forms.Panel();
            this.SelectKillAllCmd = new System.Windows.Forms.Button();
            this.BtnSelectAllCmd = new System.Windows.Forms.Button();
            this.Btn_DownRatioCmd = new System.Windows.Forms.Button();
            this.Btn_CallRationCmd = new System.Windows.Forms.Button();
            this.BtnCallPhaseCmd = new System.Windows.Forms.Button();
            this.BtnCheckCmd = new System.Windows.Forms.Button();
            this.com_Angle = new System.Windows.Forms.ComboBox();
            this.com_Current = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.com_LineNo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.com_Vol = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DataPanel = new System.Windows.Forms.Panel();
            this.LV_Phase = new System.Windows.Forms.ListView();
            this.LV_DC_Check = new DtuTestManager.MyListView(this.components);
            this.LV_AC_Check = new DtuTestManager.MyListView(this.components);
            this.Freshtimer = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            this.CtrlPanel.SuspendLayout();
            this.DataPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Azure;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Btn_Check,
            this.toolStripSeparator1,
            this.Btn_DC_Check,
            this.toolStripSeparator3,
            this.BtnCallPhase});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1018, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // Btn_Check
            // 
            this.Btn_Check.Image = ((System.Drawing.Image)(resources.GetObject("Btn_Check.Image")));
            this.Btn_Check.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_Check.Name = "Btn_Check";
            this.Btn_Check.Size = new System.Drawing.Size(88, 22);
            this.Btn_Check.Text = "交流校验值";
            this.Btn_Check.Click += new System.EventHandler(this.Btn_Check_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // Btn_DC_Check
            // 
            this.Btn_DC_Check.Image = ((System.Drawing.Image)(resources.GetObject("Btn_DC_Check.Image")));
            this.Btn_DC_Check.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Btn_DC_Check.Name = "Btn_DC_Check";
            this.Btn_DC_Check.Size = new System.Drawing.Size(88, 22);
            this.Btn_DC_Check.Text = "直流校验值";
            this.Btn_DC_Check.Click += new System.EventHandler(this.Btn_DC_Check_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // BtnCallPhase
            // 
            this.BtnCallPhase.Image = ((System.Drawing.Image)(resources.GetObject("BtnCallPhase.Image")));
            this.BtnCallPhase.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnCallPhase.Name = "BtnCallPhase";
            this.BtnCallPhase.Size = new System.Drawing.Size(76, 22);
            this.BtnCallPhase.Text = "绝对相角";
            this.BtnCallPhase.Click += new System.EventHandler(this.BtnCallPhase_Click);
            // 
            // CtrlPanel
            // 
            this.CtrlPanel.Controls.Add(this.SelectKillAllCmd);
            this.CtrlPanel.Controls.Add(this.BtnSelectAllCmd);
            this.CtrlPanel.Controls.Add(this.Btn_DownRatioCmd);
            this.CtrlPanel.Controls.Add(this.Btn_CallRationCmd);
            this.CtrlPanel.Controls.Add(this.BtnCallPhaseCmd);
            this.CtrlPanel.Controls.Add(this.BtnCheckCmd);
            this.CtrlPanel.Controls.Add(this.com_Angle);
            this.CtrlPanel.Controls.Add(this.com_Current);
            this.CtrlPanel.Controls.Add(this.label3);
            this.CtrlPanel.Controls.Add(this.label2);
            this.CtrlPanel.Controls.Add(this.com_LineNo);
            this.CtrlPanel.Controls.Add(this.label4);
            this.CtrlPanel.Controls.Add(this.com_Vol);
            this.CtrlPanel.Controls.Add(this.label1);
            this.CtrlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.CtrlPanel.Location = new System.Drawing.Point(0, 25);
            this.CtrlPanel.Name = "CtrlPanel";
            this.CtrlPanel.Size = new System.Drawing.Size(1018, 107);
            this.CtrlPanel.TabIndex = 1;
            // 
            // SelectKillAllCmd
            // 
            this.SelectKillAllCmd.Location = new System.Drawing.Point(500, 71);
            this.SelectKillAllCmd.Name = "SelectKillAllCmd";
            this.SelectKillAllCmd.Size = new System.Drawing.Size(75, 23);
            this.SelectKillAllCmd.TabIndex = 7;
            this.SelectKillAllCmd.Text = "取消选择";
            this.SelectKillAllCmd.UseVisualStyleBackColor = true;
            this.SelectKillAllCmd.Click += new System.EventHandler(this.SelectKillAllCmd_Click);
            // 
            // BtnSelectAllCmd
            // 
            this.BtnSelectAllCmd.Location = new System.Drawing.Point(409, 71);
            this.BtnSelectAllCmd.Name = "BtnSelectAllCmd";
            this.BtnSelectAllCmd.Size = new System.Drawing.Size(75, 23);
            this.BtnSelectAllCmd.TabIndex = 6;
            this.BtnSelectAllCmd.Text = "全部选择";
            this.BtnSelectAllCmd.UseVisualStyleBackColor = true;
            this.BtnSelectAllCmd.Click += new System.EventHandler(this.BtnSelectAllCmd_Click);
            // 
            // Btn_DownRatioCmd
            // 
            this.Btn_DownRatioCmd.Location = new System.Drawing.Point(73, 80);
            this.Btn_DownRatioCmd.Name = "Btn_DownRatioCmd";
            this.Btn_DownRatioCmd.Size = new System.Drawing.Size(75, 23);
            this.Btn_DownRatioCmd.TabIndex = 5;
            this.Btn_DownRatioCmd.Text = "下装系数";
            this.Btn_DownRatioCmd.UseVisualStyleBackColor = true;
            this.Btn_DownRatioCmd.Click += new System.EventHandler(this.Btn_DownRatioCmd_Click);
            // 
            // Btn_CallRationCmd
            // 
            this.Btn_CallRationCmd.Location = new System.Drawing.Point(73, 51);
            this.Btn_CallRationCmd.Name = "Btn_CallRationCmd";
            this.Btn_CallRationCmd.Size = new System.Drawing.Size(75, 23);
            this.Btn_CallRationCmd.TabIndex = 4;
            this.Btn_CallRationCmd.Text = "召唤系数";
            this.Btn_CallRationCmd.UseVisualStyleBackColor = true;
            this.Btn_CallRationCmd.Click += new System.EventHandler(this.Btn_CallRationCmd_Click);
            // 
            // BtnCallPhaseCmd
            // 
            this.BtnCallPhaseCmd.Location = new System.Drawing.Point(230, 71);
            this.BtnCallPhaseCmd.Name = "BtnCallPhaseCmd";
            this.BtnCallPhaseCmd.Size = new System.Drawing.Size(115, 23);
            this.BtnCallPhaseCmd.TabIndex = 3;
            this.BtnCallPhaseCmd.Text = "召唤绝对相角";
            this.BtnCallPhaseCmd.UseVisualStyleBackColor = true;
            this.BtnCallPhaseCmd.Click += new System.EventHandler(this.BtnCallPhaseCmd_Click);
            // 
            // BtnCheckCmd
            // 
            this.BtnCheckCmd.Location = new System.Drawing.Point(12, 51);
            this.BtnCheckCmd.Name = "BtnCheckCmd";
            this.BtnCheckCmd.Size = new System.Drawing.Size(55, 53);
            this.BtnCheckCmd.TabIndex = 2;
            this.BtnCheckCmd.Text = "校验";
            this.BtnCheckCmd.UseVisualStyleBackColor = true;
            this.BtnCheckCmd.Click += new System.EventHandler(this.BtnCheckCmd_Click);
            // 
            // com_Angle
            // 
            this.com_Angle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_Angle.FormattingEnabled = true;
            this.com_Angle.Location = new System.Drawing.Point(633, 15);
            this.com_Angle.Name = "com_Angle";
            this.com_Angle.Size = new System.Drawing.Size(78, 20);
            this.com_Angle.TabIndex = 1;
            this.com_Angle.SelectedIndexChanged += new System.EventHandler(this.com_Angle_SelectedIndexChanged);
            // 
            // com_Current
            // 
            this.com_Current.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_Current.FormattingEnabled = true;
            this.com_Current.Location = new System.Drawing.Point(456, 14);
            this.com_Current.Name = "com_Current";
            this.com_Current.Size = new System.Drawing.Size(78, 20);
            this.com_Current.TabIndex = 1;
            this.com_Current.SelectedIndexChanged += new System.EventHandler(this.com_Current_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(551, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "角度基准值：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(373, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "电流基准值：";
            // 
            // com_LineNo
            // 
            this.com_LineNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_LineNo.FormattingEnabled = true;
            this.com_LineNo.Location = new System.Drawing.Point(97, 12);
            this.com_LineNo.Name = "com_LineNo";
            this.com_LineNo.Size = new System.Drawing.Size(93, 20);
            this.com_LineNo.TabIndex = 1;
            this.com_LineNo.SelectedIndexChanged += new System.EventHandler(this.com_LineNo_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "线路选择：";
            // 
            // com_Vol
            // 
            this.com_Vol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.com_Vol.FormattingEnabled = true;
            this.com_Vol.Location = new System.Drawing.Point(279, 14);
            this.com_Vol.Name = "com_Vol";
            this.com_Vol.Size = new System.Drawing.Size(78, 20);
            this.com_Vol.TabIndex = 1;
            this.com_Vol.SelectedIndexChanged += new System.EventHandler(this.com_Vol_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(202, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "电压基准值：";
            // 
            // DataPanel
            // 
            this.DataPanel.Controls.Add(this.LV_Phase);
            this.DataPanel.Controls.Add(this.LV_DC_Check);
            this.DataPanel.Controls.Add(this.LV_AC_Check);
            this.DataPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataPanel.Location = new System.Drawing.Point(0, 132);
            this.DataPanel.Name = "DataPanel";
            this.DataPanel.Size = new System.Drawing.Size(1018, 412);
            this.DataPanel.TabIndex = 2;
            // 
            // LV_Phase
            // 
            this.LV_Phase.Location = new System.Drawing.Point(524, 24);
            this.LV_Phase.Name = "LV_Phase";
            this.LV_Phase.Size = new System.Drawing.Size(121, 97);
            this.LV_Phase.TabIndex = 1;
            this.LV_Phase.UseCompatibleStateImageBehavior = false;
            this.LV_Phase.Visible = false;
            // 
            // LV_DC_Check
            // 
            this.LV_DC_Check.FullRowSelect = true;
            this.LV_DC_Check.GridLines = true;
            this.LV_DC_Check.Location = new System.Drawing.Point(123, 180);
            this.LV_DC_Check.Name = "LV_DC_Check";
            this.LV_DC_Check.OwnerDraw = true;
            this.LV_DC_Check.ProgressColumnIndex = -1;
            this.LV_DC_Check.Size = new System.Drawing.Size(432, 76);
            this.LV_DC_Check.TabIndex = 0;
            this.LV_DC_Check.UseCompatibleStateImageBehavior = false;
            this.LV_DC_Check.View = System.Windows.Forms.View.Details;
            this.LV_DC_Check.Visible = false;
            // 
            // LV_AC_Check
            // 
            this.LV_AC_Check.FullRowSelect = true;
            this.LV_AC_Check.GridLines = true;
            this.LV_AC_Check.Location = new System.Drawing.Point(28, 45);
            this.LV_AC_Check.Name = "LV_AC_Check";
            this.LV_AC_Check.OwnerDraw = true;
            this.LV_AC_Check.ProgressColumnIndex = -1;
            this.LV_AC_Check.Size = new System.Drawing.Size(432, 76);
            this.LV_AC_Check.TabIndex = 0;
            this.LV_AC_Check.UseCompatibleStateImageBehavior = false;
            this.LV_AC_Check.View = System.Windows.Forms.View.Details;
            // 
            // Freshtimer
            // 
            this.Freshtimer.Interval = 500;
            this.Freshtimer.Tick += new System.EventHandler(this.Freshtimer_Tick);
            // 
            // CDataCheckForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(1018, 544);
            this.Controls.Add(this.DataPanel);
            this.Controls.Add(this.CtrlPanel);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CDataCheckForm";
            this.Text = "数据校验";
            this.Load += new System.EventHandler(this.CDataCheckForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.CtrlPanel.ResumeLayout(false);
            this.CtrlPanel.PerformLayout();
            this.DataPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel CtrlPanel;
        private System.Windows.Forms.Panel DataPanel;
        private System.Windows.Forms.ToolStripButton Btn_Check;
        private System.Windows.Forms.ComboBox com_Angle;
        private System.Windows.Forms.ComboBox com_Current;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox com_LineNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox com_Vol;
        private System.Windows.Forms.Label label1;
        private MyListView LV_AC_Check;
        private System.Windows.Forms.Timer Freshtimer;
        private System.Windows.Forms.ListView LV_Phase;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton BtnCallPhase;
        private System.Windows.Forms.ToolStripButton Btn_DC_Check;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private MyListView LV_DC_Check;
        private System.Windows.Forms.Button BtnCheckCmd;
        private System.Windows.Forms.Button BtnCallPhaseCmd;
        private System.Windows.Forms.Button Btn_DownRatioCmd;
        private System.Windows.Forms.Button Btn_CallRationCmd;
        private System.Windows.Forms.Button SelectKillAllCmd;
        private System.Windows.Forms.Button BtnSelectAllCmd;
    }
}