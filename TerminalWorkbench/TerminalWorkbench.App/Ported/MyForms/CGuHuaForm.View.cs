using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CGuHuaForm
    {
        private Label label1;
        private Button button1;
        private Button button2;
        private void InitializeComponent()
        {
            label1 = new Label();
            button1 = new Button();
            button2 = new Button();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(237, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "参数预置成功，是否固化？";
            this.button1.Location = new System.Drawing.Point(56, 98);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 34);
            this.button1.TabIndex = 1;
            this.button1.Text = "自动固化";
            this.button2.Location = new System.Drawing.Point(206, 98);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 34);
            this.button2.TabIndex = 2;
            this.button2.Text = "手动固化";
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(364, 185);
            this.Name = "CGuHuaForm";
            this.Text = "固化窗口";
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            button1.Click += button1_Click;
            button2.Click += button2_Click;
            Load += CGuHuaForm_Load;
        }
    }
}
