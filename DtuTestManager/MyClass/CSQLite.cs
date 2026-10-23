using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Diagnostics;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
//using System.Data.SQLite.Linq;
namespace DtuTestManager
{
    public class CSQLite
    {
        public SQLiteConnection m_Dbconn; //连接对象
        bool m_bConnected;
        public DbConnection m_TranConn;  //事务对象
      
        
        public CSQLite()
        {
            m_bConnected = false;
        }
        public void CloseDatabase()
        {
            if (m_bConnected == false) return;
            m_bConnected = false;
            m_Dbconn.Close();
            m_TranConn.Close();
        }
        public  bool ConnectDataBase(string SqliteName)
        {
            DbProviderFactory factory = SQLiteFactory.Instance;
            m_TranConn = factory.CreateConnection();
             // 连接数据库
             m_TranConn.ConnectionString = "Data Source=" + SqliteName;
             m_TranConn.Open();

            string strConnectionString = @"Data Source=" + @SqliteName+";Initial Catalog=sqlite;Integrated Security=True;Max Pool Size=10";
            m_Dbconn = new SQLiteConnection(strConnectionString);          
            try
            {
                m_Dbconn.Open();
            }
            catch (OleDbException)
            {
                return false;
            }
            if (m_Dbconn.State == System.Data.ConnectionState.Open)
            {
                m_bConnected = true;
            }
            return m_bConnected;
        }

        public DataTable GetTableName()
        {
            if (m_bConnected == false) return null;
            DataTable dt = m_Dbconn.GetSchema("Tables", null);
              return dt;
        }
        public object SelectDataTable(string sqlStr)
        {
            object returnObj = new object();
            try
            {
                if (m_bConnected == false) return null;
                SQLiteCommand cmd = new SQLiteCommand(sqlStr, m_Dbconn);
                SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(cmd);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                returnObj = dataSet.Tables[0];
                dataAdapter.Dispose();
                cmd.Dispose();
            }
            catch 
            {
                return null;
            }
            return returnObj;
           
        }
        public bool ExecSql(string sqlStr)
        {
            /*SQLiteCommand cmd = new SQLiteCommand(sqlStr, m_Dbconn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch(OleDbException e1)
            {
                MessageBox.Show(e1.Message);
                return false;
            }
            return true;*/

            if (m_bConnected == false) return false;
            DbCommand cmd = m_TranConn.CreateCommand();
            // 添加参数
            cmd.Parameters.Add(cmd.CreateParameter());
            DbTransaction trans = m_TranConn.BeginTransaction(); //开始数据库事务
            try
            {

                cmd.CommandText = sqlStr;
                cmd.Parameters[0].Value = "0";
                cmd.ExecuteNonQuery();
                trans.Commit();  //提交数据库事务
            }
            catch
            {
                trans.Rollback();
                return false;
                throw;

            }

            return true;
        }
       

        public int GetTableIntValue(DataTable pTable, int nRow, int Columm)
        {
            if (pTable.Rows.Count <= nRow || pTable.Columns.Count <= Columm) return 0;
            string strValue = pTable.Rows[nRow][pTable.Columns[Columm].ToString()].ToString();
            if (strValue.Length == 0) return 0;
            return Convert.ToInt32(strValue);
        }

        public string GetTableString(DataTable pTable, int nRow, int Columm)
        {
            if (pTable.Rows.Count <= nRow || pTable.Columns.Count <= Columm) return "";
            return pTable.Rows[nRow][pTable.Columns[Columm].ToString()].ToString();
        }


        public float GetTableFloatValue(DataTable pTable, int nRow, int Columm)
        {
            if (pTable.Rows.Count <= nRow || pTable.Columns.Count <= Columm) return 0;
            string strValue = pTable.Rows[nRow][pTable.Columns[Columm].ToString()].ToString();
            if (strValue.Length == 0) return 0;
            return Convert.ToSingle(strValue);
        }
    }
}
