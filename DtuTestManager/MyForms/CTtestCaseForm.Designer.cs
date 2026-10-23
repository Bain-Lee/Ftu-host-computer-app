namespace DtuTestManager
{
    partial class CTtestCaseForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CTtestCaseForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.BtnConnectDev = new System.Windows.Forms.ToolStripButton();
            this.BtnSendOrder = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TestCaseView = new DtuTestManager.MyListView(this.components);
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Lavender;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnConnectDev,
            this.BtnSendOrder});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1300, 31);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // BtnConnectDev
            // 
            this.BtnConnectDev.Image = ((System.Drawing.Image)(resources.GetObject("BtnConnectDev.Image")));
            this.BtnConnectDev.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnConnectDev.Name = "BtnConnectDev";
            this.BtnConnectDev.Size = new System.Drawing.Size(84, 28);
            this.BtnConnectDev.Text = "连接装置";
            this.BtnConnectDev.Click += new System.EventHandler(this.BtnConnectDev_Click);
            // 
            // BtnSendOrder
            // 
            this.BtnSendOrder.Image = ((System.Drawing.Image)(resources.GetObject("BtnSendOrder.Image")));
            this.BtnSendOrder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnSendOrder.Name = "BtnSendOrder";
            this.BtnSendOrder.Size = new System.Drawing.Size(84, 28);
            this.BtnSendOrder.Text = "下发命令";
            this.BtnSendOrder.Click += new System.EventHandler(this.BtnSendOrder_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TestCaseView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1300, 567);
            this.panel1.TabIndex = 1;
            // 
            // TestCaseView
            // 
            this.TestCaseView.BackColor = System.Drawing.Color.GhostWhite;
            this.TestCaseView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TestCaseView.FullRowSelect = true;
            this.TestCaseView.GridLines = true;
            this.TestCaseView.Location = new System.Drawing.Point(0, 0);
            this.TestCaseView.Name = "TestCaseView";
            this.TestCaseView.OwnerDraw = true;
            this.TestCaseView.ProgressColumnIndex = -1;
            this.TestCaseView.Size = new System.Drawing.Size(1300, 567);
            this.TestCaseView.TabIndex = 0;
            this.TestCaseView.UseCompatibleStateImageBehavior = false;
            this.TestCaseView.View = System.Windows.Forms.View.Details;
            this.TestCaseView.SelectedIndexChanged += new System.EventHandler(this.TestCaseView_SelectedIndexChanged);
            // 
            // CTtestCaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(1300, 598);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CTtestCaseForm";
            this.Text = "CTtestCaseForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CTtestCaseForm_FormClosing);
            this.Load += new System.EventHandler(this.CTtestCaseForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton BtnSendOrder;
        private System.Windows.Forms.Panel panel1;
        private MyListView TestCaseView;
        private System.Windows.Forms.ToolStripButton BtnConnectDev;
    }
}