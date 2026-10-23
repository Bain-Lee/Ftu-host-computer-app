using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace DtuTestManager
{
    public partial class CParaSetForm : Form
    {
        public static List<CParaCtrl> m_DiffParaDataList = new List<CParaCtrl>();
        public  List<CParaCtrl> m_AllParaDataList = new List<CParaCtrl>();  //全部参数包括重复部分
        public CParaListOperation m_ParaListOperation = new CParaListOperation();
        public CChildModuleRoot m_ModuleConection = null;
        public CGroupInterface m_ChildGroup = null;
        private CParaRootTable m_NowSelctRootTable = null;
        private CParaGroupTable m_NowGroupTable = null;
        private int m_DevViewSelectIndex = -1;
        private CParaDefine pNameSelect = null;
        public CParaSetForm()
        {
            InitializeComponent();
        }
        private void IniFontNameList()
        {
            int i;
            string strName;
            FontNameList.Items.Clear();
            for (i = 0; i < 16; i++)
            {
                strName = i.ToString() + "#线路";
                FontNameList.Items.Add(strName);
            }
            FontNameList.SelectedIndex = 0;
        }
        private void IniChildModule()
        {
            if (m_ModuleConection != null)
            {
                for (int i = 0; i < m_ModuleConection.m_ModuleList.Count; i++)
                {
                    ChildMdList.Items.Add(m_ModuleConection.m_ModuleList[i].m_ModuleName);
                }
            }
        }
        private void IniTreeView()
        {
            int i, j;
            m_ParaListOperation.m_MyTreeView = ParaListTree;
            ParaListTree.Nodes.Clear();
            for (i = 0; i < m_ParaListOperation.m_ParaRootList.Count; i++)
            {
                ParaListTree.Nodes.Add(m_ParaListOperation.m_ParaRootList[i].m_RootNode);
                for (j = 0; j < m_ParaListOperation.m_ParaRootList[i].m_GroupList.Count; j++)
                {
                   
                    m_ParaListOperation.m_ParaRootList[i].m_RootNode.Nodes.Add(m_ParaListOperation.m_ParaRootList[i].m_GroupList[j].m_GroupNode);
                    }
                }
            
        }
        private void CParaSetForm_Load(object sender, EventArgs e)
        {
            m_ParaListOperation.InitialzeReadPara();
            IniTreeView();
            IniFontNameList();
        }
        private void IniShowListView()
        {
            if (m_NowGroupTable != null)
            {
                m_NowGroupTable.IniShowListView(ParaDataListView);
                FirstAddress.Text = m_NowGroupTable.m_StartAddress.ToString();
            }
            else
            {
                FirstAddress.Text = "1";
                ParaDataListView.ClearAllItem(false);
            }
        }
        private void ParaListTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode pSelectNode = ParaListTree.SelectedNode;
            pNameSelect = null;
            if (pSelectNode != null && pSelectNode.Tag != null)
            {
                if (pSelectNode.Tag.GetType() == typeof(CParaRootTable))
                {
                    m_NowSelctRootTable = (CParaRootTable)pSelectNode.Tag;
                    m_NowGroupTable = null;
                    pNameSelect = m_NowSelctRootTable;
                }
                else if(pSelectNode.Tag.GetType() == typeof(CParaGroupTable))
                {
                    m_NowGroupTable = (CParaGroupTable)pSelectNode.Tag;
                    m_NowSelctRootTable = (CParaRootTable)pSelectNode.Parent.Tag;
                    pNameSelect = m_NowGroupTable;
                }
                IniShowListView();
            }
        }

        private void ParaDataListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ParaDataListView.SelectedIndices != null && ParaDataListView.SelectedIndices.Count > 0)
            {
                m_DevViewSelectIndex = ParaDataListView.SelectedItems[0].Index;
            }
            else
            {
                m_DevViewSelectIndex = -1;
            }
        }

        private void FirstAddress_TextChanged(object sender, EventArgs e)
        {
            if (m_NowGroupTable != null)
            {
                m_NowGroupTable.m_StartAddress = Convert.ToInt32(FirstAddress.Text);
            }
        }

        private void AddChileModule_Click(object sender, EventArgs e)
        {
            if (m_NowGroupTable != null && m_ModuleConection != null && m_ChildGroup != null)
            {
                int i;
                for (i = 0; i < m_ChildGroup.m_DataList.Count; i++)
                {
                    m_NowGroupTable.AddModulePara(m_ChildGroup.m_DataList[i], FontNameList.Text);
                }
            }
        }

        private void ResetAddNo_Click(object sender, EventArgs e)
        {
            if (m_NowGroupTable != null)
            {
                m_NowGroupTable.ResetAddress();
            }
        }

        private void ChildMdList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nIndex = ChildMdList.SelectedIndex;
            m_ChildGroup = m_ModuleConection.m_ModuleList[nIndex];
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (m_NowGroupTable != null)
            {
                m_NowGroupTable.AddNewParaData();
            }
        }

        private void BtnInsert_Click(object sender, EventArgs e)
        {
            if (m_NowGroupTable != null)
            {
                m_NowGroupTable.InsertParaData(m_DevViewSelectIndex);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (m_NowGroupTable != null)
            {
                m_NowGroupTable.DeleteParaData(m_DevViewSelectIndex);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (m_NowGroupTable != null)
            {
                m_NowGroupTable.ClearList();
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (m_NowGroupTable != null)
            {
                m_NowGroupTable.SaveParaSet();
            }
        }
        private void BtnReset_Click(object sender, EventArgs e)
        {
            if (m_NowGroupTable != null)
            {
                m_NowGroupTable.ResetParaList();
            }
        }
        private void BtnLoadNowPara_Click(object sender, EventArgs e)
        {
            if (m_NowGroupTable != null)
            {
                int i, j;
                CGetSelectParaForm pSelectForm = new CGetSelectParaForm();
                pSelectForm.m_pNowPara = m_ParaListOperation;
                pSelectForm.m_SelectGroup = m_NowGroupTable;
                for (i = 0; i < m_ParaListOperation.m_ParaRootList.Count; i++)
                {
                    for (j = 0; j < m_ParaListOperation.m_ParaRootList[i].m_GroupList.Count; j++)
                    {
                        m_ParaListOperation.m_ParaRootList[i].m_GroupList[j].m_bShow = true;
                    }
                }
                m_NowGroupTable.m_bShow = false;
                if (pSelectForm.ShowDialog() == DialogResult.OK)
                {
                    IniShowListView();
                }
            }
        }
        private void ParaDataListView_DoubleClick(object sender, EventArgs e)
        {
            if (m_NowGroupTable != null)
            {
                m_NowGroupTable.m_NowSelectIndex = m_DevViewSelectIndex;
                ParaDataListView.theProcessFun = new MyListView.ProcessDelegate(m_NowGroupTable.SetNowSelectValue);
            }
        }

        private void AddRoot_Click(object sender, EventArgs e)
        {
            if (m_ParaListOperation != null)
            {
                m_ParaListOperation.AddNewParaRoot();
            }
        }

        private void DeleteRoot_Click(object sender, EventArgs e)
        {
            if (m_ParaListOperation != null && m_NowSelctRootTable != null)
            {
                m_ParaListOperation.DeleteParaRoot(m_NowSelctRootTable);
            }
        }
        private void AddList_Click(object sender, EventArgs e)
        {
            if (m_NowSelctRootTable != null)
            {
                m_NowSelctRootTable.AddNewParaGroup();
            }
        }
        private void DeleteList_Click(object sender, EventArgs e)
        {
            if (m_NowSelctRootTable != null && m_NowGroupTable!=null)
            {
                if (MessageBox.Show("将删除此参数组及对应的数据，删除后无法恢复，确认删除？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    m_NowSelctRootTable.DeleteParaGroup(m_NowGroupTable);
                }
            }
        }

        private void BtnChangeName_Click(object sender, EventArgs e)
        {
            if (pNameSelect != null)
            {
                string strName = Interaction.InputBox("请输入新的名称：", "名称", "", 100, 100);
                strName = strName.Trim();
                if (strName.Length == 0) return;
                pNameSelect.UpdateName(strName);
            }
        }



        public int BinarySearch(int low, int high, int key, ref int m_SameIndex)
        {
            int mid = (low + high) / 2;
            if (low > high)
                return -1;
            else
            {
                if (m_AllParaDataList[mid].m_ParaAddress == key)
                {                  
                    return mid;
                }
                else if (m_AllParaDataList[mid].m_ParaAddress > key)
                {
                    if (m_AllParaDataList[mid - 1].m_ParaAddress < key)
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
                    if (m_AllParaDataList[mid + 1].m_ParaAddress > key)
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
        private void BtnParaCheck_Click(object sender, EventArgs e)
        {
            m_DiffParaDataList.Clear();
            m_AllParaDataList.Clear();
            int i, j;
            m_ParaListOperation.m_MyTreeView = ParaListTree;
            ParaListTree.Nodes.Clear();
            for (i = 0; i < m_ParaListOperation.m_ParaRootList.Count; i++)
            {
                ParaListTree.Nodes.Add(m_ParaListOperation.m_ParaRootList[i].m_RootNode);
                for (j = 0; j < m_ParaListOperation.m_ParaRootList[i].m_GroupList.Count; j++)
                {
                    for (int k = 0; k < m_ParaListOperation.m_ParaRootList[i].m_GroupList[j].m_ParaDataList.Count; k++)
                    {
                        CParaCtrl pPara = m_ParaListOperation.m_ParaRootList[i].m_GroupList[j].m_ParaDataList[k];

                        if (m_AllParaDataList.Count > 1)
                        {
                            if (pPara.m_ParaAddress < m_AllParaDataList[0].m_ParaAddress)
                            {
                                m_AllParaDataList.Insert(0, pPara);
                            }
                            else if (pPara.m_ParaAddress > m_AllParaDataList[m_AllParaDataList.Count - 1].m_ParaAddress)
                            {
                                m_AllParaDataList.Add(pPara);
                            }
                            else
                            {
                                int m_Same_Index = 0;
                                int nIndex = BinarySearch(0, m_AllParaDataList.Count, pPara.m_ParaAddress, ref m_Same_Index);
                                if (nIndex > -1)
                                {
                                    m_AllParaDataList.Insert(nIndex, pPara);
                                }
                            }
                        }
                        else
                        {
                            m_AllParaDataList.Add(pPara);
                        }
                    }
                }
            }

            int cnt=0;
            bool flag = false;
            for (i = 0; i < m_AllParaDataList.Count; i++)
            {
                for (j = (i + 1); j < m_AllParaDataList.Count; j++)
                {
                    if (m_AllParaDataList[i].m_ParaAddress == m_AllParaDataList[j].m_ParaAddress)
                    {
                        if (m_AllParaDataList[i].m_ParaType != m_AllParaDataList[j].m_ParaType)
                        {
                            flag = true;
                        }
                        cnt++;                        
                    }
                    else
                    {
                        if ((cnt > 1)&&(flag==true))
                        {
                            for (int k = i; k <=i + cnt; k++)
                            {
                                m_DiffParaDataList.Add(m_AllParaDataList[k]);                                
                            }                            
                        }
                        flag = false;
                        i = i + cnt+1;
                        break;
                    }
                }            
            }
            if (m_DiffParaDataList.Count > 0)
            {
                CDiffParaForm pform = new CDiffParaForm();
                pform.ShowDialog();
            }


        }
        private void SqlTest()
        {
            string strSql;
            DataTable pTable;
            int i;
            string strErrMsg = "";
            strSql = "Select distinct A.ParaTableID,A.ParaID,A.ParaName,A.ParaAddress from Mod_ParaTable A,Mod_ParaTable B where(A.ParaAddress=B.ParaAddress and A.ParaType<>B.ParaType) order by A.ParaAddress";

            //  strMsgSql = "Select "
            pTable = (DataTable)CGlobal.m_Device.m_DeviceMsg.m_ModuleDB.SelectDataTable(strSql);
            if (pTable == null) return;
            for (i = 0; i < pTable.Rows.Count; i++)
            {
                int nGroupID = CGlobal.m_Device.m_DeviceMsg.m_ModuleDB.GetTableIntValue(pTable, i, 0);
                string strGroupName = "";// = GetParaGroupName(nGroupID);
                int nID = CGlobal.m_Device.m_DeviceMsg.m_ModuleDB.GetTableIntValue(pTable, i, 1);
                string strName = CGlobal.m_Device.m_DeviceMsg.m_ModuleDB.GetTableString(pTable, i, 2);
                int nAddress = CGlobal.m_Device.m_DeviceMsg.m_ModuleDB.GetTableIntValue(pTable, i, 3);

                strErrMsg += nAddress.ToString("00000") + "    组名：" + strGroupName + "\r\n            序号:" + nID.ToString("000") + "    名称：" + strName + "\r\n";
            }
            if (strErrMsg.Length > 0)
            {
                strErrMsg = "参数配置错误信息:\r\n" + strErrMsg;
                MessageBox.Show(strErrMsg);
            }

        }
    }
}
