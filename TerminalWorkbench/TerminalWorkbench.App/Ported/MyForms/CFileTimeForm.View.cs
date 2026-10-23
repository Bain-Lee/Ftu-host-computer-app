using System;
using System.Drawing;
using TerminalWorkbench.Presentation;
namespace DtuTestManager
{
    partial class CFileTimeForm
    {
        private CheckBox SelectAll;
        private DateTimePicker StartTimePicker;
        private Label label1;
        private DateTimePicker EndTimePicker;
        private Label label2;
        private Button SetOK;
        private void InitializeComponent()
        {
            SelectAll = new CheckBox();
            StartTimePicker = new DateTimePicker();
            label1 = new Label();
            EndTimePicker = new DateTimePicker();
            label2 = new Label();
            SetOK = new Button();
            this.SelectAll.AutoSize = true;
            this.SelectAll.Location = new System.Drawing.Point(95, 12);
            this.SelectAll.Name = "SelectAll";
            this.SelectAll.Size = new System.Drawing.Size(96, 16);
            this.SelectAll.TabIndex = 0;
            this.SelectAll.Text = "召唤所有文件";
            this.StartTimePicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.StartTimePicker.Format = DateTimePickerFormat.Custom;
            this.StartTimePicker.Location = new System.Drawing.Point(95, 43);
            this.StartTimePicker.Name = "StartTimePicker";
            this.StartTimePicker.Size = new System.Drawing.Size(154, 21);
            this.StartTimePicker.TabIndex = 1;
            this.StartTimePicker.Value = new System.DateTime(2017, 8, 21, 0, 0, 0, 0);
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "起始时间：";
            this.EndTimePicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.EndTimePicker.Format = DateTimePickerFormat.Custom;
            this.EndTimePicker.Location = new System.Drawing.Point(95, 89);
            this.EndTimePicker.Name = "EndTimePicker";
            this.EndTimePicker.Size = new System.Drawing.Size(154, 21);
            this.EndTimePicker.TabIndex = 1;
            this.EndTimePicker.Value = new System.DateTime(2017, 8, 21, 14, 18, 14, 0);
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "起始时间：";
            this.SetOK.Location = new System.Drawing.Point(95, 131);
            this.SetOK.Name = "SetOK";
            this.SetOK.Size = new System.Drawing.Size(154, 23);
            this.SetOK.TabIndex = 3;
            this.SetOK.Text = "确  认";
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(338, 178);
            this.Name = "CFileTimeForm";
            this.Text = "召唤目录时间选择";
            this.Controls.Add(this.SetOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EndTimePicker);
            this.Controls.Add(this.StartTimePicker);
            this.Controls.Add(this.SelectAll);
            SetOK.Click += SetOK_Click;
            Load += CFileTimeForm_Load;
        }
    }
}
