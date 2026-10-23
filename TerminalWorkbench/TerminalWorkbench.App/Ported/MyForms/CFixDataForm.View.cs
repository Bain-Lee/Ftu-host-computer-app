using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CFixDataForm
    {
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private Panel panel1;
        private Button button2;
        private Button button1;
        private TextBox textBox_Value;
        private Label label2;
        private TextBox textBox_No;
        private Label label1;
        private void InitializeComponent()
        {
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            panel1 = new Panel();
            button2 = new Button();
            button1 = new Button();
            textBox_Value = new TextBox();
            label2 = new Label();
            textBox_No = new TextBox();
            label1 = new Label();
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(40, 12);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(47, 16);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.Text = "遥信";
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(116, 12);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(47, 16);
            this.radioButton2.TabIndex = 0;
            this.radioButton2.Text = "遥测";
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(352, 146);
            this.panel1.TabIndex = 1;
            this.button2.Location = new System.Drawing.Point(235, 89);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "解锁";
            this.button1.Location = new System.Drawing.Point(235, 47);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "置数";
            this.textBox_Value.Location = new System.Drawing.Point(88, 92);
            this.textBox_Value.Name = "textBox_Value";
            this.textBox_Value.Size = new System.Drawing.Size(100, 21);
            this.textBox_Value.TabIndex = 2;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "置数值";
            this.textBox_No.Location = new System.Drawing.Point(88, 49);
            this.textBox_No.Name = "textBox_No";
            this.textBox_No.Size = new System.Drawing.Size(100, 21);
            this.textBox_No.TabIndex = 2;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(40, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "序号";
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(352, 146);
            this.Name = "CFixDataForm";
            this.Text = "置数窗口";
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.textBox_Value);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox_No);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.Controls.Add(this.panel1);
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            button2.Click += button2_Click;
            button1.Click += button1_Click;
            Load += CFixDataForm_Load;
        }
    }
}
