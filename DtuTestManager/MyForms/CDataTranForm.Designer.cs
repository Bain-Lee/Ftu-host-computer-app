namespace DtuTestManager
{
    partial class CDataTranForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CDataTranForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.SourceText = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.BtTran1 = new System.Windows.Forms.ToolStripButton();
            this.BtTran5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.BtTran2 = new System.Windows.Forms.ToolStripButton();
            this.BtTran6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.BtTran3 = new System.Windows.Forms.ToolStripButton();
            this.BtTran7 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.BtTran4 = new System.Windows.Forms.ToolStripButton();
            this.BtTran8 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnClear = new System.Windows.Forms.ToolStripButton();
            this.ItemText = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.SourceText);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1312, 155);
            this.panel1.TabIndex = 0;
            // 
            // SourceText
            // 
            this.SourceText.BackColor = System.Drawing.Color.Azure;
            this.SourceText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SourceText.Location = new System.Drawing.Point(0, 0);
            this.SourceText.Name = "SourceText";
            this.SourceText.Size = new System.Drawing.Size(1312, 155);
            this.SourceText.TabIndex = 0;
            this.SourceText.Text = "";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.MediumAquamarine;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtTran1,
            this.BtTran5,
            this.toolStripSeparator1,
            this.BtTran2,
            this.BtTran6,
            this.toolStripSeparator2,
            this.BtTran3,
            this.BtTran7,
            this.toolStripSeparator3,
            this.BtTran4,
            this.BtTran8,
            this.toolStripSeparator4,
            this.BtnClear});
            this.toolStrip1.Location = new System.Drawing.Point(0, 155);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1312, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // BtTran1
            // 
            this.BtTran1.Image = ((System.Drawing.Image)(resources.GetObject("BtTran1.Image")));
            this.BtTran1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtTran1.Name = "BtTran1";
            this.BtTran1.Size = new System.Drawing.Size(88, 22);
            this.BtTran1.Text = "报文转浮点";
            this.BtTran1.Click += new System.EventHandler(this.BtTran1_Click);
            // 
            // BtTran5
            // 
            this.BtTran5.Image = ((System.Drawing.Image)(resources.GetObject("BtTran5.Image")));
            this.BtTran5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtTran5.Name = "BtTran5";
            this.BtTran5.Size = new System.Drawing.Size(88, 22);
            this.BtTran5.Text = "浮点转报文";
            this.BtTran5.Click += new System.EventHandler(this.BtTran5_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // BtTran2
            // 
            this.BtTran2.Image = ((System.Drawing.Image)(resources.GetObject("BtTran2.Image")));
            this.BtTran2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtTran2.Name = "BtTran2";
            this.BtTran2.Size = new System.Drawing.Size(88, 22);
            this.BtTran2.Text = "报文转整型";
            this.BtTran2.Click += new System.EventHandler(this.BtTran2_Click);
            // 
            // BtTran6
            // 
            this.BtTran6.Image = ((System.Drawing.Image)(resources.GetObject("BtTran6.Image")));
            this.BtTran6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtTran6.Name = "BtTran6";
            this.BtTran6.Size = new System.Drawing.Size(88, 22);
            this.BtTran6.Text = "整型转报文";
            this.BtTran6.Click += new System.EventHandler(this.BtTran6_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // BtTran3
            // 
            this.BtTran3.Image = ((System.Drawing.Image)(resources.GetObject("BtTran3.Image")));
            this.BtTran3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtTran3.Name = "BtTran3";
            this.BtTran3.Size = new System.Drawing.Size(124, 22);
            this.BtTran3.Text = "报文转无符号整型";
            this.BtTran3.Click += new System.EventHandler(this.BtTran3_Click);
            // 
            // BtTran7
            // 
            this.BtTran7.Image = ((System.Drawing.Image)(resources.GetObject("BtTran7.Image")));
            this.BtTran7.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtTran7.Name = "BtTran7";
            this.BtTran7.Size = new System.Drawing.Size(124, 22);
            this.BtTran7.Text = "无符号整型转报文";
            this.BtTran7.Click += new System.EventHandler(this.BtTran7_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // BtTran4
            // 
            this.BtTran4.Image = ((System.Drawing.Image)(resources.GetObject("BtTran4.Image")));
            this.BtTran4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtTran4.Name = "BtTran4";
            this.BtTran4.Size = new System.Drawing.Size(100, 22);
            this.BtTran4.Text = "报文转字符串";
            this.BtTran4.Click += new System.EventHandler(this.BtTran4_Click);
            // 
            // BtTran8
            // 
            this.BtTran8.Image = ((System.Drawing.Image)(resources.GetObject("BtTran8.Image")));
            this.BtTran8.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtTran8.Name = "BtTran8";
            this.BtTran8.Size = new System.Drawing.Size(100, 22);
            this.BtTran8.Text = "字符串转报文";
            this.BtTran8.Click += new System.EventHandler(this.BtTran8_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // BtnClear
            // 
            this.BtnClear.Image = ((System.Drawing.Image)(resources.GetObject("BtnClear.Image")));
            this.BtnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(52, 22);
            this.BtnClear.Text = "清空";
            this.BtnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // ItemText
            // 
            this.ItemText.BackColor = System.Drawing.Color.PowderBlue;
            this.ItemText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ItemText.Location = new System.Drawing.Point(0, 180);
            this.ItemText.Name = "ItemText";
            this.ItemText.Size = new System.Drawing.Size(1312, 469);
            this.ItemText.TabIndex = 2;
            this.ItemText.Text = "";
            // 
            // CDataTranForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(1312, 649);
            this.Controls.Add(this.ItemText);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CDataTranForm";
            this.Text = "数据转换";
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox SourceText;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton BtTran1;
        private System.Windows.Forms.ToolStripButton BtTran2;
        private System.Windows.Forms.ToolStripButton BtTran3;
        private System.Windows.Forms.ToolStripButton BtTran4;
        private System.Windows.Forms.ToolStripButton BtTran6;
        private System.Windows.Forms.ToolStripButton BtTran7;
        private System.Windows.Forms.ToolStripButton BtTran8;
        private System.Windows.Forms.ToolStripButton BtTran5;
        private System.Windows.Forms.RichTextBox ItemText;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton BtnClear;
    }
}