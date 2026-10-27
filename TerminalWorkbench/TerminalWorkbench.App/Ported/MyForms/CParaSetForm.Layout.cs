using System.Windows;
using TerminalWorkbench.Presentation;

namespace DtuTestManager
{
    partial class CParaSetForm
    {
        private void ArrangeParameterTemplateLayout()
        {
            panel1.Width = 220;
            var actions = new ToolStrip { Name = "ParameterTemplateActions", Dock = DockStyle.Fill };
            actions.Items.AddRange(new Control[]
            {
                ParameterTemplateField("ParameterStartAddress", label1, FirstAddress, 100),
                ParameterTemplateField("ParameterChildTemplate", label2, ChildMdList, 150),
                ParameterTemplateField("ParameterPrefix", label3, FontNameList, 130),
                AddChileModule,
                ResetAddNo
            });
            panel3.Controls.Clear();
            panel3.Dock = DockStyle.Fill;
            panel3.Controls.Add(actions);

            var content = new TableLayoutPanel
            {
                Name = "ParameterTemplateContent",
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3
            };
            content.ColumnWidths.Add(new GridLength(1, GridUnitType.Star));
            content.RowHeights.Add(GridLength.Auto);
            content.RowHeights.Add(GridLength.Auto);
            content.RowHeights.Add(new GridLength(1, GridUnitType.Star));
            content.Controls.Add(toolStrip2, 0, 0);
            content.Controls.Add(panel3, 0, 1);
            content.Controls.Add(ParaDataListView, 0, 2);
            panel2.Controls.Clear();
            panel2.Controls.Add(content);
        }

        private static TableLayoutPanel ParameterTemplateField(string name, Control label, Control input, double width)
        {
            var field = new TableLayoutPanel { Name = name, ColumnCount = 2, RowCount = 1 };
            field.ColumnWidths.Add(GridLength.Auto);
            field.ColumnWidths.Add(new GridLength(width));
            field.RowHeights.Add(GridLength.Auto);
            field.Controls.Add(label, 0, 0);
            field.Controls.Add(input, 1, 0);
            return field;
        }
    }
}
