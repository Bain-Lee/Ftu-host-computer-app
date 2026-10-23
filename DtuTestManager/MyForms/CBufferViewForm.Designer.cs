namespace DtuTestManager
{
    partial class CBufferViewForm
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
            this.BufferText = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.BtnStartFreshBuff = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnStopFreshBuff = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnClearBuff = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnSave = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BufferText
            // 
            this.BufferText.BackColor = System.Drawing.Color.Azure;
            this.BufferText.ContextMenuStrip = this.contextMenuStrip1;
            this.BufferText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BufferText.Location = new System.Drawing.Point(0, 0);
            this.BufferText.Multiline = true;
            this.BufferText.Name = "BufferText";
            this.BufferText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.BufferText.Size = new System.Drawing.Size(883, 363);
            this.BufferText.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnStartFreshBuff,
            this.BtnStopFreshBuff,
            this.BtnClearBuff,
            this.BtnSave});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 92);
            // 
            // BtnStartFreshBuff
            // 
            this.BtnStartFreshBuff.Name = "BtnStartFreshBuff";
            this.BtnStartFreshBuff.Size = new System.Drawing.Size(124, 22);
            this.BtnStartFreshBuff.Text = "开始刷新";
            this.BtnStartFreshBuff.Click += new System.EventHandler(this.BtnStartFreshBuff_Click);
            // 
            // BtnStopFreshBuff
            // 
            this.BtnStopFreshBuff.Name = "BtnStopFreshBuff";
            this.BtnStopFreshBuff.Size = new System.Drawing.Size(124, 22);
            this.BtnStopFreshBuff.Text = "停止刷新";
            this.BtnStopFreshBuff.Click += new System.EventHandler(this.BtnStopFreshBuff_Click);
            // 
            // BtnClearBuff
            // 
            this.BtnClearBuff.Name = "BtnClearBuff";
            this.BtnClearBuff.Size = new System.Drawing.Size(124, 22);
            this.BtnClearBuff.Text = "清除报文";
            this.BtnClearBuff.Click += new System.EventHandler(this.BtnClearBuff_Click);
            // 
            // BtnSave
            // 
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(124, 22);
            this.BtnSave.Text = "保存报文";
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // CBufferViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 363);
            this.Controls.Add(this.BufferText);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Name = "CBufferViewForm";
            this.Text = "实时报文";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CBufferViewForm_FormClosing);
            this.Load += new System.EventHandler(this.CBufferViewForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox BufferText;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem BtnStartFreshBuff;
        private System.Windows.Forms.ToolStripMenuItem BtnStopFreshBuff;
        private System.Windows.Forms.ToolStripMenuItem BtnClearBuff;
        private System.Windows.Forms.ToolStripMenuItem BtnSave;
    }
}