using System.Windows;
using TerminalWorkbench.Presentation;

namespace DtuTestManager
{
    partial class CModulForm
    {
        private void ArrangeTemplateLayout()
        {
            ClientSize = new System.Drawing.Size(1180, 740);
            panel1.Width = 240;
            panel2.Width = 240;
            var actions = new ToolStrip { Name = "TemplateGroupActions", Dock = DockStyle.Fill };
            actions.Items.AddRange(new Control[]
            {
                FirstAddress,
                TemplateField("ChildTemplateChoice", label1, ChildModuleList, 150),
                TemplateField("ChildTemplatePrefix", label2, FontName, 130),
                AddChildModule,
                ResetAddress
            });
            GroupSetPanel.Controls.Clear();
            GroupSetPanel.Dock = DockStyle.Fill;
            GroupSetPanel.Controls.Add(actions);

            var content = new TableLayoutPanel
            {
                Name = "TemplateContentLayout",
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };
            content.ColumnWidths.Add(new GridLength(1, GridUnitType.Star));
            content.RowHeights.Add(GridLength.Auto);
            content.RowHeights.Add(new GridLength(1, GridUnitType.Star));
            content.Controls.Add(GroupSetPanel, 0, 0);
            content.Controls.Add(FormPanel, 0, 1);
            content.Controls.Add(DevDataView, 0, 1);
            SetViewPanel.Controls.Clear();
            SetViewPanel.Controls.Add(content);
        }

        private static TableLayoutPanel TemplateField(string name, Control label, Control input, double width)
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
