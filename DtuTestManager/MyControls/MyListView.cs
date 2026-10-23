using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DtuTestManager
{
    public partial class MyListView : ListView
    {
        [StructLayoutAttribute(LayoutKind.Sequential)]
        public struct LV_ITEM
        {
            public UInt32 mask;
            public Int32 iItem;
            public Int32 iSubItem;
            public UInt32 state;
            public UInt32 stateMask;
            public String pszText;
            public Int32 cchTextMax;
            public Int32 iImage;
            public IntPtr lParam;
        }

        private int[,] bEnableEdit = new int[256,2];

        public const Int32 LVM_FIRST = 0x1000;
        public const Int32 LVM_GETITEM = LVM_FIRST + 5;
        public const Int32 LVM_SETITEM = LVM_FIRST + 6;
        public const Int32 LVIF_TEXT = 0x0001;
        public const Int32 LVIF_IMAGE = 0x0002;

        public const int LVW_FIRST = 0x1000;
        public const int LVM_GETEXTENDEDLISTVIEWSTYLE = LVW_FIRST + 54;

        public const int LVS_EX_GRIDLINES = 0x00000001;
        public const int LVS_EX_SUBITEMIMAGES = 0x00000002;
        public const int LVS_EX_CHECKBOXES = 0x00000004;
        public const int LVS_EX_TRACKSELECT = 0x00000008;
        public const int LVS_EX_HEADERDRAGDROP = 0x00000010;
        public const int LVS_EX_FULLROWSELECT = 0x00000020; // applies to report mode only
        public const int LVS_EX_ONECLICKACTIVATE = 0x00000040;

        private TextBox m_TextBox;
        private ComboBox m_ComboBox;

        private List<CComboxStringList> m_ColumnList = new List<CComboxStringList>();
        private List<CComboxEditList> m_ModeIndexList = new List<CComboxEditList>();

        private bool bPreCheck = false;

        public delegate void ProcessDelegate(int nIndex, object pDataObject);
        public ProcessDelegate theProcessFun = null;

        public MyListView()
        {
            InitializeComponent();
            IniSetting();
        }
        public MyListView(IContainer container)  
        {  
            container.Add(this);  
            InitializeComponent();
            IniSetting();
        }
        private void SetChangedData(int nIndex,object pData,ProcessDelegate pChangedFun)
        {
            if (pChangedFun == null) return;
            pChangedFun(nIndex, pData);
        }
        private void m_lstb_SelectChanged(object sender, EventArgs e)
        {
            if ((sender as ComboBox) != null)
            {
                if ((sender as ComboBox).Tag is ListViewItem.ListViewSubItem)
                {
                    //(this.m_ComboBox.Tag as ListViewItem.ListViewSubItem).Text = this.m_ComboBox.Text;
                    int nIndex = (int)(this.m_ComboBox.Tag as ListViewItem.ListViewSubItem).Tag;
                    SetChangedData(nIndex, this.m_ComboBox.Text, theProcessFun);
                }
            }
        }

        private void IniSetting()
        {
            //防止刷新闪烁
            SetStyle(ControlStyles.DoubleBuffer |
                    ControlStyles.OptimizedDoubleBuffer |
                    ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();

            //添加可编辑单元格
            m_TextBox = new TextBox();
            m_ComboBox = new ComboBox();
            //m_CheckBox = new CheckBox();
            m_TextBox.Multiline = false;
            m_TextBox.Visible = false;
            m_ComboBox.Visible = false;
            
            m_ComboBox.FlatStyle = FlatStyle.Popup;
            m_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.OwnerDraw = true;
            this.View = View.Details;
            this.GridLines = true;
            this.FullRowSelect = true;
            this.Controls.Add(m_TextBox);
            this.Controls.Add(m_ComboBox);
            //指定列显示图标
            System.Windows.Forms.Message m = new Message();
            m.HWnd = this.Handle;
            m.Msg = LVM_GETEXTENDEDLISTVIEWSTYLE;
            m.LParam = (IntPtr)(LVS_EX_GRIDLINES |
                                LVS_EX_FULLROWSELECT |
                                LVS_EX_SUBITEMIMAGES |
                                LVS_EX_CHECKBOXES |
                                LVS_EX_TRACKSELECT);
            m.WParam = IntPtr.Zero;
            this.WndProc(ref m);
        }
        public void ClearAllItem(bool bClearHead = true)
        {
            this.Items.Clear();
            if (bClearHead) this.Columns.Clear();
            m_ModeIndexList.Clear();
            m_ColumnList.Clear();
        }
        public void SetFreshColor(int nRowNo)
        {
            if (nRowNo % 2 == 0)
            {
                for (int j = 0; j < this.Columns.Count; j++)
                {
                    this.Items[nRowNo].SubItems[j].BackColor = Color.Lavender;
                }
            }
        }
        public void FreshRowColor()
        {
            for (int i = 0; i < this.Items.Count; i++)
            {
                SetFreshColor(i);
            }
        }
        public void RemoveAtItem(int nIndex)
        {
            if (nIndex < 0 || nIndex > this.Items.Count) return;
            Items.RemoveAt(nIndex);
            m_ModeIndexList.RemoveAt(nIndex);
            FreshRowColor();
        }
        public void AddItem(ListViewItem pItem)
        {
            this.Items.Add(pItem);
            SetFreshColor(this.Items.Count-1);
            CComboxEditList pNewMode = new CComboxEditList();
            pNewMode.m_IndexList = new int[this.Columns.Count,2];
            for(int i=0;i<this.Columns.Count;i++)
            {
                pNewMode.m_IndexList[i,0] = bEnableEdit[i,0];
                pNewMode.m_IndexList[i,1] = bEnableEdit[i,1];
            }
            m_ModeIndexList.Add(pNewMode);
        }
        //设置某列统一为编辑模式
        public void SetColumnsEditMode(int nIndex)
        {
            if (nIndex < 0) return;
            if (nIndex < bEnableEdit.Length) bEnableEdit[nIndex,0] = 1;
            for (int i = 0; i < this.Items.Count; i++)
            {
                if (nIndex < m_ModeIndexList[i].m_IndexList.Length) m_ModeIndexList[i].m_IndexList[nIndex,0] = 1;
            }
        }
        //设置某列统一为ComBox模式，统一的strList，及默认对应的字符数组序号
        public void SetColumnsComBoxMode(int nIndex, int ComIndex)
        {
            if (ComIndex < 0 || nIndex < 0) return;
            if (nIndex < bEnableEdit.Length) 
            {
                bEnableEdit[nIndex,0] = 2;
                bEnableEdit[nIndex,1] = ComIndex;
            }
            for (int i = 0; i < this.Items.Count; i++)
            {
                if (nIndex < m_ModeIndexList[i].m_IndexList.Length) 
                {
                    m_ModeIndexList[i].m_IndexList[nIndex,0] = 2;
                    m_ModeIndexList[i].m_IndexList[nIndex,1] = ComIndex;
                }
            }
        }

        public void SetCellMode(int Row, int nColumn,int nMode,int nStrList)
        {
            if (Row < 0 || nColumn < 0 || nMode < 0 || nStrList < 0) return;
            if (Row > m_ModeIndexList.Count) return;
            if (nColumn > this.Columns.Count) return;
            if (nStrList > m_ColumnList.Count && nMode == 2) return;
            m_ModeIndexList[Row].m_IndexList[nColumn, 0] = nMode;
            m_ModeIndexList[Row].m_IndexList[nColumn, 1] = nStrList;
        }
        //添加可使用的ComBoxList字符数组
        public int AddComBoxListStr(string[] strBoxList)
        {
            if (strBoxList.Length == 0) return 0;
            for (int i = 0; i < m_ColumnList.Count; i++)
            {
                CComboxStringList pExistList = m_ColumnList[i];
                if (pExistList.m_StrList.Length == strBoxList.Length)
                {
                    bool bExist = true;
                    for (int j = 0; j < pExistList.m_StrList.Length; j++)
                    {
                        if (pExistList.m_StrList[j] != strBoxList[j])
                        {
                            bExist = false;
                            break;
                        }
                    }
                    if (bExist) return i;
                }
            }
            CComboxStringList pNewList = new CComboxStringList();
            pNewList.m_StrList = new string[strBoxList.Length];
            Array.Copy(strBoxList, pNewList.m_StrList, strBoxList.Length);
            m_ColumnList.Add(pNewList);
            return m_ColumnList.Count-1;
        }

        [DllImport("user32.dll")]
        public static extern int SendMessage(
            IntPtr hWnd,	// handle to destination window 
            int Msg,		// message 
            IntPtr wParam,	// first message parameter 
            IntPtr lParam	// second message parameter 
            );

        [DllImport("user32.dll")]
        public static extern bool SendMessage(
            IntPtr hWnd,		// handle to destination window 
            Int32 msg,			// message 
            Int32 wParam,
            ref LV_ITEM lParam);// pointer to struct of LV_ITEM
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        private void EditItem(ListViewItem.ListViewSubItem subItem)
        {
            if (this.SelectedItems.Count <= 0)
            {
                return;
            }

            Rectangle _rect = subItem.Bounds;
            m_TextBox.Bounds = new Rectangle(_rect.Left + 2, _rect.Top + 1, _rect.Width - 3, _rect.Height - 3);//避免网格被遮掉
            m_TextBox.BorderStyle = BorderStyle.None;
            m_TextBox.BringToFront();
            
            m_TextBox.Text = subItem.Text;
            m_TextBox.Leave += new EventHandler(tb_Leave);
            m_TextBox.TextChanged += new EventHandler(m_TextBox_TextChanged);
            m_TextBox.Visible = true;
            m_TextBox.Tag = subItem;
            m_TextBox.Select();
        }

        private void EditComBoxItem(ListViewItem.ListViewSubItem subItem, Rectangle rt,int nStrListIndex)
        {
            if (this.SelectedItems.Count <= 0)
            {
                return;
            }

            Rectangle _rect = rt;
            m_ComboBox.Bounds = _rect;
            m_ComboBox.BringToFront();
            if (nStrListIndex < m_ColumnList.Count)
            {

            }
            m_ComboBox.Items.Clear();
            for (int i = 0;i<m_ColumnList[nStrListIndex].m_StrList.Length;i++)
            {
                m_ComboBox.Items.Add(m_ColumnList[nStrListIndex].m_StrList[i]);
            }
            m_ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
          //  m_ComboBox.Text = "";
            m_ComboBox.Text = subItem.Text;
            m_ComboBox.Leave += new EventHandler(lstb_Leave);
            m_ComboBox.TextChanged += new EventHandler(m_lstb_TextChanged);
            m_ComboBox.SelectedIndexChanged += new EventHandler(m_lstb_SelectChanged);

            m_ComboBox.Visible = true;
            m_ComboBox.Tag = subItem;
            m_ComboBox.Select();
          //  m_ComboBox.SelectedIndex = 0;
        }
        /*
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyValue == 0x203)
            {

                if (this.SelectedItems.Count > 0)
                {
                    //this.SelectedItems[0].BeginEdit();
            //        ListViewItem lvi = this.SelectedItems[0];
            //        EditItem(lvi.SubItems[0], new Rectangle(lvi.Bounds.Left, lvi.Bounds.Top, this.Columns[0].Width, lvi.Bounds.Height - 5));
                }
            }
            base.OnKeyDown(e);
        }*/

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            this.m_TextBox.Visible = false;
            this.m_ComboBox.Visible = false;
            base.OnSelectedIndexChanged(e);
        }

        protected override void OnItemCheck(ItemCheckEventArgs e)
        {
            Point tmpPoint = this.PointToClient(Cursor.Position);
            ListViewItem.ListViewSubItem subitem = this.HitTest(tmpPoint).SubItem;
            ListViewItem item = this.HitTest(tmpPoint).Item;
            if (subitem != null)
            {
                for (int i = 0; i < this.Columns.Count; i++)
                {
                    if (item.SubItems[i].Equals(subitem))// && bEnableEdit[i] == 1)
                    {
                        bPreCheck = item.Checked;
                        break;
                    }
                }
            }
            
        }
        protected override void OnDoubleClick(EventArgs e)
        {
            Point tmpPoint = this.PointToClient(Cursor.Position);
            ListViewItem.ListViewSubItem subitem = this.HitTest(tmpPoint).SubItem;
            ListViewItem item = this.HitTest(tmpPoint).Item;
            if (subitem != null && SelectedItems.Count>0)
            {
                try
                {
                    int nRow = this.SelectedItems[0].Index;
                    for (int i = 0; i < this.Columns.Count; i++)
                    {
                        if (item.SubItems[i].Equals(subitem))// && bEnableEdit[i] == 1)
                        {
                            subitem.Tag = i;
                            switch (m_ModeIndexList[nRow].m_IndexList[i, 0])
                            {
                                case 1:
                                    item.Checked = bPreCheck;
                                    EditItem(subitem);
                                    break;
                                case 2:
                                    item.Checked = bPreCheck;
                                    EditComBoxItem(subitem, subitem.Bounds, m_ModeIndexList[nRow].m_IndexList[i, 1]);
                                    break;
                            }
                            break;
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("选择出错");
                }
            }
            base.OnDoubleClick(e);

        }
        protected override void WndProc(ref   Message m)
        {
            if (m.Msg == 0x115 || m.Msg == 0x114)
            {
                this.m_TextBox.Visible = false;
            }
            base.WndProc(ref   m);
        }
        private void tb_Leave(object sender, EventArgs e)
        {
            m_TextBox.TextChanged -= new EventHandler(m_TextBox_TextChanged);
            (sender as TextBox).Visible = false;
        }

        private void m_TextBox_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Tag is ListViewItem.ListViewSubItem)
            {
                (this.m_TextBox.Tag as ListViewItem.ListViewSubItem).Text = this.m_TextBox.Text;
                int nIndex = (int)(this.m_TextBox.Tag as ListViewItem.ListViewSubItem).Tag;
                SetChangedData(nIndex, this.m_TextBox.Text,theProcessFun);
            }
        }
        private void lstb_Leave(object sender, EventArgs e)
        {
            m_ComboBox.TextChanged -= new EventHandler(m_lstb_TextChanged);
            if ((sender as ComboBox) != null)
            {
                if ((sender as ComboBox).Tag is ListViewItem.ListViewSubItem)
                {
                    (this.m_ComboBox.Tag as ListViewItem.ListViewSubItem).Text = this.m_ComboBox.Text;
                    int nIndex = (int)(this.m_ComboBox.Tag as ListViewItem.ListViewSubItem).Tag;
                    SetChangedData(nIndex, this.m_ComboBox.Text, theProcessFun);
                    theProcessFun = null;
                }
            }
        }
        private void m_lstb_TextChanged(object sender, EventArgs e)
        {
            if ((sender as ComboBox) != null)
            {
                if ((sender as ComboBox).Tag is ListViewItem.ListViewSubItem)
                {
                    (this.m_ComboBox.Tag as ListViewItem.ListViewSubItem).Text = this.m_ComboBox.Text;
                    int nIndex = (int)(this.m_ComboBox.Tag as ListViewItem.ListViewSubItem).Tag;
                    SetChangedData(nIndex, this.m_ComboBox.Text, theProcessFun);
                }
            }
        }

        private int _progressColumnIndex = -1;
        public int ProgressColumnIndex
        {
            get
            {
                return _progressColumnIndex;
            }
            set
            {
                _progressColumnIndex = value;
            }
        }
        private int _progressMaximun = 100;
        public int ProgressMaximun
        {
            get
            {
                return _progressMaximun;
            }
        }
        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
            base.OnDrawColumnHeader(e);
        }
        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            if (e.ColumnIndex == ProgressColumnIndex)
            {
                var item = e.Item.SubItems[ProgressColumnIndex];
                var rect = item.Bounds;

                //绘制进度条  
                var g = e.Graphics;
                var progressRect = new Rectangle(rect.X + 1, rect.Y + 3, rect.Width - 2, rect.Height - 5);
                g.DrawRectangle(new Pen(new SolidBrush(Color.Blue), 1), progressRect);

                //绘制进度  
                var progressMaxWidth = progressRect.Width - 1;
                var unit = (progressMaxWidth * 1.0) / (_progressMaximun * 100);
                var fValue = float.Parse(item.Text);
                var percent = fValue * unit * 100;
                if (percent >= progressMaxWidth) percent = progressMaxWidth;
                g.FillRectangle(new SolidBrush(Color.GreenYellow), new RectangleF(progressRect.X + 1, progressRect.Y + 1, float.Parse(percent.ToString()), progressRect.Height - 1));

                //绘制进度百分比  
                percent = fValue;
                var percentText = percent.ToString("F2");
                percentText += "%";
                if (fValue >= _progressMaximun) percentText = "已完成";
                var size = TextRenderer.MeasureText(percentText.ToString(), Font);
                var x = rect.X + (progressRect.Width - size.Width) / 2.0;
                var y = rect.Y + (progressRect.Height - size.Height) / 2.0 + 3;
                g.DrawString(percentText, this.Font, new SolidBrush(Color.Black), float.Parse(x.ToString()), float.Parse(y.ToString()));
            }
            else
            {
                e.DrawDefault = true;
            }
            base.OnDrawSubItem(e);
        }

        public void SetProgress(int itemIndex, int value)
        {
            var columnWidth = this.Columns[ProgressColumnIndex].Width;
            var progressSubItem = this.Items[itemIndex].SubItems[ProgressColumnIndex];
            progressSubItem.Text = value.ToString();
        }
        public int GetComboxSelectIndex(int nRow, int nColumn)
        {
            if (nRow < 0 || nColumn < 0) return -1;
            if(nRow >= m_ModeIndexList.Count) return -1;
            if(nColumn >= this.Columns.Count) return -1;
            if (m_ModeIndexList[nRow].m_IndexList[nColumn, 0] == 2)
            {
                CComboxStringList pStrList = m_ColumnList[m_ModeIndexList[nRow].m_IndexList[nColumn, 1]];
                for (int i = 0; i < pStrList.m_StrList.Length; i++)
                {
                    if (this.Items[nRow].SubItems[nColumn].Text == pStrList.m_StrList[i]) return i;
                }
            }
            return -1;
                //m_ColumnList 
        }
    }
    public class CComboxStringList
    {
        public string[] m_StrList;
    }
    public class CComboxEditList
    {
        public int[,] m_IndexList;
    }
}
