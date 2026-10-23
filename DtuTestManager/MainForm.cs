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
    public partial class MainForm : Form
    {
        private ToolTip TooltipMsg = new ToolTip();
        private bool m_bDeRun = false;
        private bool m_TableShow = false;

        public MainForm()
        {
            InitializeComponent();
        }
        #region    //主窗口消息函数
        private void MainForm_Load(object sender, EventArgs e)
        {

            string BackPic = System.Environment.CurrentDirectory + "\\" + "res" + "\\MainPic.png";
            MainPic.BackgroundImage = Image.FromFile(BackPic);
            MainPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

            string LogPic = System.Environment.CurrentDirectory + "\\" + "res" + "\\Log.png";
            TopLogPic.BackgroundImage = Image.FromFile(LogPic);
            TopLogPic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            IniToolTipMsg();

            IniSetPanelArea_1();
            IniSetPanelArea_2();
            IniSetPanelArea_3();
            IniSetPanelArea_4();

            ResetCtrlPlaceAndSize();

            this.tabFormCtrl.Parent = MainPic;
            this.tabFormCtrl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

            if (CGlobal.m_DataBase.ConnectDataBase() == false)
            {
                MessageBox.Show("连接数据库失败!");
            }
            else
            {
                IniReadSqlData();
            }

        }
        private void MainForm_Resize(object sender, EventArgs e)
        {
            ResetCtrlPlaceAndSize();
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CGlobal.m_ClientSocket.CloseThread();
        }
        #endregion
        #region    //初始化读取数据库
        private void IniReadSqlData()
        {

        }
        #endregion
        #region  //控件属性设置函数
        private void Button_Set_Option(object TheButton, string PicPath, object ParentPanel)
        {
            DtuTestManager.MyPanel pParent = (DtuTestManager.MyPanel)ParentPanel;
            Button pButton = (Button)TheButton;
            pButton.Parent = pParent;

            pButton.FlatAppearance.BorderSize = 0;//去掉边框  
            pButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            pButton.FlatAppearance.MouseOverBackColor = Color.DarkSeaGreen;
            pButton.FlatAppearance.MouseDownBackColor = Color.DarkGreen;
            pButton.BackgroundImage = Image.FromFile(PicPath);
            pButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            pButton.BackColor = Color.Transparent;
        }
        private void Button_Set_Place(object TheButton, int nLeft, int nTop)
        {
            Button pButton = (Button)TheButton;
            pButton.Left = nLeft;
            pButton.Top = nTop;
        }
        private void Button_Set_Size(object TheButton, int nWith, int nHeight)
        {
            Button pButton = (Button)TheButton;
            pButton.Width = nWith;
            pButton.Height = nHeight;
        }
        private void Panel_Set_Option(object ThePanel, string strPath, object ParentPanel)
        {
            DtuTestManager.MyPanel pPanel = (DtuTestManager.MyPanel)ThePanel;
            DtuTestManager.MyPanel pParent = (DtuTestManager.MyPanel)ParentPanel;

            pPanel.Parent = pParent;
            pPanel.BackgroundImage = Image.FromFile(strPath);
            pPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            pPanel.BackColor = Color.Transparent;
        }
        private void Panel_Set_Place(object ThePanel, int nLeft, int nTop)
        {
            DtuTestManager.MyPanel pPanel = (DtuTestManager.MyPanel)ThePanel;
            pPanel.Left = nLeft;
            pPanel.Top = nTop;
        }
        private void Panel_Set_Size(object ThePanel, int nWith, int nHeight)
        {
            DtuTestManager.MyPanel pPanel = (DtuTestManager.MyPanel)ThePanel;
            pPanel.Width = nWith;
            pPanel.Height = nHeight;
        }
        private void Label_Set_Option(object TheLabel, object ParentPanel)
        {
            DtuTestManager.MyPanel pParent = (DtuTestManager.MyPanel)ParentPanel;
            Label pLabel = (Label)TheLabel;
            pLabel.Parent = pParent;
            pLabel.BackColor = Color.Transparent;
        }
        private void Label_Set_Place(object TheLabel, int nLeft, int nTop)
        {
            Label pLabel = (Label)TheLabel;
            pLabel.Left = nLeft;
            pLabel.Top = nTop;
        }
        private void Label_Font_Reset(object TheLabel, int nLabelNewWith, int nLabelNewHeight)
        {
            Control pLabel = (Control)TheLabel;

            System.Drawing.FontFamily pFont = new System.Drawing.FontFamily(pLabel.Font.Name);
            FontStyle FontStyle = pLabel.Font.Style;
            pLabel.AutoSize = true;
            float FontSize = pLabel.Font.Size;
            if (pLabel.Width >= nLabelNewWith || pLabel.Height >= nLabelNewHeight)
            {
                while ((pLabel.Width > nLabelNewWith || pLabel.Height > nLabelNewHeight) && FontSize > 1)
                {
                    FontSize -= 1;
                    pLabel.Font = new Font(pFont, FontSize, FontStyle, GraphicsUnit.World);
                }
            }
            else if (pLabel.Width <= nLabelNewWith && pLabel.Height <= nLabelNewHeight)
            {
                while (pLabel.Width < nLabelNewWith && pLabel.Height < nLabelNewHeight)
                {
                    FontSize += 1;
                    pLabel.Font = new Font(pFont, FontSize, FontStyle, GraphicsUnit.World);
                }
            }
        }
        #endregion
        #region    //重置控件位置大小
        private void ResetPanelArea_1_PlaceAndSize()
        {
            int PanelAreaWith = (int)(MainPic.Width * 0.264);
            int PanelAreaHeight = (int)(MainPic.Height * 0.334);

            Panel_Set_Place(PanelArea1, 0, 0);
            Panel_Set_Size(PanelArea1, PanelAreaWith, PanelAreaHeight);

            int ChildPanelWith = (int)(PanelArea1.Width * 0.133);
            int ChildPanelHeight = (int)(PanelArea1.Height * 0.238);

            Panel_Set_Size(panel25, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel26, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel27, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel28, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel29, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel30, ChildPanelWith, ChildPanelHeight);


            int ChildLabelWith = (int)(PanelArea1.Width * 0.270);
            int ChildLabelHeight = (int)(PanelArea1.Height * 0.08);

            Label_Font_Reset(label1, ChildLabelWith, ChildLabelHeight);
            Label_Font_Reset(label2, ChildLabelWith, ChildLabelHeight);
            Label_Font_Reset(label3, ChildLabelWith, ChildLabelHeight);
            Label_Font_Reset(label4, ChildLabelWith, ChildLabelHeight);
            Label_Font_Reset(label5, ChildLabelWith, ChildLabelHeight);
            Label_Font_Reset(label6, ChildLabelWith, ChildLabelHeight);

            int i;
            int[][] PanelXYPlace = new int[6][];
            int[][] LabelXYPlace = new int[6][];

            for (i = 0; i < 6; i++) PanelXYPlace[i] = new int[2];
            for (i = 0; i < 6; i++) LabelXYPlace[i] = new int[2];

            PanelXYPlace[0][0] = PanelArea1.Width / 6;
            PanelXYPlace[1][0] = PanelArea1.Width / 2;
            PanelXYPlace[2][0] = PanelArea1.Width / 6 * 5;
            PanelXYPlace[3][0] = PanelXYPlace[0][0];
            PanelXYPlace[4][0] = PanelXYPlace[1][0];
            PanelXYPlace[5][0] = PanelXYPlace[2][0];

            PanelXYPlace[0][1] = (int)(PanelArea1.Height * 0.23);
            PanelXYPlace[1][1] = PanelXYPlace[0][1];
            PanelXYPlace[2][1] = PanelXYPlace[0][1];
            PanelXYPlace[3][1] = (int)(PanelArea1.Height * 0.67);
            PanelXYPlace[4][1] = PanelXYPlace[3][1];
            PanelXYPlace[5][1] = PanelXYPlace[3][1];

            for (i = 0; i < 6; i++)
            {
                LabelXYPlace[i][0] = PanelXYPlace[i][0] - label1.Width / 2;
                LabelXYPlace[i][1] = PanelXYPlace[i][1] + panel25.Height / 2 + 3;
            }

            for (i = 0; i < 6; i++)
            {
                PanelXYPlace[i][0] -= panel25.Width / 2;
                PanelXYPlace[i][1] -= panel25.Height / 2;
            }

            Panel_Set_Place(panel25, PanelXYPlace[0][0], PanelXYPlace[0][1]);
            Panel_Set_Place(panel26, PanelXYPlace[1][0], PanelXYPlace[1][1]);
            Panel_Set_Place(panel27, PanelXYPlace[2][0], PanelXYPlace[2][1]);
            Panel_Set_Place(panel28, PanelXYPlace[3][0], PanelXYPlace[3][1]);
            Panel_Set_Place(panel29, PanelXYPlace[4][0], PanelXYPlace[4][1]);
            Panel_Set_Place(panel30, PanelXYPlace[5][0], PanelXYPlace[5][1]);


            Label_Set_Place(label1, LabelXYPlace[0][0], LabelXYPlace[0][1]);
            Label_Set_Place(label2, LabelXYPlace[1][0], LabelXYPlace[1][1]);
            Label_Set_Place(label3, LabelXYPlace[2][0], LabelXYPlace[2][1]);
            Label_Set_Place(label4, LabelXYPlace[3][0], LabelXYPlace[3][1]);
            Label_Set_Place(label5, LabelXYPlace[4][0], LabelXYPlace[4][1]);
            Label_Set_Place(label6, LabelXYPlace[5][0], LabelXYPlace[5][1]);
        }
        private void ResetPanelArea_2_PlaceAndSize()
        {
            int Offset = (int)(PanelArea1.Right * 0.1);
            int PanelAreaWith = (int)(MainPic.Width * 0.325);
            int PanelAreaHeight = (int)(MainPic.Height * 0.467);

            Panel_Set_Place(PanelArea2, PanelArea1.Right + Offset, 0);
            Panel_Set_Size(PanelArea2, PanelAreaWith, PanelAreaHeight);

            int ChildPanelWith = (int)(PanelArea2.Width * 0.113);
            int ChildPanelHeight = (int)(PanelArea2.Height * 0.170);

            Panel_Set_Size(panel31, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel32, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel33, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel34, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel35, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel36, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel37, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel38, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel39, ChildPanelWith, ChildPanelHeight);


            int ChildLabelWith = (int)(PanelArea2.Width * 0.3260);
            int ChildLabelHeight = (int)(PanelArea2.Height * 0.0424);

            Label_Font_Reset(label7, ChildLabelWith, ChildLabelHeight);
            Label_Font_Reset(label8, ChildLabelWith, ChildLabelHeight);
            Label_Font_Reset(label9, ChildLabelWith, ChildLabelHeight);
            Label_Font_Reset(label10, ChildLabelWith, ChildLabelHeight);
            Label_Font_Reset(label11, ChildLabelWith, ChildLabelHeight);
            Label_Font_Reset(label12, ChildLabelWith, ChildLabelHeight);
            Label_Font_Reset(label13, ChildLabelWith, ChildLabelHeight);
            Label_Font_Reset(label14, ChildLabelWith, ChildLabelHeight);
            Label_Font_Reset(label15, ChildLabelWith, ChildLabelHeight);


            int i;
            int[][] PanelXYPlace = new int[9][];
            int[][] LabelXYPlace = new int[9][];

            for (i = 0; i < 9; i++) PanelXYPlace[i] = new int[2];
            for (i = 0; i < 9; i++) LabelXYPlace[i] = new int[2];

            PanelXYPlace[0][0] = PanelArea2.Width / 6;
            PanelXYPlace[1][0] = PanelArea2.Width / 2;
            PanelXYPlace[2][0] = PanelArea2.Width / 6 * 5;
            PanelXYPlace[3][0] = PanelXYPlace[0][0];
            PanelXYPlace[4][0] = PanelXYPlace[1][0];
            PanelXYPlace[5][0] = PanelXYPlace[2][0];
            PanelXYPlace[6][0] = PanelXYPlace[0][0];
            PanelXYPlace[7][0] = PanelXYPlace[1][0];
            PanelXYPlace[8][0] = PanelXYPlace[2][0];


            PanelXYPlace[0][1] = (int)(PanelArea2.Height * 0.1);
            PanelXYPlace[1][1] = PanelXYPlace[0][1];
            PanelXYPlace[2][1] = PanelXYPlace[0][1];
            PanelXYPlace[3][1] = (int)(PanelArea2.Height * 0.44);
            PanelXYPlace[4][1] = PanelXYPlace[3][1];
            PanelXYPlace[5][1] = PanelXYPlace[3][1];
            PanelXYPlace[6][1] = (int)(PanelArea2.Height * 0.78);
            PanelXYPlace[7][1] = PanelXYPlace[6][1];
            PanelXYPlace[8][1] = PanelXYPlace[6][1];

            for (i = 0; i < 9; i++)
            {
                LabelXYPlace[i][0] = PanelXYPlace[i][0] - label7.Width / 2;
                LabelXYPlace[i][1] = PanelXYPlace[i][1] + panel25.Height / 2 + 5;
            }

            for (i = 0; i < 9; i++)
            {
                PanelXYPlace[i][0] -= panel25.Width / 2;
                PanelXYPlace[i][1] -= panel25.Height / 2;
            }

            Panel_Set_Place(panel31, PanelXYPlace[0][0], PanelXYPlace[0][1]);
            Panel_Set_Place(panel32, PanelXYPlace[1][0], PanelXYPlace[1][1]);
            Panel_Set_Place(panel33, PanelXYPlace[2][0], PanelXYPlace[2][1]);
            Panel_Set_Place(panel34, PanelXYPlace[3][0], PanelXYPlace[3][1]);
            Panel_Set_Place(panel35, PanelXYPlace[4][0], PanelXYPlace[4][1]);
            Panel_Set_Place(panel36, PanelXYPlace[5][0], PanelXYPlace[5][1]);
            Panel_Set_Place(panel37, PanelXYPlace[6][0], PanelXYPlace[6][1]);
            Panel_Set_Place(panel38, PanelXYPlace[7][0], PanelXYPlace[7][1]);
            Panel_Set_Place(panel39, PanelXYPlace[8][0], PanelXYPlace[8][1]);


            Label_Set_Place(label7, LabelXYPlace[0][0], LabelXYPlace[0][1]);
            Label_Set_Place(label8, LabelXYPlace[1][0], LabelXYPlace[1][1]);
            Label_Set_Place(label9, LabelXYPlace[2][0], LabelXYPlace[2][1]);
            Label_Set_Place(label10, LabelXYPlace[3][0], LabelXYPlace[3][1]);
            Label_Set_Place(label11, LabelXYPlace[4][0], LabelXYPlace[4][1]);
            Label_Set_Place(label12, LabelXYPlace[5][0], LabelXYPlace[5][1]);
            Label_Set_Place(label13, LabelXYPlace[6][0], LabelXYPlace[6][1]);
            Label_Set_Place(label14, LabelXYPlace[7][0], LabelXYPlace[7][1]);
            Label_Set_Place(label15, LabelXYPlace[8][0], LabelXYPlace[8][1]);

        }
        private void ResetPanelArea_3_PlaceAndSize()
        {
            int nOffset = (int)((MainPic.Width - PanelArea1.Width - PanelArea2.Width) * 0.1);
            int PanelAreaWith = (int)((MainPic.Width - PanelArea1.Width - PanelArea2.Width) * 0.75);
            int PanelAreaHeight = (int)(MainPic.Height * 0.6);

            Panel_Set_Place(PanelArea3, MainPic.Right - PanelAreaWith - nOffset, 0);
            Panel_Set_Size(PanelArea3, PanelAreaWith, PanelAreaHeight);

            int ChildButtonWith = (int)(PanelArea3.Width * 0.17);
            int ChildButtonHeight = (int)(PanelArea3.Height * 0.25);

            Button_Set_Size(MasterConParaBtn, ChildButtonWith, ChildButtonHeight);
            Button_Set_Size(DeviceSetBtn, ChildButtonWith, ChildButtonHeight);
            Button_Set_Size(RealCtrlBtn, ChildButtonWith, ChildButtonHeight);
            Button_Set_Size(HisRecordBtn, ChildButtonWith, ChildButtonHeight);
            Button_Set_Size(LbBtn, ChildButtonWith, ChildButtonHeight);
            Button_Set_Size(TestFunBtn, ChildButtonWith, ChildButtonHeight);
            Button_Set_Size(DebRunBtn, ChildButtonWith, ChildButtonHeight);
            Button_Set_Size(AutoRunBtn, ChildButtonWith, ChildButtonHeight);
            Button_Set_Size(HostStaBtn, ChildButtonWith, ChildButtonHeight);

            int i;
            int[][] PanelXYPlace = new int[9][];

            for (i = 0; i < 9; i++) PanelXYPlace[i] = new int[2];

            PanelXYPlace[0][0] = PanelArea3.Width / 6;
            PanelXYPlace[1][0] = PanelArea3.Width / 2;
            PanelXYPlace[2][0] = PanelArea3.Width / 6 * 5;
            PanelXYPlace[3][0] = PanelXYPlace[0][0];
            PanelXYPlace[4][0] = PanelXYPlace[1][0];
            PanelXYPlace[5][0] = PanelXYPlace[2][0];
            PanelXYPlace[6][0] = PanelXYPlace[0][0];
            PanelXYPlace[7][0] = PanelXYPlace[1][0];
            PanelXYPlace[8][0] = PanelXYPlace[2][0];

            PanelXYPlace[0][1] = (int)(PanelArea3.Height * 0.15);
            PanelXYPlace[1][1] = PanelXYPlace[0][1];
            PanelXYPlace[2][1] = PanelXYPlace[0][1];
            PanelXYPlace[3][1] = (int)(PanelArea3.Height * 0.47);
            PanelXYPlace[4][1] = PanelXYPlace[3][1];
            PanelXYPlace[5][1] = PanelXYPlace[3][1];
            PanelXYPlace[6][1] = (int)(PanelArea3.Height * 0.79);
            PanelXYPlace[7][1] = PanelXYPlace[6][1];
            PanelXYPlace[8][1] = PanelXYPlace[6][1];

            for (i = 0; i < 9; i++)
            {
                PanelXYPlace[i][0] -= MasterConParaBtn.Width / 2;
                PanelXYPlace[i][1] -= MasterConParaBtn.Height / 2;
            }
            Button_Set_Place(MasterConParaBtn, PanelXYPlace[0][0], PanelXYPlace[0][1]);
            Button_Set_Place(DeviceSetBtn, PanelXYPlace[1][0], PanelXYPlace[1][1]);
            Button_Set_Place(RealCtrlBtn, PanelXYPlace[2][0], PanelXYPlace[2][1]);
            Button_Set_Place(HisRecordBtn, PanelXYPlace[3][0], PanelXYPlace[3][1]);
            Button_Set_Place(LbBtn, PanelXYPlace[4][0], PanelXYPlace[4][1]);
            Button_Set_Place(TestFunBtn, PanelXYPlace[5][0], PanelXYPlace[5][1]);
            Button_Set_Place(DebRunBtn, PanelXYPlace[6][0], PanelXYPlace[6][1]);
            Button_Set_Place(AutoRunBtn, PanelXYPlace[7][0], PanelXYPlace[7][1]);
            Button_Set_Place(HostStaBtn, PanelXYPlace[8][0], PanelXYPlace[8][1]);
        }
        private void ResetPanelArea_4_PlaceAndSize()
        {
            int Offset = 10;
            int PanelAreaWith = (int)(MainPic.Width * 0.4);
            int PanelAreaHeight = (int)(MainPic.Height * 0.3);

            Panel_Set_Size(PanelArea4, PanelAreaWith, PanelAreaHeight);
            Panel_Set_Place(PanelArea4, MainPic.Width - PanelAreaWith - Offset, PanelArea3.Bottom);

            int ChildPanelWith = (int)(PanelArea4.Width * 0.08);
            int ChildPanelHeight = (int)(PanelArea4.Height * 0.5);

            Panel_Set_Size(panel1, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel2, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel3, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel4, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel5, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel6, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel7, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel8, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel9, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel10, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel11, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel12, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel13, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel14, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel15, ChildPanelWith, ChildPanelHeight);
            Panel_Set_Size(panel16, ChildPanelWith, ChildPanelHeight);

            int i;
            int[][] PanelXYPlace = new int[16][];

            for (i = 0; i < 16; i++) PanelXYPlace[i] = new int[2];

            for (i = 0; i < 8; i++)
            {
                PanelXYPlace[i][0] = PanelArea4.Width / 10 * (i + 1) - panel1.Width / 2;
                PanelXYPlace[i + 8][0] = PanelArea4.Width / 10 * (i + 1) - panel1.Width / 2;
            }
            for (i = 0; i < 8; i++)
            {
                PanelXYPlace[i][1] = PanelArea4.Height / 4 - panel1.Height / 2;
                PanelXYPlace[i + 8][1] = PanelArea4.Height / 4 * 3 - panel1.Height / 2;
            }


            Panel_Set_Place(panel1, PanelXYPlace[0][0], PanelXYPlace[0][1]);
            Panel_Set_Place(panel2, PanelXYPlace[1][0], PanelXYPlace[1][1]);
            Panel_Set_Place(panel3, PanelXYPlace[2][0], PanelXYPlace[2][1]);
            Panel_Set_Place(panel4, PanelXYPlace[3][0], PanelXYPlace[3][1]);
            Panel_Set_Place(panel5, PanelXYPlace[4][0], PanelXYPlace[4][1]);
            Panel_Set_Place(panel6, PanelXYPlace[5][0], PanelXYPlace[5][1]);
            Panel_Set_Place(panel7, PanelXYPlace[6][0], PanelXYPlace[6][1]);
            Panel_Set_Place(panel8, PanelXYPlace[7][0], PanelXYPlace[7][1]);
            Panel_Set_Place(panel9, PanelXYPlace[8][0], PanelXYPlace[8][1]);
            Panel_Set_Place(panel10, PanelXYPlace[9][0], PanelXYPlace[9][1]);
            Panel_Set_Place(panel11, PanelXYPlace[10][0], PanelXYPlace[10][1]);
            Panel_Set_Place(panel12, PanelXYPlace[11][0], PanelXYPlace[11][1]);
            Panel_Set_Place(panel13, PanelXYPlace[12][0], PanelXYPlace[12][1]);
            Panel_Set_Place(panel14, PanelXYPlace[13][0], PanelXYPlace[13][1]);
            Panel_Set_Place(panel15, PanelXYPlace[14][0], PanelXYPlace[14][1]);
            Panel_Set_Place(panel16, PanelXYPlace[15][0], PanelXYPlace[15][1]);

        }
        #endregion
        #region    //初始化控件状态
        private void IniSetPanelArea_1()
        {
            string pVPic = System.Environment.CurrentDirectory + "\\" + "res" + "\\VIcon.png";
            string pAreaBack = System.Environment.CurrentDirectory + "\\" + "res" + "\\Area1.jpg";

            PanelArea1.Parent = MainPic;
            PanelArea1.BackgroundImage = Image.FromFile(pAreaBack);
            PanelArea1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            PanelArea1.BackColor = Color.Transparent;


            Panel_Set_Option(panel25, pVPic, PanelArea1);
            Panel_Set_Option(panel26, pVPic, PanelArea1);
            Panel_Set_Option(panel27, pVPic, PanelArea1);
            Panel_Set_Option(panel28, pVPic, PanelArea1);
            Panel_Set_Option(panel29, pVPic, PanelArea1);
            Panel_Set_Option(panel30, pVPic, PanelArea1);

            Label_Set_Option(label1, PanelArea1);
            Label_Set_Option(label2, PanelArea1);
            Label_Set_Option(label3, PanelArea1);
            Label_Set_Option(label4, PanelArea1);
            Label_Set_Option(label5, PanelArea1);
            Label_Set_Option(label6, PanelArea1);

        }
        private void IniSetPanelArea_2()
        {
            string pTemperature = System.Environment.CurrentDirectory + "\\" + "res" + "\\UpZero.png";
            string pCPUPic = System.Environment.CurrentDirectory + "\\" + "res" + "\\CPU.png";
            string pSavePic = System.Environment.CurrentDirectory + "\\" + "res" + "\\SaveSpace.png";
            string pNormalPic = System.Environment.CurrentDirectory + "\\" + "res" + "\\green.png";
            string pErrPic = System.Environment.CurrentDirectory + "\\" + "res" + "\\red.png";
            string ADVersion = System.Environment.CurrentDirectory + "\\" + "res" + "\\Version.png";
            string ConnVersion = System.Environment.CurrentDirectory + "\\" + "res" + "\\ConnVersion.png";
            string pVPic = System.Environment.CurrentDirectory + "\\" + "res" + "\\VIcon.png";
            string OnlineStatus = System.Environment.CurrentDirectory + "\\" + "res" + "\\NoLine.png";

            PanelArea2.Parent = MainPic;
            PanelArea2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            PanelArea2.BackColor = Color.Transparent;

            Panel_Set_Option(panel31, pTemperature, PanelArea2);
            Panel_Set_Option(panel32, pCPUPic, PanelArea2);
            Panel_Set_Option(panel33, pSavePic, PanelArea2);
            Panel_Set_Option(panel34, pVPic, PanelArea2);
            Panel_Set_Option(panel35, pNormalPic, PanelArea2);
            Panel_Set_Option(panel36, pErrPic, PanelArea2);
            Panel_Set_Option(panel37, ConnVersion, PanelArea2);
            Panel_Set_Option(panel38, ADVersion, PanelArea2);
            Panel_Set_Option(panel39, OnlineStatus, PanelArea2);

        }
        private void IniSetPanelArea_3()
        {
            PanelArea3.Parent = MainPic;
            PanelArea3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            PanelArea3.BackColor = Color.Transparent;

            string pBtnPic17 = System.Environment.CurrentDirectory + "\\" + "res" + "\\MasterConPara.png";
            string pBtnPic18 = System.Environment.CurrentDirectory + "\\" + "res" + "\\DevModle.png";
            string pBtnPic19 = System.Environment.CurrentDirectory + "\\" + "res" + "\\RealCtrl.png";
            string pBtnPic20 = System.Environment.CurrentDirectory + "\\" + "res" + "\\HisData.png";
            string pBtnPic21 = System.Environment.CurrentDirectory + "\\" + "res" + "\\LuBo.png";
            string pBtnPic22 = System.Environment.CurrentDirectory + "\\" + "res" + "\\TestFun.png";
            string pBtnPic23 = System.Environment.CurrentDirectory + "\\" + "res" + "\\Run.png";
            string pBtnPic24 = System.Environment.CurrentDirectory + "\\" + "res" + "\\AutoRun.png";
            string pBtnPic25 = System.Environment.CurrentDirectory + "\\" + "res" + "\\HostState.png";

            Button_Set_Option(MasterConParaBtn, pBtnPic17, PanelArea3);
            Button_Set_Option(DeviceSetBtn, pBtnPic18, PanelArea3);
            Button_Set_Option(RealCtrlBtn, pBtnPic19, PanelArea3);
            Button_Set_Option(HisRecordBtn, pBtnPic20, PanelArea3);
            Button_Set_Option(LbBtn, pBtnPic21, PanelArea3);
            Button_Set_Option(TestFunBtn, pBtnPic22, PanelArea3);
            Button_Set_Option(DebRunBtn, pBtnPic23, PanelArea3);
            Button_Set_Option(AutoRunBtn, pBtnPic24, PanelArea3);
            Button_Set_Option(HostStaBtn, pBtnPic25, PanelArea3);
        }
        private void IniSetPanelArea_4()
        {
            string pOffStatus_01 = System.Environment.CurrentDirectory + "\\" + "res" + "\\Off_01.png";
            string pOffStatus_02 = System.Environment.CurrentDirectory + "\\" + "res" + "\\Off_02.png";
            string pOffStatus_03 = System.Environment.CurrentDirectory + "\\" + "res" + "\\Off_03.png";
            string pOffStatus_04 = System.Environment.CurrentDirectory + "\\" + "res" + "\\Off_04.png";
            string pOffStatus_05 = System.Environment.CurrentDirectory + "\\" + "res" + "\\Off_05.png";
            string pOffStatus_06 = System.Environment.CurrentDirectory + "\\" + "res" + "\\Off_06.png";
            string pOffStatus_07 = System.Environment.CurrentDirectory + "\\" + "res" + "\\Off_07.png";
            string pOffStatus_08 = System.Environment.CurrentDirectory + "\\" + "res" + "\\Off_08.png";
            string pOffStatus_09 = System.Environment.CurrentDirectory + "\\" + "res" + "\\Off_09.png";
            string pOffStatus_10 = System.Environment.CurrentDirectory + "\\" + "res" + "\\Off_10.png";
            string pOffStatus_11 = System.Environment.CurrentDirectory + "\\" + "res" + "\\Off_11.png";
            string pOffStatus_12 = System.Environment.CurrentDirectory + "\\" + "res" + "\\Off_12.png";
            string pOffStatus_13 = System.Environment.CurrentDirectory + "\\" + "res" + "\\Off_13.png";
            string pOffStatus_14 = System.Environment.CurrentDirectory + "\\" + "res" + "\\Off_14.png";
            string pOffStatus_15 = System.Environment.CurrentDirectory + "\\" + "res" + "\\Off_15.png";
            string pOffStatus_16 = System.Environment.CurrentDirectory + "\\" + "res" + "\\Off_16.png";


            PanelArea4.Parent = MainPic;
            PanelArea4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            PanelArea4.BackColor = Color.Transparent;

            Panel_Set_Option(panel1, pOffStatus_01, PanelArea4);
            Panel_Set_Option(panel2, pOffStatus_02, PanelArea4);
            Panel_Set_Option(panel3, pOffStatus_03, PanelArea4);
            Panel_Set_Option(panel4, pOffStatus_04, PanelArea4);
            Panel_Set_Option(panel5, pOffStatus_05, PanelArea4);
            Panel_Set_Option(panel6, pOffStatus_06, PanelArea4);
            Panel_Set_Option(panel7, pOffStatus_07, PanelArea4);
            Panel_Set_Option(panel8, pOffStatus_08, PanelArea4);
            Panel_Set_Option(panel9, pOffStatus_09, PanelArea4);
            Panel_Set_Option(panel10, pOffStatus_10, PanelArea4);
            Panel_Set_Option(panel11, pOffStatus_11, PanelArea4);
            Panel_Set_Option(panel12, pOffStatus_12, PanelArea4);
            Panel_Set_Option(panel13, pOffStatus_13, PanelArea4);
            Panel_Set_Option(panel14, pOffStatus_14, PanelArea4);
            Panel_Set_Option(panel15, pOffStatus_15, PanelArea4);
            Panel_Set_Option(panel16, pOffStatus_16, PanelArea4);

        }
        private void ResetCtrlPlaceAndSize()
        {
            ResetPanelArea_1_PlaceAndSize();
            ResetPanelArea_2_PlaceAndSize();
            ResetPanelArea_3_PlaceAndSize();
            ResetPanelArea_4_PlaceAndSize();
        }
        private void IniToolTipMsg()
        {
            TooltipMsg.AutoPopDelay = 5000;
            TooltipMsg.InitialDelay = 100;
            TooltipMsg.ShowAlways = true;
            TooltipMsg.ReshowDelay = 100;
            TooltipMsg.SetToolTip(panel1, "线路1");
            TooltipMsg.SetToolTip(panel2, "线路2");
            TooltipMsg.SetToolTip(panel3, "线路3");
            TooltipMsg.SetToolTip(panel4, "线路4");
            TooltipMsg.SetToolTip(panel5, "线路5");
            TooltipMsg.SetToolTip(panel6, "线路6");
            TooltipMsg.SetToolTip(panel7, "线路7");
            TooltipMsg.SetToolTip(panel8, "线路8");
            TooltipMsg.SetToolTip(panel9, "线路9");
            TooltipMsg.SetToolTip(panel10, "线路10");
            TooltipMsg.SetToolTip(panel11, "线路11");
            TooltipMsg.SetToolTip(panel12, "线路12");
            TooltipMsg.SetToolTip(panel13, "线路13");
            TooltipMsg.SetToolTip(panel14, "线路14");
            TooltipMsg.SetToolTip(panel15, "线路15");
            TooltipMsg.SetToolTip(panel16, "线路16");
        }
        private void ResetPanelPlace()
        {
            ResetCtrlPlaceAndSize();
        }
        #endregion
        #region    //按钮响应函数
        private void ResetDeRunStatus(bool bStatus)
        {
            string pRunStatus = System.Environment.CurrentDirectory + "\\" + "res" + "\\Run.png";
            string pStotpStatus = System.Environment.CurrentDirectory + "\\" + "res" + "\\StopRun.png";
            string OnlineStatus = System.Environment.CurrentDirectory + "\\" + "res" + "\\Online.png";
            string NolineStatus = System.Environment.CurrentDirectory + "\\" + "res" + "\\NoLine.png";

            if (bStatus)
            {
                AutoRunBtn.Enabled = false;
                Button_Set_Option(DebRunBtn, pStotpStatus, PanelArea3);
                Panel_Set_Option(panel39, OnlineStatus, PanelArea2);
                label15.Text = "通讯状态：在线";
            }
            else
            {
                AutoRunBtn.Enabled = true;
                Button_Set_Option(DebRunBtn, pRunStatus, PanelArea3);
                Panel_Set_Option(panel39, NolineStatus, PanelArea2);
                label15.Text = "通讯状态：离线";
            }
        }
        private void DebRunBtn_Click(object sender, EventArgs e)
        {
            if (m_bDeRun)
            {
                CGlobal.m_ClientSocket.CloseThread();
                m_bDeRun = false;
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;//等待
                m_bDeRun = CGlobal.m_ClientSocket.ConnectService("192.168.10.15", 2404, 2000);
                this.Cursor = Cursors.Default;
            }
            ResetDeRunStatus(m_bDeRun);
        }
        private void HostStaBtn_Click(object sender, EventArgs e)
        {
            CGlobal.SendTaskOrder(1);
        }
        public bool tabControlCheckHave(System.Windows.Forms.TabControl tab, String tabName)
        {
            for (int i = 0; i < tab.TabCount; i++)
            {
                if (tab.TabPages[i].Text == tabName)
                {
                    tab.SelectedIndex = i;
                    return true;
                }
            }
            return false;
        }

        private void AddTabForm(System.Windows.Forms.Form   pForm,string strTabName)
        {
            this.tabFormCtrl.TabPages.Add(strTabName);
            this.tabFormCtrl.SelectTab((int)(this.tabFormCtrl.TabPages.Count - 1));
            pForm.FormBorderStyle = FormBorderStyle.None;
            pForm.TopLevel = false;
            pForm.Show();
            pForm.Dock = DockStyle.Fill;
            pForm.Parent = this.tabFormCtrl.SelectedTab;
            this.tabFormCtrl.Dock = DockStyle.Fill;
            this.tabFormCtrl.Show();

            PanelArea1.Hide();
            PanelArea2.Hide();
            PanelArea3.Hide();
            PanelArea4.Hide();
        }
        private void MasterConParaBtn_Click(object sender, EventArgs e)
        {
            string strTabName = "  通讯配置    ";
            if (tabControlCheckHave(tabFormCtrl, strTabName) == false)
            {
                CMasterConForm myForm = new CMasterConForm();
                AddTabForm(myForm, strTabName);
            }
        }
        private void RealCtrlBtn_Click(object sender, EventArgs e)
        {
            string strTabName = "  实时监控    ";
            if (tabControlCheckHave(tabFormCtrl, strTabName) == false)
            {
                CRealCtrlForm myForm = new CRealCtrlForm();
                AddTabForm(myForm, strTabName);
            }
        }
        #endregion
        private void tabFormCtrl_VisibleChanged(object sender, EventArgs e)
        {
            m_TableShow = tabFormCtrl.Visible;
            if (m_TableShow)
            {
                PanelArea1.Hide();
                PanelArea2.Hide();
                PanelArea3.Hide();
                PanelArea4.Hide();
            }
            else
            {
                PanelArea1.Show();
                PanelArea2.Show();
                PanelArea3.Show();
                PanelArea4.Show();

            }
            this.Invalidate();
        }
    }
}
