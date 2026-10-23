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
    public partial class CDataCheckForm : Form
    {
        public CDataCheckForm()
        {
            InitializeComponent();
        }

        float SelVol;
        int SelCur;
        float SelAng;
        bool flag_Ac = true;
        private void CDataCheckForm_Load(object sender, EventArgs e)
        {
            InitPhasedataValue();
            InitCheckdataValue();
            InitDC_CheckdataValue();
            Init_LV_DC_Check();
            Init_LV_Phase();
            Init_LV_AC_Check();
            InitCombox();
            Btn_Check_Click(sender, e);
            Freshtimer.Start();
        }
        private void InitCombox()
        {
            com_Vol.Items.Add("100V");
            com_Vol.Items.Add("220V");
            com_Vol.Items.Add("380V");
            com_Vol.Items.Add("57.7V");
            com_Vol.Items.Add("2V");

            com_Current.Items.Add("1A");
            com_Current.Items.Add("5A");
            com_Angle.Items.Add("0度");
            com_Angle.Items.Add("30度");
            com_Angle.Items.Add("45度");
            com_Angle.Items.Add("60度");
            com_Angle.Items.Add("90度");

            com_LineNo.SelectedIndex = 0;
            com_Vol.SelectedIndex = 0;
            com_Current.SelectedIndex = 1;
            com_Angle.SelectedIndex = 0;
        }
        private void InitPhasedataValue()
        {
            com_LineNo.Items.Clear();
            for (int i = 0; i < 16; i++)
            {
                com_LineNo.Items.Add(i.ToString() + "#线路");
                List<CPhasedata> T_checkdata = new List<CPhasedata>();
                for (int j = 0; j < 6; j++)
                {
                    CPhasedata temp = new CPhasedata();
                    temp.m_LineNo = j;
                    switch (j)
                    {
                        case 0:
                            temp.m_Name = "UA相角";
                            break;
                        case 1:
                            temp.m_Name = "UB相角";
                            break;
                        case 2:
                            temp.m_Name = "UC相角";
                            break;
                        case 3:
                            temp.m_Name = "IA相角";
                            break;
                        case 4:
                            temp.m_Name = "IB相角";
                            break;
                        case 5:
                            temp.m_Name = "IC相角";
                            break;
                    }
                    T_checkdata.Add(temp);
                }
                CGlobal.m_Device.m_Phasedata.Add(T_checkdata);
            }
        }
        private void InitCheckdataValue()
        {
            com_LineNo.Items.Clear();

            for (int i = 0; i < 16; i++)
            {
                com_LineNo.Items.Add(i.ToString() + "#线路");
                List<CCheckdata> T_checkdata = new List<CCheckdata>();
                for (int j = 0; j < 18; j++)
                {
                    CCheckdata temp = new CCheckdata();
                    temp.m_LineNo = j;
                    MainRealYc pRealIndex = null;
                    temp.m_Index = 0;
                    if ((i + 1) < CGlobal.m_Device.m_MainRealIndex.Count)
                    {
                        pRealIndex = CGlobal.m_Device.m_MainRealIndex[i + 1];

                    }
                    switch (j)
                    {
                        case 0:
                            temp.m_Name = "U1";
                            if (pRealIndex != null)
                                temp.m_Index = pRealIndex.m_UaIndex;
                            break;
                        case 1:
                            temp.m_Name = "U2";
                            if (pRealIndex != null)
                                temp.m_Index = pRealIndex.m_UbIndex;
                            break;
                        case 2:
                            temp.m_Name = "U3";
                            if (pRealIndex != null)
                                temp.m_Index = pRealIndex.m_UcIndex;
                            break;
                        case 3:
                            temp.m_Name = "U0";
                            if (pRealIndex != null)
                                temp.m_Index = pRealIndex.m_UoIndex;
                            break;
                        case 4:
                            temp.m_Name = "CI1";
                            if (pRealIndex != null)
                                temp.m_Index = pRealIndex.m_CIaIndex;
                            break;
                        case 5:
                            temp.m_Name = "CI2";
                            if (pRealIndex != null)
                                temp.m_Index = pRealIndex.m_CIbIndex;
                            break;
                        case 6:
                            temp.m_Name = "CI3";
                            if (pRealIndex != null)
                                temp.m_Index = pRealIndex.m_CIcIndex;
                            break;
                        case 7:
                            temp.m_Name = "BI1";
                            if (pRealIndex != null)
                                temp.m_Index = pRealIndex.m_BIaIndex;
                            break;
                        case 8:
                            temp.m_Name = "BI2";
                            if (pRealIndex != null)
                                temp.m_Index = pRealIndex.m_BIbIndex;
                            break;
                        case 9:
                            temp.m_Name = "BI3";
                            if (pRealIndex != null)
                                temp.m_Index = pRealIndex.m_BIcIndex;
                            break;
                        case 10:
                            temp.m_Name = "I0";
                            if (pRealIndex != null)
                                temp.m_Index = pRealIndex.m_IoIndex;
                            break;
                        case 11:
                            temp.m_Name = "ΦA";
                            if (pRealIndex != null)
                                temp.m_Index = pRealIndex.m_PhAIndex;
                            break;
                        case 12:
                            temp.m_Name = "ΦB";
                            if (pRealIndex != null)
                                temp.m_Index = pRealIndex.m_PhBIndex;
                            break;
                        case 13:
                            temp.m_Name = "ΦC";
                            if (pRealIndex != null)
                                temp.m_Index = pRealIndex.m_PhCIndex;
                            break;
                        case 14:
                            temp.m_Name = "Freaq";
                            if (pRealIndex != null)
                                temp.m_Index = pRealIndex.m_FIndex;
                            break;
                        case 15:
                            temp.m_Name = "Cos";
                            if (pRealIndex != null)
                                temp.m_Index = pRealIndex.m_CosIndex;
                            break;
                        case 16:
                            temp.m_Name = "P";
                            if (pRealIndex != null)
                                temp.m_Index = pRealIndex.m_PIndex;
                            break;
                        case 17:
                            temp.m_Name = "Q";
                            if (pRealIndex != null)
                                temp.m_Index = pRealIndex.m_QIndex;
                            break;
                    }
                    T_checkdata.Add(temp);
                }
                CGlobal.m_Device.m_AC_Checkdata.Add(T_checkdata);
            }
        }
        private void InitDC_CheckdataValue()
        {
            for (int j = 0; j < 4; j++)
            {
                CDCCheckdata temp = new CDCCheckdata();
                switch (j)
                {
                    case 0:
                        temp.m_Name = "DC1";
                        temp.m_Index = CGlobal.m_Device.m_Connection.m_DC1Index;
                        break;
                    case 1:
                        temp.m_Name = "DC2";
                        temp.m_Index = CGlobal.m_Device.m_Connection.m_DC2Index;
                        break;
                    case 2:
                        temp.m_Name = "AC电源电压1";
                        temp.m_Index = CGlobal.m_Device.m_Connection.m_DC2Index;
                        break;
                    case 3:
                        temp.m_Name = "AC电源电压2";
                        temp.m_Index = CGlobal.m_Device.m_Connection.m_DC2Index;
                        break;
                }
                CGlobal.m_Device.m_DC_Checkdata.Add(temp);
            }
        }
        private void IniListView()
        {
            for (int i = 0; i < CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo].Count; i++)
            {
                LV_AC_Check.Items[i].SubItems[2].Text = CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].m_Default.ToString();
                LV_AC_Check.Items[i].SubItems[3].Text = CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].m_Ratio.ToString();
                LV_AC_Check.Items[i].SubItems[5].Text = CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].m_Mul_A.ToString();
                LV_AC_Check.Items[i].SubItems[6].Text = CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].m_Mul_B.ToString();
            }
        }
        private void Init_LV_AC_Check()
        {
            string LineName = CGlobal.m_Device.SelLineNo.ToString() + "#线路";
            LV_AC_Check.ClearAllItem();
            LV_AC_Check.Columns.Add("选择", 50, HorizontalAlignment.Center);
            LV_AC_Check.Columns.Add(LineName, 100, HorizontalAlignment.Center);
            LV_AC_Check.Columns.Add("基准值", 120, HorizontalAlignment.Center);
            LV_AC_Check.Columns.Add("系数", 120, HorizontalAlignment.Center);
            LV_AC_Check.Columns.Add("实时值", 120, HorizontalAlignment.Center);
            LV_AC_Check.Columns.Add("系数A", 120, HorizontalAlignment.Center);
            LV_AC_Check.Columns.Add("系数B", 120, HorizontalAlignment.Center);

            LV_AC_Check.GridLines = true;
            LV_AC_Check.FullRowSelect = true;
            LV_AC_Check.View = View.Details;
            LV_AC_Check.Scrollable = true;
            LV_AC_Check.MultiSelect = false;
            LV_AC_Check.HeaderStyle = ColumnHeaderStyle.Clickable;
            LV_AC_Check.CheckBoxes = true;
            LV_AC_Check.Dock = DockStyle.Fill;
            LV_AC_Check.Visible = true;
            string[] strHead = new string[7];
            for (int i = 0; i < CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo].Count; i++)
            {
                strHead[1] = CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].m_Name;
                strHead[2] = CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].m_Default.ToString();
                strHead[3] = CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].m_Ratio.ToString();
                strHead[4] = CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].m_Real.ToString();
                strHead[5] = CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].m_Mul_A.ToString();
                strHead[6] = CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].m_Mul_B.ToString();

                ListViewItem item1 = new ListViewItem(strHead);
                if (CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].Is_Check == 1)
                    item1.Checked = true;
                this.LV_AC_Check.AddItem(item1);
            }
            LV_AC_Check.SetColumnsEditMode(2);
            LV_AC_Check.SetColumnsEditMode(3);
            LV_AC_Check.SetColumnsEditMode(5);
            LV_AC_Check.SetColumnsEditMode(6);
        }
        private void Init_LV_DC_Check()
        {

            LV_DC_Check.ClearAllItem();
            LV_DC_Check.Columns.Add("选择", 50, HorizontalAlignment.Center);
            LV_DC_Check.Columns.Add("名称", 100, HorizontalAlignment.Center);
            LV_DC_Check.Columns.Add("基准值", 120, HorizontalAlignment.Center);
            LV_DC_Check.Columns.Add("实时值", 120, HorizontalAlignment.Center);

            LV_DC_Check.GridLines = true;
            LV_DC_Check.FullRowSelect = true;
            LV_DC_Check.View = View.Details;
            LV_DC_Check.Scrollable = true;
            LV_DC_Check.MultiSelect = false;
            LV_DC_Check.HeaderStyle = ColumnHeaderStyle.Clickable;
            LV_DC_Check.CheckBoxes = true;
            LV_DC_Check.Dock = DockStyle.Fill;
            string[] strHead = new string[4];
            for (int i = 0; i < CGlobal.m_Device.m_DC_Checkdata.Count; i++)
            {
                strHead[1] = CGlobal.m_Device.m_DC_Checkdata[i].m_Name;
                strHead[2] = CGlobal.m_Device.m_DC_Checkdata[i].m_Default.ToString();
                strHead[3] = CGlobal.m_Device.m_DC_Checkdata[i].m_Real.ToString();
                ListViewItem item1 = new ListViewItem(strHead);
                if (CGlobal.m_Device.m_DC_Checkdata[i].Is_Check == 1)
                    item1.Checked = true;
                this.LV_DC_Check.AddItem(item1);
            }
            LV_DC_Check.SetColumnsEditMode(2);
        }
        private void Init_LV_Phase()
        {
            string LineName = CGlobal.m_Device.SelLineNo.ToString() + "#线路";
            LV_Phase.Items.Clear();
            LV_Phase.Columns.Clear();
            LV_Phase.Columns.Add(LineName, 100, HorizontalAlignment.Center);
            LV_Phase.Columns.Add("绝对相角度数", 120, HorizontalAlignment.Center);
            LV_Phase.GridLines = true;
            LV_Phase.FullRowSelect = true;
            LV_Phase.View = View.Details;
            LV_Phase.Scrollable = true;
            LV_Phase.MultiSelect = false;
            LV_Phase.HeaderStyle = ColumnHeaderStyle.Clickable;
            LV_Phase.Dock = DockStyle.Fill;
            string[] strHead = new string[2];
            for (int i = 0; i < CGlobal.m_Device.m_Phasedata[CGlobal.m_Device.SelLineNo].Count; i++)
            {
                strHead[0] = CGlobal.m_Device.m_Phasedata[CGlobal.m_Device.SelLineNo][i].m_Name;
                strHead[1] = CGlobal.m_Device.m_Phasedata[CGlobal.m_Device.SelLineNo][i].m_Value.ToString();

                ListViewItem item1 = new ListViewItem(strHead);
                this.LV_Phase.Items.Add(item1);
            }
        }
        private void com_LineNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CGlobal.m_Device.SelLineNo = com_LineNo.SelectedIndex;
            ReSetSelect(0);
            ReSetSelect(1);
            ReSetSelect(2);
            Init_LV_Phase();
            Init_LV_AC_Check();
        }
        private void ReSetSelect(int nType)
        {
            if (nType == 0)
            {
                switch (com_Vol.SelectedIndex)
                {
                    case 0:
                        SelVol = 100;
                        break;
                    case 1:
                        SelVol = 220;
                        break;
                    case 2:
                        SelVol = 380;
                        break;
                    case 3:
                        SelVol = (float)57.7;
                        break;
                    case 4:
                        SelVol = 2;
                        break;
                }
                CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][0].m_Default = SelVol;
                CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][1].m_Default = SelVol;
                CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][2].m_Default = SelVol;
                CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][3].m_Default = SelVol;
            }
            else if (nType == 1)
            {
                switch (com_Current.SelectedIndex)
                {
                    case 0:
                        SelCur = 1;
                        break;
                    case 1:
                        SelCur = 5;
                        break;
                }
                CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][4].m_Default = SelCur;
                CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][5].m_Default = SelCur;
                CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][6].m_Default = SelCur;
                CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][7].m_Default = SelCur;
                CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][8].m_Default = SelCur;
                CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][9].m_Default = SelCur;
                CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][10].m_Default = SelCur;
            }
            else
            {
                switch (com_Angle.SelectedIndex)
                {
                    case 0:
                        SelAng = 0;
                        break;
                    case 1:
                        SelAng = 30;
                        break;
                    case 2:
                        SelAng = 45;
                        break;
                    case 3:
                        SelAng = 60;
                        break;
                    case 4:
                        SelAng = 90;
                        break;
                }

                CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][11].m_Default = SelAng;
                CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][12].m_Default = SelAng;
                CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][13].m_Default = SelAng;
            }
        }
        private void com_Vol_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReSetSelect(0);
            IniListView();
        }

        private void com_Current_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReSetSelect(1);
            IniListView();
        }

        private void com_Angle_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReSetSelect(2);
            IniListView();
        }
        private void Btn_Check_Click(object sender, EventArgs e)
        {
            flag_Ac = true;
            LV_AC_Check.Visible = true;
            LV_AC_Check.Dock = DockStyle.Fill;
            LV_Phase.Visible = false;
            LV_DC_Check.Visible = false;


            BtnCheckCmd.Enabled = true;
            Btn_CallRationCmd.Enabled = true;
            Btn_DownRatioCmd.Enabled = true;
            BtnCallPhaseCmd.Enabled = false;
        }
        private void Btn_DC_Check_Click(object sender, EventArgs e)
        {
            flag_Ac = false;
            LV_DC_Check.Visible = true;
            LV_DC_Check.Dock = DockStyle.Fill;
            LV_Phase.Visible = false;
            LV_AC_Check.Visible = false;

            BtnCheckCmd.Enabled = true;
            Btn_CallRationCmd.Enabled = false;
            Btn_DownRatioCmd.Enabled = false;
            BtnCallPhaseCmd.Enabled = false;
        }
        private void BtnCallPhase_Click(object sender, EventArgs e)
        {
            LV_Phase.Visible = true;
            LV_Phase.Dock = DockStyle.Fill;
            LV_AC_Check.Visible = false;
            LV_DC_Check.Visible = false;

            BtnCheckCmd.Enabled = false;
            Btn_CallRationCmd.Enabled = false;
            Btn_DownRatioCmd.Enabled = false;
            BtnCallPhaseCmd.Enabled = true;
        }


        //数据刷新

        private void Fresh_CheckData()
        {
            if (flag_Ac == true)
            {
                for (int i = 0; i < CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo].Count; i++)
                {
                    int nYcIndex = CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].m_Index;
                    if (nYcIndex > 0 && nYcIndex <= CGlobal.m_Device.m_YcList.Count)
                    {
                        float fValue = CGlobal.m_Device.m_YcList[nYcIndex - 1].m_YcValue;
                        LV_AC_Check.Items[i].SubItems[4].Text = fValue.ToString("F3");
                    }
                }
            }
            else
            {
                for (int i = 0; i < CGlobal.m_Device.m_DC_Checkdata.Count; i++)
                {
                    int nYcIndex = CGlobal.m_Device.m_DC_Checkdata[i].m_Index;
                    if (nYcIndex > 0 && nYcIndex <= CGlobal.m_Device.m_YcList.Count)
                    {
                        float fValue = CGlobal.m_Device.m_YcList[nYcIndex - 1].m_YcValue;
                        LV_DC_Check.Items[i].SubItems[3].Text = fValue.ToString("F3");
                    }
                }
            }

        }
        private void Fresh_RatioData()
        {
            for (int i = 0; i < CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo].Count; i++)
            {
                LV_AC_Check.Items[i].SubItems[3].Text = CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].m_Ratio.ToString();
                LV_AC_Check.Items[i].SubItems[5].Text = CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].m_Mul_A.ToString();
                LV_AC_Check.Items[i].SubItems[6].Text = CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].m_Mul_B.ToString();
            }
        }
        private void Fresh_PhaseData()
        {
            for (int i = 0; i < CGlobal.m_Device.m_Phasedata[CGlobal.m_Device.SelLineNo].Count; i++)
            {
                LV_Phase.Items[i].SubItems[1].Text = CGlobal.m_Device.m_Phasedata[CGlobal.m_Device.SelLineNo][i].m_Value.ToString();
            }
        }
        private void Freshtimer_Tick(object sender, EventArgs e)
        {
            Fresh_CheckData();
            if (CGlobal.m_Device.m_bOnlineStatus == false)
                return;
            if (CGlobal.m_Device.GetRatioFreshStatus())
            {
                Fresh_RatioData();
            }
            if (CGlobal.m_Device.GetPhaseFreshStatus())
            {
                Fresh_PhaseData();
            }
        }


        //校验按钮
        private void BtnCheckCmd_Click(object sender, EventArgs e)
        {
            if (flag_Ac == true)
            {
                for (int i = 0; i < CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo].Count; i++)
                {
                    if (LV_AC_Check.Items[i].Checked)
                    {
                        CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].Is_Check = 0x01;
                    }
                    else
                    {
                        CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].Is_Check = 0x00;
                    }
                    CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].m_Default = Convert.ToSingle(LV_AC_Check.Items[i].SubItems[2].Text);
                }
                CGlobal.SendTaskOrder(25);
            }
            else
            {
                for (int i = 0; i < CGlobal.m_Device.m_DC_Checkdata.Count; i++)
                {
                    if (LV_DC_Check.Items[i].Checked)
                    {
                        CGlobal.m_Device.m_DC_Checkdata[i].Is_Check = 0x01;
                    }
                    else
                    {
                        CGlobal.m_Device.m_DC_Checkdata[i].Is_Check = 0x00;
                    }
                    CGlobal.m_Device.m_DC_Checkdata[i].m_Default = Convert.ToSingle(LV_DC_Check.Items[i].SubItems[2].Text);
                }
                CGlobal.SendTaskOrder(32);
            }
        }

        private void BtnCallPhaseCmd_Click(object sender, EventArgs e)
        {
            CGlobal.SendTaskOrder(31);
        }

        private void Btn_CallRationCmd_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo].Count; i++)
            {
                if (LV_AC_Check.Items[i].Checked)
                {
                    CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].Is_Check = 0x01;
                }
                else
                {
                    CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].Is_Check = 0x00;
                }
                LV_AC_Check.Items[i].SubItems[3].Text = "";
            }
            CGlobal.SendTaskOrder(26);
        }

        private void Btn_DownRatioCmd_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo].Count; i++)
            {
                if (LV_AC_Check.Items[i].Checked)
                {
                    CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].Is_Down = 0x01;
                }
                else
                {
                    CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].Is_Down = 0x00;
                }
                if (LV_AC_Check.Items[i].SubItems[3].Text.Length > 0)
                {
                    try
                    {
                        CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].m_Ratio = Convert.ToUInt16(LV_AC_Check.Items[i].SubItems[3].Text);
                        CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].m_Mul_A = Convert.ToSingle(LV_AC_Check.Items[i].SubItems[5].Text);
                        CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo][i].m_Mul_B = Convert.ToSingle(LV_AC_Check.Items[i].SubItems[6].Text);
                    }
                    catch
                    {
                        MessageBox.Show("系数为整数！", "提示");
                    }
                }
            }
            CGlobal.SendTaskOrder(27);
        }

        private void BtnSelectAllCmd_Click(object sender, EventArgs e)
        {

            if (flag_Ac == true)
            {
                for (int i = 0; i < CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo].Count; i++)
                {
                    LV_AC_Check.Items[i].Checked = true;
                }
            }
            else
            {
                for (int i = 0; i < CGlobal.m_Device.m_DC_Checkdata.Count; i++)
                {
                    LV_DC_Check.Items[i].Checked = true;
                }

            }
        }

        private void SelectKillAllCmd_Click(object sender, EventArgs e)
        {
            if (flag_Ac == true)
            {
                for (int i = 0; i < CGlobal.m_Device.m_AC_Checkdata[CGlobal.m_Device.SelLineNo].Count; i++)
                {
                    LV_AC_Check.Items[i].Checked = false;
                }
            }
            else
            {
                for (int i = 0; i < CGlobal.m_Device.m_DC_Checkdata.Count; i++)
                {
                    LV_DC_Check.Items[i].Checked = false;
                }

            }
        }
    }
}
