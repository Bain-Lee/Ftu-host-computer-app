using System.Windows;
using TerminalWorkbench.Presentation;

namespace DtuTestManager
{
    partial class CDataTranForm
    {
        private void ArrangeDataConversionLayout()
        {
            SourceText.BackColor = System.Drawing.Color.White;
            ItemText.BackColor = System.Drawing.Color.White;
            panel1.Dock = DockStyle.Fill;
            var source = new GroupBox
            {
                Name = "ConversionSourceGroup",
                Text = "待转换内容"
            };
            source.Controls.Add(panel1);

            var result = new GroupBox
            {
                Name = "ConversionResultGroup",
                Text = "转换结果"
            };
            result.Controls.Add(ItemText);

            var layout = new TableLayoutPanel
            {
                Name = "DataConversionLayout",
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3
            };
            layout.RowHeights.Add(new GridLength(1, GridUnitType.Star));
            layout.RowHeights.Add(GridLength.Auto);
            layout.RowHeights.Add(new GridLength(1, GridUnitType.Star));
            layout.Controls.Add(source, 0, 0);
            layout.Controls.Add(toolStrip1, 0, 1);
            layout.Controls.Add(result, 0, 2);
            Controls.Clear();
            Controls.Add(layout);
        }
    }
}
