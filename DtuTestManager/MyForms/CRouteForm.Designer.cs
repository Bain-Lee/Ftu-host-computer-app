namespace DtuTestManager
{
    partial class CRouteForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CRouteForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.RouteTreeView = new System.Windows.Forms.TreeView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.AddBtn = new System.Windows.Forms.ToolStripButton();
            this.CopyBtn = new System.Windows.Forms.ToolStripButton();
            this.DelBtn = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.FormPanel = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.RouteTreeView);
            this.panel1.Controls.Add(this.toolStrip2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 474);
            this.panel1.TabIndex = 1;
            // 
            // RouteTreeView
            // 
            this.RouteTreeView.BackColor = System.Drawing.Color.LightSkyBlue;
            this.RouteTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RouteTreeView.HideSelection = false;
            this.RouteTreeView.Location = new System.Drawing.Point(0, 40);
            this.RouteTreeView.Name = "RouteTreeView";
            this.RouteTreeView.Size = new System.Drawing.Size(200, 434);
            this.RouteTreeView.TabIndex = 1;
            this.RouteTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.RouteTreeView_AfterSelect);
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddBtn,
            this.CopyBtn,
            this.DelBtn});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(200, 40);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // AddBtn
            // 
            this.AddBtn.Image = ((System.Drawing.Image)(resources.GetObject("AddBtn.Image")));
            this.AddBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(36, 37);
            this.AddBtn.Text = "新建";
            this.AddBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // CopyBtn
            // 
            this.CopyBtn.Image = ((System.Drawing.Image)(resources.GetObject("CopyBtn.Image")));
            this.CopyBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CopyBtn.Name = "CopyBtn";
            this.CopyBtn.Size = new System.Drawing.Size(36, 37);
            this.CopyBtn.Text = "复制";
            this.CopyBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.CopyBtn.Click += new System.EventHandler(this.CopyBtn_Click);
            // 
            // DelBtn
            // 
            this.DelBtn.Image = ((System.Drawing.Image)(resources.GetObject("DelBtn.Image")));
            this.DelBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DelBtn.Name = "DelBtn";
            this.DelBtn.Size = new System.Drawing.Size(36, 37);
            this.DelBtn.Text = "删除";
            this.DelBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.DelBtn.Click += new System.EventHandler(this.DelBtn_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.FormPanel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(200, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(374, 474);
            this.panel2.TabIndex = 2;
            // 
            // FormPanel
            // 
            this.FormPanel.Location = new System.Drawing.Point(157, 242);
            this.FormPanel.Name = "FormPanel";
            this.FormPanel.Size = new System.Drawing.Size(27, 77);
            this.FormPanel.TabIndex = 0;
            this.FormPanel.Visible = false;
            // 
            // CRouteForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(574, 474);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CRouteForm";
            this.Text = "CTermForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CRouteForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView RouteTreeView;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton AddBtn;
        private System.Windows.Forms.ToolStripButton CopyBtn;
        private System.Windows.Forms.ToolStripButton DelBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel FormPanel;
    }
}