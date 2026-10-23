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
    public partial class CDisplayFileForm : Form
    {
        class fLv
        {
            public string[] name = new string[256];
            public string[,] value = new string[256, 49];
            public string[,] TimeF = new string[256, 49];  //反向功率冻结
            public string[] Time = new string[96];
            public int n;
            public int K;
        }
        fLv FLV = new fLv();
        public CDisplayFileForm()
        {
            InitializeComponent();
        }

        private void CDisplayFileForm_Load(object sender, EventArgs e)
        {
            try
            {
                LV_display();

                for (int i = 0; i < 256; i++)
                {
                    for (int ii = 0; ii < 49; ii++)
                    {
                        FLV.value[i, ii] = "";
                        FLV.TimeF[i, ii] = "";
                    }

                }
            }
            catch
            { }
        }

        //极值
        private void DowithExv()
        {
            LV.Items.Clear();
            LV.Columns.Clear();
            LV.Columns.Add("序号", 80, HorizontalAlignment.Left);
            LV.Columns.Add("名称", 180, HorizontalAlignment.Left);
            LV.Columns.Add("最大值", 150, HorizontalAlignment.Left);
            LV.Columns.Add("最大值发生时间", 200, HorizontalAlignment.Left);
            LV.Columns.Add("最小值", 150, HorizontalAlignment.Left);
            LV.Columns.Add("最小值发生时间", 200, HorizontalAlignment.Left);
            LV.GridLines = true;
            LV.FullRowSelect = true;
            LV.View = View.Details;
            LV.Scrollable = true;
            LV.MultiSelect = false;
            LV.HeaderStyle = ColumnHeaderStyle.Clickable;
            LV.Visible = true;
            string[] h = new string[6];
            string[] maxV = new string[1000];
            string[] minV = new string[1000];

            maxV = CGlobal.m_showfile.value[0].Split(','); //最大值
            minV = CGlobal.m_showfile.value[1].Split(','); //最小值
            int MaxNum = Convert.ToInt32(maxV[0]); //最大值信息对象数目
            for (int i = 0; i < MaxNum; i++)
            {
                h[0] = (i + 1).ToString();//序号
                int Addr = Convert.ToInt32(maxV[1 + i * 3], 16);
                h[1] = CGlobal.m_104Ptr.Lyc[Addr - CGlobal.m_104Ptr.m_YcStartAddress].name;
                h[2] = maxV[2 + i * 3]; //最大值
                h[3] = maxV[3 + i * 3];//最大值时间

                h[4] = minV[2 + i * 3]; //最小值
                h[5] = minV[3 + i * 3];//最小值时间
                ListViewItem item1 = new ListViewItem(h);
                this.LV.Items.Add(item1);
            }
        }

        //SOE
        private void DowithSOE()
        {
            LV.Items.Clear();
            LV.Columns.Clear();
            LV.Columns.Add("序号", 50, HorizontalAlignment.Left);
            LV.Columns.Add("SOE发生时间", 150, HorizontalAlignment.Left);
            LV.Columns.Add("名称", 200, HorizontalAlignment.Left);
            LV.Columns.Add("值", 150, HorizontalAlignment.Left);
            LV.GridLines = true;
            LV.FullRowSelect = true;
            LV.View = View.Details;
            LV.Scrollable = true;
            LV.MultiSelect = false;
            LV.HeaderStyle = ColumnHeaderStyle.Clickable;
            LV.Visible = true;
            string[] h = new string[4];
            string[] ts = new string[3];
            string str = "";
            for (int i = 0; i < CGlobal.m_showfile.value.Length - 1; i++)
            {
                try
                {
                    str = CGlobal.m_showfile.value[i];
                    ts = str.Split(',');
                    h[0] = (i + 1).ToString();//序号
                    int Addr = Convert.ToInt32(ts[0]);
                    h[2] = CGlobal.m_104Ptr.Lyx[Addr - CGlobal.m_104Ptr.m_YxStartAddress].name;
                    h[3] = (ts[1]).ToString();  //值
                    h[1] = ts[2];
                    ListViewItem item1 = new ListViewItem(h);
                    this.LV.Items.Add(item1);
                }
                catch
                { }
            }
        }

        //YK记录
        private void DowithYKRecord()
        {
            LV.Items.Clear();
            LV.Columns.Clear();
            LV.Columns.Add("序号", 100, HorizontalAlignment.Left);
            LV.Columns.Add("遥控发生时间", 150, HorizontalAlignment.Left);
            LV.Columns.Add("名称", 300, HorizontalAlignment.Left);
            LV.Columns.Add("操作类型", 150, HorizontalAlignment.Left);
            LV.Columns.Add("操作命令", 150, HorizontalAlignment.Left);
            LV.GridLines = true;
            LV.FullRowSelect = true;
            LV.View = View.Details;
            LV.Scrollable = true;
            LV.MultiSelect = false;
            LV.HeaderStyle = ColumnHeaderStyle.Clickable;
            LV.Visible = true;
            string[] h = new string[5];
            string[] ts = new string[3];
            string str = "";
            for (int i = 0; i < CGlobal.m_showfile.value.Length - 1; i++)
            {
                try
                {
                    str = CGlobal.m_showfile.value[i];
                    ts = str.Split(',');
                    h[0] = (i + 1).ToString();//序号                  

                    int Addr = Convert.ToInt32(ts[1 + i * 3], 16);
                    h[2] = CGlobal.m_104Ptr.Lyk[Addr - CGlobal.m_104Ptr.m_YkSrartAddress].name;
                    h[3] = ts[1];  //值
                    h[1] = ts[3];
                    h[4] = ts[2];
                    ListViewItem item1 = new ListViewItem(h);
                    this.LV.Items.Add(item1);
                }
                catch
                { }
            }
        }

        //定点
        private void Dowithfixpt()
        {
            string[] ts = new string[1000];
            string str = "";
            for (int i = 0; i < CGlobal.m_showfile.value.Length - 1; i++)
            {
                try
                {
                    str = CGlobal.m_showfile.value[i];
                    ts = str.Split(',');
                    FLV.n = Convert.ToInt32(ts[0]);//数目  

                    FLV.Time[i] = ts[1]; //时间
                    for (int ii = 0; ii < FLV.n; ii++)
                    {
                        int Addr = Convert.ToInt32(ts[1 + i * 3], 16);
                       FLV.name[ii] = CGlobal.m_104Ptr.Lyc[Addr - CGlobal.m_104Ptr.m_YcStartAddress].name;                        
                        FLV.value[i, ii] = ts[ii * 2 + 3];
                    }
                }
                catch
                { }
            }

            LV.Items.Clear();
            LV.Columns.Clear();
            string[] h = new string[2 + FLV.n];
            LV.Columns.Add("序号", 50, HorizontalAlignment.Left);
            LV.Columns.Add("定点记录时间", 140, HorizontalAlignment.Left);
            for (int i = 0; i < FLV.n; i++)
            {
                LV.Columns.Add(FLV.name[i], 140, HorizontalAlignment.Left);
            }
            LV.GridLines = true;
            LV.FullRowSelect = true;
            LV.View = View.Details;
            LV.Scrollable = true;
            LV.MultiSelect = false;
            LV.HeaderStyle = ColumnHeaderStyle.Clickable;
            LV.Visible = true;
            for (int i = 0; i < CGlobal.m_showfile.value.Length - 1; i++)
            {
                try
                {
                    h[0] = (i + 1).ToString();
                    h[1] = FLV.Time[i];
                    for (int ii = 0; ii < FLV.n; ii++)
                    {
                        h[2 + ii] = FLV.value[i, ii];
                    }
                    ListViewItem item1 = new ListViewItem(h);
                    this.LV.Items.Add(item1);
                }
                catch
                { }
            }
        }


        //日志
        private void DowithLog()
        {
            LV.Items.Clear();
            LV.Columns.Clear();
            LV.Columns.Add("序号", 50, HorizontalAlignment.Left);
            LV.Columns.Add("类型", 150, HorizontalAlignment.Left);
            LV.Columns.Add("时间", 150, HorizontalAlignment.Left);
            LV.Columns.Add("内容", 200, HorizontalAlignment.Left);
            LV.Columns.Add("值", 150, HorizontalAlignment.Left);
            LV.GridLines = true;
            LV.FullRowSelect = true;
            LV.View = View.Details;
            LV.Scrollable = true;
            LV.MultiSelect = false;
            LV.HeaderStyle = ColumnHeaderStyle.Clickable;
            LV.Visible = true;
            string[] h = new string[5];
            string[] ts = new string[4];
            string str = "";
            for (int i = 0; i < CGlobal.m_showfile.value.Length - 1; i++)
            {
                try
                {
                    str = CGlobal.m_showfile.value[i];
                    ts = str.Split(',');
                    h[0] = (i + 1).ToString();//序号
                    h[1] = ts[0];
                    h[2] = ts[1];
                    h[3] = ts[2];
                    h[4] = ts[3];

                    ListViewItem item1 = new ListViewItem(h);
                    this.LV.Items.Add(item1);
                }
                catch
                { }
            }
        }

        //日冻结
        private void Dowithfrz()
        {
            string[] ts = new string[1000];
            string str = "";
            for (int i = 0; i < CGlobal.m_showfile.value.Length - 1; i++)
            {
                try
                {
                    str = CGlobal.m_showfile.value[i];
                    ts = str.Split(',');
                    FLV.n = Convert.ToInt32(ts[0]);//数目  

                    FLV.Time[i] = ts[1]; //时间
                    for (int ii = 0; ii < FLV.n; ii++)
                    {
                        try
                        {
                            int Addr = Convert.ToInt32(ts[1 + i * 3], 16);
                            FLV.name[ii] = CGlobal.m_104Ptr.Ldd[Addr - CGlobal.m_104Ptr.m_PowerStartAddress].name;
                            FLV.value[i, ii] = ts[ii * 2 + 3];
                        }
                        catch
                        { }
                    }
                }
                catch
                { }
            }
            LV.Items.Clear();
            LV.Columns.Clear();
            string[] h = new string[2 + FLV.n];
            LV.Columns.Add("序号", 50, HorizontalAlignment.Left);
            LV.Columns.Add("日冻结时间", 160, HorizontalAlignment.Left);
            for (int i = 0; i < FLV.n; i++)
            {
                LV.Columns.Add(FLV.name[i], 160, HorizontalAlignment.Left);
            }
            LV.GridLines = true;
            LV.FullRowSelect = true;
            LV.View = View.Details;
            LV.Scrollable = true;
            LV.MultiSelect = false;
            LV.HeaderStyle = ColumnHeaderStyle.Clickable;
            LV.Visible = true;
            for (int i = 0; i < CGlobal.m_showfile.value.Length - 1; i++)
            {
                try
                {
                    h[0] = (i + 1).ToString();
                    h[1] = FLV.Time[i];
                    for (int ii = 0; ii < FLV.n; ii++)
                    {
                        h[2 + ii] = FLV.value[i, ii];
                    }
                    ListViewItem item1 = new ListViewItem(h);
                    this.LV.Items.Add(item1);
                }
                catch
                { }
            }
        }

        //功率反向电能量冻结值
        private void Dowithflowrev()
        {
            string[] ts = new string[1000];
            string str = "";

            for (int i = 0; i < CGlobal.m_showfile.value.Length - 1; i++)
            {
                try
                {
                    str = CGlobal.m_showfile.value[i];
                    ts = str.Split(',');
                    FLV.n = Convert.ToInt32(ts[0]);//数目
                    for (int ii = 0; ii < FLV.n; ii++)
                    {
                        try
                        {
                            int Addr = Convert.ToInt32(ts[1 + i * 3], 16);
                            FLV.name[ii] = CGlobal.m_104Ptr.Ldd[Addr - CGlobal.m_104Ptr.m_PowerStartAddress].name;
                            FLV.value[i, ii] = (ts[ii * 3 + 2]);  //先放着
                            FLV.TimeF[i, ii] = ts[ii * 3 + 3];
                        }
                        catch
                        { }
                    }
                }
                catch
                { }
            }
            LV.Items.Clear();
            LV.Columns.Clear();
            for (int i = 0; i < FLV.n; i++)
            {
                LV.Columns.Add(FLV.name[i], 220, HorizontalAlignment.Left);
            }
            LV.GridLines = true;
            LV.FullRowSelect = true;
            LV.View = View.Details;
            LV.Scrollable = true;
            LV.MultiSelect = false;
            LV.HeaderStyle = ColumnHeaderStyle.Clickable;
            LV.Visible = true;
            string[] h = new string[FLV.n];
            for (int i = 0; i < FLV.n; i++)
            {
                h[i] = "   时间                    值";
            }
            ListViewItem item1 = new ListViewItem(h);
            item1.BackColor = Color.Yellow;
            this.LV.Items.Add(item1);
            for (int i = 0; i < CGlobal.m_showfile.value.Length - 1; i++)
            {
                try
                {
                    for (int ii = 0; ii < FLV.n; ii++)
                    {
                        h[ii] = FLV.TimeF[i, ii] + "    " + FLV.value[i, ii];
                    }
                    ListViewItem item2 = new ListViewItem(h);
                    item2.BackColor = Color.GreenYellow;
                    this.LV.Items.Add(item2);
                }
                catch
                { }
            }
        }

        public void LV_display()
        {

         /*   int ind = CHisRecordForm.flag_file;
            switch (CGlobal.m_showfile.flag_file)
            {
                case 0://录波文件
                    //DowithLB();
                    break;
                case 1: //极值数据
                    this.Text = "极值数据" + "- [" + CGlobal.m_showfile.SelFileName + "]";
                    DowithExv(); break;
                case 2: //定点数据
                    this.Text = "定点数据" + "- [" + CGlobal.m_showfile.SelFileName + "]";
                    Dowithfixpt(); break;
                case 3: //日冻结能量
                    this.Text = "日冻结能量" + "- [" + CGlobal.m_showfile.SelFileName + "]";
                    Dowithfrz(); break;
                case 4: //功率反向电能量冻结值
                    this.Text = "功率反向电能量冻结值" + "- [" + CGlobal.m_showfile.SelFileName + "]";
                    Dowithflowrev(); break;
                case 5://日志
                    this.Text = "日志" + "- [" + CGlobal.m_showfile.SelFileName + "]";
                    DowithLog();
                    break;
                case 6://SOE记录
                    this.Text = "SOE记录" + "- [" + CGlobal.m_showfile.SelFileName + "]";
                    DowithSOE();
                    break;
                case 7://遥控操作记录
                    this.Text = "遥控操作记录" + "- [" + CGlobal.m_showfile.SelFileName + "]";
                    DowithYKRecord();
                    break;
            }*/
        }
    }
}
