using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CEventMsgForm
    {
        private ListView Event_View;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem BtnClear;
        private void InitializeComponent()
        {
            Event_View = new ListView();
            contextMenuStrip1 = new ContextMenuStrip();
            BtnClear = new ToolStripMenuItem();
            this.Event_View.BackColor = System.Drawing.Color.Azure;
            this.Event_View.ContextMenuStrip = this.contextMenuStrip1;
            this.Event_View.Dock = DockStyle.Fill;
            this.Event_View.Location = new System.Drawing.Point(0, 0);
            this.Event_View.Name = "Event_View";
            this.Event_View.Size = new System.Drawing.Size(824, 363);
            this.Event_View.TabIndex = 0;
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(100, 22);
            this.BtnClear.Text = "清除";
            this.ClientSize = new System.Drawing.Size(824, 363);
            this.Name = "CEventMsgForm";
            this.Text = "事件信息";
            this.contextMenuStrip1.Items.AddRange(new Control[] {
            this.BtnClear});
            this.Controls.Add(this.Event_View);
            BtnClear.Click += BtnClear_Click;
            FormClosing += CEventMsgForm_FormClosing;
            Load += CEventMsgForm_Load;
        }
    }
}
