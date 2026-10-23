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
    public partial class CAddIPForm : Form
    {
        private CIPModule _tempMyIP =null;

        public CIPModule TempMyIP
        {
            get
            {
                return _tempMyIP;
            }
            set
            {
                _tempMyIP = value;
            }
        }

        public CAddIPForm(string form_name)
        {
            InitializeComponent();
            this.Text = form_name;
        }

        private void CAddIPForm_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            //this.Parent = MainForm;
            //this.StartPosition = CenterToParent; //不知道为何通过不了
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            combox_NetSel.Items.Clear();
            combox_NetSel.Items.Add(0);
            combox_NetSel.Items.Add(1);

            if (this.Tag != null)
            {
                CIPModule defaultIP = (CIPModule)this.Tag;
                combox_NetSel.Text = defaultIP.NetPort.ToString();
                ipBox.Text=defaultIP.m_IPString;
                MaskBox.Text = defaultIP.m_SubnetMaskString;

                /*
                IPByte1.Text = defaultIP.m_IP_byte[0].ToString();
                IPByte2.Text = defaultIP.m_IP_byte[1].ToString();
                IPByte3.Text = defaultIP.m_IP_byte[2].ToString();
                IPByte4.Text = defaultIP.m_IP_byte[3].ToString();

                txt_masked1.Text = defaultIP.m_SubnetMask_byte[0].ToString();
                txt_masked2.Text = defaultIP.m_SubnetMask_byte[1].ToString();
                txt_masked3.Text = defaultIP.m_SubnetMask_byte[2].ToString();
                txt_masked4.Text = defaultIP.m_SubnetMask_byte[3].ToString();
                 */
            }
            else
            {
                combox_NetSel.SelectedIndex = 0;

                MaskBox.Text = "255.255.255.0";
                // txt_masked1.Text = "255";
                // txt_masked2.Text = "255";
                // txt_masked3.Text = "255";
                // txt_masked4.Text = "0";
            }
        }

        private void btn_addIP_Click(object sender, EventArgs e)
        {
            if (CParaCtrl4G.IsIP(ipBox.Text) && CParaCtrl4G.IsIP(MaskBox.Text))
            {
                System.Net.IPAddress ipaddress, ipaddress1;

                CIPModule m_IPGroup = new CIPModule();

                m_IPGroup.NetPort = Convert.ToInt32(combox_NetSel.Text);

                m_IPGroup.m_IPString = ipBox.Text;
                ipaddress=System.Net.IPAddress.Parse(m_IPGroup.m_IPString);
                m_IPGroup.m_IP_byte = ipaddress.GetAddressBytes();

                m_IPGroup.m_SubnetMaskString = MaskBox.Text;
                ipaddress1 = System.Net.IPAddress.Parse(m_IPGroup.m_SubnetMaskString);
                m_IPGroup.m_SubnetMask_byte = ipaddress1.GetAddressBytes();

                this._tempMyIP = m_IPGroup;
            }
            else
            {
                MessageBox.Show("地址错误!");
            }              
        }
    }
}
