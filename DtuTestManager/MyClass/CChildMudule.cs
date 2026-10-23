using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace DtuTestManager
{
    public class CChildModule
    {
        public int m_DataNo;
        public int m_DataType;
        public string m_DataName;
        public string m_DataUnit;
        public string m_DataDesc;
        
        public CChildModule()
        {

        }
    }
    public class CGroupInterface
    {
        public int m_ModuleNo;
        public int m_ModuleType;
        public string m_ModuleName;
        public int m_ModuleIndex;
        public CChildModuleRoot m_Root = null;
        public TreeNode m_Nodes;
        public string strTableName;
        public MyListView m_ListView = null;
        public int m_NowSelectIndex = -1;
        public List<CChildModule> m_DataList = new List<CChildModule>();
        public CGroupInterface()
        {
            m_Nodes = new TreeNode();
            m_Nodes.Tag = this;
        }
        public void CreateTreeNode()
        {
            m_Nodes.Text = m_ModuleName;
        }
        public virtual void IniReadChildModule()
        {

        }
        public virtual void IniShowListView(MyListView pListView)
        {
            m_ListView = pListView;
            pListView.GridLines = true;
            pListView.FullRowSelect = true;
            pListView.View = View.Details;
            pListView.Scrollable = true;
            pListView.MultiSelect = false;
            pListView.HeaderStyle = ColumnHeaderStyle.Clickable;
            pListView.Visible = true;
        }
        public virtual void AddNewRow()
        {
        }
        public virtual void InsertNewRow(int nPlaceIndex)
        {

        }
        public virtual void DelSelectRow(int nPlaceIndex)
        {

        }
        public virtual void ClearAllRow()
        {

        }
        public virtual void SaveNowSet()
        {

        }
        public virtual void SetNowSelectValue(int nIndex, object pData)
        {

        }
    }
    public class CYcGroup : CGroupInterface
    {
        public CYcGroup()
        {
            strTableName = "ChildYcModule";
        }
        public override void AddNewRow()
        {
            CChildModule pNewModule = new CChildModule();
            pNewModule.m_DataNo = m_DataList.Count + 1;
            pNewModule.m_DataName = "遥测" + pNewModule.m_DataNo.ToString();
            pNewModule.m_DataUnit = "";
            
            m_DataList.Add(pNewModule);

            string[] strHead = new string[3];

            strHead[0] = pNewModule.m_DataNo.ToString();
            strHead[1] = pNewModule.m_DataName;
            strHead[2] = pNewModule.m_DataUnit;
            ListViewItem pItem = new ListViewItem(strHead);
            m_ListView.AddItem(pItem);
        }
        public override void InsertNewRow(int nPlaceIndex)
        {
            CChildModule pNewModule = new CChildModule();
            pNewModule.m_DataNo = nPlaceIndex + 1;
            pNewModule.m_DataName = "遥测" + pNewModule.m_DataNo.ToString();
            pNewModule.m_DataUnit = "";
            
            m_DataList.Insert(nPlaceIndex, pNewModule);
            IniShowListView(m_ListView);
            m_ListView.Items[nPlaceIndex + 1].Selected = true;
        }
        public override void DelSelectRow(int nPlaceIndex)
        {
            if (nPlaceIndex >= m_DataList.Count) return;
            m_DataList.RemoveAt(nPlaceIndex);
            IniShowListView(m_ListView);
            if (nPlaceIndex < m_DataList.Count)
                m_ListView.Items[nPlaceIndex].Selected = true;
        }
        public override void ClearAllRow()
        {
            m_DataList.Clear();
            IniShowListView(m_ListView);
        }
        public override void SetNowSelectValue(int nIndex, object pData)
        {
            if (m_NowSelectIndex >= 0 && m_NowSelectIndex < m_DataList.Count)
            {
                switch (nIndex)
                {
                    case 1:
                        m_DataList[m_NowSelectIndex].m_DataName = (string)pData;
                        break;
                    case 2:
                        m_DataList[m_NowSelectIndex].m_DataUnit = (string)pData;
                        break;
                }
            }
        }
        public override void SaveNowSet()
        {
            string strSql;
            strSql = "delete from " + strTableName + "  where(ModuleIndex=" + m_ModuleIndex.ToString() + ")";
            CGlobal.m_DataBase.ExecSql(strSql);
            int i;
            for (i = 0; i < m_DataList.Count; i++)
            {
                strSql = "insert into " + strTableName + " values(" +
                    m_ModuleIndex.ToString() + "," +
                    m_DataList[i].m_DataNo.ToString() + ",'" +
                    m_DataList[i].m_DataName + "','" +
                    m_DataList[i].m_DataUnit + "')";
                CGlobal.m_DataBase.ExecSql(strSql);
            }
        }
        public override void IniReadChildModule()
        {
            string strSql;
            DataTable pTable;
            int i;
            strSql = "Select * from " + strTableName + " where(ModuleIndex=" + m_ModuleIndex.ToString() + ") order by YcNumber";
            pTable = (DataTable)CGlobal.m_DataBase.SelectDataTable(strSql);
            if (pTable == null) return;
            for (i = 0; i < pTable.Rows.Count; i++)
            {
                CChildModule pNewModule = new CChildModule();
                pNewModule.m_DataNo = CGlobal.m_DataBase.GetTableIntValue(pTable, i, 1);
                pNewModule.m_DataName = CGlobal.m_DataBase.GetTableString(pTable, i, 2);
                pNewModule.m_DataUnit = CGlobal.m_DataBase.GetTableString(pTable, i, 3);
                
                m_DataList.Add(pNewModule);
            }
        }
        public override void IniShowListView(MyListView pListView)
        {
            base.IniShowListView(pListView);

            pListView.ClearAllItem();
            pListView.Columns.Add("序号", 80, HorizontalAlignment.Center);
            pListView.Columns.Add("遥测名称", 240, HorizontalAlignment.Left);
            pListView.Columns.Add("遥测单位", 80, HorizontalAlignment.Left);

            string[] strHead = new string[3];
            for (int i = 0; i < m_DataList.Count; i++)
            {
                CChildModule pData = m_DataList[i];
                pData.m_DataNo = i + 1;
                strHead[0] = pData.m_DataNo.ToString();
                strHead[1] = pData.m_DataName;
                strHead[2] = pData.m_DataUnit;


                ListViewItem pItem = new ListViewItem(strHead);
                pListView.AddItem(pItem);
            }
            pListView.SetColumnsEditMode(1);
            pListView.SetColumnsEditMode(2);

        }
    }
    public class CYxGroup : CGroupInterface
    {
        public CYxGroup()
        {
            strTableName = "ChildYxModule";
        }
        public override void AddNewRow()
        {
            CChildModule pNewModule = new CChildModule();
            pNewModule.m_DataNo = m_DataList.Count + 1;
            pNewModule.m_DataName = "遥信" + pNewModule.m_DataNo.ToString();
            
            m_DataList.Add(pNewModule);

            string[] strHead = new string[2];

            strHead[0] = pNewModule.m_DataNo.ToString();
            strHead[1] = pNewModule.m_DataName;
            ListViewItem pItem = new ListViewItem(strHead);
            m_ListView.AddItem(pItem);
        }
        public override void InsertNewRow(int nPlaceIndex)
        {
            CChildModule pNewModule = new CChildModule();
            pNewModule.m_DataNo = nPlaceIndex + 1;
            pNewModule.m_DataName = "遥信" + pNewModule.m_DataNo.ToString();
            
            m_DataList.Insert(nPlaceIndex, pNewModule);
            IniShowListView(m_ListView);
            m_ListView.Items[nPlaceIndex + 1].Selected = true;
        }
        public override void DelSelectRow(int nPlaceIndex)
        {
            m_DataList.RemoveAt(nPlaceIndex);
            IniShowListView(m_ListView);
            if (nPlaceIndex < m_DataList.Count)
                m_ListView.Items[nPlaceIndex].Selected = true;
        }
        public override void ClearAllRow()
        {
            m_DataList.Clear();
            IniShowListView(m_ListView);
        }
        public override void SetNowSelectValue(int nIndex, object pData)
        {
            if (m_NowSelectIndex >= 0 && m_NowSelectIndex < m_DataList.Count)
            {
                switch (nIndex)
                {
                    case 1:
                        m_DataList[m_NowSelectIndex].m_DataName = (string)pData;
                        break;
                }
            }
        }
        public override void SaveNowSet()
        {
            string strSql;
            strSql = "delete from " + strTableName + "  where(ModuleIndex=" + m_ModuleIndex.ToString() + ")";
            CGlobal.m_DataBase.ExecSql(strSql);
            int i;
            for (i = 0; i < m_DataList.Count; i++)
            {
                strSql = "insert into " + strTableName + " values(" +
                    m_ModuleIndex.ToString() + "," +
                    m_DataList[i].m_DataNo.ToString() + ",'" +
                    m_DataList[i].m_DataName + "')";
                CGlobal.m_DataBase.ExecSql(strSql);
            }
        }
        public override void IniReadChildModule()
        {
            string strSql;
            DataTable pTable;
            int i;
            strSql = "Select * from " + strTableName + " where(ModuleIndex=" + m_ModuleIndex.ToString() + ") order by YxNumber";
            pTable = (DataTable)CGlobal.m_DataBase.SelectDataTable(strSql);
            if (pTable == null) return;
            for (i = 0; i < pTable.Rows.Count; i++)
            {
                CChildModule pNewModule = new CChildModule();
                pNewModule.m_DataNo = CGlobal.m_DataBase.GetTableIntValue(pTable, i, 1);
                pNewModule.m_DataName = CGlobal.m_DataBase.GetTableString(pTable, i, 2);
                
                m_DataList.Add(pNewModule);
            }
        }
        public override void IniShowListView(MyListView pListView)
        {
            base.IniShowListView(pListView);

            pListView.Columns.Add("序号", 80, HorizontalAlignment.Center);
            pListView.Columns.Add("遥信名称", 240, HorizontalAlignment.Left);

            string[] strHead = new string[2];
            for (int i = 0; i < m_DataList.Count; i++)
            {
                CChildModule pData = m_DataList[i];
                pData.m_DataNo = i + 1;
                strHead[0] = pData.m_DataNo.ToString();
                strHead[1] = pData.m_DataName;

                ListViewItem pItem = new ListViewItem(strHead);
                pListView.AddItem(pItem);
            }
            pListView.SetColumnsEditMode(1);
        }
    }

    public class CPowerGroup : CGroupInterface
    {
        public CPowerGroup()
        {
            strTableName = "ChildPowerModule";
        }
        public override void AddNewRow()
        {
            CChildModule pNewModule = new CChildModule();
            pNewModule.m_DataNo = m_DataList.Count + 1;
            pNewModule.m_DataName = "电量" + pNewModule.m_DataNo.ToString();
            pNewModule.m_DataUnit = "";
           
            m_DataList.Add(pNewModule);

            string[] strHead = new string[3];

            strHead[0] = pNewModule.m_DataNo.ToString();
            strHead[1] = pNewModule.m_DataName;
            strHead[2] = pNewModule.m_DataUnit;
            ListViewItem pItem = new ListViewItem(strHead);
            m_ListView.AddItem(pItem);
        }
        public override void InsertNewRow(int nPlaceIndex)
        {
            CChildModule pNewModule = new CChildModule();
            pNewModule.m_DataNo = nPlaceIndex + 1;
            pNewModule.m_DataName = "电量" + pNewModule.m_DataNo.ToString();
            pNewModule.m_DataUnit = "";
            
            m_DataList.Insert(nPlaceIndex, pNewModule);
            IniShowListView(m_ListView);
            m_ListView.Items[nPlaceIndex + 1].Selected = true;
        }
        public override void DelSelectRow(int nPlaceIndex)
        {
            m_DataList.RemoveAt(nPlaceIndex);
            IniShowListView(m_ListView);
            if (nPlaceIndex < m_DataList.Count)
                m_ListView.Items[nPlaceIndex].Selected = true;
        }
        public override void ClearAllRow()
        {
            m_DataList.Clear();
            IniShowListView(m_ListView);
        }
        public override void SetNowSelectValue(int nIndex, object pData)
        {
            if (m_NowSelectIndex >= 0 && m_NowSelectIndex < m_DataList.Count)
            {
                switch (nIndex)
                {
                    case 1:
                        m_DataList[m_NowSelectIndex].m_DataName = (string)pData;
                        break;
                    case 2:
                        m_DataList[m_NowSelectIndex].m_DataUnit = (string)pData;
                        break;
                }
            }
        }
        public override void SaveNowSet()
        {
            string strSql;
            strSql = "delete from " + strTableName + "  where(ModuleIndex=" + m_ModuleIndex.ToString() + ")";
            CGlobal.m_DataBase.ExecSql(strSql);
            int i;
            for (i = 0; i < m_DataList.Count; i++)
            {
                strSql = "insert into " + strTableName + " values(" +
                    m_ModuleIndex.ToString() + "," +
                    m_DataList[i].m_DataNo.ToString() + ",'" +
                    m_DataList[i].m_DataName + "','" +
                    m_DataList[i].m_DataUnit + "')";
                CGlobal.m_DataBase.ExecSql(strSql);
            }
        }
        public override void IniReadChildModule()
        {
            string strSql;
            DataTable pTable;
            int i;
            strSql = "Select * from " + strTableName + " where(ModuleIndex=" + m_ModuleIndex.ToString() + ") order by PowerNumber";
            pTable = (DataTable)CGlobal.m_DataBase.SelectDataTable(strSql);
            if (pTable == null) return;
            for (i = 0; i < pTable.Rows.Count; i++)
            {
                CChildModule pNewModule = new CChildModule();
                pNewModule.m_DataNo = CGlobal.m_DataBase.GetTableIntValue(pTable, i, 1);
                pNewModule.m_DataName = CGlobal.m_DataBase.GetTableString(pTable, i, 2);
                pNewModule.m_DataUnit = CGlobal.m_DataBase.GetTableString(pTable, i, 3);
                
                m_DataList.Add(pNewModule);
            }
        }
        public override void IniShowListView(MyListView pListView)
        {
            base.IniShowListView(pListView);

            pListView.Columns.Add("序号", 80, HorizontalAlignment.Center);
            pListView.Columns.Add("电量名称", 240, HorizontalAlignment.Left);
            pListView.Columns.Add("电量单位", 80, HorizontalAlignment.Left);

            string[] strHead = new string[3];
            for (int i = 0; i < m_DataList.Count; i++)
            {
                CChildModule pData = m_DataList[i];
                pData.m_DataNo = i + 1;
                strHead[0] = pData.m_DataNo.ToString();
                strHead[1] = pData.m_DataName;
                strHead[2] = pData.m_DataUnit;
                ListViewItem pItem = new ListViewItem(strHead);
                pListView.AddItem(pItem);
            }
            pListView.SetColumnsEditMode(1);
            pListView.SetColumnsEditMode(2);
        }
    }
    public class CParaGroup : CGroupInterface
    {
        string[] strParaTypeList = new string[12];
        public CParaGroup()
        {
            strTableName = "ChildParaModule";

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
        public override void AddNewRow()
        {
            CChildModule pNewModule = new CChildModule();
            pNewModule.m_DataNo = m_DataList.Count + 1;
            pNewModule.m_DataName = "参数" + pNewModule.m_DataNo.ToString();
            pNewModule.m_DataType = 1;
            pNewModule.m_DataUnit = "";
            pNewModule.m_DataDesc = "";
            
            m_DataList.Add(pNewModule);

            string[] strHead = new string[5];

            strHead[0] = pNewModule.m_DataNo.ToString();
            strHead[1] = pNewModule.m_DataName;
            strHead[2] = strParaTypeList[GetListIndex(pNewModule.m_DataType)];
            strHead[3] = pNewModule.m_DataUnit;
            strHead[4] = pNewModule.m_DataDesc;
            ListViewItem pItem = new ListViewItem(strHead);
            m_ListView.AddItem(pItem);
        }
        public override void InsertNewRow(int nPlaceIndex)
        {
            CChildModule pNewModule = new CChildModule();
            pNewModule.m_DataName = "参数" + pNewModule.m_DataNo.ToString();
            pNewModule.m_DataType = 1;
            pNewModule.m_DataUnit = "";
            pNewModule.m_DataDesc = "";
            
            m_DataList.Insert(nPlaceIndex, pNewModule);
            IniShowListView(m_ListView);
            m_ListView.Items[nPlaceIndex + 1].Selected = true;
        }
        public override void DelSelectRow(int nPlaceIndex)
        {
            m_DataList.RemoveAt(nPlaceIndex);
            IniShowListView(m_ListView);
            if (nPlaceIndex < m_DataList.Count)
                m_ListView.Items[nPlaceIndex].Selected = true;
        }
        public override void ClearAllRow()
        {
            m_DataList.Clear();
            IniShowListView(m_ListView);
        }
        private int GetIndexDataType(string strIndex)
        {
            string strDataType;
            strDataType = strIndex.Substring(0, 2);
            return Convert.ToInt32(strDataType);
        }
        public override void SetNowSelectValue(int nIndex, object pData)
        {
            if (m_NowSelectIndex >= 0 && m_NowSelectIndex < m_DataList.Count)
            {
                switch (nIndex)
                {
                    case 1:
                        m_DataList[m_NowSelectIndex].m_DataName = (string)pData;
                        break;
                    case 2:
                        m_DataList[m_NowSelectIndex].m_DataType = GetIndexDataType((string)pData);
                        break;
                    case 3:
                        m_DataList[m_NowSelectIndex].m_DataUnit = (string)pData;
                        break;
                    case 4:
                        m_DataList[m_NowSelectIndex].m_DataDesc = (string)pData;
                        break;
                }
            }
        }
        public override void SaveNowSet()
        {
            string strSql;
            strSql = "delete from " + strTableName + "  where(ModuleIndex=" + m_ModuleIndex.ToString() + ")";
            CGlobal.m_DataBase.ExecSql(strSql);
            int i;
            for (i = 0; i < m_DataList.Count; i++)
            {
                strSql = "insert into " + strTableName + " values(" +
                    m_ModuleIndex.ToString() + "," +
                    m_DataList[i].m_DataNo.ToString() + "," +
                    m_DataList[i].m_DataType.ToString() + ",'" +
                    m_DataList[i].m_DataName + "','" +
                    m_DataList[i].m_DataDesc + "','" +
                    m_DataList[i].m_DataUnit + "')";
                CGlobal.m_DataBase.ExecSql(strSql);
            }
        }
        public override void IniReadChildModule()
        {
            string strSql;
            DataTable pTable;
            int i; 
            strSql = "Select * from " + strTableName + " where(ModuleIndex=" + m_ModuleIndex.ToString() + ") order by ParaNo";
            pTable = (DataTable)CGlobal.m_DataBase.SelectDataTable(strSql);
            if (pTable == null) return;
            for (i = 0; i < pTable.Rows.Count; i++)
            {
                CChildModule pNewModule = new CChildModule();
                pNewModule.m_DataNo = CGlobal.m_DataBase.GetTableIntValue(pTable, i, 1);
                pNewModule.m_DataType = CGlobal.m_DataBase.GetTableIntValue(pTable, i, 2);
                pNewModule.m_DataName = CGlobal.m_DataBase.GetTableString(pTable, i, 3);
                pNewModule.m_DataDesc = CGlobal.m_DataBase.GetTableString(pTable, i, 4);
                pNewModule.m_DataUnit = CGlobal.m_DataBase.GetTableString(pTable, i, 5);
                
                m_DataList.Add(pNewModule);
            }
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
        public override void IniShowListView(MyListView pListView)
        {
            base.IniShowListView(pListView);
            pListView.ClearAllItem();
            pListView.Columns.Add("序号", 80, HorizontalAlignment.Center);
            pListView.Columns.Add("参数名称", 240, HorizontalAlignment.Left);
            pListView.Columns.Add("参数类型", 200, HorizontalAlignment.Left);
            pListView.Columns.Add("参数单位", 80, HorizontalAlignment.Left);
            pListView.Columns.Add("参数说明", 240, HorizontalAlignment.Left);

            pListView.AddComBoxListStr(strParaTypeList);

            string[] strHead = new string[5];
            for (int i = 0; i < m_DataList.Count; i++)
            {
                CChildModule pData = m_DataList[i];
                pData.m_DataNo = i + 1;
                strHead[0] = pData.m_DataNo.ToString();
                strHead[1] = pData.m_DataName;
                strHead[2] = strParaTypeList[GetListIndex(pData.m_DataType)];
                strHead[3] = pData.m_DataUnit;
                strHead[4] = pData.m_DataDesc;
                ListViewItem pItem = new ListViewItem(strHead);
                pListView.AddItem(pItem);
            }

            pListView.SetColumnsEditMode(1);
            pListView.SetColumnsComBoxMode(2, 0);
            pListView.SetColumnsEditMode(3);
            pListView.SetColumnsEditMode(4);
        }
    }

    public class CChildModuleRoot
    {
        public string m_RootName;
        public int m_RootType;
        public TreeNode m_Nodes;
        public List<CGroupInterface> m_ModuleList = new List<CGroupInterface>();
        public static Dictionary<int, int> m_ModuleNoList = new Dictionary<int, int>();
        public CChildModuleRoot()
        {

        }
        public void DeleteGroup(CGroupInterface pDelGroup)
        {
            string strSql;
            m_ModuleNoList.Remove(pDelGroup.m_ModuleNo);
            strSql = "delete from ChildModuleTable where(ModuleNo=" + pDelGroup.m_ModuleNo.ToString() + ")";
            CGlobal.m_DataBase.ExecSql(strSql);
            strSql = "delete from " + pDelGroup.strTableName + "  where(ModuleIndex=" + pDelGroup.m_ModuleIndex.ToString() + ")";
            CGlobal.m_DataBase.ExecSql(strSql);
            m_ModuleList.Remove(pDelGroup);
        }
        public void CreateTreeNode()
        {
            m_Nodes = new TreeNode();
            m_Nodes.Text = m_RootName;
            m_Nodes.Tag = this;
        }
        public CGroupInterface GetNewGroupObject()
        {
            CGroupInterface pObject = null;
            switch (m_RootType)
            {
                case 1:
                    pObject = new CYcGroup();
                    break;
                case 2:
                    pObject = new CYxGroup();
                    break;
                case 3:
                    pObject = new CPowerGroup();
                    break;
                case 4:
                    pObject = new CParaGroup();
                    break;

            }
            return pObject;
        }
        public void AddNewGroup()
        {
            string strName = Interaction.InputBox("请输入模板名称", "模板名称", "", 100, 100);
            strName = strName.Trim();
            if (strName.Length == 0) return;
            CGroupInterface pNewGroup = GetNewGroupObject();
            pNewGroup.m_ModuleNo = CGlobal.GetCanuUseNo(m_ModuleNoList);
            pNewGroup.m_ModuleName = strName;
            pNewGroup.m_ModuleType = m_RootType;
            pNewGroup.m_ModuleIndex = pNewGroup.m_ModuleNo;
            pNewGroup.m_Root = this;
            pNewGroup.CreateTreeNode();
            string strSql;
            strSql = "insert into ChildModuleTable values(" +
                pNewGroup.m_ModuleNo.ToString() + ",'" +
                pNewGroup.m_ModuleName + "'," +
                pNewGroup.m_ModuleType.ToString() + "," +
                pNewGroup.m_ModuleIndex.ToString() + ")";

            if (CGlobal.m_DataBase.ExecSql(strSql))
            {
                m_ModuleList.Add(pNewGroup);
                m_Nodes.Nodes.Add(pNewGroup.m_Nodes);
            }
        }
        public void IniReadGroup()
        {
            string strSql;
            DataTable pTable;
            int i;
            strSql = "Select * from ChildModuleTable where(ModuleType=" + m_RootType.ToString() + ") order by ModuleNo";
            pTable = (DataTable)CGlobal.m_DataBase.SelectDataTable(strSql);
            if (pTable == null) return;
            for (i = 0; i < pTable.Rows.Count; i++)
            {
                CGroupInterface pNewGroup = GetNewGroupObject();
                pNewGroup.m_ModuleNo = CGlobal.m_DataBase.GetTableIntValue(pTable, i, 0);
                pNewGroup.m_ModuleName = CGlobal.m_DataBase.GetTableString(pTable, i, 1);
                pNewGroup.m_ModuleType = CGlobal.m_DataBase.GetTableIntValue(pTable, i, 2);
                pNewGroup.m_ModuleIndex = CGlobal.m_DataBase.GetTableIntValue(pTable, i, 3);
                pNewGroup.m_Root = this;
                pNewGroup.CreateTreeNode();
                pNewGroup.IniReadChildModule();
                m_ModuleList.Add(pNewGroup);
                m_ModuleNoList.Add(pNewGroup.m_ModuleNo,0);
            }
        }
    }
}
