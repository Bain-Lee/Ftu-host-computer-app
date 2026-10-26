using System;
using System.Windows;
using TerminalWorkbench.Presentation;

namespace DtuTestManager
{
    partial class CSecrteKeyForm
    {
        private CNormalTabControl encryptionTabs;

        private void ArrangeEncryptionLayout()
        {
            ClientSize = new System.Drawing.Size(1180, 720);
            toolStripLabel1.Text = "密钥索引：";
            toolStripLabel2.Text = "执行状态：";
            toolStrip1.Dock = DockStyle.Top;

            SetEncryptionGroup(groupBox2, EncryptionStack("MasterEncryptionSections",
                EncryptionGroup("MasterIdentity", "身份认证与版本", EncryptionButtons("MasterIdentityButtons",
                    BtnGateDegree, BtnGateAnswer, BtnIdentity, BtnMasterAnswer,
                    BtnTermReadChipNo, BtnReadVersion, BtnAutoCheck)),
                EncryptionGroup("MasterKeys", "密钥管理", EncryptionButtons("MasterKeyButtons",
                    BtnSecretUpdate, BtnSecretReset)),
                EncryptionGroup("MasterCertificates", "证书管理", EncryptionStack("MasterCertificateFields",
                    EncryptionChoices("MasterCertificateChoice", Cred1, Cred2, Cred3, Cred4, Cred5, Cred6),
                    EncryptionButtons("MasterCertificateButtons", BtnCredUpdate, BtnCredInput, BtnCredDown)))));

            SetEncryptionGroup(groupBox3, EncryptionStack("ServiceEncryptionSections",
                EncryptionGroup("ServiceIdentity", "身份认证与版本", EncryptionButtons("ServiceIdentityButtons",
                    BtnDegree, BtnYWRequest, BtnYWReadVersion, BtnReadNo, BtnReadChipNo, BtnReadKey)),
                EncryptionGroup("ServiceCertificates", "证书管理", EncryptionStack("ServiceCertificateFields",
                    EncryptionChoices("ServiceCertificateChoice",
                        ServicCred1, ServicCred2, ServicCred3, ServicCred4, ServicCred5, ServicCred6, ServicCred7),
                    EncryptionButtons("ServiceCertificateButtons", BtnSign, BtnCredWrite, BtnWriteBack, BtnTerDown))),
                EncryptionGroup("ServiceKeysAndData", "密钥与数据", EncryptionButtons("ServiceKeyButtons",
                    BtnResetKey, BtnSendData))));

            SetEncryptionGroup(groupBox1, EncryptionChoices("EncryptionTestChoice",
                Use_Normal, NormalUseErrCS, UseErrSign, UseErrMACCheck, UseErrTime,
                UseErrRand, UseNoSign, UseNoTime, UseNoRand, UseMingWen));

            encryptionTabs = new CNormalTabControl
            {
                Name = "encryptionTabs",
                Dock = DockStyle.Fill,
                SelectedIndex = 0
            };
            encryptionTabs.TabPages.Add(EncryptionPage("MasterEncryptionPage", "主站加密", groupBox2));
            encryptionTabs.TabPages.Add(EncryptionPage("ServiceEncryptionPage", "运维加密", groupBox3));
            encryptionTabs.TabPages.Add(EncryptionPage("EncryptionTestPage", "异常测试", groupBox1));

            var results = new TableLayoutPanel
            {
                Name = "EncryptionResults",
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };
            results.ColumnWidths.Add(new GridLength(1, GridUnitType.Star));
            results.RowHeights.Add(new GridLength(7, GridUnitType.Star));
            results.RowHeights.Add(new GridLength(3, GridUnitType.Star));
            results.Controls.Add(EncryptionGroup("EncryptionData", "认证数据", EncodeParaGrid), 0, 0);
            results.Controls.Add(EncryptionGroup("EncryptionMessages", "执行日志", ResultMsg), 0, 1);

            var layout = new TableLayoutPanel
            {
                Name = "EncryptionLayout",
                Dock = DockStyle.Fill,
                ColumnCount = 3,
                RowCount = 1
            };
            layout.ColumnWidths.Add(new GridLength(1, GridUnitType.Star));
            layout.ColumnWidths.Add(new GridLength(12));
            layout.ColumnWidths.Add(new GridLength(1.15, GridUnitType.Star));
            layout.RowHeights.Add(new GridLength(1, GridUnitType.Star));
            layout.Controls.Add(encryptionTabs, 0, 0);
            layout.Controls.Add(results, 2, 0);

            Controls.Clear();
            Controls.Add(layout);
            Controls.Add(toolStrip1);
            Load += EncryptionLayout_Load;
        }

        private void EncryptionLayout_Load(object sender, EventArgs e)
        {
            EncodeParaGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            EncodeParaGrid.Columns[0].Width = 220;
            EncodeParaGrid.Columns[1].Width = 340;
        }

        private static TabPage EncryptionPage(string name, string title, Control content)
        {
            var page = new TabPage { Name = name, Text = title, Dock = DockStyle.Fill };
            content.Dock = DockStyle.Fill;
            page.Controls.Add(content);
            return page;
        }

        private static GroupBox EncryptionGroup(string name, string title, Control content)
        {
            var group = new GroupBox { Name = name, Text = title, Dock = DockStyle.Fill };
            SetEncryptionGroup(group, content);
            return group;
        }

        private static void SetEncryptionGroup(GroupBox group, Control content)
        {
            group.Controls.Clear();
            group.Dock = DockStyle.Fill;
            content.Dock = DockStyle.Fill;
            group.Controls.Add(content);
        }

        private static TableLayoutPanel EncryptionStack(string name, params Control[] sections)
        {
            var layout = new TableLayoutPanel
            {
                Name = name,
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = sections.Length
            };
            layout.ColumnWidths.Add(new GridLength(1, GridUnitType.Star));
            for (int row = 0; row < sections.Length; row++)
            {
                layout.RowHeights.Add(GridLength.Auto);
                sections[row].Dock = DockStyle.Fill;
                layout.Controls.Add(sections[row], 0, row);
            }
            return layout;
        }

        private static TableLayoutPanel EncryptionButtons(string name, params Control[] buttons)
        {
            var layout = new TableLayoutPanel
            {
                Name = name,
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = (buttons.Length + 1) / 2
            };
            layout.ColumnWidths.Add(new GridLength(1, GridUnitType.Star));
            layout.ColumnWidths.Add(new GridLength(1, GridUnitType.Star));
            for (int row = 0; row < layout.RowCount; row++)
            {
                layout.RowHeights.Add(GridLength.Auto);
            }
            for (int index = 0; index < buttons.Length; index++)
            {
                buttons[index].Dock = DockStyle.Fill;
                layout.Controls.Add(buttons[index], index % 2, index / 2);
            }
            return layout;
        }

        private static ToolStrip EncryptionChoices(string name, params Control[] choices)
        {
            var toolbar = new ToolStrip { Name = name, Dock = DockStyle.Fill };
            foreach (Control choice in choices)
            {
                choice.Dock = DockStyle.Fill;
                toolbar.Items.Add(choice);
            }
            return toolbar;
        }
    }
}
