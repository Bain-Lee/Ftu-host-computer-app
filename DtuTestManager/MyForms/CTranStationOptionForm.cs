using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace DtuTestManager
{
    public partial class CTranStationOptionForm: Form
    {
        public CTransStation m_Station = null;
        private List<CTranValue> m_OptionList = new List<CTranValue>();
        private List<CTranValue> m_CountList = new List<CTranValue>();
        private List<CTranValue> m_RecordList = new List<CTranValue>();

        private int m_OptionIndex = 0;
        private int m_RecordIndex = 0;
        private CTransCalcYxCfg m_CalcYxTable = new CTransCalcYxCfg();     //遥信计算表
        private CTransYxDB m_YxTable = new CTransYxDB();   
        private CTransYcDB m_YcTable = new CTransYcDB();
        private CTransYkDB m_YkTable = new CTransYkDB();
        private CTransPowerDB m_PowerTable = new CTransPowerDB();
        private CTransSetDB m_SetTable = new CTransSetDB();
        private CBattertyAcv m_BatteryTable = new CBattertyAcv();
        private CComOption m_ComTable = new CComOption();
       

        public CTranStationOptionForm()
        {
            InitializeComponent();
        }
        private void IniListView_One()
        {
        
            OptionView.Clear();
            OptionView.GridLines = true;
            OptionView.FullRowSelect = true;
            OptionView.View = View.Details;
            OptionView.Scrollable = true;
            OptionView.MultiSelect = false;
            OptionView.HeaderStyle = ColumnHeaderStyle.Clickable;
            OptionView.Visible = true;

            OptionView.Columns.Add("序号", 60, HorizontalAlignment.Center);
            OptionView.Columns.Add("名称", 120, HorizontalAlignment.Left);
            OptionView.Columns.Add("数值", 120, HorizontalAlignment.Left);
            OptionView.Columns.Add("列名", 120, HorizontalAlignment.Left);
            OptionView.Columns.Add("类型", 60, HorizontalAlignment.Left);

            string[] strItem = new string[5];
            for (int i = 0; i < m_OptionList.Count; i++)
            {
                CTranValue pData = m_OptionList[i];
                strItem[0] = (i+1).ToString();
                strItem[1] = pData.m_StrDataName;
                strItem[2] = (string)pData.m_TranValue;
                strItem[3] = pData.m_StrColName;
                strItem[4] = pData.GetDataTypeDesc();
                ListViewItem pItem = new ListViewItem(strItem);
                OptionView.AddItem(pItem);
                if (pData.m_EditMode)
                {
                    int nIndex = OptionView.AddComBoxListStr(pData.m_ComStringList);
                    OptionView.SetCellMode(i, 2, 2, nIndex);
                }
                else
                {
                    OptionView.SetCellMode(i, 2, 1, 0);
                }
            }
        }
        private void IniListView_Two()
        {
            CountSetView.Clear();
            CountSetView.GridLines = true;
            CountSetView.FullRowSelect = true;
            CountSetView.View = View.Details;
            CountSetView.Scrollable = true;
            CountSetView.MultiSelect = false;
            CountSetView.HeaderStyle = ColumnHeaderStyle.Clickable;
            CountSetView.Visible = true;

            CountSetView.Columns.Add("序号", 60, HorizontalAlignment.Center);
            CountSetView.Columns.Add("名称", 120, HorizontalAlignment.Left);
            CountSetView.Columns.Add("数值", 120, HorizontalAlignment.Left);
            CountSetView.Columns.Add("列名", 120, HorizontalAlignment.Left);
            CountSetView.Columns.Add("类型", 60, HorizontalAlignment.Left);

            string[] strItem = new string[5];
            for (int i = 0; i < m_CountList.Count; i++)
            {
                CTranValue pData = m_CountList[i];
                strItem[0] = (i + 1).ToString();
                strItem[1] = pData.m_StrDataName;
                strItem[2] = (string)pData.m_TranValue;
                strItem[3] = pData.m_StrColName;
                strItem[4] = pData.GetDataTypeDesc();
                ListViewItem pItem = new ListViewItem(strItem);
                CountSetView.Items.Add(pItem);
            }
        }
        private void IniListView_Three()
        {
            RecordView.Clear();
            RecordView.GridLines = true;
            RecordView.FullRowSelect = true;
            RecordView.View = View.Details;
            RecordView.Scrollable = true;
            RecordView.MultiSelect = false;
            RecordView.HeaderStyle = ColumnHeaderStyle.Clickable;
            RecordView.Visible = true;

            RecordView.Columns.Add("序号", 60, HorizontalAlignment.Center);
            RecordView.Columns.Add("名称", 120, HorizontalAlignment.Left);
            RecordView.Columns.Add("数值", 120, HorizontalAlignment.Left);
            RecordView.Columns.Add("列名", 120, HorizontalAlignment.Left);
            RecordView.Columns.Add("类型", 60, HorizontalAlignment.Left);

            string[] strItem = new string[5];
            for (int i = 0; i < m_RecordList.Count; i++)
            {
                CTranValue pData = m_RecordList[i];
                strItem[0] = (i + 1).ToString();
                strItem[1] = pData.m_StrDataName;
                strItem[2] = (string)pData.m_TranValue;
                strItem[3] = pData.m_StrColName;
                strItem[4] = pData.GetDataTypeDesc();
                ListViewItem pItem = new ListViewItem(strItem);
                RecordView.AddItem(pItem);
                if (pData.m_EditMode)
                {
                    int nIndex = RecordView.AddComBoxListStr(pData.m_ComStringList);
                    RecordView.SetCellMode(i, 2, 2, nIndex);
                }
                else
                {
                    RecordView.SetCellMode(i, 2, 1, 0);
                }
            }
        }
        private void CTranStationOptionForm_Load(object sender, EventArgs e)
        {
            if (m_Station.m_Sqlite_Main == null) return;
            if (m_Station.m_IsEmpty) return;
            m_CalcYxTable.pData = m_Station.m_ValueList[20];
            m_CalcYxTable.m_TableName = (string)m_CalcYxTable.pData.m_TranValue;
            m_CalcYxTable.m_SqlDataBase = m_Station.m_MainSqlite;
            m_CalcYxTable.IniReadTableData();

            m_YxTable.m_TableName = "SubYxDB";
            m_YxTable.m_SqlDataBase = m_Station.m_MainSqlite;
            m_YxTable.IniReadTableData();
          


            m_YcTable.m_TableName = "SubYcDB";
            m_YcTable.m_SqlDataBase = m_Station.m_MainSqlite;
            m_YcTable.IniReadTableData();
           

            m_YkTable.m_TableName = "SubYkDB";
            m_YkTable.m_SqlDataBase = m_Station.m_MainSqlite;
            m_YkTable.IniReadTableData();
           
            m_PowerTable.m_TableName = "SubPowerDB";
            m_PowerTable.m_SqlDataBase = m_Station.m_MainSqlite;
            m_PowerTable.IniReadTableData();
           

            m_SetTable.m_TableName = "SubSetDB";
            m_SetTable.m_SqlDataBase = m_Station.m_MainSqlite;
            m_SetTable.IniReadTableData();

            m_BatteryTable.pData = m_Station.m_ValueList[3];
            m_BatteryTable.m_TableName = (string)m_BatteryTable.pData.m_TranValue;
            m_BatteryTable.m_SqlDataBase = m_Station.m_UserSqlite;
            m_BatteryTable.IniReadTableData();

            m_ComTable.m_TableName = "SerialPortCfg";
            m_ComTable.m_SqlDataBase = m_Station.m_UserSqlite;
            m_ComTable.IniReadTableData();

            for (int i = 0; i < m_Station.m_ValueList.Count; i++)
            {
                CTranValue pData = m_Station.m_ValueList[i];
                switch (pData.m_GroupNo)
                {
                    case 1:
                        m_OptionList.Add(pData);
                        break;
                    case 2:
                        m_CountList.Add(pData);
                        break;
                    case 3:
                        m_RecordList.Add(pData);
                        break;
                }
            }
            IniListView_One();
            IniListView_Two();
            IniListView_Three();
            m_Station.SubYxCnt = Convert.ToInt32(m_CountList[0].m_TranValue);
        }

        private void OptionView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (OptionView.SelectedIndices != null && OptionView.SelectedIndices.Count > 0)
            {
                m_OptionIndex = OptionView.SelectedItems[0].Index;
            }
            else
            {
                m_OptionIndex = -1;
            }
        }

        private void RecordView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RecordView.SelectedIndices != null && RecordView.SelectedIndices.Count > 0)
            {
                m_RecordIndex = RecordView.SelectedItems[0].Index;
            }
            else
            {
                m_RecordIndex = -1;
            }
        }
        private void OptionView_DoubleClick(object sender, EventArgs e)
        {
            if (m_OptionIndex >=0)
            {
                OptionView.theProcessFun = new MyListView.ProcessDelegate(m_OptionList[m_OptionIndex].UpateValue);
            }
        }

        private void RecordView_DoubleClick(object sender, EventArgs e)
        {
            if (m_RecordIndex >= 0)
            {
                RecordView.theProcessFun = new MyListView.ProcessDelegate(m_RecordList[m_OptionIndex].UpateValue);
            }
        }

        private void SaveSetBtn_Click(object sender, EventArgs e)
        {
            string strSql = "";
            string strDataContent = "";
            int i,nCount = 0;

            strSql = "update SubStationNodeCfg set ";
            for(i=0;i<m_OptionList.Count;i++)
            {
                CTranValue pData = m_OptionList[i];
                if (pData.m_bChange == false) continue;
                pData.m_bChange = false;
                if (nCount > 0) strDataContent += ",";
                strDataContent += pData.GetUpdateValueString();
                nCount++;
            }
            for (i = 0; i < m_RecordList.Count; i++)
            {
                CTranValue pData = m_RecordList[i];
                if (pData.m_bChange == false) continue;
                pData.m_bChange = false;
                if (nCount > 0) strDataContent += ",";
                strDataContent += pData.GetUpdateValueString();
                nCount++;
            }
            if (nCount == 0) return;

            strSql += strDataContent;
            m_Station.m_MainSqlite.ExecSql(strSql);
            CheckSingleTableUpdate();

            m_Station.SubYxCnt = Convert.ToInt32(m_CountList[0].m_TranValue);
        }
        private void CheckSingleTableUpdate()
        {
            m_CalcYxTable.UpdateTable();
        }
        private void YxCalcBtn_Click(object sender, EventArgs e)
        {
            if ((string)m_CalcYxTable.pData.m_TranValue == "null" || (string)m_CalcYxTable.pData.m_TranValue == "NULL") return;
            if (m_CalcYxTable.m_TableName == "null" || m_CalcYxTable.m_TableName == "NULL") return;
            string filepath = m_Station.m_StationPath + "\\" + "公共配置.ini";
            if (m_Station.m_Sub.m_TermDataValue[5].m_isReadDataName== false)
            {
                if (File.Exists(filepath))
                {
                    m_Station.m_Sub.ReadDataINIFile(5);
                }   
            }
            CTranTableManageSubForm pForm = new CTranTableManageSubForm();
            pForm.m_Station = m_Station;
            pForm.m_Table = m_CalcYxTable;
            pForm.NodeType = 5;
            pForm.pSub = m_Station.m_Sub;
            pForm.ShowDialog();
        }

        private void BtnYc_Click(object sender, EventArgs e)
        {

            if (m_YcTable.m_TableName == "null" || m_YcTable.m_TableName == "NULL") return;
            string filepath = m_Station.m_StationPath + "\\" + "公共配置.ini";
            if (m_Station.m_Sub.m_TermDataValue[1].m_isReadDataName == false)
            {
                if (File.Exists(filepath))
                {
                    m_Station.m_Sub.ReadDataINIFile(1);
                }               
            }
            CTranTableManageSubForm pForm = new CTranTableManageSubForm();
         
            pForm.m_Table = m_YcTable;
            pForm.NodeType = 1;
            pForm.pSub = m_Station.m_Sub;
            pForm.ShowDialog();
            int nCount = m_YcTable.GetRowCount();
            m_Station.m_ValueList[7].m_TranValue = nCount;
            string strSql = "update SubStationNodeCfg set ";
            strSql += "YcDB=" + nCount.ToString();
            m_Station.m_MainSqlite.ExecSql(strSql);
            IniListView_Two();

        }

        private void BtnYx_Click(object sender, EventArgs e)
        {
            if (m_YxTable.m_TableName == "null" || m_YxTable.m_TableName == "NULL") return;
            string filepath = m_Station.m_StationPath + "\\" + "公共配置.ini";
            if (m_Station.m_Sub.m_TermDataValue[0].m_isReadDataName == false)
            {
                if (File.Exists(filepath))
                {
                    m_Station.m_Sub.ReadDataINIFile(0);
                }
            }
            CTranTableManageSubForm pForm = new CTranTableManageSubForm();
            pForm.m_Station = m_Station;
            pForm.m_Table = m_YxTable;
            pForm.NodeType = 0;
            pForm.pSub = m_Station.m_Sub;
            pForm.ShowDialog();
            int nCount=m_YxTable.GetRowCount();
            m_Station.m_ValueList[6].m_TranValue = nCount;
            string strSql = "update SubStationNodeCfg set ";
            strSql += "YxDB=" + nCount.ToString();
            m_Station.m_MainSqlite.ExecSql(strSql);
            IniListView_Two();
        }

        private void BtnYk_Click(object sender, EventArgs e)
        {
            if (m_YkTable.m_TableName == "null" || m_YkTable.m_TableName == "NULL") return;
            string filepath = m_Station.m_StationPath + "\\" + "公共配置.ini";
            if (m_Station.m_Sub.m_TermDataValue[2].m_isReadDataName == false)
            {
                if (File.Exists(filepath))
                {
                    m_Station.m_Sub.ReadDataINIFile(2);
                }
            }
            CTranTableManageSubForm pForm = new CTranTableManageSubForm();
            pForm.m_Table = m_YkTable;
            pForm.NodeType = 2;
            pForm.pSub = m_Station.m_Sub;
            pForm.ShowDialog();

            int nCount = m_YkTable.GetRowCount();

            m_Station.m_ValueList[8].m_TranValue = nCount;

            string strSql = "update SubStationNodeCfg set ";
            strSql += "YkDB=" + nCount.ToString();
            m_Station.m_MainSqlite.ExecSql(strSql);

            IniListView_Two();
        }

        private void BtnYm_Click(object sender, EventArgs e)
        {
            if (m_PowerTable.m_TableName == "null" || m_PowerTable.m_TableName == "NULL") return;
            string filepath = m_Station.m_StationPath + "\\" + "公共配置.ini";
            if (m_Station.m_Sub.m_TermDataValue[4].m_isReadDataName == false)
            {
                if (File.Exists(filepath))
                {
                    m_Station.m_Sub.ReadDataINIFile(4);
                }
            }
            CTranTableManageSubForm pForm = new CTranTableManageSubForm();
            pForm.m_Table = m_PowerTable;
            pForm.NodeType = 4;
            pForm.pSub = m_Station.m_Sub;
            pForm.ShowDialog();
            int nCount = m_PowerTable.GetRowCount();
            m_Station.m_ValueList[10].m_TranValue = nCount;
            string strSql = "update SubStationNodeCfg set ";
            strSql += "PowerDB=" + nCount.ToString();
            m_Station.m_MainSqlite.ExecSql(strSql);

            IniListView_Two();
        }

        private void BtnSet_Click(object sender, EventArgs e)
        {
            if (m_SetTable.m_TableName == "null" || m_SetTable.m_TableName == "NULL") return;
            string filepath = m_Station.m_StationPath + "\\" + "公共配置.ini";
            if (m_Station.m_Sub.m_TermDataValue[3].m_isReadDataName == false)
            {
                if (File.Exists(filepath))
                {
                    m_Station.m_Sub.ReadDataINIFile(3);
                }
            }
            CTranTableManageSubForm pForm = new CTranTableManageSubForm();
            pForm.m_Table = m_SetTable;
            pForm.NodeType = 3;
            pForm.pSub = m_Station.m_Sub;
            pForm.ShowDialog();
            int nCount = m_SetTable.GetRowCount();
            m_Station.m_ValueList[9].m_TranValue = nCount;
            string strSql = "update SubStationNodeCfg set ";
            strSql += "SetDB=" + nCount.ToString();
            m_Station.m_MainSqlite.ExecSql(strSql);
            IniListView_Two();
        }

        private void BtnDCHH_Click(object sender, EventArgs e)
        {
            if ((string)m_BatteryTable.pData.m_TranValue == "null" || (string)m_BatteryTable.pData.m_TranValue == "NULL") return;
            if (m_BatteryTable.m_TableName == "null" || m_BatteryTable.m_TableName == "NULL") return;
            CTranTableManageSubForm pForm = new CTranTableManageSubForm();
            pForm.m_Table = m_BatteryTable;
            pForm.NodeType = 6;
            pForm.pSub = m_Station.m_Sub;
            pForm.ShowDialog();
        }

        private void BtnComSet_Click(object sender, EventArgs e)
        {
            if (m_ComTable.m_TableName == "null" || m_ComTable.m_TableName == "NULL") return;
            CTranTableManageSubForm pForm = new CTranTableManageSubForm();
            pForm.m_Table = m_ComTable;
            pForm.NodeType = 7;
            pForm.pSub = m_Station.m_Sub;
            pForm.ShowDialog();
        }
    }
}
