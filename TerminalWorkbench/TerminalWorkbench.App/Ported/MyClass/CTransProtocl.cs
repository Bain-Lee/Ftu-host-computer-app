using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Data.Common;
using Microsoft.Data.Sqlite;
namespace DtuTestManager
{
    public class CTransProtocl : CTrasnSingleTable<CProtoclData>
    {
        public CTransProtocl()
        {
            m_tableType = 5;
            CProtoclData m_ColNameData = new CProtoclData();
            for (int i = 0; i < m_ColNameData.m_ValueList.Count; i++)
            {
                m_ValueList.Add(m_ColNameData.m_ValueList[i]);
            }
        }
        private void ResetDataNoID()
        {
            for (int i = 0; i < m_DataList.Count; i++)
            {
                m_DataList[i].m_ValueList[0].m_TranValue = i + 1;
                m_DataList[i].m_ValueList[1].m_TranValue = i;
            }
        }
        public override CTranValue GetTranValue(int nRow, int nCol)
        {
            if (nRow < m_DataList.Count)
            {
                if (nCol < m_DataList[nRow].m_ValueList.Count)
                    return m_DataList[nRow].m_ValueList[nCol];
            }
            return null;
        }

        public override void AddNewData()
        {
            base.AddNewData();
            CProtoclData pRowData = m_DataList.Last();
            pRowData.SetDefaultValue();
            ResetDataNoID();
        }
        public override void RemoveAtData(int nIndex)
        {
            base.RemoveAtData(nIndex);
            ResetDataNoID();
        }
        public override void IniReadTableData()
        {
            string strSql;
            DataTable pTable;
            ClearAllData();
            if (m_TableName == "NULL" || m_TableName == "null")
                return;
            strSql = "SELECT  * FROM  " + m_TableName + " order by ID";
            pTable = (DataTable)m_SqlDataBase.SelectDataTable(strSql);
            if (pTable == null)
                return;

            for (int i = 0; i < pTable.Rows.Count; i++)
            {
                CProtoclData pNewData = new CProtoclData();
                m_DataList.Add(pNewData);
                for (int j = 0; j < pNewData.m_ValueList.Count; j++)
                {
                    CTranValue pData = pNewData.m_ValueList[j];
                    pData.m_TranValue = m_SqlDataBase.GetTableString(pTable, i, j);
                }
            }
        }
        public override int SaveTableContent()
        {
            int i, j;
            string strSql;

            using DbCommand cmd = m_SqlDataBase.m_TranConn.CreateCommand();

            using DbTransaction trans = m_SqlDataBase.m_TranConn.BeginTransaction();
            cmd.Transaction = trans;
            try
            {
                strSql = "delete from " + m_TableName;
                cmd.CommandText = strSql;

                cmd.ExecuteNonQuery();

                for (i = 0; i < m_DataList.Count; i++)
                {
                    strSql = "insert into " + m_TableName + " values(";
                    for (j = 0; j < m_DataList[i].m_ValueList.Count; j++)
                    {
                        if (j > 0)
                            strSql += ",";
                        strSql += m_DataList[i].m_ValueList[j].GetAddStringDesc();
                    }
                    strSql += ")";
                    cmd.CommandText = strSql;

                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
                return m_DataList.Count;
            }
            catch
            {
                trans.Rollback();
                throw;
            }

        }
        public override void UpdateTranValue(int nIndex, object pValue)
        {
            if (m_SelectIndex < m_DataList.Count)
            {
                if (nIndex < m_DataList[m_SelectIndex].m_ValueList.Count)
                    m_DataList[m_SelectIndex].m_ValueList[nIndex].m_TranValue = pValue;
            }
        }
    }
    public class CProtoclData : CTranValueList
    {
        public CProtoclData()
        {
            string[] strBooList = { "TRUE", "FALSE" };
            string[] strFileList = { "NoLog", "XmlLog", "TextLog", "SQLLog" };
            string[] strEsamList = { "NULL", "ESAM" };
            int nIndex = 0;

            AddNewValue(0, "ID", "自动序号", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(2, "PrintFrameTerm", "打印报文", 0, 30, strBooList);
            m_ValueList[nIndex++].m_TranValue = "FALSE";
            AddNewValue(2, "PrintStatusTerm", "打印状态", 0, 30, strBooList);
            m_ValueList[nIndex++].m_TranValue = "FALSE";
            AddNewValue(2, "StatusLogName", "状态日志存储文件名", 0, 50);
            m_ValueList[nIndex++].m_TranValue = "NULL";
            AddNewValue(2, "StatusLogType", "状态日志存储文件类型", 0, 30, strFileList);
            m_ValueList[nIndex++].m_TranValue = "TextLog";
            AddNewValue(2, "StatusLogLimit", "状态日志存储条数限制", 0, 30);
            m_ValueList[nIndex++].m_TranValue = 1024;
            AddNewValue(2, "FrameLogName", "报文打印存储文件名", 0, 50);
            m_ValueList[nIndex++].m_TranValue = "NULL";
            AddNewValue(2, "FrameLogType", "报文打印存储文件类型", 0, 30, strFileList);
            m_ValueList[nIndex++].m_TranValue = "TextLog";
            AddNewValue(2, "FrameLogLimit", "报文打印存储限制", 0, 30);
            m_ValueList[nIndex++].m_TranValue = 1024;
            AddNewValue(2, "ActiveDataUp", "数据主动上传", 0, 30, strBooList);
            m_ValueList[nIndex++].m_TranValue = "FALSE";
            AddNewValue(2, "ActiveRepeatFrame", "报文是否重发", 0, 30, strBooList);
            m_ValueList[nIndex++].m_TranValue = "FALSE";
            AddNewValue(0, "FrameRepeatSum", "报文重发次数", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "LostAnswerTimesOut", "命令回复超时时间", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "WaitforanswerTimeout", "等待回复超时时间", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "ConsecutiveSendTimeOut", "连续发送超时时间", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(2, "ActiveDataRecv", "数据接收标志", 0, 30, strBooList);
            m_ValueList[nIndex++].m_TranValue = "FALSE";
            AddNewValue(0, "LostRecvTimeOut", "数据接收超时时间", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(2, "ClearQuality", "清质量标志", 0, 30, strBooList);
            m_ValueList[nIndex++].m_TranValue = "FALSE";
            AddNewValue(2, "ClearEvent", "清事件标志", 0, 30, strBooList);
            m_ValueList[nIndex++].m_TranValue = "FALSE";
            AddNewValue(0, "SynCharNum", "报文同步字符个数", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "SYxStartAddr", "单点遥信起始地址", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "DYxStartAddr", "双点遥信起始地址", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "YcStartAddr", "遥测起始地址", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "SYkStartAddr", "单点遥控起始地址", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "DYkStartAddr", "双点遥信起始地址", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "FrameLenLength", "报文长度字符长", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "FrameTypeLength", "报文类型长度", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "InfoNumLength", "信息体长度", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "TransReasonLength", "传送原因长度", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "AsduAddrLength", "公共地址长度", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "InfoAddrLength", "信息体地址长度", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "FunTypeLength", "功能类型长度", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "NormalYkFrameLength", "普通遥控报文长度", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "TimeOutQueryUnActivePoint", "通道检测等待计数", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "TimeOutRequireLink", "请求链路循环时间", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "TimeOutCallAllData", "总召唤循环时间", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "TimeOutSynTime", "时钟同步循环时间", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "TimeOutHeartFrame", "心跳循环时间", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "TimeOutYkSel", "遥控选择超时时间", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "TimeOutYkSelToExe", "遥控选择后等待遥控执行时间", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "TimeOutYkExe", "遥控执行超时时间", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "TimeOutYkCancel", "遥控撤销超时时间", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "TimeOutStartInit", "初始化超时时间", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "ResetTimer", "复位时钟标志", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "HeartTimeOutResetLink", "心跳超时后重新链接时间", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "ShortHeartTest", "短帧测试标识", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "TimeOutIGramRecv", "I帧接收最大时间", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "TimeOutIGramSend", "I帧发送最大时间", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(2, "CheckRecvI", "检测接收I帧标志", 0, 30, strBooList);
            m_ValueList[nIndex++].m_TranValue = "FALSE";
            AddNewValue(2, "CheckSendI", "检测发送I帧标志", 0, 30, strBooList);
            m_ValueList[nIndex++].m_TranValue = "FALSE";
            AddNewValue(0, "SUM_K", "I帧接收最大个数", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(0, "SUM_W", "I帧发送最大个数", 0);
            m_ValueList[nIndex++].m_TranValue = 0;
            AddNewValue(2, "FaultWaveBinType", "录波文件类型标志", 0, 30, strBooList);
            m_ValueList[nIndex++].m_TranValue = "FALSE";
            AddNewValue(2, "KeyType", "加密类型", 0, 30, strEsamList);
            m_ValueList[nIndex++].m_TranValue = "NULL";
            AddNewValue(2, "SecretID", "密钥ID", 0, 30);
            m_ValueList[nIndex++].m_TranValue = "NULL";
            AddNewValue(2, "KeyPath1", "密钥1文件路径", 0, 50);
            m_ValueList[nIndex++].m_TranValue = "NULL";
            AddNewValue(2, "KeyPath2", "密钥2文件路径", 0, 50);
            m_ValueList[nIndex++].m_TranValue = "NULL";
            AddNewValue(2, "KeyPath3", "密钥3文件路径", 0, 50);
            m_ValueList[nIndex++].m_TranValue = "NULL";
            AddNewValue(2, "KeyPath4", "密钥4文件路径", 0, 50);
            m_ValueList[nIndex++].m_TranValue = "NULL";
            AddNewValue(2, "ModbusCmdCfg", "modbus命令标志", 0, 30);
            m_ValueList[nIndex++].m_TranValue = "NULL";
        }
        public void SetDefaultValue()
        {
            int nIndex = 0;
            m_ValueList[nIndex++].m_TranValue = 0;
            m_ValueList[nIndex++].m_TranValue = "FALSE";

            m_ValueList[nIndex++].m_TranValue = "FALSE";

            m_ValueList[nIndex++].m_TranValue = "NULL";

            m_ValueList[nIndex++].m_TranValue = "TextLog";

            m_ValueList[nIndex++].m_TranValue = 1024;

            m_ValueList[nIndex++].m_TranValue = "NULL";

            m_ValueList[nIndex++].m_TranValue = "TextLog";

            m_ValueList[nIndex++].m_TranValue = 1024;

            m_ValueList[nIndex++].m_TranValue = "FALSE";

            m_ValueList[nIndex++].m_TranValue = "FALSE";

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = "FALSE";

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = "FALSE";

            m_ValueList[nIndex++].m_TranValue = "FALSE";

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = "FALSE";

            m_ValueList[nIndex++].m_TranValue = "FALSE";

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = 0;

            m_ValueList[nIndex++].m_TranValue = "FALSE";

            m_ValueList[nIndex++].m_TranValue = "NULL";

            m_ValueList[nIndex++].m_TranValue = "NULL";

            m_ValueList[nIndex++].m_TranValue = "NULL";

            m_ValueList[nIndex++].m_TranValue = "NULL";

            m_ValueList[nIndex++].m_TranValue = "NULL";

            m_ValueList[nIndex++].m_TranValue = "NULL";

            m_ValueList[nIndex++].m_TranValue = "NULL";
        }
    }
}
