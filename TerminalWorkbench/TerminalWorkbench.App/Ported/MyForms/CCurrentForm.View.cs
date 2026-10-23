using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CCurrentForm
    {
        private AxTChart TestTChart;
        private void InitializeComponent()
        {
            TestTChart = new AxTChart();
            this.TestTChart.Dock = DockStyle.Fill;
            this.TestTChart.Enabled = true;
            this.TestTChart.Location = new System.Drawing.Point(0, 0);
            this.TestTChart.Name = "TestTChart";
            this.TestTChart.Size = new System.Drawing.Size(852, 433);
            this.TestTChart.TabIndex = 0;
            this.ClientSize = new System.Drawing.Size(852, 433);
            this.Name = "CCurrentForm";
            this.Text = "数据曲线";
            this.Controls.Add(this.TestTChart);
            Load += CCurrentForm_Load;
        }
    }
}
