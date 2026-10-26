using System.Windows;
using TerminalWorkbench.Presentation;

namespace DtuTestManager
{
    partial class CVersionForm
    {
        private void ArrangeBoardLayout()
        {
            ClientSize = new System.Drawing.Size(580, 420);
            var slots = new TableLayoutPanel
            {
                Name = "BoardSlots",
                Dock = DockStyle.Fill,
                ColumnCount = 5,
                RowCount = 8
            };
            slots.ColumnWidths.Add(GridLength.Auto);
            slots.ColumnWidths.Add(new GridLength(1, GridUnitType.Star));
            slots.ColumnWidths.Add(new GridLength(16));
            slots.ColumnWidths.Add(GridLength.Auto);
            slots.ColumnWidths.Add(new GridLength(1, GridUnitType.Star));
            Control[] first = { label1, V1, label2, V2, label3, V3, label4, V4, label5, V5, label13, label14 };
            Control[] second = { label7, V7, label6, V8, label8, V9, label9, V10, label11, V11, label10, V12, label12, V13, label15, label16 };
            for (int row = 0; row < 8; row++)
            {
                slots.RowHeights.Add(GridLength.Auto);
                if (row * 2 < first.Length)
                {
                    slots.Controls.Add(first[row * 2], 0, row);
                    slots.Controls.Add(first[row * 2 + 1], 1, row);
                }
                slots.Controls.Add(second[row * 2], 3, row);
                slots.Controls.Add(second[row * 2 + 1], 4, row);
            }
            panel1.Controls.Clear();
            panel1.Controls.Add(slots);
            groupBox1.Dock = DockStyle.Fill;
            var actions = new ToolStrip { Dock = DockStyle.Bottom };
            actions.Items.AddRange(new Control[] { Btn_Cmd, Btn_Can });
            Controls.Clear();
            Controls.Add(groupBox1);
            Controls.Add(actions);
        }
    }
}
