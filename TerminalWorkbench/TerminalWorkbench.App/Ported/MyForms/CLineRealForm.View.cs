using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CLineRealForm
    {
        private Panel panel1;
        private MyListView LineSetView;
        private ToolStrip toolStrip1;
        private ToolStripButton ClearZero;
        private ToolStripLabel toolStripLabel1;
        private ToolStripTextBox AddSpaceCount;
        private ToolStripButton FastSet;
        private ToolStripButton SaveSet;
        private ToolStripLabel toolStripLabel2;
        private ToolStripTextBox DC1Index;
        private ToolStripLabel toolStripLabel3;
        private ToolStripTextBox DC2Index;
        private void InitializeComponent()
        {
            panel1 = new Panel();
            LineSetView = new MyListView();
            toolStrip1 = new ToolStrip();
            ClearZero = new ToolStripButton();
            toolStripLabel1 = new ToolStripLabel();
            AddSpaceCount = new ToolStripTextBox();
            FastSet = new ToolStripButton();
            SaveSet = new ToolStripButton();
            toolStripLabel2 = new ToolStripLabel();
            DC1Index = new ToolStripTextBox();
            toolStripLabel3 = new ToolStripLabel();
            DC2Index = new ToolStripTextBox();
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(881, 428);
            this.panel1.TabIndex = 0;
            this.LineSetView.BackColor = System.Drawing.Color.Azure;
            this.LineSetView.Dock = DockStyle.Fill;
            this.LineSetView.Location = new System.Drawing.Point(0, 25);
            this.LineSetView.Name = "LineSetView";
            this.LineSetView.Size = new System.Drawing.Size(881, 403);
            this.LineSetView.TabIndex = 0;
            this.toolStrip1.BackColor = System.Drawing.Color.LightBlue;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(881, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            this.ClearZero.Name = "ClearZero";
            this.ClearZero.Size = new System.Drawing.Size(52, 22);
            this.ClearZero.Text = "清零";
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(84, 22);
            this.toolStripLabel1.Text = "    累加间隔：";
            this.AddSpaceCount.Name = "AddSpaceCount";
            this.AddSpaceCount.Size = new System.Drawing.Size(100, 25);
            this.AddSpaceCount.Text = "18";
            this.FastSet.Name = "FastSet";
            this.FastSet.Size = new System.Drawing.Size(76, 22);
            this.FastSet.Text = "快速设置";
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(75, 22);
            this.toolStripLabel2.Text = "直流1索引：";
            this.DC1Index.Name = "DC1Index";
            this.DC1Index.Size = new System.Drawing.Size(100, 25);
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(75, 22);
            this.toolStripLabel3.Text = "直流2索引：";
            this.DC2Index.Name = "DC2Index";
            this.DC2Index.Size = new System.Drawing.Size(100, 25);
            this.SaveSet.Name = "SaveSet";
            this.SaveSet.Size = new System.Drawing.Size(52, 22);
            this.SaveSet.Text = "保存";
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(881, 428);
            this.Name = "CLineRealForm";
            this.Text = "CLineRealForm";
            this.panel1.Controls.Add(this.LineSetView);
            this.panel1.Controls.Add(this.toolStrip1);
            this.toolStrip1.Items.AddRange(new Control[] {
            this.ClearZero,
            this.toolStripLabel1,
            this.AddSpaceCount,
            this.FastSet,
            this.toolStripLabel2,
            this.DC1Index,
            this.toolStripLabel3,
            this.DC2Index,
            this.SaveSet});
            this.Controls.Add(this.panel1);
            ClearZero.Click += ClearZero_Click;
            FastSet.Click += FastSet_Click;
            SaveSet.Click += SaveSet_Click;
            Load += CLineRealForm_Load;
        }
    }
}
