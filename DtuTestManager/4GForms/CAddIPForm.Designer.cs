namespace DtuTestManager
{
    partial class CAddIPForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_addIP = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.combox_NetSel = new System.Windows.Forms.ComboBox();
            this.ipBox = new DtuTestManager.IpBox();
            this.MaskBox = new DtuTestManager.IpBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(73, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP地址:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(73, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "子网掩码:";
            // 
            // btn_addIP
            // 
            this.btn_addIP.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_addIP.Location = new System.Drawing.Point(85, 149);
            this.btn_addIP.Name = "btn_addIP";
            this.btn_addIP.Size = new System.Drawing.Size(75, 23);
            this.btn_addIP.TabIndex = 19;
            this.btn_addIP.Text = "确定";
            this.btn_addIP.UseVisualStyleBackColor = true;
            this.btn_addIP.Click += new System.EventHandler(this.btn_addIP_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(278, 149);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 20;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(73, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 21;
            this.label6.Text = "网口:";
            // 
            // combox_NetSel
            // 
            this.combox_NetSel.FormattingEnabled = true;
            this.combox_NetSel.Location = new System.Drawing.Point(151, 11);
            this.combox_NetSel.Name = "combox_NetSel";
            this.combox_NetSel.Size = new System.Drawing.Size(81, 20);
            this.combox_NetSel.TabIndex = 22;
            // 
            // ipBox
            // 
            this.ipBox.Location = new System.Drawing.Point(151, 55);
            this.ipBox.Name = "ipBox";
            this.ipBox.Size = new System.Drawing.Size(136, 20);
            this.ipBox.TabIndex = 23;
            // 
            // MaskBox
            // 
            this.MaskBox.Location = new System.Drawing.Point(151, 100);
            this.MaskBox.Name = "MaskBox";
            this.MaskBox.Size = new System.Drawing.Size(136, 20);
            this.MaskBox.TabIndex = 24;
            // 
            // CAddIPForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.ClientSize = new System.Drawing.Size(476, 203);
            this.Controls.Add(this.MaskBox);
            this.Controls.Add(this.ipBox);
            this.Controls.Add(this.combox_NetSel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_addIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "CAddIPForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加IP地址";
            this.Load += new System.EventHandler(this.CAddIPForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_addIP;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox combox_NetSel;
        private IpBox ipBox;
        private IpBox MaskBox;
    }
}