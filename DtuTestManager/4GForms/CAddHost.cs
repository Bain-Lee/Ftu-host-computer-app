using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DtuTestManager
{
    public partial class CAddHost : Form
    {


        private CHostModule _tempMyHost = null;

        public CHostModule TempMyHost
        {
            get
            {
                return _tempMyHost;
            }
            set
            {
                _tempMyHost = value;
            }
        }

        public CAddHost()
        {
            InitializeComponent();
        }

        public CAddHost(string form_name)
        {
            InitializeComponent();
            this.Text = form_name;
        }

        private void CAddHost_Load(object sender, EventArgs e)
        {
            this.MinimizeBox = false;
            this.MaximizeBox = false;

            cbx_HostIndex.Items.Clear();
            for(int i=0;i<6;i++)
             cbx_HostIndex.Items.Add(i);
            cbx_HostIndex.SelectedIndex = 0;

            cbx_HostType.Items.Clear();
            cbx_HostType.Items.Add("串口");
            cbx_HostType.Items.Add("网口");
            cbx_HostType.SelectedIndex = 0;

            if (cbx_HostType.Text == "串口")
            {
                NetPanel.Visible = false;
                //btn_addIP.Location = new Point(NetPanel.Location.X, NetPanel.Location.Y);
                //btn_Cancel.Location = new Point(NetPanel.Location.X+100, NetPanel.Location.Y);
                
            }
               
            else
                NetPanel.Visible = true;

            if (this.Tag != null)
            {
                CHostModule defaultValue = (CHostModule)this.Tag;
                cbx_HostIndex.Text = defaultValue.m_HostIndex.ToString();
                txt_Host_Addr.Text = defaultValue.m_address.ToString();
                cbx_HostType.SelectedIndex = defaultValue.m_ChanType;
                txt_Local_Port.Text = defaultValue.LocalPort.ToString();
                txt_Remote_Port.Text = defaultValue.RemotePort.ToString();
                
                if (cbx_HostType.Text == "串口")
                    NetPanel.Visible = false;
                else
                    NetPanel.Visible = true;

                txt1_Local_IP1.Text = defaultValue.m_LocalIP_byte1[0].ToString();
                txt2_Local_IP1.Text = defaultValue.m_LocalIP_byte1[1].ToString();
                txt3_Local_IP1.Text = defaultValue.m_LocalIP_byte1[2].ToString();
                txt4_Local_IP1.Text = defaultValue.m_LocalIP_byte1[3].ToString();

                txt1_Local_IP2.Text = defaultValue.m_LocalIP_byte2[0].ToString();
                txt2_Local_IP2.Text = defaultValue.m_LocalIP_byte2[1].ToString();
                txt3_Local_IP2.Text = defaultValue.m_LocalIP_byte2[2].ToString();
                txt4_Local_IP2.Text = defaultValue.m_LocalIP_byte2[3].ToString();

                txt1_Local_IP3.Text = defaultValue.m_LocalIP_byte3[0].ToString(); 
                txt2_Local_IP3.Text = defaultValue.m_LocalIP_byte3[1].ToString(); 
                txt3_Local_IP3.Text = defaultValue.m_LocalIP_byte3[2].ToString(); 
                txt4_Local_IP3.Text = defaultValue.m_LocalIP_byte3[3].ToString();

                txt1_Remote_IP1.Text = defaultValue.m_RemoteIP_byte1[0].ToString();
                txt2_Remote_IP1.Text = defaultValue.m_RemoteIP_byte1[1].ToString();
                txt3_Remote_IP1.Text = defaultValue.m_RemoteIP_byte1[2].ToString();
                txt4_Remote_IP1.Text = defaultValue.m_RemoteIP_byte1[3].ToString();

                txt1_Remote_IP2.Text = defaultValue.m_RemoteIP_byte2[0].ToString();
                txt2_Remote_IP2.Text = defaultValue.m_RemoteIP_byte2[1].ToString();
                txt3_Remote_IP2.Text = defaultValue.m_RemoteIP_byte2[2].ToString();
                txt4_Remote_IP2.Text = defaultValue.m_RemoteIP_byte2[3].ToString();

                txt1_Remote_IP3.Text = defaultValue.m_RemoteIP_byte3[0].ToString();
                txt2_Remote_IP3.Text = defaultValue.m_RemoteIP_byte2[1].ToString();
                txt3_Remote_IP3.Text = defaultValue.m_RemoteIP_byte2[2].ToString();
                txt4_Remote_IP3.Text = defaultValue.m_RemoteIP_byte2[3].ToString();
              
            }
            else
            {
                txt1_Local_IP1.Text = "0";
                txt2_Local_IP1.Text = "0";
                txt3_Local_IP1.Text = "0";
                txt4_Local_IP1.Text = "0";

                txt1_Local_IP2.Text = "0";
                txt2_Local_IP2.Text = "0";
                txt3_Local_IP2.Text = "0";
                txt4_Local_IP2.Text = "0";

                txt1_Local_IP3.Text = "0";
                txt2_Local_IP3.Text = "0";
                txt3_Local_IP3.Text = "0";
                txt4_Local_IP3.Text = "0";

                txt1_Remote_IP1.Text = "0";
                txt2_Remote_IP1.Text = "0";
                txt3_Remote_IP1.Text = "0";
                txt4_Remote_IP1.Text = "0";

                txt1_Remote_IP2.Text = "0";
                txt2_Remote_IP2.Text = "0";
                txt3_Remote_IP2.Text = "0";
                txt4_Remote_IP2.Text = "0";

                txt1_Remote_IP3.Text = "0";
                txt2_Remote_IP3.Text = "0";
                txt3_Remote_IP3.Text = "0";
                txt4_Remote_IP3.Text = "0";
            }
        }

        private void cbx_HostType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbx_HostType.Text == "串口")
                NetPanel.Visible = false;
            else
                NetPanel.Visible = true;
        }

        
        private void btn_addIP_Click(object sender, EventArgs e)
        {
            //if (isIPAddress())
            {
                CHostModule m_HostGroup = new CHostModule();
            
                m_HostGroup.m_HostIndex = Convert.ToByte(cbx_HostIndex.Text);
                m_HostGroup.m_address = Convert.ToByte(txt_Host_Addr.Text); //IPByte1.Text + "." + IPByte2.Text + "." + IPByte3.Text + "." + IPByte4.Text;
                m_HostGroup.m_ChanType = Convert.ToByte(cbx_HostType.SelectedIndex);
                m_HostGroup.LocalPort = Convert.ToInt32(txt_Local_Port.Text);
                
                if (cbx_HostType.Text == "网口")
                {
                    m_HostGroup.RemotePort = Convert.ToInt32(txt_Remote_Port.Text);
                    m_HostGroup.m_LocalIP_byte1[0] = Convert.ToByte(txt1_Local_IP1.Text);
                    m_HostGroup.m_LocalIP_byte1[1] = Convert.ToByte(txt2_Local_IP1.Text);
                    m_HostGroup.m_LocalIP_byte1[2] = Convert.ToByte(txt3_Local_IP1.Text);
                    m_HostGroup.m_LocalIP_byte1[3] = Convert.ToByte(txt4_Local_IP1.Text);
                    m_HostGroup.m_LocalIPString1 = txt1_Local_IP1.Text + "." + txt1_Local_IP1.Text + "." + txt3_Local_IP1.Text + "." + txt4_Local_IP1.Text;

                    m_HostGroup.m_LocalIP_byte2[0] = Convert.ToByte(txt1_Local_IP2.Text);
                    m_HostGroup.m_LocalIP_byte2[1] = Convert.ToByte(txt2_Local_IP2.Text);
                    m_HostGroup.m_LocalIP_byte2[2] = Convert.ToByte(txt3_Local_IP2.Text);
                    m_HostGroup.m_LocalIP_byte2[3] = Convert.ToByte(txt4_Local_IP2.Text);
                    m_HostGroup.m_LocalIPString2 = txt1_Local_IP2.Text + "." + txt2_Local_IP2.Text + "." + txt3_Local_IP2.Text + "." + txt4_Local_IP2.Text;

                    m_HostGroup.m_LocalIP_byte3[0] = Convert.ToByte(txt1_Local_IP3.Text);
                    m_HostGroup.m_LocalIP_byte3[1] = Convert.ToByte(txt2_Local_IP3.Text);
                    m_HostGroup.m_LocalIP_byte3[2] = Convert.ToByte(txt3_Local_IP3.Text);
                    m_HostGroup.m_LocalIP_byte3[3] = Convert.ToByte(txt4_Local_IP3.Text);
                    m_HostGroup.m_LocalIPString3 = txt1_Local_IP3.Text + "." + txt2_Local_IP3.Text + "." + txt3_Local_IP3.Text + "." + txt4_Local_IP3.Text;



                    m_HostGroup.m_RemoteIP_byte1[0] = Convert.ToByte(txt1_Remote_IP1.Text);
                    m_HostGroup.m_RemoteIP_byte1[1] = Convert.ToByte(txt2_Remote_IP1.Text);
                    m_HostGroup.m_RemoteIP_byte1[2] = Convert.ToByte(txt3_Remote_IP1.Text);
                    m_HostGroup.m_RemoteIP_byte1[3] = Convert.ToByte(txt4_Remote_IP1.Text);
                    m_HostGroup.m_RemoteIPString1 = txt1_Remote_IP1.Text + "." + txt1_Remote_IP1.Text + "." + txt3_Remote_IP1.Text + "." + txt4_Remote_IP1.Text;

                    m_HostGroup.m_RemoteIP_byte2[0] = Convert.ToByte(txt1_Remote_IP2.Text);
                    m_HostGroup.m_RemoteIP_byte2[1] = Convert.ToByte(txt2_Remote_IP2.Text);
                    m_HostGroup.m_RemoteIP_byte2[2] = Convert.ToByte(txt3_Remote_IP2.Text);
                    m_HostGroup.m_RemoteIP_byte2[3] = Convert.ToByte(txt4_Remote_IP2.Text);
                    m_HostGroup.m_RemoteIPString2 = txt1_Remote_IP2.Text + "." + txt2_Remote_IP2.Text + "." + txt3_Remote_IP2.Text + "." + txt4_Remote_IP2.Text;

                    m_HostGroup.m_RemoteIP_byte3[0] = Convert.ToByte(txt1_Remote_IP3.Text);
                    m_HostGroup.m_RemoteIP_byte3[1] = Convert.ToByte(txt2_Remote_IP3.Text);
                    m_HostGroup.m_RemoteIP_byte3[2] = Convert.ToByte(txt3_Remote_IP3.Text);
                    m_HostGroup.m_RemoteIP_byte3[3] = Convert.ToByte(txt4_Remote_IP3.Text);
                    m_HostGroup.m_RemoteIPString3 = txt1_Remote_IP3.Text + "." + txt2_Remote_IP3.Text + "." + txt3_Remote_IP3.Text + "." + txt4_Remote_IP3.Text;
                }

                this._tempMyHost = m_HostGroup;
                
            }
           // else
           // {
          //      MessageBox.Show("地址错误!");
          //      //this._tempMyIP = null;              //不确定是否需要添加
          //  }          
        }
    }
}
