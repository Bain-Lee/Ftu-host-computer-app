using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using DtuTestManager;
using TerminalWorkbench.Presentation;
using W = System.Windows.Controls;

internal static class LayoutRegression
{
    internal static void Run()
    {
        string previews = Path.Combine(AppContext.BaseDirectory, "layout-previews");
        Directory.CreateDirectory(previews);
        var failures = new List<string>();
        CGlobal.m_Device.CloseThread();
        CGlobal.m_Device = new CDeviceClass();
        CGlobal.m_RunSendMode = false;
        using var startup = new MainForm();
        startup.ActivateScreen();
        try
        {
            Assert(CGlobal.m_Device.m_YcList.Count == 400, "Layout startup did not load the standard template.");
            foreach (Size viewport in new[] { new Size(1440, 900), new Size(1024, 720) })
            {
                Connection(viewport, previews, failures);
                History(viewport, previews, failures);
                Calibration(viewport, previews, failures);
                Modules(viewport, previews, failures);
                Conversion(viewport, previews, failures);
                FileTransfer(viewport, previews, failures);
                ParameterSelection(viewport, previews, failures);
                Realtime(viewport, previews, failures);
                Encryption(viewport, previews, failures);
            }
            Dialogs(previews, failures);
            CalibrationReopen();
        }
        finally
        {
            startup.Close();
            var timeout = Stopwatch.StartNew();
            while (!startup.IsDisposed && timeout.ElapsedMilliseconds < 5000)
            {
                Pump();
                Thread.Sleep(2);
            }
            Assert(startup.IsDisposed, "Layout startup did not finish shutdown.");
        }
        Assert(failures.Count == 0, string.Join(Environment.NewLine, failures));
        Console.WriteLine("Layout previews: " + previews);
    }

    private static void Connection(Size viewport, string previews, List<string> failures)
    {
        using var form = new CMasterConForm();
        form.ActivateScreen();
        using var renderer = new Renderer(form, viewport);
        Scene("connection", form, renderer, previews, failures, () =>
        {
            foreach (string name in new[] { "ConnPort", "AsduAddress_101", "YxAddress_101", "AsduAddress_104", "EncodeIP", "DevModList" })
            {
                FrameworkElement input = Field<Control>(form, name).Element;
                Assert(input.ActualWidth >= 60 && input.ActualHeight >= 28, "Communication input is too small: " + name);
            }
            Assert(Field<Control>(form, "SaveDataSet").Element.IsVisible, "The configuration save action is missing.");
        });
        form.Close();
    }

    private static void History(Size viewport, string previews, List<string> failures)
    {
        using var form = new CHisRecordForm();
        form.ActivateScreen();
        Field<ListView>(form, "LV_File").Items.Add(new ListViewItem(new[]
        {
            "", "1", "本机布局预览.msg", "1024", "2026-10-26 10:00:00", "0%", "未执行读取", "本机示例"
        }));
        using var renderer = new Renderer(form, viewport);
        string[] names = { "LV_File", "HisDataView", "FileView" };
        for (int mode = 0; mode < names.Length; mode++)
        {
            form.GetType().GetMethod("ShowMode", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(form, new object[] { mode });
            string name = names[mode];
            Scene("history-" + mode, form, renderer, previews, failures, () =>
            {
                W.DataGrid table = NativeTable(Field<Control>(form, name));
                AssertTable(table, viewport.Width * 0.57, viewport.Height * 0.68, "History " + name);
                foreach (string other in names.Where(item => item != name))
                {
                    Assert(!Field<Control>(form, other).Element.IsVisible, "History modes overlap: " + other);
                }
                Assert(Field<Control>(form, "btn_CallDir").Element.IsVisible, "History directory action is missing.");
            });
        }
        form.Close();
    }

    private static void Calibration(Size viewport, string previews, List<string> failures)
    {
        using var form = new CDataCheckForm();
        form.ActivateScreen();
        Assert(CGlobal.m_Device.m_AC_Checkdata.Count == 16 && CGlobal.m_Device.m_Phasedata.Count == 16
            && CGlobal.m_Device.m_DC_Checkdata.Count == 4, "Reopening calibration accumulated data groups.");
        using var renderer = new Renderer(form, viewport);
        string[] buttons = { "Btn_Check", "Btn_DC_Check", "BtnCallPhase" };
        string[] names = { "LV_AC_Check", "LV_DC_Check", "LV_Phase" };
        for (int mode = 0; mode < names.Length; mode++)
        {
            Field<Control>(form, buttons[mode]).PerformClick();
            string name = names[mode];
            Scene("calibration-" + mode, form, renderer, previews, failures, () =>
            {
                W.DataGrid table = NativeTable(Field<Control>(form, name));
                AssertTable(table, viewport.Width * 0.86, viewport.Height * 0.62, "Calibration " + name);
                Assert(table.Columns.All(column => column.ActualWidth >= 58), "Calibration columns are compressed.");
                Assert(table.RowHeight >= 28 || double.IsNaN(table.RowHeight), "Calibration rows are too short.");
                Assert(table.Items.Count == new[] { 18, 4, 6 }[mode], "Calibration contains duplicate or missing rows.");
                foreach (string other in names.Where(item => item != name))
                {
                    Assert(!Field<Control>(form, other).Element.IsVisible, "Calibration mode action did not hide " + other);
                }
            });
        }
        form.Close();
    }

    private static void CalibrationReopen()
    {
        CGlobal.m_Device.m_AC_Checkdata[0][0].m_Ratio = 123;
        CGlobal.m_Device.m_DC_Checkdata[0].Is_Check = 1;
        CGlobal.m_Device.m_Phasedata[0][0].m_Value = 45;
        using var form = new CDataCheckForm();
        form.ActivateScreen();
        Assert(CGlobal.m_Device.m_AC_Checkdata.Count == 16 && CGlobal.m_Device.m_Phasedata.Count == 16
            && CGlobal.m_Device.m_DC_Checkdata.Count == 4, "Calibration data grew when reopened.");
        Assert(CGlobal.m_Device.m_AC_Checkdata[0][0].m_Ratio == 123
            && CGlobal.m_Device.m_DC_Checkdata[0].Is_Check == 1
            && CGlobal.m_Device.m_Phasedata[0][0].m_Value == 45, "Reopening calibration discarded session values.");
        form.Close();
    }

    private static void Realtime(Size viewport, string previews, List<string> failures)
    {
        using var form = new CRealCtrlForm();
        form.ActivateScreen();
        using var renderer = new Renderer(form, viewport);
        Scene("realtime", form, renderer, previews, failures, () =>
        {
            foreach (string name in new[] { "LV_YX", "LV_YC", "LV_DD" })
            {
                AssertTable(NativeTable(Field<Control>(form, name)), viewport.Width * 0.25, viewport.Height * 0.75, name);
            }
            Assert(Field<ListView>(form, "LV_YC").Items.Count == 400, "Realtime layout lost telemetry rows.");
        });
        form.Close();
    }

    private static void ParameterSelection(Size viewport, string previews, List<string> failures)
    {
        var group = new CParaGroupTable { m_GroupName = "本机示例参数", m_GroupIndex = 1 };
        group.m_ParaDataList.Add(new CParaCtrl { m_ParaNo = 1, m_ParaAddress = 100, m_ParaName = "本机示例参数一" });
        group.m_ParaDataList.Add(new CParaCtrl { m_ParaNo = 2, m_ParaAddress = 101, m_ParaName = "本机示例参数二" });
        var root = new CParaRootTable { m_RootName = "布局预览" };
        root.m_GroupList.Add(group);
        var operation = new CParaListOperation();
        operation.m_ParaRootList.Add(root);
        using var form = new CGetSelectParaForm { m_pNowPara = operation, m_SelectGroup = new CParaGroupTable() };
        form.ActivateScreen();
        var tree = Field<TreeView>(form, "ParaGroupTree");
        tree.Nodes[0].Expand();
        tree.SelectedNode = tree.Nodes[0].Nodes[0];
        using var renderer = new Renderer(form, viewport);
        Scene("parameter-selection", form, renderer, previews, failures, () =>
        {
            foreach (string name in new[] { "ParaListView", "HaveSelectView" })
            {
                AssertTable(NativeTable(Field<Control>(form, name)), viewport.Width * 0.25, viewport.Height * 0.75, name);
            }
            Assert(Field<ListView>(form, "ParaListView").Items.Count == 2, "Parameter preview did not load its local sample.");
        });
        form.Close();
    }

    private static void Encryption(Size viewport, string previews, List<string> failures)
    {
        using var form = new CSecrteKeyForm();
        form.ActivateScreen();
        using var renderer = new Renderer(form, viewport);
        var tabs = Field<CNormalTabControl>(form, "encryptionTabs");
        for (int page = 0; page < 3; page++)
        {
            tabs.SelectedIndex = page;
            Scene("encryption-" + page, form, renderer, previews, failures, () =>
            {
                AssertTable(NativeTable(Field<Control>(form, "EncodeParaGrid")), viewport.Width * 0.44, viewport.Height * 0.52, "Encryption results");
                FrameworkElement log = Field<Control>(form, "ResultMsg").Element;
                Assert(log.IsVisible && log.ActualWidth > viewport.Width * 0.44 && log.ActualHeight > viewport.Height * 0.2,
                    "Encryption log is compressed.");
                Assert(!CGlobal.m_Device.m_bOnlineStatus, "Encryption layout connected to a device.");
            });
        }
        form.Close();
    }

    private static void Dialogs(string previews, List<string> failures)
    {
        CGlobal.strDevModulList = new[] { "DTU", "FTU" };
        Type[] types = { typeof(CAddCom), typeof(CAddGateWay), typeof(CAddHost), typeof(CAddIPForm), typeof(CAddTermForm), typeof(CDownFileForm),
            typeof(CFileTimeForm), typeof(CFixDataForm), typeof(CFTUFastUpdate), typeof(CGuHuaForm), typeof(CVersionForm), typeof(CWaitForm), typeof(CUpdataForm), typeof(CSFTPLogin) };
        foreach (Type type in types)
        {
            using var form = (Form)(type.GetConstructor(Type.EmptyTypes) != null
                ? Activator.CreateInstance(type) : Activator.CreateInstance(type, new object[] { "布局预览" }));
            form.ActivateScreen();
            var viewport = new Size(Math.Max(480, form.ClientSize.Width), Math.Max(240, form.ClientSize.Height));
            using var renderer = new Renderer(form, viewport);
            Scene("dialog-" + type.Name, form, renderer, previews, failures, () => { });
            form.Close();
        }
    }

    private static void Modules(Size viewport, string previews, List<string> failures)
    {
        using var form = new CommuCFG();
        form.ActivateScreen();
        using var renderer = new Renderer(form, viewport);
        var tabs = Field<CNormalTabControl>(form, "moduleTabs");
        string[] names = { "SetIPGrid", "SetGateWayGrid", "SetMacGrid", "SetComGrid", "Set4GGrid", "SetHostGrid", "SetDevGrid" };
        Assert(tabs.TabPages.Count == names.Length, "Communication module tabs are missing.");
        for (int page = 0; page < names.Length; page++)
        {
            tabs.SelectedIndex = page;
            string name = names[page];
            Scene("modules-" + page, form, renderer, previews, failures, () =>
            {
                W.DataGrid table = NativeTable(Field<Control>(form, name));
                AssertTable(table, viewport.Width * 0.86, viewport.Height * 0.67, "Communication module " + name);
                Assert(table.Columns.All(column => column.ActualWidth >= 45), "Communication module columns are compressed: " + name);
                foreach (string other in names.Where(item => item != name))
                {
                    Assert(!Field<Control>(form, other).Element.IsVisible, "Communication module tab selection retained " + other);
                }
            });
        }
        form.Close();
    }

    private static void Scene(string name, Form form, Renderer renderer, string previews, List<string> failures, Action verify)
    {
        renderer.Layout();
        string size = ((int)renderer.Viewport.Width).ToString(CultureInfo.InvariantCulture) + "x" + ((int)renderer.Viewport.Height).ToString(CultureInfo.InvariantCulture);
        string scene = name + "-" + size;
        renderer.Save(Path.Combine(previews, scene + ".png"));
        try
        {
            AssertReadableControls(form);
            AssertOuterScrolling(renderer.Root);
            verify();
            Console.WriteLine("PASS layout: " + scene);
        }
        catch (Exception error)
        {
            failures.Add(scene + ": " + error.Message);
            Console.WriteLine("FAIL layout: " + scene + ": " + error.Message);
        }
    }

    private static void Conversion(Size viewport, string previews, List<string> failures)
    {
        using var form = new CDataTranForm();
        form.ActivateScreen();
        Field<RichTextBox>(form, "SourceText").Text = string.Join(Environment.NewLine, Enumerable.Repeat("00 00 80 3F", 36));
        Field<Control>(form, "BtTran1").PerformClick();
        using var renderer = new Renderer(form, viewport);
        Scene("conversion", form, renderer, previews, failures, () =>
        {
            foreach (string name in new[] { "SourceText", "ItemText" })
            {
                FrameworkElement text = Field<Control>(form, name).Element;
                Assert(text.ActualWidth >= viewport.Width * 0.9 && text.ActualHeight >= viewport.Height * 0.34,
                    "Conversion text does not use the available area: " + name);
            }
            string[] values = Field<RichTextBox>(form, "ItemText").Text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            Assert(values.Length == 36 && values.All(value => value == "1"), "The conversion toolbar action lost its route.");
        });
        form.Close();
    }

    private static void FileTransfer(Size viewport, string previews, List<string> failures)
    {
        using var form = new CSFTPMainForm();
        // Load reads the remote directory. Render the view with local sample rows only.
        for (int index = 1; index <= 3; index++)
        {
            form.GetType().GetMethod("InitLV" + index, BindingFlags.Instance | BindingFlags.NonPublic).Invoke(form, null);
        }
        foreach (string name in new[] { "LV1", "LV2" })
        {
            var table = Field<ListView>(form, name);
            table.Items.Add(new ListViewItem(new[] { "本机布局预览.txt", "文件", "1024" }));
            table.Items.Add(new ListViewItem(new[] { "离线示例目录", "文件夹", "—" }));
        }
        Field<ListView>(form, "LV3").Items.Add(new ListViewItem(new[] { "本机布局预览.txt", "本机示例", "离线示例", "未执行传输", "—" }));
        Field<TextBox>(form, "FilePath").Text = "本机示例路径（仅界面预览）";
        Field<TextBox>(form, "RemoteFilePath").Text = "离线示例路径（未连接装置）";
        using var renderer = new Renderer(form, viewport);
        Scene("file-transfer", form, renderer, previews, failures, () =>
        {
            foreach (string name in new[] { "LV1", "LV2" })
            {
                AssertTable(NativeTable(Field<Control>(form, name)), viewport.Width * 0.34, viewport.Height * 0.42, "File browser " + name);
            }
            AssertTable(NativeTable(Field<Control>(form, "LV3")), viewport.Width * 0.9, viewport.Height * 0.22, "Transfer history");
            Assert(form.test == null, "The file-transfer preview created a remote connection.");
        });
    }

    private static void AssertReadableControls(Control parent)
    {
        foreach (Control model in Models(parent))
        {
            FrameworkElement element = model.CreatedElement;
            if (element == null || !element.IsVisible)
            {
                continue;
            }
            if (model is TextBox text && !text.Multiline || model is ComboBox)
            {
                Assert(element.ActualHeight >= 28, "Input text is vertically clipped: " + model.Name + " (" + element.ActualHeight + ").");
            }
            if (element is W.Button button && button.Content is string caption)
            {
                var required = new FormattedText(caption, CultureInfo.CurrentUICulture, button.FlowDirection,
                    new Typeface(button.FontFamily, button.FontStyle, button.FontWeight, button.FontStretch), button.FontSize,
                    Brushes.Black, VisualTreeHelper.GetDpi(button).PixelsPerDip);
                Assert(required.WidthIncludingTrailingWhitespace + button.Padding.Left + button.Padding.Right <= button.ActualWidth + 2,
                    "Button caption is clipped: " + model.Name + " / " + caption);
            }
            if (model != parent && (model is Label || model is Button || model is TextBox || model is ComboBox))
            {
                Rect bounds = element.TransformToAncestor(parent.Element).TransformBounds(new Rect(element.RenderSize));
                Assert(bounds.Left >= -2 && bounds.Top >= -2 && bounds.Right <= parent.Element.ActualWidth + 2 && bounds.Bottom <= parent.Element.ActualHeight + 2,
                    "Control extends beyond the window: " + model.Name);
            }
            if (element is W.TextBlock label && !string.IsNullOrWhiteSpace(label.Text))
            {
                var required = new FormattedText(label.Text, CultureInfo.CurrentUICulture, label.FlowDirection,
                    new Typeface(label.FontFamily, label.FontStyle, label.FontWeight, label.FontStretch), label.FontSize,
                    Brushes.Black, VisualTreeHelper.GetDpi(label).PixelsPerDip);
                if (label.TextWrapping != TextWrapping.NoWrap)
                {
                    required.MaxTextWidth = Math.Max(1, label.ActualWidth + 1);
                }
                else
                {
                    Assert(required.WidthIncludingTrailingWhitespace <= label.ActualWidth + 1,
                        "Label text is horizontally clipped: " + model.Name + " / " + label.Text);
                }
                Assert(required.Height <= label.ActualHeight + 1,
                    "Label text is vertically clipped: " + model.Name + " / " + label.Text
                    + " (required=" + required.Height + ", actual=" + label.ActualHeight + ", width=" + label.ActualWidth + ").");
            }
        }
    }

    private static IEnumerable<Control> Models(Control model)
    {
        yield return model;
        foreach (Control child in model.Controls)
        {
            foreach (Control descendant in Models(child))
            {
                yield return descendant;
            }
        }
        if (model is SplitContainer split)
        {
            foreach (Control descendant in Models(split.Panel1).Concat(Models(split.Panel2)))
            {
                yield return descendant;
            }
        }
    }

    private static void AssertOuterScrolling(DependencyObject root)
    {
        foreach (W.ScrollViewer scroll in Visuals(root).OfType<W.ScrollViewer>().Where(view => view.IsVisible))
        {
            if (Ancestors(scroll).Any(parent => parent is W.DataGrid || parent is W.TreeView || parent is System.Windows.Controls.Primitives.TextBoxBase || parent is W.ComboBox))
            {
                continue;
            }
            Assert(scroll.ScrollableWidth < 1 && scroll.ScrollableHeight < 1,
                "Page has unnecessary outer scrolling: " + scroll.ScrollableWidth + " x " + scroll.ScrollableHeight);
        }
    }

    private static IEnumerable<DependencyObject> Visuals(DependencyObject parent)
    {
        for (int index = 0; index < VisualTreeHelper.GetChildrenCount(parent); index++)
        {
            DependencyObject child = VisualTreeHelper.GetChild(parent, index);
            yield return child;
            foreach (DependencyObject descendant in Visuals(child))
            {
                yield return descendant;
            }
        }
    }

    private static IEnumerable<DependencyObject> Ancestors(DependencyObject child)
    {
        DependencyObject parent = VisualTreeHelper.GetParent(child);
        while (parent != null)
        {
            yield return parent;
            parent = VisualTreeHelper.GetParent(parent);
        }
    }

    private static W.DataGrid NativeTable(Control model)
    {
        return model.Element as W.DataGrid ?? ((W.Grid)model.Element).Children.OfType<W.DataGrid>().Single();
    }

    private static void AssertTable(W.DataGrid table, double width, double height, string name)
    {
        Assert(table.IsVisible && table.ActualWidth >= width && table.ActualHeight >= height,
            name + " does not use the available area: " + table.ActualWidth + " x " + table.ActualHeight);
        Console.WriteLine(name + " table area: " + table.ActualWidth.ToString("F0", CultureInfo.InvariantCulture) + " x " + table.ActualHeight.ToString("F0", CultureInfo.InvariantCulture));
    }

    private static T Field<T>(object target, string name)
    {
        return (T)target.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(target);
    }

    private static void Assert(bool condition, string message)
    {
        if (!condition)
        {
            throw new InvalidOperationException(message);
        }
    }

    private static void Pump()
    {
        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.ContextIdle, new Action(() => { }));
    }

    private sealed class Renderer : IDisposable
    {
        private readonly HwndSource host;
        internal FrameworkElement Root { get; }
        internal Size Viewport { get; }

        internal Renderer(Form form, Size viewport)
        {
            Viewport = viewport;
            var background = new W.Border
            {
                Background = new SolidColorBrush(Color.FromRgb(241, 245, 248)),
                Child = form.Element
            };
            System.Windows.Documents.TextElement.SetFontFamily(background, new FontFamily("Microsoft YaHei UI"));
            System.Windows.Documents.TextElement.SetFontSize(background, 13);
            Root = background;
            host = new HwndSource(new HwndSourceParameters("TerminalWorkbench layout renderer")
            {
                Width = (int)viewport.Width,
                Height = (int)viewport.Height,
                PositionX = -32000,
                PositionY = -32000,
                WindowStyle = unchecked((int)0x90000000)
            });
            host.RootVisual = Root;
            Layout();
        }

        internal void Layout()
        {
            Pump();
            Root.Measure(Viewport);
            Root.Arrange(new Rect(new Point(), Viewport));
            Root.UpdateLayout();
        }

        internal void Save(string path)
        {
            DpiScale dpi = VisualTreeHelper.GetDpi(Root);
            var bitmap = new RenderTargetBitmap((int)Math.Ceiling(Viewport.Width * dpi.DpiScaleX),
                (int)Math.Ceiling(Viewport.Height * dpi.DpiScaleY), 96 * dpi.DpiScaleX, 96 * dpi.DpiScaleY, PixelFormats.Pbgra32);
            bitmap.Render(Root);
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            using var output = File.Create(path);
            encoder.Save(output);
        }

        public void Dispose()
        {
            host.Dispose();
        }
    }
}
