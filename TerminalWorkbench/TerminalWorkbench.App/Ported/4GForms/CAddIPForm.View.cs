using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CAddIPForm
    {
        private Label label1;
        private Label label2;
        private Button btn_addIP;
        private Button btn_Cancel;
        private Label label6;
        private ComboBox combox_NetSel;
        private IpBox ipBox;
        private IpBox MaskBox;
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            btn_addIP = new Button();
            btn_Cancel = new Button();
            label6 = new Label();
            combox_NetSel = new ComboBox();
            ipBox = new IpBox();
            MaskBox = new IpBox();
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(73, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP地址:";
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(73, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "子网掩码:";
            this.btn_addIP.Location = new System.Drawing.Point(85, 149);
            this.btn_addIP.Name = "btn_addIP";
            this.btn_addIP.Size = new System.Drawing.Size(75, 23);
            this.btn_addIP.TabIndex = 19;
            this.btn_addIP.Text = "确定";
            this.btn_Cancel.Location = new System.Drawing.Point(278, 149);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 20;
            this.btn_Cancel.Text = "取消";
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(73, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 21;
            this.label6.Text = "网口:";
            this.combox_NetSel.Location = new System.Drawing.Point(151, 11);
            this.combox_NetSel.Name = "combox_NetSel";
            this.combox_NetSel.Size = new System.Drawing.Size(81, 20);
            this.combox_NetSel.TabIndex = 22;
            this.ipBox.Location = new System.Drawing.Point(151, 55);
            this.ipBox.Name = "ipBox";
            this.ipBox.Size = new System.Drawing.Size(136, 20);
            this.ipBox.TabIndex = 23;
            this.MaskBox.Location = new System.Drawing.Point(151, 100);
            this.MaskBox.Name = "MaskBox";
            this.MaskBox.Size = new System.Drawing.Size(136, 20);
            this.MaskBox.TabIndex = 24;
            this.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.ClientSize = new System.Drawing.Size(476, 203);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "CAddIPForm";
            this.Text = "添加IP地址";
            this.Controls.Add(this.MaskBox);
            this.Controls.Add(this.ipBox);
            this.Controls.Add(this.combox_NetSel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_addIP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            btn_addIP.Click += btn_addIP_Click;
            Load += CAddIPForm_Load;
        }
    }
}
