namespace DtuTestManager
{
    partial class CAddCom
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
            this.combox_ComIndexSel = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_addIP = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.combox_ComBaud = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // combox_ComIndexSel
            // 
            this.combox_ComIndexSel.FormattingEnabled = true;
            this.combox_ComIndexSel.Location = new System.Drawing.Point(205, 34);
            this.combox_ComIndexSel.Name = "combox_ComIndexSel";
            this.combox_ComIndexSel.Size = new System.Drawing.Size(81, 20);
            this.combox_ComIndexSel.TabIndex = 42;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(127, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 41;
            this.label6.Text = "串口:";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(278, 135);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 40;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // btn_addIP
            // 
            this.btn_addIP.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_addIP.Location = new System.Drawing.Point(87, 135);
            this.btn_addIP.Name = "btn_addIP";
            this.btn_addIP.Size = new System.Drawing.Size(75, 23);
            this.btn_addIP.TabIndex = 39;
            this.btn_addIP.Text = "确定";
            this.btn_addIP.UseVisualStyleBackColor = true;
            this.btn_addIP.Click += new System.EventHandler(this.btn_addIP_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(127, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "波特率:";
            // 
            // combox_ComBaud
            // 
            this.combox_ComBaud.FormattingEnabled = true;
            this.combox_ComBaud.Location = new System.Drawing.Point(205, 80);
            this.combox_ComBaud.Name = "combox_ComBaud";
            this.combox_ComBaud.Size = new System.Drawing.Size(81, 20);
            this.combox_ComBaud.TabIndex = 43;
            // 
            // CAddCom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.ClientSize = new System.Drawing.Size(446, 193);
            this.Controls.Add(this.combox_ComBaud);
            this.Controls.Add(this.combox_ComIndexSel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_addIP);
            this.Controls.Add(this.label1);
            this.Name = "CAddCom";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CAddCom";
            this.Load += new System.EventHandler(this.CAddCom_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox combox_ComIndexSel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_addIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox combox_ComBaud;
    }
}