using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CSpeParaForm
    {
        private Label label1;
        private TextBox IDvalue;
        private Button button1;
        private Button button2;
        private Label label2;
        private void InitializeComponent()
        {
            label1 = new Label();
            IDvalue = new TextBox();
            button1 = new Button();
            button2 = new Button();
            label2 = new Label();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "装置ID：";
            this.IDvalue.Location = new System.Drawing.Point(69, 51);
            this.IDvalue.Name = "IDvalue";
            this.IDvalue.Size = new System.Drawing.Size(181, 21);
            this.IDvalue.TabIndex = 1;
            this.button1.Location = new System.Drawing.Point(56, 106);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "读取";
            this.button2.Location = new System.Drawing.Point(156, 106);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "下发";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(67, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 12);
            this.label2.TabIndex = 3;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Name = "CSpeParaForm";
            this.Text = "厂站参数";
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.IDvalue);
            this.Controls.Add(this.label1);
            IDvalue.TextChanged += textBox1_TextChanged;
            button1.Click += button1_Click;
            button2.Click += button2_Click;
            Load += CSpeParaForm_Load;
        }
    }
}
