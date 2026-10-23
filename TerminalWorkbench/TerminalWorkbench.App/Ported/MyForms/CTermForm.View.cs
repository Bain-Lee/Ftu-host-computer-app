using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CTermForm
    {
        private Panel panel1;
        private TreeView TermTreeView;
        private ToolStrip toolStrip2;
        private ToolStripButton AddTermBtn;
        private ToolStripButton CopyTermBtn;
        private ToolStripButton DelTermBtn;
        private Panel panel2;
        private Panel FormPanel;
        private void InitializeComponent()
        {
            panel1 = new Panel();
            TermTreeView = new TreeView();
            toolStrip2 = new ToolStrip();
            AddTermBtn = new ToolStripButton();
            CopyTermBtn = new ToolStripButton();
            DelTermBtn = new ToolStripButton();
            panel2 = new Panel();
            FormPanel = new Panel();
            this.panel1.Dock = DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 474);
            this.panel1.TabIndex = 1;
            this.TermTreeView.BackColor = System.Drawing.Color.LightSkyBlue;
            this.TermTreeView.Dock = DockStyle.Fill;
            this.TermTreeView.Location = new System.Drawing.Point(0, 40);
            this.TermTreeView.Name = "TermTreeView";
            this.TermTreeView.Size = new System.Drawing.Size(200, 434);
            this.TermTreeView.TabIndex = 1;
            this.toolStrip2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(200, 40);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            this.AddTermBtn.Name = "AddTermBtn";
            this.AddTermBtn.Size = new System.Drawing.Size(36, 37);
            this.AddTermBtn.Text = "新建";
            this.CopyTermBtn.Name = "CopyTermBtn";
            this.CopyTermBtn.Size = new System.Drawing.Size(36, 37);
            this.CopyTermBtn.Text = "复制";
            this.DelTermBtn.Name = "DelTermBtn";
            this.DelTermBtn.Size = new System.Drawing.Size(36, 37);
            this.DelTermBtn.Text = "删除";
            this.panel2.Dock = DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(200, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(374, 474);
            this.panel2.TabIndex = 2;
            this.FormPanel.Location = new System.Drawing.Point(52, 12);
            this.FormPanel.Name = "FormPanel";
            this.FormPanel.Size = new System.Drawing.Size(105, 77);
            this.FormPanel.TabIndex = 0;
            this.FormPanel.Visible = false;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(574, 474);
            this.Name = "CTermForm";
            this.Text = "CTermForm";
            this.panel1.Controls.Add(this.TermTreeView);
            this.panel1.Controls.Add(this.toolStrip2);
            this.toolStrip2.Items.AddRange(new Control[] {
            this.AddTermBtn,
            this.CopyTermBtn,
            this.DelTermBtn});
            this.panel2.Controls.Add(this.FormPanel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            TermTreeView.AfterSelect += TermTreeView_AfterSelect;
            AddTermBtn.Click += AddTermBtn_Click;
            CopyTermBtn.Click += CopyTermBtn_Click;
            DelTermBtn.Click += DelTermBtn_Click;
            Load += CTermForm_Load;
        }
    }
}
