using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CDataTranForm
    {
        private Panel panel1;
        private RichTextBox SourceText;
        private ToolStrip toolStrip1;
        private ToolStripButton BtTran1;
        private ToolStripButton BtTran2;
        private ToolStripButton BtTran3;
        private ToolStripButton BtTran4;
        private ToolStripButton BtTran6;
        private ToolStripButton BtTran7;
        private ToolStripButton BtTran8;
        private ToolStripButton BtTran5;
        private RichTextBox ItemText;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripButton BtnClear;
        private void InitializeComponent()
        {
            panel1 = new Panel();
            SourceText = new RichTextBox();
            toolStrip1 = new ToolStrip();
            BtTran1 = new ToolStripButton();
            BtTran2 = new ToolStripButton();
            BtTran3 = new ToolStripButton();
            BtTran4 = new ToolStripButton();
            BtTran6 = new ToolStripButton();
            BtTran7 = new ToolStripButton();
            BtTran8 = new ToolStripButton();
            BtTran5 = new ToolStripButton();
            ItemText = new RichTextBox();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripSeparator3 = new ToolStripSeparator();
            toolStripSeparator4 = new ToolStripSeparator();
            BtnClear = new ToolStripButton();
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1312, 155);
            this.panel1.TabIndex = 0;
            this.SourceText.BackColor = System.Drawing.Color.Azure;
            this.SourceText.Dock = DockStyle.Fill;
            this.SourceText.Location = new System.Drawing.Point(0, 0);
            this.SourceText.Name = "SourceText";
            this.SourceText.Size = new System.Drawing.Size(1312, 155);
            this.SourceText.TabIndex = 0;
            this.SourceText.Text = "";
            this.toolStrip1.BackColor = System.Drawing.Color.MediumAquamarine;
            this.toolStrip1.Location = new System.Drawing.Point(0, 155);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1312, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            this.BtTran1.Name = "BtTran1";
            this.BtTran1.Size = new System.Drawing.Size(88, 22);
            this.BtTran1.Text = "报文转浮点";
            this.BtTran5.Name = "BtTran5";
            this.BtTran5.Size = new System.Drawing.Size(88, 22);
            this.BtTran5.Text = "浮点转报文";
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            this.BtTran2.Name = "BtTran2";
            this.BtTran2.Size = new System.Drawing.Size(88, 22);
            this.BtTran2.Text = "报文转整型";
            this.BtTran6.Name = "BtTran6";
            this.BtTran6.Size = new System.Drawing.Size(88, 22);
            this.BtTran6.Text = "整型转报文";
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            this.BtTran3.Name = "BtTran3";
            this.BtTran3.Size = new System.Drawing.Size(124, 22);
            this.BtTran3.Text = "报文转无符号整型";
            this.BtTran7.Name = "BtTran7";
            this.BtTran7.Size = new System.Drawing.Size(124, 22);
            this.BtTran7.Text = "无符号整型转报文";
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            this.BtTran4.Name = "BtTran4";
            this.BtTran4.Size = new System.Drawing.Size(100, 22);
            this.BtTran4.Text = "报文转字符串";
            this.BtTran8.Name = "BtTran8";
            this.BtTran8.Size = new System.Drawing.Size(100, 22);
            this.BtTran8.Text = "字符串转报文";
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            this.BtnClear.Name = "BtnClear";
            this.BtnClear.Size = new System.Drawing.Size(52, 22);
            this.BtnClear.Text = "清空";
            this.ItemText.BackColor = System.Drawing.Color.PowderBlue;
            this.ItemText.Dock = DockStyle.Fill;
            this.ItemText.Location = new System.Drawing.Point(0, 180);
            this.ItemText.Name = "ItemText";
            this.ItemText.Size = new System.Drawing.Size(1312, 469);
            this.ItemText.TabIndex = 2;
            this.ItemText.Text = "";
            this.BackColor = System.Drawing.Color.PaleTurquoise;
            this.ClientSize = new System.Drawing.Size(1312, 649);
            this.Name = "CDataTranForm";
            this.Text = "数据转换";
            this.panel1.Controls.Add(this.SourceText);
            this.toolStrip1.Items.AddRange(new Control[] {
            this.BtTran1,
            this.BtTran5,
            this.toolStripSeparator1,
            this.BtTran2,
            this.BtTran6,
            this.toolStripSeparator2,
            this.BtTran3,
            this.BtTran7,
            this.toolStripSeparator3,
            this.BtTran4,
            this.BtTran8,
            this.toolStripSeparator4,
            this.BtnClear});
            this.Controls.Add(this.ItemText);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            BtTran1.Click += BtTran1_Click;
            BtTran5.Click += BtTran5_Click;
            BtTran2.Click += BtTran2_Click;
            BtTran6.Click += BtTran6_Click;
            BtTran3.Click += BtTran3_Click;
            BtTran7.Click += BtTran7_Click;
            BtTran4.Click += BtTran4_Click;
            BtTran8.Click += BtTran8_Click;
            BtnClear.Click += BtnClear_Click;
            ArrangeDataConversionLayout();
        }
    }
}
