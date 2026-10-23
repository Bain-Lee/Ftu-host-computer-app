using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace DtuTestManager
{
    public partial class CommuCFG : Form
    {
        private ParaTaskMsg pNowTask = null;
        private List<ParaTaskMsg> m_TaskList = new List<ParaTaskMsg>();
        private int _4GParaCount;
        private int[,] EditMode = new int[256, 2];
        private List<ComboBox> Cbx_List = new List<ComboBox>();     //添加combox控件的

        private int[] m_ComBaund = new int[] { 600, 1200, 2400, 4800, 9600, 14400, 19200, 38400 };
        private List<CIPModule> m_IPGroup = new List<CIPModule>();
        private List<CGateWayModule> m_GWGroup = new List<CGateWayModule>();
        private List<CComModule> m_ComGroup = new List<CComModule>();
        private List<CHostModule> m_HostGroup = new List<CHostModule>();
        private List<CHostModule> m_DevGroup = new List<CHostModule>();
        private bool m_FreshBuff = true;

        string[] m_ChanelName = new string[] { "1.TcpClientChannel", "2.TcpServerChannel", "3.UdpClientChannel", "4.UdpServerChannel", "5.SerialPortChannel" };     //通道名称雷彪
        int[] m_ChanlIndex = new int[] { 1, 2, 3, 4, 5, };
        ComboBox m_ChanelComBox = new ComboBox();                                                                     //通道选择Combox对象

        string[] m_PtrName = new string[] 
        { "1.H101", "2.H104", "3.S101", "4.S104", "5.H101_B", "6.S101_B", "7.Modbus", "8.BF533", "9.SNTP", "10.S104_Console",
          "11.S104_GDW", "12.S104_GDWC ", "13.S104_GDWS", "14.S101_Console", "15.S101_GDW","16.S101_GDWC", "17.S101_GDWS", "18.S101_BConsole", "19.S101_BGDW", "20.S101_BGDWC",
          "21.S101_BGDWS", "22.H101_GDW ", "23.H104_GDW", "24.H101_BGDW", "25.ProtocolTemplate","26.SPRI_TEST", "27.HPRI_TEST", "28.S104_Transmit", "29.H104_Transmit", "30.S101_Transmit",
          "31.H101_Transmit"
        };     //通道名称
        int[] m_PtrlIndex = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 };

        ComboBox m_PtrComBox = new ComboBox();                                                                     //通道选择Combox对象

        CHostModule m_NowSetModule = null;                                                                      //选择通道设置对象
        ComboBox m_NowCombox = null;            //当前使用的Combox
        DataGridView m_NowDataGrid = null;      //当前选择的DataGridView
        int m_RowIndex = 0;                     //选择的行
        int m_ColIndex = 0;                     //选择的列

        public CommuCFG()
        {
            InitializeComponent();
        }

        private void IniSetComboxSelect()
        {
            m_ChanelComBox.SelectedIndex = 0;
        }

        /// <summary>
        /// 通道设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index;
            if (m_NowSetModule != null)
            {
                int nColIndex = (int)(m_NowCombox.Tag);
                int nSelect = m_NowCombox.SelectedIndex;
                if (m_NowDataGrid != null)
                {
                    m_NowDataGrid.Rows[m_RowIndex].Cells[m_ColIndex].Value = m_NowCombox.Text;
                }
                switch (nColIndex)
                {
                    case 2:
                        index = m_NowCombox.SelectedIndex;
                        m_NowSetModule.m_ChanType = (byte)m_ChanlIndex[index];
                        Console.WriteLine("设置通道参数");
                        break;
                    case 3:
                        index = m_NowCombox.SelectedIndex;
                        m_NowSetModule.m_PrtclType = (byte)m_PtrlIndex[index];
                        Console.WriteLine("设置规约参数");
                        break;
                }


                ///添加设置对象值代码
            }
        }
        private void CommuCFG_Load(object sender, EventArgs e)
        {
            m_ChanelComBox.DropDownStyle = ComboBoxStyle.DropDownList;
            m_ChanelComBox.Tag = 0;
            m_ChanelComBox.SelectedIndexChanged += new System.EventHandler(ComBox_SelectedIndexChanged);
            foreach (string strName in m_ChanelName) m_ChanelComBox.Items.Add(strName);
            m_ChanelComBox.SelectedIndex = 0;

            m_PtrComBox.DropDownStyle = ComboBoxStyle.DropDownList;
            m_PtrComBox.Tag = 0;
            m_PtrComBox.SelectedIndexChanged += new System.EventHandler(ComBox_SelectedIndexChanged);
            foreach (string strName in m_PtrName) m_PtrComBox.Items.Add(strName);
            m_PtrComBox.SelectedIndex = 0;


            ReLoadForm();
        }
        public void ReLoadForm()
        {           
            CGlobal.m_Device.m_CommuCFGFormHandle = this.Handle.ToInt32();
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;          
            IniShowDataGridView();          
            Init4GData();
            InitHostData();        
        }
        private void IniSetGridStyle(DataGridView pGrid)
        {
            pGrid.RowHeadersVisible = false;
            pGrid.AllowUserToAddRows = false;
            pGrid.RowsDefaultCellStyle.Font = new Font("宋体",8, FontStyle.Regular);
            pGrid.RowsDefaultCellStyle.ForeColor = Color.Black;
            pGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            pGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            pGrid.MultiSelect = false;
            pGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        private void AddNewRow(string strName, string data, DataTable pDataTable)
        {
            DataRow pRowData = pDataTable.NewRow();
            pRowData[0] = strName;
            pRowData[1] = data;
            pDataTable.Rows.Add(pRowData);
        }
        private void AddNewRow_IP(string port, string IP, string mask, DataTable pDataTable)
        {
            DataRow pRowData = pDataTable.NewRow();
            pRowData[0] = port;
            pRowData[1] = IP;
            pRowData[2] = mask;
            pDataTable.Rows.Add(pRowData);
        }
        private void AddModifyRow_IP(string port, string IP, string mask, int row, DataTable pDataTable)
        {
            pDataTable.Rows[row][0] = port;
            pDataTable.Rows[row][1] = IP;
            pDataTable.Rows[row][2] = mask;
        }
        private void IniShowDataGridView()
        {
            //IniShowDataGridView(SetIPGrid);
            IniShowDataGridView_Mac(SetMacGrid);
            InitMacGridViewData();
            IniShowDataGridView_IP(SetIPGrid);
            IniShowDataGridView_GateWay(SetGateWayGrid);
            IniShowDataGridView_Com(SetComGrid);
            IniShowDataGridView_Host(SetHostGrid);
            IniShowDataGridView_Host(SetDevGrid);
            IniShowDataGridView_CheckBox(Set4GGrid);
            Init4GGridViewData();
            //IniShowDataGridView_CheckBox(SetHostGrid);
            //InitHostGridViewData();
        }
        private void Set4GParaPro(CParaCtrl4G pPara, int m_ParaAddress, ParaType m_ParaType, ParaType m_ProType)
        {
            pPara.m_ParaAddress = m_ParaAddress;
            pPara.m_ParaType = m_ParaType;
            pPara.m_ProParaType = m_ProType;
        }
        private void Init4GData()
        {
            int i = 0;

            ParaType[] Dsptype = new ParaType[] { ParaType.String, ParaType.String, ParaType.String, ParaType.String, ParaType.String, ParaType.IP };

            ParaType[] Protype = new ParaType[] { ParaType.String, ParaType.String, ParaType.String, ParaType.String, ParaType.String, ParaType.Uint };

            EditMode[0, 0] = 1;
            EditMode[0, 1] = 0;


            for (i = 1; i < (Dsptype.Length - 1); i++)
            {
                EditMode[i, 0] = 0;
                EditMode[i, 1] = 0;
            }

            for (i = 0; i < Dsptype.Length; i++)
            {
                CParaCtrl4G pPara = new CParaCtrl4G();
                Set4GParaPro(pPara, i, Dsptype[i], Protype[i]);
                CGlobal.m_4GList.Add(pPara);
            }

            _4GParaCount = Dsptype.Length;
        }
        private void InitHostData()
        {
            int i = 0;

            ParaType[] Dsptype = new ParaType[] { ParaType.IP, ParaType.UShort, ParaType.IP, ParaType.UShort, ParaType.IP, ParaType.UShort, ParaType.IP, ParaType.UShort, ParaType.IP, ParaType.UShort };

            ParaType[] Protype = new ParaType[] { ParaType.Uint, ParaType.UShort, ParaType.Uint, ParaType.UShort, ParaType.Uint, ParaType.UShort, ParaType.Uint, ParaType.UShort, ParaType.Uint, ParaType.UShort };


            // for (i = 0; i < Dsptype.Length; i++)
            // {
            //     EditMode[i,0] = 0;
            //     EditMode[i, 1] = 0;
            // }

            for (i = 0; i < Dsptype.Length; i++)
            {
                CParaCtrl4G pPara = new CParaCtrl4G();
                Set4GParaPro(pPara, i + _4GParaCount, Dsptype[i], Protype[i]);
                CGlobal.m_HostList.Add(pPara);
            }
        }
       private void BindDataMode()
        {

            ComboBox cmb_Temp = new ComboBox();
            cmb_Temp.Items.Add("透传模式");
            cmb_Temp.Items.Add("命令模式");
            cmb_Temp.DropDownStyle = ComboBoxStyle.DropDownList;
            Cbx_List.Add(cmb_Temp);

            /*
             ComboBox cmb_Temp=new ComboBox();
             DataTable dt_TransMode = new DataTable();
             dt_TransMode.Columns.Add("Value");
             dt_TransMode.Columns.Add("Name");
             DataRow dr_Mode;
             dr_Mode = dt_TransMode.NewRow();
             dr_Mode[0] = "0";
             dr_Mode[1] = "透传模式";
             dt_TransMode.Rows.Add(dr_Mode);
             dr_Mode = dt_TransMode.NewRow();
             dr_Mode[0] = "1";
             dr_Mode[1] = "AT命令模式";
             dt_TransMode.Rows.Add(dr_Mode);
             cmb_Temp.ValueMember = "Value";
             cmb_Temp.DisplayMember = "Name";
             cmb_Temp.DataSource = dt_TransMode;
             cmb_Temp.DropDownStyle = ComboBoxStyle.DropDownList;
             Cbx_List.Add(cmb_Temp);

             ComboBox cmb_Temp1 = new ComboBox();
             DataTable dt_sex = new DataTable();
             dt_sex.Columns.Add("Value");
             dt_sex.Columns.Add("Name");
             DataRow dr_sex;
             dr_sex = dt_sex.NewRow();
             dr_sex[0] = "1";
             dr_sex[1] = "男";
             dt_sex.Rows.Add(dr_sex);
             dr_sex = dt_sex.NewRow();
             dr_sex[0] = "0";
             dr_sex[1] = "女";
             dt_sex.Rows.Add(dr_sex);
             cmb_Temp1.ValueMember = "Value";
             cmb_Temp1.DisplayMember = "Name";
             cmb_Temp1.DataSource = dt_sex;
             cmb_Temp1.DropDownStyle = ComboBoxStyle.DropDownList;
             Cbx_List.Add(cmb_Temp1);
   
              */
        }

        private void BindData_4G()
        {
            DataTable pDataTable = (DataTable)Set4GGrid.DataSource;
            AddNewRow("数据传输模式", "", pDataTable);
            AddNewRow("用户名", "", pDataTable);
            AddNewRow("密码", "", pDataTable);
            AddNewRow("APN", "", pDataTable);
            AddNewRow("装置ID", "", pDataTable);
            AddNewRow("隧道IP", "", pDataTable);  
        }

        private void BindData_Mac()
        {
            DataTable pDataTable = (DataTable)SetMacGrid.DataSource;
            AddNewRow("MAC1", "", pDataTable);
            AddNewRow("MAC2", "", pDataTable);
        }

        // 当用户移动到性别这一列时单元格显示下拉列表框
        private void Set4GGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            if (Set4GGrid.CurrentCell == null) return;
            if (Set4GGrid.CurrentCell == null) return;

            try
            {
                int c_row = Set4GGrid.CurrentCell.RowIndex;
                int c_col = Set4GGrid.CurrentCell.ColumnIndex;

                int cbxIndex = EditMode[c_row, 1];
                if (EditMode[c_row, 0] == 1 && Set4GGrid.Columns[c_col].HeaderText == "值")
                {
                    Rectangle rect = Set4GGrid.GetCellDisplayRectangle(Set4GGrid.CurrentCell.ColumnIndex, Set4GGrid.CurrentCell.RowIndex, false);
                    if (cbxIndex < Cbx_List.Count)
                    {
                        Cbx_List[cbxIndex].Left = rect.Left;
                        Cbx_List[cbxIndex].Top = rect.Top;
                        Cbx_List[cbxIndex].Width = rect.Width;
                        Cbx_List[cbxIndex].Height = rect.Height;


                        for (int i = 0; i < Cbx_List.Count; i++)
                            Cbx_List[i].Visible = false;

                        Cbx_List[cbxIndex].Visible = true;
                    }
                    /*
                    string sexValue = Set4GGrid.CurrentCell.Value.ToString();
                    if (sexValue == "透传模式")
                    {
                        Cbx_List[cbxIndex].Text = "透传模式11";
                    }
                    else
                    {
                        Cbx_List[cbxIndex].Text = "AT命令模式22";
                    }
                    */
                }
                else
                {
                    for (int i = 0; i < Cbx_List.Count; i++)
                        Cbx_List[i].Visible = false;
                }

            }
            catch
            {
            }
        }
        // 当用户选择下拉列表框时改变DataGridView单元格的内容
        private void cmb_Temp_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if(Cbx_List[0].Visible ==true)
            //Set4GGrid.CurrentCell.Value = ((ComboBox)sender).SelectedText;
            //Set4GGrid.CurrentCell.Value = ((ComboBox)sender).Text.ToString();
            Set4GGrid.CurrentCell.Value = ((ComboBox)sender).SelectedItem.ToString();
            Set4GGrid.CurrentCell.Tag = ((ComboBox)sender).SelectedIndex.ToString();
            //Set4GGrid.CurrentCell.Tag = ((ComboBox)sender).SelectedValue.ToString();
            /*
            if (((ComboBox)sender).SelectedValue.ToString() == "1")
            {
                Set4GGrid.CurrentCell.Value = ((ComboBox)sender).Text.ToString();
                Set4GGrid.CurrentCell.Tag = "1";
            }
            else if (((ComboBox)sender).SelectedText == "1")
            {
                Set4GGrid.CurrentCell.Value = "AT命令模式";
                Set4GGrid.CurrentCell.Tag = "0";
            }
            */
        }

        /*
        private void cmb_Temp_SelectedIndexChanged1(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedValue == "1")
            {
                Set4GGrid.CurrentCell.Value = "男";
                Set4GGrid.CurrentCell.Tag = "1";
            }
            else
            {
                Set4GGrid.CurrentCell.Value = "女";
                Set4GGrid.CurrentCell.Tag = "0";
            }
        }
        
        // 绑定数据表后将性别列中的每一单元格的Value和Tag属性（Tag为值文本，Value为显示文本）
        private void Set4GGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < this.Set4GGrid.Rows.Count; i++)
            {
                if (Set4GGrid.Rows[i].Cells[2].Value != null && Set4GGrid.Rows[i].Cells[2].ColumnIndex == 2)
                {
                    //Set4GGrid.Rows[i].Cells[2].Tag = Set4GGrid.Rows[i].Cells[2].Value.ToString();
                    if (Set4GGrid.Rows[i].Cells[2].Tag != null)
                    {
                        if (Set4GGrid.Rows[i].Cells[2].Tag.ToString() == "0")
                        {
                            Set4GGrid.Rows[i].Cells[2].Value = "透传模式";   //这样写会重复触发Set4GGrid_DataBindingComplete
                        }
                        else if (Set4GGrid.Rows[i].Cells[2].Tag.ToString() == "1")
                        {
                            Set4GGrid.Rows[i].Cells[2].Value = "AT命令模式";
                        }
                    }
                }
            }
        }

      */

        private void InitMacGridViewData()
        {
            BindData_Mac();
        }

        private void Init4GGridViewData()
        {
            // DataGridViewCheckBoxColumn ChCol = new DataGridViewCheckBoxColumn();
            // ChCol.Name = "CheckBoxRow";
            // ChCol.HeaderText = "选择";
            // // ChCol.Width = 10;
            // ChCol.TrueValue = "1";
            // ChCol.FalseValue = "0";
            // Set4GGrid.Columns.Insert(0, ChCol);

            BindDataMode();
            BindData_4G();




            // 将下拉列表框加入到DataGridView控件中
            for (int i = 0; i < Cbx_List.Count; i++)
            {
                Cbx_List[i].Visible = false;
                this.Set4GGrid.Controls.Add(Cbx_List[i]);
                Cbx_List[i].SelectedIndexChanged += new EventHandler(cmb_Temp_SelectedIndexChanged);
            }
        }


        private void InitHostGridViewData()
        {
            DataTable pDataTable = (DataTable)SetHostGrid.DataSource;

            AddNewRow("IP地址", " ", pDataTable);
            AddNewRow("端口", " ", pDataTable);
            AddNewRow("备用IP地址1", " ", pDataTable);
            AddNewRow("备用端口1", " ", pDataTable);
            AddNewRow("备用IP地址2", " ", pDataTable);
            AddNewRow("备用端口2", " ", pDataTable);
            AddNewRow("备用IP地址3", " ", pDataTable);
            AddNewRow("备用端口3", " ", pDataTable);
            AddNewRow("备用IP地址4", " ", pDataTable);
            AddNewRow("备用端口4", " ", pDataTable);
        }
        private void IniShowDataGridView_GateWay(DataGridView pGridView)
        {
            int i;
            IniSetGridStyle(pGridView);
            DataTable pDataTable = new DataTable();


            pDataTable.Columns.Add("端口号");
            pDataTable.Columns.Add("目标网段");
            pDataTable.Columns.Add("子网掩码");
            pDataTable.Columns.Add("网关");
            pGridView.DataSource = pDataTable;

            IniGridView(pGridView);

            for (i = 0; i < pGridView.ColumnCount; i++)
                pGridView.Columns[i].ReadOnly = true;
        }
        private void IniShowDataGridView_IP(DataGridView pGridView)
        {
            int i;
            IniSetGridStyle(pGridView);
            DataTable pDataTable = new DataTable();

            pDataTable.Columns.Add("端口号");
            pDataTable.Columns.Add("IP地址");
            pDataTable.Columns.Add("子网掩码");
            pGridView.DataSource = pDataTable;

            IniGridView(pGridView);

            for (i = 0; i < pGridView.ColumnCount; i++)
                pGridView.Columns[i].ReadOnly = true;
        }
        private void IniShowDataGridView_Mac(DataGridView pGridView)
        {
            IniSetGridStyle(pGridView);
            DataTable pDataTable = new DataTable();

            pDataTable.Columns.Add("名称");
            pDataTable.Columns.Add("值");
            pGridView.DataSource = pDataTable;

            IniGridView(pGridView);

            pGridView.Columns[0].ReadOnly = true;
            pGridView.SelectionMode = DataGridViewSelectionMode.CellSelect;
            pGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;  //单击进入编辑状态
        }
        private void IniShowDataGridView_Com(DataGridView pGridView)
        {
            int i;
            IniSetGridStyle(pGridView);
            DataTable pDataTable = new DataTable();

            pDataTable.Columns.Add("串口号");
            pDataTable.Columns.Add("波特率");
            pGridView.DataSource = pDataTable;

            IniGridView(pGridView);

            for (i = 0; i < pGridView.ColumnCount; i++)
                pGridView.Columns[i].ReadOnly = true;
        }
        private void IniShowDataGridView_Host(DataGridView pGridView)
        {
            IniSetGridStyle(pGridView);
            DataTable pDataTable = new DataTable();

            pDataTable.Columns.Add("主站号");
            pDataTable.Columns.Add("地址");
            pDataTable.Columns.Add("通道类型");
            pDataTable.Columns.Add("规约类型");
            //
            pDataTable.Columns.Add("本地端口");
            pDataTable.Columns.Add("远方端口");
            pDataTable.Columns.Add("本地IP1");
            pDataTable.Columns.Add("本地IP2");
            pDataTable.Columns.Add("本地IP3");
            pDataTable.Columns.Add("远方IP1");
            pDataTable.Columns.Add("远方IP2");
            pDataTable.Columns.Add("远方IP3");
            pGridView.DataSource = pDataTable;

            IniGridView(pGridView);
            pGridView.Columns[0].ReadOnly = true;       //主站号不可修改
            //pGridView.Columns[2].ReadOnly = true;
            pGridView.SelectionMode = DataGridViewSelectionMode.CellSelect;
            pGridView.Columns[0].HeaderCell.Style.BackColor = Color.LightGray;
            pGridView.Columns[2].HeaderCell.Style.BackColor = Color.LightGray;
            pGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;  //单击进入编辑状态

        }

        private void IniGridView(DataGridView pGridView)
        {
            int i;
            IniSetGridStyle(pGridView);
            DataTable pDataTable = (DataTable)pGridView.DataSource;

          //  pGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //这两个是换标题栏颜色的
            //pGridView.EnableHeadersVisualStyles = false;
            //pGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkSeaGreen;

            for (i = 0; i < pGridView.ColumnCount; i++)
            {
                //ZDD-DEBUG pGridView.Columns[i].DefaultCellStyle.BackColor = Color.LightSteelBlue;//Color.DarkSeaGreen;//Color.LightSteelBlue;//Color.Lavender;
                pGridView.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                pGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                //pGridView.Columns[i].ReadOnly = true;  放在了函数外面,目前只有主站和终端可以直接修改，其他不行
            }
            // pGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            pGridView.Columns[0].Width = 50;

        }

        private void IniShowDataGridView(DataGridView pGridView)
        {
            int i;
            IniSetGridStyle(pGridView);
            DataTable pDataTable = new DataTable();


            pDataTable.Columns.Add("名称");
            pDataTable.Columns.Add("值");

            pGridView.DataSource = pDataTable;

            pGridView.Columns[0].ReadOnly = true;
            // pGridView.Columns[1].ReadOnly = true;   因为可以设置

            pGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            pGridView.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            pGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            //ZDD-DEBUG  pGridView.Columns[0].DefaultCellStyle.BackColor = Color.DarkCyan;

            for (i = 1; i < pGridView.ColumnCount; i++)
            {
                //ZDD-DEBUG pGridView.Columns[i].DefaultCellStyle.BackColor = Color.DarkCyan;// Color.Lavender;
            }
            // pGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            for (i = 0; i < pGridView.Columns.Count; i++)
            {
                pGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            pGridView.Columns[0].Width = 50;
            // pGridView.Columns[1].Width = 480;
        }

        private void IniShowDataGridView_CheckBox(DataGridView pGridView)
        {
            int i;
            IniSetGridStyle(pGridView);
            DataTable pDataTable = new DataTable();


            pDataTable.Columns.Add("名称");
            pDataTable.Columns.Add("值");

            pGridView.DataSource = pDataTable;

            // pGridView.Columns[0].ReadOnly = true;
            pGridView.Columns[1].ReadOnly = true;  //列名

            pGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            pGridView.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            pGridView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            //pGridView.Columns[0].DefaultCellStyle.BackColor = Color.LightSteelBlue;


            for (i = 0; i < pGridView.ColumnCount; i++)
            {
                //ZDD-DEBUG pGridView.Columns[i].DefaultCellStyle.BackColor = Color.LightSteelBlue;//Color.Lavender;
            }

            // pGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            for (i = 0; i < pGridView.Columns.Count; i++)
            {
                pGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            //一共三列了，第一列在前台设计的时候已经搞好为checkBox
            pGridView.Columns[0].Width = 40;
            pGridView.Columns[1].Width = 100;

            pGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;  //单击进入编辑状态
        }

        private string GetGridRowValue(int nRow, DataGridView pGridView)
        {
            string str_gridVies = "";
            if (nRow >= pGridView.RowCount) return str_gridVies;
            DataTable pDataTable = (DataTable)pGridView.DataSource;
            str_gridVies = (string)pDataTable.Rows[nRow][1];
            return str_gridVies.Trim();
        }



        private void AddGridRowValue_GateWay(DataGridView pGridView, CGateWayModule pValue, int row)
        {
            DataTable pDataTable = (DataTable)pGridView.DataSource;
            DataRow pRowData = pDataTable.NewRow();


            pValue.m_NetSegmentString = pValue.m_NetSegment_byte[0].ToString() + "." + pValue.m_NetSegment_byte[1].ToString() + "." + pValue.m_NetSegment_byte[2].ToString() + "." + pValue.m_NetSegment_byte[3].ToString();
            pValue.m_SubnetMaskString = pValue.m_SubnetMask_byte[0].ToString() + "." + pValue.m_SubnetMask_byte[1].ToString() + "." + pValue.m_SubnetMask_byte[2].ToString() + "." + pValue.m_SubnetMask_byte[3].ToString();
            pValue.m_GateWayString = pValue.m_GateWay_byte[0].ToString() + "." + pValue.m_GateWay_byte[1].ToString() + "." + pValue.m_GateWay_byte[2].ToString() + "." + pValue.m_GateWay_byte[3].ToString();

            pRowData[0] = pValue.NetPort.ToString();
            pRowData[1] = pValue.m_NetSegmentString;
            pRowData[2] = pValue.m_SubnetMaskString;
            pRowData[3] = pValue.m_GateWayString;
            pDataTable.Rows.InsertAt(pRowData, row);

            //AddNewRow_IP(IPValue.NetPort.ToString(), IPValue.m_IPString, IPValue.m_SubnetMaskString, pDataTable);
        }

        private void ModifyGridRowValue_GateWay(DataGridView pGridView, CGateWayModule pValue, int row)
        {
            DataTable pDataTable = (DataTable)pGridView.DataSource;
            pDataTable.Rows[row][0] = pValue.NetPort.ToString();
            pDataTable.Rows[row][1] = pValue.m_NetSegmentString;
            pDataTable.Rows[row][2] = pValue.m_SubnetMaskString;
            pDataTable.Rows[row][3] = pValue.m_GateWayString;
            // AddModifyRow_IP(IPValue.NetPort.ToString(), IPValue.m_IPString, IPValue.m_SubnetMaskString,row, pDataTable);
        }

        private void ModifyGridRowValue_Com(DataGridView pGridView, CComModule pValue, int row)
        {
            DataTable pDataTable = (DataTable)pGridView.DataSource;
            pDataTable.Rows[row][0] = pValue.ComPort.ToString();
            pDataTable.Rows[row][1] = pValue.ComBaud.ToString();
            // AddModifyRow_IP(IPValue.NetPort.ToString(), IPValue.m_IPString, IPValue.m_SubnetMaskString,row, pDataTable);
        }
        private void DeleteGridRowValue(DataGridView pGridView, int row)
        {
            DataTable pDataTable = (DataTable)pGridView.DataSource;

            pDataTable.Rows.RemoveAt(row);
        }

        private void AddGridRowValue_IP(DataGridView pGridView, CIPModule pValue, int row)
        {
            DataTable pDataTable = (DataTable)pGridView.DataSource;
            DataRow pRowData = pDataTable.NewRow();

            pValue.m_IPString = pValue.m_IP_byte[0].ToString() + "." + pValue.m_IP_byte[1].ToString() + "." + pValue.m_IP_byte[2].ToString() + "." + pValue.m_IP_byte[3].ToString();
            pValue.m_SubnetMaskString = pValue.m_SubnetMask_byte[0].ToString() + "." + pValue.m_SubnetMask_byte[1].ToString() + "." + pValue.m_SubnetMask_byte[2].ToString() + "." + pValue.m_SubnetMask_byte[3].ToString();

            pRowData[0] = pValue.NetPort.ToString();
            pRowData[1] = pValue.m_IPString;
            pRowData[2] = pValue.m_SubnetMaskString;
            pDataTable.Rows.InsertAt(pRowData, row);

            //AddNewRow_IP(IPValue.NetPort.ToString(), IPValue.m_IPString, IPValue.m_SubnetMaskString, pDataTable);
        }

        private void AddGridRowValue_Com(DataGridView pGridView, CComModule pValue, int row)
        {
            DataTable pDataTable = (DataTable)pGridView.DataSource;
            DataRow pRowData = pDataTable.NewRow();
            pRowData[0] = pValue.ComPort.ToString();
            pRowData[1] = pValue.ComBaud.ToString();
            pDataTable.Rows.InsertAt(pRowData, row);
        }

        private void ModifyGridRowValue_Host(DataGridView pGridView, CHostModule pValue, int row)
        {
            DataTable pDataTable = (DataTable)pGridView.DataSource;
            pDataTable.Rows[row][0] = pValue.m_HostIndex.ToString();
            pDataTable.Rows[row][1] = pValue.m_address.ToString();
            pDataTable.Rows[row][2] = pValue.m_ChanType.ToString();
            pDataTable.Rows[row][3] = pValue.LocalPort;
            pDataTable.Rows[row][4] = pValue.RemotePort;
            pDataTable.Rows[row][5] = pValue.m_LocalIPString1;
            pDataTable.Rows[row][6] = pValue.m_LocalIPString2;
            pDataTable.Rows[row][7] = pValue.m_LocalIPString3;

            pDataTable.Rows[row][8] = pValue.m_RemoteIPString1;
            pDataTable.Rows[row][9] = pValue.m_RemoteIPString2;
            pDataTable.Rows[row][10] = pValue.m_RemoteIPString3;
        }
        private int FindIndex(int[] IntArray, int value)
        {
            int index = -1;
            for (int i = 0; i < IntArray.Length; i++)
            {
                if (IntArray[i] == value)
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        private void AddGridRowValue_Host(DataGridView pGridView, CHostModule pValue, int row)
        {
            DataTable pDataTable = (DataTable)pGridView.DataSource;
            DataRow pRowData = pDataTable.NewRow();

            pValue.m_LocalIPString1 = pValue.m_LocalIP_byte1[0].ToString() + "." + pValue.m_LocalIP_byte1[1].ToString() + "." + pValue.m_LocalIP_byte1[2].ToString() + "." + pValue.m_LocalIP_byte1[3].ToString();
            pValue.m_LocalIPString2 = pValue.m_LocalIP_byte2[0].ToString() + "." + pValue.m_LocalIP_byte2[1].ToString() + "." + pValue.m_LocalIP_byte2[2].ToString() + "." + pValue.m_LocalIP_byte2[3].ToString();
            pValue.m_LocalIPString3 = pValue.m_LocalIP_byte3[0].ToString() + "." + pValue.m_LocalIP_byte3[1].ToString() + "." + pValue.m_LocalIP_byte3[2].ToString() + "." + pValue.m_LocalIP_byte3[3].ToString();

            pValue.m_RemoteIPString1 = pValue.m_RemoteIP_byte1[0].ToString() + "." + pValue.m_RemoteIP_byte1[1].ToString() + "." + pValue.m_RemoteIP_byte1[2].ToString() + "." + pValue.m_RemoteIP_byte1[3].ToString();
            pValue.m_RemoteIPString2 = pValue.m_RemoteIP_byte2[0].ToString() + "." + pValue.m_RemoteIP_byte2[1].ToString() + "." + pValue.m_RemoteIP_byte2[2].ToString() + "." + pValue.m_RemoteIP_byte2[3].ToString();
            pValue.m_RemoteIPString3 = pValue.m_RemoteIP_byte3[0].ToString() + "." + pValue.m_RemoteIP_byte3[1].ToString() + "." + pValue.m_RemoteIP_byte3[2].ToString() + "." + pValue.m_RemoteIP_byte3[3].ToString();

            pRowData[0] = pValue.m_HostIndex.ToString();
            pRowData[1] = pValue.m_address.ToString();

            int m_Index = FindIndex(m_ChanlIndex, pValue.m_ChanType); //找到数值对应的index，再找到对应的名称
            if (m_Index != -1)
                pRowData[2] = m_ChanelName[m_Index];//pValue.m_ChanType.ToString();

            m_Index = FindIndex(m_PtrlIndex, pValue.m_PrtclType);       //找到数值对应的index，再找到对应的名称
            if (m_Index != -1)
                pRowData[3] = m_PtrName[m_Index];                        //pValue.m_PrtclType.ToString();


            pRowData[4] = pValue.LocalPort;
            pRowData[5] = pValue.RemotePort;
            pRowData[6] = pValue.m_LocalIPString1;
            pRowData[7] = pValue.m_LocalIPString2;
            pRowData[8] = pValue.m_LocalIPString3;

            pRowData[9] = pValue.m_RemoteIPString1;
            pRowData[10] = pValue.m_RemoteIPString2;
            pRowData[11] = pValue.m_RemoteIPString3;


            pDataTable.Rows.InsertAt(pRowData, row);
        }

        private void ModifyGridRowValue_Mac(DataGridView pGridView, CMacModule pValue, int row)
        {
            if (row >= 2) return;
            DataTable pDataTable = (DataTable)pGridView.DataSource;

            pDataTable.Rows[row][1] = pValue.m_MacString.ToString();
        }

        private void ModifyGridRowValue_IP(DataGridView pGridView, CIPModule IPValue, int row)
        {
            DataTable pDataTable = (DataTable)pGridView.DataSource;
            pDataTable.Rows[row][0] = IPValue.NetPort.ToString();
            pDataTable.Rows[row][1] = IPValue.m_IPString;
            pDataTable.Rows[row][2] = IPValue.m_SubnetMaskString;
            // AddModifyRow_IP(IPValue.NetPort.ToString(), IPValue.m_IPString, IPValue.m_SubnetMaskString,row, pDataTable);
        }

        private void AddGridRowValue(DataGridView pGridView, CIPModule IPValue, int row)
        {
            //DataTable pDataTable = (DataTable)pGridView.DataSource;
            // pDataTable.Rows[nRow].ItemArray[1] = strValue;

            //pGridView.Rows[nRow].Cells["值"].Value = strValue;
            DataTable pDataTable = (DataTable)pGridView.DataSource;
            AddNewRow("端口" + IPValue.NetPort + " IP", IPValue.m_IPString, pDataTable);
            AddNewRow("端口" + IPValue.NetPort + " 子网掩码", IPValue.m_SubnetMaskString, pDataTable);
            //pDataTable.Rows[pGridView.RowCount][0] = "网口";
            //pDataTable.Rows[pGridView.RowCount + 1][0] = "掩码";
        }

        private void FreshGridRowValue(int nRow, string strValue, DataGridView pGridView)
        {
            if (nRow >= pGridView.RowCount) return;
            //DataTable pDataTable = (DataTable)pGridView.DataSource;
            // pDataTable.Rows[nRow].ItemArray[1] = strValue;

            //pGridView.Rows[nRow].Cells["值"].Value = strValue;
            DataTable pDataTable = (DataTable)pGridView.DataSource;
            pDataTable.Rows[nRow][1] = strValue;
        }

        private void UpdateIPTable()
        {
            m_IPGroup.Clear();
            int i = 0;
            for (i = 0; i < CGlobal.m_IPGroup.Count; i++)
            {
                m_IPGroup.Add(CGlobal.m_IPGroup[i]);
            }

        }

        private void UpdateGWTable()
        {
            m_GWGroup.Clear();
            int i = 0;
            for (i = 0; i < CGlobal.m_GateWayGroup.Count; i++)
            {
                m_GWGroup.Add(CGlobal.m_GateWayGroup[i]);
            }
        }

        private void UpdateComTable()
        {
            m_ComGroup.Clear();
            foreach (CComModule OneComPort in CGlobal.m_GComList)
            {
                m_ComGroup.Add(OneComPort);
            }
        }


        private void UpdateHostTable()
        {
            m_HostGroup.Clear();
            foreach (CHostModule OneHostPort in CGlobal.m_GHostList)
            {
                m_HostGroup.Add(OneHostPort);
            }
        }

        private void UpdateDevTable()
        {
            m_DevGroup.Clear();
            foreach (CHostModule OneHostDev in CGlobal.m_GDevList)
            {
                m_DevGroup.Add(OneHostDev);
            }
        }

        protected override void WndProc(ref Message m)
        {
            //const int WM_SYSCOMMAND = 0x0112;

            switch (m.Msg)
            {
                /*
            case WM_SYSCOMMAND:     //关闭窗体
                try
                {
                    const int SC_CLOSE = 0xF060;
                    if ( (int)m.WParam == SC_CLOSE)
                     CGlobal.m_Device.CloseThread();
                }
                catch { }
                break;
            */

                case UseMessage4G.MY_MSG_1:
                    Fresh4GPara();
                    //MessageBox.Show("参数读取成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);              
                    break;
                case UseMessage4G.MY_MSG_2:
                    UpdateIPTable();
                    FreshGridPara_IP();
                    //MessageBox.Show("参数读取成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);              
                    break;
                case UseMessage4G.MY_MSG_3:
                    UpdateGWTable();
                    FreshGridPara_GateWay();
                    //MessageBox.Show("参数读取成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);              
                    break;

                case UseMessage4G.MY_MSG_4:
                    UpdateComTable();
                    FreshGridPara_Com();
                    //MessageBox.Show("参数读取成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);              
                    break;

                case UseMessage4G.MY_MSG_5:
                    UpdateHostTable();
                    FreshHostPara();
                    //MessageBox.Show("参数读取成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);              
                    break;

                case UseMessage4G.MY_MSG_6:
                    MessageBox.Show("应答超时", "提示", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    break;

                case UseMessage4G.MY_MSG_7:
                    MessageBox.Show("参数设置失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    break;

                case UseMessage4G.MY_MSG_8:
                    if (MessageBox.Show("参数设置成功,重启生效,是否重启？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        CGlobal.SendTaskOrder((int)TI_Type.Restart_D);
                    }
                    break;

                case UseMessage4G.MY_MSG_9:
                    MessageBox.Show("参数设置成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
                    break;

                case UseMessage4G.MY_MSG_10:
                    UpdateDevTable();
                    FreshDevPara();
                    break;

                case UseMessage4G.MY_MSG_11:
                    FreshMacPara();
                    break;
            }
            base.WndProc(ref m);
        }

        private void FreshHostPara()
        {
            int i;

            DataTable pDataTable = (DataTable)SetHostGrid.DataSource;
            pDataTable.Clear();

            for (i = 0; i < m_HostGroup.Count; i++)
            {
                AddGridRowValue_Host(SetHostGrid, m_HostGroup[i], i);
            }

        }

        private void FreshDevPara()
        {
            int i;

            DataTable pDataTable = (DataTable)SetDevGrid.DataSource;
            pDataTable.Clear();

            for (i = 0; i < m_DevGroup.Count; i++)
            {
                AddGridRowValue_Host(SetDevGrid, m_DevGroup[i], i);
            }
        }

        private void FreshMacPara()
        {
            int i;

            for (i = 0; i < CGlobal.m_MacGroup.Count; i++)
            {
                ModifyGridRowValue_Mac(SetMacGrid, CGlobal.m_MacGroup[i], i);      //这里需要验证下，不行需要用4G的方式
            }

        }
        private void Fresh4GPara()
        {
            string str_value = null;
            int i;

            for (i = 0; i < Set4GGrid.Rows.Count; i++)
            {
                if (CGlobal.m_4GList[i].m_Select == true)
                {
                    if (CGlobal.m_4GList[i].m_ParaValue == null)
                        continue;
                    str_value = CGlobal.m_4GList[i].m_ParaValue.ToString();

                    if (EditMode[i, 0] == 1)    //combox模式
                    {
                        int cbxIndex = EditMode[i, 1];
                        if (str_value == "0")
                        {
                            str_value = Cbx_List[cbxIndex].Items[0].ToString();//"透传模式";  //
                            //str_value = Cbx_List[cbxIndex].GetItemText(Cbx_List[cbxIndex].Items[0]);
                            Set4GGrid.Rows[0].Cells["值"].Tag = "0"; ;
                        }
                        else
                        {
                            str_value = Cbx_List[cbxIndex].Items[1].ToString(); // "AT命令模式";//
                            Set4GGrid.Rows[0].Cells["值"].Tag = "1";
                        }

                    }
                    SetGridValue(Set4GGrid, i, str_value);
                }
            }
        }
        private void FreshGridPara_IP()
        {
            int i;

            DataTable pDataTable = (DataTable)SetIPGrid.DataSource;
            pDataTable.Clear();

            for (i = 0; i < m_IPGroup.Count; i++)
            {
                AddGridRowValue_IP(SetIPGrid, m_IPGroup[i], i);
            }
        }

        private void FreshGridPara_GateWay()
        {
          
            int i;
            DataTable pDataTable = (DataTable)SetGateWayGrid.DataSource;
            pDataTable.Clear();

            for (i = 0; i < m_GWGroup.Count; i++)
            {
                //AddGateWayGridRowValue(SetGateWayGrid, m_GWGroup[i], i);
                AddGridRowValue_GateWay(SetGateWayGrid, m_GWGroup[i], i);

            }
        }

        private void FreshGridPara_Com()
        {
            int i = 0;
            DataTable pDataTable = (DataTable)SetComGrid.DataSource;
            pDataTable.Clear();

            foreach (CComModule OneComPort in m_ComGroup)
            {
                AddGridRowValue_Com(SetComGrid, OneComPort, i);
                i++;

            }
        }
        private static string FindKeyString(string stStr, string EndStr, string DesString)
        {
            string KeyStr = "";
            int st = DesString.IndexOf(stStr);
            if (st < 0) return KeyStr;
            int end = DesString.IndexOf(EndStr, st);
            if (end < 0) return KeyStr;
            if ((end - st - stStr.Length + 1) < 0) return KeyStr;
            KeyStr = DesString.Substring(st + stStr.Length, end - st - stStr.Length + 1);
            return KeyStr;
        }
        public void WriteFileCmd(string fileStr, string strFilePath)
        {
           
            File.WriteAllText(strFilePath, fileStr);
        }  
        private void DevAddBtn_Click(object sender, EventArgs e)
        {
            CAddIPForm pForm = new CAddIPForm("添加IP");

            if (pForm.ShowDialog() == DialogResult.OK)
            {
                if (pForm.TempMyIP != null)
                {
                    m_IPGroup.Add(pForm.TempMyIP);                            //全局数据更新
                    AddGridRowValue_IP(SetIPGrid, pForm.TempMyIP, SetIPGrid.RowCount);      //更新datagridview相关的数据库
                  
                }
            }
        }

        private void DevClearBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否清空所有数据？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                DataTable pDataTable = (DataTable)SetIPGrid.DataSource;
                pDataTable.Clear();

                m_IPGroup.Clear();
            }
            //SetIPGrid.Rows.Clear();
        }

        private void DevDeleteBtn_Click(object sender, EventArgs e)
        {
            DataGridView pDataGrid = new DataGridView();
            pDataGrid = SetIPGrid;

            DataTable pDataTable = (DataTable)pDataGrid.DataSource;
            if (pDataTable.Rows.Count == 0) return;
            int SelectRow = pDataGrid.CurrentRow.Index;
            if (MessageBox.Show("是否删除当前数据组？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                m_IPGroup.RemoveAt(SelectRow);
                DeleteGridRowValue(pDataGrid, SelectRow);
                //FreshGridPara_IP();
                return;
            }
        }

        private void DevInsertBtn_Click(object sender, EventArgs e)
        {
            DataGridView pDataGrid = new DataGridView();
            pDataGrid = SetIPGrid;

            DataTable pDataTable = (DataTable)pDataGrid.DataSource;
            CAddIPForm pForm = new CAddIPForm("添加IP");
            int insertIndex;
            if (pForm.ShowDialog() == DialogResult.OK)
            {
                if (pForm.TempMyIP != null)
                {
                    if (pDataTable.Rows.Count > 0)
                    {
                        insertIndex = pDataGrid.CurrentRow.Index;
                    }
                    else
                    {
                        insertIndex = 0;
                    }

                    m_IPGroup.Insert(insertIndex, pForm.TempMyIP);
                    AddGridRowValue_IP(pDataGrid, pForm.TempMyIP, insertIndex);
                    //FreshGridPara_IP();

                }
            }
        }


        private void GWAddBtn_Click(object sender, EventArgs e)
        {
            CAddGateWay pForm = new CAddGateWay("添加网关");
            if (pForm.ShowDialog() == DialogResult.OK)
            {
                if (pForm.TempMyGateWay != null)
                {
                    m_GWGroup.Add(pForm.TempMyGateWay);     //全局数据更新
                    // FreshGridPara_GateWay();
                    AddGridRowValue_GateWay(SetGateWayGrid, pForm.TempMyGateWay, SetGateWayGrid.RowCount);      //更新datagridview相关的数据库
                }
            }
        }

        private void GWInsertBtn_Click(object sender, EventArgs e)
        {
            DataTable pDataTable = (DataTable)SetGateWayGrid.DataSource;
            CAddGateWay pForm = new CAddGateWay("添加网关");
            int insertIndex;
            if (pForm.ShowDialog() == DialogResult.OK)
            {
                if (pForm.TempMyGateWay != null)
                {
                    if (pDataTable.Rows.Count > 0)
                        insertIndex = SetGateWayGrid.CurrentRow.Index;
                    // m_GWGroup.Insert(SetGateWayGrid.CurrentRow.Index / 3 + 1, pForm.TempMyGateWay);
                    else
                        insertIndex = 0;
                    // m_GWGroup.Add(pForm.TempMyGateWay);

                    m_GWGroup.Insert(insertIndex, pForm.TempMyGateWay);
                    AddGridRowValue_GateWay(SetGateWayGrid, pForm.TempMyGateWay, insertIndex);
                    //FreshGridPara_GateWay();
                }
            }
        }

        private void GWDeleteBtn_Click(object sender, EventArgs e)
        {
            DataGridView pDataGrid = new DataGridView();
            pDataGrid = SetGateWayGrid;

            DataTable pDataTable = (DataTable)pDataGrid.DataSource;
            if (pDataTable.Rows.Count == 0) return;
            int SelectRow = pDataGrid.CurrentRow.Index;
            if (MessageBox.Show("是否删除当前数据组？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                m_GWGroup.RemoveAt(SelectRow);
                DeleteGridRowValue(pDataGrid, SelectRow);
                //FreshGridPara_GateWay();
                return;
            }
        }

        private void GWClearBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否清空所有数据？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                DataTable pDataTable = (DataTable)SetGateWayGrid.DataSource;
                pDataTable.Clear();

                m_GWGroup.Clear();
            }
        }

        private void IPSaveBtn_Click(object sender, EventArgs e)
        {
            if (m_IPGroup.Count <= 0) return;

            if (MessageBox.Show("是否保存参数？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                int i;
                if (m_IPGroup.Count <= 0) return;

                List<CIPModule> IP0 = new List<CIPModule>();
                List<CIPModule> IP1 = new List<CIPModule>();

                IP0.Clear();
                IP1.Clear();
                CGlobal.m_IPGroup.Clear();

                for (i = 0; i < m_IPGroup.Count; i++)
                {
                    if (m_IPGroup[i].NetPort == 0)
                        IP0.Add(m_IPGroup[i]);
                    else if (m_IPGroup[i].NetPort == 1)
                        IP1.Add(m_IPGroup[i]);
                }

                foreach (CIPModule oneIP in IP0)
                    CGlobal.m_IPGroup.Add(oneIP);

                foreach (CIPModule oneIP in IP1)
                    CGlobal.m_IPGroup.Add(oneIP);

                //CGlobal.SendTaskOrder(0x88);
                GetParaOperationList(Convert.ToInt32(TI_Type.W_NT_PA_1), 20, false);
            }
        }



        private bool CheckSelParaRight()
        {
            int i = 0;
            Byte[] pValueByte = null;
            try
            {
                for (i = 0; i < CGlobal.m_Device.m_SelectParaList4G.Count; i++)
                {
                    CParaCtrl4G pPara = CGlobal.m_Device.m_SelectParaList4G[i];
                    int nLength = pPara.GetValueBuff(ref pValueByte);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool GetGridValue(DataGridView pGrid, int row, ref string str_Value, int read0_save1)
        {
            //pGrid.CurrentCell = null;  //因为checkbox最后一行选中的光标如果也停留,读出来还是false,加上这句，可以解决这个问题
            pGrid.EndEdit();
            bool check = Convert.ToBoolean(pGrid.Rows[row].Cells[0].FormattedValue);
            if (check == false)
                return false;
            else
            {
                if (read0_save1 == 1)
                {
                    if (EditMode[row, 0] == 0)
                        str_Value = pGrid.Rows[row].Cells["值"].Value.ToString();
                    else
                    {
                        if (pGrid.Rows[row].Cells["值"].Tag != null)
                            str_Value = pGrid.Rows[row].Cells["值"].Tag.ToString();
                        else
                            str_Value = "";
                    }
                }
                return true;
            }
        }

        private bool GetGridValue_Host(DataGridView pGrid, int row, ref string str_Value, int read0_save1)
        {
            pGrid.CurrentCell = null;  //因为checkbox最后一行选中的光标如果也停留,读出来还是false,加上这句，可以解决这个问题

            bool check = Convert.ToBoolean(pGrid.Rows[row].Cells[0].FormattedValue);
            if (check == false)
                return false;
            else
            {
                if (read0_save1 == 1)
                {
                    str_Value = pGrid.Rows[row].Cells["值"].Value.ToString();
                }
                return true;
            }
        }


        private void SetGridValue(DataGridView pGrid, int row, string str_Value)
        {
            pGrid.CurrentCell = null;  //因为checkbox最后一行选中的光标如果也停留,读出来还是false,加上这句，可以解决这个问题

            bool check = Convert.ToBoolean(pGrid.Rows[row].Cells[0].FormattedValue);
            if (check == true)
                pGrid.Rows[row].Cells["值"].Value = str_Value;
        }

        private void Btn_4GSave_Click(object sender, EventArgs e)
        {
            if (Set4GGrid.Rows.Count <= 0) return;
            if (MessageBox.Show("是否保存参数？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                string str_value = null;
                int i;
                //UpdateSelectPara(1);

                CGlobal.m_Device.m_SelectParaList4G.Clear();

                for (i = 0; i < Set4GGrid.Rows.Count; i++)
                {
                    if (GetGridValue(Set4GGrid, i, ref str_value, 1))
                    {
                        CGlobal.m_4GList[i].m_ParaValue = str_value;
                        CGlobal.m_Device.m_SelectParaList4G.Add(CGlobal.m_4GList[i]);
                    }
                }

                if (CheckSelParaRight() == false)
                {
                    MessageBox.Show("参数输入异常", "提示");
                    return;
                }

                if (CGlobal.m_Device.m_SelectParaList4G.Count <= 0) return;
                int nGroupNo = 20;
                int nMaxCount = CGlobal.m_Device.m_SelectParaList4G.Count;
                ParaTaskMsg pNewTask = new ParaTaskMsg();
                pNewTask.m_TaskNo = 0x86;
                //pNewTask.m_AnswerFlag = false;
                pNewTask.m_StartPalce = 0;
                if (nMaxCount >= nGroupNo)
                    pNewTask.m_ParaCount = nGroupNo;
                else
                    pNewTask.m_ParaCount = nMaxCount;

                pNewTask.m_EndFlag = true;
                pNewTask.m_AnswerFlag = false;

                CGlobal.m_Device.m_Ptr.m_NowParaTask = pNewTask;        //当前任务相关的所有信息放到全局变量中

                CGlobal.SendTaskOrder(pNewTask.m_TaskNo);                //下发4G参数
            }
        }

        private void Btn_4GRead_Click(object sender, EventArgs e)
        {
            int i;
            string str_value = null;

            CGlobal.m_Device.m_SelectParaList4G.Clear();

            for (i = 0; i < Set4GGrid.Rows.Count; i++)
            {
                if (GetGridValue(Set4GGrid, i, ref str_value, 0))
                {
                    CGlobal.m_4GList[i].m_Select = true;
                    CGlobal.m_Device.m_SelectParaList4G.Add(CGlobal.m_4GList[i]);
                }
                else
                    CGlobal.m_4GList[i].m_Select = false;
            }

            if (CGlobal.m_Device.m_SelectParaList4G.Count <= 0) return;

            GetParaOperationList(Convert.ToInt32(TI_Type.R_4G_PA_1), 20, false);
           
        }

        private void IPReadBtn_Click(object sender, EventArgs e)
        {
            CGlobal.m_IPGroup.Clear();
            CGlobal.SendTaskOrder((int)TI_Type.R_NT_PA_1);
        }

        private void GWReadBtn_Click(object sender, EventArgs e)
        {
            CGlobal.m_GateWayGroup.Clear();
            CGlobal.SendTaskOrder((int)TI_Type.R_GW_PA_1);
        }

        private void GWSaveBtn_Click(object sender, EventArgs e)
        {
            int i;
            if (m_GWGroup.Count <= 0) return;

            if (MessageBox.Show("是否保存参数？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                List<CGateWayModule> GW0 = new List<CGateWayModule>();
                List<CGateWayModule> GW1 = new List<CGateWayModule>();

                GW0.Clear();
                GW1.Clear();
                CGlobal.m_GateWayGroup.Clear();
                for (i = 0; i < m_GWGroup.Count; i++)
                {
                    if (m_GWGroup[i].NetPort == 0)
                        GW0.Add(m_GWGroup[i]);
                    else if (m_GWGroup[i].NetPort == 1)
                        GW1.Add(m_GWGroup[i]);
                }

                foreach (CGateWayModule oneGW in GW0)
                    CGlobal.m_GateWayGroup.Add(oneGW);
                foreach (CGateWayModule oneGW in GW1)
                    CGlobal.m_GateWayGroup.Add(oneGW);

                //CGlobal.SendTaskOrder((int)TI_Type.W_GW_PA_1);
                GetParaOperationList(Convert.ToInt32(TI_Type.W_GW_PA_1), 10, false);
            }
        }

        public void ShowBuffer(string strBuffName, Byte[] pBuffer, int nLeng)
        {
            if (m_FreshBuff == false || nLeng <= 0) return;
            string strOut;
            int i;
            //if (strBuffName == "接收")
            //    RevFlag = 1;
            // RevEvent.Set();         //????
            strOut = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            strOut += "  " + strBuffName + "  长度：" + nLeng.ToString();
            strOut += "\r\n";
            for (i = 0; i < nLeng; i++)
            {
                strOut += String.Format("{0:X2}", pBuffer[i]);
                strOut += " ";
            }
            //txt_Showbuf.AppendText(strOut + "\r\n"); 

            txt_Showbuf.Invoke
        (
              new MethodInvoker
              (
                  delegate
                  {
                      txt_Showbuf.AppendText(strOut + "\r\n");
                      //if (strBuffName == "接收")
                      //    RecvFlag = true;// BufferText.AppendText(CGlobal.m_Device.m_Ptr.GetBuffContent(pBuffer));
                  }
              )
        );

        }

        private void BtnStartFreshBuff_Click(object sender, EventArgs e)
        {
            m_FreshBuff = true;
            txt_Showbuf.AppendText("开始刷新\r\n");
        }

        private void BtnStopFreshBuff_Click(object sender, EventArgs e)
        {
            m_FreshBuff = false;
            txt_Showbuf.AppendText("停止刷新\r\n");
        }

        private void BtnClearBuff_Click(object sender, EventArgs e)
        {
            txt_Showbuf.Text = "";
        }

        private void BtnSaveSet_Click(object sender, EventArgs e)
        {
            // if (radioButton_Serial.Checked)
            // {
            CGlobal.m_Device.m_Connection.m_nConnType = 0;
            // }
            // else
            // {
            //     CGlobal.m_Device.m_Connection.m_nConnType = 1;
            // }
            // if (radioButton_101.Checked)
            //  {
            CGlobal.m_Device.m_Connection.m_n_ProType = 0;
            // }
            // else
            //{
            //     CGlobal.m_Device.m_Connection.m_n_ProType = 1;
            //}

            // CGlobal.m_Device.m_Connection.m_strIP_Address = IPByte1.Text + "." + IPByte2.Text + "." + IPByte3.Text + "." + IPByte4.Text;
            // CGlobal.m_Device.m_Connection.m_nIP_Port = Convert.ToInt32(ConnPort.Text);
            //CGlobal.m_Device.m_Connection.m_SocketMode = m_ConnType.SelectedIndex;

        
            CGlobal.m_Device.m_Connection.m_strutComOption.m_nCom_Baund = 9600;//Convert.ToInt32(Combox_ComBaund.Text);
            CGlobal.m_Device.m_Connection.m_strutComOption.m_nCom_DataBit = 8;//Convert.ToInt32(Combox_DataBit.Text);
            CGlobal.m_Device.m_Connection.m_strutComOption.m_nCom_StopBit = 1; //Convert.ToInt32(Combox_StopBit.Text);
            CGlobal.m_Device.m_Connection.m_strutComOption.m_nCom_CheckBit = 0; //Convert.ToInt32(Combox_Parity.SelectedIndex);

            // CGlobal.m_Device.m_Connection.m_104Address.m_AsduAddress = Convert.ToInt32(AsduAddress.Text);


            //  CGlobal.m_Device.m_Connection.m_101Address.m_AsduAddress = Convert.ToInt32(AsduAddress.Text);

            //CGlobal.m_Device.m_Connection.m_101Address.m_AddressType = comboBox_AddrLen.SelectedIndex;
            //CGlobal.m_Device.m_Connection.m_101Address.m_CotType = comboBox_SendLen.SelectedIndex;   


            CGlobal.m_Device.InitConnect();

            CGlobal.m_Device.m_Connection.IsEncode = false;

            CGlobal.WriteComPara();
        }

        private void DevModifyBtn_Click(object sender, EventArgs e)
        {
            int sel_index;

            DataGridView pDataGrid = new DataGridView();
            pDataGrid = SetIPGrid;

            if (pDataGrid.SelectedCells.Count != 0)
            {
                sel_index = pDataGrid.CurrentRow.Index;

                CAddIPForm pForm = new CAddIPForm("修改IP");
                pForm.Tag = m_IPGroup[sel_index];

                if (pForm.ShowDialog() == DialogResult.OK)
                {
                    if (pForm.TempMyIP != null)
                    {
                        //m_IPGroup.Add(pForm.TempMyIP);
                        ModifyGridRowValue_IP(pDataGrid, pForm.TempMyIP, sel_index);         //更新datagridview相关的数据库
                        m_IPGroup[sel_index] = pForm.TempMyIP;                               //修改数据里对应index的值
                    }
                }
            }
            else
            {
                MessageBox.Show("未选择项。先选择一个项。", "提示", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }

        private void GWModifyBtn_Click(object sender, EventArgs e)
        {
            int sel_index;

            DataGridView pDataGrid = new DataGridView();
            pDataGrid = SetGateWayGrid;

            if (pDataGrid.SelectedCells.Count != 0)
            {
                sel_index = pDataGrid.CurrentRow.Index;

                CAddGateWay pForm = new CAddGateWay("修改网关");
                pForm.Tag = m_GWGroup[sel_index];

                if (pForm.ShowDialog() == DialogResult.OK)
                {
                    if (pForm.TempMyGateWay != null)
                    {
                        //m_IPGroup.Add(pForm.TempMyIP);
                        ModifyGridRowValue_GateWay(pDataGrid, pForm.TempMyGateWay, sel_index);         //更新datagridview相关的数据库
                        m_GWGroup[sel_index] = pForm.TempMyGateWay;                               //修改数据里对应index的值
                    }
                }
            }
            else
            {
                MessageBox.Show("未选择项。先选择一个项。", "提示", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }
        private bool ComPortExist(CComModule myCom)
        {
            foreach (CComModule OneComPort in m_ComGroup)
            {
                if (OneComPort.ComPort == myCom.ComPort)
                    return true;
            }


            return false;
        }

        private void Btn_Add_COM_Click(object sender, EventArgs e)
        {
            CAddCom pForm = new CAddCom("添加串口");


            if (pForm.ShowDialog() == DialogResult.OK)
            {
                if (pForm.TempMyCOM != null)
                {
                    if (ComPortExist(pForm.TempMyCOM) == false)
                    {
                        m_ComGroup.Add(pForm.TempMyCOM);                            //全局数据更新

                        AddGridRowValue_Com(SetComGrid, pForm.TempMyCOM, SetComGrid.RowCount);      //更新datagridview相关的数据库

                    }
                    else
                    {
                        MessageBox.Show("添加的串口号已存在。", "提示", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);

                    }

                }
            }

        }

        private void Btn_Modify_COM_Click(object sender, EventArgs e)
        {
            int sel_index;

            DataGridView pDataGrid = new DataGridView();
            pDataGrid = SetComGrid;

            if (pDataGrid.SelectedCells.Count != 0)
            {
                sel_index = pDataGrid.CurrentRow.Index;

                CAddCom pForm = new CAddCom("修改串口");
                pForm.Tag = m_ComGroup[sel_index];

                if (pForm.ShowDialog() == DialogResult.OK)
                {
                    if (pForm.TempMyCOM != null)
                    {
                        ModifyGridRowValue_Com(pDataGrid, pForm.TempMyCOM, sel_index);         //更新datagridview相关的数据库
                        m_ComGroup[sel_index] = pForm.TempMyCOM;                               //修改数据里对应index的值
                    }
                }
            }
            else
            {
                MessageBox.Show("未选择项。先选择一个项。", "提示", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }

        private void Btn_Insert_COM_Click(object sender, EventArgs e)
        {
            DataGridView pDataGrid = new DataGridView();
            pDataGrid = SetComGrid;

            DataTable pDataTable = (DataTable)pDataGrid.DataSource;
            CAddCom pForm = new CAddCom("添加串口");
            int insertIndex;
            if (pForm.ShowDialog() == DialogResult.OK)
            {
                if (pForm.TempMyCOM != null)
                {
                    if (ComPortExist(pForm.TempMyCOM) == false)
                    {
                        if (pDataTable.Rows.Count > 0)
                        {
                            insertIndex = pDataGrid.CurrentRow.Index;
                        }
                        else
                        {
                            insertIndex = 0;
                        }

                        m_ComGroup.Insert(insertIndex, pForm.TempMyCOM);
                        AddGridRowValue_Com(pDataGrid, pForm.TempMyCOM, insertIndex);
                    }

                    else
                    {
                        MessageBox.Show("添加的串口号已存在。", "提示", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);

                    }

                }
            }
        }

        private void Btn_Del_COM_Click(object sender, EventArgs e)
        {
            DataGridView pDataGrid = new DataGridView();
            pDataGrid = SetComGrid;

            DataTable pDataTable = (DataTable)pDataGrid.DataSource;
            if (pDataTable.Rows.Count == 0) return;
            int SelectRow = pDataGrid.CurrentRow.Index;
            if (MessageBox.Show("是否删除当前数据组？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                m_ComGroup.RemoveAt(SelectRow);
                DeleteGridRowValue(pDataGrid, SelectRow);
                //FreshGridPara_IP();
                return;
            }
        }

        private void Btn_Clear_COM_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否清空所有数据？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                DataTable pDataTable = (DataTable)SetComGrid.DataSource;
                pDataTable.Clear();

                m_ComGroup.Clear();
            }
        }

        private void Btn_Read_COM_Click(object sender, EventArgs e)
        {
            CGlobal.m_GComList.Clear();
            CGlobal.SendTaskOrder(0x8b);
        }

        private void Btn_Save_COM_Click(object sender, EventArgs e)
        {
            if (m_ComGroup.Count <= 0) return;
            if (MessageBox.Show("是否保存参数？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                CGlobal.m_GComList.Clear();         //更新全局变量
                foreach (CComModule OneCom in m_ComGroup)
                    CGlobal.m_GComList.Add(OneCom);

                //CGlobal.SendTaskOrder((int)TI_Type.W_SR_PA_1);
                GetParaOperationList(Convert.ToInt32(TI_Type.W_SR_PA_1), 20, false);
            }

        }

        private void Btn_HostSave_Click(object sender, EventArgs e)
        {
            string str_value = null;
            int i;

            CGlobal.m_Device.m_SelectParaList4G.Clear();

            for (i = 0; i < SetHostGrid.Rows.Count; i++)
            {
                if (GetGridValue_Host(SetHostGrid, i, ref str_value, 1))
                {
                    CGlobal.m_HostList[i].m_ParaValue = str_value;
                    CGlobal.m_Device.m_SelectParaList4G.Add(CGlobal.m_HostList[i]);
                }
            }

            if (CheckSelParaRight() == false)
            {
                MessageBox.Show("参数输入异常", "提示");
                return;
            }

            if (CGlobal.m_Device.m_SelectParaList4G.Count <= 0) return;
            int nGroupNo = 20;
            int nMaxCount = CGlobal.m_Device.m_SelectParaList4G.Count;
            ParaTaskMsg pNewTask = new ParaTaskMsg();
            pNewTask.m_TaskNo = 0x86;
            //pNewTask.m_AnswerFlag = false;
            pNewTask.m_StartPalce = 0;
            if (nMaxCount >= nGroupNo)
                pNewTask.m_ParaCount = nGroupNo;
            else
                pNewTask.m_ParaCount = nMaxCount;

            pNewTask.m_EndFlag = true;
            pNewTask.m_AnswerFlag = false;

            CGlobal.m_Device.m_Ptr.m_NowParaTask = pNewTask;        //当前任务相关的所有信息放到全局变量中

            CGlobal.SendTaskOrder(pNewTask.m_TaskNo);                //下发4G参数
        }

        private void Btn_Add_Host_Click(object sender, EventArgs e)
        {
            CAddHost pForm = new CAddHost("添加主站");


            if (pForm.ShowDialog() == DialogResult.OK)
            {

                if (pForm.TempMyHost != null)
                {
                    m_HostGroup.Add(pForm.TempMyHost);                            //全局数据更新

                    AddGridRowValue_Host(SetHostGrid, pForm.TempMyHost, SetHostGrid.RowCount);      //更新datagridview相关的数据库
                }

            }
        }

        private void Btn_Read_Host_Click(object sender, EventArgs e)
        {
            CGlobal.m_GHostList.Clear();            //读的时候需要清理一下
            CGlobal.SendTaskOrder(0x8d);
        }

        private void Btn_Modify_Host_Click(object sender, EventArgs e)
        {
            int sel_index;

            DataGridView pDataGrid = new DataGridView();
            pDataGrid = SetHostGrid;

            if (pDataGrid.SelectedCells.Count != 0)
            {
                sel_index = pDataGrid.CurrentRow.Index;

                CAddHost pForm = new CAddHost("修改主站");
                pForm.Tag = m_HostGroup[sel_index];

                if (pForm.ShowDialog() == DialogResult.OK)
                {
                    if (pForm.TempMyHost != null)
                    {
                        ModifyGridRowValue_Host(pDataGrid, pForm.TempMyHost, sel_index);         //更新datagridview相关的数据库
                        m_HostGroup[sel_index] = pForm.TempMyHost;                               //修改数据里对应index的值
                    }
                }
            }
            else
            {
                MessageBox.Show("未选择项。先选择一个项。", "提示", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);
            }
        }

        private void Btn_Del_Host_Click(object sender, EventArgs e)
        {
            DataGridView pDataGrid = new DataGridView();
            pDataGrid = SetHostGrid;

            DataTable pDataTable = (DataTable)pDataGrid.DataSource;
            if (pDataTable.Rows.Count == 0) return;
            int SelectRow = pDataGrid.CurrentRow.Index;
            if (MessageBox.Show("是否删除当前数据组？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                m_HostGroup.RemoveAt(SelectRow);
                DeleteGridRowValue(pDataGrid, SelectRow);
                return;
            }
        }

        private void Btn_Save_Host_Click(object sender, EventArgs e)
        {

            DataGridView pDataGrid = SetHostGrid;
            pDataGrid.EndEdit();
            List<CHostModule> pCfg = m_HostGroup;       //m_HostGroup本来就有,这里只是修改
            if (pDataGrid.RowCount <= 0) return;
            if (MessageBox.Show("是否保存参数？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                int i, col;


                System.Net.IPAddress ipaddress;

                CGlobal.m_GHostList.Clear();         //更新全局变量

                try
                {
                    for (i = 0; i < pDataGrid.RowCount; i++)
                    {
                        col = 0;

                        pCfg[i].m_HostIndex = Convert.ToByte(pDataGrid.Rows[i].Cells[col].Value);
                        col++;

                        pCfg[i].m_address = Convert.ToByte(pDataGrid.Rows[i].Cells[col].Value);
                        col++;

                        //if (pDataGrid.Rows[i].Cells[col].Value=="网口")
                        //     pCfg[i].m_ChanType = 1;
                        // else
                        //    pCfg[i].m_ChanType = 0;
                        //  col++;

                        //pCfg[i].m_ChanType = Convert.ToByte(pDataGrid.Rows[i].Cells[col].Value);
                        col++;

                        //pCfg[i].m_PrtclType = Convert.ToByte(pDataGrid.Rows[i].Cells[col].Value);
                        col++;


                        pCfg[i].LocalPort = Convert.ToInt32(pDataGrid.Rows[i].Cells[col].Value);
                        col++;

                        pCfg[i].RemotePort = Convert.ToInt32(pDataGrid.Rows[i].Cells[col].Value);
                        col++;

                        pCfg[i].m_LocalIPString1 = pDataGrid.Rows[i].Cells[col].Value.ToString();
                        ipaddress = System.Net.IPAddress.Parse(pCfg[i].m_LocalIPString1);
                        pCfg[i].m_LocalIP_byte1 = ipaddress.GetAddressBytes();
                        col++;

                        pCfg[i].m_LocalIPString2 = pDataGrid.Rows[i].Cells[col].Value.ToString();
                        ipaddress = System.Net.IPAddress.Parse(pCfg[i].m_LocalIPString2);
                        pCfg[i].m_LocalIP_byte2 = ipaddress.GetAddressBytes();
                        col++;

                        pCfg[i].m_LocalIPString3 = pDataGrid.Rows[i].Cells[col].Value.ToString();
                        ipaddress = System.Net.IPAddress.Parse(pCfg[i].m_LocalIPString3);
                        pCfg[i].m_LocalIP_byte3 = ipaddress.GetAddressBytes();
                        col++;

                        pCfg[i].m_RemoteIPString1 = pDataGrid.Rows[i].Cells[col].Value.ToString();
                        ipaddress = System.Net.IPAddress.Parse(pCfg[i].m_RemoteIPString1);
                        pCfg[i].m_RemoteIP_byte1 = ipaddress.GetAddressBytes();
                        col++;

                        pCfg[i].m_RemoteIPString2 = pDataGrid.Rows[i].Cells[col].Value.ToString();
                        ipaddress = System.Net.IPAddress.Parse(pCfg[i].m_RemoteIPString2);
                        pCfg[i].m_RemoteIP_byte2 = ipaddress.GetAddressBytes();
                        col++;

                        pCfg[i].m_RemoteIPString3 = pDataGrid.Rows[i].Cells[col].Value.ToString();
                        ipaddress = System.Net.IPAddress.Parse(pCfg[i].m_RemoteIPString3);
                        pCfg[i].m_RemoteIP_byte3 = ipaddress.GetAddressBytes();


                    }
                }
                catch
                {
                    MessageBox.Show("参数输入异常", "提示");
                    return;
                }


                foreach (CHostModule OneHost in m_HostGroup)
                    CGlobal.m_GHostList.Add(OneHost);

                GetParaOperationList(Convert.ToInt32(TI_Type.W_PI_PA_1), 3, false);
            }
        }

        private void Btn_Clear_Host_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否清空所有数据？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                DataTable pDataTable = (DataTable)SetHostGrid.DataSource;
                pDataTable.Clear();

                m_HostGroup.Clear();
            }
        }

        private void GetParaOperationList(int nTaskNo, int nGroupNo, bool bNoWait = false)      //添加了几个任务
        {
            int nMaxCount = 0;
            //m_TaskList.Clear();

            switch (nTaskNo)
            {
                case (int)TI_Type.W_MC_PA_1:
                    nMaxCount = CGlobal.m_MacGroup.Count;          //一共有几组参数
                    break;

                case (int)TI_Type.W_PI_PA_1:
                    nMaxCount = CGlobal.m_GHostList.Count;          //一共有几组参数
                    break;

                case (int)TI_Type.W_TI_PA_1:
                    nMaxCount = CGlobal.m_GDevList.Count;          //一共有几组参数
                    break;

                case (int)TI_Type.W_SR_PA_1:
                    nMaxCount = CGlobal.m_GComList.Count;          //一共有几组参数
                    break;

                case (int)TI_Type.W_GW_PA_1:
                    nMaxCount = CGlobal.m_GateWayGroup.Count;          //一共有几组参数
                    break;

                case (int)TI_Type.W_NT_PA_1:
                    nMaxCount = CGlobal.m_IPGroup.Count;
                    break;

                case (int)TI_Type.R_4G_PA_1:
                    nMaxCount = CGlobal.m_Device.m_SelectParaList4G.Count;
                    break;
            }

            if (nMaxCount == 0) return;

            int addCount = nMaxCount;

            TaskTime.Stop();            //这时候只要加就行了,m_TaskList不会被移除
            int nCroup = 0;
            while (nMaxCount > 0)           //把若干个参数按照 单元个数 分割，并放入m_TaskList
            {
                ParaTaskMsg pNewTask = new ParaTaskMsg();
                pNewTask.m_TaskNo = nTaskNo;

                pNewTask.m_StartPalce = nCroup * nGroupNo;
                if (nMaxCount >= nGroupNo)
                    pNewTask.m_ParaCount = nGroupNo;
                else
                    pNewTask.m_ParaCount = nMaxCount;

                if (nMaxCount - pNewTask.m_ParaCount > 0)
                {
                    pNewTask.m_AnswerFlag = bNoWait;
                    pNewTask.m_EndFlag = false;
                }
                else   //如果是最后一帧则需要回复,并且标志为最后一帧
                {
                    pNewTask.m_AnswerFlag = false;
                    pNewTask.m_EndFlag = true;
                }

                m_TaskList.Add(pNewTask);
                nCroup++;
                nMaxCount = nMaxCount - pNewTask.m_ParaCount;
            }
         
            pNowTask = null;    //启动  
            TaskTime.Start();
        }

        private void TaskTime_Tick(object sender, EventArgs e)          //就几个任务分别传送
        {
            if (pNowTask != null)
            {
                if (pNowTask.m_AnswerFlag == true)
                    pNowTask = null;
            }
            else
            {
                if (m_TaskList.Count > 0)
                {
                    pNowTask = m_TaskList[0];
                    CGlobal.m_Device.m_Ptr.m_NowParaTask = pNowTask;
                    CGlobal.SendTaskOrder(pNowTask.m_TaskNo);
                    m_TaskList.RemoveAt(0);
                }
                else
                    TaskTime.Stop();
            }
        }

        private void Btn_Read_Dev_Click(object sender, EventArgs e)
        {
            CGlobal.m_GDevList.Clear();            //读的时候需要清理一下
            CGlobal.SendTaskOrder((int)TI_Type.R_TI_PA_1);
        }

        private void Btn_Del_Dev_Click(object sender, EventArgs e)
        {
            DataGridView pDataGrid = new DataGridView();
            pDataGrid = SetDevGrid;

            DataTable pDataTable = (DataTable)pDataGrid.DataSource;
            if (pDataTable.Rows.Count == 0) return;
            int SelectRow = pDataGrid.CurrentRow.Index;
            if (MessageBox.Show("是否删除当前数据组？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                m_DevGroup.RemoveAt(SelectRow);
                DeleteGridRowValue(pDataGrid, SelectRow);
                return;
            }
        }

        private void Btn_Clear_Dev_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否清空所有数据？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                DataTable pDataTable = (DataTable)SetDevGrid.DataSource;
                pDataTable.Clear();

                m_DevGroup.Clear();
            }
        }

        private void Btn_Save_Dev_Click(object sender, EventArgs e)
        {
            DataGridView pDataGrid = SetDevGrid;
            pDataGrid.EndEdit();
            List<CHostModule> pCfg = m_DevGroup;
            if (pDataGrid.RowCount <= 0) return;

            if (MessageBox.Show("是否保存参数？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    int i, col;

                    System.Net.IPAddress ipaddress;


                    CGlobal.m_GDevList.Clear();         //更新全局变量
                    for (i = 0; i < pDataGrid.RowCount; i++)
                    {
                        col = 0;
                        m_DevGroup[i].m_HostIndex = Convert.ToByte(pDataGrid.Rows[i].Cells[col].Value);
                        col++;

                        m_DevGroup[i].m_address = Convert.ToByte(pDataGrid.Rows[i].Cells[col].Value);
                        col++;

                        //m_DevGroup[i].m_ChanType = Convert.ToByte(pDataGrid.Rows[i].Cells[col].Value);
                        col++;

                        //m_DevGroup[i].m_PrtclType = Convert.ToByte(pDataGrid.Rows[i].Cells[col].Value);
                        col++;

                        m_DevGroup[i].LocalPort = Convert.ToInt32(pDataGrid.Rows[i].Cells[col].Value);
                        col++;

                        m_DevGroup[i].RemotePort = Convert.ToInt32(pDataGrid.Rows[i].Cells[col].Value);
                        col++;

                        m_DevGroup[i].m_LocalIPString1 = pDataGrid.Rows[i].Cells[col].Value.ToString();
                        ipaddress = System.Net.IPAddress.Parse(m_DevGroup[i].m_LocalIPString1);
                        m_DevGroup[i].m_LocalIP_byte1 = ipaddress.GetAddressBytes();
                        col++;

                        m_DevGroup[i].m_LocalIPString2 = pDataGrid.Rows[i].Cells[col].Value.ToString();
                        ipaddress = System.Net.IPAddress.Parse(m_DevGroup[i].m_LocalIPString2);
                        m_DevGroup[i].m_LocalIP_byte2 = ipaddress.GetAddressBytes();
                        col++;

                        m_DevGroup[i].m_LocalIPString3 = pDataGrid.Rows[i].Cells[col].Value.ToString();
                        ipaddress = System.Net.IPAddress.Parse(m_DevGroup[i].m_LocalIPString3);
                        m_DevGroup[i].m_LocalIP_byte3 = ipaddress.GetAddressBytes();
                        col++;

                        m_DevGroup[i].m_RemoteIPString1 = pDataGrid.Rows[i].Cells[col].Value.ToString();
                        ipaddress = System.Net.IPAddress.Parse(m_DevGroup[i].m_RemoteIPString1);
                        m_DevGroup[i].m_RemoteIP_byte1 = ipaddress.GetAddressBytes();
                        col++;

                        m_DevGroup[i].m_RemoteIPString2 = pDataGrid.Rows[i].Cells[col].Value.ToString();
                        ipaddress = System.Net.IPAddress.Parse(m_DevGroup[i].m_RemoteIPString2);
                        m_DevGroup[i].m_RemoteIP_byte2 = ipaddress.GetAddressBytes();
                        col++;

                        m_DevGroup[i].m_RemoteIPString3 = pDataGrid.Rows[i].Cells[col].Value.ToString();
                        ipaddress = System.Net.IPAddress.Parse(m_DevGroup[i].m_RemoteIPString3);
                        m_DevGroup[i].m_RemoteIP_byte3 = ipaddress.GetAddressBytes();
                        col++;
                    }
                }
                catch
                {
                    MessageBox.Show("参数输入异常", "提示");
                    return;
                }


                foreach (CHostModule OneDev in m_DevGroup)
                    CGlobal.m_GDevList.Add(OneDev);

                GetParaOperationList(Convert.ToInt32(TI_Type.W_TI_PA_1), 3, false);
            }
        }

        private void CommuCFG_FormClosing(object sender, FormClosingEventArgs e)
        {
           

        }

      

        private void Btn_MacRead_Click(object sender, EventArgs e)
        {
            CGlobal.m_MacGroup.Clear();
            CGlobal.SendTaskOrder((int)TI_Type.R_MC_PA_1);
        }

        private void Btn_MacSave_Click(object sender, EventArgs e)
        {

            DataGridView pDataGrid = SetMacGrid;
            pDataGrid.EndEdit();
            if (pDataGrid.RowCount <= 0) return;

            if (MessageBox.Show("是否保存参数？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    int i, j;

                    string[] str_mac;

                    CGlobal.m_MacGroup.Clear();         //更新全局变量

                    for (i = 0; i < pDataGrid.RowCount; i++)
                    {
                        CMacModule mac = new CMacModule();
                        mac.m_MacString = pDataGrid.Rows[i].Cells[1].Value.ToString();
                        if (mac.m_MacString == "" || mac.m_MacString == null) continue;

                        str_mac = mac.m_MacString.Split(':');

                        if (str_mac.Length != 6)
                        {
                            MessageBox.Show("参数输入异常", "提示");
                            return;
                        }
                        for (j = 0; j < 6; j++)
                            mac.m_Mac_byte[j] = Convert.ToByte(str_mac[j], 16);
                        mac.Port = i;

                        CGlobal.m_MacGroup.Add(mac);
                    }

                    if (CGlobal.m_MacGroup.Count == 0)
                    {
                        MessageBox.Show("参数输入异常", "提示");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("参数输入异常", "提示");
                    return;
                }

                GetParaOperationList(Convert.ToInt32(TI_Type.W_MC_PA_1), 20, true);
            }
        }

        private void MacClearBtn_Click(object sender, EventArgs e)
        {
            DataGridView pDataGrid = SetMacGrid;
            DataTable pDataTable = (DataTable)pDataGrid.DataSource;

            if (MessageBox.Show("是否清空所有数据？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                CGlobal.m_MacGroup.Clear();
                pDataTable.Rows[0][1] = "";
                pDataTable.Rows[1][1] = "";
              
               
            }
        }

        static public void UpdateComBox(Control pComBox, DataGridView pDataGrid)
        {
            if (pComBox != null)
            {
                pComBox.Visible = false;
                pDataGrid.Controls.Remove(pComBox);
                pComBox = null;
            }
        }

        private void UpdateComBoxPalce(ComboBox pUseComBox, DataGridView pDataGrid)
        {
            int nColIndex = pDataGrid.CurrentCell.ColumnIndex;
            int nRowIndex = pDataGrid.CurrentCell.RowIndex;
            UpdateComBox(m_NowCombox, pDataGrid);
            if (nColIndex != 2 && nColIndex != 3) return;

            Rectangle rect = pDataGrid.GetCellDisplayRectangle(nColIndex, nRowIndex, false);
            m_NowCombox = pUseComBox;

            m_NowCombox.Left = rect.Left;
            m_NowCombox.Top = rect.Top;
            m_NowCombox.Width = rect.Width;
            m_NowCombox.Height = rect.Height;

            pDataGrid.Controls.Add(m_NowCombox);
            m_NowCombox.Visible = true;
            m_NowCombox.Tag = nColIndex;

            m_NowDataGrid = pDataGrid;
            m_RowIndex = nRowIndex;
            m_ColIndex = nColIndex;

            string consultingRoom = pDataGrid.Rows[nRowIndex].Cells[nColIndex].Value.ToString();
            int index = m_NowCombox.Items.IndexOf(consultingRoom);
            if (index < 0) index = 0;
            m_NowCombox.SelectedIndex = index;
        }
        /*
         * 获取选择单元格模式的选择项
         */
        /// <summary>
        /// 需要获取当前所要设置的对象
        /// </summary>
        /// <returns></returns>

        private CHostModule GetNowSetDevModule(object sender, int RowIndex)
        {
            CHostModule m_module = new CHostModule();
            if (RowIndex <= -1)
                m_module = null;
            if (sender == SetDevGrid)
                m_module = m_DevGroup[RowIndex];
            else if (sender == SetHostGrid)
                m_module = m_HostGroup[RowIndex];
            return m_module;
        }

        private void SetDevGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            DataGridView pDataGrid = (DataGridView)sender;
            if (pDataGrid.CurrentCell == null) return;

            int nRowIndex = pDataGrid.CurrentCell.RowIndex;
            m_NowSetModule = GetNowSetDevModule(sender, nRowIndex);

            int nColIndex = pDataGrid.CurrentCell.ColumnIndex;
            UpdateComBox(m_NowCombox, pDataGrid);
            if (nColIndex == 2) UpdateComBoxPalce(m_ChanelComBox, (DataGridView)sender);
            if (nColIndex == 3) UpdateComBoxPalce(m_PtrComBox, (DataGridView)sender);
        }

        private void SetDevGrid_Scroll(object sender, ScrollEventArgs e)
        {
            DataGridView pDataGrid = (DataGridView)sender;
            if (pDataGrid.CurrentCell == null) return;

            int nRowIndex = pDataGrid.CurrentCell.RowIndex;
            m_NowSetModule = GetNowSetDevModule(sender, nRowIndex);

            int nColIndex = pDataGrid.CurrentCell.ColumnIndex;
            UpdateComBox(m_NowCombox, pDataGrid);
            if (nColIndex == 2) UpdateComBoxPalce(m_ChanelComBox, (DataGridView)sender);
            if (nColIndex == 3) UpdateComBoxPalce(m_PtrComBox, (DataGridView)sender);
        }

        private void SetHostGrid_CurrentCellChanged(object sender, EventArgs e)
        {
            DataGridView pDataGrid = (DataGridView)sender;
            if (pDataGrid.CurrentCell == null) return;

            int nRowIndex = pDataGrid.CurrentCell.RowIndex;
            m_NowSetModule = GetNowSetDevModule(sender, nRowIndex);

            int nColIndex = pDataGrid.CurrentCell.ColumnIndex;
            UpdateComBox(m_NowCombox, pDataGrid);
            if (nColIndex == 2) UpdateComBoxPalce(m_ChanelComBox, (DataGridView)sender);
            if (nColIndex == 3) UpdateComBoxPalce(m_PtrComBox, (DataGridView)sender);
        }

        private void SetHostGrid_Scroll(object sender, ScrollEventArgs e)
        {
            DataGridView pDataGrid = (DataGridView)sender;
            if (pDataGrid.CurrentCell == null) return;

            int nRowIndex = pDataGrid.CurrentCell.RowIndex;
            m_NowSetModule = GetNowSetDevModule(sender, nRowIndex);

            int nColIndex = pDataGrid.CurrentCell.ColumnIndex;
            UpdateComBox(m_NowCombox, pDataGrid);
            if (nColIndex == 2) UpdateComBoxPalce(m_ChanelComBox, (DataGridView)sender);
            if (nColIndex == 3) UpdateComBoxPalce(m_PtrComBox, (DataGridView)sender);
        }
    }
}
