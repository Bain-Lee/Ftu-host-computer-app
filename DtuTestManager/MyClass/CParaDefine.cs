using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Data;
using Microsoft.VisualBasic;

namespace DtuTestManager
{
    public class CParaDefine
    {
        public CDataBase m_ParaDB = null;
        public virtual  void    UpdateName(string strName)
        {
            
        }
    }
    public class CParaCtrl
    {
        public int m_ParaListType;      //参数表类型
        public int m_ParaNo;            //参数序号
        public int m_ParaType;          //参数类型
        public int m_ParaAddress;       //参数地址
        public string m_ParaName;       //参数名称
        public int m_ParaLeng;          //参数长度
        public string m_ParaUnit;       //参数单位
        public string m_ParaLimit;      //上下限描述
        public bool m_Select = false;   //是否选择
        public bool m_bShow = true;     //是否显示
        public bool m_Fresh = false;    //是否刷新
        private object pValue;


        public CParaCtrl()
        {
        }
        public CParaCtrl(CParaCtrl pCopyPara)
        {
            m_ParaListType = pCopyPara.m_ParaListType;
            m_ParaNo = pCopyPara.m_ParaNo;
            m_ParaType = pCopyPara.m_ParaType;
            m_ParaAddress = pCopyPara.m_ParaAddress;
            m_ParaName = pCopyPara.m_ParaName;
            m_ParaLeng = pCopyPara.m_ParaLeng;
            m_ParaUnit = pCopyPara.m_ParaUnit;
            m_ParaLimit = pCopyPara.m_ParaLimit;
            m_ParaLimit = pCopyPara.m_ParaLimit;
            m_ParaValue = pCopyPara.m_ParaValue;
        }
        public object m_ParaValue       //参数值
        {
            set
            {
                try
                {
                    switch (m_ParaType)
                    {
                        case 1:         //bool，转为Byte
                            pValue = Convert.ChangeType(value, typeof(Byte));
                            m_ParaLeng = 1;
                            break;
                        case 2:         //int
                            pValue = Convert.ChangeType(value, typeof(int));
                            m_ParaLeng = 4;
                            break;
                        case 4:         //string
                            pValue = Convert.ChangeType(value, typeof(string));
                            m_ParaLeng = ((string)pValue).Length;
                            break;
                        case 32:        //Byte
                            pValue = Convert.ChangeType(value, typeof(Byte));
                            m_ParaLeng = 1;
                            break;
                        case 33:        //short
                            pValue = Convert.ChangeType(value, typeof(short));
                            m_ParaLeng = 2;
                            break;
                        case 43:        //sbyte
                            pValue = Convert.ChangeType(value, typeof(sbyte));
                            m_ParaLeng = 1;
                            break;
                        case 45:        //ushort
                            pValue = Convert.ChangeType(value, typeof(ushort));
                            m_ParaLeng = 2;
                            break;
                        case 35:        //uint
                            pValue = Convert.ChangeType(value, typeof(uint));
                            m_ParaLeng = 4;
                            break;
                        case 36:        //long
                            pValue = Convert.ChangeType(value, typeof(long));
                            m_ParaLeng = 8;
                            break;
                        case 37:        //ulong
                            pValue = Convert.ChangeType(value, typeof(ulong));
                            m_ParaLeng = 8;
                            break;
                        case 38:        //float
                            pValue = Convert.ChangeType(value, typeof(float));
                            m_ParaLeng = 4;
                            break;
                        case 39:        //double
                            pValue = Convert.ChangeType(value, typeof(double));
                            m_ParaLeng = 8;
                            break;
                        default:
                            pValue = Convert.ChangeType(value, typeof(string));
                            m_ParaLeng = ((string)pValue).Length;
                            break;
                    }
                }
                catch { 
                
                
                }
            }
            get
            {
                try
                {
                    switch (m_ParaType)
                    {
                        case 1:         //bool，转为Byte
                            return (Byte)pValue;
                        case 2:         //int
                            return (int)pValue;
                        case 4:         //string
                            return (string)pValue;
                        case 32:        //Byte
                            return (Byte)pValue;
                        case 33:        //short
                            return (short)pValue;
                        case 43:        //sbyte
                            return (sbyte)pValue;
                        case 45:        //ushort
                            return (ushort)pValue;
                        case 35:        //uint
                            return (uint)pValue;
                        case 36:        //long
                            return (long)pValue;
                        case 37:        //ulong
                            return (ulong)pValue;
                        case 38:        //float
                            return (float)pValue;
                        case 39:        //double
                            return (double)pValue;
                        default:
                            return (string)pValue;
                    }
                }
                catch { return (string)pValue; }
            }
        }
        public string GetValueTypeName()
        {
            switch (m_ParaType)
            {
                case 1:         //bool，转为Byte
                    return "bool";
                case 2:         //int
                    return "int";
                case 4:         //string
                    return "string";
                case 32:        //Byte
                    return "Byte";
                case 33:        //short
                    return "short";
                case 43:        //sbyte
                    return "sbyte";
                case 45:        //ushort
                    return "ushort";
                case 35:        //uint
                    return "uint";
                case 36:        //long
                    return "long";
                case 37:        //ulong
                    return "ulong";
                case 38:        //float
                    return "float";
                case 39:        //double
                    return "double";
                default:
                    return "string";
            }
        }
        public int GetValueBuff(ref Byte[] pBuff)
        {
            switch (m_ParaType)
            {
                case 1:         //bool，转为Byte
                    pBuff = System.BitConverter.GetBytes((Byte)m_ParaValue);
                    break;
                case 2:         //int
                    pBuff = System.BitConverter.GetBytes((int)m_ParaValue);
                    break;
                case 4:         //string
                    pBuff = Encoding.Default.GetBytes((string)m_ParaValue);
                    break;
                case 32:        //Byte
                    pBuff = System.BitConverter.GetBytes((Byte)m_ParaValue);
                    break;
                case 33:        //short
                    pBuff = System.BitConverter.GetBytes((short)m_ParaValue);
                    break;
                case 43:        //sbyte
                    pBuff = System.BitConverter.GetBytes((sbyte)m_ParaValue);
                    break;
                case 45:        //ushort
                    pBuff = System.BitConverter.GetBytes((ushort)m_ParaValue);
                    break;
                case 35:        //uint
                    pBuff = System.BitConverter.GetBytes((uint)m_ParaValue);
                    break;
                case 36:        //long
                    pBuff = System.BitConverter.GetBytes((long)m_ParaValue);
                    break;
                case 37:        //ulong
                    pBuff = System.BitConverter.GetBytes((ulong)m_ParaValue);
                    break;
                case 38:        //float
                    pBuff = System.BitConverter.GetBytes((float)m_ParaValue);
                    break;
                case 39:        //double
                    pBuff = System.BitConverter.GetBytes((double)m_ParaValue);
                    break;
                default:
                    pBuff = Encoding.Default.GetBytes((string)m_ParaValue);
                    break;
            }
            return m_ParaLeng;
        }

    }
    public class CParaGroupTable:CParaDefine
    {
        public int m_GroupNo;
        public int m_RootIndex;
        public string m_GroupName;
        public int m_GroupIndex;
        public string m_TableName;
        public string m_GroupTableName;
        public int m_NowSelectIndex;
        public bool m_bShow = true;
        public int m_StartAddress = 1;
        public MyListView m_ListView = null;
        public List<CParaCtrl> m_ParaDataList = new List<CParaCtrl>();
        public List<CParaCtrl> m_ResetList = new List<CParaCtrl>();

        public TreeNode m_GroupNode = null;
        string[] strParaTypeList = new string[12];

        public CParaGroupTable()
        {
            strParaTypeList[0] = "01.bool(1字节布尔类型)";
            strParaTypeList[1] = "04.string(字符串)";
            strParaTypeList[2] = "32.Byte(1字节无符号整型)";
            strParaTypeList[3] = "43.char(1字节有符号整型)";
            strParaTypeList[4] = "33.short(2字节有符号整型)";
            strParaTypeList[5] = "45.ushort(2字节无符号整型)";
            strParaTypeList[6] = "02.int(4字节有符号整型)";
            strParaTypeList[7] = "35.uint(4字节无符号整型)";
            strParaTypeList[8] = "36.long(8字节有符号整型)";
            strParaTypeList[9] = "37.ulong(8字节无符号整型)";
            strParaTypeList[10] = "38.float(4字节短浮点数)";
            strParaTypeList[11] = "39.doublt(8字节双精度数)";
        }
        public override void UpdateName(string strName)
        {
            string strSql;
            m_GroupName = strName;
            m_GroupNode.Text = m_GroupName;

            strSql = "update " + m_GroupTableName + " set ParaGroupName='" + m_GroupName + "' where(GroupIndex=" + m_GroupIndex + ")";
            m_ParaDB.ExecSql(strSql);
        }
        public void SetNowSelectValue(int nIndex, object pData)
        {
            if (m_NowSelectIndex >= 0 && m_NowSelectIndex < m_ParaDataList.Count)
            {
                switch (nIndex)
                {
                    case 1:
                        m_ParaDataList[m_NowSelectIndex].m_ParaName = (string)pData;
                        break;
                    case 2:
                        m_ParaDataList[m_NowSelectIndex].m_ParaAddress = Convert.ToInt32((string)pData);
                        break;
                    case 3:
                        string strValue = m_ParaDataList[m_NowSelectIndex].m_ParaValue.ToString();
                        m_ParaDataList[m_NowSelectIndex].m_ParaType = GetIndexDataType((string)pData);
                        m_ParaDataList[m_NowSelectIndex].m_ParaValue = strValue;       
                        break;
                    case 4:

                        m_ParaDataList[m_NowSelectIndex].m_ParaValue = (string)pData;
                        break;
                    case 5:
                        m_ParaDataList[m_NowSelectIndex].m_ParaUnit = (string)pData;
                        break;
                    case 6:
                        m_ParaDataList[m_NowSelectIndex].m_ParaLimit = (string)pData;
                        break;
                }
            }
        }
        public void ResetAddress()
        {
            for (int i = 0; i < m_ParaDataList.Count; i++)
            {
                m_ParaDataList[i].m_ParaAddress = m_StartAddress + i;
                m_ListView.Items[i].SubItems[2].Text = m_ParaDataList[i].m_ParaAddress.ToString();
            }
        }
        public void IniReadData(string strTableName)
        {
            string strParaSql;
            DataTable pParaTable;
            m_TableName = strTableName;
            int i;
            strParaSql = "SELECT  *  FROM " + strTableName + " where(ParaTableID=" + m_GroupIndex.ToString() + ") order by ParaID";
            pParaTable = (DataTable)m_ParaDB.SelectDataTable(strParaSql);
            if (pParaTable == null) return;
            for (i = 0; i < pParaTable.Rows.Count; i++)
            {
                CParaCtrl pPara = new CParaCtrl();
                pPara.m_ParaListType = m_ParaDB.GetTableIntValue(pParaTable, i, 0); ;
                pPara.m_ParaNo = m_ParaDB.GetTableIntValue(pParaTable, i, 1);
                pPara.m_ParaAddress = m_ParaDB.GetTableIntValue(pParaTable, i, 2);
                pPara.m_ParaName = m_ParaDB.GetTableString(pParaTable, i, 3);
                pPara.m_ParaType = m_ParaDB.GetTableIntValue(pParaTable, i, 5);
                pPara.m_ParaValue = m_ParaDB.GetTableString(pParaTable, i, 4);
                pPara.m_ParaUnit = m_ParaDB.GetTableString(pParaTable, i, 6);
                pPara.m_ParaLimit = m_ParaDB.GetTableString(pParaTable, i, 7);
                pPara.m_Select = m_ParaDB.GetTableBoolValue(pParaTable, i, 8);
                m_ParaDataList.Add(pPara);
                if (CGlobal.m_Device.m_TotalParaDataList.Count > 1)
                {
                    if (pPara.m_ParaAddress < CGlobal.m_Device.m_TotalParaDataList[0].m_ParaAddress)
                    {
                        CGlobal.m_Device.m_TotalParaDataList.Insert(0, pPara);                        
                    }
                    else if (pPara.m_ParaAddress > CGlobal.m_Device.m_TotalParaDataList[CGlobal.m_Device.m_TotalParaDataList.Count - 1].m_ParaAddress)
                    {
                        CGlobal.m_Device.m_TotalParaDataList.Add(pPara);                        
                    }
                    else
                    {
                        int m_Same_Index = 0;
                        int nIndex = BinarySearch(0, CGlobal.m_Device.m_TotalParaDataList.Count, pPara.m_ParaAddress, ref m_Same_Index);
                        if (nIndex > -1)
                        {
                            CGlobal.m_Device.m_TotalParaDataList.Insert(nIndex, pPara);                           
                        }                       
                    }
                }
                else
                {
                    CGlobal.m_Device.m_TotalParaDataList.Add(pPara);                  
                }               
                if (i == 0) m_StartAddress = pPara.m_ParaAddress;
                if (m_StartAddress > pPara.m_ParaAddress) m_StartAddress = pPara.m_ParaAddress;
                CParaCtrl pOldPara = new CParaCtrl(pPara);
                m_ResetList.Add(pOldPara);
            }
        }
        public int BinarySearch(int low, int high, int key, ref int m_SameIndex)
        {
            int mid = (low + high) / 2;
            if (low > high)
                return -1;
            else
            {
                if (CGlobal.m_Device.m_TotalParaDataList[mid].m_ParaAddress == key)
                {
                    m_SameIndex = mid;
                    return -1;
                }
                else if (CGlobal.m_Device.m_TotalParaDataList[mid].m_ParaAddress > key)
                {
                    if (CGlobal.m_Device.m_TotalParaDataList[mid - 1].m_ParaAddress < key)
                    {
                        return mid;
                    }
                    else
                    {
                        return BinarySearch(low, mid - 1, key, ref m_SameIndex);
                    }
                }
                else
                {
                    if (CGlobal.m_Device.m_TotalParaDataList[mid + 1].m_ParaAddress > key)
                    {
                        return mid + 1;
                    }
                    else
                    {
                        return BinarySearch(mid + 1, high, key, ref m_SameIndex);
                    }
                }
            }
        }
        public void IniCreatNode()
        {
            m_GroupNode = new TreeNode();
            m_GroupNode.Text = m_GroupName;
            m_GroupNode.Tag = this;
        }
        private int GetIndexDataType(string strIndex)
        {
            string strDataType;
            strDataType = strIndex.Split('.')[0];
            return Convert.ToInt32(strDataType);
        }
        private int GetListIndex(int nDataType)
        {
            string strDataType;
            strDataType = nDataType.ToString("D2");
            int i;
            for (i = 0; i < strParaTypeList.Length; i++)
            {
                if (strParaTypeList[i].IndexOf(strDataType) == 0)
                {
                    return i;
                }
            }
            return 0;
        }
        public void IniShowListView(MyListView pListView)
        {
            m_ListView = pListView;
            pListView.ClearAllItem();
            pListView.GridLines = true;
            pListView.FullRowSelect = true;
            pListView.View = View.Details;
            pListView.Scrollable = true;
            pListView.MultiSelect = false;
            pListView.HeaderStyle = ColumnHeaderStyle.Clickable;
            pListView.Visible = true;

            pListView.Columns.Add("序号", 60, HorizontalAlignment.Center);
            pListView.Columns.Add("参数名称", 240, HorizontalAlignment.Left);
            pListView.Columns.Add("参数地址", 60, HorizontalAlignment.Left);
            pListView.Columns.Add("参数类型", 200, HorizontalAlignment.Left);
            pListView.Columns.Add("默认值", 100, HorizontalAlignment.Left);
            pListView.Columns.Add("参数单位", 60, HorizontalAlignment.Left);
            pListView.Columns.Add("参数说明", 240, HorizontalAlignment.Left);

            pListView.AddComBoxListStr(strParaTypeList);

            string[] strHead = new string[7];
            for (int i = 0; i < m_ParaDataList.Count; i++)
            {
                try
                {
                    CParaCtrl pData = m_ParaDataList[i];
                    pData.m_ParaNo = i + 1;
                    strHead[0] = pData.m_ParaNo.ToString();
                    strHead[1] = pData.m_ParaName;
                    strHead[2] = pData.m_ParaAddress.ToString();
                    strHead[3] = strParaTypeList[GetListIndex(pData.m_ParaType)];
                    strHead[4] = pData.m_ParaValue.ToString();
                    strHead[5] = pData.m_ParaUnit;
                    strHead[6] = pData.m_ParaLimit;
                    ListViewItem pItem = new ListViewItem(strHead);
                    pListView.AddItem(pItem);
                }
                catch { }
            }

            pListView.SetColumnsEditMode(1);
            pListView.SetColumnsEditMode(2);
            pListView.SetColumnsComBoxMode(3, 0);
            pListView.SetColumnsEditMode(4);
            pListView.SetColumnsEditMode(5);
            pListView.SetColumnsEditMode(6);
        }
        private void SaveParaData(CParaCtrl pPara)
        {
            string strSql;
            strSql = "insert into " + m_TableName + " values(" +
                pPara.m_ParaListType.ToString() + "," +
                pPara.m_ParaNo.ToString() + "," +
                pPara.m_ParaAddress.ToString() + ",'" +
                pPara.m_ParaName + "','" +
                pPara.m_ParaValue.ToString() + "'," +
                pPara.m_ParaType.ToString() + ",'" +
                pPara.m_ParaUnit + "','" +
                pPara.m_ParaLimit + "',0)";
            m_ParaDB.ExecSql(strSql);
        }
        private int GetNowAddress()
        {
            if (m_ParaDataList.Count == 0) return m_StartAddress;
            return m_ParaDataList[m_ParaDataList.Count - 1].m_ParaAddress + 1;
        }
        public void AddNewParaToList(CParaCtrl pPara)
        {
            m_ParaDataList.Add(pPara);

            string[] strHead = new string[7];
            strHead[0] = pPara.m_ParaNo.ToString();
            strHead[1] = pPara.m_ParaName;
            strHead[2] = pPara.m_ParaAddress.ToString();
            strHead[3] = strParaTypeList[GetListIndex(pPara.m_ParaType)];
            strHead[4] = pPara.m_ParaValue.ToString();
            strHead[5] = pPara.m_ParaUnit;
            strHead[6] = pPara.m_ParaLimit;
            ListViewItem pItem = new ListViewItem(strHead);
            m_ListView.AddItem(pItem);

            int nTopIndex = m_ListView.TopItem.Index;
            IniShowListView(m_ListView);
            if (nTopIndex < (m_ListView.Items.Count-1)) m_ListView.TopItem = m_ListView.Items[nTopIndex + 1];
        }
        public void AddNewParaData()
        {
            CParaCtrl pPara = new CParaCtrl();
            pPara.m_ParaListType = m_GroupIndex;
            pPara.m_ParaNo = m_ParaDataList.Count + 1;
            pPara.m_ParaAddress = GetNowAddress();
            pPara.m_ParaName = "新建参数";
            pPara.m_ParaType = 1;
            pPara.m_ParaValue = 0;
            pPara.m_ParaUnit = "";
            pPara.m_ParaLimit = "";
            pPara.m_Select = false;
            AddNewParaToList(pPara);
        }
        public void AddModulePara(CChildModule pData, string strNameHead)
        {
            CParaCtrl pPara = new CParaCtrl();
            pPara.m_ParaListType = m_GroupIndex;
            pPara.m_ParaNo = m_ParaDataList.Count + 1;
            pPara.m_ParaAddress = GetNowAddress();
            pPara.m_ParaName = strNameHead + pData.m_DataName;
            pPara.m_ParaType = pData.m_DataType;
            pPara.m_ParaValue = 0;
            pPara.m_ParaUnit = pData.m_DataUnit;
            pPara.m_ParaLimit = pData.m_DataDesc;
            pPara.m_Select = false;
            AddNewParaToList(pPara);
        }
        public void InsertParaData(int nIndex)
        {
            if (nIndex < 0 || nIndex >= m_ParaDataList.Count) return;
            CParaCtrl pPara = new CParaCtrl();
            pPara.m_ParaListType = m_GroupIndex;
            pPara.m_ParaNo = m_ParaDataList.Count + 1;
            pPara.m_ParaAddress = GetNowAddress();
            pPara.m_ParaName = "新建参数";
            pPara.m_ParaType = 1;
            pPara.m_ParaValue = 0;
            pPara.m_ParaUnit = "";
            pPara.m_ParaLimit = "";
            pPara.m_Select = false;
            m_ParaDataList.Insert(nIndex, pPara);

            int nTopIndex = m_ListView.TopItem.Index;
            IniShowListView(m_ListView);
            m_ListView.Items[nIndex + 1].Selected = true;
            m_ListView.TopItem = m_ListView.Items[nTopIndex];
        }
        public void DeleteParaData(int nIndex)
        {
            if (nIndex < 0 || nIndex >= m_ParaDataList.Count) return;
            m_ParaDataList.RemoveAt(nIndex);
            int nTopIndex = m_ListView.TopItem.Index;
            IniShowListView(m_ListView);
            if (nIndex < m_ParaDataList.Count)
                m_ListView.Items[nIndex].Selected = true;
            m_ListView.TopItem = m_ListView.Items[nTopIndex];
        }
        public void ClearList()
        {
            m_ParaDataList.Clear();
            IniShowListView(m_ListView);
        }
        public void ClearAllData()
        {
            string strSql;
            strSql = "delete from " + m_TableName + " where(ParaTableID=" + m_GroupIndex.ToString() + ")";
            m_ParaDB.ExecSql(strSql);
            strSql = "delete from " + m_GroupTableName + " where(ParaGroupNo=" + m_GroupIndex.ToString() + ")";
            m_ParaDB.ExecSql(strSql);
            m_ParaDataList.Clear();
            m_GroupNode.Remove();
        }
        public void SaveParaSet()
        {
            string strSql;
           strSql = "delete from " + m_TableName + " where(ParaTableID=" + m_GroupIndex.ToString() + ")";
          m_ParaDB.ExecSql(strSql);
            for (int i = 0; i < m_ParaDataList.Count; i++)
            {
                SaveParaData(m_ParaDataList[i]);
            }
        }
        public void ResetParaList()
        {
            int  i;
            m_ParaDataList.Clear();
            for (i = 0; i < m_ResetList.Count; i++)
            {
                CParaCtrl pPara = new CParaCtrl(m_ResetList[i]);
                m_ParaDataList.Add(pPara);
                if (i == 0) m_StartAddress = pPara.m_ParaAddress;
                if (m_StartAddress > pPara.m_ParaAddress) m_StartAddress = pPara.m_ParaAddress;
            }
            IniShowListView(m_ListView);
        }
    }
    public class CParaRootTable : CParaDefine
    {
        public int m_RootNo;
        public string m_RootName;
        public int m_RootIndex;
        public string m_GroupTableName;
        public string m_DataTableName;
        public string m_RootTableName;
        public List<CParaGroupTable> m_GroupList = new List<CParaGroupTable>();
        public Dictionary<int, int> m_ParaGroupNo = null;
        public TreeNode m_RootNode = null;

        public void IniReadGroup(string strTableName, string strChildTable)
        {
            string strGroupSql;
            DataTable pGroupTable;
            int i;
            m_GroupTableName = strTableName;
            m_DataTableName = strChildTable;
            strGroupSql = "SELECT  *  FROM " + m_GroupTableName + " where(RootIndex=" + m_RootIndex.ToString() + ") order by ParaGroupNo";
            pGroupTable = (DataTable)m_ParaDB.SelectDataTable(strGroupSql);
            if (pGroupTable == null) return;
            for (i = 0; i < pGroupTable.Rows.Count; i++)
            {
                CParaGroupTable pNewGroup = new CParaGroupTable();
                pNewGroup.m_ParaDB = m_ParaDB;
                pNewGroup.m_GroupNo = m_ParaDB.GetTableIntValue(pGroupTable, i, 0);
                pNewGroup.m_GroupName = m_ParaDB.GetTableString(pGroupTable, i, 1);
                pNewGroup.m_RootIndex = m_ParaDB.GetTableIntValue(pGroupTable, i, 2);
                pNewGroup.m_GroupIndex = m_ParaDB.GetTableIntValue(pGroupTable, i, 3);
                pNewGroup.m_GroupTableName = m_GroupTableName;

                pNewGroup.IniCreatNode();
                pNewGroup.IniReadData(strChildTable);
                m_GroupList.Add(pNewGroup);
                if (m_ParaGroupNo != null) m_ParaGroupNo.Add(pNewGroup.m_GroupNo, pNewGroup.m_GroupNo);
            }
        }
        public override void UpdateName(string strName)
        {
            string strSql;
            m_RootName = strName;
            m_RootNode.Text = m_RootName;

            strSql = "update " + m_RootTableName + " set RootName='" + m_RootName + "' where(RootIndex=" + m_RootIndex + ")";
            CGlobal.m_DataBase.ExecSql(strSql);
        }
        public void IniCreatNode()
        {
            m_RootNode = new TreeNode();
            m_RootNode.Text = m_RootName;
            m_RootNode.Tag = this;
        }
        public void AddNewParaGroup()
        {
            string strName = Interaction.InputBox("请输入参数组名称", "名称", "", 100, 100);
            strName = strName.Trim();
            if (strName.Length == 0) return;

            CParaGroupTable pNewGroup = new CParaGroupTable();
            pNewGroup.m_ParaDB = m_ParaDB;
            pNewGroup.m_GroupNo = CGlobal.GetCanuUseNo(m_ParaGroupNo);
            pNewGroup.m_GroupName = strName;
            pNewGroup.m_RootIndex = m_RootIndex;
            pNewGroup.m_GroupIndex = pNewGroup.m_GroupNo;
            pNewGroup.m_TableName = m_DataTableName;
            pNewGroup.IniCreatNode();
            m_GroupList.Add(pNewGroup);
            m_RootNode.Nodes.Add(pNewGroup.m_GroupNode);

            string strSql;
            strSql = "insert into " + m_GroupTableName + " values(" +
                pNewGroup.m_GroupNo.ToString() + ",'" +
                pNewGroup.m_GroupName + "'," +
                pNewGroup.m_RootIndex.ToString() + "," +
                pNewGroup.m_GroupIndex.ToString() + ")";
            m_ParaDB.ExecSql(strSql);
        }
        public void DeleteParaGroup(CParaGroupTable pDelGroup)
        {
            if (pDelGroup == null) return;
            m_GroupList.Remove(pDelGroup);
            m_ParaGroupNo.Remove(pDelGroup.m_GroupNo);
            pDelGroup.ClearAllData();
        }
        public void ClearAll()
        {
            while (m_GroupList.Count > 0)
            {
                DeleteParaGroup(m_GroupList[0]);
            }
            string strSql;
            strSql = "delete from " + m_GroupTableName + " where(RootIndex=" + m_RootIndex.ToString() + ")";
            m_ParaDB.ExecSql(strSql);
            m_RootNode.Remove();
        }
    }
    public class CParaListOperation
    {
        public Dictionary<int, int> m_ParaRootNo = new Dictionary<int, int>();
        public Dictionary<int, int> m_ParaGroupNo = new Dictionary<int, int>();
        public List<CParaRootTable> m_ParaRootList = new List<CParaRootTable>();
        public CDataBase m_ParaDB = null;
        public string m_strTootTable;
        public string m_strGroupTable;
        public string m_strDataTable;
        public TreeView m_MyTreeView = null;
        public void InitialzeReadPara()
        {
            string strRootSql;
            DataTable pRootTable;
            int i;
            m_ParaRootList.Clear();

            strRootSql = "SELECT  *  FROM " + m_strTootTable + "  order by RootNo";
            pRootTable = (DataTable)m_ParaDB.SelectDataTable(strRootSql);
            if (pRootTable == null) return;
            for (i = 0; i < pRootTable.Rows.Count; i++)
            {
                CParaRootTable pNewRoot = new CParaRootTable();
                pNewRoot.m_ParaDB = m_ParaDB;
                pNewRoot.m_RootNo = m_ParaDB.GetTableIntValue(pRootTable, i, 0);
                pNewRoot.m_RootName = m_ParaDB.GetTableString(pRootTable, i, 1);
                pNewRoot.m_RootIndex = m_ParaDB.GetTableIntValue(pRootTable, i, 2);
                pNewRoot.m_ParaGroupNo = m_ParaGroupNo;
                pNewRoot.m_RootTableName = m_strTootTable;
                pNewRoot.IniCreatNode();
                pNewRoot.IniReadGroup(m_strGroupTable, m_strDataTable);
                m_ParaRootList.Add(pNewRoot);
                m_ParaRootNo.Add(pNewRoot.m_RootNo, pNewRoot.m_RootNo);
            }
        }
        public void AddNewParaRoot()
        {
            string strName = Interaction.InputBox("请输入参数类组名称", "名称", "", 100, 100);
            strName = strName.Trim();
            if (strName.Length == 0) return;

            CParaRootTable pNewRoot = new CParaRootTable();
            pNewRoot.m_ParaDB = m_ParaDB;
            pNewRoot.m_RootNo = CGlobal.GetCanuUseNo(m_ParaRootNo);
            pNewRoot.m_RootName = strName;
            pNewRoot.m_ParaGroupNo = m_ParaGroupNo;
            pNewRoot.m_RootIndex = pNewRoot.m_RootNo;
            pNewRoot.m_GroupTableName = m_strGroupTable;
            pNewRoot.m_DataTableName = m_strDataTable;

            pNewRoot.IniCreatNode();
            m_ParaRootList.Add(pNewRoot);
            m_MyTreeView.Nodes.Add(pNewRoot.m_RootNode);

            string strSql;
            strSql = "insert into " + m_strTootTable + " values(" +
                pNewRoot.m_RootNo.ToString() + ",'" +
                pNewRoot.m_RootName + "'," +
                pNewRoot.m_RootIndex.ToString() + ")";
            m_ParaDB.ExecSql(strSql);
        }
        public void DeleteParaRoot(CParaRootTable pDelRoot)
        {
            if (pDelRoot == null) return;
            if (MessageBox.Show("将删除此参数组及对应的数据，删除后无法恢复，确认删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                m_ParaRootNo.Remove(pDelRoot.m_RootNo);
                m_ParaRootList.Remove(pDelRoot);
                pDelRoot.ClearAll();
                string strSql;
                strSql = "delete from " + m_strTootTable + " where(RootIndex=" + pDelRoot.m_RootIndex.ToString() + ")";
                m_ParaDB.ExecSql(strSql);
            }
        }
    }
}
