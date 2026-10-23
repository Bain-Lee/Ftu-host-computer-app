using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DtuTestManager
{
    public partial class MyTabControl : TabControl
    {
        private Color _lineColor = Color.FromArgb(157, 162, 168); //边线颜色 
        private Color _ColorDefaultA = Color.FromArgb(231, 231, 231);//默认标签渐变 a
        private Color _ColorDefaultB = Color.FromArgb(255, 255, 255);//默认标签渐变 b
        private Color _ColorActivateA = Color.FromArgb(184, 203, 217);//点击渐变a
        private Color _ColorActivateB = Color.FromArgb(255, 255, 255);//点击渐变a
        private static readonly int Radius = 10;
        private bool CloseEX = false;
        const int CLOSE_SIZE = 12;
        public MyTabControl()
        {
            //SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true); 
            this.DrawMode = TabDrawMode.OwnerDrawFixed;
            base.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor, true);
            base.UpdateStyles();

        }
        private Rectangle GetCloseRect(Rectangle myTabRect)
        {
            myTabRect.Offset(myTabRect.Width - (CLOSE_SIZE + 3), 2);
            myTabRect.Width = CLOSE_SIZE;
            myTabRect.Height = CLOSE_SIZE;
            return myTabRect;
        }
        private bool TabPageMouseClose(Point pt)
        {
            if (this.SelectedIndex < 0 || this.TabCount == 0)
            {
                Console.WriteLine("NowTab_Status!  " + this.SelectedIndex + "__" +this.TabCount.ToString());
                return false;
            }
            Console.WriteLine("NowTabCout!" + this.TabCount.ToString());
            Rectangle rect = this.GetTabRect(this.SelectedIndex);
            rect = GetCloseRect(rect);
            return rect.Contains(pt);
        }
        private GraphicsPath CreateTabPath(Rectangle rect)
        {

            GraphicsPath path = new GraphicsPath();
            rect.Width -= 1;
            path.AddLine(rect.X + Radius, rect.Y, rect.Right - Radius / 2, rect.Y);
            path.AddArc(rect.Right - Radius, rect.Y, Radius, Radius, 270F, 90F);
            path.AddLine(rect.Right, rect.Y + Radius / 2, rect.Right, rect.Bottom + 1);
            path.AddLine(rect.Right, rect.Bottom + 1, rect.X - Radius, rect.Bottom + 1);
            path.CloseFigure(); 
            path.CloseFigure();
            return path;
        }
        
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            pe.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pe.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
           
            //pe.Graphics.TextRenderingHint = TextRenderingHint.AntiAlias;
            //绘制背景
            Rectangle rect = this.ClientRectangle;
            pe.Graphics.FillRectangle(new TextureBrush(Properties.Resources.TabBack), rect);
            //NowPe = new PaintEventArgs(pe.Graphics, rect);
            //绘制边线 
            int height = this.ItemSize.Height + 3;
            Rectangle r = new Rectangle(DisplayRectangle.X - 1, DisplayRectangle.Y - 1, DisplayRectangle.Width + 1, DisplayRectangle.Height + 1);

            // pe.Graphics.DrawLine(new Pen(Color.FromArgb(157, 162, 168)), new Point(rect.X, rect.Y + height), new Point(rect.Right, rect.Y + height));
            pe.Graphics.DrawRectangle(new Pen(_lineColor), r);
            //绘制边框  

            //绘制标头
            
            foreach (TabPage tp in this.TabPages)
            {
                DrawTabPage(pe.Graphics, this.GetTabRect(this.TabPages.IndexOf(tp)), tp);
            } 
        }
        private void DrawTabPage(Graphics graphics, Rectangle rectangle, TabPage tp)
        {
            //绘制底纹  

            StringFormat sf = new StringFormat();

            sf.Trimming = StringTrimming.EllipsisCharacter;
            sf.FormatFlags = StringFormatFlags.NoWrap;
            Rectangle rect = new Rectangle(rectangle.X, rectangle.Y + 2, rectangle.Width, rectangle.Height - 2); //标准区域
            Rectangle fontRect = new Rectangle(rectangle.X + 5, rectangle.Y + 5, rectangle.Width - 10, tp.Font.Height);//文字区域

            if (this.SelectedTab.Equals(tp))
            {
                rect = new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
                fontRect = new Rectangle(rectangle.X + 5, rectangle.Y + 5, rectangle.Width - 25, rectangle.Height - 7);


                //绘制边框   
                graphics.FillPath(new LinearGradientBrush(new Point(rect.X, rect.Y), new Point(rect.X, rect.Y + rect.Height), _ColorActivateA, _ColorActivateB), CreateTabPath(rect)); //填充颜色
                graphics.DrawString(tp.Text, tp.Font, new SolidBrush(tp.ForeColor), fontRect, sf); //文字绘制


                graphics.DrawPath(new Pen(_lineColor), CreateTabPath(rect));//绘制实边线  


                //掩盖下部的绘制 
                graphics.DrawLine(new Pen(_ColorActivateB, 3), rect.X, rect.Bottom + 1, rect.X + rect.Width, rect.Bottom + 1);


                //绘制图片
                Rectangle myTabRect = GetCloseRect(rectangle);
                //画关闭符号   
                using (Pen objpen = new Pen(Color.Black))
                {
                    ////=============================================   
                    //自己画X   
                    //"\"线   
                    Point p1 = new Point(myTabRect.X + 3, myTabRect.Y + 3);
                    Point p2 = new Point(myTabRect.X + myTabRect.Width - 3, myTabRect.Y + myTabRect.Height - 3);
                    graphics.DrawLine(objpen, p1, p2);
                    //"/"线   
                    Point p3 = new Point(myTabRect.X + 3, myTabRect.Y + myTabRect.Height - 3);
                    Point p4 = new Point(myTabRect.X + myTabRect.Width - 3, myTabRect.Y + 3);
                    graphics.DrawLine(objpen, p3, p4);


                    ////=============================================   
                    //使用图片   
                    //Bitmap bt = new Bitmap(image);
                    //Point p5 = new Point(myTabRect.X, 4);
                    //e.Graphics.DrawImage(bt, p5);
                    //e.Graphics.DrawString(this.MainTabControl.TabPages[e.Index].Text, this.Font, objpen.Brush, p5);   
                }
            }
            else
            {
                //绘制边框   
                graphics.FillPath(new LinearGradientBrush(new Point(rect.X, rect.Y), new Point(rect.X, rect.Y + rect.Height), _ColorDefaultA, _ColorDefaultB), CreateTabPath(rect)); //填充颜色
                graphics.DrawString(tp.Text, tp.Font, new SolidBrush(tp.ForeColor), fontRect, sf); //文字绘制
                graphics.DrawPath(new Pen(Color.Wheat, 2), CreateTabPath(rect));//绘制高光边线
                // graphics.DrawPath(new Pen(_lineColor), CreateTabPath(rect));//绘制实边线  
            }
        }
        protected override CreateParams CreateParams
        {
           get
            {
               CreateParams cp = base.CreateParams;
               cp.ExStyle |= 0x02000000;
               return cp;
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = e.X, y = e.Y;
                //计算关闭区域      
                Rectangle myTabRect = this.GetTabRect(this.SelectedIndex);


                myTabRect.Offset(myTabRect.Width - (CLOSE_SIZE + 3), 2);
                myTabRect.Width = CLOSE_SIZE;
                myTabRect.Height = CLOSE_SIZE;


                //如果鼠标在区域内就关闭选项卡      
                bool isClose = x > myTabRect.X && x < myTabRect.Right && y > myTabRect.Y && y < myTabRect.Bottom;
                if (isClose == true)
                {
                    this.TabPages.Remove(this.SelectedTab);
                    if (this.TabCount == 0)
                    {
                        Hide();
                    } 
                }
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {

            if (TabPageMouseClose(e.Location) != CloseEX)
            {
                CloseEX = !CloseEX;
               // this.Invalidate();
         
            }
            if (CloseEX) 
                this.Cursor = Cursors.Hand;
            else
                this.Cursor = Cursors.Default;
            
            base.OnMouseMove(e);
        }
    }
}
