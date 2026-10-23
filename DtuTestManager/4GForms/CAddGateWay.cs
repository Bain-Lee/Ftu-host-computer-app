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
    public partial class CAddGateWay : Form
    {
        private CGateWayModule _tempMyGateWay = null;

        public CGateWayModule TempMyGateWay
        {
            get
            {
                return _tempMyGateWay;
            }
            set
            {
                _tempMyGateWay = value;
            }
        }

        public CAddGateWay(string form_name)
        {
            InitializeComponent();
            this.Text = form_name;
        }

        private void CAddGateWay_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            combox_NetSel.Items.Clear();
            combox_NetSel.Items.Add(0);
            combox_NetSel.Items.Add(1);
            combox_NetSel.SelectedIndex = 0;

            if (this.Tag != null)
            {
                CGateWayModule defaultValue = (CGateWayModule)this.Tag;
                combox_NetSel.Text = defaultValue.NetPort.ToString();

                SegBox.Text = defaultValue.m_NetSegmentString;
                //txt_seg4.Text = "0";
                MaskBox.Text = defaultValue.m_SubnetMaskString;
                GateWayBox.Text = defaultValue.m_GateWayString;
            }
            else
            {
                combox_NetSel.SelectedIndex = 0;

                MaskBox.Text = "255.255.255.0";
                //txt_masked1.Text = "255";
                //txt_masked2.Text = "255";
                //txt_masked3.Text = "255";
                //txt_masked4.Text = "0";

                //txt_seg4.Text = "0";
            }
        }

        private void btn_Enter_Click(object sender, EventArgs e)
        {
            if (CParaCtrl4G.IsIP(SegBox.Text) && CParaCtrl4G.IsIP(MaskBox.Text) && CParaCtrl4G.IsIP(GateWayBox.Text))
            {
                CGateWayModule m_GateWayGroup = new CGateWayModule();
                System.Net.IPAddress ipaddress, ipaddress1, ipaddress2;
                m_GateWayGroup.NetPort = Convert.ToInt32(combox_NetSel.Text);

                string[] tr = SegBox.Text.Split('.');
                tr[3] = "0";
                SegBox.Text = tr[0] + "." + tr[1] + "." + tr[2] + "." + tr[3] ;

                m_GateWayGroup.m_NetSegmentString = SegBox.Text;
                ipaddress = System.Net.IPAddress.Parse(m_GateWayGroup.m_NetSegmentString);
                m_GateWayGroup.m_NetSegment_byte = ipaddress.GetAddressBytes();

                //m_GateWayGroup.m_NetSegment_byte[0] = Convert.ToByte(txt_seg1.Text);
                //m_GateWayGroup.m_NetSegment_byte[1] = Convert.ToByte(txt_seg2.Text);
                //m_GateWayGroup.m_NetSegment_byte[2] = Convert.ToByte(txt_seg3.Text);
                //m_GateWayGroup.m_NetSegment_byte[3] = Convert.ToByte(txt_seg4.Text);

                m_GateWayGroup.m_SubnetMaskString = MaskBox.Text;
                ipaddress1 = System.Net.IPAddress.Parse(m_GateWayGroup.m_SubnetMaskString);
                m_GateWayGroup.m_SubnetMask_byte = ipaddress1.GetAddressBytes();
                //m_GateWayGroup.m_SubnetMask_byte[0] = Convert.ToByte(txt_masked1.Text);
                //m_GateWayGroup.m_SubnetMask_byte[1] = Convert.ToByte(txt_masked2.Text);
                //m_GateWayGroup.m_SubnetMask_byte[2] = Convert.ToByte(txt_masked3.Text);
                //m_GateWayGroup.m_SubnetMask_byte[3] = Convert.ToByte(txt_masked4.Text);

                m_GateWayGroup.m_GateWayString = GateWayBox.Text;
                ipaddress2 = System.Net.IPAddress.Parse(m_GateWayGroup.m_GateWayString);
                m_GateWayGroup.m_GateWay_byte = ipaddress2.GetAddressBytes();
                //m_GateWayGroup.m_GateWay_byte[0] = Convert.ToByte(txt_GateWay1.Text);
                //m_GateWayGroup.m_GateWay_byte[1] = Convert.ToByte(txt_GateWay2.Text);
                //m_GateWayGroup.m_GateWay_byte[2] = Convert.ToByte(txt_GateWay3.Text);
                //m_GateWayGroup.m_GateWay_byte[3] = Convert.ToByte(txt_GateWay4.Text);

                this.TempMyGateWay = m_GateWayGroup;
                
            }
            else
            {
                MessageBox.Show("地址错误!");          
            }            
        }
    }
}
