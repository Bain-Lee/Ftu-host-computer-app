namespace DtuTestManager
{
    partial class CWaitForm
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
            this.WaitMsg = new System.Windows.Forms.Label();
            this.WaitTime = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // WaitMsg
            // 
            this.WaitMsg.AutoSize = true;
            this.WaitMsg.Font = new System.Drawing.Font("宋体", 14F);
            this.WaitMsg.ForeColor = System.Drawing.Color.DarkGreen;
            this.WaitMsg.Location = new System.Drawing.Point(86, 15);
            this.WaitMsg.Name = "WaitMsg";
            this.WaitMsg.Size = new System.Drawing.Size(221, 19);
            this.WaitMsg.TabIndex = 0;
            this.WaitMsg.Text = "正在读取，请稍等......";
            // 
            // WaitTime
            // 
            this.WaitTime.Interval = 1000;
            this.WaitTime.Tick += new System.EventHandler(this.WaitTime_Tick);
            // 
            // CWaitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(423, 51);
            this.ControlBox = false;
            this.Controls.Add(this.WaitMsg);
            this.Name = "CWaitForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.CWaitForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label WaitMsg;
        private System.Windows.Forms.Timer WaitTime;
    }
}