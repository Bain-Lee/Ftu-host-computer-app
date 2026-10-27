using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;

namespace TerminalWorkbench.Presentation
{
    public class ListViewItem : INotifyPropertyChanged
    {
        private bool selected;
        private bool isChecked;
        internal ListView Owner
        {
            get; set;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public SubItemCollection SubItems { get; } = new SubItemCollection();
        public object Tag
        {
            get; set;
        }
        public int Index => Owner?.Items.IndexOf(this) ?? -1;
        public int ImageIndex
        {
            get; set;
        }
        public Color BackColor
        {
            get; set;
        }
        public Color ForeColor
        {
            get; set;
        }
        public bool UseItemStyleForSubItems
        {
            get; set;
        }
        public string Text
        {
            get => SubItems[0].Text; set => SubItems[0].Text = value;
        }
        public bool Checked
        {
            get => isChecked;
            set
            {
                if (isChecked == value)
                    return;
                isChecked = value;
                Changed();
            }
        }
        public bool Selected
        {
            get => selected;
            set
            {
                if (selected == value)
                    return;
                selected = value;
                Changed();
                Owner?.SelectionChanged(this);
            }
        }
        public ListViewItem() : this(string.Empty)
        {
        }
        public ListViewItem(string text) : this(new[] { text })
        {
        }
        public ListViewItem(string[] text)
        {
            foreach (string value in text)
                SubItems.Add(value);
            if (SubItems.Count == 0)
                SubItems.Add(string.Empty);
        }
        public void EnsureVisible() => Owner?.ScrollTo(this);
        private void Changed([CallerMemberName] string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public class ListViewSubItem : INotifyPropertyChanged
        {
            private string text = string.Empty;
            private int imageIndex = -1;
            public event PropertyChangedEventHandler PropertyChanged;
            public object Tag
            {
                get; set;
            }
            public Color BackColor
            {
                get; set;
            }
            public Color ForeColor
            {
                get; set;
            }
            public string Text
            {
                get => text;
                set
                {
                    if (text == value)
                        return;
                    text = value ?? string.Empty;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
                }
            }
            public int ImageIndex
            {
                get => imageIndex;
                set
                {
                    if (imageIndex == value)
                        return;
                    imageIndex = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ImageIndex)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StatusBrush)));
                }
            }
            public System.Windows.Media.Brush StatusBrush => ImageIndex == 1 ? System.Windows.Media.Brushes.SeaGreen :
                ImageIndex == 2 ? System.Windows.Media.Brushes.Firebrick : System.Windows.Media.Brushes.Transparent;
        }
        public sealed class SubItemCollection : ObservableCollection<ListViewSubItem>
        {
            public ListViewSubItem Add(string value)
            {
                var cell = new ListViewSubItem { Text = value, Tag = Count };
                Add(cell);
                return cell;
            }
            public void AddRange(string[] values)
            {
                foreach (string value in values)
                    Add(value);
            }
        }
    }

    public class ColumnHeader
    {
        public string Text
        {
            get; set;
        }
        public int Width { get; set; } = 120;
        public HorizontalAlignment TextAlign
        {
            get; set;
        }
    }
    public class ColumnCollection : ObservableCollection<ColumnHeader>
    {
        public ColumnHeader Add(string text, int width = 120, HorizontalAlignment alignment = HorizontalAlignment.Left)
        {
            var column = new ColumnHeader { Text = text, Width = width, TextAlign = alignment };
            Add(column);
            return column;
        }
    }
    public class ListView : Control
    {
        private bool checkBoxes;
        public event EventHandler SelectedIndexChanged;
        public ListItemCollection Items
        {
            get;
        }
        public ColumnCollection Columns { get; } = new ColumnCollection();
        public List<ListViewItem> SelectedItems => Items.Where(item => item.Selected).ToList();
        public List<int> SelectedIndices => Items.Where(item => item.Selected).Select(item => item.Index).ToList();
        public List<ListViewItem> CheckedItems => Items.Where(item => item.Checked).ToList();
        public bool GridLines
        {
            get; set;
        }
        public bool FullRowSelect
        {
            get; set;
        }
        public bool Scrollable
        {
            get; set;
        }
        public bool MultiSelect
        {
            get; set;
        }
        public bool HideSelection
        {
            get; set;
        }
        public ColumnHeaderStyle HeaderStyle
        {
            get; set;
        }
        public View View
        {
            get; set;
        }
        public ImageList SmallImageList
        {
            get; set;
        }
        public bool CheckBoxes
        {
            get => checkBoxes; set => Set(ref checkBoxes, value);
        }
        public ListViewItem TopItem
        {
            get => WpfViews.FirstVisible(this) ?? Items.FirstOrDefault();
            set
            {
                if (value != null)
                    ScrollTo(value);
            }
        }
        public ListView()
        {
            Items = new ListItemCollection(this);
        }
        public void Clear()
        {
            Items.Clear();
            Columns.Clear();
        }
        public void BeginUpdate() => Items.BeginUpdate();
        public void EndUpdate() => Items.EndUpdate();
        internal void ScrollTo(ListViewItem item) => WpfViews.ScrollTo(this, item);
        internal void SelectionChanged(ListViewItem changed)
        {
            if (changed.Selected && !MultiSelect)
            {
                foreach (ListViewItem item in Items.Where(item => item != changed && item.Selected).ToArray())
                    item.Selected = false;
            }
            WpfViews.SynchronizeSelection(this);
            SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
        }
        public sealed class ListItemCollection : ObservableCollection<ListViewItem>
        {
            private readonly ListView owner;
            private int updateDepth;
            private bool collectionChanged;
            public ListItemCollection(ListView owner)
            {
                this.owner = owner;
            }
            protected override void InsertItem(int index, ListViewItem item)
            {
                item.Owner = owner;
                base.InsertItem(index, item);
            }
            public ListViewItem Add(string text)
            {
                var item = new ListViewItem(text);
                Add(item);
                return item;
            }
            public void AddRange(ListViewItem[] items)
            {
                foreach (ListViewItem item in items)
                    Add(item);
            }
            internal void BeginUpdate() => updateDepth++;
            internal void EndUpdate()
            {
                if (updateDepth == 0 || --updateDepth != 0 || !collectionChanged)
                    return;
                collectionChanged = false;
                base.OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
                base.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                base.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
            protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
            {
                if (updateDepth > 0)
                    collectionChanged = true;
                else
                    base.OnCollectionChanged(e);
            }
            protected override void OnPropertyChanged(PropertyChangedEventArgs e)
            {
                if (updateDepth == 0)
                    base.OnPropertyChanged(e);
            }
        }
    }

    public class MyListView : ListView
    {
        public delegate void ProcessDelegate(int nIndex, object pDataObject);
        public ProcessDelegate theProcessFun;
        private readonly List<string[]> choices = new List<string[]>();
        private readonly Dictionary<int, (int Mode, int Choice)> columnModes = new Dictionary<int, (int, int)>();
        private readonly Dictionary<(ListViewItem Row, int Column), (int Mode, int Choice)> cellModes = new Dictionary<(ListViewItem, int), (int, int)>();
        public int ProgressColumnIndex { get; set; } = -1;
        public void ClearAllItem(bool clearHead = true)
        {
            Items.Clear();
            if (clearHead)
                Columns.Clear();
            cellModes.Clear();
            choices.Clear();
        }
        public void AddItem(ListViewItem item) => Items.Add(item);
        public void RemoveAtItem(int index) => Items.RemoveAt(index);
        public void SetColumnsEditMode(int column) => columnModes[column] = (1, 0);
        public void SetColumnsComBoxMode(int column, int list) => columnModes[column] = (2, list);
        public int AddComBoxListStr(string[] values)
        {
            int index = choices.FindIndex(list => list.SequenceEqual(values));
            if (index >= 0)
                return index;
            choices.Add((string[])values.Clone());
            return choices.Count - 1;
        }
        public void SetCellMode(int row, int column, int mode, int list) => cellModes[(Items[row], column)] = (mode, list);
        internal (int Mode, string[] Choices) GetEditMode(ListViewItem row, int column)
        {
            if (!cellModes.TryGetValue((row, column), out var mode))
                columnModes.TryGetValue(column, out mode);
            return (mode.Mode, mode.Mode == 2 && mode.Choice < choices.Count ? choices[mode.Choice] : Array.Empty<string>());
        }
        internal void CommitEdit(ListViewItem row, int column, string value)
        {
            row.SubItems[column].Text = value;
            theProcessFun?.Invoke(column, value);
        }

        public const int LVIF_TEXT = 1;
        public const int LVIF_IMAGE = 2;
        public const int LVM_SETITEM = 0x104C;
        public const int LVM_GETEXTENDEDLISTVIEWSTYLE = 0x1036;
        public const int LVS_EX_SUBITEMIMAGES = 2;
        public struct LV_ITEM
        {
            public int iItem, iSubItem, mask, iImage;
            public string pszText;
        }
        public static int SendMessage(IntPtr handle, int message, int parameter, ref LV_ITEM item)
        {
            if (UiMessages.Find(handle) is not ListView table || item.iItem < 0 || item.iItem >= table.Items.Count)
                return 0;
            var cells = table.Items[item.iItem].SubItems;
            if (item.iSubItem < 0 || item.iSubItem >= cells.Count)
                return 0;
            if ((item.mask & LVIF_TEXT) != 0 && item.pszText != null)
                cells[item.iSubItem].Text = item.pszText;
            cells[item.iSubItem].ImageIndex = item.iImage;
            return 1;
        }
        public static int SendMessage(IntPtr handle, int message, IntPtr mask, IntPtr value) => 1;
    }

    public class TreeNode : INotifyPropertyChanged
    {
        private string text = string.Empty;
        private bool expanded;
        private bool selected;
        public event PropertyChangedEventHandler PropertyChanged;
        public TreeNodeCollection Nodes
        {
            get;
        }
        public TreeNode Parent
        {
            get; internal set;
        }
        internal TreeNodeCollection Collection
        {
            get; set;
        }
        public object Tag
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
        public int Index => Collection?.IndexOf(this) ?? -1;
        public int Level => Parent == null ? 0 : Parent.Level + 1;
        public Rectangle Bounds
        {
            get; set;
        }
        public bool Checked
        {
            get; set;
        }
        public string Text
        {
            get => text;
            set
            {
                text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
            }
        }
        public bool IsExpanded
        {
            get => expanded;
            set
            {
                expanded = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsExpanded)));
            }
        }
        public bool IsSelected
        {
            get => selected;
            set
            {
                selected = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected)));
            }
        }
        public TreeNode()
        {
            Nodes = new TreeNodeCollection(this);
        }
        public TreeNode(string text) : this()
        {
            Text = text;
        }
        public void Expand() => IsExpanded = true;
        public void ExpandAll()
        {
            Expand();
            foreach (TreeNode node in Nodes)
                node.ExpandAll();
        }
        public void Remove() => Collection?.Remove(this);
        public override string ToString() => Text;
    }
    public class TreeNodeCollection : ObservableCollection<TreeNode>
    {
        private readonly TreeNode parent;
        public TreeNodeCollection(TreeNode parent)
        {
            this.parent = parent;
        }
        protected override void InsertItem(int index, TreeNode item)
        {
            item.Parent = parent;
            item.Collection = this;
            base.InsertItem(index, item);
        }
        public TreeNode Add(string text)
        {
            var node = new TreeNode(text);
            Add(node);
            return node;
        }
        public void AddRange(TreeNode[] items)
        {
            foreach (TreeNode node in items)
                Add(node);
        }
    }
    public class TreeView : Control
    {
        private TreeNode selectedNode;
        public TreeNodeCollection Nodes { get; } = new TreeNodeCollection(null);
        public event TreeViewEventHandler AfterSelect;
        public bool HideSelection
        {
            get; set;
        }
        public TreeViewDrawMode DrawMode
        {
            get; set;
        }
        public TreeNode TopNode
        {
            get => Nodes.FirstOrDefault();
        }
        public TreeNode SelectedNode
        {
            get => selectedNode;
            set
            {
                if (selectedNode == value)
                    return;
                if (selectedNode != null)
                    selectedNode.IsSelected = false;
                if (Set(ref selectedNode, value))
                {
                    if (value != null)
                        value.IsSelected = true;
                    AfterSelect?.Invoke(this, new TreeViewEventArgs { Node = value });
                }
            }
        }
        public void ExpandAll()
        {
            foreach (TreeNode node in Nodes)
                node.ExpandAll();
        }
    }
    public class TreeViewEventArgs : EventArgs
    {
        public TreeNode Node
        {
            get; set;
        }
    }
    public delegate void TreeViewEventHandler(object sender, TreeViewEventArgs e);
    public enum TreeViewDrawMode
    {
        Normal, OwnerDrawText, OwnerDrawAll
    }
    public enum ColumnHeaderStyle
    {
        None, Nonclickable, Clickable
    }
    public enum View
    {
        Details, LargeIcon, SmallIcon, List, Tile
    }
    public enum HorizontalAlignment
    {
        Left, Right, Center
    }

    public static class CollectionExtensions
    {
        public static void AddRange<T>(this ICollection<T> items, IEnumerable<T> values)
        {
            foreach (T value in values)
                items.Add(value);
        }
    }
}
