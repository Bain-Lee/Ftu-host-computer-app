using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO;
using System.Threading;
using System.Data.OleDb;


namespace DtuTestManager
{
    public partial class CPriOptionForm : Form
    {
        public CTransStation m_Station = null;
        public CPriConfig pDevice = null;
        private List<CTranValue> m_OptionList = new List<CTranValue>();
        private List<CTranValue> m_CountList = new List<CTranValue>();
        private List<CTranValue> m_RecordList = new List<CTranValue>();

        private int m_OptionIndex = 0;
        private int m_RecordIndex = 0;


        private CTransYxDB m_YxTable = new CTransYxDB();
        private CTransYcDB m_YcTable = new CTransYcDB();
        private CTransYkDB m_YkTable = new CTransYkDB();
        private CTransPowerDB m_PowerTable = new CTransPowerDB();
        private CTransSetDB m_SetTable = new CTransSetDB();
        private CTransProtocl m_ProtoclTable = new CTransProtocl();

        public CPriOptionForm()
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
                strItem[0] = (i + 1).ToString();
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

            // BtnCreateTestModule.Visible = false;
            m_ProtoclTable.m_TableName = (string)(pDevice.m_ValueList[14].m_TranValue);
            m_ProtoclTable.m_SqlDataBase = pDevice.m_PriRoot.m_TheStation.m_UserSqlite;
            m_ProtoclTable.IniReadTableData();
            for (int i = 0; i < pDevice.m_ValueList.Count; i++)
            {
                CTranValue pData = pDevice.m_ValueList[i];
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
            if (m_OptionIndex >= 0)
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
            int m_NodeNo = Convert.ToInt32(OptionView.Items[0].SubItems[2].Text);
            string strSql = "";
            string strDataContent = "";
            int i, nCount = 0;

            strSql = "update PriStationNodeCfg set ";
            for (i = 0; i < m_OptionList.Count; i++)
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
            // strSql += " where(NodeType='" + pDevice.m_Node.Text + "')";
            strSql += " where(NodeNo=" + pDevice.m_TermNo.ToString() + ")";
            pDevice.m_PriRoot.m_TheStation.m_MainSqlite.ExecSql(strSql);
        }

        private void BtnProtocl_Click(object sender, EventArgs e)
        {
            if (m_ProtoclTable.m_TableName == "null" || m_ProtoclTable.m_TableName == "NULL") return;
            CTranTableManageTermForm pForm = new CTranTableManageTermForm();
            pForm.m_Table = m_ProtoclTable;
            pForm.NodeType = 7;
            pForm.ShowDialog();

        }

        private bool CheckNameExist(string strNewName)
        {
            string strPath = "DevModuleDataBase\\" + strNewName + ".mdb";
            if (File.Exists(strPath))
            {
                return true;
            }
            strPath = "DefaultDevModule\\" + strNewName + ".mdb";
            if (File.Exists(strPath))
            {
                return true;
            }
            return false;
        }
        private void BtnCreateTestModule_Click(object sender, EventArgs e)
        {
            int i;
            string[] strTableName = new string[5];
            List<CRouteTable> m_RoutList = new List<CRouteTable>();
            strTableName[0] = (string)m_OptionList[24].m_TranValue;
            strTableName[1] = (string)m_OptionList[26].m_TranValue;
            strTableName[2] = (string)m_OptionList[28].m_TranValue;
            strTableName[3] = (string)m_OptionList[30].m_TranValue;
            strTableName[4] = (string)m_OptionList[32].m_TranValue;
            for (i = 0; i < 5; i++)
            {
                if (strTableName[i] != "NULL") break;
            }
            if (i == 5) return;
            string strName =m_Station.m_StationName+ "主站" + pDevice.m_TermNo.ToString() + ".mdb";
            // if (CheckNameExist(strName)) return;
            string strSourceFile = "DefaultDevModule\\DTU标准模板.mdb";
            string txtFileSaveDir = System.Environment.CurrentDirectory + "\\" + "工程模板";
            if (!Directory.Exists(txtFileSaveDir))//若文件夹不存在则新建文件夹   
            {
                Directory.CreateDirectory(txtFileSaveDir); //新建文件夹   
            }
            if (File.Exists(strSourceFile) == false) return;
            string strItemFile = txtFileSaveDir + "\\" + strName;
            if (File.Exists(strItemFile) == false)
            {
                File.Copy(strSourceFile, strItemFile);
            }
            Thread.Sleep(500);
            CDataBase m_ModuleDB = new CDataBase();
            m_ModuleDB.m_Path = strItemFile;
            m_ModuleDB.ConnectDataBase();

            SaveYxData(strTableName[0], m_ModuleDB);
            SaveYcData(strTableName[1], m_ModuleDB);
            SaveYkData(strTableName[2], m_ModuleDB);
            SaveYmData(strTableName[4], m_ModuleDB);
        }
        private void SaveYxData(string strTableName, CDataBase m_ModuleDB)
        {
            List<CDevModuleData> m_DataModuleList = new List<CDevModuleData>();
            CDevYcGroup m_DevYc = new CDevYcGroup();
            int Cnt = 0;
            List<string> m_DataName = new List<string>();
            string m_filePath = m_Station.m_StationPath + "\\" + strTableName + ".ini";
            StringBuilder m_Value = new StringBuilder(100);
            if (File.Exists(m_filePath) == true)
            {
                CGlobal.GetPrivateProfileString("DataCount", "Count", "0", m_Value, 100, m_filePath);
                Cnt = Convert.ToInt32(m_Value.ToString());
                for (int j = 0; j < Cnt; j++)
                {
                    CGlobal.GetPrivateProfileString("NameList", "Name" + j.ToString(), "未知", m_Value, 100, m_filePath);
                    CDevModuleData temp = new CDevModuleData();
                    temp.m_DataNo = (j + 1);
                    temp.m_DataName = m_Value.ToString();
                    temp.m_Address = 1 + j;
                    m_DataModuleList.Add(temp);
                }
            }

            string strSql = "delete from " + "Mod_YxTable";
            m_ModuleDB.ExecSql(strSql);
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = m_ModuleDB.m_Dbconn;
            cmd.Parameters.Add(cmd.CreateParameter());
            OleDbTransaction trans = m_ModuleDB.m_Dbconn.BeginTransaction();
            cmd.Transaction = trans;
            try
            {

                for (int i = 0; i < m_DataModuleList.Count; i++)
                {
                    strSql = "insert into " + "Mod_YxTable" + " values(" +
                        m_DataModuleList[i].m_DataNo.ToString() + ",'" +
                        m_DataModuleList[i].m_DataName + "'," +
                        m_DataModuleList[i].m_Address.ToString() + ")";
                    cmd.CommandText = strSql;
                    cmd.Parameters[0].Value = i.ToString();
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                throw;

            }
        }
        private void SaveYcData(string strTableName, CDataBase m_ModuleDB)
        {
            List<CDevModuleData> m_DataModuleList = new List<CDevModuleData>();
            CDevYcGroup m_DevYc = new CDevYcGroup();
            int Cnt = 0;
            List<string> m_DataName = new List<string>();
            string m_filePath = m_Station.m_StationPath + "\\" + strTableName + ".ini";
            StringBuilder m_Value = new StringBuilder(100);
            if (File.Exists(m_filePath) == true)
            {
                CGlobal.GetPrivateProfileString("DataCount", "Count", "0", m_Value, 100, m_filePath);
                Cnt = Convert.ToInt32(m_Value.ToString());
                for (int j = 0; j < Cnt; j++)
                {
                    CGlobal.GetPrivateProfileString("NameList", "Name" + j.ToString(), "未知", m_Value, 100, m_filePath);
                    CDevModuleData temp = new CDevModuleData();
                    temp.m_DataNo = (j + 1);
                    temp.m_DataName = m_Value.ToString();
                    temp.m_fReadio = 1;
                    temp.m_Offset = 0;
                    temp.m_DataUnit = " ";
                    temp.m_Address = 16385 + j;
                    m_DataModuleList.Add(temp);
                }
            }

            string strSql = "delete from " + "Mod_YcTable";
            m_ModuleDB.ExecSql(strSql);
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = m_ModuleDB.m_Dbconn;
            cmd.Parameters.Add(cmd.CreateParameter());
            OleDbTransaction trans = m_ModuleDB.m_Dbconn.BeginTransaction();
            cmd.Transaction = trans;
            try
            {

                for (int i = 0; i < m_DataModuleList.Count; i++)
                {
                    strSql = "insert into " + "Mod_YcTable" + " values(" +
                        m_DataModuleList[i].m_DataNo.ToString() + ",'" +
                        m_DataModuleList[i].m_DataName + "'," +
                        m_DataModuleList[i].m_fReadio.ToString("F5") + "," +
                        m_DataModuleList[i].m_Offset.ToString("F5") + ",'" +
                        m_DataModuleList[i].m_DataUnit + "'," +
                        m_DataModuleList[i].m_Address.ToString() + ")";
                    cmd.CommandText = strSql;
                    cmd.Parameters[0].Value = i.ToString();
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                throw;

            }
        }
        private void SaveYmData(string strTableName, CDataBase m_ModuleDB)
        {
            List<CDevModuleData> m_DataModuleList = new List<CDevModuleData>();
            CDevYcGroup m_DevYc = new CDevYcGroup();
            int Cnt = 0;
            List<string> m_DataName = new List<string>();
            string m_filePath = m_Station.m_StationPath + "\\" + strTableName + ".ini";
            StringBuilder m_Value = new StringBuilder(100);
            if (File.Exists(m_filePath) == true)
            {
                CGlobal.GetPrivateProfileString("DataCount", "Count", "0", m_Value, 100, m_filePath);
                Cnt = Convert.ToInt32(m_Value.ToString());
                for (int j = 0; j < Cnt; j++)
                {
                    CGlobal.GetPrivateProfileString("NameList", "Name" + j.ToString(), "未知", m_Value, 100, m_filePath);
                    CDevModuleData temp = new CDevModuleData();
                    temp.m_DataNo = (j + 1);
                    temp.m_DataName = m_Value.ToString();                  
                    temp.m_DataUnit = " ";
                    temp.m_Address = 25601 + j;
                    m_DataModuleList.Add(temp);
                }
            }

            string strSql = "delete from " + "Mod_PowerTable";
            m_ModuleDB.ExecSql(strSql);
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = m_ModuleDB.m_Dbconn;
            cmd.Parameters.Add(cmd.CreateParameter());
            OleDbTransaction trans = m_ModuleDB.m_Dbconn.BeginTransaction();
            cmd.Transaction = trans;
            try
            {

                for (int i = 0; i < m_DataModuleList.Count; i++)
                {
                    strSql = "insert into " + "Mod_PowerTable" + " values(" +
                        m_DataModuleList[i].m_DataNo.ToString() + ",'" +
                        m_DataModuleList[i].m_DataName + "','" +
                        m_DataModuleList[i].m_DataUnit + "'," +
                        m_DataModuleList[i].m_Address.ToString() + ")";
                    cmd.CommandText = strSql;
                    cmd.Parameters[0].Value = i.ToString();
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                throw;
            }
        }
        private void SaveYkData(string strTableName, CDataBase m_ModuleDB)
        {
            List<CDevModuleData> m_DataModuleList = new List<CDevModuleData>();
            CDevYcGroup m_DevYc = new CDevYcGroup();
            int Cnt = 0;
            List<string> m_DataName = new List<string>();
            string m_filePath = m_Station.m_StationPath + "\\" + strTableName + ".ini";
            StringBuilder m_Value = new StringBuilder(100);
            if (File.Exists(m_filePath) == true)
            {
                CGlobal.GetPrivateProfileString("DataCount", "Count", "0", m_Value, 100, m_filePath);
                Cnt = Convert.ToInt32(m_Value.ToString());
                for (int j = 0; j < Cnt; j++)
                {
                    CGlobal.GetPrivateProfileString("NameList", "Name" + j.ToString(), "未知", m_Value, 100, m_filePath);
                    CDevModuleData temp = new CDevModuleData();
                    temp.m_DataNo = (j + 1);
                    temp.m_DataName = m_Value.ToString();
                    temp.m_Address = 24577 + j;
                    m_DataModuleList.Add(temp);
                }
            }

            string strSql = "delete from " + "Mod_YkTable";
            m_ModuleDB.ExecSql(strSql);
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = m_ModuleDB.m_Dbconn;
            cmd.Parameters.Add(cmd.CreateParameter());
            OleDbTransaction trans = m_ModuleDB.m_Dbconn.BeginTransaction();
            cmd.Transaction = trans;
            try
            {

                for (int i = 0; i < m_DataModuleList.Count; i++)
                {
                    strSql = "insert into " + "Mod_YkTable" + " values(" +
                        m_DataModuleList[i].m_DataNo.ToString() + ",'" +
                        m_DataModuleList[i].m_DataName + "'," +
                        m_DataModuleList[i].m_Address.ToString() + ")";
                    cmd.CommandText = strSql;
                    cmd.Parameters[0].Value = i.ToString();
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                throw;

            }
        }
       

    }
}
