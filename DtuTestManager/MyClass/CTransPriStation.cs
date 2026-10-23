using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
namespace DtuTestManager
{
        
  
   

   
    public class CPriConfig : CTranValueList
    {
       
        public CPriRoot m_PriRoot = null;       
        public TreeNode m_Node = null;
        public int m_TermNo = 0;
       
        public CPriConfig()
        {          
           
           
        }       
        public void IniAllDataTableName()
        {
           
           
        }
    
        private void CopyTable(string DestTable, string SourceTable) //两个表都存在的情况下
        {
             string dbstr = " Insert into  " + DestTable + " select * from " + SourceTable;
             m_PriRoot.m_TheStation.m_MainSqlite.ExecSql(dbstr);
        }
        private void DeleteTable(string tableName)//删除表格，释放空间
        {
            string strSql = "drop table " + tableName;
            m_PriRoot.m_TheStation.m_UserSqlite.ExecSql(strSql);
        }
        public void DropTable()
        {
            int NodeNo = Convert.ToInt32(m_ValueList[1].m_TranValue);
            string strSql;
            strSql = "delete from PriStationNodeCfg where(NodeNo=" + NodeNo.ToString() + ")";
            m_PriRoot.m_TheStation.m_MainSqlite.ExecSql(strSql);
            if (m_Node != null) m_Node.Remove();
        }
        public void IniValueList()
        {
            string[] strBoolList = { "TRUE", "FALSE" };
            string[] StrListYxRouteTable = new string[m_PriRoot.m_TheStation.StrListYxRouteTable.Count];
            string[] StrListYcRouteTable = new string[m_PriRoot.m_TheStation.StrListYcRouteTable.Count];
            string[] StrListYkRouteTable = new string[m_PriRoot.m_TheStation.StrListYkRouteTable.Count];
            string[] StrListSetRouteTable = new string[m_PriRoot.m_TheStation.StrListSetRouteTable.Count];
            string[] StrListPowerRouteTable = new string[m_PriRoot.m_TheStation.StrListPowerRouteTable.Count];
            for (int i = 0; i < StrListYxRouteTable.Length; i++)
            {
                StrListYxRouteTable[i] = m_PriRoot.m_TheStation.StrListYxRouteTable[i];            
            }
            for (int i = 0; i < StrListYcRouteTable.Length; i++)
            {
                StrListYcRouteTable[i] = m_PriRoot.m_TheStation.StrListYcRouteTable[i];
            }
            for (int i = 0; i < StrListYkRouteTable.Length; i++)
            {
                StrListYkRouteTable[i] = m_PriRoot.m_TheStation.StrListYkRouteTable[i];
            }
            for (int i = 0; i < StrListSetRouteTable.Length; i++)
            {
                StrListSetRouteTable[i] = m_PriRoot.m_TheStation.StrListSetRouteTable[i];
            }
            for (int i = 0; i < StrListPowerRouteTable.Length; i++)
            {
                StrListPowerRouteTable[i] = m_PriRoot.m_TheStation.StrListPowerRouteTable[i];
            }

            m_ValueList.Clear();
            string[] strStationList = { "NormalPriStation", "ConsolePriStation", "LcdDisplayPriStation" };
            string[] strConnList = { "SerialPortChannel","TcpClientChannel","TcpServerChannel","UdpClientChannel","UdpServerChannel"};
            string[] strPtrList = { "S101", "S104", "S101_B", "S104_Console", "S104_GDW", "S101_GDW","S101_BGDW","S101_Console",
                                      "S101_BConsole","S104_GDWS","S104_GDWC","S101_GDWS","S101_GDWC","S101_BGDWS","S101_BGDWC","Modbus","SNTP" };
            AddNewValue(0, "ID", "自动序号", 0);
            AddNewValue(0, "NodeNo", "主站号", 1);
            AddNewValue(2, "NodeType", "主站类型", 1, 50, strStationList);
            AddNewValue(1, "Address", "主站地址", 1);
            AddNewValue(2, "CommChannel", "通讯类型", 1, 30, strConnList);
            AddNewValue(2, "LocalIP1", "本地IP地址1", 1, 30);
            AddNewValue(2, "LocalPort1", "本地端口号1", 1, 30);
            AddNewValue(2, "RemoteIP1", "远方IP地址1", 1, 30);
            AddNewValue(2, "RemotePort1", "远方端口号1", 1, 30);
            AddNewValue(2, "LocalIP2", "本地IP地址2", 1, 30);
            AddNewValue(2, "RemoteIP2", "远方IP地址2", 1, 30);
            AddNewValue(2, "LocalIP3", "本地IP地址3", 1, 30);
            AddNewValue(2, "RemoteIP3", "远方IP地址3", 1, 30);
            AddNewValue(2, "Protocol", "规约类型", 1, 30, strPtrList);
            AddNewValue(2, "ProtocolCfgFile", "规约配置文件", 1, 30);
            AddNewValue(2, "PrintFrameTerm", "是否打印报文", 1, 30, strBoolList);
            AddNewValue(2, "PrintStatusTerm", "是否打印状态", 1, 30, strBoolList);
            AddNewValue(2, "CommChannelStatusLog", "通讯通道状态记录", 1, 50);
            AddNewValue(2, "CommChannelStatusType", "通讯通道状态类型", 1, 50);
            AddNewValue(2, "CommChannelStatusLimit", "通讯通道状态限制", 1, 50);
            AddNewValue(2, "CommChannelFrameLog", "通道报文记录", 1, 50);
            AddNewValue(2, "CommChannelFrameType", "通道报文类型", 1, 50);
            AddNewValue(2, "CommChannelFrameLimit", "通讯通道状态限制", 1, 50);
            AddNewValue(2, "SynImm", "是否触发对时", 1, 30, strBoolList);
            AddNewValue(2, "CallImm", "是否触发总召", 1, 30, strBoolList);
            AddNewValue(0, "YxDB", "遥信点表", 2);
            AddNewValue(0, "YcDB", "遥测点表", 2);
            AddNewValue(0, "YkDB", "遥控点表", 2);
            AddNewValue(0, "SetDB", "定值点表", 2);
            AddNewValue(0, "PowerDB", "电度量点表", 2);
            AddNewValue(2, "YxRouteTab", "遥信转发表名称", 1, 30,StrListYxRouteTable);
            AddNewValue(0, "UseYxTabIndex", "是否使用已建立的点表", 1);
            AddNewValue(2, "YcRouteTab", "遥测转发表名称", 1, 30, StrListYcRouteTable);
            AddNewValue(0, "UseYcTabIndex", "是否使用已建立的点表", 1);
            AddNewValue(2, "YkRouteTab", "遥控转发表名称", 1, 30, StrListYkRouteTable);
            AddNewValue(0, "UseYkTabIndex", "是否使用已建立的点表", 1);
            AddNewValue(2, "SetRouteTab", "定值转发表名称", 1, 30, StrListSetRouteTable);
            AddNewValue(0, "UseSetTabIndex", "是否使用已建立的点表", 1);
            AddNewValue(2, "PowerRouteTab", "遥脉转发表名称", 1, 30, StrListPowerRouteTable);
            AddNewValue(0, "UsePowerTabIndex", "是否使用已建立的点表", 1);
            AddNewValue(2, "ActiveCommYX", "是否触发遥信变位主动上送", 1, 30, strBoolList);
            AddNewValue(2, "AcceptSynTime", "SOE是否接受对时", 1, 30, strBoolList);
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
                for (int i = 0; i < pStrList.Length; i++) pNewValue.m_ComStringList[i] = pStrList[i];
                pNewValue.m_EditMode = true;
            }
            m_ValueList.Add(pNewValue);
        }
        public void IniReadStationTable(int Row)
        {
            string strSql;
            DataTable pTable;
            int i;
            strSql = "SELECT  * FROM  PriStationNodeCfg order by NodeNo";
            pTable = (DataTable)m_PriRoot.m_TheStation.m_MainSqlite.SelectDataTable(strSql);
            if (pTable == null) return;
            for (i = 0; i < m_ValueList.Count; i++)
            {
                CTranValue pData = m_ValueList[i];
                string strValue = m_PriRoot.m_TheStation.m_MainSqlite.GetTableString(pTable, Row, i);
                pData.m_TranValue = strValue;
            }
        }
    }



    public class CPriRoot
    {
        public CTransStation m_TheStation = null;
        public TreeNode m_Node = null;
        public List<CPriConfig> m_PriList = new List<CPriConfig>();
        public List<string> m_YxRouteTableList = new List<string>();
        public List<string> m_YcRouteTableList = new List<string>();
        public List<string> m_YkRouteTableList = new List<string>();
        public List<string> m_SetRouteTableList = new List<string>();
        public List<string> m_PowerRouteTableList = new List<string>();


        public static Dictionary<int, int> m_PriNoList = new Dictionary<int, int>();
        public CPriRoot()
        {

        }
        public void LoadPriStationSet()
        {
            if (m_TheStation.m_MainSqlite == null) return;
            string strSql;
            DataTable pTable;
            strSql = "SELECT  * FROM  PriStationNodeCfg order by NodeNo";
            pTable = (DataTable)m_TheStation.m_MainSqlite.SelectDataTable(strSql);
            m_PriList.Clear();
            if (pTable == null) return;
            for (int i = 0; i < pTable.Rows.Count; i++)
            {
                CPriConfig pNewDevice = new CPriConfig();
                pNewDevice.m_PriRoot = this; 
                pNewDevice.IniValueList();
                pNewDevice.IniReadStationTable(i);
                pNewDevice.m_TermNo = Convert.ToInt32(pNewDevice.m_ValueList[1].m_TranValue);
                int NodeNo = Convert.ToInt32(pNewDevice.m_ValueList[1].m_TranValue);
                if (m_PriNoList.ContainsKey(NodeNo) == false) m_PriNoList.Add(NodeNo, NodeNo);
                pNewDevice.IniAllDataTableName();
                m_PriList.Add(pNewDevice);
            }
        }
        public static int GetPriUseNo(Dictionary<int, int> pMap)
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

        private void AddDeviceRecord(CPriConfig pNewDevice)
        {
            string strDataContent = "";
            int i, nCount = 0;
            string strSql = "";
            strSql = "insert into PriStationNodeCfg values(";
            for (i = 0; i < pNewDevice.m_ValueList.Count; i++)
            {
                CTranValue pData = pNewDevice.m_ValueList[i];              
                if (nCount > 0) strDataContent += ",";
                strDataContent += pData.GetInsertValueString();
                nCount++;
            }           
            if (nCount == 0) return;
            strSql += strDataContent;
            strSql += ")";
            m_TheStation.m_MainSqlite.ExecSql(strSql);
        }
        public CPriConfig AddNewPri()
        {
           // string strName = Interaction.InputBox("请输入新建转发配置名称", "名称", "", 100, 100);
           // strName = strName.Trim();
          //  if (strName.Length == 0) return null;
            CPriConfig pNewDevice = new CPriConfig();          
            pNewDevice.m_PriRoot = this;
            pNewDevice.IniValueList();

            for (int i = 0; i < pNewDevice.m_ValueList.Count; i++)
            {
                pNewDevice.m_ValueList[i].m_TranValue = CTransGlobal.m_DefaultPriValueList[i].m_TranValue;
            }                
            pNewDevice.m_ValueList[1].m_TranValue = GetPriUseNo(m_PriNoList);
            pNewDevice.m_ValueList[0].m_TranValue = Convert.ToInt32(pNewDevice.m_ValueList[1].m_TranValue) + 1;
            AddDeviceRecord(pNewDevice);       
            m_PriList.Add(pNewDevice);
            return pNewDevice;
        }
        public CPriConfig CopyPri(CPriConfig pBasicPri)
        {
            if (pBasicPri == null) return null;
            //string strName = Interaction.InputBox("请输入拷贝后转发配置名称", "名称", "", 100, 100);
            //strName = strName.Trim();
          //  if (strName.Length == 0) return null;
            CPriConfig pNewDevice = new CPriConfig();
            pNewDevice.m_PriRoot = this;

            pNewDevice.m_ValueList = pBasicPri.m_ValueList;
            pNewDevice.m_ValueList[1].m_TranValue = GetPriUseNo(m_PriNoList);
            pNewDevice.m_ValueList[0].m_TranValue = Convert.ToInt32(pNewDevice.m_ValueList[1].m_TranValue) + 1;
            AddDeviceRecord(pNewDevice);
            m_PriList.Add(pNewDevice);
            return pNewDevice;
        }
        public void DeletePri(CPriConfig pBasicTerm)
        {
            if (pBasicTerm == null) return;
            pBasicTerm.DropTable();
            m_PriList.Remove(pBasicTerm);
            int NodeNo = Convert.ToInt32(pBasicTerm.m_ValueList[1].m_TranValue);
            m_PriNoList.Remove(NodeNo);
        }
      
    }

   
    
}
