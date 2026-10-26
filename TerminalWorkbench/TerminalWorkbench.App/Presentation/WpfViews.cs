using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Documents;
using W = System.Windows.Controls;
using Primitive = System.Windows.Controls.Primitives;

namespace TerminalWorkbench.Presentation
{
    internal static class WpfViews
    {
        private static readonly ConditionalWeakTable<ListView, W.DataGrid> tables = new ConditionalWeakTable<ListView, W.DataGrid>();
        private static readonly ConditionalWeakTable<TreeNode, WeakReference<W.TreeViewItem>> treeItems = new ConditionalWeakTable<TreeNode, WeakReference<W.TreeViewItem>>();
        private static readonly ConditionalWeakTable<DataGridView, W.DataGrid> editors = new ConditionalWeakTable<DataGridView, W.DataGrid>();
        internal static void Release(Control model)
        {
            if (model is ListView table)
            {
                if (tables.TryGetValue(table, out var grid))
                {
                    tables.Remove(table);
                    grid.ItemsSource = null;
                }
            }
            if (model is DataGridView editor && editors.TryGetValue(editor, out var native))
            {
                editors.Remove(editor);
                native.ItemsSource = null;
            }
            if (model.CreatedElement != null)
                BindingOperations.ClearAllBindings(model.CreatedElement);
        }

        internal static FrameworkElement Create(Control model)
        {
            FrameworkElement view;
            if (model is ContextMenuStrip contextModel)
            {
                var menu = new W.ContextMenu();
                foreach (Control child in contextModel.Items)
                    menu.Items.Add(child.Element);
                view = menu;
            }
            else if (model is ListView table)
                view = CreateTable(table);
            else if (model is DataGridView editor)
                view = CreateEditor(editor);
            else if (model is TreeView tree)
                view = CreateTree(tree);
            else if (model is PictureBox picture)
                view = new DrawingSurface(picture);
            else if (model is AxTChart chart)
                view = new TrendSurface(chart);
            else if (model is ComboBox combo)
                view = CreateCombo(combo);
            else if (model is RadioButton radio)
            {
                var control = new W.RadioButton { Content = radio.Text, GroupName = (radio.Parent?.Handle.ToInt32() ?? 0).ToString() };
                Bind(control, Primitive.ToggleButton.IsCheckedProperty, radio, nameof(CheckBox.Checked), true);
                view = control;
            }
            else if (model is CheckBox check)
            {
                var control = new W.CheckBox { Content = check.Text };
                Bind(control, Primitive.ToggleButton.IsCheckedProperty, check, nameof(CheckBox.Checked), true);
                view = control;
            }
            else if (model is ToolStripMenuItem menu)
                view = CreateMenuItem(menu);
            else if (model is Button || model is ToolStripButton)
            {
                var button = new W.Button { MinWidth = 48, Padding = new Thickness(10, 5, 10, 5) };
                if (model.Name == "Btu_RunStart")
                {
                    button.Background = Brush("#087D8A");
                    button.Foreground = Brushes.White;
                }
                Bind(button, W.ContentControl.ContentProperty, model, nameof(Control.Text));
                button.Click += (_, _) => model.PerformClick();
                view = button;
            }
            else if (model is RichTextBox rich)
                view = CreateRichText(rich);
            else if (model is TextBox text)
            {
                if (text.PasswordChar != '\0')
                {
                    var password = new W.PasswordBox { Password = text.Text, Padding = new Thickness(5, 3, 5, 3) };
                    password.PasswordChanged += (_, _) => text.Text = password.Password;
                    text.PropertyChanged += (_, e) =>
                    {
                        if (e.PropertyName == nameof(Control.Text) && password.Password != text.Text)
                            password.Password = text.Text;
                    };
                    Bind(password, UIElement.IsEnabledProperty, model, nameof(Control.Enabled));
                    password.SetBinding(UIElement.VisibilityProperty, new Binding(nameof(Control.Visible)) { Source = model, Converter = new W.BooleanToVisibilityConverter() });
                    password.Margin = new Thickness(2);
                    ApplyControlMetrics(model, password);
                    return password;
                }
                var box = new W.TextBox
                {
                    IsReadOnly = text.ReadOnly,
                    AcceptsReturn = text.Multiline,
                    TextWrapping = text.Multiline ? TextWrapping.Wrap : TextWrapping.NoWrap,
                    VerticalScrollBarVisibility = text.Multiline ? W.ScrollBarVisibility.Auto : W.ScrollBarVisibility.Hidden,
                    Padding = new Thickness(5, 3, 5, 3),
                    VerticalContentAlignment = VerticalAlignment.Center
                };
                if (text.MaxLength > 0)
                    box.MaxLength = text.MaxLength;
                Bind(box, W.TextBox.TextProperty, model, nameof(Control.Text), true);
                box.LostKeyboardFocus += (_, _) => model.Validate();
                box.Background = ColorBrush(model.BackColor);
                model.PropertyChanged += (_, e) => { if (e.PropertyName == nameof(Control.BackColor)) box.Background = ColorBrush(model.BackColor); };
                view = box;
            }
            else if (model is DateTimePicker date)
                view = CreateDateTime(date);
            else if (model is ProgressBar progress)
            {
                var bar = new W.ProgressBar { Minimum = progress.Minimum, Maximum = progress.Maximum };
                Bind(bar, Primitive.RangeBase.MinimumProperty, model, nameof(ProgressBar.Minimum));
                Bind(bar, Primitive.RangeBase.MaximumProperty, model, nameof(ProgressBar.Maximum));
                Bind(bar, Primitive.RangeBase.ValueProperty, model, nameof(ProgressBar.Value));
                view = bar;
            }
            else if (model is ToolStripSeparator)
                view = new W.Separator { Margin = new Thickness(5, 2, 5, 2) };
            else if (model is Label || model is ToolStripLabel)
            {
                var label = new W.TextBlock { VerticalAlignment = VerticalAlignment.Center, TextWrapping = TextWrapping.Wrap, Margin = new Thickness(5, 2, 5, 2) };
                Bind(label, W.TextBlock.TextProperty, model, nameof(Control.Text));
                view = label;
            }
            else
                view = CreateContainer(model);

            ApplyControlMetrics(model, view);
            Bind(view, UIElement.IsEnabledProperty, model, nameof(Control.Enabled));
            view.SetBinding(UIElement.VisibilityProperty, new Binding(nameof(Control.Visible)) { Source = model, Converter = new W.BooleanToVisibilityConverter() });
            view.DataContext = model;
            view.ToolTip = model is ToolStripItem ? model.Text : null;
            view.AddHandler(UIElement.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler((_, e) =>
            {
                if (e.ClickCount == 2)
                    model.PerformDoubleClick();
            }), true);
            if (model is not Form)
                view.Margin = new Thickness(2);
            if (model.ContextMenuStrip != null)
            {
                W.ContextMenuService.SetContextMenu(view, (W.ContextMenu)model.ContextMenuStrip.Element);
            }
            return view;
        }

        private static void ApplyControlMetrics(Control model, FrameworkElement view)
        {
            view.UseLayoutRounding = true;
            view.SnapsToDevicePixels = true;
            if (model is TextBox || model is ComboBox || model is DateTimePicker)
            {
                view.MinHeight = 32;
            }
            else if (model is Button || model is ToolStripButton)
            {
                view.MinHeight = 34;
            }
            else if (model is CheckBox || model is Label || model is ToolStripLabel)
            {
                view.MinHeight = 24;
            }
        }

        private static void Bind(DependencyObject target, DependencyProperty property, object source, string name, bool twoWay = false)
        {
            BindingOperations.SetBinding(target, property, new Binding(name)
            {
                Source = source,
                Mode = twoWay ? BindingMode.TwoWay : BindingMode.OneWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });
        }

        private static FrameworkElement CreateCombo(ComboBox model)
        {
            var combo = new W.ComboBox { ItemsSource = model.Items, IsEditable = model.DropDownStyle != ComboBoxStyle.DropDownList, MinWidth = 70, Padding = new Thickness(3) };
            Bind(combo, Primitive.Selector.SelectedIndexProperty, model, nameof(ComboBox.SelectedIndex), true);
            Bind(combo, W.ComboBox.TextProperty, model, nameof(Control.Text), true);
            combo.LostKeyboardFocus += (_, _) => model.Validate();
            return combo;
        }
        private static FrameworkElement CreateRichText(RichTextBox model)
        {
            var paragraph = new Paragraph { Margin = new Thickness(0) };
            var box = new W.RichTextBox
            {
                Document = new FlowDocument(paragraph),
                IsReadOnly = model.ReadOnly,
                VerticalScrollBarVisibility = W.ScrollBarVisibility.Auto,
                Background = ColorBrush(model.BackColor),
                Padding = new Thickness(5)
            };
            bool updating = false;
            void Replace()
            {
                updating = true;
                try
                {
                    paragraph.Inlines.Clear();
                    paragraph.Inlines.Add(new Run(model.Text));
                }
                finally { updating = false; }
            }
            Replace();
            box.TextChanged += (_, _) =>
            {
                if (updating)
                    return;
                updating = true;
                try
                {
                    string text = new TextRange(box.Document.ContentStart, box.Document.ContentEnd).Text;
                    model.Text = text.EndsWith("\r\n", StringComparison.Ordinal) ? text.Substring(0, text.Length - 2) : text;
                }
                finally { updating = false; }
            };
            model.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(Control.Text) && !updating && !model.IsAppending)
                    Replace();
                if (e.PropertyName == nameof(Control.BackColor))
                    box.Background = ColorBrush(model.BackColor);
            };
            model.Appended += (_, e) =>
            {
                updating = true;
                try
                {
                    paragraph.Inlines.Add(new Run(e.Text) { Foreground = ColorBrush(e.Color.IsEmpty ? model.ForeColor : e.Color) });
                    box.ScrollToEnd();
                }
                finally { updating = false; }
            };
            return box;
        }

        private static FrameworkElement CreateMenuItem(ToolStripMenuItem model)
        {
            var menu = new W.MenuItem();
            Bind(menu, W.HeaderedItemsControl.HeaderProperty, model, nameof(Control.Text));
            foreach (Control child in model.DropDownItems)
                menu.Items.Add(child.Element);
            menu.Click += (_, e) => { if (ReferenceEquals(e.Source, menu)) model.PerformClick(); };
            return menu;
        }

        private static FrameworkElement CreateDateTime(DateTimePicker model)
        {
            var box = new W.TextBox { Text = model.Value.ToString(string.IsNullOrEmpty(model.CustomFormat) ? "yyyy-MM-dd HH:mm:ss" : model.CustomFormat) };
            box.LostKeyboardFocus += (_, _) =>
            {
                if (DateTime.TryParse(box.Text, out DateTime value))
                    model.Value = value;
                else
                    box.Text = model.Value.ToString("yyyy-MM-dd HH:mm:ss");
            };
            model.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(DateTimePicker.Value))
                    box.Text = model.Value.ToString(string.IsNullOrEmpty(model.CustomFormat) ? "yyyy-MM-dd HH:mm:ss" : model.CustomFormat);
            };
            return box;
        }

        private static FrameworkElement CreateContainer(Control model)
        {
            if (model is SplitContainer split)
                return CreateSplit(split);
            if (model is CNormalTabControl || model is DockPanel)
            {
                var tabs = new W.TabControl();
                void Refresh()
                {
                    foreach (W.TabItem item in tabs.Items)
                        item.Content = null;
                    tabs.Items.Clear();
                    foreach (Control child in model.Controls)
                    {
                        object header = child.Text;
                        if (child is DockContent document)
                        {
                            var bar = new W.StackPanel { Orientation = W.Orientation.Horizontal };
                            bar.Children.Add(new W.TextBlock { Text = child.Text, VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(3, 0, 6, 0) });
                            var floating = new W.Button { Content = "↗", ToolTip = "独立窗口", Padding = new Thickness(4, 0, 4, 0) };
                            floating.Click += (_, e) => { e.Handled = true; document.Float(); };
                            var close = new W.Button { Content = "×", ToolTip = "关闭", Padding = new Thickness(4, 0, 4, 0) };
                            close.Click += (_, e) => { e.Handled = true; document.Close(); };
                            bar.Children.Add(floating);
                            bar.Children.Add(close);
                            header = bar;
                        }
                        tabs.Items.Add(new W.TabItem { Header = header, Content = child.Element });
                    }
                    if (tabs.Items.Count > 0)
                        tabs.SelectedIndex = 0;
                }
                model.Controls.CollectionChanged += (_, _) => Refresh();
                Refresh();
                if (model is CNormalTabControl tabModel)
                {
                    Bind(tabs, Primitive.Selector.SelectedIndexProperty, tabModel, nameof(CNormalTabControl.SelectedIndex), true);
                }
                return tabs;
            }
            if (model is MenuStrip)
            {
                var menu = new W.Menu();
                foreach (Control child in model.Controls)
                    menu.Items.Add(child.Element);
                return menu;
            }
            if (model is ToolStrip)
            {
                var toolbar = new W.WrapPanel { Background = Brush("#F1F5F8"), Margin = new Thickness(4) };
                foreach (Control child in model.Controls)
                {
                    var item = child.Element;
                    item.VerticalAlignment = VerticalAlignment.Center;
                    if (child is ComboBox || child is TextBox)
                        item.Width = Math.Max(80, Math.Min(240, child.Width));
                    toolbar.Children.Add(item);
                }
                return toolbar;
            }
            if (model is TableLayoutPanel layout)
            {
                var grid = new W.Grid();
                for (int column = 0; column < layout.ColumnCount; column++)
                    grid.ColumnDefinitions.Add(new W.ColumnDefinition { Width = column < layout.ColumnWidths.Count ? layout.ColumnWidths[column] : new GridLength(1, GridUnitType.Star) });
                for (int row = 0; row < layout.RowCount; row++)
                    grid.RowDefinitions.Add(new W.RowDefinition { Height = row < layout.RowHeights.Count ? layout.RowHeights[row] : new GridLength(1, GridUnitType.Star) });
                foreach (Control child in model.Controls)
                {
                    var cell = model.Controls.Cells.TryGetValue(child, out var position) ? position : new System.Drawing.Point(0, 0);
                    W.Grid.SetColumn(child.Element, cell.X);
                    W.Grid.SetRow(child.Element, cell.Y);
                    grid.Children.Add(child.Element);
                }
                return grid;
            }
            var root = new W.Grid { Background = model is Form ? Brush("#F1F5F8") : null };
            if (model is Form)
            {
                TextElement.SetFontFamily(root, new FontFamily("Microsoft YaHei UI"));
                TextElement.SetFontSize(root, 13);
            }
            var panel = new W.DockPanel { LastChildFill = true };
            root.Children.Add(panel);
            var dockSubscriptions = new Dictionary<Control, PropertyChangedEventHandler>();
            var elementHosts = new List<W.Panel>();
            var positionSubscriptions = new Dictionary<Control, PropertyChangedEventHandler>();
            void Populate()
            {
                foreach (W.Panel host in elementHosts)
                {
                    host.Children.Clear();
                }
                elementHosts.Clear();
                foreach (var subscription in positionSubscriptions)
                {
                    subscription.Key.PropertyChanged -= subscription.Value;
                }
                positionSubscriptions.Clear();
                panel.Children.Clear();
                while (root.Children.Count > 1)
                {
                    if (root.Children[1] is W.ScrollViewer scroll)
                    {
                        scroll.Content = null;
                    }
                    root.Children.RemoveAt(1);
                }
                foreach (Control previous in dockSubscriptions.Keys.Except(model.Controls).ToArray())
                {
                    previous.PropertyChanged -= dockSubscriptions[previous];
                    dockSubscriptions.Remove(previous);
                }
                foreach (Control child in model.Controls)
                {
                    if (!dockSubscriptions.ContainsKey(child))
                    {
                        PropertyChangedEventHandler changed = (_, e) =>
                        {
                            if (e.PropertyName == nameof(Control.Dock))
                            {
                                Populate();
                            }
                        };
                        dockSubscriptions.Add(child, changed);
                        child.PropertyChanged += changed;
                    }
                }
                foreach (Control child in model.Controls.Where(child => child.Dock != DockStyle.None && child.Dock != DockStyle.Fill).Reverse())
                {
                    var element = child.Element;
                    element.Width = double.NaN;
                    element.Height = double.NaN;
                    var dock = child.Dock == DockStyle.Top ? W.Dock.Top : child.Dock == DockStyle.Bottom ? W.Dock.Bottom : child.Dock == DockStyle.Right ? W.Dock.Right : W.Dock.Left;
                    W.DockPanel.SetDock(element, dock);
                    if (child.Dock == DockStyle.Left || child.Dock == DockStyle.Right)
                        element.Width = child.Width;
                    if (child.Dock == DockStyle.Top || child.Dock == DockStyle.Bottom)
                        element.Height = child is ToolStrip ? double.NaN : child.Height;
                    panel.Children.Add(element);
                }
                var fill = model.Controls.Where(child => child.Dock == DockStyle.Fill).ToArray();
                if (fill.Length > 0)
                {
                    var content = new W.Grid();
                    elementHosts.Add(content);
                    foreach (Control child in fill)
                    {
                        child.Element.Width = double.NaN;
                        child.Element.Height = double.NaN;
                        content.Children.Add(child.Element);
                    }
                    panel.Children.Add(content);
                }
                var absolute = model.Controls.Where(child => child.Dock == DockStyle.None).ToArray();
                if (absolute.Length > 0)
                {
                    if (panel.Children.Count == 0 && absolute.All(IsFormField))
                    {
                        root.Children.Add(CreateFieldRows(absolute, elementHosts));
                        return;
                    }
                    if (panel.Children.Count == 0 && absolute.Length == 1 && absolute[0] is not Label)
                    {
                        absolute[0].Element.Width = double.NaN;
                        absolute[0].Element.Height = double.NaN;
                        root.Children.Add(absolute[0].Element);
                        return;
                    }
                    var canvas = new W.Canvas { MinWidth = absolute.Max(child => child.Right) + 8, MinHeight = absolute.Max(child => child.Bottom) + 8 };
                    elementHosts.Add(canvas);
                    foreach (Control child in absolute)
                    {
                        var element = child.Element;
                        element.Width = Math.Max(1, child.Width);
                        element.Height = Math.Max(element.MinHeight, child.Height);
                        W.Canvas.SetLeft(element, child.Left);
                        W.Canvas.SetTop(element, child.Top);
                        PropertyChangedEventHandler changed = (_, e) =>
                        {
                            if (e.PropertyName == nameof(Control.Location))
                            {
                                W.Canvas.SetLeft(element, child.Left);
                                W.Canvas.SetTop(element, child.Top);
                            }
                            if (e.PropertyName == nameof(Control.Size))
                            {
                                element.Width = Math.Max(1, child.Width);
                                element.Height = Math.Max(element.MinHeight, child.Height);
                            }
                        };
                        positionSubscriptions.Add(child, changed);
                        child.PropertyChanged += changed;
                        canvas.Children.Add(element);
                    }
                    if (panel.Children.Count == 0)
                        root.Children.Add(new W.ScrollViewer { Content = canvas, HorizontalScrollBarVisibility = W.ScrollBarVisibility.Auto, VerticalScrollBarVisibility = W.ScrollBarVisibility.Auto });
                    else
                        root.Children.Add(canvas);
                }
            }
            model.Controls.CollectionChanged += (_, _) => Populate();
            Populate();
            if (model is GroupBox group)
                return new W.GroupBox { Header = group.Text, Content = root, Padding = new Thickness(4) };
            return root;
        }

        private static bool IsFormField(Control control)
        {
            return control is Label || control is Button || control is ComboBox || control is CheckBox
                || control is DateTimePicker || control is ProgressBar || control is TextBox text && !text.Multiline;
        }

        private static FrameworkElement CreateFieldRows(Control[] controls, List<W.Panel> elementHosts)
        {
            var rows = new W.StackPanel { Margin = new Thickness(8) };
            var groups = new List<List<Control>>();
            List<Control> group = null;
            int rowCenter = int.MinValue;
            foreach (Control control in controls.OrderBy(control => control.Top + Math.Min(control.Height, 32) / 2).ThenBy(control => control.Left))
            {
                int center = control.Top + Math.Min(control.Height, 32) / 2;
                if (group == null || center - rowCenter > 12)
                {
                    group = new List<Control>();
                    groups.Add(group);
                    rowCenter = center;
                }
                group.Add(control);
            }
            foreach (List<Control> fields in groups)
            {
                var row = new W.WrapPanel { Margin = new Thickness(0, 3, 0, 3) };
                elementHosts.Add(row);
                rows.Children.Add(row);
                foreach (Control control in fields.OrderBy(control => control.Left))
                {
                    FrameworkElement element = control.Element;
                    element.Width = control is Label || control is Button || control is CheckBox ? double.NaN : Math.Max(40, control.Width);
                    element.Height = double.NaN;
                    element.VerticalAlignment = VerticalAlignment.Center;
                    element.Margin = new Thickness(4, 3, 8, 3);
                    row.Children.Add(element);
                }
            }
            return new W.ScrollViewer
            {
                Content = rows,
                HorizontalScrollBarVisibility = W.ScrollBarVisibility.Disabled,
                VerticalScrollBarVisibility = W.ScrollBarVisibility.Auto,
                HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch
            };
        }

        private static FrameworkElement CreateSplit(SplitContainer model)
        {
            var grid = new W.Grid();
            bool horizontal = model.Orientation == Orientation.Horizontal;
            if (horizontal)
            {
                grid.RowDefinitions.Add(new W.RowDefinition { Height = new GridLength(model.SplitterDistance) });
                grid.RowDefinitions.Add(new W.RowDefinition { Height = GridLength.Auto });
                grid.RowDefinitions.Add(new W.RowDefinition());
            }
            else
            {
                grid.ColumnDefinitions.Add(new W.ColumnDefinition { Width = new GridLength(model.SplitterDistance) });
                grid.ColumnDefinitions.Add(new W.ColumnDefinition { Width = GridLength.Auto });
                grid.ColumnDefinitions.Add(new W.ColumnDefinition());
            }
            var first = model.Panel1.Element;
            var second = model.Panel2.Element;
            var splitter = new W.GridSplitter { Width = horizontal ? double.NaN : 5, Height = horizontal ? 5 : double.NaN, Background = Brush("#D5E0E7"), HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch, VerticalAlignment = VerticalAlignment.Stretch };
            if (horizontal)
            {
                W.Grid.SetRow(splitter, 1);
                W.Grid.SetRow(second, 2);
            }
            else
            {
                W.Grid.SetColumn(splitter, 1);
                W.Grid.SetColumn(second, 2);
            }
            grid.Children.Add(first);
            grid.Children.Add(splitter);
            grid.Children.Add(second);
            void Collapse()
            {
                first.Visibility = model.Panel1Collapsed ? Visibility.Collapsed : Visibility.Visible;
                second.Visibility = model.Panel2Collapsed ? Visibility.Collapsed : Visibility.Visible;
                splitter.Visibility = model.Panel1Collapsed || model.Panel2Collapsed ? Visibility.Collapsed : Visibility.Visible;
                if (horizontal)
                {
                    grid.RowDefinitions[0].Height = model.Panel1Collapsed ? new GridLength(0) : model.Panel2Collapsed ? new GridLength(1, GridUnitType.Star) : new GridLength(model.SplitterDistance);
                    grid.RowDefinitions[2].Height = model.Panel2Collapsed ? new GridLength(0) : new GridLength(1, GridUnitType.Star);
                }
                else
                {
                    grid.ColumnDefinitions[0].Width = model.Panel1Collapsed ? new GridLength(0) : model.Panel2Collapsed ? new GridLength(1, GridUnitType.Star) : new GridLength(model.SplitterDistance);
                    grid.ColumnDefinitions[2].Width = model.Panel2Collapsed ? new GridLength(0) : new GridLength(1, GridUnitType.Star);
                }
            }
            model.PropertyChanged += (_, _) => Collapse();
            Collapse();
            return grid;
        }

        private static W.DataGrid NewGrid()
        {
            var grid = new W.DataGrid
            {
                AutoGenerateColumns = false,
                CanUserAddRows = false,
                CanUserDeleteRows = false,
                CanUserReorderColumns = false,
                CanUserSortColumns = false,
                EnableRowVirtualization = true,
                EnableColumnVirtualization = true,
                HeadersVisibility = W.DataGridHeadersVisibility.Column,
                RowHeight = 34,
                GridLinesVisibility = W.DataGridGridLinesVisibility.Horizontal,
                HorizontalGridLinesBrush = Brush("#E5EDF1"),
                AlternatingRowBackground = Brush("#F5F9FB"),
                BorderBrush = Brush("#D5E0E7"),
                BorderThickness = new Thickness(1),
                Background = Brushes.White,
                Foreground = Brush("#193549"),
                SelectionUnit = W.DataGridSelectionUnit.FullRow
            };
            W.VirtualizingPanel.SetIsVirtualizing(grid, true);
            W.VirtualizingPanel.SetVirtualizationMode(grid, W.VirtualizationMode.Recycling);
            W.ScrollViewer.SetCanContentScroll(grid, true);
            return grid;
        }

        private static FrameworkElement CreateTable(ListView model)
        {
            var grid = NewGrid();
            tables.AddOrUpdate(model, grid);
            grid.ItemsSource = model.Items;
            grid.SelectionMode = model.MultiSelect ? W.DataGridSelectionMode.Extended : W.DataGridSelectionMode.Single;
            bool selecting = false;
            grid.SelectionChanged += (_, _) =>
            {
                if (selecting)
                    return;
                selecting = true;
                try
                {
                    foreach (ListViewItem row in model.Items)
                        row.Selected = grid.SelectedItems.Contains(row);
                }
                finally { selecting = false; }
            };
            void Columns()
            {
                grid.Columns.Clear();
                for (int column = 0; column < model.Columns.Count; column++)
                {
                    var header = model.Columns[column];
                    grid.Columns.Add(new PointColumn(model, column)
                    {
                        Header = header.Text,
                        MinWidth = Math.Max(Math.Min(240, header.Width), Math.Max(64, Math.Min(160, header.Text.Length * 13 + 26))),
                        Width = new W.DataGridLength(Math.Max(35, header.Width), W.DataGridLengthUnitType.Star)
                    });
                }
            }
            model.Columns.CollectionChanged += (_, _) => Columns();
            model.PropertyChanged += (_, e) => { if (e.PropertyName == nameof(ListView.CheckBoxes)) Columns(); };
            grid.BeginningEdit += (_, e) =>
            {
                if (e.Column is PointColumn column)
                {
                    if (model is MyListView && (e.EditingEventArgs is not MouseButtonEventArgs mouse || mouse.ClickCount < 2))
                        model.PerformDoubleClick();
                    if (model is not MyListView editable || editable.GetEditMode((ListViewItem)e.Row.Item, column.Index).Mode == 0)
                        e.Cancel = true;
                }
            };
            grid.CellEditEnding += (_, e) =>
            {
                if (e.EditAction != W.DataGridEditAction.Commit || e.Column is not PointColumn column || model is not MyListView editable)
                    return;
                string value = e.EditingElement is W.ComboBox combo ? combo.Text : ((W.TextBox)e.EditingElement).Text;
                editable.CommitEdit((ListViewItem)e.Row.Item, column.Index, value);
            };
            grid.Unloaded += (_, _) => { if (model.IsDisposed) tables.Remove(model); };
            Columns();
            return grid;
        }

        private sealed class PointColumn : W.DataGridColumn
        {
            private readonly ListView table;
            internal int Index
            {
                get;
            }
            internal PointColumn(ListView table, int index)
            {
                this.table = table;
                Index = index;
            }
            protected override FrameworkElement GenerateElement(W.DataGridCell cell, object dataItem)
            {
                var row = (ListViewItem)dataItem;
                var panel = new W.DockPanel { LastChildFill = true, VerticalAlignment = VerticalAlignment.Center };
                if (Index == 0 && table.CheckBoxes)
                {
                    var check = new W.CheckBox { VerticalAlignment = VerticalAlignment.Center, Margin = new Thickness(3, 0, 4, 0) };
                    check.SetBinding(Primitive.ToggleButton.IsCheckedProperty, new Binding(nameof(ListViewItem.Checked)) { Source = row, Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
                    check.PreviewMouseLeftButtonDown += (_, _) => row.Selected = true;
                    W.DockPanel.SetDock(check, W.Dock.Left);
                    panel.Children.Add(check);
                }
                var status = new System.Windows.Shapes.Ellipse { Width = 8, Height = 8, Margin = new Thickness(0, 0, 5, 0), VerticalAlignment = VerticalAlignment.Center };
                status.SetBinding(System.Windows.Shapes.Shape.FillProperty, new Binding($"SubItems[{Index}].StatusBrush") { Source = row });
                var statusStyle = new Style(typeof(System.Windows.Shapes.Ellipse));
                statusStyle.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Collapsed));
                foreach (int index in new[] { 1, 2 })
                {
                    var trigger = new DataTrigger { Binding = new Binding($"SubItems[{Index}].ImageIndex") { Source = row }, Value = index };
                    trigger.Setters.Add(new Setter(UIElement.VisibilityProperty, Visibility.Visible));
                    statusStyle.Triggers.Add(trigger);
                }
                status.Style = statusStyle;
                W.DockPanel.SetDock(status, W.Dock.Left);
                var text = new W.TextBlock { Margin = new Thickness(6, 0, 6, 0), TextTrimming = TextTrimming.CharacterEllipsis };
                text.SetBinding(W.TextBlock.TextProperty, new Binding($"SubItems[{Index}].Text") { Source = row });
                text.SetBinding(FrameworkElement.ToolTipProperty, new Binding($"SubItems[{Index}].Text") { Source = row });
                panel.Children.Add(status);
                panel.Children.Add(text);
                return panel;
            }
            protected override FrameworkElement GenerateEditingElement(W.DataGridCell cell, object dataItem)
            {
                var row = (ListViewItem)dataItem;
                var mode = ((MyListView)table).GetEditMode(row, Index);
                if (mode.Mode == 2)
                {
                    return new W.ComboBox { ItemsSource = mode.Choices, Text = row.SubItems[Index].Text, IsEditable = false };
                }
                return new W.TextBox { Text = row.SubItems[Index].Text };
            }
        }

        internal static void SynchronizeSelection(ListView model)
        {
            if (!tables.TryGetValue(model, out var grid))
                return;
            if (grid.SelectionMode == W.DataGridSelectionMode.Single)
            {
                var selected = model.SelectedItems.FirstOrDefault();
                if (!ReferenceEquals(grid.SelectedItem, selected))
                    grid.SelectedItem = selected;
                return;
            }
            foreach (ListViewItem row in model.Items)
            {
                if (row.Selected && !grid.SelectedItems.Contains(row))
                    grid.SelectedItems.Add(row);
                else if (!row.Selected && grid.SelectedItems.Contains(row))
                    grid.SelectedItems.Remove(row);
            }
        }
        internal static void ScrollTo(ListView model, ListViewItem row)
        {
            if (tables.TryGetValue(model, out var grid))
                grid.ScrollIntoView(row);
        }
        internal static ListViewItem FirstVisible(ListView model)
        {
            if (!tables.TryGetValue(model, out var grid))
                return null;
            for (int i = 0; i < grid.Items.Count; i++)
            {
                if (grid.ItemContainerGenerator.ContainerFromIndex(i) is W.DataGridRow row && row.IsVisible)
                    return (ListViewItem)row.Item;
            }
            return null;
        }

        private static FrameworkElement CreateTree(TreeView model)
        {
            var tree = new W.TreeView { ItemsSource = model.Nodes, BorderBrush = Brush("#D5E0E7"), BorderThickness = new Thickness(1) };
            var template = new HierarchicalDataTemplate(typeof(TreeNode)) { ItemsSource = new Binding(nameof(TreeNode.Nodes)) };
            var text = new FrameworkElementFactory(typeof(W.TextBlock));
            text.SetBinding(W.TextBlock.TextProperty, new Binding(nameof(TreeNode.Text)));
            text.SetValue(FrameworkElement.MarginProperty, new Thickness(3, 5, 3, 5));
            template.VisualTree = text;
            tree.ItemTemplate = template;
            var style = new Style(typeof(W.TreeViewItem));
            style.Setters.Add(new Setter(W.TreeViewItem.IsExpandedProperty, new Binding(nameof(TreeNode.IsExpanded)) { Mode = BindingMode.TwoWay }));
            style.Setters.Add(new Setter(W.TreeViewItem.IsSelectedProperty, new Binding(nameof(TreeNode.IsSelected)) { Mode = BindingMode.TwoWay }));
            style.Setters.Add(new EventSetter(FrameworkElement.LoadedEvent, new RoutedEventHandler((sender, _) =>
            {
                var item = (W.TreeViewItem)sender;
                if (item.DataContext is TreeNode node)
                    treeItems.AddOrUpdate(node, new WeakReference<W.TreeViewItem>(item));
            })));
            tree.ItemContainerStyle = style;
            tree.Resources[typeof(W.TreeViewItem)] = style;
            tree.SelectedItemChanged += (_, e) => model.SelectedNode = (TreeNode)e.NewValue;
            model.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(TreeView.SelectedNode) && model.SelectedNode != null && treeItems.TryGetValue(model.SelectedNode, out var item) && item.TryGetTarget(out var target))
                    target.IsSelected = true;
            };
            return tree;
        }
        internal static void ExpandNode(TreeNode node)
        {
            if (treeItems.TryGetValue(node, out var item) && item.TryGetTarget(out var target))
                target.IsExpanded = true;
        }

        private static FrameworkElement CreateEditor(DataGridView model)
        {
            var grid = NewGrid();
            editors.AddOrUpdate(model, grid);
            grid.SelectionUnit = W.DataGridSelectionUnit.Cell;
            var root = new W.Grid();
            var overlay = new W.Canvas { IsHitTestVisible = true };
            root.Children.Add(grid);
            root.Children.Add(overlay);
            void Data()
            {
                grid.ItemsSource = model.Rows;
                grid.Columns.Clear();
                foreach (DataGridViewColumn column in model.Columns)
                {
                    var binding = new Binding($"[{model.Columns.IndexOf(column)}].Value") { Mode = BindingMode.TwoWay, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
                    W.DataGridColumn native = column is DataGridViewCheckBoxColumn
                        || model.DataSource is DataTable source && source.Columns.Contains(column.DataPropertyName) && source.Columns[column.DataPropertyName].DataType == typeof(bool)
                        ? new W.DataGridCheckBoxColumn { Binding = binding }
                        : new W.DataGridTextColumn { Binding = binding };
                    native.Header = column.HeaderText;
                    void ColumnSize()
                    {
                        native.MinWidth = column is DataGridViewCheckBoxColumn ? 56
                            : Math.Max(Math.Min(160, column.Width), Math.Max(72, Math.Min(150, column.HeaderText.Length * 13 + 26)));
                        native.Width = model.AutoSizeColumnsMode == DataGridViewAutoSizeColumnsMode.Fill
                            ? new W.DataGridLength(Math.Max(native.MinWidth, column.Width), W.DataGridLengthUnitType.Star)
                            : new W.DataGridLength(Math.Max(native.MinWidth, column.Width));
                    }
                    ColumnSize();
                    native.IsReadOnly = model.ReadOnly || column.ReadOnly;
                    column.PropertyChanged += (_, _) => { ColumnSize(); native.IsReadOnly = model.ReadOnly || column.ReadOnly; };
                    grid.Columns.Add(native);
                }
            }
            model.PropertyChanged += (_, e) => { if (e.PropertyName == nameof(DataGridView.DataSource)) Data(); };
            model.Columns.CollectionChanged += (_, _) => Data();
            grid.CurrentCellChanged += (_, _) =>
            {
                if (grid.CurrentItem is DataGridViewRow row && grid.CurrentColumn != null)
                {
                    int index = row.Index;
                    int column = grid.Columns.IndexOf(grid.CurrentColumn);
                    if (index >= 0 && index < model.Rows.Count && column < model.Rows[index].Cells.Count)
                        model.CurrentCell = model.Rows[index].Cells[column];
                }
            };
            grid.AddHandler(W.ScrollViewer.ScrollChangedEvent, new W.ScrollChangedEventHandler((_, _) => model.RaiseScroll()));
            model.Controls.CollectionChanged += (_, _) =>
            {
                overlay.Children.Clear();
                foreach (Control control in model.Controls)
                {
                    overlay.Children.Add(control.Element);
                    control.Element.Width = control.Width;
                    control.Element.Height = control.Height;
                    W.Canvas.SetLeft(control.Element, control.Left);
                    W.Canvas.SetTop(control.Element, control.Top);
                    control.PropertyChanged += (_, e) =>
                    {
                        if (e.PropertyName == nameof(Control.Location))
                        {
                            W.Canvas.SetLeft(control.Element, control.Left);
                            W.Canvas.SetTop(control.Element, control.Top);
                        }
                        if (e.PropertyName == nameof(Control.Size))
                        {
                            control.Element.Width = control.Width;
                            control.Element.Height = control.Height;
                        }
                    };
                }
            };
            Data();
            return root;
        }
        internal static System.Drawing.Rectangle CellRectangle(DataGridView model, int column, int row)
        {
            if (!editors.TryGetValue(model, out var grid) || row < 0 || row >= grid.Items.Count || column < 0 || column >= grid.Columns.Count)
                return System.Drawing.Rectangle.Empty;
            var content = grid.Columns[column].GetCellContent(grid.Items[row]);
            if (content == null)
                return System.Drawing.Rectangle.Empty;
            var point = content.TransformToAncestor(grid).Transform(new Point(0, 0));
            return new System.Drawing.Rectangle((int)point.X, (int)point.Y, (int)content.ActualWidth, (int)content.ActualHeight);
        }
        internal static bool EndEditorEdit(DataGridView model)
        {
            if (!editors.TryGetValue(model, out var grid))
                return true;
            return grid.CommitEdit(W.DataGridEditingUnit.Cell, true) && grid.CommitEdit(W.DataGridEditingUnit.Row, true);
        }
        private static SolidColorBrush Brush(string hex) => (SolidColorBrush)new BrushConverter().ConvertFromString(hex);
        private static SolidColorBrush ColorBrush(System.Drawing.Color color) => new SolidColorBrush(System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B));
    }
}
