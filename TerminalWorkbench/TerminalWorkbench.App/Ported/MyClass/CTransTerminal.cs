using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using TerminalWorkbench.Presentation;
using Microsoft.VisualBasic;
using System.IO;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using System.Threading;


namespace DtuTestManager
{
    [Serializable]
    public class CTermData
    {
        public int nRealCnt = 0; //终端真实的Yx、Yc等个数
        public List<string> m_DataName = new List<string>(); //终端对应模板的全部名称
        public bool m_isReadDataName = false;
    }
    public class CDeviceConfig : CTranValueList
    {
        public CTransStation m_TheStation = null;
        public CTermRoot m_TermRoot = new CTermRoot();
        public TreeNode m_Node = null;
        public int m_TermNo = 0;
        public string m_TermName; //终端名称
        public List<CTermData> m_TermDataValue = new List<CTermData>();//五种数据的名称
        public string m_YcTableName;
        public string m_YxTableName;
        public string m_YkTableName;
        public string m_SetTableName;
        public string m_PowerTableName;
        public string m_YcRouteTableName;
        public string m_YxRouteTableName;
        public string m_YkRouteTableName;
        public string m_SetRouteTableName;
        public string m_PowerRouteTableName;
        public CDeviceConfig()
        {
            for (int i = 0; i < 5; i++)
            {
                CTermData temp = new CTermData();
                m_TermDataValue.Add(temp);
            }
            m_TheStation = m_TermRoot.m_TheStation;
        }
        public void IniAllDataTableName()
        {
            m_YcTableName = "YcDB" + (string)(m_ValueList[1].m_TranValue);
            m_YxTableName = "YxDB" + (string)(m_ValueList[1].m_TranValue);
            m_YkTableName = "YkDB" + (string)(m_ValueList[1].m_TranValue);
            m_SetTableName = "SetDB" + (string)(m_ValueList[1].m_TranValue);
            m_PowerTableName = "PowerDB" + (string)(m_ValueList[1].m_TranValue);


            m_YcRouteTableName = "TerYcTab" + (string)(m_ValueList[1].m_TranValue);
            m_YxRouteTableName = "TerYxTab" + (string)(m_ValueList[1].m_TranValue);
            m_YkRouteTableName = "TerYkTab" + (string)(m_ValueList[1].m_TranValue);
            m_SetRouteTableName = "TerSetTab" + (string)(m_ValueList[1].m_TranValue);
            m_PowerRouteTableName = "TerPowerTab" + (string)(m_ValueList[1].m_TranValue);
        }
        public void CreateNewTable()
        {
            string strSql;
            DelAllTable();
            //创建遥测表
            strSql = "CREATE TABLE " + m_YcTableName + "(ID INTEGER, DataNo INTEGER,YcType  INTEGER,Freeze INTEGER,YcDeadLimit INTEGER, FloatYcDeadLimit REAL,YcMul REAL,YcPlus INTEGER,YcFloatPlus REAL)";
            m_TermRoot.m_TheStation.m_MainSqlite.ExecSql(strSql);
            //创建遥信表
            strSql = "CREATE TABLE " + m_YxTableName + "(ID INTEGER, DataNo INTEGER,Freeze INTEGER,YxPolar INTEGER,YxType INTEGER,CalcType VARCHAR(50),LeftIndex INTEGER,RightIndex INTEGER,EnableEvent VARCHAR(30))";
            m_TermRoot.m_TheStation.m_MainSqlite.ExecSql(strSql);
            //创建遥控表
            strSql = "CREATE TABLE " + m_YkTableName + "(ID INTEGER, DataNo INTEGER,YkSType INTEGER,YkHType INTEGER,YkAllow INTEGER)";
            m_TermRoot.m_TheStation.m_MainSqlite.ExecSql(strSql);
            //创建电量表
            strSql = "CREATE TABLE " + m_PowerTableName + "(ID INTEGER, DataNo INTEGER,PowerType  INTEGER,Freeze INTEGER,PowerDeadLimit INTEGER, FloatPowerDeadLimit REAL,PowerMul REAL,PowerPlus INTEGER,PowerFloatPlus REAL)";
            m_TermRoot.m_TheStation.m_MainSqlite.ExecSql(strSql);
            //创建参数根目录表
            strSql = "CREATE TABLE " + m_SetTableName + "(ID INTEGER, DataNo INTEGER,SetVal0 VARCHAR(30),SetVal1 VARCHAR(30),ValType INTEGER)";
            m_TermRoot.m_TheStation.m_MainSqlite.ExecSql(strSql);

            strSql = "CREATE TABLE " + m_YcRouteTableName + "(ID INTEGER, DstNode INTEGER,SrcNode  INTEGER)";
            m_TermRoot.m_TheStation.m_UserSqlite.ExecSql(strSql);

            strSql = "CREATE TABLE " + m_YxRouteTableName + "(ID INTEGER, DstNode INTEGER,SrcNode  INTEGER)";
            m_TermRoot.m_TheStation.m_UserSqlite.ExecSql(strSql);

            strSql = "CREATE TABLE " + m_SetRouteTableName + "(ID INTEGER, DstNode INTEGER,SrcNode  INTEGER)";
            m_TermRoot.m_TheStation.m_UserSqlite.ExecSql(strSql);

            strSql = "CREATE TABLE " + m_YkRouteTableName + "(ID INTEGER, DstNode INTEGER,SrcNode  INTEGER)";
            m_TermRoot.m_TheStation.m_UserSqlite.ExecSql(strSql);

            strSql = "CREATE TABLE " + m_PowerRouteTableName + "(ID INTEGER, DstNode INTEGER,SrcNode  INTEGER)";
            m_TermRoot.m_TheStation.m_UserSqlite.ExecSql(strSql);

        }
        public void ClearRouteTableData(int NodeNo)
        {
            string strSql;
            try
            {
                switch (NodeNo)
                {
                    case 0:
                        strSql = "delete  from " + m_YxRouteTableName;
                        m_TermRoot.m_TheStation.m_UserSqlite.ExecSql(strSql);
                        break;
                    case 1:
                        strSql = "delete  from " + m_YcRouteTableName;
                        m_TermRoot.m_TheStation.m_UserSqlite.ExecSql(strSql);
                        break;
                    case 2:
                        strSql = "delete  from " + m_YkRouteTableName;
                        m_TermRoot.m_TheStation.m_UserSqlite.ExecSql(strSql);
                        break;
                    case 3:
                        strSql = "delete  from " + m_SetRouteTableName;
                        m_TermRoot.m_TheStation.m_UserSqlite.ExecSql(strSql);
                        break;
                    case 4:
                        strSql = "delete  from " + m_PowerRouteTableName;
                        m_TermRoot.m_TheStation.m_UserSqlite.ExecSql(strSql);
                        break;
                }

            }
            catch
            {

            }
        }
        public void CreatRouteTableData(int NodeNo)
        {
            int i;
            string strSql;
            using DbCommand cmd = m_TermRoot.m_TheStation.m_UserSqlite.m_TranConn.CreateCommand();

            using DbTransaction trans = m_TermRoot.m_TheStation.m_UserSqlite.m_TranConn.BeginTransaction();
            cmd.Transaction = trans;
            try
            {
                switch (NodeNo)
                {
                    case 0:

                        for (i = 0; i < m_TermDataValue[0].nRealCnt; i++)
                        {
                            strSql = "insert into " + m_YxRouteTableName + " values(";

                            strSql += (i + 1).ToString() + "," + i.ToString() + "," + i.ToString();

                            strSql += ")";
                            cmd.CommandText = strSql;

                            cmd.ExecuteNonQuery();
                        }
                        break;
                    case 1:

                        for (i = 0; i < m_TermDataValue[1].nRealCnt; i++)
                        {
                            strSql = "insert into " + m_YcRouteTableName + " values(";

                            strSql += (i + 1).ToString() + "," + i.ToString() + "," + i.ToString();

                            strSql += ")";

                            cmd.CommandText = strSql;

                            cmd.ExecuteNonQuery();
                        }
                        break;
                    case 2:

                        for (i = 0; i < m_TermDataValue[2].nRealCnt; i++)
                        {
                            strSql = "insert into " + m_YkRouteTableName + " values(";

                            strSql += (i + 1).ToString() + "," + i.ToString() + "," + i.ToString();

                            strSql += ")";

                            cmd.CommandText = strSql;

                            cmd.ExecuteNonQuery();
                        }
                        break;
                    case 3:

                        for (i = 0; i < m_TermDataValue[3].nRealCnt; i++)
                        {
                            strSql = "insert into " + m_SetRouteTableName + " values(";

                            strSql += (i + 1).ToString() + "," + i.ToString() + "," + i.ToString();

                            strSql += ")";

                            cmd.CommandText = strSql;

                            cmd.ExecuteNonQuery();
                        }
                        break;
                    case 4:

                        for (i = 0; i < m_TermDataValue[4].nRealCnt; i++)
                        {
                            strSql = "insert into " + m_PowerRouteTableName + " values(";
                            strSql += (i + 1).ToString() + "," + i.ToString() + "," + i.ToString();
                            strSql += ")";
                            cmd.CommandText = strSql;

                            cmd.ExecuteNonQuery();
                        }
                        break;
                }
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                throw;
            }
        }
        public void InitDBTableData()
        {
            int i;
            string strSql;
            using DbCommand cmd = m_TermRoot.m_TheStation.m_MainSqlite.m_TranConn.CreateCommand();

            using DbTransaction trans = m_TermRoot.m_TheStation.m_MainSqlite.m_TranConn.BeginTransaction();
            cmd.Transaction = trans;
            try
            {
                for (i = 0; i < m_TermDataValue[0].nRealCnt; i++)
                {
                    strSql = "insert into " + m_YxTableName + " values(";

                    strSql += (i + 1).ToString() + "," + i.ToString() + "," + "0,1,0,'DoubleByBit',0,1,'TRUE'";

                    strSql += ")";
                    cmd.CommandText = strSql;

                    cmd.ExecuteNonQuery();
                }
                for (i = 0; i < m_TermDataValue[1].nRealCnt; i++)
                {
                    strSql = "insert into " + m_YcTableName + " values(";

                    strSql += (i + 1).ToString() + "," + i.ToString() + "," + "2,0,20,0,1,0,0";

                    strSql += ")";

                    cmd.CommandText = strSql;

                    cmd.ExecuteNonQuery();
                }

                for (i = 0; i < m_TermDataValue[2].nRealCnt; i++)
                {
                    strSql = "insert into " + m_YkTableName + " values(";

                    strSql += (i + 1).ToString() + "," + i.ToString() + "," + "0,0,1";

                    strSql += ")";

                    cmd.CommandText = strSql;

                    cmd.ExecuteNonQuery();
                }

                for (i = 0; i < m_TermDataValue[3].nRealCnt; i++)
                {
                    strSql = "insert into " + m_SetTableName + " values(";

                    strSql += (i + 1).ToString() + "," + i.ToString() + "," + "0,0,38";

                    strSql += ")";

                    cmd.CommandText = strSql;

                    cmd.ExecuteNonQuery();
                }
                for (i = 0; i < m_TermDataValue[4].nRealCnt; i++)
                {
                    strSql = "insert into " + m_PowerTableName + " values(";

                    strSql += (i + 1).ToString() + "," + i.ToString() + "," + "2,0,20,0.2,1,0,0";

                    strSql += ")";
                    cmd.CommandText = strSql;

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
        public void ReadDataFromINIFile(string m_NodeType)
        {
            for (int i = 0; i < 5; i++)
            {
                ReadDataINIFile(i, m_NodeType);
            }
        }
        public void UpdateNIFileContent(string m_NodeType)
        {
            string FileName2 = m_TermRoot.m_TheStation.m_StationPath + "\\" + m_TermName + ".ini";
            int pYxCount, pYcCount, pYkCount, pParaCount, pYmCount;
            for (int j = 0; j < CGlobal.m_PriTerm.Count; j++)
            {
                if (m_NodeType == CGlobal.m_PriTerm[j].m_ModulName)
                {
                    pYxCount = CGlobal.m_PriTerm[j].m_Data[0].m_DataName.Count;
                    pYcCount = CGlobal.m_PriTerm[j].m_Data[1].m_DataName.Count;
                    pYkCount = CGlobal.m_PriTerm[j].m_Data[2].m_DataName.Count;
                    pParaCount = CGlobal.m_PriTerm[j].m_Data[3].m_DataName.Count;
                    pYmCount = CGlobal.m_PriTerm[j].m_Data[4].m_DataName.Count;
                    int[] nPCount = new int[5];
                    nPCount[0] = pYxCount;
                    nPCount[1] = pYcCount;
                    nPCount[2] = pYkCount;
                    nPCount[3] = pParaCount;
                    nPCount[4] = pYmCount;

                    for (int n = 0; n < 5; n++)
                    {
                        m_TermDataValue[n].m_DataName.Clear();
                        for (int i = 0; i < nPCount[n]; i++)
                        {
                            string m_name = CGlobal.m_PriTerm[j].m_Data[n].m_DataName[i].Name;
                            m_TermDataValue[n].m_DataName.Add(m_name);
                        }
                    }
                    break;
                }
            }
            WriteINIFile();

        }
        public void CreateInialINIFile(string m_NodeType)
        {
            string FileName2 = m_TermRoot.m_TheStation.m_StationPath + "\\" + m_TermName + ".ini";
            int pYxCount, pYcCount, pYkCount, pParaCount, pYmCount;
            if (File.Exists(FileName2) == false)
            {
                for (int j = 0; j < CGlobal.m_PriTerm.Count; j++)
                {
                    if (m_NodeType == CGlobal.m_PriTerm[j].m_ModulName)
                    {
                        pYxCount = CGlobal.m_PriTerm[j].m_Data[0].m_DataName.Count;
                        pYcCount = CGlobal.m_PriTerm[j].m_Data[1].m_DataName.Count;
                        pYkCount = CGlobal.m_PriTerm[j].m_Data[2].m_DataName.Count;
                        pParaCount = CGlobal.m_PriTerm[j].m_Data[3].m_DataName.Count;
                        pYmCount = CGlobal.m_PriTerm[j].m_Data[4].m_DataName.Count;
                        int[] nPCount = new int[5];
                        nPCount[0] = pYxCount;
                        nPCount[1] = pYcCount;
                        nPCount[2] = pYkCount;
                        nPCount[3] = pParaCount;
                        nPCount[4] = pYmCount;

                        for (int n = 0; n < 5; n++)
                        {
                            m_TermDataValue[n].m_DataName.Clear();
                            for (int i = 0; i < nPCount[n]; i++)
                            {
                                string m_name = CGlobal.m_PriTerm[j].m_Data[n].m_DataName[i].Name;
                                m_TermDataValue[n].m_DataName.Add(m_name);
                            }
                        }
                        break;
                    }
                }
                WriteINIFile();
            }

        }
        public void ReadDataINIFile(int NodeNo, string m_NodeType)
        {

            string FileName2 = m_TermRoot.m_TheStation.m_StationPath + "\\" + m_TermName + ".ini";
            int Cnt;
            //判断这个db文件里是否按模板配置过
            StringBuilder m_Value = new StringBuilder(100);
            if (File.Exists(FileName2) == false)
            {
                CreateInialINIFile(m_NodeType);
            }
            else
            {
                m_TermDataValue[NodeNo].m_DataName.Clear();
                switch (NodeNo)
                {
                    case 0:
                        CGlobal.GetPrivateProfileString("DataCount", "YxCount", "0", m_Value, 100, FileName2);
                        Cnt = Convert.ToInt32(m_Value.ToString());
                        if (Cnt > 0)
                        {
                            for (int j = 0; j < Cnt; j++)
                            {
                                CGlobal.GetPrivateProfileString("YxNameList", "Name" + j.ToString(), "未知", m_Value, 100, FileName2);
                                string m_name = m_Value.ToString();
                                m_TermDataValue[NodeNo].m_DataName.Add(m_name);
                            }
                        }
                        break;
                    case 1:

                        CGlobal.GetPrivateProfileString("DataCount", "YcCount", "0", m_Value, 100, FileName2);
                        Cnt = Convert.ToInt32(m_Value.ToString());
                        for (int j = 0; j < Cnt; j++)
                        {
                            CGlobal.GetPrivateProfileString("YcNameList", "Name" + j.ToString(), "未知", m_Value, 100, FileName2);
                            string m_name = m_Value.ToString();
                            m_TermDataValue[NodeNo].m_DataName.Add(m_name);
                        }
                        break;
                    case 2:

                        CGlobal.GetPrivateProfileString("DataCount", "YkCount", "0", m_Value, 100, FileName2);
                        Cnt = Convert.ToInt32(m_Value.ToString());
                        for (int j = 0; j < Cnt; j++)
                        {
                            CGlobal.GetPrivateProfileString("YkNameList", "Name" + j.ToString(), "未知", m_Value, 100, FileName2);
                            string m_name = m_Value.ToString();
                            m_TermDataValue[NodeNo].m_DataName.Add(m_name);
                        }
                        break;
                    case 3:
                        CGlobal.GetPrivateProfileString("DataCount", "SetCount", "0", m_Value, 100, FileName2);
                        Cnt = Convert.ToInt32(m_Value.ToString());
                        for (int j = 0; j < Cnt; j++)
                        {
                            CGlobal.GetPrivateProfileString("SetNameList", "Name" + j.ToString(), "未知", m_Value, 100, FileName2);
                            string m_name = m_Value.ToString();
                            m_TermDataValue[NodeNo].m_DataName.Add(m_name);
                        }
                        if (m_TermDataValue[NodeNo].m_DataName.Count == 0)
                        {
                            for (int i = 0; i < CGlobal.m_PreDefauleDevice.m_ParaList.Count; i++)
                            {
                                for (int j = 0; j < CGlobal.m_PreDefauleDevice.m_ParaList[i].m_GroupList.Count; j++)
                                {
                                    if (CGlobal.m_PreDefauleDevice.m_ParaList[i].m_GroupList[j].m_ParaDataList.Count > 0)
                                    {
                                        for (int k = 0; k < CGlobal.m_PreDefauleDevice.m_ParaList[i].m_GroupList[j].m_ParaDataList.Count; k++)
                                        {
                                            CParaCtrl pPara = CGlobal.m_PreDefauleDevice.m_ParaList[i].m_GroupList[j].m_ParaDataList[k];
                                            m_TermDataValue[NodeNo].m_DataName.Add(pPara.m_ParaName);

                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case 4:

                        CGlobal.GetPrivateProfileString("DataCount", "PowerCount", "0", m_Value, 100, FileName2);
                        Cnt = Convert.ToInt32(m_Value.ToString());
                        for (int j = 0; j < Cnt; j++)
                        {
                            CGlobal.GetPrivateProfileString("PowerNameList", "Name" + j.ToString(), "未知", m_Value, 100, FileName2);
                            string m_name = m_Value.ToString();
                            m_TermDataValue[NodeNo].m_DataName.Add(m_name);
                        }
                        break;
                }
            }
            m_TermDataValue[NodeNo].m_isReadDataName = true;
        }
        public void WriteDtaToINIFile(int NodeNo, int Cnt)
        {
            string FileName = m_TermRoot.m_TheStation.m_StationPath + "\\" + m_TermName + ".ini";
            switch (NodeNo)
            {
                case 0:
                    CGlobal.WritePrivateProfileString("DataCount", "YxCount", Cnt.ToString(), FileName);
                    for (int i = 0; i < Cnt; i++)
                    {
                        string m_name = m_TermDataValue[NodeNo].m_DataName[i];
                        CGlobal.WritePrivateProfileString("YxNameList", "Name" + i.ToString(), m_name, FileName);
                    }
                    break;
                case 1:
                    CGlobal.WritePrivateProfileString("DataCount", "YcCount", Cnt.ToString(), FileName);
                    for (int i = 0; i < Cnt; i++)
                    {
                        string m_name = m_TermDataValue[NodeNo].m_DataName[i];
                        CGlobal.WritePrivateProfileString("YcNameList", "Name" + i.ToString(), m_name, FileName);
                    }
                    break;
                case 2:
                    CGlobal.WritePrivateProfileString("DataCount", "YkCount", Cnt.ToString(), FileName);
                    for (int i = 0; i < Cnt; i++)
                    {
                        string m_name = m_TermDataValue[NodeNo].m_DataName[i];
                        CGlobal.WritePrivateProfileString("YkNameList", "Name" + i.ToString(), m_name, FileName);
                    }
                    break;
                case 3:
                    CGlobal.WritePrivateProfileString("DataCount", "SetCount", Cnt.ToString(), FileName);
                    for (int i = 0; i < Cnt; i++)
                    {
                        string m_name = m_TermDataValue[NodeNo].m_DataName[i];
                        CGlobal.WritePrivateProfileString("SetNameList", "Name" + i.ToString(), m_name, FileName);
                    }
                    break;
                case 4:
                    CGlobal.WritePrivateProfileString("DataCount", "PowerCount", Cnt.ToString(), FileName);
                    for (int i = 0; i < Cnt; i++)
                    {
                        string m_name = m_TermDataValue[NodeNo].m_DataName[i];
                        CGlobal.WritePrivateProfileString("PowerNameList", "Name" + i.ToString(), m_name, FileName);
                    }
                    break;
            }
            m_TermDataValue[NodeNo].m_isReadDataName = false;
        }
        public void UpdateINIFileContent(string m_NodeType) //更新模板之后的内容更新
        {






        }
        public void UpdateINIFileCnt(string m_NodeType) //更新yx、yc等点表的个数
        {
            string FileName = m_TermRoot.m_TheStation.m_StationPath + "\\" + m_TermName + ".ini";
            if (File.Exists(FileName) == false)
            {
                CreateInialINIFile(m_NodeType);
            }


            CGlobal.WritePrivateProfileString("DataCount", "YxCount", m_ValueList[25].m_TranValue.ToString(), FileName);
            CGlobal.WritePrivateProfileString("DataCount", "YcCount", m_ValueList[26].m_TranValue.ToString(), FileName);
            CGlobal.WritePrivateProfileString("DataCount", "YkCount", m_ValueList[27].m_TranValue.ToString(), FileName);
            CGlobal.WritePrivateProfileString("DataCount", "SetCount", m_ValueList[28].m_TranValue.ToString(), FileName);
            CGlobal.WritePrivateProfileString("DataCount", "PowerCount", m_ValueList[29].m_TranValue.ToString(), FileName);
        }
        public void WriteINIFile()
        {
            string FileName = m_TermRoot.m_TheStation.m_StationPath + "\\" + m_TermName + ".ini";
            CGlobal.WritePrivateProfileString("DataCount", "YxCount", m_TermDataValue[0].m_DataName.Count.ToString(), FileName);
            CGlobal.WritePrivateProfileString("DataCount", "YcCount", m_TermDataValue[1].m_DataName.Count.ToString(), FileName);
            CGlobal.WritePrivateProfileString("DataCount", "YkCount", m_TermDataValue[2].m_DataName.Count.ToString(), FileName);
            CGlobal.WritePrivateProfileString("DataCount", "SetCount", m_TermDataValue[3].m_DataName.Count.ToString(), FileName);
            CGlobal.WritePrivateProfileString("DataCount", "PowerCount", m_TermDataValue[4].m_DataName.Count.ToString(), FileName);
            for (int i = 0; i < m_TermDataValue[0].m_DataName.Count; i++)
            {
                string m_name = m_TermDataValue[0].m_DataName[i];
                CGlobal.WritePrivateProfileString("YxNameList", "Name" + i.ToString(), m_name, FileName);
            }
            for (int i = 0; i < m_TermDataValue[1].m_DataName.Count; i++)
            {
                string m_name = m_TermDataValue[1].m_DataName[i];
                CGlobal.WritePrivateProfileString("YcNameList", "Name" + i.ToString(), m_name, FileName);
            }
            for (int i = 0; i < m_TermDataValue[2].m_DataName.Count; i++)
            {
                string m_name = m_TermDataValue[2].m_DataName[i];
                CGlobal.WritePrivateProfileString("YkNameList", "Name" + i.ToString(), m_name, FileName);
            }
            for (int i = 0; i < m_TermDataValue[3].m_DataName.Count; i++)
            {
                string m_name = m_TermDataValue[3].m_DataName[i];
                CGlobal.WritePrivateProfileString("SetNameList", "Name" + i.ToString(), m_name, FileName);
            }
            for (int i = 0; i < m_TermDataValue[4].m_DataName.Count; i++)
            {
                string m_name = m_TermDataValue[4].m_DataName[i];
                CGlobal.WritePrivateProfileString("PowerNameList", "Name" + i.ToString(), m_name, FileName);
            }

        }
        private void CopyTable(string DestTable, string SourceTable)
        {
            string dbstr = " Insert into  " + DestTable + " select * from " + SourceTable;
            m_TermRoot.m_TheStation.m_MainSqlite.ExecSql(dbstr);
        }
        private void CopyTable1(string DestTable, string SourceTable)
        {
            string dbstr = " Insert into  " + DestTable + " select * from " + SourceTable;
            m_TermRoot.m_TheStation.m_UserSqlite.ExecSql(dbstr);
        }
        public void CopyTableData(CDeviceConfig pBasicDev)
        {
            CopyTable(m_YcTableName, pBasicDev.m_YcTableName);
            CopyTable(m_YxTableName, pBasicDev.m_YxTableName);
            CopyTable(m_YkTableName, pBasicDev.m_YkTableName);
            CopyTable(m_PowerTableName, pBasicDev.m_PowerTableName);
            CopyTable(m_SetTableName, pBasicDev.m_SetTableName);

            CopyTable1(m_YcRouteTableName, pBasicDev.m_YcRouteTableName);
            CopyTable1(m_YxRouteTableName, pBasicDev.m_YxRouteTableName);
            CopyTable1(m_YkRouteTableName, pBasicDev.m_YkRouteTableName);
            CopyTable1(m_PowerRouteTableName, pBasicDev.m_PowerRouteTableName);
            CopyTable1(m_SetRouteTableName, pBasicDev.m_SetRouteTableName);
        }
        private void DeleteTable(string tableName)//删除表格，释放空间
        {
            try
            {
                string strSql = "drop table " + tableName;
                m_TermRoot.m_TheStation.m_MainSqlite.ExecSql(strSql);
            }
            catch
            {
            }
        }
        private void DeleteTable1(string tableName)//删除表格，释放空间
        {
            try
            {
                string strSql = "drop table " + tableName;
                m_TermRoot.m_TheStation.m_UserSqlite.ExecSql(strSql);
            }
            catch
            {
            }
        }
        public void ReNameTable(string oldname, string newname)//更改表名
        {
            string strSql = "alter table " + oldname + " rename to " + newname;
            m_TermRoot.m_TheStation.m_MainSqlite.ExecSql(strSql);
        }
        public void ReNameTable1(string oldname, string newname)//更改表名
        {
            string strSql = "alter table " + oldname + " rename to " + newname;
            m_TermRoot.m_TheStation.m_UserSqlite.ExecSql(strSql);
        }
        public void DelAllTable()
        {
            DeleteTable(m_YcTableName);
            DeleteTable(m_YxTableName);
            DeleteTable(m_YkTableName);
            DeleteTable(m_PowerTableName);
            DeleteTable(m_SetTableName);

            DeleteTable1(m_YcRouteTableName);
            DeleteTable1(m_YxRouteTableName);
            DeleteTable1(m_YkRouteTableName);
            DeleteTable1(m_PowerRouteTableName);
            DeleteTable1(m_SetRouteTableName);
        }
        public void DropTermTable()
        {
            DelAllTable();
            string strSql;
            strSql = "delete from TerminalNodeCfg where(NodeNo=" + (string)(m_ValueList[1].m_TranValue) + ")";
            m_TermRoot.m_TheStation.m_MainSqlite.ExecSql(strSql);
            if (m_Node != null)
                m_Node.Remove();
        }
        public void IniValueList()
        {
            string[] strBoolList = { "TRUE", "FALSE" };


            string[] strDevList = { "NormalTerminal", "BF533Terminal" };


            string[] strConnList = { "SerialPortChannel", "TcpClientChannel", "TcpServerChannel", "UdpClientChannel", "UdpServerChannel" };
            string[] strPtrList = { "UnspecificProtocol", "BF533", "H101", "H101_GDW", "H104", "H101_B" };
            string[] strFileList = { "NoLog", "XmlLog", "TextLog", "SQLLog" };
            m_ValueList.Clear();
            AddNewValue(0, "ID", "自动序号", 0);
            AddNewValue(0, "NodeNo", "设备号", 1);
            AddNewValue(2, "NodeType", "设备类型", 1, 50, strDevList);
            AddNewValue(1, "Address", "设备地址", 1);
            AddNewValue(2, "CommChannel", "通讯类型", 1, 30, strConnList);
            AddNewValue(2, "LocalIP1", "本地IP地址1", 1, 30);
            AddNewValue(2, "LocalPort1", "本地端口号1", 1, 30);
            AddNewValue(2, "RemoteIP1", "设备IP地址1", 1, 30);
            AddNewValue(2, "RemotePort1", "设备端口号1", 1, 30);
            AddNewValue(2, "LocalIP2", "本地IP地址2", 1, 30);
            AddNewValue(2, "RemoteIP2", "设备IP地址2", 1, 30);
            AddNewValue(2, "LocalIP3", "本地IP地址3", 1, 30);
            AddNewValue(2, "RemoteIP3", "设备IP地址3", 1, 30);
            AddNewValue(2, "Protocol", "规约类型", 1, 30, strPtrList);
            AddNewValue(2, "ProtocolCfgFile", "规约配置文件", 1, 30);
            AddNewValue(2, "PrintFrameTerm", "是否打印报文", 2, 30, strBoolList);
            AddNewValue(2, "PrintStatusTerm", "是否打印状态", 2, 30, strBoolList);
            AddNewValue(2, "CommChannelStatusLog", "通讯通道状态记录", 2, 50);
            AddNewValue(2, "CommChannelStatusType", "通讯通道状态类型", 2, 50, strFileList);
            AddNewValue(2, "CommChannelStatusLimit", "通讯通道状态限制", 2, 50);
            AddNewValue(2, "CommChannelFrameLog", "报文记录名称", 2, 50);
            AddNewValue(2, "CommChannelFrameType", "报文记录类型", 2, 50, strFileList);
            AddNewValue(2, "CommChannelFrameLimit", "通讯通道状态限制", 2, 50);
            AddNewValue(2, "SynImm", "是否触发对时", 2, 30, strBoolList);
            AddNewValue(2, "CallImm", "是否触发总召", 2, 30, strBoolList);
            AddNewValue(0, "YxDB", "遥信点表", 2);
            AddNewValue(0, "YcDB", "遥测点表", 2);
            AddNewValue(0, "YkDB", "遥控点表", 2);
            AddNewValue(0, "SetDB", "定值点表", 2);
            AddNewValue(0, "PowerDB", "电度量点表", 2);
            AddNewValue(2, "YxRouteTab", "遥信转发表名称", 2, 30);
            AddNewValue(0, "UseYxTabIndex", "是否使用已建立的点表", 2);
            AddNewValue(2, "YcRouteTab", "遥测转发表名称", 2, 30);
            AddNewValue(0, "UseYcTabIndex", "是否使用已建立的点表", 2);
            AddNewValue(2, "YkRouteTab", "遥控转发表名称", 2, 30);
            AddNewValue(0, "UseYkTabIndex", "是否使用已建立的点表", 2);
            AddNewValue(2, "SetRouteTab", "定值转发表名称", 2, 30);
            AddNewValue(0, "UseSetTabIndex", "是否使用已建立的点表", 2);
            AddNewValue(2, "PowerRouteTab", "遥脉转发表名称", 2, 30);
            AddNewValue(0, "UsePowerTabIndex", "是否使用已建立的点表", 2);
            AddNewValue(2, "ActiveCommYX", "是否触发遥信变位主动上送", 2, 30, strBoolList);
            AddNewValue(2, "AcceptSynTime", "SOE是否接受对时", 2, 30, strBoolList);
            AddNewValue(2, "SoeRecord", "SOE文件名称", 3, 50);
            AddNewValue(0, "SoeLimit", "记录数量", 3);
            AddNewValue(2, "CosRecord", "变位记录文件名", 3, 50);
            AddNewValue(0, "CosLimit", "记录数量", 3);
            AddNewValue(2, "YkRecord", "遥控记录文件名", 3, 50);
            AddNewValue(0, "YkLimit", "记录数量", 3);
            AddNewValue(2, "ExvRecord", "极值记录文件名称", 3, 50);
            AddNewValue(0, "ExvLimit", "记录数量", 3);
            AddNewValue(2, "FixptRecord", "定点记录文件名称", 3, 50);
            AddNewValue(0, "FixptLimit", "记录数量", 3);
            AddNewValue(2, "UlogRecord", "日志文件名称", 3, 50);
            AddNewValue(0, "UlogLimit", "记录数量", 3);
            AddNewValue(2, "FrzRecord", "日冻结文件名称", 3, 50);
            AddNewValue(0, "FrzLimit", "记录数量", 3);
            AddNewValue(2, "FlowrevRecord", "反向功率冻结文件名称", 3, 50);
            AddNewValue(0, "FlowrevLimit", "记录数量", 3);
            AddNewValue(2, "ModuleNodeType", "设备模板类型", 1, 50, CGlobal.strDevModulList);
        }
        private void AddNewValue(int nValueType, string strColName, string strDataName, int nGroupNo, string[] pStrList = null)
        {
            CTranValue pNewValue = new CTranValue();

            pNewValue.m_GroupNo = nGroupNo;
            pNewValue.m_ValueType = nValueType;
            pNewValue.m_StrColName = strColName;
            pNewValue.m_StrDataName = strDataName;

            if (pStrList != null && pStrList.Length > 0)
            {
                pNewValue.m_ComStringList = new string[pStrList.Length];
                for (int i = 0; i < pStrList.Length; i++)
                    pNewValue.m_ComStringList[i] = pStrList[i];
                pNewValue.m_EditMode = true;
            }
            m_ValueList.Add(pNewValue);
        }
        public void IniReadStationTable(DataTable pTable, int nRow)
        {
            int i = 0;
            if (pTable == null)
                return;
            for (i = 0; i < m_ValueList.Count; i++)
            {
                CTranValue pData = m_ValueList[i];
                string strValue = m_TermRoot.m_TheStation.m_MainSqlite.GetTableString(pTable, nRow, i);
                pData.m_TranValue = strValue;
            }
        }
    }
    public class CTermRoot
    {
        public CTransStation m_TheStation = null;
        public List<CDeviceConfig> m_TermList = new List<CDeviceConfig>();
        // public Dictionary<int, int> m_TermNoList = new Dictionary<int, int>();
        public bool IsInial = false;
        public CTermRoot()
        {

        }
        public void LoadTerminalSet()
        {
            if (m_TheStation.m_MainSqlite == null)
                return;
            string m_DevName = "";
            string strSql;
            DataTable pTable;
            strSql = "SELECT  * FROM  TerminalNodeCfg order by NodeNo";
            pTable = (DataTable)m_TheStation.m_MainSqlite.SelectDataTable(strSql);
            m_TermList.Clear();
            if (pTable == null)
                return;

            StringBuilder m_Value = new StringBuilder(100);
            StringBuilder m_ModeuleName = new StringBuilder(100);
            string FileName1 = m_TheStation.m_StationPath + "\\DevName.ini";
            for (int i = 0; i < pTable.Rows.Count; i++)
            {
                CDeviceConfig pDevice = new CDeviceConfig();
                pDevice.m_TermRoot = this;
                pDevice.IniValueList();
                pDevice.IniReadStationTable(pTable, i);
                pDevice.m_TermNo = Convert.ToInt32(pDevice.m_ValueList[1].m_TranValue);
                m_Value.Clear();
                if (File.Exists(FileName1))
                {
                    CGlobal.GetPrivateProfileString("TermName", "Name" + pDevice.m_TermNo.ToString(), "终端", m_Value, 100, FileName1);
                    m_DevName = m_Value.ToString();
                    string FileName2 = m_TheStation.m_StationPath + "\\" + m_DevName + ".ini";
                    CGlobal.GetPrivateProfileString("ModuleName", "Name", "BF533Terminal", m_ModeuleName, 100, FileName2);
                    pDevice.m_ValueList[58].m_TranValue = m_ModeuleName.ToString();
                }
                else
                {
                    m_Value.Append("终端" + (i + 1).ToString());
                    pDevice.m_ValueList[58].m_TranValue = "BF533Terminal";
                }
                pDevice.m_TermName = m_Value.ToString();
                pDevice.IniAllDataTableName();
                pDevice.m_TermDataValue[0].nRealCnt = Convert.ToInt32(pDevice.m_ValueList[25].m_TranValue);
                pDevice.m_TermDataValue[1].nRealCnt = Convert.ToInt32(pDevice.m_ValueList[26].m_TranValue);
                pDevice.m_TermDataValue[2].nRealCnt = Convert.ToInt32(pDevice.m_ValueList[27].m_TranValue);
                pDevice.m_TermDataValue[3].nRealCnt = Convert.ToInt32(pDevice.m_ValueList[28].m_TranValue);
                pDevice.m_TermDataValue[4].nRealCnt = Convert.ToInt32(pDevice.m_ValueList[29].m_TranValue);
                m_TermList.Add(pDevice);
            }
            if (File.Exists(FileName1) == false)
            {
                for (int i = 0; i < pTable.Rows.Count; i++)
                {
                    CGlobal.WritePrivateProfileString("TermName", "Name" + i.ToString(), "终端" + (i + 1).ToString(), FileName1);
                }
            }

            IsInial = true;
        }
        public int GetTermUseNo(Dictionary<int, int> pMap)
        {
            int nNo = 0;
            while (true)
            {
                if (pMap.ContainsKey(nNo))
                {
                    nNo++;
                    continue;
                }
                else
                {
                    pMap.Add(nNo, nNo);
                    return nNo;
                }
            }
        }

        public void AddDeviceRecord(CDeviceConfig pNewDevice)
        {
            string strDataContent = "";
            int i, nCount = 0;
            string strSql = "";
            strSql = "insert into TerminalNodeCfg values(";
            for (i = 0; i < pNewDevice.m_ValueList.Count - 1; i++)
            {
                CTranValue pData = pNewDevice.m_ValueList[i];
                if (nCount > 0)
                    strDataContent += ",";
                strDataContent += pData.GetInsertValueString();
                nCount++;
            }
            if (nCount == 0)
                return;
            strSql += strDataContent;
            strSql += ")";
            m_TheStation.m_MainSqlite.ExecSql(strSql);
        }
        public CDeviceConfig AddNewTerm(string m_TermName, string m_NodeType, int nYcCount = 0, int nYxCount = 0, int nYkCount = 0, int nParaCount = 0, int nYmCount = 0)
        {
            int pYxCount, pYcCount, pYkCount, pParaCount, pYmCount;
            CDeviceConfig pNewDevice = new CDeviceConfig();
            pNewDevice.m_TermName = m_TermName;
            pNewDevice.m_TermRoot = this;
            pNewDevice.m_ValueList = ModelCopies.CopyValues(CTransGlobal.m_DefaultTermValueList);
            pNewDevice.IniValueList();
            pNewDevice.m_ValueList[1].m_TranValue = m_TermList.Count;
            pNewDevice.m_ValueList[0].m_TranValue = Convert.ToInt32(pNewDevice.m_ValueList[1].m_TranValue) + 1;
            pNewDevice.m_ValueList[58].m_TranValue = m_NodeType;
            for (int j = 0; j < CGlobal.m_PriTerm.Count; j++)
            {
                if (m_NodeType == CGlobal.m_PriTerm[j].m_ModulName)
                {
                    pYxCount = CGlobal.m_PriTerm[j].m_Data[0].m_DataName.Count;
                    pYcCount = CGlobal.m_PriTerm[j].m_Data[1].m_DataName.Count;
                    pYkCount = CGlobal.m_PriTerm[j].m_Data[2].m_DataName.Count;
                    pParaCount = CGlobal.m_PriTerm[j].m_Data[3].m_DataName.Count;
                    pYmCount = CGlobal.m_PriTerm[j].m_Data[4].m_DataName.Count;

                    if (nYxCount > 0 && nYxCount < pYxCount)
                        pYxCount = nYxCount;
                    if (nYcCount > 0 && nYcCount < pYcCount)
                        pYcCount = nYcCount;
                    if (nYkCount > 0 && nYkCount < pYkCount)
                        pYkCount = nYkCount;
                    if (nParaCount > 0 && nParaCount < pParaCount)
                        pParaCount = nParaCount;
                    if (nYmCount > 0 && nYmCount < pYmCount)
                        pYmCount = nYmCount;


                    pNewDevice.m_ValueList[25].m_TranValue = pYxCount;//YX
                    pNewDevice.m_ValueList[26].m_TranValue = nYcCount;//YC
                    pNewDevice.m_ValueList[27].m_TranValue = nYkCount;//YK
                    pNewDevice.m_ValueList[28].m_TranValue = nParaCount;//Set
                    pNewDevice.m_ValueList[29].m_TranValue = nYmCount;//Power

                    int[] nPCount = new int[5];
                    nPCount[0] = pYxCount;
                    nPCount[1] = pYcCount;
                    nPCount[2] = pYkCount;
                    nPCount[3] = pParaCount;
                    nPCount[4] = pYmCount;

                    for (int n = 0; n < 5; n++)
                    {
                        for (int i = 0; i < nPCount[n]; i++)
                        {
                            string m_name = CGlobal.m_PriTerm[j].m_Data[n].m_DataName[i].Name;
                            pNewDevice.m_TermDataValue[n].m_DataName.Add(m_name);
                        }
                    }
                    break;
                }
            }
            pNewDevice.IniAllDataTableName();
            pNewDevice.CreateNewTable();
            pNewDevice.InitDBTableData();
            for (int i = 0; i < 5; i++)
            {
                pNewDevice.CreatRouteTableData(i);
            }

            pNewDevice.WriteINIFile();
            pNewDevice.m_ValueList[30].m_TranValue = pNewDevice.m_YxRouteTableName;
            pNewDevice.m_ValueList[32].m_TranValue = pNewDevice.m_YcRouteTableName;
            pNewDevice.m_ValueList[34].m_TranValue = pNewDevice.m_YkRouteTableName;
            pNewDevice.m_ValueList[36].m_TranValue = pNewDevice.m_SetRouteTableName;
            pNewDevice.m_ValueList[38].m_TranValue = pNewDevice.m_PowerRouteTableName;
            m_TermList.Add(pNewDevice);
            string FileName = m_TheStation.m_StationPath + "\\DevName.ini";
            CGlobal.WritePrivateProfileString("TermName", "Name" + pNewDevice.m_ValueList[1].m_TranValue.ToString(), m_TermName, FileName);
            return pNewDevice;
        }


        public CDeviceConfig CopyTerm(CDeviceConfig pBasicTerm)
        {
            if (pBasicTerm == null)
                return null;
            string strName = Interaction.InputBox("请输入拷贝后终端名称", "名称", "", 100, 100);
            strName = strName.Trim();
            if (strName.Length == 0)
                return null;
            CDeviceConfig pNewDevice = new CDeviceConfig();
            pNewDevice.m_TermRoot = this;
            pNewDevice.m_TermName = strName;
            pNewDevice.m_ValueList = ModelCopies.CopyValues(pBasicTerm.m_ValueList);
            pNewDevice.m_ValueList[1].m_TranValue = m_TermList.Count;
            pNewDevice.m_ValueList[0].m_TranValue = Convert.ToInt32(pNewDevice.m_ValueList[1].m_TranValue) + 1;
            int NodeNo = Convert.ToInt32(pNewDevice.m_ValueList[1].m_TranValue);

            pNewDevice.IniAllDataTableName();       //初始化表格名称
            pNewDevice.CreateNewTable();            //创建新的表格
            pNewDevice.CopyTableData(pBasicTerm);    //拷贝表格数据
            pNewDevice.m_TermDataValue = ModelCopies.CopyTerminalData(pBasicTerm.m_TermDataValue);
            pNewDevice.m_ValueList[30].m_TranValue = pNewDevice.m_YxRouteTableName;
            pNewDevice.m_ValueList[32].m_TranValue = pNewDevice.m_YcRouteTableName;
            pNewDevice.m_ValueList[34].m_TranValue = pNewDevice.m_YkRouteTableName;
            pNewDevice.m_ValueList[36].m_TranValue = pNewDevice.m_SetRouteTableName;
            pNewDevice.m_ValueList[38].m_TranValue = pNewDevice.m_PowerRouteTableName;
            AddDeviceRecord(pNewDevice);
            string SourcePath = m_TheStation.m_StationPath + "\\" + pBasicTerm.m_TermName + ".ini";
            string DestPath = m_TheStation.m_StationPath + "\\" + strName + ".ini";
            File.Copy(SourcePath, DestPath);
            string FileName = m_TheStation.m_StationPath + "\\DevName.ini";
            CGlobal.WritePrivateProfileString("TermName", "Name" + NodeNo.ToString(), strName, FileName);
            m_TermList.Add(pNewDevice);
            return pNewDevice;
        }
        public void CreateTotalTable()
        {

        }
        public void DeleteTerm(CDeviceConfig pBasicTerm)
        {
            int start = Convert.ToInt32(pBasicTerm.m_ValueList[1].m_TranValue);
            string strSql = "";
            if (pBasicTerm == null)
                return;
            pBasicTerm.DropTermTable();
            m_TermList.Remove(pBasicTerm);
            int m_NodeNo = Convert.ToInt32(pBasicTerm.m_ValueList[1].m_TranValue);
            //m_TermNoList.Remove(m_NodeNo);
            string Path = m_TheStation.m_StationPath + "\\" + pBasicTerm.m_TermName + ".ini";
            File.Delete(Path);      //删除指定文件

            if (start == m_TermList.Count)
            {
                return;
            }
            for (int i = start; i < m_TermList.Count; i++)
            {
                CDeviceConfig temp = m_TermList[i];
                string oldname0 = "YcDB" + temp.m_ValueList[1].m_TranValue.ToString();
                string oldname1 = "YxDB" + temp.m_ValueList[1].m_TranValue.ToString();
                string oldname2 = "YkDB" + temp.m_ValueList[1].m_TranValue.ToString();
                string oldname3 = "SetDB" + temp.m_ValueList[1].m_TranValue.ToString();
                string oldname4 = "PowerDB" + temp.m_ValueList[1].m_TranValue.ToString();
                string newname0 = "YcDB" + i.ToString();
                string newname1 = "YxDB" + i.ToString();
                string newname2 = "YkDB" + i.ToString();
                string newname3 = "SetDB" + i.ToString();
                string newname4 = "PowerDB" + i.ToString();
                temp.ReNameTable(oldname0, newname0);
                temp.ReNameTable(oldname1, newname1);
                temp.ReNameTable(oldname2, newname2);
                temp.ReNameTable(oldname3, newname3);
                temp.ReNameTable(oldname4, newname4);

                temp.m_YcTableName = newname0;
                temp.m_YxTableName = newname1;
                temp.m_YkTableName = newname2;
                temp.m_SetTableName = newname3;
                temp.m_PowerTableName = newname4;


                oldname0 = "TerYcTab" + temp.m_ValueList[1].m_TranValue.ToString();
                oldname1 = "TerYxTab" + temp.m_ValueList[1].m_TranValue.ToString();
                oldname2 = "TerYkTab" + temp.m_ValueList[1].m_TranValue.ToString();
                oldname3 = "TerSetTab" + temp.m_ValueList[1].m_TranValue.ToString();
                oldname4 = "TerPowerTab" + temp.m_ValueList[1].m_TranValue.ToString();

                newname0 = "TerYcTab" + i.ToString();
                newname1 = "TerYxTab" + i.ToString();
                newname2 = "TerYkTab" + i.ToString();
                newname3 = "TerSetTab" + i.ToString();
                newname4 = "TerPowerTab" + i.ToString();
                temp.ReNameTable1(oldname0, newname0);
                temp.ReNameTable1(oldname1, newname1);
                temp.ReNameTable1(oldname2, newname2);
                temp.ReNameTable1(oldname3, newname3);
                temp.ReNameTable1(oldname4, newname4);

                temp.m_YcRouteTableName = newname0;
                temp.m_YxRouteTableName = newname1;
                temp.m_YkRouteTableName = newname2;
                temp.m_SetRouteTableName = newname3;
                temp.m_PowerRouteTableName = newname4;


                temp.m_ValueList[30].m_TranValue = newname0;
                temp.m_ValueList[32].m_TranValue = newname0;
                temp.m_ValueList[34].m_TranValue = newname0;
                temp.m_ValueList[36].m_TranValue = newname0;
                temp.m_ValueList[38].m_TranValue = newname0;

                temp.m_ValueList[1].m_TranValue = i;
                strSql = "update " + "TerminalNodeCfg" + " set NodeNo=" + i.ToString() + " where(ID=" + temp.m_ValueList[0].m_TranValue.ToString() + ")";
                m_TheStation.m_MainSqlite.ExecSql(strSql);
                temp.m_ValueList[0].m_TranValue = i + 1;
                strSql = "update " + "TerminalNodeCfg" + " set ID=" + (i + 1).ToString() + " where(NodeNo=" + i.ToString() + ")";
                m_TheStation.m_MainSqlite.ExecSql(strSql);
                string FileName = m_TheStation.m_StationPath + "\\DevName.ini";
                CGlobal.WritePrivateProfileString("TermName", "Name" + i.ToString(), temp.m_TermName, FileName);

            }

        }
    }

}
