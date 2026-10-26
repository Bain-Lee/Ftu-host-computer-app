using System.Windows;
using TerminalWorkbench.Presentation;

namespace DtuTestManager
{
    partial class CMasterConForm
    {
        private void ArrangeConfigurationLayout()
        {
            ClientSize = new System.Drawing.Size(1120, 660);
            SaveDataSet.Text = "保存配置";
            groupBox1.Text = "连接通道";
            groupBox2.Text = "网络参数";
            groupBox3.Text = "串口参数";
            groupBox4.Text = "规约与地址";
            groupBox5.Text = "104 信息体地址";
            groupBox6.Text = "101 地址与报文";
            groupBox7.Text = "模板与加密";
            label22.Text = "装置模板：";
            label23.Text = "密钥索引：";
            ISAutoCon.Text = "自动重连";
            IsAutoCall.Text = "定时总召";

            var ipAddress = new TableLayoutPanel
            {
                Name = "NetworkAddressFields",
                Dock = DockStyle.Fill,
                ColumnCount = 7,
                RowCount = 1
            };
            Control[] addressParts = { IPByte1, label12, IPByte2, label13, IPByte3, label14, IPByte4 };
            ipAddress.RowHeights.Add(GridLength.Auto);
            for (int column = 0; column < addressParts.Length; column++)
            {
                ipAddress.ColumnWidths.Add(column % 2 == 0 ? new GridLength(1, GridUnitType.Star) : GridLength.Auto);
                addressParts[column].Dock = DockStyle.Fill;
                ipAddress.Controls.Add(addressParts[column], column, 0);
            }

            SetConfigurationGroup(groupBox3, ConfigurationFields("SerialFields",
                label2, Combox_ComNo,
                label1, Combox_ComBaund,
                label3, Combox_DataBit,
                label4, Combox_StopBit,
                label5, Combox_Parity));
            SetConfigurationGroup(groupBox2, ConfigurationFields("NetworkFields",
                label6, ipAddress,
                label8, ConnPort,
                label7, YxType,
                label24, m_ConnType));
            SetConfigurationGroup(groupBox1, ConfigurationStack("ChannelSections",
                ConfigurationChoices("ChannelChoice", radioButton_Serial, radioButton_Net),
                groupBox3, groupBox2));

            SetConfigurationGroup(groupBox6, ConfigurationFields("Protocol101Fields",
                label15, AsduAddress_101,
                label11, YxAddress_101,
                label9, YcAddress_101,
                label10, YkAddress_101,
                label20, PowerAddress_101,
                label25, comboBox_AddrLen,
                label26, comboBox_SendLen));
            SetConfigurationGroup(groupBox5, ConfigurationFields("Protocol104Fields",
                label19, AsduAddress_104,
                label21, YxAddress_104,
                label16, YcAddress_104,
                label17, YkAddress_104,
                label18, PowerAddress_104));
            SetConfigurationGroup(groupBox4, ConfigurationStack("ProtocolSections",
                ConfigurationChoices("ProtocolChoice", radioButton_101, radioButton_104),
                groupBox6, groupBox5));

            SetConfigurationGroup(groupBox7, ConfigurationStack("TemplateAndSecuritySections",
                ConfigurationFields("TemplateFields", label22, DevModList, label29, SecrestType),
                ConfigurationChoices("EncryptionChoice", radioButton_NoEncode, radioButton_Encode),
                ConfigurationFields("EncryptionFields",
                    label27, EncodeIP,
                    label28, EncodePort,
                    label23, combo_KeyIndex),
                ISAutoCon, IsAutoCall));

            var parameters = new TableLayoutPanel
            {
                Name = "CommunicationConfigurationLayout",
                Dock = DockStyle.Fill,
                ColumnCount = 5,
                RowCount = 1
            };
            parameters.ColumnWidths.Add(new GridLength(1, GridUnitType.Star));
            parameters.ColumnWidths.Add(new GridLength(12));
            parameters.ColumnWidths.Add(new GridLength(1, GridUnitType.Star));
            parameters.ColumnWidths.Add(new GridLength(12));
            parameters.ColumnWidths.Add(new GridLength(1, GridUnitType.Star));
            parameters.RowHeights.Add(new GridLength(1, GridUnitType.Star));
            parameters.Controls.Add(groupBox1, 0, 0);
            parameters.Controls.Add(groupBox4, 2, 0);
            parameters.Controls.Add(groupBox7, 4, 0);

            Controls.Clear();
            Controls.Add(parameters);
            Controls.Add(toolStrip1);
        }

        private static TableLayoutPanel ConfigurationFields(string name, params Control[] fields)
        {
            var layout = new TableLayoutPanel
            {
                Name = name,
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = fields.Length / 2
            };
            layout.ColumnWidths.Add(GridLength.Auto);
            layout.ColumnWidths.Add(new GridLength(1, GridUnitType.Star));
            for (int row = 0; row < layout.RowCount; row++)
            {
                layout.RowHeights.Add(GridLength.Auto);
                fields[row * 2].Dock = DockStyle.Fill;
                fields[row * 2 + 1].Dock = DockStyle.Fill;
                layout.Controls.Add(fields[row * 2], 0, row);
                layout.Controls.Add(fields[row * 2 + 1], 1, row);
            }
            return layout;
        }

        private static TableLayoutPanel ConfigurationChoices(string name, Control first, Control second)
        {
            var layout = new TableLayoutPanel
            {
                Name = name,
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };
            layout.ColumnWidths.Add(new GridLength(1, GridUnitType.Star));
            layout.ColumnWidths.Add(new GridLength(1, GridUnitType.Star));
            layout.RowHeights.Add(GridLength.Auto);
            first.Dock = DockStyle.Fill;
            second.Dock = DockStyle.Fill;
            layout.Controls.Add(first, 0, 0);
            layout.Controls.Add(second, 1, 0);
            return layout;
        }

        private static TableLayoutPanel ConfigurationStack(string name, params Control[] sections)
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

        private static void SetConfigurationGroup(GroupBox group, Control content)
        {
            group.Controls.Clear();
            group.Dock = DockStyle.Fill;
            group.Controls.Add(content);
        }
    }
}
