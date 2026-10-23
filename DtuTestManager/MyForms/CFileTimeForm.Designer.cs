namespace DtuTestManager
{
    partial class CFileTimeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SelectAll = new System.Windows.Forms.CheckBox();
            this.StartTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.EndTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.SetOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SelectAll
            // 
            this.SelectAll.AutoSize = true;
            this.SelectAll.Location = new System.Drawing.Point(95, 12);
            this.SelectAll.Name = "SelectAll";
            this.SelectAll.Size = new System.Drawing.Size(96, 16);
            this.SelectAll.TabIndex = 0;
            this.SelectAll.Text = "召唤所有文件";
            this.SelectAll.UseVisualStyleBackColor = true;
            // 
            // StartTimePicker
            // 
            this.StartTimePicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.StartTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.StartTimePicker.Location = new System.Drawing.Point(95, 43);
            this.StartTimePicker.Name = "StartTimePicker";
            this.StartTimePicker.Size = new System.Drawing.Size(154, 21);
            this.StartTimePicker.TabIndex = 1;
            this.StartTimePicker.Value = new System.DateTime(2017, 8, 21, 0, 0, 0, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "起始时间：";
            // 
            // EndTimePicker
            // 
            this.EndTimePicker.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.EndTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.EndTimePicker.Location = new System.Drawing.Point(95, 89);
            this.EndTimePicker.Name = "EndTimePicker";
            this.EndTimePicker.Size = new System.Drawing.Size(154, 21);
            this.EndTimePicker.TabIndex = 1;
            this.EndTimePicker.Value = new System.DateTime(2017, 8, 21, 14, 18, 14, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "起始时间：";
            // 
            // SetOK
            // 
            this.SetOK.Location = new System.Drawing.Point(95, 131);
            this.SetOK.Name = "SetOK";
            this.SetOK.Size = new System.Drawing.Size(154, 23);
            this.SetOK.TabIndex = 3;
            this.SetOK.Text = "确  认";
            this.SetOK.UseVisualStyleBackColor = true;
            this.SetOK.Click += new System.EventHandler(this.SetOK_Click);
            // 
            // CFileTimeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(338, 178);
            this.Controls.Add(this.SetOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EndTimePicker);
            this.Controls.Add(this.StartTimePicker);
            this.Controls.Add(this.SelectAll);
            this.Name = "CFileTimeForm";
            this.Text = "召唤目录时间选择";
            this.Load += new System.EventHandler(this.CFileTimeForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox SelectAll;
        private System.Windows.Forms.DateTimePicker StartTimePicker;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker EndTimePicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button SetOK;
    }
}