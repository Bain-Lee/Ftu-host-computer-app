using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DtuTestManager
{
    public partial class CEngParaNewForm : Form
    {
    
        private CParaCtrl m_ItemIndex = null;      
        private int m_ItemPlace = -1;       
        private int m_ItemTopPlace = -1;
        CParaGroupTable m_NowParaGroup = null;  //当前树节点选中的组
        public CParaGroupTable m_DefaultGroup = new CParaGroupTable();
        public CEngParaNewForm()
        {
            InitializeComponent();
        }
        private void CEngParaNewForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < CEngPara.m_CurrentParaDataList.Count; i++)
            {
                m_DefaultGroup.m_ParaDataList.Add(CEngPara.m_CurrentParaDataList[i]);
            }     
            SetListViewMode();
            IniParaLV();
            Init_LV_Para();
            InitionTreeView();         
        }
        private void SetListViewMode()
        {
            LV_Para.ClearAllItem();
            LV_Para.Columns.Add("序号", 50, HorizontalAlignment.Center);
            LV_Para.Columns.Add("参数名称", 250, HorizontalAlignment.Center);          
            LV_Para.Columns.Add("地址", 100, HorizontalAlignment.Center); 
            LV_Para.GridLines = true;
            LV_Para.FullRowSelect = true;
            LV_Para.View = View.Details;
            LV_Para.Scrollable = true;
            LV_Para.MultiSelect = true;
            LV_Para.HeaderStyle = ColumnHeaderStyle.Clickable;
            LV_Para.Visible = true;
            LV_Para.CheckBoxes = true;

            ParaLV.ClearAllItem();
            ParaLV.Columns.Add("序号", 50, HorizontalAlignment.Center);
            ParaLV.Columns.Add("参数名称", 250);
            ParaLV.Columns.Add("类型", 100, HorizontalAlignment.Center);
            ParaLV.Columns.Add("地址", 100, HorizontalAlignment.Center);
            ParaLV.Columns.Add("值", 100, HorizontalAlignment.Center);
            ParaLV.GridLines = true;
            ParaLV.FullRowSelect = true;
            ParaLV.View = View.Details;
            ParaLV.Scrollable = true;
            ParaLV.MultiSelect = true;
            ParaLV.HeaderStyle = ColumnHeaderStyle.Clickable;
            ParaLV.Dock = DockStyle.Fill;
            ParaLV.SetColumnsEditMode(3);
            ParaLV.SetColumnsEditMode(4);
        }      
        private void InitionTreeView()
        {
            int i, j;
            ParaTreeView.Nodes.Clear();
            for (i = 0; i < CGlobal.m_Device.m_ParaList.Count; i++)
            {
                if (CGlobal.m_Device.m_ParaList[i].m_RootIndex > 1)
                {
                    TreeNode pRootNode = new TreeNode();
                    pRootNode.Text = CGlobal.m_Device.m_ParaList[i].m_RootName;
                    pRootNode.Tag = CGlobal.m_Device.m_ParaList[i];
                    ParaTreeView.Nodes.Add(pRootNode);
                    for (j = 0; j < CGlobal.m_Device.m_ParaList[i].m_GroupList.Count; j++)
                    {
                        TreeNode pGroupNode = new TreeNode();
                        pGroupNode.Text = CGlobal.m_Device.m_ParaList[i].m_GroupList[j].m_GroupName;
                        pGroupNode.Tag = CGlobal.m_Device.m_ParaList[i].m_GroupList[j];
                        pRootNode.Nodes.Add(pGroupNode);
                    }
                }
            }
        }
        private void ResetShowStatus()
        {
            int i, j;
            for (j = 0; j < m_NowParaGroup.m_ParaDataList.Count; j++)
            {
                m_NowParaGroup.m_ParaDataList[j].m_bShow = true;
            }

            for (i = 0; i < CEngPara.m_CurrentParaDataList.Count; i++)
            {
                for (j = 0; j < m_NowParaGroup.m_ParaDataList.Count; j++)
                {
                    if (CEngPara.m_CurrentParaDataList[i].m_ParaAddress == m_NowParaGroup.m_ParaDataList[j].m_ParaAddress)
                    {
                        m_NowParaGroup.m_ParaDataList[j].m_bShow = false;
                        break;
                    }
                }
            }
        }
        private void Init_LV_Para()
        {
            if (m_NowParaGroup == null) return;         
            ResetShowStatus();          
            try
            {
                int Cnt = 0;
                string[] strHead = new string[3];
                LV_Para.ClearAllItem(false);
                for (int i = 0; i < m_NowParaGroup.m_ParaDataList.Count; i++)
                {
                    CParaCtrl pPara = m_NowParaGroup.m_ParaDataList[i];
                    if (pPara.m_bShow == false) continue;
                    strHead[0] = Cnt.ToString();
                    Cnt++;
                    strHead[1] = pPara.m_ParaName;                    
                    strHead[2] = pPara.m_ParaAddress.ToString() + "( " + Convert.ToString(pPara.m_ParaAddress, 16).ToUpper() + " )";
                    ListViewItem pItem = new ListViewItem(strHead);
                    pItem.Tag = pPara;
                    this.LV_Para.AddItem(pItem);                   
                }
            }
            catch { }
        }       //初始化参数界面
        private void IniParaLV()
        {         
         
            string[] strHead = new string[5];          
            ParaLV.Items.Clear();
            for (int i = 0; i < CEngPara.m_CurrentParaDataList.Count; i++)
            {
                CParaCtrl pPara = CEngPara.m_CurrentParaDataList[i];
                strHead[0] = i.ToString();
                strHead[1] = pPara.m_ParaName;
                strHead[2] = pPara.GetValueTypeName();
                strHead[3] = pPara.m_ParaAddress.ToString();
                strHead[4] = pPara.m_ParaValue.ToString();
                ListViewItem pItem = new ListViewItem(strHead);
                pItem.Tag = pPara;
                ParaLV.AddItem(pItem);
            }
            ParaLV.Select();
            if (m_ItemTopPlace >= 0 && m_ItemTopPlace < ParaLV.Items.Count)
            {
                ParaLV.TopItem = ParaLV.Items[m_ItemTopPlace];
            }
            if (m_ItemPlace >= 0 && m_ItemPlace < ParaLV.Items.Count)
            {
                ParaLV.Items[m_ItemPlace].Selected = true;
            }
            else
            {
                if (ParaLV.Items.Count > 0)
                {
                    m_ItemPlace = ParaLV.Items.Count - 1;
                    ParaLV.Items[m_ItemPlace].Selected = true;
                }
                else
                    m_ItemPlace = -1;
            }
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            string[] strHead = new string[6];
            strHead[0] = ParaLV.Items.Count.ToString();
            strHead[1] = "参数1";
            strHead[2] = "";
            strHead[3] = "";
            strHead[4] = "";          
            ListViewItem pItem = new ListViewItem(strHead);        
            ParaLV.AddItem(pItem);
            //if ((ParaLV.TopItem.Index + 1) >= ParaLV.Items.Count) return;
           // ParaLV.TopItem = ParaLV.Items[ParaLV.TopItem.Index + 1];
        }
        private void BtnInsert_Click(object sender, EventArgs e)
        {     
            if (this.ParaLV.SelectedItems.Count != 0)    //如果选中的行等于0,就不执行。默认设置了不能多选
            {
                int number = ParaLV.SelectedItems[0].Index;     //用于记录选中行号，加一是因为本来是从0开始计数的。
                string Row_Number = (number + 1).ToString();     //然后将选中的行数加一
                ListViewItem item = new ListViewItem();       //   创建一个listview行的对象
                item.SubItems.Add("");             //  给新增的行第2列添加数据  
                item.SubItems.Add("");             //  给新增的行第3列添加数据
                item.SubItems.Add("");              //  给新增的行第4列添加数据
                item.SubItems.Add("");              //  给新增的行第5列添加数据
                this.ParaLV.Items.Insert(number, item);     //     将新增的对象item插入到指定行
                this.ParaLV.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
                for (int i = 0; i <ParaLV.Items.Count; i++)             // 重新绘制行号，不影响第二，第三列的内容
                {
                    this.ParaLV.Items[i].SubItems[0].Text = (i).ToString();     //添加行号
                }
                this.ParaLV.EndUpdate();  //结束数据处理，UI界面一次性绘制               
            }
        }
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (this.ParaLV.SelectedItems.Count != 0)    //如果选中的行等于0,就不执行。默认设置了不能多选
            {
                int number = ParaLV.SelectedItems[0].Index; 
                this.ParaLV.Items.RemoveAt(number);    
                this.ParaLV.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度
                for (int i = 0; i < ParaLV.Items.Count; i++)             // 重新绘制行号，不影响第二，第三列的内容
                {
                    this.ParaLV.Items[i].SubItems[0].Text = (i).ToString();     //添加行号
                }
                this.ParaLV.EndUpdate();  //结束数据处理，UI界面一次性绘制。                
            }
        }
        private void BtnClear_Click(object sender, EventArgs e)
        {
            this.ParaLV.Items.Clear();
        }      
        private void BtnSave_Click(object sender, EventArgs e)
        {
            CEngPara.m_CurrentParaDataList.Clear();            
            CGlobal.WritePrivateProfileString("ParaInfo", "ParaCnt", ParaLV.Items.Count.ToString(), CEngPara.m_CurrentPath);
            string m_Value;
            for (int i = 0; i < ParaLV.Items.Count; i++)
            {
                if (ParaLV.Items[i].SubItems[3].Text == "")
                {
                    MessageBox.Show("地址不能为空");
                    return;
                }
                int Addr = Convert.ToInt32(ParaLV.Items[i].SubItems[3].Text);

                string value = ParaLV.Items[i].SubItems[4].Text;
                m_Value = Addr.ToString() + "," + value;
                CGlobal.WritePrivateProfileString("ParaInfo", "Para" + i.ToString(), m_Value, CEngPara.m_CurrentPath);
                CParaCtrl pPara = new CParaCtrl(CGlobal.m_Device.m_TotalParaDataList[Addr]);
                pPara.m_ParaValue = value;                
                pPara.m_ParaAddress = Addr;
                pPara.m_Select = false;
                CEngPara.m_CurrentParaDataList.Add(pPara);
            }
        }
        private void CEngParaNewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CEngPara.pform = null;      
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string[] strHead = new string[6];
            int  t=1;
            for (int i = 0; i < 3000; i++)
            {
                strHead[0] = ParaLV.Items.Count.ToString();
                strHead[1] = "参数1";
                strHead[2] = "";
                strHead[3] = (t + i).ToString();
                strHead[4] = "";
                ListViewItem pItem = new ListViewItem(strHead);
                ParaLV.AddItem(pItem);
            }
        }
        private void ParaTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode pSelectNode = ParaTreeView.SelectedNode;
            if (pSelectNode != null && pSelectNode.Tag != null)
            {
                if (pSelectNode.Tag.GetType() == typeof(CParaGroupTable))
                {
                    m_NowParaGroup = (CParaGroupTable)pSelectNode.Tag;
                    Init_LV_Para();
                }
            }
        }
        //添加
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < LV_Para.Items.Count; i++)
            {
                if(LV_Para.Items[i].Checked==true)
                {                    
                    CParaCtrl m_SourceIndex = (CParaCtrl)LV_Para.Items[i].Tag;
                    CParaCtrl pNewPara = new CParaCtrl(m_SourceIndex);
                    pNewPara.m_ParaListType = m_NowParaGroup.m_GroupIndex;
                    CEngPara.m_CurrentParaDataList.Add(pNewPara);
                }
            }
            IniParaLV();
            Init_LV_Para();
        }
        //插入
        private void button2_Click(object sender, EventArgs e)
        {
            if ( m_ItemPlace >= 0)
            {
                for (int i = 0; i < LV_Para.Items.Count; i++)
                {
                    if (LV_Para.Items[i].Checked == true)
                    {
                        CParaCtrl m_SourceIndex = (CParaCtrl)LV_Para.Items[i].Tag;
                        CParaCtrl pNewPara = new CParaCtrl(m_SourceIndex);
                        pNewPara.m_ParaListType = m_NowParaGroup.m_GroupIndex;
                        CEngPara.m_CurrentParaDataList.Insert(m_ItemPlace, pNewPara);
                        m_ItemPlace++;
                    }
                }
                IniParaLV();
                Init_LV_Para();
            }
        }
        //移除
        private void button3_Click(object sender, EventArgs e)
        {
            if (m_ItemIndex != null)
            {
                CEngPara.m_CurrentParaDataList.Remove(m_ItemIndex);
                m_ItemIndex = null;
                IniParaLV();
                Init_LV_Para();
            }
        }
        //保存
        private void button4_Click(object sender, EventArgs e)
        {            
            CGlobal.WritePrivateProfileString("ParaInfo", "ParaCnt", ParaLV.Items.Count.ToString(), CEngPara.m_CurrentPath);
            string m_Value;
            for (int i = 0; i < CEngPara.m_CurrentParaDataList.Count; i++)
            {

                int Addr = CEngPara.m_CurrentParaDataList[i].m_ParaAddress;
                string value = Convert.ToString(CEngPara.m_CurrentParaDataList[i].m_ParaValue);
                m_Value = Addr.ToString() + "," + value;
                CGlobal.WritePrivateProfileString("ParaInfo", "Para" + i.ToString(), m_Value, CEngPara.m_CurrentPath);
            }
            this.Close();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            CEngPara.m_CurrentParaDataList.Clear();
            for (int i = 0; i < m_DefaultGroup.m_ParaDataList.Count; i++)
            {
                CEngPara.m_CurrentParaDataList.Add(m_DefaultGroup.m_ParaDataList[i]);
            }
            this.Close();
        }
        private void ParaLV_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < ParaLV.Items.Count; i++)
            {
                ParaLV.Items[i].BackColor = Color.Empty;
            }
            if (ParaLV.SelectedIndices != null && ParaLV.SelectedIndices.Count > 0)
            {
                m_ItemIndex = (CParaCtrl)ParaLV.SelectedItems[0].Tag;
                m_ItemPlace = ParaLV.SelectedItems[0].Index;
                m_ItemTopPlace = ParaLV.TopItem.Index;
            }
            else
            {
                m_ItemIndex = null;
                m_ItemPlace = -1;
                m_ItemTopPlace = -1;
            }
        }
        private void ParaLV_Validated(object sender, EventArgs e)
        {
            for (int i = 0; i < ParaLV.Items.Count; i++)
            {
                ParaLV.Items[i].BackColor = Color.Empty;
            }
            if (m_ItemPlace >= 0 && m_ItemPlace < ParaLV.Items.Count)
            {
                ParaLV.Items[m_ItemPlace].BackColor = SystemColors.Highlight;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < LV_Para.Items.Count; i++)
            {
               
                    CParaCtrl m_SourceIndex = (CParaCtrl)LV_Para.Items[i].Tag;
                    CParaCtrl pNewPara = new CParaCtrl(m_SourceIndex);
                    pNewPara.m_ParaListType = m_NowParaGroup.m_GroupIndex;
                    CEngPara.m_CurrentParaDataList.Add(pNewPara);
                
            }
            IniParaLV();
            Init_LV_Para();
        }

      
    }
}
