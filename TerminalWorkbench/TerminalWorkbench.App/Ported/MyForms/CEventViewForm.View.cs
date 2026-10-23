using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CEventViewForm
    {
        private ListView EventView;
        private ToolStrip toolStrip1;
        private ToolStripLabel toolStripLabel1;
        private ToolStripLabel EndTimer;
        private void InitializeComponent()
        {
            EventView = new ListView();
            toolStrip1 = new ToolStrip();
            toolStripLabel1 = new ToolStripLabel();
            EndTimer = new ToolStripLabel();
            this.EventView.BackColor = System.Drawing.Color.GhostWhite;
            this.EventView.Dock = DockStyle.Fill;
            this.EventView.Location = new System.Drawing.Point(0, 25);
            this.EventView.Name = "EventView";
            this.EventView.Size = new System.Drawing.Size(1263, 582);
            this.EventView.TabIndex = 0;
            this.toolStrip1.BackColor = System.Drawing.Color.Lavender;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1263, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(59, 22);
            this.toolStripLabel1.Text = "结束时间:";
            this.EndTimer.Name = "EndTimer";
            this.EndTimer.Size = new System.Drawing.Size(16, 22);
            this.EndTimer.Text = "  ";
            this.ClientSize = new System.Drawing.Size(1263, 607);
            this.Name = "CEventViewForm";
            this.Text = "事件信息";
            this.toolStrip1.Items.AddRange(new Control[] {
            this.toolStripLabel1,
            this.EndTimer});
            this.Controls.Add(this.EventView);
            this.Controls.Add(this.toolStrip1);
            Load += CEventViewForm_Load;
        }
    }
}
