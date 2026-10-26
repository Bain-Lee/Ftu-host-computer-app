using System.Windows;
using TerminalWorkbench.Presentation;

namespace DtuTestManager
{
    partial class CGetSelectParaForm
    {
        private void ArrangeParameterSelectionLayout()
        {
            ParaGroupTree.Dock = DockStyle.Fill;
            ParaListView.Dock = DockStyle.Fill;
            HaveSelectView.Dock = DockStyle.Fill;

            var groups = new GroupBox { Name = "ParameterGroups", Text = "参数分组" };
            groups.Controls.Add(ParaGroupTree);
            var available = new GroupBox { Name = "AvailableParameters", Text = "可选参数" };
            available.Controls.Add(ParaListView);
            var selected = new GroupBox { Name = "SelectedParameters", Text = "已选参数" };
            selected.Controls.Add(HaveSelectView);

            var actions = new TableLayoutPanel
            {
                Name = "ParameterSelectionActions",
                ColumnCount = 1,
                RowCount = 5
            };
            actions.RowHeights.Add(new GridLength(1, GridUnitType.Star));
            actions.RowHeights.Add(GridLength.Auto);
            actions.RowHeights.Add(GridLength.Auto);
            actions.RowHeights.Add(GridLength.Auto);
            actions.RowHeights.Add(new GridLength(1, GridUnitType.Star));
            actions.Controls.Add(BtnAddSelect, 0, 1);
            actions.Controls.Add(BtnInsert, 0, 2);
            actions.Controls.Add(BtnDelete, 0, 3);

            var parameters = new TableLayoutPanel
            {
                Name = "ParameterSelectionColumns",
                ColumnCount = 4,
                RowCount = 1
            };
            parameters.ColumnWidths.Add(new GridLength(200));
            parameters.ColumnWidths.Add(new GridLength(1, GridUnitType.Star));
            parameters.ColumnWidths.Add(GridLength.Auto);
            parameters.ColumnWidths.Add(new GridLength(1, GridUnitType.Star));
            parameters.Controls.Add(groups, 0, 0);
            parameters.Controls.Add(available, 1, 0);
            parameters.Controls.Add(actions, 2, 0);
            parameters.Controls.Add(selected, 3, 0);

            var confirmation = new ToolStrip
            {
                Name = "ParameterSelectionConfirmation",
                Dock = DockStyle.Fill
            };
            confirmation.Items.AddRange(new Control[] { BtnSetOK, BtnCancel });
            panel1.Controls.Clear();
            panel1.Dock = DockStyle.Fill;
            panel1.Controls.Add(confirmation);

            var layout = new TableLayoutPanel
            {
                Name = "ParameterSelectionLayout",
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };
            layout.RowHeights.Add(new GridLength(1, GridUnitType.Star));
            layout.RowHeights.Add(GridLength.Auto);
            layout.Controls.Add(parameters, 0, 0);
            layout.Controls.Add(panel1, 0, 1);
            Controls.Clear();
            Controls.Add(layout);
        }
    }
}
