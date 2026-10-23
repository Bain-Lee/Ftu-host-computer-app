namespace DtuTestManager
{
    partial class CDownFileForm
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
            this.BtnStartUpdate = new System.Windows.Forms.Button();
            this.DownResult = new System.Windows.Forms.Label();
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
            this.Label1.Text = "选择文件：";
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
            this.FileDownMsg.Location = new System.Drawing.Point(93, 63);
            this.FileDownMsg.Name = "FileDownMsg";
            this.FileDownMsg.Size = new System.Drawing.Size(612, 23);
            this.FileDownMsg.TabIndex = 3;
            // 
            // BtnStartDown
            // 
            this.BtnStartDown.BackColor = System.Drawing.Color.Azure;
            this.BtnStartDown.Enabled = false;
            this.BtnStartDown.Location = new System.Drawing.Point(5, 63);
            this.BtnStartDown.Name = "BtnStartDown";
            this.BtnStartDown.Size = new System.Drawing.Size(82, 23);
            this.BtnStartDown.TabIndex = 2;
            this.BtnStartDown.Text = "开始传输";
            this.BtnStartDown.UseVisualStyleBackColor = false;
            this.BtnStartDown.Click += new System.EventHandler(this.BtnStartDown_Click);
            // 
            // FpbValue
            // 
            this.FpbValue.AutoSize = true;
            this.FpbValue.Location = new System.Drawing.Point(723, 67);
            this.FpbValue.Name = "FpbValue";
            this.FpbValue.Size = new System.Drawing.Size(41, 12);
            this.FpbValue.TabIndex = 4;
            this.FpbValue.Text = "进度0%";
            // 
            // DownFileTime
            // 
            this.DownFileTime.Interval = 20;
            this.DownFileTime.Tick += new System.EventHandler(this.DownFileTime_Tick);
            // 
            // BtnStartUpdate
            // 
            this.BtnStartUpdate.BackColor = System.Drawing.Color.Azure;
            this.BtnStartUpdate.Location = new System.Drawing.Point(348, 118);
            this.BtnStartUpdate.Name = "BtnStartUpdate";
            this.BtnStartUpdate.Size = new System.Drawing.Size(82, 23);
            this.BtnStartUpdate.TabIndex = 2;
            this.BtnStartUpdate.Text = "启动升级";
            this.BtnStartUpdate.UseVisualStyleBackColor = false;
            this.BtnStartUpdate.Click += new System.EventHandler(this.BtnStartUpdate_Click);
            // 
            // DownResult
            // 
            this.DownResult.AutoSize = true;
            this.DownResult.Location = new System.Drawing.Point(93, 118);
            this.DownResult.Name = "DownResult";
            this.DownResult.Size = new System.Drawing.Size(0, 12);
            this.DownResult.TabIndex = 5;
            // 
            // CDownFileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(843, 181);
            this.Controls.Add(this.DownResult);
            this.Controls.Add(this.FpbValue);
            this.Controls.Add(this.FileDownMsg);
            this.Controls.Add(this.BtnStartUpdate);
            this.Controls.Add(this.BtnStartDown);
            this.Controls.Add(this.BtnOpenFile);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.FilePath);
            this.Name = "CDownFileForm";
            this.Text = "文件传输";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CDownFileForm_FormClosed);
            this.Load += new System.EventHandler(this.CDownFileForm_Load);
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
        private System.Windows.Forms.Button BtnStartUpdate;
        private System.Windows.Forms.Label DownResult;
    }
}