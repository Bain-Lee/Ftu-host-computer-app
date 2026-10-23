using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TerminalWorkbench.Presentation;
using System.Collections;
using System.Data;
using Microsoft.VisualBasic;

namespace DtuTestManager
{
    public class CTransStation : CTranValueList
    {
        public string m_StationName;
        public string m_StationPath;
        public TreeNode m_Nodes;
        public string m_Sqlite_Main;
        public string m_Sqlite_User;
        public CSQLite m_MainSqlite = new CSQLite();
        public CSQLite m_UserSqlite = new CSQLite();
        public CTermRoot m_Term = new CTermRoot();
        public CPriRoot m_Pri = new CPriRoot();
        public CTransSubStation m_Sub = new CTransSubStation();
        public List<List<CRouteTable>> m_PriRoute = new List<List<CRouteTable>>();
        public List<CPriTerm> L_PriTerm = new List<CPriTerm>();   //所有终端的数据
        public List<Dictionary<int, int>> m_PriNoList = new List<Dictionary<int, int>>();
        public List<string> StrListYxRouteTable = new List<string>();
        public List<string> StrListYcRouteTable = new List<string>();
        public List<string> StrListYkRouteTable = new List<string>();
        public List<string> StrListSetRouteTable = new List<string>();
        public List<string> StrListPowerRouteTable = new List<string>();
        public CTransCalcYxCfg m_CalcYxTable = new CTransCalcYxCfg();     //遥信计算表
        public CTransYxDB m_YxTable = new CTransYxDB();
        public CTransYcDB m_YcTable = new CTransYcDB();
        public CTransYkDB m_YkTable = new CTransYkDB();
        public CTransPowerDB m_PowerTable = new CTransPowerDB();
        public CTransSetDB m_SetTable = new CTransSetDB();
        public CPriTerm m_SubTerm = new CPriTerm();
        public int TemYxSumCnt = 0;
        public int SubYxCnt = 0;
        public bool m_IsEmpty = true;
        public CTransStation()
        {
            m_Term.m_TheStation = this;
            m_Pri.m_TheStation = this;
            m_Sub.m_TheStation = this;

            for (int i = 0; i < 5; i++)
            {
                Dictionary<int, int> temp = new Dictionary<int, int>();
                m_PriNoList.Add(temp);
            }
            for (int i = 0; i < 5; i++)
            {
                List<CRouteTable> temp = new List<CRouteTable>();
                m_PriRoute.Add(temp);
            }

        }
        public void IniSubStationDataList(int nNode)
        {
            string filepath = m_StationPath + "\\" + "公共配置.ini";
            int i = 0;

            switch (nNode)
            {
                case 0:
                    m_YxTable.m_TableName = "SubYxDB";
                    m_YxTable.m_SqlDataBase = m_MainSqlite;
                    m_YxTable.IniReadTableData();
                    if (m_Sub.m_TermDataValue[0].m_isReadDataName == false)
                    {
                        m_Sub.ReadDataINIFile(0);
                        m_SubTerm.m_Data[0].m_DataName.Clear();
                        for (i = 0; i < m_Sub.m_TermDataValue[0].m_DataName.Count; i++)
                        {
                            CPriTerminal temp = new CPriTerminal();
                            temp.Name = m_Sub.m_TermDataValue[0].m_DataName[i];
                            m_SubTerm.m_Data[0].m_DataName.Add(temp);
                        }
                    }
                    m_CalcYxTable.pData = m_ValueList[20];
                    m_CalcYxTable.m_TableName = (string)m_CalcYxTable.pData.m_TranValue;
                    m_CalcYxTable.m_SqlDataBase = m_MainSqlite;
                    m_CalcYxTable.IniReadTableData();
                    if (m_Sub.m_TermDataValue[5].m_isReadDataName == false)
                    {
                        m_Sub.ReadDataINIFile(5);
                        m_SubTerm.m_Data[5].m_DataName.Clear();
                        for (i = 0; i < m_Sub.m_TermDataValue[5].m_DataName.Count; i++)
                        {
                            CPriTerminal temp = new CPriTerminal();
                            temp.Name = m_Sub.m_TermDataValue[5].m_DataName[i];
                            m_SubTerm.m_Data[5].m_DataName.Add(temp);
                        }
                    }
                    break;
                case 1:
                    m_YcTable.m_TableName = "SubYcDB";
                    m_YcTable.m_SqlDataBase = m_MainSqlite;
                    m_YcTable.IniReadTableData();
                    if (m_Sub.m_TermDataValue[1].m_isReadDataName == false)
                    {
                        m_Sub.ReadDataINIFile(1);
                        m_SubTerm.m_Data[1].m_DataName.Clear();
                        for (i = 0; i < m_Sub.m_TermDataValue[1].m_DataName.Count; i++)
                        {
                            CPriTerminal temp = new CPriTerminal();
                            temp.Name = m_Sub.m_TermDataValue[1].m_DataName[i];
                            m_SubTerm.m_Data[1].m_DataName.Add(temp);
                        }
                    }
                    break;
                case 2:
                    m_YkTable.m_TableName = "SubYkDB";
                    m_YkTable.m_SqlDataBase = m_MainSqlite;
                    m_YkTable.IniReadTableData();
                    if (m_Sub.m_TermDataValue[2].m_isReadDataName == false)
                    {
                        m_Sub.ReadDataINIFile(2);
                        m_SubTerm.m_Data[2].m_DataName.Clear();
                        for (i = 0; i < m_Sub.m_TermDataValue[2].m_DataName.Count; i++)
                        {
                            CPriTerminal temp = new CPriTerminal();
                            temp.Name = m_Sub.m_TermDataValue[2].m_DataName[i];
                            m_SubTerm.m_Data[2].m_DataName.Add(temp);
                        }
                    }
                    break;
                case 3:
                    m_SetTable.m_TableName = "SubSetDB";
                    m_SetTable.m_SqlDataBase = m_MainSqlite;
                    m_SetTable.IniReadTableData();
                    if (m_Sub.m_TermDataValue[3].m_isReadDataName == false)
                    {
                        m_Sub.ReadDataINIFile(3);
                        m_SubTerm.m_Data[3].m_DataName.Clear();
                        for (i = 0; i < m_Sub.m_TermDataValue[3].m_DataName.Count; i++)
                        {
                            CPriTerminal temp = new CPriTerminal();
                            temp.Name = m_Sub.m_TermDataValue[3].m_DataName[i];
                            m_SubTerm.m_Data[3].m_DataName.Add(temp);
                        }
                    }
                    break;
                case 4:
                    m_PowerTable.m_TableName = "SubPowerDB";
                    m_PowerTable.m_SqlDataBase = m_MainSqlite;
                    m_PowerTable.IniReadTableData();
                    if (m_Sub.m_TermDataValue[4].m_isReadDataName == false)
                    {
                        m_Sub.ReadDataINIFile(4);
                        m_SubTerm.m_Data[4].m_DataName.Clear();
                        for (i = 0; i < m_Sub.m_TermDataValue[4].m_DataName.Count; i++)
                        {
                            CPriTerminal temp = new CPriTerminal();
                            temp.Name = m_Sub.m_TermDataValue[4].m_DataName[i];
                            m_SubTerm.m_Data[4].m_DataName.Add(temp);
                        }
                    }
                    break;
            }
        }
        private TreeNode GetRouteNode(int nNodeNo, string strNode)
        {
            TreeNode pNode = new TreeNode();
            pNode.Text = strNode;
            pNode.Tag = nNodeNo;
            return pNode;

        }
        private void AddStationNode(int nNodeNo, string strNode)
        {
            TreeNode pNode = new TreeNode();
            pNode.Text = strNode;
            pNode.Tag = nNodeNo;

            m_Nodes.Nodes.Add(pNode);
            if (nNodeNo == 3)
            {
                pNode.Nodes.Add(GetRouteNode(0, "遥信转发点表"));
                pNode.Nodes.Add(GetRouteNode(1, "遥测转发点表"));
                pNode.Nodes.Add(GetRouteNode(2, "遥控转发点表"));
                pNode.Nodes.Add(GetRouteNode(3, "定值转发点表"));
                pNode.Nodes.Add(GetRouteNode(4, "遥脉转发点表"));
            }
        }
        public void CreateTreeNode()
        {
            m_Nodes = new TreeNode();
            m_Nodes.Text = m_StationName;
            m_Nodes.Tag = this;

            AddStationNode(1, "装置配置");
            AddStationNode(2, "主站配置");
            AddStationNode(3, "转发点表");


        }
        private void GetSubInfo(int nNode, int nIndex)
        {
            if (nNode < 5)
            {
                m_Sub.m_TermDataValue[nNode].nRealCnt = Convert.ToInt32(m_ValueList[nIndex].m_TranValue);
                for (int j = 0; j < CGlobal.m_PriTerm_Sub.m_Data[nNode].m_DataName.Count; j++)
                {
                    string name = CGlobal.m_PriTerm_Sub.m_Data[nNode].m_DataName[j].Name;
                    m_Sub.m_TermDataValue[nNode].m_DataName.Add(name);
                }
                if (m_Sub.m_TermDataValue[3].nRealCnt > CGlobal.m_PriTerm_Sub.m_Data[3].m_DataName.Count)
                {
                    for (int j = CGlobal.m_PriTerm_Sub.m_Data[3].m_DataName.Count; j < m_Sub.m_TermDataValue[3].nRealCnt; j++)
                    {
                        string name = "备用";
                        m_Sub.m_TermDataValue[3].m_DataName.Add(name);
                    }
                }
            }
            else
            {
                CTransCalcYxCfg m_CalcYxTable = new CTransCalcYxCfg();     //遥信计算表
                m_CalcYxTable.pData = m_ValueList[nIndex];
                m_CalcYxTable.m_TableName = (string)m_CalcYxTable.pData.m_TranValue;
                m_CalcYxTable.m_SqlDataBase = m_MainSqlite;
                m_CalcYxTable.IniReadTableData();
                m_Sub.m_TermDataValue[nNode].nRealCnt = m_CalcYxTable.GetRowCount();
                for (int j = 0; j < CGlobal.m_PriTerm_Sub.m_Data[nNode].m_DataName.Count; j++)
                {
                    string name = CGlobal.m_PriTerm_Sub.m_Data[nNode].m_DataName[j].Name;
                    m_Sub.m_TermDataValue[nNode].m_DataName.Add(name);
                }
            }
        }
        public void IniReadStationTable()
        {
            string strSql;
            DataTable pTable;
            int i;
            strSql = "Select * from SubStationNodeCfg";
            pTable = (DataTable)m_MainSqlite.SelectDataTable(strSql);
            if (pTable == null)
                return;
            for (i = 0; i < m_ValueList.Count; i++)
            {
                m_ValueList[i].m_TranValue = m_MainSqlite.GetTableString(pTable, 0, i);
            }

            GetSubInfo(1, 7);
            GetSubInfo(2, 8);
            GetSubInfo(3, 9);
            GetSubInfo(4, 10);
            GetSubInfo(0, 6);
            GetSubInfo(5, 20);
        }
        public void IniValueList()
        {
            string[] strBoolList = { "TRUE", "FALSE" };
            string[] strDogList = { "WatchDogNull", "WatchDog_AM335X" };
            string[] strFileList = { "NoLog", "XmlLog", "TextLog", "SQLLog" };
            string[] strEsamList = { "NULL", "ESAM" };
            m_ValueList.Clear();
            AddNewValue(0, "ID", "自动序号", 0);
            AddNewValue(2, "SynImm", "是否触发对时", 1, 30, strBoolList);
            AddNewValue(2, "CallImm", "是否触发总召", 1, 30, strBoolList);
            AddNewValue(2, "EnableBattery", "电池活化配置", 1, 50);
            AddNewValue(2, "EnableWatchDog", "看门狗配置", 1, 50, strDogList);
            AddNewValue(2, "VirtualDIO", "本地IO口配置", 1, 50);
            AddNewValue(0, "YxDB", "本地遥信数量", 2);
            AddNewValue(0, "YcDB", "本地遥测数量", 2);
            AddNewValue(0, "YkDB", "本地遥控数量", 2);
            AddNewValue(0, "SetDB", "本地参数数量", 2);
            AddNewValue(0, "PowerDB", "本地电量数量", 2);
            AddNewValue(0, "Address", "本地地址", 1);
            AddNewValue(0, "PriStationNum", "转发通道数量", 2);
            AddNewValue(0, "TerminalNum", "设备配置数量", 2);
            AddNewValue(2, "LoadSubCfgLogName", "厂站记录文件", 1, 50);
            AddNewValue(2, "LoadSubCfgLogType", "厂站记录类型", 1, 50, strFileList);
            AddNewValue(2, "LoadSubCfgLogLimit", "厂站记录限制数量", 1, 50);
            AddNewValue(2, "InitCommLogName", "通讯记录名称", 1, 50);
            AddNewValue(2, "InitCommLogType", "通讯记录类型", 1, 50, strFileList);
            AddNewValue(2, "InitCommLogLimit", "通讯记录限制数量", 1, 50);
            AddNewValue(2, "CalcYxCfg", "遥信计算表名称", 1, 50);
            AddNewValue(2, "HardWareKeyCfg", "加密方式", 1, 50, strEsamList);
            AddNewValue(2, "HardWareKeySpi", "加密设备", 1, 50);

            AddNewValue(2, "HardWareKeyCs", "加密芯片片选", 1, 100, strEsamList);
            AddNewValue(2, "HardWareKeyPw", "加密芯片电源", 1, 100);
            AddNewValue(2, "HardWareAntiSpi", "认证芯片SPI设备名", 1, 50, strEsamList);
            AddNewValue(2, "HardWareAntiCs", "认证芯片片选", 1, 100);
            AddNewValue(2, "HardWareAntiPw", "认证芯片电源", 1, 100, strEsamList);

            AddNewValue(2, "SoeRecord", "SOE文件名称", 3, 50);
            AddNewValue(0, "SoeLimit", "记录数量", 3);
            AddNewValue(2, "CosRecord", "COS文件名称", 3, 50);
            AddNewValue(0, "CosLimit", "记录数量", 3);
            AddNewValue(2, "YkRecord", "遥控文件名称", 3, 50);
            AddNewValue(0, "YkLimit", "记录数量", 3);
            AddNewValue(2, "ExvRecord", "极值文件名称", 3, 50);
            AddNewValue(0, "ExvLimit", "记录数量", 3);
            AddNewValue(2, "FixptRecord", "定点文件名称", 3, 50);
            AddNewValue(0, "FixptLimit", "记录数量", 3);
            AddNewValue(2, "UlogRecord", "日志文件名称", 3, 50);
            AddNewValue(0, "UlogLimit", "记录数量", 3);
            AddNewValue(2, "FrzRecord", "日冻结文件名称", 3, 50);
            AddNewValue(0, "FrzLimit", "记录数量", 3);
            AddNewValue(2, "FlowrevRecord", "功率反向电能量文件名称", 3, 50);
            AddNewValue(0, "FlowrevLimit", "记录数量", 3);
            AddNewValue(0, "UseSetZone", "UseSetZone", 1);

            // IniReadStationTable();  //初始化时先不读数据
        }
        public bool IniSetAndConnSqlite()
        {
            m_Sqlite_Main = m_StationPath + "\\MainCfgBase.db";
            m_Sqlite_User = m_StationPath + "\\UserCfgBase.db";
            if (File.Exists(m_Sqlite_Main) == false)
                return false;
            if (File.Exists(m_Sqlite_User) == false)
                return false;

            if (m_MainSqlite.ConnectDataBase(m_Sqlite_Main) == false)
                return false;
            if (m_UserSqlite.ConnectDataBase(m_Sqlite_User) == false)
                return false;

            IniValueList();
            IniReadStationTable();
            return true;
        }
        public void VisitAllPriTable()
        {
            StrListYxRouteTable.Add("NULL");
            StrListYcRouteTable.Add("NULL");
            StrListYkRouteTable.Add("NULL");
            StrListSetRouteTable.Add("NULL");
            StrListPowerRouteTable.Add("NULL");

            DataTable dt = m_UserSqlite.GetTableName();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string tablename = dt.Rows[i][2].ToString();

                if ((tablename.Length > 8) && (tablename.Substring(0, 8) == "PriYxTab"))
                {
                    StrListYxRouteTable.Add(tablename);
                }
                if ((tablename.Length > 8) && (tablename.Substring(0, 8) == "PriYcTab"))
                {
                    StrListYcRouteTable.Add(tablename);
                }

                if ((tablename.Length > 8) && (tablename.Substring(0, 8) == "PriYkTab"))
                {
                    StrListYkRouteTable.Add(tablename);
                }

                if ((tablename.Length > 11) && (tablename.Substring(0, 11) == "PriPowerTab"))
                {
                    StrListPowerRouteTable.Add(tablename);
                }

                if ((tablename.Length > 9) && (tablename.Substring(0, 9) == "PriSetTab"))
                {
                    StrListSetRouteTable.Add(tablename);
                }
            }
        }
        public void UpdateTerminalNum(int num)
        {
            string strSql = "update " + "SubStationNodeCfg" + " set TerminalNum=" + num.ToString() + " where(ID=1)";
            m_MainSqlite.ExecSql(strSql);
        }
        public void UpdatePriNum(int num)
        {
            string strSql = "update " + "SubStationNodeCfg" + " set PriStationNum=" + num.ToString() + " where(ID=1)";
            m_MainSqlite.ExecSql(strSql);
        }
    }
}
