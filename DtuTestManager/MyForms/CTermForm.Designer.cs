namespace DtuTestManager
{
    partial class CTermForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CTermForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.TermTreeView = new System.Windows.Forms.TreeView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.AddTermBtn = new System.Windows.Forms.ToolStripButton();
            this.CopyTermBtn = new System.Windows.Forms.ToolStripButton();
            this.DelTermBtn = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.FormPanel = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.TermTreeView);
            this.panel1.Controls.Add(this.toolStrip2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 474);
            this.panel1.TabIndex = 1;
            // 
            // TermTreeView
            // 
            this.TermTreeView.BackColor = System.Drawing.Color.LightSkyBlue;
            this.TermTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TermTreeView.HideSelection = false;
            this.TermTreeView.Location = new System.Drawing.Point(0, 40);
            this.TermTreeView.Name = "TermTreeView";
            this.TermTreeView.Size = new System.Drawing.Size(200, 434);
            this.TermTreeView.TabIndex = 1;
            this.TermTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TermTreeView_AfterSelect);
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddTermBtn,
            this.CopyTermBtn,
            this.DelTermBtn});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(200, 40);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // AddTermBtn
            // 
            this.AddTermBtn.Image = ((System.Drawing.Image)(resources.GetObject("AddTermBtn.Image")));
            this.AddTermBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddTermBtn.Name = "AddTermBtn";
            this.AddTermBtn.Size = new System.Drawing.Size(36, 37);
            this.AddTermBtn.Text = "新建";
            this.AddTermBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.AddTermBtn.Click += new System.EventHandler(this.AddTermBtn_Click);
            // 
            // CopyTermBtn
            // 
            this.CopyTermBtn.Image = ((System.Drawing.Image)(resources.GetObject("CopyTermBtn.Image")));
            this.CopyTermBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CopyTermBtn.Name = "CopyTermBtn";
            this.CopyTermBtn.Size = new System.Drawing.Size(36, 37);
            this.CopyTermBtn.Text = "复制";
            this.CopyTermBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.CopyTermBtn.Click += new System.EventHandler(this.CopyTermBtn_Click);
            // 
            // DelTermBtn
            // 
            this.DelTermBtn.Image = ((System.Drawing.Image)(resources.GetObject("DelTermBtn.Image")));
            this.DelTermBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DelTermBtn.Name = "DelTermBtn";
            this.DelTermBtn.Size = new System.Drawing.Size(36, 37);
            this.DelTermBtn.Text = "删除";
            this.DelTermBtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.DelTermBtn.Click += new System.EventHandler(this.DelTermBtn_Click);
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
            this.FormPanel.Location = new System.Drawing.Point(52, 12);
            this.FormPanel.Name = "FormPanel";
            this.FormPanel.Size = new System.Drawing.Size(105, 77);
            this.FormPanel.TabIndex = 0;
            this.FormPanel.Visible = false;
            // 
            // CTermForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(574, 474);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "CTermForm";
            this.Text = "CTermForm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CTermForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TreeView TermTreeView;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton AddTermBtn;
        private System.Windows.Forms.ToolStripButton CopyTermBtn;
        private System.Windows.Forms.ToolStripButton DelTermBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel FormPanel;
    }
}