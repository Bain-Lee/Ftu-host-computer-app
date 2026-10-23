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
    public partial class CEngModPara : Form
    {
        public CEngModPara()
        {
            InitializeComponent();
        }
        private void CEngParaNewForm_Load(object sender, EventArgs e)
        {
            IniParaLV();          
        }
        private void IniParaLV()
        {         
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
            ParaLV.SetColumnsEditMode(4);
            string[] strHead = new string[5];          
            ParaLV.Items.Clear();
            for (int i = 0; i < CEngPara.m_SelectParaIndex.Count; i++)
            {
                CParaCtrl pPara = CEngPara.m_CurrentParaDataList[CEngPara.m_SelectParaIndex[i]];
                strHead[0] = CEngPara.m_SelectParaIndex[i].ToString();
                strHead[1] = pPara.m_ParaName;
                strHead[2] = pPara.GetValueTypeName();
                strHead[3] = pPara.m_ParaAddress.ToString();
                strHead[4] = pPara.m_ParaValue.ToString();
                ListViewItem pItem = new ListViewItem(strHead);
                ParaLV.AddItem(pItem);
            }
        }  
        private void BtnSave_Click(object sender, EventArgs e)
        {                    
            CGlobal.WritePrivateProfileString("ParaInfo", "ParaCnt", ParaLV.Items.Count.ToString(), CEngPara.m_CurrentPath);
            string m_Value;
            for (int i = 0; i < ParaLV.Items.Count; i++)
            {
                int Addr = Convert.ToInt32(ParaLV.Items[i].SubItems[3].Text);
                string value = ParaLV.Items[i].SubItems[4].Text;
                m_Value = Addr.ToString() + "," + value;
                CGlobal.WritePrivateProfileString("ParaInfo", "Para" + CEngPara.m_SelectParaIndex[i].ToString(), m_Value, CEngPara.m_CurrentPath);
                CEngPara.m_CurrentParaDataList[CEngPara.m_SelectParaIndex[i]].m_ParaValue = value;
            }
        }

        private void CEngModPara_FormClosing(object sender, FormClosingEventArgs e)
        {
            CEngPara.pModParaForm = null;
        }
      
    }
}
