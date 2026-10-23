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
        private int[] m_ComBaund = new int[]{600,1200,2400,4800,9600,14400,19200,38400,56000,57600,115200,128000,230400,256000,460800,921600};
        public CMasterConForm()
        {
            InitializeComponent();
        }
        public void ReLoadForm()
        {            
            IniComBox();
            Ini_display(); 
        }
        private void CMasterConParaForm_Load(object sender, EventArgs e)
        {
            combo_KeyIndex.Items.Add("秘钥1");
            combo_KeyIndex.Items.Add("秘钥2");
            combo_KeyIndex.Items.Add("秘钥3");
            combo_KeyIndex.Items.Add("秘钥4");
            combo_KeyIndex.SelectedIndex = CEnCodePtr.m_KeyIndex - 1;
            EncodeIP.Text = CEnCodePtr.m_EncodeIP;
            EncodePort.Text = CEnCodePtr.m_EncodePort.ToString();
            ReLoadForm();
        }
        private void Ini_display()
        {
            YxType.Items.Clear();
            YxType.Items.Add("单点遥信");
            YxType.Items.Add("双点遥信");
            if (CGlobal.m_Device.m_Connection.m_SingleType >= YxType.Items.Count) CGlobal.m_Device.m_Connection.m_SingleType = 0;

            DevModList.Items.Clear();
            for (int i = 0; i < CGlobal.m_ModuleList.Count; i++)
            {
                DevModList.Items.Add(CGlobal.m_ModuleList[i].m_ModuleName);
            }
            DevModList.SelectedItem = CGlobal.m_Device.m_Connection.m_DevName;

            YxType.SelectedIndex =  CGlobal.m_Device.m_Connection.m_SingleType;
            if ( CGlobal.m_Device.m_Connection.m_nConnType == 0)
            {
                radioButton_Serial.Checked = true;
            }
            else
            {
                radioButton_Net.Checked = true;
            }

            if ( CGlobal.m_Device.m_Connection.m_n_ProType == 0)
            {
                radioButton_101.Checked = true;
            }
            else
            {
                radioButton_104.Checked = true;
            }

            Combox_ComNo.Text =  CGlobal.m_Device.m_Connection.m_strutComOption.m_nCom_No.ToString();
            Combox_ComBaund.Text =  CGlobal.m_Device.m_Connection.m_strutComOption.m_nCom_Baund.ToString();
            Combox_DataBit.Text =  CGlobal.m_Device.m_Connection.m_strutComOption.m_nCom_DataBit.ToString();
            Combox_StopBit.Text =  CGlobal.m_Device.m_Connection.m_strutComOption.m_nCom_StopBit.ToString();


            if ( CGlobal.m_Device.m_Connection.m_strutComOption.m_nCom_CheckBit == 0)
            {
                Combox_Parity.Text = "无校验";
            }
            else if ( CGlobal.m_Device.m_Connection.m_strutComOption.m_nCom_CheckBit == 1)
            {
                Combox_Parity.Text = "奇校验";
            }
            else if ( CGlobal.m_Device.m_Connection.m_strutComOption.m_nCom_CheckBit == 2)
            {
                Combox_Parity.Text = "偶校验";
            }
            ConnPort.Text =  CGlobal.m_Device.m_Connection.m_nIP_Port.ToString();
            string[] ips =  CGlobal.m_Device.m_Connection.m_strIP_Address.Split('.');
            IPByte1.Text = ips[0];
            IPByte2.Text = ips[1];
            IPByte3.Text = ips[2];
            IPByte4.Text = ips[3];


            AsduAddress_104.Text =  CGlobal.m_Device.m_Connection.m_104Address.m_AsduAddress.ToString();
            YxAddress_104.Text =  CGlobal.m_Device.m_Connection.m_104Address.m_YxStartAddress.ToString();
            YcAddress_104.Text =  CGlobal.m_Device.m_Connection.m_104Address.m_YcStartAddress.ToString();
            YkAddress_104.Text =  CGlobal.m_Device.m_Connection.m_104Address.m_YkSrartAddress.ToString();
            PowerAddress_104.Text =  CGlobal.m_Device.m_Connection.m_104Address.m_PowerStartAddress.ToString();

            AsduAddress_101.Text =  CGlobal.m_Device.m_Connection.m_101Address.m_AsduAddress.ToString();
            YxAddress_101.Text =  CGlobal.m_Device.m_Connection.m_101Address.m_YxStartAddress.ToString();
            YcAddress_101.Text =  CGlobal.m_Device.m_Connection.m_101Address.m_YcStartAddress.ToString();
            YkAddress_101.Text =  CGlobal.m_Device.m_Connection.m_101Address.m_YkSrartAddress.ToString();
            PowerAddress_101.Text =  CGlobal.m_Device.m_Connection.m_101Address.m_PowerStartAddress.ToString();

            comboBox_AddrLen.SelectedIndex = CGlobal.m_Device.m_Connection.m_101Address.m_AddressType;
            comboBox_SendLen.SelectedIndex = CGlobal.m_Device.m_Connection.m_101Address.m_CotType;   

            if (CGlobal.m_Device.m_Connection.IsEncode == true)
            {
                radioButton_Encode.Checked = true;
            }
            else
            {
                radioButton_NoEncode.Checked = true;
            }
            m_ConnType.SelectedIndex = CGlobal.m_Device.m_Connection.m_SocketMode;
        }
        private void IniComBox()
        {
            int i;
            Combox_ComNo.Items.Clear();
            for(i=1;i<=20;i++)
            {
                Combox_ComNo.Items.Add(i);
            }
            Combox_ComNo.SelectedIndex = 0;

            Combox_ComBaund.Items.Clear();
            foreach (int nBaund in m_ComBaund)
            {
                Combox_ComBaund.Items.Add(nBaund);
            }
            Combox_ComBaund.SelectedIndex = 4;

            Combox_DataBit.Items.Clear();
            Combox_DataBit.Items.Add(6);
            Combox_DataBit.Items.Add(7);
            Combox_DataBit.Items.Add(8);
            Combox_DataBit.SelectedIndex = 2;

            Combox_StopBit.Items.Clear();
            Combox_StopBit.Items.Add(1);
            Combox_StopBit.Items.Add(1.5);
            Combox_StopBit.Items.Add(2);
            Combox_StopBit.SelectedIndex = 0;

            Combox_Parity.Items.Clear();
            Combox_Parity.Items.Add("无校验");
            Combox_Parity.Items.Add("奇校验");
            Combox_Parity.Items.Add("偶校验");
            Combox_Parity.SelectedIndex = 0;

            m_ConnType.Items.Clear();
            m_ConnType.Items.Add("客户端模式");
            m_ConnType.Items.Add("服务器模式");


            SecrestType.Items.Add("V1.1版本");
            SecrestType.Items.Add("V1.2版本");


            comboBox_AddrLen.SelectedIndex = 1;
            comboBox_SendLen.SelectedIndex = 1;
            SecrestType.SelectedIndex = CEnCodePtr.m_SecretType;
        }

        private void SaveDataSet_Click(object sender, EventArgs e)
        {
            if (ISAutoCon.Checked == true)
            {
                CGlobal.IsAutoCon = 1;
            }
            else
            {
                CGlobal.IsAutoCon =0;
            }
            if (IsAutoCall.Checked == true)
            {
                CGlobal.IsAutoCall = 1;
            }
            else
            {
                CGlobal.IsAutoCall = 0;
            }          
            bool bDeviceSelectChanged = false;            
            if (radioButton_Serial.Checked)
            {
                CGlobal.m_Device.m_Connection.m_nConnType = 0;
            }
            else
            {
                CGlobal.m_Device.m_Connection.m_nConnType = 1;
            }
            if (radioButton_101.Checked)
            {
                CGlobal.m_Device.m_Connection.m_n_ProType = 0;  
            }
            else
            {
                CGlobal.m_Device.m_Connection.m_n_ProType = 1;             
            }
            CGlobal.m_Device.m_Connection.m_SingleType = YxType.SelectedIndex;
            if (CGlobal.m_Device.m_Connection.m_DevName != DevModList.SelectedItem.ToString()) bDeviceSelectChanged = true;
            CGlobal.m_Device.m_Connection.m_DevName = DevModList.SelectedItem.ToString();


            CGlobal.m_Device.m_Connection.m_strIP_Address = IPByte1.Text + "." + IPByte2.Text + "." + IPByte3.Text + "." + IPByte4.Text;
            CGlobal.m_Device.m_Connection.m_nIP_Port = Convert.ToInt32(ConnPort.Text);
            CGlobal.m_Device.m_Connection.m_SocketMode = m_ConnType.SelectedIndex;

            CGlobal.m_Device.m_Connection.m_strutComOption.m_nCom_No = Convert.ToInt32(Combox_ComNo.Text);
            CGlobal.m_Device.m_Connection.m_strutComOption.m_nCom_Baund = Convert.ToInt32(Combox_ComBaund.Text);
            CGlobal.m_Device.m_Connection.m_strutComOption.m_nCom_DataBit = Convert.ToInt32(Combox_DataBit.Text);
            CGlobal.m_Device.m_Connection.m_strutComOption.m_nCom_StopBit = Convert.ToInt32(Combox_StopBit.Text);
            CGlobal.m_Device.m_Connection.m_strutComOption.m_nCom_CheckBit = Convert.ToInt32(Combox_Parity.SelectedIndex);

            CGlobal.m_Device.m_Connection.m_104Address.m_AsduAddress = Convert.ToInt32(AsduAddress_104.Text);
            CGlobal.m_Device.m_Connection.m_104Address.m_YxStartAddress = Convert.ToUInt16(YxAddress_104.Text);
            CGlobal.m_Device.m_Connection.m_104Address.m_YcStartAddress = Convert.ToUInt16(YcAddress_104.Text);
            CGlobal.m_Device.m_Connection.m_104Address.m_YkSrartAddress = Convert.ToUInt16(YkAddress_104.Text);
            CGlobal.m_Device.m_Connection.m_104Address.m_PowerStartAddress = Convert.ToUInt16(PowerAddress_104.Text);

            CGlobal.m_Device.m_Connection.m_101Address.m_AsduAddress = Convert.ToInt32(AsduAddress_101.Text);
            CGlobal.m_Device.m_Connection.m_101Address.m_YxStartAddress = Convert.ToUInt16(YxAddress_101.Text);
            CGlobal.m_Device.m_Connection.m_101Address.m_YcStartAddress = Convert.ToUInt16(YcAddress_101.Text);
            CGlobal.m_Device.m_Connection.m_101Address.m_YkSrartAddress = Convert.ToUInt16(YkAddress_101.Text);
            CGlobal.m_Device.m_Connection.m_101Address.m_PowerStartAddress = Convert.ToUInt16(PowerAddress_101.Text);
            CGlobal.m_Device.m_Connection.m_101Address.m_AddressType = comboBox_AddrLen.SelectedIndex;
            CGlobal.m_Device.m_Connection.m_101Address.m_CotType = comboBox_SendLen.SelectedIndex;

            CEnCodePtr.m_SecretType = SecrestType.SelectedIndex;
            if (CEnCodePtr.m_SecretType == 0)
            {
                CEnCodePtr.m_Cerfilepath = "证书套件\\";
            }
            else
            {
                CEnCodePtr.m_Cerfilepath = "证书\\";
            }
            
            CGlobal.m_Device.InitConnect();
            if (radioButton_Encode.Checked)
            {
                CGlobal.m_Device.m_Connection.IsEncode = true;
            }
            else
            {
                CGlobal.m_Device.m_Connection.IsEncode = false;
            }
            CEnCodePtr.m_KeyIndex = Convert.ToByte(combo_KeyIndex.SelectedIndex + 1);

            CEnCodePtr.m_EncodeIP = EncodeIP.Text;
            CEnCodePtr.m_EncodePort = Convert.ToInt32(EncodePort.Text);

            CGlobal.WriteComPara();
            CGlobal.m_SecretEncode.CreateSecretDevice();
            if (bDeviceSelectChanged)
            {
                if (MessageBox.Show("选择新的装置模板，需要手动重启生效，是否重启？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    Close();              
                    System.Windows.Forms.Application.Restart();
                    return;
                }
            }
            this.Close();
        }   
        private void CMasterConForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }       
    }
}
