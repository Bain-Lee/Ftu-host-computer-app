using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using System.Diagnostics;


namespace DtuTestManager
{
    public class CDataBase
    {
        public string m_Path;
        public OleDbConnection m_Dbconn;
        bool m_bConnected;

        public CDataBase()
        {
            m_bConnected = false;
        }
        public CDataBase(string strPath)
        {
            m_bConnected = false;
            m_Path = strPath;
        }
        public void CloseDatabase()
        {
            m_bConnected = false;
            m_Dbconn?.Dispose();
            m_Dbconn = null;
        }
        public bool ConnectDataBase()
        {
            CloseDatabase();
            string strConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + m_Path;
            // string strConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=DataBase.accdb;Persist Security Info=False;";
            m_Dbconn = new OleDbConnection(strConnectionString);

            try
            {
                m_Dbconn.Open();
            }
            catch (OleDbException)
            {
                CloseDatabase();
                return false;
            }
            if (m_Dbconn.State == System.Data.ConnectionState.Open)
            {
                m_bConnected = true;
            }
            return m_bConnected;
        }
        public object SelectDataTable(string sqlStr)
        {
            object returnObj = new object();
            try
            {
                if (m_bConnected == false)
                    return null;
                using OleDbDataAdapter dataAdapter = new OleDbDataAdapter(sqlStr, m_Dbconn);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Select");
                returnObj = dataSet.Tables[0];
            }
            catch (OleDbException)
            {
                return null;
            }
            return returnObj;

        }
        public bool ExecSql(string sqlStr)
        {
            if (m_bConnected == false)
                return false;
            try
            {
                using OleDbCommand odbc = new OleDbCommand(sqlStr, m_Dbconn);
                odbc.ExecuteNonQuery();
            }
            catch (OleDbException)
            {
                return false;
            }
            return true;
        }
        public int GetTableIntValue(DataTable pTable, int nRow, int Columm)
        {
            if (pTable.Rows.Count <= nRow || pTable.Columns.Count <= Columm)
                return 0;
            string strValue = pTable.Rows[nRow][pTable.Columns[Columm].ToString()].ToString();
            if (strValue.Length == 0)
                return 0;
            return Convert.ToInt32(strValue);
        }
        public bool GetTableBoolValue(DataTable pTable, int nRow, int Columm)
        {
            if (pTable.Rows.Count <= nRow || pTable.Columns.Count <= Columm)
                return false;
            string strValue = pTable.Rows[nRow][pTable.Columns[Columm].ToString()].ToString();
            if (strValue.Length == 0)
                return false;
            int nValue = Convert.ToInt32(strValue);
            if (nValue == 0)
                return false;
            return true;
        }
        public float GetTableFloatValue(DataTable pTable, int nRow, int Columm)
        {
            if (pTable.Rows.Count <= nRow || pTable.Columns.Count <= Columm)
                return 0;
            string strValue = pTable.Rows[nRow][pTable.Columns[Columm].ToString()].ToString();
            if (strValue.Length == 0)
                return 0;
            return Convert.ToSingle(strValue);
        }
        public string GetTableString(DataTable pTable, int nRow, int Columm)
        {
            if (pTable.Rows.Count <= nRow || pTable.Columns.Count <= Columm)
                return "";
            return pTable.Rows[nRow][pTable.Columns[Columm].ToString()].ToString();
        }

    }
}
