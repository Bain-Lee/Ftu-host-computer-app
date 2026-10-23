using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CEngModPara
    {
        private ToolStrip toolStrip1;
        private ToolStripButton BtnSave;
        private MyListView ParaLV;
        private void InitializeComponent()
        {
            toolStrip1 = new ToolStrip();
            BtnSave = new ToolStripButton();
            ParaLV = new MyListView();
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(811, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(52, 22);
            this.BtnSave.Text = "保存";
            this.ParaLV.Dock = DockStyle.Fill;
            this.ParaLV.Location = new System.Drawing.Point(0, 25);
            this.ParaLV.Name = "ParaLV";
            this.ParaLV.Size = new System.Drawing.Size(811, 222);
            this.ParaLV.TabIndex = 2;
            this.ClientSize = new System.Drawing.Size(811, 247);
            this.Name = "CEngModPara";
            this.Text = "参数配置";
            this.toolStrip1.Items.AddRange(new Control[] {
            this.BtnSave});
            this.Controls.Add(this.ParaLV);
            this.Controls.Add(this.toolStrip1);
            BtnSave.Click += BtnSave_Click;
            FormClosing += CEngModPara_FormClosing;
            Load += CEngParaNewForm_Load;
        }
    }
}
