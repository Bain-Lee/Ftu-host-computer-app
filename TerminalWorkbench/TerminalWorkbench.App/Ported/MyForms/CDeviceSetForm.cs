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
    public partial class CDeviceSetForm : Form
    {
        public CDeviceMudule pDevice = null;
        public CDeviceSetForm()
        {
            InitializeComponent();
        }

        private void BtnChange_Click(object sender, EventArgs e)
        {
            if (pDevice != null)
            {
                DevName.Text = DevName.Text.Trim();
                if (DevName.Text.Length == 0)
                    return;
                pDevice.m_DeviceMsg.m_ModuleName = DevName.Text;
                pDevice.m_DeviceMsg.m_LineCount = Convert.ToInt32(LineCount.Text);
                if (pDevice.m_Node != null)
                {
                    pDevice.m_Node.Text = pDevice.m_DeviceMsg.m_ModuleName;
                }
                pDevice.m_DeviceMsg.UpdateMsg();
                if (pDevice.CheckNameExist(DevName.Text))
                    return;
                pDevice.ReNameDeviceName(DevName.Text);
            }
        }

        private void CDeviceSetForm_Load(object sender, EventArgs e)
        {
            if (pDevice != null)
            {
                DevName.Text = pDevice.m_DeviceMsg.m_ModuleName;
                DevNo.Text = pDevice.m_DeviceMsg.m_ModuleNo.ToString();
                DevType.Text = pDevice.m_DeviceMsg.m_ModuleType.ToString();
                YcCount.Text = pDevice.m_GroupList[0].m_DataModuleList.Count.ToString();
                YxCount.Text = pDevice.m_GroupList[1].m_DataModuleList.Count.ToString();
                YkCount.Text = pDevice.m_GroupList[2].m_DataModuleList.Count.ToString();
                PowerCount.Text = pDevice.m_GroupList[3].m_DataModuleList.Count.ToString();
                ParaCount.Text = "0";
                LineCount.Text = pDevice.m_DeviceMsg.m_LineCount.ToString();

                if (pDevice.m_bDefaultFlag)
                {
                    DevName.Enabled = false;
                    LineCount.Enabled = false;
                    BtnChange.Enabled = false;
                    BtnReloadDefault.Enabled = false;
                }
            }
        }

        private void BtnReloadDefault_Click(object sender, EventArgs e)
        {
            if (pDevice != null)
            {
                if (MessageBox.Show("此装置模板将恢复为默认配置，原配置将无法恢复，是否恢复？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    pDevice.ReloadDefault();
                    LineCount.Text = pDevice.m_DeviceMsg.m_LineCount.ToString();
                    pDevice.m_DeviceMsg.UpdateMsg();
                }
            }
        }
    }
}
