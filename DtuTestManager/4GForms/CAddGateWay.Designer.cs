namespace DtuTestManager
{
    partial class CAddGateWay
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
            this.combox_NetSel = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Enter = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.MaskBox = new DtuTestManager.IpBox();
            this.SegBox = new DtuTestManager.IpBox();
            this.GateWayBox = new DtuTestManager.IpBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // combox_NetSel
            // 
            this.combox_NetSel.FormattingEnabled = true;
            this.combox_NetSel.Location = new System.Drawing.Point(139, 30);
            this.combox_NetSel.Name = "combox_NetSel";
            this.combox_NetSel.Size = new System.Drawing.Size(81, 20);
            this.combox_NetSel.TabIndex = 42;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(74, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 41;
            this.label6.Text = "端口:";
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.ForeColor = System.Drawing.Color.Black;
            this.btn_Cancel.Location = new System.Drawing.Point(275, 214);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 40;
            this.btn_Cancel.Text = "取消";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            // 
            // btn_Enter
            // 
            this.btn_Enter.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btn_Enter.ForeColor = System.Drawing.Color.Black;
            this.btn_Enter.Location = new System.Drawing.Point(102, 214);
            this.btn_Enter.Name = "btn_Enter";
            this.btn_Enter.Size = new System.Drawing.Size(75, 23);
            this.btn_Enter.TabIndex = 39;
            this.btn_Enter.Text = "确定";
            this.btn_Enter.UseVisualStyleBackColor = true;
            this.btn_Enter.Click += new System.EventHandler(this.btn_Enter_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(74, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 24;
            this.label2.Text = "子网掩码:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "目标网段:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(74, 161);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 12);
            this.label10.TabIndex = 43;
            this.label10.Text = "网关:";
            // 
            // MaskBox
            // 
            this.MaskBox.Location = new System.Drawing.Point(139, 113);
            this.MaskBox.Name = "MaskBox";
            this.MaskBox.Size = new System.Drawing.Size(136, 20);
            this.MaskBox.TabIndex = 51;
            // 
            // SegBox
            // 
            this.SegBox.Location = new System.Drawing.Point(139, 67);
            this.SegBox.Name = "SegBox";
            this.SegBox.Size = new System.Drawing.Size(136, 20);
            this.SegBox.TabIndex = 52;
            // 
            // GateWayBox
            // 
            this.GateWayBox.Location = new System.Drawing.Point(139, 161);
            this.GateWayBox.Name = "GateWayBox";
            this.GateWayBox.Size = new System.Drawing.Size(136, 20);
            this.GateWayBox.TabIndex = 53;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(298, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 54;
            this.label3.Text = "(格式:x.x.x.0)";
            // 
            // CAddGateWay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.ClientSize = new System.Drawing.Size(452, 262);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.GateWayBox);
            this.Controls.Add(this.SegBox);
            this.Controls.Add(this.MaskBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.combox_NetSel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Enter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "CAddGateWay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "添加网关";
            this.Load += new System.EventHandler(this.CAddGateWay_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox combox_NetSel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Enter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label10;
        private IpBox MaskBox;
        private IpBox SegBox;
        private IpBox GateWayBox;
        private System.Windows.Forms.Label label3;
    }
}