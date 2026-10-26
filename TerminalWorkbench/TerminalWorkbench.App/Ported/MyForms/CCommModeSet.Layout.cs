using System.Windows;
using TerminalWorkbench.Presentation;

namespace DtuTestManager
{
    partial class CommuCFG
    {
        private CNormalTabControl moduleTabs;

        private void ArrangeModuleConfigurationLayout()
        {
            ClientSize = new System.Drawing.Size(1180, 720);
            moduleTabs = new CNormalTabControl
            {
                Name = "moduleTabs",
                Dock = DockStyle.Fill,
                SelectedIndex = 0
            };
            moduleTabs.TabPages.Add(ModuleConfigurationPage("IpModule", "IP 地址", toolStrip2, SetIPGrid));
            moduleTabs.TabPages.Add(ModuleConfigurationPage("GatewayModule", "网关", toolStrip3, SetGateWayGrid));
            moduleTabs.TabPages.Add(ModuleConfigurationPage("MacModule", "MAC 地址", toolStrip5, SetMacGrid));
            moduleTabs.TabPages.Add(ModuleConfigurationPage("SerialModule", "串口", tool_COM, SetComGrid));
            moduleTabs.TabPages.Add(ModuleConfigurationPage("MobileModule", "4G 接入", toolStrip4, Set4GGrid));
            moduleTabs.TabPages.Add(ModuleConfigurationPage("HostModule", "主站通道", toolStrip6, SetHostGrid));
            moduleTabs.TabPages.Add(ModuleConfigurationPage("TerminalModule", "终端通道", toolStrip7, SetDevGrid));

            Controls.Remove(splitContainer4);
            txt_Showbuf.Dock = DockStyle.Bottom;
            Controls.Add(txt_Showbuf);
            Controls.Add(moduleTabs);
            Load += (_, _) =>
            {
                foreach (DataGridView table in new[] { SetIPGrid, SetGateWayGrid, SetHostGrid, SetDevGrid })
                {
                    foreach (DataGridViewColumn column in table.Columns)
                    {
                        column.Width = column.HeaderText.Contains("IP") || column.HeaderText == "子网掩码" || column.HeaderText == "网关" ? 136 : 100;
                    }
                }
            };
        }

        private static TabPage ModuleConfigurationPage(string name, string title, ToolStrip toolbar, DataGridView table)
        {
            var page = new TabPage
            {
                Name = name,
                Text = title,
                Dock = DockStyle.Fill
            };
            var layout = new TableLayoutPanel
            {
                Name = name + "Layout",
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };
            layout.ColumnWidths.Add(new GridLength(1, GridUnitType.Star));
            layout.RowHeights.Add(GridLength.Auto);
            layout.RowHeights.Add(new GridLength(1, GridUnitType.Star));
            toolbar.Dock = DockStyle.Fill;
            table.Dock = DockStyle.Fill;
            layout.Controls.Add(toolbar, 0, 0);
            layout.Controls.Add(table, 0, 1);
            page.Controls.Add(layout);
            return page;
        }
    }
}
