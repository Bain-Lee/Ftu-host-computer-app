using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace DtuTestManager
{
    public class CPointXY
    {
        int m_Left;
        int m_Top;
        int m_Right;
        int m_Bottom;
        int m_Width;
        int m_Height;
        int m_Middle;
        int m_DrawHeight;
        public int nLeft
        {
            set
            {
                m_Left = 5 + value;
                m_Right = m_Left + m_Width;
            }
            get
            {
                return m_Left;
            }
        }
        public int nTop
        {
            set
            {
                m_Top = value;
                m_Bottom = m_Top + m_Height;
                m_Middle = m_Top + m_Height / 2;
            }
            get
            {
                return m_Top;
            }
        }
        public int nWidth
        {
            set
            {
                m_Width = value;
                m_Right = m_Left + m_Width;
            }
            get
            {
                return m_Width;
            }
        }
        public int nHeight
        {
            set
            {
                m_Height = value;
                m_Bottom = m_Top + m_Height;
                m_Middle = m_Top + m_Height / 2;
                m_DrawHeight = m_Height / 2;
            }
            get
            {
                return m_Height;
            }
        }
        public int nRight
        {
            get
            {
                return m_Right;
            }
        }
        public int nBottom
        {
            get
            {
                return m_Bottom;
            }
        }
        public int nMiddle
        {
            get
            {
                return m_Middle;
            }
        }
        public int nDrawHeight
        {
            get
            {
                return m_DrawHeight;
            }
        }
    }
    public class CDataPoint
    {
        public double m_DataValue;
        public Point m_Point;
    }
    public class CSelLine
    {
        public int m_nX = -1;
        public double m_Place = 0;
        public Pen m_Pen;
        public bool m_Click = false;
        public bool m_Move = false;
        public CSelLine(Color pColor, int nBlod)
        {
            if (nBlod > 0)
            {
                m_Pen = new Pen(pColor, nBlod);
            }
            else
            {
                m_Pen = new Pen(pColor, 1);
                m_Pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Custom;
                m_Pen.DashPattern = new float[] { 5, 5 };
            }
        }
    }
    public class CSoePicData
    {
        public string m_Name;           //曲线名称
        public int m_ModeType = 0;
        public double m_fMaxValue = 0; //最大值
        public double m_HeightMul = 1.0;
        public CPointXY m_NamePoint = new CPointXY();           //名称坐标
        public CPointXY m_PicPoint = new CPointXY();            //曲线坐标

        private IList<double> m_DataList = new List<double>();             //数据列表
        public IList<CDataPoint> m_DrawList = new List<CDataPoint>();   //绘图坐标列表
        public Pen m_Pen;
        public Pen m_ViturlPen;
        public CSoePicData()
        {
            m_Name = "曲线 " + DateTime.Now.ToString();
        }
        public void AddData(double dfValue)
        {
            CDataPoint pNewData = new CDataPoint();
            pNewData.m_DataValue = dfValue;
            m_DrawList.Add(pNewData);
            if (Math.Abs(dfValue) > m_fMaxValue)
                m_fMaxValue = Math.Abs(dfValue);
        }
        public void SetPicPoint(int nLeft, int nTop, int nWidth, int nHeight)
        {
            m_PicPoint.nLeft = 5 + nLeft;
            m_PicPoint.nTop = nTop + 2;
            m_PicPoint.nWidth = nWidth * 59 / 60;
            m_PicPoint.nHeight = (int)((double)nHeight * m_HeightMul);
        }
        public void SetWidth(int npWidth)
        {
            m_PicPoint.nWidth = npWidth * 79 / 80;
        }
        public void SetHeight(int npHeight)
        {
            m_PicPoint.nHeight = npHeight;
        }
        public void SetNamePoint(int nLeft, int nTop)
        {
            m_NamePoint.nLeft = nLeft;
            m_NamePoint.nTop = nTop;
        }
        public void IniSetDrawPointData()
        {
            int i;
            if (m_PicPoint.nWidth <= 50 || m_DrawList.Count == 0)
                return;
            float fSpace = ((float)m_PicPoint.nWidth) / ((float)m_DrawList.Count);
            for (i = 0; i < m_DrawList.Count; i++)
            {
                m_DrawList[i].m_Point.X = (int)(fSpace * (float)i) + m_PicPoint.nLeft;
                m_DrawList[i].m_Point.Y = m_PicPoint.nMiddle - (int)(m_DrawList[i].m_DataValue / m_fMaxValue * ((float)m_PicPoint.nDrawHeight));
                if (m_DrawList[i].m_Point.X >= 10000)
                    m_DrawList[i].m_Point.X = 10000;
                if (m_DrawList[i].m_Point.Y < -10000)
                    m_DrawList[i].m_Point.Y = -10000;
                if (m_DrawList[i].m_Point.Y > 10000)
                    m_DrawList[i].m_Point.Y = 10000;
            }

            m_NamePoint.nLeft = m_PicPoint.nLeft;
            m_NamePoint.nTop = m_PicPoint.nMiddle - 5;
        }
    }
}
