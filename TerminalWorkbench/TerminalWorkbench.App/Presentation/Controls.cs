using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using W = System.Windows.Controls;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace TerminalWorkbench.Presentation
{
    // Presentation state used by the migrated operation controllers. Rendering
    // lives in WpfViews; none of these types uses or hosts a WinForms control.
    public class Control : INotifyPropertyChanged, IDisposable
    {
        private string text = string.Empty;
        private bool visible = true;
        private bool enabled = true;
        private Point location;
        private Size size = new Size(100, 28);
        private DockStyle dock;
        private System.Windows.FrameworkElement element;
        private static int nextId;

        public Control()
        {
            Controls = new ControlCollection(this);
            Handle = new IntPtr(Interlocked.Increment(ref nextId));
            UiMessages.Register(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler Click;
        public event EventHandler DoubleClick;
        public event EventHandler TextChanged;
        public event EventHandler Validated;
        public event EventHandler Leave;
        public event MouseEventHandler MouseDown;
        public event MouseEventHandler MouseMove;
        public event MouseEventHandler MouseUp;
        public ControlCollection Controls
        {
            get;
        }
        public Control Parent
        {
            get; internal set;
        }
        public IntPtr Handle
        {
            get;
        }
        public string Name { get; set; } = string.Empty;
        public object Tag
        {
            get; set;
        }
        public bool IsDisposed
        {
            get; private set;
        }
        public ContextMenuStrip ContextMenuStrip
        {
            get; set;
        }
        public Font Font
        {
            get; set;
        }
        private Color backColor = Color.White;
        public Color BackColor
        {
            get => backColor; set => Set(ref backColor, value);
        }
        public Color ForeColor { get; set; } = Color.FromArgb(25, 53, 73);
        public Image BackgroundImage
        {
            get; set;
        }
        public ImageLayout BackgroundImageLayout
        {
            get; set;
        }
        public object Cursor
        {
            get; set;
        }
        public bool AutoSize
        {
            get; set;
        }
        public int TabIndex
        {
            get; set;
        }
        public ContentAlignment TextAlign
        {
            get; set;
        }

        public string Text
        {
            get => text;
            set
            {
                if (Set(ref text, value ?? string.Empty))
                    TextChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public bool Visible
        {
            get => visible; set => Set(ref visible, value);
        }
        public bool Enabled
        {
            get => enabled; set => Set(ref enabled, value);
        }
        public Point Location
        {
            get => location; set
            {
                location = value;
                Changed();
            }
        }
        public Size Size
        {
            get => size; set
            {
                size = value;
                Changed();
            }
        }
        public int Left
        {
            get => location.X; set => Location = new Point(value, Top);
        }
        public int Top
        {
            get => location.Y; set => Location = new Point(Left, value);
        }
        public int Width
        {
            get => size.Width; set => Size = new Size(value, Height);
        }
        public int Height
        {
            get => size.Height; set => Size = new Size(Width, value);
        }
        public int Right => Left + Width;
        public int Bottom => Top + Height;
        public DockStyle Dock
        {
            get => dock; set => Set(ref dock, value);
        }
        public System.Windows.FrameworkElement Element => element ??= WpfViews.Create(this);
        internal System.Windows.FrameworkElement CreatedElement => element;

        protected bool Set<T>(ref T storage, T value, [CallerMemberName] string name = null)
        {
            if (IsDisposed)
                return false;
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;
            storage = value;
            Changed(name);
            return true;
        }

        internal void Changed([CallerMemberName] string name = null)
        {
            if (!IsDisposed)
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public void PerformClick() => Click?.Invoke(this, EventArgs.Empty);
        internal void PerformDoubleClick() => DoubleClick?.Invoke(this, EventArgs.Empty);
        internal void Validate()
        {
            Validated?.Invoke(this, EventArgs.Empty);
            Leave?.Invoke(this, EventArgs.Empty);
        }
        internal void RaiseMouse(int kind, MouseEventArgs args)
        {
            if (kind == 0)
                MouseDown?.Invoke(this, args);
            else if (kind == 1)
                MouseMove?.Invoke(this, args);
            else
                MouseUp?.Invoke(this, args);
        }

        public void Focus() => Element.Focus();
        public void Select() => Focus();
        public void BringToFront() => System.Windows.Controls.Panel.SetZIndex(Element, 1);
        public void SuspendLayout()
        {
        }
        public void ResumeLayout(bool performLayout = true)
        {
        }
        public object Invoke(Delegate action) => System.Windows.Application.Current.Dispatcher.Invoke(action);
        public void BeginInvoke(Delegate action) => System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
        {
            if (!IsDisposed)
                action.DynamicInvoke();
        }));
        public virtual void Invalidate() => Element.InvalidateVisual();
        protected virtual void OnMouseDown(MouseEventArgs e) => MouseDown?.Invoke(this, e);

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;
            IsDisposed = true;
            UiMessages.Unregister(this);
            WpfViews.Release(this);
            foreach (Control child in Controls.ToArray())
                child.Dispose();
            BackgroundImage?.Dispose();
            Font?.Dispose();
        }

        public void Dispose() => Dispose(true);

        public sealed class ControlCollection : ObservableCollection<Control>
        {
            private readonly Control owner;
            public Dictionary<Control, Point> Cells { get; } = new Dictionary<Control, Point>();
            public ControlCollection(Control owner)
            {
                this.owner = owner;
            }
            protected override void InsertItem(int index, Control item)
            {
                if (item.Parent != null && item.Parent != owner)
                    item.Parent.Controls.Remove(item);
                item.Parent = owner;
                base.InsertItem(index, item);
            }
            protected override void RemoveItem(int index)
            {
                this[index].Parent = null;
                base.RemoveItem(index);
            }
            protected override void ClearItems()
            {
                foreach (Control item in this.ToArray())
                {
                    if (item is Form form)
                        form.Close();
                    item.Parent = null;
                }
                base.ClearItems();
            }
            public void Add(Control item, int column, int row)
            {
                Cells[item] = new Point(column, row);
                Add(item);
            }
            public void AddRange(Control[] values)
            {
                foreach (Control value in values)
                    Add(value);
            }
        }
    }

    public class Panel : Control
    {
    }
    public class MyPanel : Panel
    {
    }
    public class GroupBox : Panel
    {
    }
    public class TabPage : Panel
    {
    }
    public class SplitterPanel : Panel
    {
    }
    public class Label : Control
    {
    }
    public class Button : Control
    {
    }
    public class IpBox : TextBox
    {
    }
    public class MaskedTextBox : TextBox
    {
    }
    public class TextBox : Control
    {
        public bool ReadOnly
        {
            get; set;
        }
        public bool Multiline
        {
            get; set;
        }
        public char PasswordChar
        {
            get; set;
        }
        public int MaxLength
        {
            get; set;
        }
        public virtual void AppendText(string value) => Text += value;
        public void Clear() => Text = string.Empty;
        public void SelectAll()
        {
            if (Element is W.TextBox box)
                box.SelectAll();
        }
    }
    public class RichTextBox : TextBox
    {
        public Color SelectionColor
        {
            get; set;
        }
        internal bool IsAppending
        {
            get; private set;
        }
        internal event EventHandler<TextAppendEventArgs> Appended;
        public RichTextBox()
        {
            Multiline = true;
        }
        public override void AppendText(string value)
        {
            IsAppending = true;
            try
            {
                base.AppendText(value);
            }
            finally { IsAppending = false; }
            Appended?.Invoke(this, new TextAppendEventArgs(value, SelectionColor));
        }
    }
    internal sealed class TextAppendEventArgs : EventArgs
    {
        internal string Text
        {
            get;
        }
        internal Color Color
        {
            get;
        }
        internal TextAppendEventArgs(string text, Color color)
        {
            Text = text;
            Color = color;
        }
    }
    public class CheckBox : Control
    {
        private bool isChecked;
        public event EventHandler CheckedChanged;
        public bool Checked
        {
            get => isChecked;
            set
            {
                if (Set(ref isChecked, value))
                    CheckedChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public class RadioButton : CheckBox
    {
    }
    public class ComboBox : Control
    {
        private int selectedIndex = -1;
        public event EventHandler SelectedIndexChanged;
        public ObservableCollection<object> Items { get; } = new ObservableCollection<object>();
        public ComboBoxStyle DropDownStyle
        {
            get; set;
        }
        public int MaxDropDownItems
        {
            get; set;
        }
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                if (!Set(ref selectedIndex, value))
                    return;
                if (value >= 0 && value < Items.Count)
                    Text = Convert.ToString(Items[value]);
                SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public object SelectedItem
        {
            get => selectedIndex >= 0 && selectedIndex < Items.Count ? Items[selectedIndex] : null;
            set => SelectedIndex = Items.IndexOf(value);
        }
    }
    public class ToolStripItem : Control
    {
    }
    public class ToolStripItemClickedEventArgs : EventArgs
    {
        public ToolStripItem ClickedItem
        {
            get; set;
        }
    }
    public class ToolStripButton : ToolStripItem
    {
        public bool CheckOnClick
        {
            get; set;
        }
        public bool Checked
        {
            get; set;
        }
    }
    public class ToolStripLabel : ToolStripItem
    {
    }
    public class ToolStripSeparator : ToolStripItem
    {
    }
    public class ToolStripComboBox : ComboBox
    {
    }
    public class ToolStripTextBox : TextBox
    {
    }
    public class ToolStripStatusLabel : ToolStripLabel
    {
    }
    public class ToolStrip : Panel
    {
        public ToolStrip()
        {
            Dock = DockStyle.Top;
        }
        public ControlCollection Items => Controls;
    }
    public class ToolStripMenuItem : ToolStripItem
    {
        public ControlCollection DropDownItems => Controls;
    }
    public class MenuStrip : ToolStrip
    {
    }
    public class ContextMenuStrip : ToolStrip
    {
    }
    public class TableLayoutPanel : Panel
    {
        public int ColumnCount { get; set; } = 1;
        public int RowCount { get; set; } = 1;
        public List<System.Windows.GridLength> ColumnWidths { get; } = new List<System.Windows.GridLength>();
        public List<System.Windows.GridLength> RowHeights { get; } = new List<System.Windows.GridLength>();
    }
    public class SplitContainer : Panel
    {
        private bool panel1Collapsed;
        private bool panel2Collapsed;
        public SplitterPanel Panel1 { get; } = new SplitterPanel();
        public SplitterPanel Panel2 { get; } = new SplitterPanel();
        public int SplitterDistance { get; set; } = 220;
        public Orientation Orientation
        {
            get; set;
        }
        public bool Panel1Collapsed
        {
            get => panel1Collapsed; set => Set(ref panel1Collapsed, value);
        }
        public bool Panel2Collapsed
        {
            get => panel2Collapsed; set => Set(ref panel2Collapsed, value);
        }
        protected override void Dispose(bool disposing)
        {
            Panel1.Dispose();
            Panel2.Dispose();
            base.Dispose(disposing);
        }
    }
    public class CNormalTabControl : Panel
    {
        private int selectedIndex;
        public event EventHandler SelectedIndexChanged;
        public ControlCollection TabPages => Controls;
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                if (Set(ref selectedIndex, value))
                    SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public class MyTabControl : CNormalTabControl
    {
    }
    public class ProgressBar : Control
    {
        private int value;
        private int minimum;
        private int maximum = 100;
        public int Minimum
        {
            get => minimum; set => Set(ref minimum, value);
        }
        public int Maximum
        {
            get => maximum; set => Set(ref maximum, value);
        }
        public int Step { get; set; } = 10;
        public int Value
        {
            get => value; set => Set(ref this.value, value);
        }
        public void PerformStep() => Value = Math.Min(Maximum, Value + Step);
    }
    public class DateTimePicker : Control
    {
        private DateTime value = DateTime.Now;
        public DateTime Value
        {
            get => value; set => Set(ref this.value, value);
        }
        public DateTimePickerFormat Format
        {
            get; set;
        }
        public string CustomFormat
        {
            get; set;
        }
    }
    public class ImageList
    {
    }
    public class BindingSource
    {
    }
    public class ToolTip
    {
        public void SetToolTip(Control control, string text) => control.Element.ToolTip = text;
    }
    public class DockPanel : Panel
    {
        public DocumentStyle DocumentStyle
        {
            get; set;
        }
    }
    public class DockContent : Form
    {
        private DockPanel dockOwner;
        public void Show(DockPanel panel)
        {
            ReleaseWindowForDocking();
            dockOwner = panel;
            TopLevel = false;
            if (!panel.Controls.Contains(this))
                panel.Controls.Add(this);
            Show();
        }
        public void Float()
        {
            if (dockOwner == null)
                return;
            Parent?.Controls.Remove(this);
            TopLevel = true;
            Window window = GetWindow();
            var menu = new W.ContextMenu();
            var dock = new W.MenuItem { Header = "停靠到主窗口" };
            dock.Click += (_, _) => Show(dockOwner);
            menu.Items.Add(dock);
            window.ContextMenu = menu;
            window.Show();
        }
    }

    public delegate void MethodInvoker();
    public delegate void MouseEventHandler(object sender, MouseEventArgs e);
    public class MouseEventArgs : EventArgs
    {
        public int X
        {
            get; set;
        }
        public int Y
        {
            get; set;
        }
        public Point Location => new Point(X, Y);
        public MouseButtons Button
        {
            get; set;
        }
    }
    public enum DockStyle
    {
        None, Top, Bottom, Left, Right, Fill
    }
    public enum Orientation
    {
        Vertical, Horizontal
    }
    public enum ImageLayout
    {
        None, Tile, Center, Stretch, Zoom
    }
    public enum ComboBoxStyle
    {
        Simple, DropDown, DropDownList
    }
    public enum DateTimePickerFormat
    {
        Long, Short, Time, Custom
    }
    public enum MouseButtons
    {
        None, Left, Right, Middle
    }
    public enum DocumentStyle
    {
        DockingWindow
    }
    public static class Cursors
    {
        public static object Default => System.Windows.Input.Cursors.Arrow;
        public static object WaitCursor => System.Windows.Input.Cursors.Wait;
        public static object SizeWE => System.Windows.Input.Cursors.SizeWE;
    }
    public static class TextEncoding
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern uint GetACP();
        public static System.Text.Encoding Ansi { get; } = CreateAnsi();
        private static System.Text.Encoding CreateAnsi()
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            return System.Text.Encoding.GetEncoding((int)GetACP());
        }
    }
}
