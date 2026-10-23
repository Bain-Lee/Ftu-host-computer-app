using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CAddCom
    {
        private ComboBox combox_ComIndexSel;
        private Label label6;
        private Button btn_Cancel;
        private Button btn_addIP;
        private Label label1;
        private ComboBox combox_ComBaud;
        private void InitializeComponent()
        {
            combox_ComIndexSel = new ComboBox();
            label6 = new Label();
            btn_Cancel = new Button();
            btn_addIP = new Button();
            label1 = new Label();
            combox_ComBaud = new ComboBox();
            this.combox_ComIndexSel.Location = new System.Drawing.Point(205, 34);
            this.combox_ComIndexSel.Name = "combox_ComIndexSel";
            this.combox_ComIndexSel.Size = new System.Drawing.Size(81, 20);
            this.combox_ComIndexSel.TabIndex = 42;
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(127, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 41;
            this.label6.Text = "串口:";
            this.btn_Cancel.Location = new System.Drawing.Point(278, 135);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 40;
            this.btn_Cancel.Text = "取消";
            this.btn_addIP.Location = new System.Drawing.Point(87, 135);
            this.btn_addIP.Name = "btn_addIP";
            this.btn_addIP.Size = new System.Drawing.Size(75, 23);
            this.btn_addIP.TabIndex = 39;
            this.btn_addIP.Text = "确定";
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(127, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "波特率:";
            this.combox_ComBaud.Location = new System.Drawing.Point(205, 80);
            this.combox_ComBaud.Name = "combox_ComBaud";
            this.combox_ComBaud.Size = new System.Drawing.Size(81, 20);
            this.combox_ComBaud.TabIndex = 43;
            this.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.ClientSize = new System.Drawing.Size(446, 193);
            this.Name = "CAddCom";
            this.Text = "CAddCom";
            this.Controls.Add(this.combox_ComBaud);
            this.Controls.Add(this.combox_ComIndexSel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_addIP);
            this.Controls.Add(this.label1);
            btn_addIP.Click += btn_addIP_Click;
            Load += CAddCom_Load;
        }
    }
}
