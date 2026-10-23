using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using TerminalWorkbench.Presentation;

namespace DtuTestManager
{
    public partial class CVersionForm : Form
    {
        public static UInt32 pIntVersion = 0;//板件类型
        public CVersionForm()
        {
            InitializeComponent();
        }



        private void CVersionForm_Load(object sender, EventArgs e)
        {
            Init_Com();
            Init_Value();
        }
        private void Init_ComBox(ComboBox pBox, bool bYcFlag = true)
        {
            pBox.Items.Add("空");
            pBox.Items.Add("DIDO板");
            if (bYcFlag)
                pBox.Items.Add("YC板");
            else
                pBox.Items.Add("无效");
            pBox.Items.Add("无效");
            pBox.SelectedIndex = 0;
        }
        private void Init_Com()
        {
            Init_ComBox(V1);
            Init_ComBox(V2);
            Init_ComBox(V3);
            Init_ComBox(V4);
            Init_ComBox(V5);

            Init_ComBox(V7, false);
            Init_ComBox(V8, false);
            Init_ComBox(V9, false);
            Init_ComBox(V10, false);
            Init_ComBox(V11, false);
            Init_ComBox(V12, false);
            Init_ComBox(V13, false);
        }
        private void Init_Value()
        {

            UInt32[] nValue = new UInt32[12];
            UInt32 pValue = pIntVersion;
            for (int i = 0; i < 12; i++)
            {
                nValue[i] = pValue % 4;
                pValue = pValue >> 2;
            }
            int nP = 0;
            V13.SelectedIndex = (int)nValue[nP++];
            V12.SelectedIndex = (int)nValue[nP++];
            V11.SelectedIndex = (int)nValue[nP++];
            V10.SelectedIndex = (int)nValue[nP++];
            V9.SelectedIndex = (int)nValue[nP++];
            V8.SelectedIndex = (int)nValue[nP++];
            V7.SelectedIndex = (int)nValue[nP++];
            V5.SelectedIndex = (int)nValue[nP++];
            V4.SelectedIndex = (int)nValue[nP++];
            V3.SelectedIndex = (int)nValue[nP++];
            V2.SelectedIndex = (int)nValue[nP++];
            V1.SelectedIndex = (int)nValue[nP++];
        }


        private void Btn_Cmd_Click(object sender, EventArgs e)
        {
            UInt32 V = (UInt32)((V1.SelectedIndex) * Math.Pow(2, 22));
            V = V + (UInt32)((V2.SelectedIndex) * Math.Pow(2, 20));
            V = V + (UInt32)((V3.SelectedIndex) * Math.Pow(2, 18));
            V = V + (UInt32)((V4.SelectedIndex) * Math.Pow(2, 16));
            V = V + (UInt32)((V5.SelectedIndex) * Math.Pow(2, 14));
            V = V + (UInt32)((V7.SelectedIndex) * Math.Pow(2, 12));
            V = V + (UInt32)((V8.SelectedIndex) * Math.Pow(2, 10));
            V = V + (UInt32)((V9.SelectedIndex) * Math.Pow(2, 8));
            V = V + (UInt32)((V10.SelectedIndex) * Math.Pow(2, 6));
            V = V + (UInt32)((V11.SelectedIndex) * Math.Pow(2, 4));
            V = V + (UInt32)((V12.SelectedIndex) * Math.Pow(2, 2));
            V = V + (UInt32)V13.SelectedIndex;
            pIntVersion = V;
            this.DialogResult = TerminalWorkbench.Presentation.DialogResult.OK;
            this.Close();
        }

        private void Btn_Can_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
