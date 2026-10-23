namespace DtuTestManager
{
    partial class CUpdataForm
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
            this.FilePath = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.BtnOpenFile = new System.Windows.Forms.Button();
            this.FileDownMsg = new System.Windows.Forms.ProgressBar();
            this.BtnStartDown = new System.Windows.Forms.Button();
            this.FpbValue = new System.Windows.Forms.Label();
            this.DownFileTime = new System.Windows.Forms.Timer(this.components);
            this.BtnEndDown = new System.Windows.Forms.Button();
            this.DelayTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.UpResult = new System.Windows.Forms.Label();
            this.Btn_SendVerfy = new System.Windows.Forms.Button();
            this.BtnSendEnd = new System.Windows.Forms.Button();
            this.BtnUpID = new System.Windows.Forms.Button();
            this.myPanel1 = new DtuTestManager.MyPanel();
            this.Use_Normal = new System.Windows.Forms.RadioButton();
            this.UseErrRand = new System.Windows.Forms.RadioButton();
            this.UseMingWen = new System.Windows.Forms.RadioButton();
            this.UseNoSign = new System.Windows.Forms.RadioButton();
            this.UseErrTime = new System.Windows.Forms.RadioButton();
            this.UseErrSign = new System.Windows.Forms.RadioButton();
            this.myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // FilePath
            // 
            this.FilePath.Location = new System.Drawing.Point(93, 13);
            this.FilePath.Name = "FilePath";
            this.FilePath.Size = new System.Drawing.Size(612, 21);
            this.FilePath.TabIndex = 0;
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(13, 13);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(65, 12);
            this.Label1.TabIndex = 1;
            this.Label1.Text = "升级文件：";
            // 
            // BtnOpenFile
            // 
            this.BtnOpenFile.BackColor = System.Drawing.Color.Azure;
            this.BtnOpenFile.Location = new System.Drawing.Point(723, 11);
            this.BtnOpenFile.Name = "BtnOpenFile";
            this.BtnOpenFile.Size = new System.Drawing.Size(82, 23);
            this.BtnOpenFile.TabIndex = 2;
            this.BtnOpenFile.Text = "浏览...";
            this.BtnOpenFile.UseVisualStyleBackColor = false;
            this.BtnOpenFile.Click += new System.EventHandler(this.BtnOpenFile_Click);
            // 
            // FileDownMsg
            // 
            this.FileDownMsg.Location = new System.Drawing.Point(164, 65);
            this.FileDownMsg.Name = "FileDownMsg";
            this.FileDownMsg.Size = new System.Drawing.Size(589, 23);
            this.FileDownMsg.TabIndex = 3;
            // 
            // BtnStartDown
            // 
            this.BtnStartDown.BackColor = System.Drawing.Color.Azure;
            this.BtnStartDown.Enabled = false;
            this.BtnStartDown.Location = new System.Drawing.Point(12, 143);
            this.BtnStartDown.Name = "BtnStartDown";
            this.BtnStartDown.Size = new System.Drawing.Size(103, 23);
            this.BtnStartDown.TabIndex = 2;
            this.BtnStartDown.Text = "开始升级";
            this.BtnStartDown.UseVisualStyleBackColor = false;
            this.BtnStartDown.Click += new System.EventHandler(this.BtnStartDown_Click);
            // 
            // FpbValue
            // 
            this.FpbValue.AutoSize = true;
            this.FpbValue.Location = new System.Drawing.Point(771, 69);
            this.FpbValue.Name = "FpbValue";
            this.FpbValue.Size = new System.Drawing.Size(41, 12);
            this.FpbValue.TabIndex = 4;
            this.FpbValue.Text = "进度0%";
            // 
            // DownFileTime
            // 
            this.DownFileTime.Tick += new System.EventHandler(this.DownFileTime_Tick);
            // 
            // BtnEndDown
            // 
            this.BtnEndDown.BackColor = System.Drawing.Color.Azure;
            this.BtnEndDown.Enabled = false;
            this.BtnEndDown.Location = new System.Drawing.Point(12, 242);
            this.BtnEndDown.Name = "BtnEndDown";
            this.BtnEndDown.Size = new System.Drawing.Size(103, 23);
            this.BtnEndDown.TabIndex = 2;
            this.BtnEndDown.Text = "终止升级";
            this.BtnEndDown.UseVisualStyleBackColor = false;
            this.BtnEndDown.Click += new System.EventHandler(this.BtnEndDown_Click);
            // 
            // DelayTime
            // 
            this.DelayTime.Location = new System.Drawing.Point(456, 109);
            this.DelayTime.Name = "DelayTime";
            this.DelayTime.Size = new System.Drawing.Size(100, 21);
            this.DelayTime.TabIndex = 6;
            this.DelayTime.Text = "500";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(301, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(149, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "报文传输时间间隔(毫秒)：";
            // 
            // UpResult
            // 
            this.UpResult.AutoSize = true;
            this.UpResult.Location = new System.Drawing.Point(415, 164);
            this.UpResult.Name = "UpResult";
            this.UpResult.Size = new System.Drawing.Size(0, 12);
            this.UpResult.TabIndex = 8;
            // 
            // Btn_SendVerfy
            // 
            this.Btn_SendVerfy.BackColor = System.Drawing.Color.Azure;
            this.Btn_SendVerfy.Enabled = false;
            this.Btn_SendVerfy.Location = new System.Drawing.Point(12, 176);
            this.Btn_SendVerfy.Name = "Btn_SendVerfy";
            this.Btn_SendVerfy.Size = new System.Drawing.Size(103, 23);
            this.Btn_SendVerfy.TabIndex = 2;
            this.Btn_SendVerfy.Text = "发送升级验证包";
            this.Btn_SendVerfy.UseVisualStyleBackColor = false;
            this.Btn_SendVerfy.Click += new System.EventHandler(this.Btn_SendVerfy_Click);
            // 
            // BtnSendEnd
            // 
            this.BtnSendEnd.BackColor = System.Drawing.Color.Azure;
            this.BtnSendEnd.Enabled = false;
            this.BtnSendEnd.Location = new System.Drawing.Point(12, 209);
            this.BtnSendEnd.Name = "BtnSendEnd";
            this.BtnSendEnd.Size = new System.Drawing.Size(103, 23);
            this.BtnSendEnd.TabIndex = 2;
            this.BtnSendEnd.Text = "发送升级结束";
            this.BtnSendEnd.UseVisualStyleBackColor = false;
            this.BtnSendEnd.Click += new System.EventHandler(this.BtnSendEnd_Click);
            // 
            // BtnUpID
            // 
            this.BtnUpID.BackColor = System.Drawing.Color.Azure;
            this.BtnUpID.Enabled = false;
            this.BtnUpID.Location = new System.Drawing.Point(12, 103);
            this.BtnUpID.Name = "BtnUpID";
            this.BtnUpID.Size = new System.Drawing.Size(103, 23);
            this.BtnUpID.TabIndex = 2;
            this.BtnUpID.Text = "升级身份认证";
            this.BtnUpID.UseVisualStyleBackColor = false;
            this.BtnUpID.Click += new System.EventHandler(this.BtnUpID_Click);
            // 
            // myPanel1
            // 
            this.myPanel1.Controls.Add(this.Use_Normal);
            this.myPanel1.Controls.Add(this.UseErrRand);
            this.myPanel1.Controls.Add(this.UseMingWen);
            this.myPanel1.Controls.Add(this.UseNoSign);
            this.myPanel1.Controls.Add(this.UseErrTime);
            this.myPanel1.Controls.Add(this.UseErrSign);
            this.myPanel1.Location = new System.Drawing.Point(134, 103);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Size = new System.Drawing.Size(110, 233);
            this.myPanel1.TabIndex = 9;
            // 
            // Use_Normal
            // 
            this.Use_Normal.AutoSize = true;
            this.Use_Normal.Location = new System.Drawing.Point(12, 24);
            this.Use_Normal.Name = "Use_Normal";
            this.Use_Normal.Size = new System.Drawing.Size(71, 16);
            this.Use_Normal.TabIndex = 18;
            this.Use_Normal.Text = "正常测试";
            this.Use_Normal.UseVisualStyleBackColor = true;
            this.Use_Normal.CheckedChanged += new System.EventHandler(this.Use_Normal_CheckedChanged);
            // 
            // UseErrRand
            // 
            this.UseErrRand.AutoCheck = false;
            this.UseErrRand.AutoSize = true;
            this.UseErrRand.Location = new System.Drawing.Point(12, 167);
            this.UseErrRand.Name = "UseErrRand";
            this.UseErrRand.Size = new System.Drawing.Size(95, 16);
            this.UseErrRand.TabIndex = 13;
            this.UseErrRand.TabStop = true;
            this.UseErrRand.Text = "错误的随机数";
            this.UseErrRand.UseVisualStyleBackColor = true;
            this.UseErrRand.CheckedChanged += new System.EventHandler(this.UseErrRand_CheckedChanged);
            this.UseErrRand.Click += new System.EventHandler(this.UseErrRand_Click);
            // 
            // UseMingWen
            // 
            this.UseMingWen.AutoCheck = false;
            this.UseMingWen.AutoSize = true;
            this.UseMingWen.Location = new System.Drawing.Point(12, 203);
            this.UseMingWen.Name = "UseMingWen";
            this.UseMingWen.Size = new System.Drawing.Size(71, 16);
            this.UseMingWen.TabIndex = 14;
            this.UseMingWen.TabStop = true;
            this.UseMingWen.Text = "下发明文";
            this.UseMingWen.UseVisualStyleBackColor = true;
            this.UseMingWen.CheckedChanged += new System.EventHandler(this.UseMingWen_CheckedChanged);
            this.UseMingWen.Click += new System.EventHandler(this.UseMingWen_Click);
            // 
            // UseNoSign
            // 
            this.UseNoSign.AutoCheck = false;
            this.UseNoSign.AutoSize = true;
            this.UseNoSign.Location = new System.Drawing.Point(12, 59);
            this.UseNoSign.Name = "UseNoSign";
            this.UseNoSign.Size = new System.Drawing.Size(71, 16);
            this.UseNoSign.TabIndex = 17;
            this.UseNoSign.TabStop = true;
            this.UseNoSign.Text = "不带签名";
            this.UseNoSign.UseVisualStyleBackColor = true;
            this.UseNoSign.CheckedChanged += new System.EventHandler(this.UseNoSign_CheckedChanged);
            this.UseNoSign.Click += new System.EventHandler(this.UseNoSign_Click);
            // 
            // UseErrTime
            // 
            this.UseErrTime.AutoCheck = false;
            this.UseErrTime.AutoSize = true;
            this.UseErrTime.Location = new System.Drawing.Point(12, 131);
            this.UseErrTime.Name = "UseErrTime";
            this.UseErrTime.Size = new System.Drawing.Size(83, 16);
            this.UseErrTime.TabIndex = 18;
            this.UseErrTime.TabStop = true;
            this.UseErrTime.Text = "错误的时间";
            this.UseErrTime.UseVisualStyleBackColor = true;
            this.UseErrTime.CheckedChanged += new System.EventHandler(this.UseErrTime_CheckedChanged);
            this.UseErrTime.Click += new System.EventHandler(this.UseErrTime_Click);
            // 
            // UseErrSign
            // 
            this.UseErrSign.AutoCheck = false;
            this.UseErrSign.AutoSize = true;
            this.UseErrSign.Location = new System.Drawing.Point(12, 95);
            this.UseErrSign.Name = "UseErrSign";
            this.UseErrSign.Size = new System.Drawing.Size(83, 16);
            this.UseErrSign.TabIndex = 19;
            this.UseErrSign.TabStop = true;
            this.UseErrSign.Text = "错误的签名";
            this.UseErrSign.UseVisualStyleBackColor = true;
            this.UseErrSign.CheckedChanged += new System.EventHandler(this.UseErrSign_CheckedChanged);
            this.UseErrSign.Click += new System.EventHandler(this.UseErrSign_Click);
            // 
            // CUpdataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(856, 348);
            this.Controls.Add(this.myPanel1);
            this.Controls.Add(this.UpResult);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DelayTime);
            this.Controls.Add(this.FpbValue);
            this.Controls.Add(this.FileDownMsg);
            this.Controls.Add(this.BtnEndDown);
            this.Controls.Add(this.BtnSendEnd);
            this.Controls.Add(this.Btn_SendVerfy);
            this.Controls.Add(this.BtnUpID);
            this.Controls.Add(this.BtnStartDown);
            this.Controls.Add(this.BtnOpenFile);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.FilePath);
            this.Name = "CUpdataForm";
            this.Text = "远程升级";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CUpdataForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CUpdataForm_FormClosed);
            this.Load += new System.EventHandler(this.CUpdataForm_Load);
            this.myPanel1.ResumeLayout(false);
            this.myPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FilePath;
        private System.Windows.Forms.Label Label1;
        private System.Windows.Forms.Button BtnOpenFile;
        private System.Windows.Forms.ProgressBar FileDownMsg;
        private System.Windows.Forms.Button BtnStartDown;
        private System.Windows.Forms.Label FpbValue;
        private System.Windows.Forms.Timer DownFileTime;
        private System.Windows.Forms.Button BtnEndDown;
        private System.Windows.Forms.TextBox DelayTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label UpResult;
        private System.Windows.Forms.Button Btn_SendVerfy;
        private System.Windows.Forms.Button BtnSendEnd;
        private System.Windows.Forms.Button BtnUpID;
        private MyPanel myPanel1;
        private System.Windows.Forms.RadioButton UseErrRand;
        private System.Windows.Forms.RadioButton UseMingWen;
        private System.Windows.Forms.RadioButton UseNoSign;
        private System.Windows.Forms.RadioButton UseErrTime;
        private System.Windows.Forms.RadioButton UseErrSign;
        private System.Windows.Forms.RadioButton Use_Normal;
    }
}