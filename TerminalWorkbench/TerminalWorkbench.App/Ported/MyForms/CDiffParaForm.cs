using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalWorkbench.Presentation;

namespace DtuTestManager
{
    public partial class CDiffParaForm : Form
    {
        private int m_DevViewSelectIndex = -1;
        public string[] strParaTypeList = new string[12];
        public CDiffParaForm()
        {
            InitializeComponent();
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
        private void CDiffParaForm_Load(object sender, EventArgs e)
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

            LV.ClearAllItem();
            LV.GridLines = true;
            LV.FullRowSelect = true;
            LV.View = View.Details;
            LV.Scrollable = true;
            LV.MultiSelect = false;
            LV.HeaderStyle = ColumnHeaderStyle.Clickable;
            LV.Visible = true;

            LV.Columns.Add("序号", 60, HorizontalAlignment.Center);
            LV.Columns.Add("参数名称", 240, HorizontalAlignment.Left);
            LV.Columns.Add("参数地址", 60, HorizontalAlignment.Left);
            LV.Columns.Add("参数类型", 200, HorizontalAlignment.Left);



            string[] strHead = new string[4];
            for (int i = 0; i < CParaSetForm.m_DiffParaDataList.Count; i++)
            {
                try
                {
                    CParaCtrl pData = CParaSetForm.m_DiffParaDataList[i];
                    strHead[0] = pData.m_ParaNo.ToString();
                    strHead[1] = pData.m_ParaName;
                    strHead[2] = pData.m_ParaAddress.ToString();
                    strHead[3] = strParaTypeList[GetListIndex(pData.m_ParaType)];
                    ListViewItem pItem = new ListViewItem(strHead);
                    LV.AddItem(pItem);
                }
                catch { }
            }
            LV.SetColumnsComBoxMode(3, 0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CParaSetForm.m_DiffParaDataList.Count; i++)
            {
                string strSql;
                strSql = "update " + "Mod_paraTable" + " set ParaType='" + CParaSetForm.m_DiffParaDataList[i].m_ParaType.ToString() + "' where(ParaAddress=" + CParaSetForm.m_DiffParaDataList[i].m_ParaAddress.ToString() + ")";
                CGlobal.m_DataBase.ExecSql(strSql);
            }
        }

        private void LV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LV.SelectedIndices != null && LV.SelectedIndices.Count > 0)
            {
                m_DevViewSelectIndex = LV.SelectedItems[0].Index;
            }
            else
            {
                m_DevViewSelectIndex = -1;
            }
        }
        private int GetIndexDataType(string strIndex)
        {
            string strDataType;
            strDataType = strIndex.Substring(0, 2);
            return Convert.ToInt32(strDataType);
        }
        public void SetNowSelectValue(int nIndex, object pData)
        {
            if (m_DevViewSelectIndex >= 0 && m_DevViewSelectIndex < CParaSetForm.m_DiffParaDataList.Count)
            {
                string strValue = CParaSetForm.m_DiffParaDataList[m_DevViewSelectIndex].m_ParaValue.ToString();
                CParaSetForm.m_DiffParaDataList[m_DevViewSelectIndex].m_ParaType = GetIndexDataType((string)pData);
                CParaSetForm.m_DiffParaDataList[m_DevViewSelectIndex].m_ParaValue = strValue;

            }
        }
        private void LV_DoubleClick(object sender, EventArgs e)
        {
            LV.theProcessFun = new MyListView.ProcessDelegate(SetNowSelectValue);
        }

    }
}
