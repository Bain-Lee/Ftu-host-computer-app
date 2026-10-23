using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CPriForm
    {
        private Panel panel1;
        private Panel panel2;
        private ToolStrip toolStrip2;
        private TreeView PriTreeView;
        private ToolStripButton AddTermBtn;
        private ToolStripButton CopyTermBtn;
        private ToolStripButton DelTermBtn;
        private Panel FormPanel;
        private void InitializeComponent()
        {
            panel1 = new Panel();
            panel2 = new Panel();
            toolStrip2 = new ToolStrip();
            PriTreeView = new TreeView();
            AddTermBtn = new ToolStripButton();
            CopyTermBtn = new ToolStripButton();
            DelTermBtn = new ToolStripButton();
            FormPanel = new Panel();
            this.panel1.Dock = DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 518);
            this.panel1.TabIndex = 1;
            this.PriTreeView.BackColor = System.Drawing.Color.PowderBlue;
            this.PriTreeView.Dock = DockStyle.Fill;
            this.PriTreeView.Location = new System.Drawing.Point(0, 40);
            this.PriTreeView.Name = "PriTreeView";
            this.PriTreeView.Size = new System.Drawing.Size(200, 478);
            this.PriTreeView.TabIndex = 1;
            this.toolStrip2.BackColor = System.Drawing.Color.PowderBlue;
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
            this.panel2.Size = new System.Drawing.Size(745, 518);
            this.panel2.TabIndex = 1;
            this.FormPanel.Location = new System.Drawing.Point(234, 119);
            this.FormPanel.Name = "FormPanel";
            this.FormPanel.Size = new System.Drawing.Size(200, 100);
            this.FormPanel.TabIndex = 1;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(945, 518);
            this.Name = "CPriForm";
            this.Text = "CPriForm";
            this.panel1.Controls.Add(this.PriTreeView);
            this.panel1.Controls.Add(this.toolStrip2);
            this.toolStrip2.Items.AddRange(new Control[] {
            this.AddTermBtn,
            this.CopyTermBtn,
            this.DelTermBtn});
            this.panel2.Controls.Add(this.FormPanel);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            PriTreeView.AfterSelect += PriTreeView_AfterSelect;
            AddTermBtn.Click += AddTermBtn_Click;
            CopyTermBtn.Click += CopyTermBtn_Click;
            DelTermBtn.Click += DelTermBtn_Click;
            Load += CPriForm_Load;
        }
    }
}
