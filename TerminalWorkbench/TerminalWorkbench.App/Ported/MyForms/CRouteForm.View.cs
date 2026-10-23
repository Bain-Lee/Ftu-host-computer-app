using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CRouteForm
    {
        private Panel panel1;
        private TreeView RouteTreeView;
        private ToolStrip toolStrip2;
        private ToolStripButton AddBtn;
        private ToolStripButton CopyBtn;
        private ToolStripButton DelBtn;
        private Panel panel2;
        private Panel FormPanel;
        private void InitializeComponent()
        {
            panel1 = new Panel();
            RouteTreeView = new TreeView();
            toolStrip2 = new ToolStrip();
            AddBtn = new ToolStripButton();
            CopyBtn = new ToolStripButton();
            DelBtn = new ToolStripButton();
            panel2 = new Panel();
            FormPanel = new Panel();
            this.panel1.Dock = DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 474);
            this.panel1.TabIndex = 1;
            this.RouteTreeView.BackColor = System.Drawing.Color.LightSkyBlue;
            this.RouteTreeView.Dock = DockStyle.Fill;
            this.RouteTreeView.Location = new System.Drawing.Point(0, 40);
            this.RouteTreeView.Name = "RouteTreeView";
            this.RouteTreeView.Size = new System.Drawing.Size(200, 434);
            this.RouteTreeView.TabIndex = 1;
            this.toolStrip2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(200, 40);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(36, 37);
            this.AddBtn.Text = "新建";
            this.CopyBtn.Name = "CopyBtn";
            this.CopyBtn.Size = new System.Drawing.Size(36, 37);
            this.CopyBtn.Text = "复制";
            this.DelBtn.Name = "DelBtn";
            this.DelBtn.Size = new System.Drawing.Size(36, 37);
            this.DelBtn.Text = "删除";
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(200, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(374, 474);
            this.panel2.TabIndex = 2;
            this.FormPanel.Location = new System.Drawing.Point(157, 242);
            this.FormPanel.Name = "FormPanel";
            this.FormPanel.Size = new System.Drawing.Size(27, 77);
            this.FormPanel.TabIndex = 0;
            this.FormPanel.Visible = false;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(574, 474);
            this.Name = "CRouteForm";
            this.Text = "CTermForm";
            this.panel1.Controls.Add(this.RouteTreeView);
            this.panel1.Controls.Add(this.toolStrip2);
            this.toolStrip2.Items.AddRange(new Control[] {
            this.AddBtn,
            this.CopyBtn,
            this.DelBtn});
            this.panel2.Controls.Add(this.FormPanel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            RouteTreeView.AfterSelect += RouteTreeView_AfterSelect;
            AddBtn.Click += AddBtn_Click;
            CopyBtn.Click += CopyBtn_Click;
            DelBtn.Click += DelBtn_Click;
            Load += CRouteForm_Load;
        }
    }
}
