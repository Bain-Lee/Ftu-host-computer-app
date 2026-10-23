using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DtuTestManager
{
    public partial class CNormalTabControl : TabControl
    {
        public CNormalTabControl()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint  //全部在窗口绘制消息中绘图
                      | ControlStyles.OptimizedDoubleBuffer //使用双缓冲
                      , true);
        }
    }
}
