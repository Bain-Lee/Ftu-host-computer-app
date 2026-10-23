namespace DtuTestManager
{
    partial class CCurrentForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CCurrentForm));
            this.TestTChart = new AxTeeChart.AxTChart();
            ((System.ComponentModel.ISupportInitialize)(this.TestTChart)).BeginInit();
            this.SuspendLayout();
            // 
            // TestTChart
            // 
            this.TestTChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TestTChart.Enabled = true;
            this.TestTChart.Location = new System.Drawing.Point(0, 0);
            this.TestTChart.Name = "TestTChart";
            this.TestTChart.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("TestTChart.OcxState")));
            this.TestTChart.Size = new System.Drawing.Size(852, 433);
            this.TestTChart.TabIndex = 0;
            // 
            // CCurrentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 433);
            this.Controls.Add(this.TestTChart);
            this.Name = "CCurrentForm";
            this.Text = "数据曲线";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.TestTChart)).EndInit();
            this.Load += new System.EventHandler(this.CCurrentForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AxTeeChart.AxTChart TestTChart;




    }
}