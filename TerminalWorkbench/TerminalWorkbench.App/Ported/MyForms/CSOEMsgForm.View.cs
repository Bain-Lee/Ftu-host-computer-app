using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CSOEMsgForm
    {
        private ListView SOE_View;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem BtnClearSOE;
        private void InitializeComponent()
        {
            SOE_View = new ListView();
            contextMenuStrip1 = new ContextMenuStrip();
            BtnClearSOE = new ToolStripMenuItem();
            this.SOE_View.BackColor = System.Drawing.Color.Azure;
            this.SOE_View.ContextMenuStrip = this.contextMenuStrip1;
            this.SOE_View.Dock = DockStyle.Fill;
            this.SOE_View.Location = new System.Drawing.Point(0, 0);
            this.SOE_View.Name = "SOE_View";
            this.SOE_View.Size = new System.Drawing.Size(824, 363);
            this.SOE_View.TabIndex = 0;
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 26);
            this.BtnClearSOE.Name = "BtnClearSOE";
            this.BtnClearSOE.Size = new System.Drawing.Size(148, 22);
            this.BtnClearSOE.Text = "清空变位信息";
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(824, 363);
            this.Name = "CSOEMsgForm";
            this.Text = "SOE信息";
            this.contextMenuStrip1.Items.AddRange(new Control[] {
            this.BtnClearSOE});
            this.Controls.Add(this.SOE_View);
            BtnClearSOE.Click += BtnClearSOE_Click;
            FormClosing += CSOEMsgForm_FormClosing;
            Load += CSOEMsgForm_Load;
        }
    }
}
