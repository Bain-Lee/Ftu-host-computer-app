using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace TerminalWorkbench.Presentation
{
    public class DataGridView : Control
    {
        private DataTable table;
        private DataGridViewCell currentCell;
        public event EventHandler CurrentCellChanged;
        public event ScrollEventHandler Scroll;
        public DataGridColumns Columns { get; } = new DataGridColumns();
        public ObservableCollection<DataGridViewRow> Rows { get; } = new ObservableCollection<DataGridViewRow>();
        public bool ReadOnly
        {
            get; set;
        }
        public int RowCount => Rows.Count;
        public int ColumnCount => Columns.Count;
        public bool RowHeadersVisible
        {
            get; set;
        }
        public bool AllowUserToAddRows
        {
            get; set;
        }
        public bool MultiSelect
        {
            get; set;
        }
        public DataGridViewAutoSizeRowsMode AutoSizeRowsMode
        {
            get; set;
        }
        public DataGridViewAutoSizeColumnsMode AutoSizeColumnsMode
        {
            get; set;
        }
        public DataGridViewSelectionMode SelectionMode
        {
            get; set;
        }
        public DataGridViewEditMode EditMode
        {
            get; set;
        }
        public DataGridViewCellStyle RowsDefaultCellStyle { get; } = new DataGridViewCellStyle();
        public DataGridViewCellStyle ColumnHeadersDefaultCellStyle { get; } = new DataGridViewCellStyle();
        public RowTemplate RowTemplate { get; } = new RowTemplate();
        public List<DataGridViewCell> SelectedCells => CurrentCell == null ? new List<DataGridViewCell>() : new List<DataGridViewCell> { CurrentCell };
        public bool EndEdit() => WpfViews.EndEditorEdit(this);
        public object DataSource
        {
            get => table;
            set
            {
                table = (DataTable)value;
                RebuildRows();
                Columns.Clear();
                if (table != null)
                {
                    foreach (DataColumn column in table.Columns)
                        Columns.Add(new DataGridViewColumn { HeaderText = column.ColumnName, DataPropertyName = column.ColumnName });
                    table.RowChanged += TableChanged;
                    table.RowDeleted += TableChanged;
                    table.TableCleared += (_, _) => RebuildRows();
                }
                Changed();
            }
        }
        private void TableChanged(object sender, DataRowChangeEventArgs e)
        {
            if (Rows.Count != table.Rows.Count)
                RebuildRows();
        }
        private void RebuildRows()
        {
            Rows.Clear();
            if (table == null)
                return;
            for (int row = 0; row < table.Rows.Count; row++)
                Rows.Add(new DataGridViewRow(table.Rows[row], row));
        }
        public DataGridViewCell CurrentCell
        {
            get => currentCell;
            set
            {
                currentCell = value;
                CurrentCellChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public DataGridViewRow CurrentRow => currentCell == null || currentCell.RowIndex >= Rows.Count ? null : Rows[currentCell.RowIndex];
        public Rectangle GetCellDisplayRectangle(int column, int row, bool cutOverflow) => WpfViews.CellRectangle(this, column, row);
        internal void RaiseScroll() => Scroll?.Invoke(this, new ScrollEventArgs());
    }
    public class DataGridViewRow
    {
        public int Index
        {
            get;
        }
        public CellCollection Cells
        {
            get;
        }
        public DataGridViewRow(DataRow row, int index)
        {
            Index = index;
            Cells = new CellCollection(row);
            for (int column = 0; column < row.Table.Columns.Count; column++)
                Cells.Add(new DataGridViewCell(row, index, column));
        }
    }
    public class DataGridViewCell
    {
        private readonly DataRow row;
        public int RowIndex
        {
            get;
        }
        public int ColumnIndex
        {
            get;
        }
        public object Tag
        {
            get; set;
        }
        public DataGridViewCellStyle Style { get; } = new DataGridViewCellStyle();
        public bool ReadOnly
        {
            get; set;
        }
        public object Value
        {
            get => row[ColumnIndex]; set => row[ColumnIndex] = value ?? DBNull.Value;
        }
        public object FormattedValue => Value;
        public DataGridViewCell(DataRow row, int rowIndex, int columnIndex)
        {
            this.row = row;
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
        }
    }
    public class DataGridViewColumn : INotifyPropertyChanged
    {
        private bool readOnly;
        private int width = 140;
        public event PropertyChangedEventHandler PropertyChanged;
        public string HeaderText
        {
            get; set;
        }
        public string Name
        {
            get; set;
        }
        public string DataPropertyName
        {
            get; set;
        }
        public bool ReadOnly
        {
            get => readOnly; set
            {
                readOnly = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ReadOnly)));
            }
        }
        public int Width
        {
            get => width; set
            {
                width = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Width)));
            }
        }
        public DataGridViewCellStyle DefaultCellStyle { get; } = new DataGridViewCellStyle();
        public HeaderCell HeaderCell { get; } = new HeaderCell();
        public DataGridViewColumnSortMode SortMode
        {
            get; set;
        }
    }
    public class DataGridViewCheckBoxColumn : DataGridViewColumn
    {
    }
    public class HeaderCell
    {
        public DataGridViewCellStyle Style { get; } = new DataGridViewCellStyle();
    }
    public class DataGridViewCellStyle
    {
        public Font Font
        {
            get; set;
        }
        public Color ForeColor
        {
            get; set;
        }
        public Color BackColor
        {
            get; set;
        }
        public DataGridViewContentAlignment Alignment
        {
            get; set;
        }
        public bool WrapMode
        {
            get; set;
        }
    }
    public class DataGridColumns : ObservableCollection<DataGridViewColumn>
    {
        public DataGridViewColumn this[string name] => this.First(column => column.Name == name || column.HeaderText == name || column.DataPropertyName == name);
    }
    public class CellCollection : ObservableCollection<DataGridViewCell>
    {
        private readonly DataRow row;
        public CellCollection(DataRow row)
        {
            this.row = row;
        }
        public DataGridViewCell this[string name] => this[row.Table.Columns.IndexOf(name)];
    }
    public class RowTemplate
    {
        public int Height { get; set; } = 30;
    }
    public enum DataGridViewContentAlignment
    {
        MiddleLeft, MiddleCenter, MiddleRight
    }
    public enum DataGridViewColumnSortMode
    {
        NotSortable, Automatic, Programmatic
    }
    public delegate void ScrollEventHandler(object sender, ScrollEventArgs e);
    public class ScrollEventArgs : EventArgs
    {
    }
    public enum DataGridViewAutoSizeRowsMode
    {
        None, AllCells
    }
    public enum DataGridViewAutoSizeColumnsMode
    {
        None, Fill, AllCells
    }
    public enum DataGridViewSelectionMode
    {
        CellSelect, FullRowSelect
    }
    public enum DataGridViewEditMode
    {
        EditOnEnter, EditOnKeystrokeOrF2
    }
}
