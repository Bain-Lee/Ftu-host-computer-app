using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CDiffParaForm
    {
        private Label label1;
        private MyPanel myPanel1;
        private Label label2;
        private MyListView LV;
        private Button button1;
        private void InitializeComponent()
        {
            label1 = new Label();
            myPanel1 = new MyPanel();
            label2 = new Label();
            LV = new MyListView();
            button1 = new Button();
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 0;
            this.myPanel1.Dock = DockStyle.Top;
            this.myPanel1.Location = new System.Drawing.Point(0, 0);
            this.myPanel1.Name = "myPanel1";
            this.myPanel1.Size = new System.Drawing.Size(646, 100);
            this.myPanel1.TabIndex = 1;
            this.button1.Location = new System.Drawing.Point(264, 37);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "保存";
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(200, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "提示：地址相同，类型不同";
            this.LV.Dock = DockStyle.Fill;
            this.LV.Location = new System.Drawing.Point(0, 100);
            this.LV.Name = "LV";
            this.LV.Size = new System.Drawing.Size(646, 329);
            this.LV.TabIndex = 2;
            this.ClientSize = new System.Drawing.Size(646, 429);
            this.Name = "CDiffParaForm";
            this.Text = "CDiffParaForm";
            this.myPanel1.Controls.Add(this.button1);
            this.myPanel1.Controls.Add(this.label2);
            this.Controls.Add(this.LV);
            this.Controls.Add(this.myPanel1);
            this.Controls.Add(this.label1);
            button1.Click += button1_Click;
            LV.SelectedIndexChanged += LV_SelectedIndexChanged;
            LV.DoubleClick += LV_DoubleClick;
            Load += CDiffParaForm_Load;
        }
    }
}
