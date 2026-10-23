using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Data;
using System.Threading;
using Microsoft.VisualBasic;
using System.Data.Common;
using System.Data.SQLite;

namespace DtuTestManager
{
   public  class CRouteTable
    {
        public CTransStation m_TheStation = null;
        public TreeNode m_Node = null;       
        public string m_TableName;
        public List<CPriTable> m_PriData = new List<CPriTable>();

        public CRouteTable()
        {
            
        }
        public void LoadData()
        {
            string strSql;
            DataTable pTable;
            strSql = "SELECT  * FROM  "+m_TableName+" order by ID";
            pTable = (DataTable)m_TheStation.m_UserSqlite.SelectDataTable(strSql);
            m_PriData.Clear();
            if (pTable == null) return;
            for (int i = 0; i < pTable.Rows.Count; i++)
            {
                CPriTable temp = new CPriTable();
                temp.DstNode = i;
                temp.SrcNode = m_TheStation.m_UserSqlite.GetTableIntValue(pTable, i, 2);
                m_PriData.Add(temp);
            }        
        }
        public void AddPriTab(string m_TableName)
        {
            string strSql= "CREATE TABLE " + m_TableName + "(ID INTEGER, DstNode INTEGER,SrcNode  INTEGER)";
            m_TheStation.m_UserSqlite.ExecSql(strSql);
            
        }
        public void CopyPriTab(string m_TableName, CRouteTable pRouteTable)
        {
            AddPriTab(m_TableName);
            CopyTable(m_TableName, pRouteTable.m_TableName);
        }

        private void CopyTable(string DestTable, string SourceTable) 
        {
            string strSql = " Insert into  " + DestTable + " select * from " + SourceTable;
            m_TheStation.m_UserSqlite.ExecSql(strSql);
        }
        public void DeletePritab(CRouteTable pRouteTable, int NodeType)
        {
            if (pRouteTable == null) return;
            string strSql = "drop table " + pRouteTable.m_TableName;
            m_TheStation.m_UserSqlite.ExecSql(strSql);
            string tablename = pRouteTable.m_TableName;
            switch (NodeType)
            {
                case 0:
                    if ((tablename.Length > 8) && (tablename.Substring(0, 8) == "PriYxTab"))
                    {                    
                        int n = Convert.ToInt32(tablename.Substring(8, tablename.Length - 8));
                        m_TheStation.m_PriNoList[NodeType].Remove(n);
                        m_TheStation.m_PriRoute[NodeType].Remove(pRouteTable);
                    }
                    break;
                case 1:
                    if ((tablename.Length > 8) && (tablename.Substring(0, 8) == "PriYcTab"))
                    {                       
                        int n = Convert.ToInt32(tablename.Substring(8, tablename.Length - 8));
                        m_TheStation.m_PriNoList[NodeType].Remove(n);
                        m_TheStation.m_PriRoute[NodeType].Remove(pRouteTable);
                    }
                    break;
                case 2:
                    if ((tablename.Length > 8) && (tablename.Substring(0, 8) == "PriYkTab"))
                    {                       
                        int n = Convert.ToInt32(tablename.Substring(8, tablename.Length - 8));
                        m_TheStation.m_PriNoList[NodeType].Remove(n);
                        m_TheStation.m_PriRoute[NodeType].Remove(pRouteTable);
                    }
                    break;
                case 4:
                    if ((tablename.Length > 11) && (tablename.Substring(0, 11) == "PriPowerTab"))
                    {                       
                        int n = Convert.ToInt32(tablename.Substring(11, tablename.Length - 11));
                        m_TheStation.m_PriNoList[NodeType].Remove(n);
                        m_TheStation.m_PriRoute[NodeType].Remove(pRouteTable);
                    }
                    break;
                case 3:
                    if ((tablename.Length > 9) && (tablename.Substring(0, 9) == "PriSetTab"))
                    {                       
                        int n = Convert.ToInt32(tablename.Substring(9, tablename.Length - 9));
                        m_TheStation.m_PriNoList[NodeType].Remove(n);
                        m_TheStation.m_PriRoute[NodeType].Remove(pRouteTable);
                    }
                    break;
            }
        }

        public void SaveDataTable()
        {
            string strSql = "delete from " + m_TableName;
            m_TheStation.m_UserSqlite.ExecSql(strSql);

            DbCommand cmd = m_TheStation.m_UserSqlite.m_TranConn.CreateCommand();
            cmd.Parameters.Add(cmd.CreateParameter());
            DbTransaction trans = m_TheStation.m_UserSqlite.m_TranConn.BeginTransaction();  //开始事务
            try
            {
                for (int i = 0; i < m_PriData.Count; i++)
                {
                    strSql = "insert into " + m_TableName + " values(";
                    strSql += (i+1).ToString() + "," + (i).ToString() + "," + (m_PriData[i].SrcNode).ToString();                
                    strSql += ")";
                    cmd.CommandText = strSql;
                    cmd.Parameters[0].Value = i.ToString();
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();  //批量提交
            }
            catch
            {
                trans.Rollback();
                throw;
            }
        }
    }

    public class CPriTerminal
    {
        public int TotalNode;//总序号
        public int DataNode; //数据序号
        public string Name = "";//名称
        public bool SelectFlag;//选中标志  true选中
    }
    public class CPriTable
    {
        public int DstNode;
        public int SrcNode;//序号
        public string Name = "";//名称
    }

    public class CPriData
    {
        public int StartAddr;
        public int EndAddr;
        public List<CPriTerminal> m_DataName = new List<CPriTerminal>();
    }
    public class CPriTerm
    {
        public string m_ModulName;
        public List<CPriData> m_Data = new List<CPriData>();      
        public CPriTerm()
        {
            for (int i = 0; i <6; i++)
            {
                CPriData temp = new CPriData();
                m_Data.Add(temp);
            }
        }
        public void Clear()
        {
            for (int i = 0; i < 6; i++)
            {
                m_Data[i].m_DataName.Clear();
            }
        }
        public void ReadDataName(CDataBase m_DB)
        {
            string strSql;
            DataTable pTable;
            strSql = "SELECT  * FROM  " + "Mod_YxTable" + " order by YxID";
            pTable = (DataTable)m_DB.SelectDataTable(strSql);
            if (pTable == null) return;
            for (int i = 0; i < pTable.Rows.Count; i++)
            {
                CPriTerminal temp = new CPriTerminal();
                temp.Name = m_DB.GetTableString(pTable, i, 1);
                m_Data[0].m_DataName.Add(temp);
            }

            strSql = "SELECT  * FROM  " + "Mod_YcTable" + " order by YcID";
            pTable = (DataTable)m_DB.SelectDataTable(strSql);
            if (pTable == null) return;
            for (int i = 0; i < pTable.Rows.Count; i++)
            {
                CPriTerminal temp = new CPriTerminal();
                temp.Name = m_DB.GetTableString(pTable, i, 1);
                m_Data[1].m_DataName.Add(temp);
            }

            strSql = "SELECT  * FROM  " + "Mod_YkTable" + " order by YkID";
            pTable = (DataTable)m_DB.SelectDataTable(strSql);
            if (pTable == null) return;
            for (int i = 0; i < pTable.Rows.Count; i++)
            {
                CPriTerminal temp = new CPriTerminal();
                temp.Name = m_DB.GetTableString(pTable, i, 1);
                m_Data[2].m_DataName.Add(temp);
            }          

            strSql = "SELECT  * FROM  " + "Mod_ParaTable" + " WHERE  (ParaTableID > 5)" + " order by ParaTableID,ParaID";
            pTable = (DataTable)m_DB.SelectDataTable(strSql);
            if (pTable == null) return;
            for (int i = 0; i < pTable.Rows.Count; i++)
            {
                CPriTerminal temp = new CPriTerminal();
                temp.Name = m_DB.GetTableString(pTable, i, 3);
                m_Data[3].m_DataName.Add(temp);
            }

            strSql = "SELECT  * FROM  " + "Mod_PowerTable" + " order by PowerID";
            pTable = (DataTable)m_DB.SelectDataTable(strSql);
            if (pTable == null) return;
            for (int i = 0; i < pTable.Rows.Count; i++)
            {
                CPriTerminal temp = new CPriTerminal();
                temp.Name = m_DB.GetTableString(pTable, i, 1);
                m_Data[4].m_DataName.Add(temp);
            }
        }
    }
}
