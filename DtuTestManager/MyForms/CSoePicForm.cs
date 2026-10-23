using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace DtuTestManager
{
    public partial class CSoePicForm : Form
    {
        private     CSelLine m_LeftLine = new CSelLine(Color.Red,1);      //左键直线
        private     CSelLine m_RightLine = new CSelLine(Color.Blue,1);    //右键直线
        private     CSelLine m_VirtualLine = new CSelLine(Color.White, 0);    //默认开始位置

        private CSelLine m_RightSelLine = null;
        private CSelLine m_LeftSelLine = null;
        public CSoePicData[] m_SinCurrent = null;
        private List<Label> m_NameList = new List<Label>();

        private int nPenWith = 1;
        private double[] dfX = new double[7];
        private int m_X_SelIndex = 2;
        private int m_Y_SelIndex = 2;
        private int m_CurrSelIndex = 0;
        private bool bWinFoc = false;
        private int SinFreH = 80;
        public  CFileCtrl m_File = null;
        public bool m_bShowMsg = true;
        public CSoePicForm()
        {
            InitializeComponent();
        }

        private void IniMaxValue(string ChanelType)
        {
            int i;
            double dMaxValue = 0;
            for (i = 0; i < m_SinCurrent.Length; i++)
            {
                if (m_File.m_CWaveInfo.m_ChanelList[i].m_ChanelType == ChanelType)
                {
                    if (dMaxValue < m_SinCurrent[i].m_fMaxValue) dMaxValue = m_SinCurrent[i].m_fMaxValue;
                }
            }
            for (i = 0; i < m_SinCurrent.Length; i++)
            {
                if (m_File.m_CWaveInfo.m_ChanelList[i].m_ChanelType == ChanelType)
                {
                    m_SinCurrent[i].m_fMaxValue = dMaxValue;
                }
            }
        }
        private void IniCurrentData()
        {
            int i,j;
            for (i = 0; i < m_SinCurrent.Length; i++)
            {
                for (j = 0; j < m_File.m_CWaveInfo.m_ChanelList[i].m_DataList.Count; j++)
                {
                    m_SinCurrent[i].AddData(m_File.m_CWaveInfo.m_ChanelList[i].m_DataList[j]);
                }
            }
            for (i = 0; i < m_SinCurrent.Length; i++)
            {
                IniMaxValue(m_File.m_CWaveInfo.m_ChanelList[i].m_ChanelType);
            }
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0xA3)
            {
                FreshTimer.Stop();
                FreshTimer.Start();
            }
            if (m.Msg == 132)
            {
                bWinFoc = true;
            }
         }
        private void IniCurrentObject()
        {
            int i;
            Color[] pColor = new Color[4];
            pColor[0] = Color.Yellow;
            pColor[1] = Color.Green;
            pColor[2] = Color.Red;
            pColor[3] = Color.Blue;

            m_VirtualLine.m_Place = 0.10;

            //初始化画笔
            for (i = 0; i < m_SinCurrent.Length; i++)
            {
                m_SinCurrent[i].m_Pen = new Pen(pColor[i%4], nPenWith);
                m_SinCurrent[i].m_ViturlPen = new Pen(Color.Azure, 1);
                m_SinCurrent[i].m_ViturlPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                m_SinCurrent[i].m_ViturlPen.DashPattern = new float[] { 10, 10 };
            }
            //初始化名称
            for (i = 0; i < m_SinCurrent.Length; i++)
            {
                m_SinCurrent[i].m_Name = m_File.m_CWaveInfo.m_ChanelList[i].m_ChanelName;
                m_SinCurrent[i].m_ModeType = m_File.m_CWaveInfo.m_ChanelList[i].m_nModeType;
            }
            //初始化数据
            IniCurrentData();
            //初始化坐标系
            for (i = 0; i < m_SinCurrent.Length; i++)
            {
                m_SinCurrent[i].SetPicPoint(0, CurrentPicBox.Height / m_SinCurrent.Length * i, CurrentPicBox.Width, CurrentPicBox.Height / m_SinCurrent.Length);
                m_SinCurrent[i].IniSetDrawPointData();
            }
        }
        private void CSoePicForm_Load(object sender, EventArgs e)
        {
            if (m_File == null) return;
            if (m_File.m_CWaveInfo.m_ChanelCount == 0) return;
            this.DoubleBuffered = true;
            int i;

            //初始化曲线对象
            m_SinCurrent = new CSoePicData[m_File.m_CWaveInfo.m_ChanelCount];
            for (i = 0; i < m_SinCurrent.Length; i++)
            {
                m_SinCurrent[i] = new CSoePicData();
            }
            //初始化数据
            IniCurrentObject();

            //采样频率
            SinFreH = m_File.m_CWaveInfo.m_Samp/50;
            if (SinFreH <= 0) SinFreH = 80;
            SinFreH *= 4;
           
            //初始化控件
            StartTime.Text = "起始时间：" + m_File.m_CWaveInfo.m_strStartTime;
            EndTime.Text = "结束时间：" + m_File.m_CWaveInfo.m_strEndTime;

            for (i = 0; i < m_SinCurrent.Length; i++)
            {
                Label pNewLabel = new Label();
                pNewLabel.Text = m_SinCurrent[i].m_Name;
                m_NameList.Add(pNewLabel);
                SoeNamePanel.Controls.Add(pNewLabel);
                pNewLabel.Visible = true;
            }

            for (i = 0; i < m_SinCurrent.Length; i++)
            {
                LineSel.Items.Add(m_SinCurrent[i].m_Name);
            }
            LineSel.SelectedIndex = 0;

            ComBox_X.Items.Add("20%");
            ComBox_X.Items.Add("50%");
            ComBox_X.Items.Add("100%");
            ComBox_X.Items.Add("150%");
            ComBox_X.Items.Add("200%");
            ComBox_X.Items.Add("300%");
            ComBox_X.Items.Add("400%");
            ComBox_X.SelectedIndex = 2;

            dfX[0] = 0.2;
            dfX[1] = 0.5;
            dfX[2] = 1.0;
            dfX[3] = 1.5;
            dfX[4] = 2.0;
            dfX[5] = 3.0;
            dfX[6] = 4.0;

            ComBox_Y.Items.Add("20%");
            ComBox_Y.Items.Add("50%");
            ComBox_Y.Items.Add("100%");
            ComBox_Y.Items.Add("150%");
            ComBox_Y.Items.Add("200%");
            ComBox_Y.Items.Add("300%");
            ComBox_Y.Items.Add("400%");
            ComBox_Y.SelectedIndex = 2;
        }
        private void ClcLinePalce(int nXPlace,bool nLeft,Graphics pGP)
        {
            int nX = nXPlace - m_SinCurrent[0].m_DrawList[0].m_Point.X;
            int nLength = m_SinCurrent[0].m_DrawList[m_SinCurrent[0].m_DrawList.Count - 1].m_Point.X - m_SinCurrent[0].m_DrawList[0].m_Point.X;
            float fPlace = (float)nX / (float)nLength;
            int nPlace = (int)((float)m_SinCurrent[0].m_DrawList.Count * fPlace);
            double TimeOffset = m_File.m_CWaveInfo.m_EndTime - m_File.m_CWaveInfo.m_StartTime;
            double nNowTimeOffset = m_File.m_CWaveInfo.m_StartTime + TimeOffset * fPlace;
            DateTime pThisTime = new DateTime((long)nNowTimeOffset);
            if (nLeft == false)
            {
                LeftTime.Text = pThisTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            }
            else
            {
                RightTime.Text = pThisTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            }
            if (m_bShowMsg)
            {
                Font f = new Font("Arial", 10);
                for (int i = 0; i < m_SinCurrent.Length; i++)
                {
                    string strMsg = m_SinCurrent[i].m_DrawList[nPlace].m_DataValue.ToString("F5");
                    pGP.DrawString(strMsg, f, Brushes.WhiteSmoke, m_SinCurrent[i].m_DrawList[nPlace].m_Point);
                }
            }
        }
        private void CurrentPicBox_Paint(object sender, PaintEventArgs e)
        {
            int i, j;
            if (m_SinCurrent == null) return;
            Graphics TempBg = e.Graphics;
            for (i = 0; i < m_SinCurrent.Length; i++)
            {
                TempBg.DrawLine(m_SinCurrent[i].m_ViturlPen, m_SinCurrent[i].m_PicPoint.nLeft, m_SinCurrent[i].m_PicPoint.nMiddle, CurrentPicBox.Width, m_SinCurrent[i].m_PicPoint.nMiddle);
                m_NameList[i].Left = SoeNamePanel.Width / 2 - m_NameList[i].Width / 2;
                m_NameList[i].Top = m_SinCurrent[i].m_NamePoint.nTop;

                if (m_SinCurrent[i].m_ModeType == 0)
                {
                    for (j = 1; j < m_SinCurrent[i].m_DrawList.Count; j++)
                    {
                        TempBg.DrawLine(m_SinCurrent[i].m_Pen, m_SinCurrent[i].m_DrawList[j - 1].m_Point, m_SinCurrent[i].m_DrawList[j].m_Point);
                    }
                }
                else
                {
                    for (j = 1; j < m_SinCurrent[i].m_DrawList.Count; j++)
                    {
                        if (m_SinCurrent[i].m_DrawList[j - 1].m_DataValue == m_SinCurrent[i].m_DrawList[j].m_DataValue)
                        {
                            TempBg.DrawLine(m_SinCurrent[i].m_Pen, m_SinCurrent[i].m_DrawList[j - 1].m_Point, m_SinCurrent[i].m_DrawList[j].m_Point);
                        }
                        else
                        {
                            TempBg.DrawLine(m_SinCurrent[i].m_Pen, 
                                                m_SinCurrent[i].m_DrawList[j - 1].m_Point.X,
                                                m_SinCurrent[i].m_DrawList[j - 1].m_Point.Y, 
                                                m_SinCurrent[i].m_DrawList[j].m_Point.X,
                                                m_SinCurrent[i].m_DrawList[j - 1].m_Point.Y);
                            TempBg.DrawLine(m_SinCurrent[i].m_Pen,
                                                m_SinCurrent[i].m_DrawList[j].m_Point.X,
                                                m_SinCurrent[i].m_DrawList[j - 1].m_Point.Y,
                                                m_SinCurrent[i].m_DrawList[j].m_Point.X,
                                                m_SinCurrent[i].m_DrawList[j].m_Point.Y);
                        }
                    }
                }

               
            }

            if (m_VirtualLine != null)
            {
                if(m_SinCurrent.Length>0 && SinFreH<m_SinCurrent[0].m_DrawList.Count)
                {
                    int nX = m_SinCurrent[0].m_DrawList[SinFreH-1].m_Point.X;
                    m_VirtualLine.m_nX = nX;
                    TempBg.DrawLine(m_VirtualLine.m_Pen, nX, 0, nX, CurrentPicBox.Bottom);
                }
            }
            if (m_LeftLine.m_Click)
            {
                if (m_SinCurrent.Length > 0)
                {
                    int nX = (int)(m_LeftLine.m_Place * CurrentPicBox.Width);
                    if (nX >= m_SinCurrent[0].m_DrawList[0].m_Point.X &&
                            nX <= m_SinCurrent[0].m_DrawList[m_SinCurrent[0].m_DrawList.Count - 1].m_Point.X)
                    {
                        m_LeftLine.m_nX = nX;
                        TempBg.DrawLine(m_LeftLine.m_Pen, nX, 0, nX, CurrentPicBox.Bottom);
                        ClcLinePalce(nX,false,TempBg);
                    }
                }
            }
            if (m_RightLine.m_Click)
            {
                if (m_SinCurrent.Length > 0)
                {
                    int nX = (int)(m_RightLine.m_Place * CurrentPicBox.Width);
                    if (nX >= m_SinCurrent[0].m_DrawList[0].m_Point.X &&
                            nX <= m_SinCurrent[0].m_DrawList[m_SinCurrent[0].m_DrawList.Count - 1].m_Point.X)
                    {
                        m_RightLine.m_nX = nX;
                        TempBg.DrawLine(m_RightLine.m_Pen, nX, 0, nX, CurrentPicBox.Bottom);
                        ClcLinePalce(nX, true, TempBg);
                    }
                }
            }
        }
        private void CurrentPicBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_SinCurrent == null) return;
            if (e.Button == MouseButtons.Left)
            {
                if (m_LeftSelLine != null)
                {
                    if (m_LeftSelLine.m_Move)
                    {
                        m_LeftLine.m_Place = (double)(e.Location.X) / (double)CurrentPicBox.Width;
                        CurrentPicBox.Invalidate();
                        return;
                    }
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                if (m_RightSelLine != null)
                {
                    if (m_RightSelLine.m_Move)
                    {
                        m_RightSelLine.m_Place = (double)(e.Location.X) / (double)CurrentPicBox.Width;
                        CurrentPicBox.Invalidate();
                        return;
                    }
                }
            }
            if (Math.Abs(e.Location.X - m_LeftLine.m_nX) <= 2 && m_LeftLine.m_nX>=0)
            {
                this.Cursor = Cursors.SizeWE;
                m_LeftSelLine = m_LeftLine;
            }
            else
            {
                m_LeftSelLine = null;
            }
            if (Math.Abs(e.Location.X - m_RightLine.m_nX) <= 2 && m_RightLine.m_nX >= 0)
            {
                this.Cursor = Cursors.SizeWE;
                m_RightSelLine = m_RightLine;
            }
            else
            {
                m_RightSelLine = null;
            }
            if (m_LeftSelLine == null && m_RightSelLine == null)
            {
                this.Cursor = Cursors.Default;
            }

        }
        private void CurrentPicBox_MouseUp(object sender, MouseEventArgs e)
        {
            m_LeftLine.m_Move = false;
            m_RightLine.m_Move = false;
            m_LeftSelLine = null;
            m_RightSelLine = null;
        }
        private void CurrentPicBox_MouseDown(object sender, MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (m_SinCurrent == null) return;
            Rectangle rect = new Rectangle(CurrentPicBox.Left, CurrentPicBox.Top, CurrentPicBox.Width, CurrentPicBox.Height);
            if (e.Button == MouseButtons.Left)
            {
                if (m_LeftSelLine == null)
                {
                    m_LeftLine.m_Click = rect.Contains(e.Location);
                    if (m_LeftLine.m_Click)
                    {
                        Point msPoint = e.Location;
                        int LineX = msPoint.X - CurrentPicBox.Left;
                        m_LeftLine.m_Place = (double)LineX / (double)CurrentPicBox.Width;
                        CurrentPicBox.Invalidate();
                    }
                }
                else
                {
                    m_LeftSelLine.m_Move = true;
                }
              
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (m_RightSelLine == null)
                {
                    m_RightLine.m_Click = rect.Contains(e.Location);
                    if (m_RightLine.m_Click)
                    {
                        Point msPoint = e.Location;
                        int LineX = msPoint.X - CurrentPicBox.Left;
                        m_RightLine.m_Place = (double)LineX / (double)CurrentPicBox.Width;
                        CurrentPicBox.Invalidate();
                    }
                }
                else
                {
                    m_RightSelLine.m_Move = true;
                }
            }
        }
        private void BtnReset_Click(object sender, EventArgs e)
        {
            if (m_SinCurrent == null) return;
            ComBox_X.SelectedIndex = 2;
            ComBox_Y.SelectedIndex = 2;
            CurrentPicPanel.Dock = DockStyle.Fill;
            PicMainPanel.Dock = DockStyle.Fill;

            for (int i = 0; i < m_SinCurrent.Length; i++)
            {
                m_SinCurrent[i].m_HeightMul = 1.0;
                m_SinCurrent[i].SetPicPoint(0, CurrentPicBox.Height / 9 * i, CurrentPicBox.Width, CurrentPicBox.Height / 9);
                m_SinCurrent[i].IniSetDrawPointData();

            }
            this.Validate();
            CurrentPicBox.Invalidate();
        }
        private void ComBox_X_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_X_SelIndex == ComBox_X.SelectedIndex) return;
            m_X_SelIndex = ComBox_X.SelectedIndex;
            FreshTimer.Stop();
            FreshTimer.Start();
        }
        private void ComBox_Y_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_Y_SelIndex = ComBox_Y.SelectedIndex;
            m_SinCurrent[m_CurrSelIndex].m_HeightMul = dfX[m_Y_SelIndex];
            FreshTimer.Stop();
            FreshTimer.Start();
        }
        private void LineSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_CurrSelIndex = LineSel.SelectedIndex;
        }
        private void ReDrawCurrent()
        {
            int i;
            if (m_SinCurrent == null) return;
            CurrentPicPanel.Dock = DockStyle.Fill;
            PicMainPanel.Dock = DockStyle.Fill;
            this.Validate();
            int MainPanelWidth = PicMainPanel.Width;
            int CurrentWidth = CurrentPicPanel.Width;
            int CurrentHeight = CurrentPicPanel.Height;

            int LineNewHeight = (int)((double)CurrentHeight * dfX[m_Y_SelIndex] / m_SinCurrent.Length);
            int nChangeHeight = LineNewHeight - m_SinCurrent[m_CurrSelIndex].m_PicPoint.nHeight;

            CurrentPicPanel.Dock = DockStyle.None;
            PicMainPanel.Dock = DockStyle.None;

            int nNewHeight = 0;
            double dfDfaultHeight = (double)CurrentHeight / m_SinCurrent.Length;
            PicMainPanel.Left = 0;
            PicMainPanel.Top = toolStrip1.Bottom;
            for (i = 0; i < m_SinCurrent.Length; i++)
            {
                nNewHeight += (int)(dfDfaultHeight * m_SinCurrent[i].m_HeightMul);
            }
            PicMainPanel.Height = nNewHeight;
            PicMainPanel.Width = (int)((double)CurrentWidth * dfX[m_X_SelIndex]) + SoeNamePanel.Width;

            CurrentPicPanel.Left = SoeNamePanel.Right;
            CurrentPicPanel.Top = 0;
            CurrentPicPanel.Width = PicMainPanel.Width - SoeNamePanel.Width;
            CurrentPicPanel.Height = PicMainPanel.Height;

            for (i = 0; i < m_SinCurrent.Length; i++)
            {
                if (i == 0)
                {
                    m_SinCurrent[i].SetPicPoint(0, 0, CurrentPicPanel.Width, (int)dfDfaultHeight);
                }
                else
                {
                    m_SinCurrent[i].SetPicPoint(0, m_SinCurrent[i - 1].m_PicPoint.nBottom, CurrentPicPanel.Width, (int)dfDfaultHeight);
                }
                m_SinCurrent[i].IniSetDrawPointData();
            }
            CurrentPicBox.Invalidate();
        }
        private void FreshTimer_Tick(object sender, EventArgs e)
        {
            FreshTimer.Stop();
            ReDrawCurrent();
        }
        int nResizeSpace = 0;
        private void CSoePicForm_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
            CurrentPicBox.Invalidate();
            nResizeSpace++;
            if (bWinFoc)
            {
                bWinFoc = true;
                nResizeSpace = 0;
                ReDrawCurrent();
            }
        }

        private void BtnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "录波文件(*.cfg)|*.cfg";
            ofd.ValidateNames = true;
            ofd.CheckPathExists = true;
            ofd.CheckFileExists = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                CFileCtrl pFile = new CFileCtrl();
                pFile.m_FilePath = ofd.FileName;
                pFile.m_FileType = 0;

                if (".cfg".IndexOf(pFile.m_FilePath.Substring(pFile.m_FilePath.LastIndexOf("."))) != -1)
                {
                    pFile.m_FileTypeName = pFile.m_FilePath.Substring(pFile.m_FilePath.LastIndexOf("\\") + 1);
                    pFile.ReadFileContent();//读取文件
                    if (pFile.m_CWaveInfo == null || pFile.m_CWaveInfo.m_Success == false) return;
                    this.Hide();
                    CSoePicForm pForm = new CSoePicForm();
                    pForm.m_File = pFile;
                    pForm.ShowDialog();
                    this.Close();
                }
            }
        }

        private void ShowMsg_Click(object sender, EventArgs e)
        {
            m_bShowMsg = true;
            CurrentPicBox.Invalidate();
        }

        private void HideMsg_Click(object sender, EventArgs e)
        {
            m_bShowMsg = false;
            CurrentPicBox.Invalidate();
        }
    }
}
