using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AxTeeChart;
using TeeChart;

namespace DtuTestManager
{
    public partial class CCurrentForm: Form
    {
        public CCurrentForm()
        {
            InitializeComponent();
        }

        public void show(List<float> Data)
        {
            int i;
            for (i = 0; i < TestTChart.SeriesCount; i++)
            {
                TestTChart.Series(i).Clear();
            }
            TestTChart.Series(0).Title = "曲线";
            float dValue;
            for (i = 0; i < Data.Count; i++)
            {
                dValue = Data[i];
                TestTChart.Series(0).AddXY((double)i, (double)dValue, i.ToString("D4"), 0);
            }
            this.ShowDialog();

        }

        private void CCurrentForm_Load(object sender, EventArgs e)
        {

        }
    }
}
