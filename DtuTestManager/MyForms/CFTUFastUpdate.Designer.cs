namespace DtuTestManager
{
    partial class CFTUFastUpdate
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
            this.FpbValue = new System.Windows.Forms.Label();
            this.FilePath = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.FileDownMsg = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnStartDown = new System.Windows.Forms.Button();
            this.BtnEndDown = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // FpbValue
            // 
            this.FpbValue.AutoSize = true;
            this.FpbValue.Location = new System.Drawing.Point(628, 156);
            this.FpbValue.Name = "FpbValue";
            this.FpbValue.Size = new System.Drawing.Size(41, 12);
            this.FpbValue.TabIndex = 0;
            this.FpbValue.Text = "进度0%";
            // 
            // FilePath
            // 
            this.FilePath.Location = new System.Drawing.Point(135, 68);
            this.FilePath.Name = "FilePath";
            this.FilePath.Size = new System.Drawing.Size(466, 21);
            this.FilePath.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(607, 66);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "浏览...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FileDownMsg
            // 
            this.FileDownMsg.Location = new System.Drawing.Point(135, 151);
            this.FileDownMsg.Name = "FileDownMsg";
            this.FileDownMsg.Size = new System.Drawing.Size(466, 23);
            this.FileDownMsg.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "升级文件：";
            // 
            // BtnStartDown
            // 
            this.BtnStartDown.Enabled = false;
            this.BtnStartDown.Location = new System.Drawing.Point(701, 66);
            this.BtnStartDown.Name = "BtnStartDown";
            this.BtnStartDown.Size = new System.Drawing.Size(75, 23);
            this.BtnStartDown.TabIndex = 2;
            this.BtnStartDown.Text = "开始升级";
            this.BtnStartDown.UseVisualStyleBackColor = true;
            this.BtnStartDown.Click += new System.EventHandler(this.BtnStartDown_Click);
            // 
            // BtnEndDown
            // 
            this.BtnEndDown.Enabled = false;
            this.BtnEndDown.Location = new System.Drawing.Point(701, 151);
            this.BtnEndDown.Name = "BtnEndDown";
            this.BtnEndDown.Size = new System.Drawing.Size(75, 23);
            this.BtnEndDown.TabIndex = 2;
            this.BtnEndDown.Text = "停止升级";
            this.BtnEndDown.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(369, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 12);
            this.label3.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // CFTUFastUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(839, 226);
            this.Controls.Add(this.FileDownMsg);
            this.Controls.Add(this.BtnEndDown);
            this.Controls.Add(this.BtnStartDown);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.FilePath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.FpbValue);
            this.Name = "CFTUFastUpdate";
            this.Text = "CFTUFastUpdate";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CFTUFastUpdate_FormClosed);
            this.Load += new System.EventHandler(this.CFTUFastUpdate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label FpbValue;
        private System.Windows.Forms.TextBox FilePath;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ProgressBar FileDownMsg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BtnStartDown;
        private System.Windows.Forms.Button BtnEndDown;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer1;
    }
}