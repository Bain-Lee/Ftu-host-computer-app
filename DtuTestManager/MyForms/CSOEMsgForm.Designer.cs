namespace DtuTestManager
{
    partial class CSOEMsgForm
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
            this.components = new System.ComponentModel.Container();
            this.SOE_View = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.BtnClearSOE = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SOE_View
            // 
            this.SOE_View.BackColor = System.Drawing.Color.Azure;
            this.SOE_View.ContextMenuStrip = this.contextMenuStrip1;
            this.SOE_View.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SOE_View.Location = new System.Drawing.Point(0, 0);
            this.SOE_View.Name = "SOE_View";
            this.SOE_View.Size = new System.Drawing.Size(824, 363);
            this.SOE_View.TabIndex = 0;
            this.SOE_View.UseCompatibleStateImageBehavior = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnClearSOE});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 26);
            // 
            // BtnClearSOE
            // 
            this.BtnClearSOE.Name = "BtnClearSOE";
            this.BtnClearSOE.Size = new System.Drawing.Size(148, 22);
            this.BtnClearSOE.Text = "清空变位信息";
            this.BtnClearSOE.Click += new System.EventHandler(this.BtnClearSOE_Click);
            // 
            // CSOEMsgForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(824, 363);
            this.Controls.Add(this.SOE_View);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "CSOEMsgForm";
            this.Text = "SOE信息";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CSOEMsgForm_FormClosing);
            this.Load += new System.EventHandler(this.CSOEMsgForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView SOE_View;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem BtnClearSOE;

    }
}