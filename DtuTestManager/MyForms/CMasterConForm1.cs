using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DtuTestManager
{
    public partial class CMasterConForm : Form
    {
        private int[] m_ComBaund = new int[]{600,1200,2400,4800,9600,14400,19200,38400};

        public CMasterConForm()
        {
            InitializeComponent();
        }

        private void CMasterConParaForm_Load(object sender, EventArgs e)
        {
            string pBackImage = System.Environment.CurrentDirectory + "\\" + "res" + "\\TabFromBack.jpg";
            this.BackgroundImage = Image.FromFile(pBackImage);
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            IniComBox();
            label7.SendToBack();
        }
        private void IniComBox()
        {
            int i;
            for(i=1;i<=20;i++)
            {
                Combox_ComNo.Items.Add(i);
            }
            Combox_ComNo.SelectedIndex = 0;

            foreach (int nBaund in m_ComBaund)
            {
                Combox_ComBaund.Items.Add(nBaund);
            }
            Combox_ComBaund.SelectedIndex = 4;

            Combox_DataBit.Items.Add(6);
            Combox_DataBit.Items.Add(7);
            Combox_DataBit.Items.Add(8);
            Combox_DataBit.SelectedIndex = 2;

            Combox_StopBit.Items.Add(1);
            Combox_StopBit.Items.Add(1.5);
            Combox_StopBit.Items.Add(2);
            Combox_StopBit.SelectedIndex = 0;

            Combox_Parity.Items.Add("无校验");
            Combox_Parity.Items.Add("奇校验");
            Combox_Parity.Items.Add("偶校验");
            Combox_Parity.SelectedIndex = 0;
        }
    }
}
