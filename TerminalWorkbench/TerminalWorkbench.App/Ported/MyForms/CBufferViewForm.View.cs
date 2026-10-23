using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CBufferViewForm
    {
        private TextBox BufferText;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem BtnStartFreshBuff;
        private ToolStripMenuItem BtnStopFreshBuff;
        private ToolStripMenuItem BtnClearBuff;
        private ToolStripMenuItem BtnSave;
        private void InitializeComponent()
        {
            BufferText = new TextBox();
            contextMenuStrip1 = new ContextMenuStrip();
            BtnStartFreshBuff = new ToolStripMenuItem();
            BtnStopFreshBuff = new ToolStripMenuItem();
            BtnClearBuff = new ToolStripMenuItem();
            BtnSave = new ToolStripMenuItem();
            this.BufferText.BackColor = System.Drawing.Color.Azure;
            this.BufferText.ContextMenuStrip = this.contextMenuStrip1;
            this.BufferText.Dock = DockStyle.Fill;
            this.BufferText.Location = new System.Drawing.Point(0, 0);
            this.BufferText.Multiline = true;
            this.BufferText.Name = "BufferText";
            this.BufferText.Size = new System.Drawing.Size(883, 363);
            this.BufferText.TabIndex = 0;
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 92);
            this.BtnStartFreshBuff.Name = "BtnStartFreshBuff";
            this.BtnStartFreshBuff.Size = new System.Drawing.Size(124, 22);
            this.BtnStartFreshBuff.Text = "开始刷新";
            this.BtnStopFreshBuff.Name = "BtnStopFreshBuff";
            this.BtnStopFreshBuff.Size = new System.Drawing.Size(124, 22);
            this.BtnStopFreshBuff.Text = "停止刷新";
            this.BtnClearBuff.Name = "BtnClearBuff";
            this.BtnClearBuff.Size = new System.Drawing.Size(124, 22);
            this.BtnClearBuff.Text = "清除报文";
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(124, 22);
            this.BtnSave.Text = "保存报文";
            this.ClientSize = new System.Drawing.Size(883, 363);
            this.Name = "CBufferViewForm";
            this.Text = "实时报文";
            this.contextMenuStrip1.Items.AddRange(new Control[] {
            this.BtnStartFreshBuff,
            this.BtnStopFreshBuff,
            this.BtnClearBuff,
            this.BtnSave});
            this.Controls.Add(this.BufferText);
            BtnStartFreshBuff.Click += BtnStartFreshBuff_Click;
            BtnStopFreshBuff.Click += BtnStopFreshBuff_Click;
            BtnClearBuff.Click += BtnClearBuff_Click;
            BtnSave.Click += BtnSave_Click;
            FormClosing += CBufferViewForm_FormClosing;
            Load += CBufferViewForm_Load;
        }
    }
}
