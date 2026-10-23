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
    public partial class CYKForm : Form
    {
        public static CYkOption m_YkMsg = new CYkOption();
        public static bool YKTest = false;
        public static bool YKStatus = false;
        public CYKForm()
        {
            InitializeComponent();

        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case UseMessage.MY_MSG_7:
                    LV_YK.Items[m_YkMsg.m_YkNo - 1].SubItems[4].Text = "操作成功";
                    if (m_YkMsg.m_YkStatus == 0)
                    {
                        if (m_YkMsg.m_YkValue == 0)
                        {
                            YK_F_Sel_btn.Enabled = false;
                            YK_F_Ex_btn.Enabled = true;
                            YK_F_C_btn.Enabled = true;
                            YK_H_Sel_btn.Enabled = false;
                            YK_H_Ex_btn.Enabled = false;
                            YK_H_C_btn.Enabled = false;

                        }
                        else
                        {
                            YK_H_Sel_btn.Enabled = false;
                            YK_F_Sel_btn.Enabled = false;
                            YK_F_Ex_btn.Enabled = false;
                            YK_F_C_btn.Enabled = false;
                            YK_H_Ex_btn.Enabled = true;
                            YK_H_C_btn.Enabled = true;
                        }
                    }
                    break;
                case UseMessage.MY_MSG_8:
                    LV_YK.Items[m_YkMsg.m_YkNo - 1].SubItems[4].Text = "操作失败";
                    if (m_YkMsg.m_YkStatus == 0)
                    {
                        YK_F_Sel_btn.Enabled = true;
                        YK_H_Sel_btn.Enabled = true;
                        YK_F_Ex_btn.Enabled = false;
                        YK_F_C_btn.Enabled = false;
                        YK_H_Ex_btn.Enabled = false;
                        YK_H_C_btn.Enabled = false;

                    }


                    break;
                case UseMessage.MY_MSG_9:
                    YKCmdTest();
                    break;
            }
        }
        public void ReLoadForm()
        {
            CGlobal.m_Device.m_Ptr.pYkMsg = m_YkMsg;
            m_YkMsg.m_YkNo = 1;
            m_YkMsg.m_YkType = 0;
            radioButton1.Checked = true;
            Use_Normal.Checked = true;
            CEnCodePtr.NormalTest();
            Init_LV_YK();
        }
        private void CYKForm_Load(object sender, EventArgs e)
        {
            ReLoadForm();
        }
        private void Init_LV_YK()
        {
            try
            {
                string[] h = new string[5];
                LV_YK.Items.Clear();
                LV_YK.Columns.Clear();
                LV_YK.Columns.Add("遥控号", 60, HorizontalAlignment.Center);
                LV_YK.Columns.Add("遥控名称", 160, HorizontalAlignment.Center);
                LV_YK.Columns.Add("操作时间", 160, HorizontalAlignment.Center);
                LV_YK.Columns.Add("操作状态", 160, HorizontalAlignment.Center);
                LV_YK.Columns.Add("操作结果", 80, HorizontalAlignment.Center);
                LV_YK.GridLines = true;
                LV_YK.FullRowSelect = true;
                LV_YK.View = View.Details;
                LV_YK.Scrollable = true;
                LV_YK.MultiSelect = false;
                LV_YK.HeaderStyle = ColumnHeaderStyle.Clickable;
                LV_YK.Visible = true;
                for (int i = 0; i < CGlobal.m_Device.m_YkList.Count; i++)
                {
                    h[0] = Convert.ToString(CGlobal.m_Device.m_YkList[i].m_YkNo);
                    h[1] = CGlobal.m_Device.m_YkList[i].m_YkName;
                    ListViewItem item1 = new ListViewItem(h);
                    this.LV_YK.Items.Add(item1);
                }
            }
            catch { }
        }
        private void YK_F_btn_Click(object sender, EventArgs e)
        {
            if (m_YkMsg == null)
                m_YkMsg = new CYkOption();
            m_YkMsg.m_YkStatus = 1;
            m_YkMsg.m_YkValue = 0;
            FreshYkMsg();
            YK_F_Ex_btn.Enabled = false;
            YK_F_C_btn.Enabled = false;
            YK_F_Sel_btn.Enabled = true;
            YK_H_Sel_btn.Enabled = true;
            YK_H_Ex_btn.Enabled = false;
            YK_H_C_btn.Enabled = false;
            CGlobal.SendTaskOrder(6);
        }
        private void YK_S_F_btn_Click(object sender, EventArgs e)
        {
            if (m_YkMsg == null)
                m_YkMsg = new CYkOption();
            m_YkMsg.m_YkStatus = 0;
            m_YkMsg.m_YkValue = 0;
            FreshYkMsg();
            CGlobal.SendTaskOrder(6);
        }
        private void YK_C_F_btn_Click(object sender, EventArgs e)
        {
            if (m_YkMsg == null)
                m_YkMsg = new CYkOption();
            m_YkMsg.m_YkStatus = 2;
            m_YkMsg.m_YkValue = 0;
            FreshYkMsg();
            YK_F_Ex_btn.Enabled = false;
            YK_F_C_btn.Enabled = false;
            YK_F_Sel_btn.Enabled = true;
            YK_H_Sel_btn.Enabled = true;
            YK_H_Ex_btn.Enabled = false;
            YK_H_C_btn.Enabled = false;
            CGlobal.SendTaskOrder(6);
        }
        private void YK_H_C_btn_Click(object sender, EventArgs e)
        {
            if (m_YkMsg == null)
                m_YkMsg = new CYkOption();
            m_YkMsg.m_YkStatus = 2;
            m_YkMsg.m_YkValue = 1;
            FreshYkMsg();
            YK_F_Ex_btn.Enabled = false;
            YK_F_C_btn.Enabled = false;
            YK_F_Sel_btn.Enabled = true;
            YK_H_Sel_btn.Enabled = true;
            YK_H_Ex_btn.Enabled = false;
            YK_H_C_btn.Enabled = false;
            CGlobal.SendTaskOrder(6);
        }
        private void YK_S_H_btn_Click(object sender, EventArgs e)
        {
            if (m_YkMsg == null)
                m_YkMsg = new CYkOption();
            m_YkMsg.m_YkStatus = 0;
            m_YkMsg.m_YkValue = 1;
            FreshYkMsg();
            CGlobal.SendTaskOrder(6);
        }
        private void YK_H_btn_Click(object sender, EventArgs e)
        {
            if (m_YkMsg == null)
                m_YkMsg = new CYkOption();
            m_YkMsg.m_YkStatus = 1;
            m_YkMsg.m_YkValue = 1;
            FreshYkMsg();
            YK_F_Ex_btn.Enabled = false;
            YK_F_C_btn.Enabled = false;
            YK_F_Sel_btn.Enabled = true;
            YK_H_Sel_btn.Enabled = true;
            YK_H_Ex_btn.Enabled = false;
            YK_H_C_btn.Enabled = false;
            CGlobal.SendTaskOrder(6);
        }
        private void FreshYkMsg()
        {
            CGlobal.m_Device.m_Ptr.pYkMsg = m_YkMsg;
            if (m_YkMsg.m_YkNo > 0 && m_YkMsg.m_YkNo <= LV_YK.Items.Count)
            {
                CGlobal.m_Device.m_YkFormHandle = (int)this.Handle;
                LV_YK.Items[m_YkMsg.m_YkNo - 1].SubItems[2].Text = DateTime.Now.ToString();
                string strOperation;
                string strType;
                string strStatus;
                string strDesc;
                if (m_YkMsg.m_YkStatus == 0)
                    strOperation = "遥控选择";
                else if (m_YkMsg.m_YkStatus == 1)
                    strOperation = "遥控执行";
                else
                    strOperation = "遥控撤销";

                if (m_YkMsg.m_YkType == 0)
                {
                    strType = "(单点)";
                    if (m_YkMsg.m_YkValue == 1)
                        strStatus = "(合闸)";
                    else
                        strStatus = "(分闸)";
                }
                else
                {
                    strType = "(双点)";
                    if (m_YkMsg.m_YkValue == 2)
                        strStatus = "(合闸)";
                    else
                        strStatus = "(分闸)";
                }
                strDesc = strOperation + strStatus + strType;
                LV_YK.Items[m_YkMsg.m_YkNo - 1].SubItems[3].Text = strDesc;
                LV_YK.Items[m_YkMsg.m_YkNo - 1].SubItems[4].Text = "";
            }
        }
        private void LV_YK_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LV_YK.SelectedIndices != null && LV_YK.SelectedIndices.Count > 0)
            {
                try
                {
                    //    LV_YK.Items[m_YkMsg.m_YkNo - 1].BackColor = Color.Red;
                    m_YkMsg.m_YkNo = LV_YK.SelectedItems[0].Index + 1;
                    m_YkMsg.m_YkAddress = CGlobal.m_Device.m_YkList[m_YkMsg.m_YkNo - 1].m_YkAddress;

                    //        LV_YK.Items[m_YkMsg.m_YkNo - 1].BackColor = Color.FromArgb(51, 153, 255);
                }
                catch { }
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                m_YkMsg.m_YkType = 0;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                m_YkMsg.m_YkType = 1;
        }

        private void Flag_Status()
        {
            CEnCodePtr.m_TimeErrFlag = UseErrTime.Checked;
            CEnCodePtr.m_SignErrFlag = UseErrSign.Checked;
            CEnCodePtr.m_RandErrFlag = UseErrRand.Checked;
            CEnCodePtr.m_UseNoSignFlag = UseNoSign.Checked;
            CEnCodePtr.m_UseNoTimeFlag = UseNoTime.Checked;
            CEnCodePtr.m_UseNoRandFlag = UseNoRand.Checked;
            CEnCodePtr.m_UseMingWenFlag = UseMingWen.Checked;
        }
        private void UseErrTime_CheckedChanged(object sender, EventArgs e)
        {
            Flag_Status();
        }


        private void UseErrSign_CheckedChanged(object sender, EventArgs e)
        {
            Flag_Status();
        }

        private void UseErrRand_CheckedChanged(object sender, EventArgs e)
        {
            Flag_Status();
        }

        private void UseNoSign_CheckedChanged(object sender, EventArgs e)
        {
            Flag_Status();
        }

        private void UseNoTime_CheckedChanged(object sender, EventArgs e)
        {
            Flag_Status();
        }


        private void UseNoRand_CheckedChanged(object sender, EventArgs e)
        {
            Flag_Status();
        }

        private void UseMingWen_CheckedChanged(object sender, EventArgs e)
        {
            Flag_Status();
        }

        private void Use_Normal_CheckedChanged(object sender, EventArgs e)
        {
            if (Use_Normal.Checked == true)
            {
                CEnCodePtr.NormalTest();
            }
        }





        public void YKOPenTest()
        {
            YKTest = true;
            YKStatus = true;
            if (m_YkMsg == null)
                m_YkMsg = new CYkOption();
            m_YkMsg.m_YkNo = 1;
            m_YkMsg.m_YkAddress = CGlobal.m_Device.m_YkList[m_YkMsg.m_YkNo - 1].m_YkAddress;
            m_YkMsg.m_YkStatus = 0;
            m_YkMsg.m_YkValue = 0;
            FreshYkMsg();
            CGlobal.SendTaskOrder(6);
        }
        public void YKCmdTest()
        {
            YKStatus = false;
            if (m_YkMsg == null)
                m_YkMsg = new CYkOption();
            m_YkMsg.m_YkNo = 1;
            m_YkMsg.m_YkAddress = CGlobal.m_Device.m_YkList[m_YkMsg.m_YkNo - 1].m_YkAddress;
            m_YkMsg.m_YkStatus = 1;
            m_YkMsg.m_YkValue = 0;
            FreshYkMsg();
            CGlobal.SendTaskOrder(6);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            YKOPenTest();
        }

        private void UseMingWen_CheckedChanged_1(object sender, EventArgs e)
        {

        }











    }
}
