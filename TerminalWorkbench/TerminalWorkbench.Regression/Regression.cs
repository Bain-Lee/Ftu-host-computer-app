using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Interop;
using System.Windows.Threading;
using System.Windows.Input;
using System.Windows.Documents;
using DtuTestManager;
using TerminalWorkbench.Presentation;
using W = System.Windows.Controls;

internal static class Regression
{
    private static int failures;
    private static int passed;
    [STAThread]
    private static int Main(string[] args)
    {
        Console.OutputEncoding = new System.Text.UTF8Encoding(false);
        if (args.Length == 1 && args[0] == "--workflow")
        {
            return WorkflowRegression.Run();
        }
        if (args.Length == 1 && args[0] == "--golden")
        {
            foreach (var frame in ProtocolGoldenCases.Capture())
                Console.WriteLine("GOLDEN " + frame.Key + "=" + frame.Value);
            return 0;
        }
        var app = new System.Windows.Application();
        app.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("pack://application:,,,/TerminalWorkbench;component/Presentation/Styles.xaml") });
        app.ShutdownMode = ShutdownMode.OnExplicitShutdown;
        CGlobal.m_RunSendMode = false;
        TerminalWorkbench.Presentation.MessageBox.DialogHandler = (_, _) => throw new InvalidOperationException("Unexpected dialog during offline regression.");
        Run("18 existing buffer and protocol cases", () => Assert(BaselineRegression.RunCore() == 0, "Core baseline failed."));
        using var inventory = JsonDocument.Parse(File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "FEATURE_INVENTORY.json")));
        foreach (var entry in inventory.RootElement.EnumerateArray())
        {
            string name = entry.GetProperty("type").GetString();
            Run("native WPF view: " + name, () => Smoke(name));
        }
        Run("no legacy UI assembly references", NoLegacyUiReferences);
        Run("SQLite transactions and rollback", SqliteTransactions);
        Run("independent configuration copies", Copies);
        Run("real-time row reuse", RealTime);
        Run("parameter reply dispatch", ParameterReply);
        Run("list editor callback", CellEdit);
        Run("data-table bool and named cells", TableEdit);
        Run("remote control reply", ControlReply);
        Run("112 fixed frames match the stable executable", GoldenFrames);
        Run("dynamic controls can be reparented", DynamicControls);
        Run("closing a docked page removes its tab", DockedClose);
        Run("modal result closes its native window", ModalResult);
        Run("failed encryption connection is reported as failure", EncryptionFailure);
        Run("handled mouse events reach business edit callbacks", EditMouseEvent);
        Run("single selection synchronizes with native grid", NativeSelection);
        Run("date-time replies update the displayed value", DateTimeReply);
        Run("rich-text appends retain per-message colors", RichText);
        Run("hex conversion accepts CRLF separated frames", HexConversion);
        Run("native monitor windows can return to their dock", DockReturn);
        Run("native trend renders and supports zoom", Trend);
        Run("waveform channels render in native WPF host", Waveform);
        Run("disposed timers cannot be restarted by queued view updates", DisposedTimer);
        foreach (Type tableType in new[] { typeof(CComOption), typeof(CBattertyAcv), typeof(CTransYcDB), typeof(CTransYxDB), typeof(CTransYkDB), typeof(CTransPowerDB), typeof(CTransSetDB), typeof(CTransCalcYxCfg), typeof(CTransProtocl) })
        {
            Run("configuration save/reload: " + tableType.Name, () => ConfigurationSave(tableType));
        }
        Run("failed configuration batch retains committed values", ConfigurationRollback);
        Run("failed route replacement retains the old route", RouteRollback);
        Run("file progress follows the current packet count", FileProgress);
        Run("5000 rows retain a virtualized viewport", VirtualizedRows);
        if (args.Length == 2 && args[0] == "--templates")
        {
            foreach (string source in Directory.GetFiles(Path.GetFullPath(args[1]), "*.mdb"))
                Run("Access template copy: " + Path.GetFileName(source), () => TemplateCopy(source));
        }
        Console.WriteLine($"Passed: {passed}; failures: {failures}");
        app.Shutdown();
        return failures == 0 ? 0 : 1;
    }
    private static void Run(string name, Action action)
    {
        try
        {
            action();
            passed++;
            Console.WriteLine("PASS " + name);
        }
        catch (Exception error) { failures++; Console.WriteLine("FAIL " + name + ": " + error); }
    }
    private static void Assert(bool condition, string message)
    {
        if (!condition)
            throw new InvalidOperationException(message);
    }
    private static T Field<T>(object target, string name) => (T)target.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).GetValue(target);
    private static void Call(object target, string name) => target.GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).Invoke(target, new object[] { null, EventArgs.Empty });
    private static void StopTimers(Form screen)
    {
        foreach (var field in screen.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
            if (field.GetValue(screen) is TerminalWorkbench.Presentation.Timer timer)
                timer.Stop();
    }
    private static void Smoke(string name)
    {
        var type = typeof(CGlobal).Assembly.GetType("DtuTestManager." + name, true);
        using var screen = (Form)(type.GetConstructor(Type.EmptyTypes) != null
            ? Activator.CreateInstance(type)
            : Activator.CreateInstance(type, new object[] { "Offline regression" }));
        StopTimers(screen);
        FrameworkElement view = screen.Element;
        view.Measure(new Size(1440, 900));
        view.Arrange(new Rect(0, 0, 1440, 900));
        foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
            if (field.GetValue(screen) is Control control)
                Assert(control.Element != null, "Missing control: " + field.Name);
    }
    private static void NoLegacyUiReferences()
    {
        var refs = typeof(CGlobal).Assembly.GetReferencedAssemblies().Select(reference => reference.Name).ToArray();
        Assert(!refs.Any(name => name == "System.Windows.Forms" || name.Contains("TeeChart") || name.Contains("Docking") || name == "配电终端测试软件V2.0"), string.Join(", ", refs));
    }
    private static void SqliteTransactions()
    {
        using var db = new CSQLite();
        Assert(db.ConnectDataBase(":memory:"), "Open failed.");
        Assert(db.ExecSql("CREATE TABLE CheckValues (Value INTEGER NOT NULL UNIQUE); INSERT INTO CheckValues VALUES (42)"), "Create failed.");
        Assert(!db.ExecSql("INSERT INTO CheckValues VALUES (42)"), "Duplicate must fail.");
        var table = (DataTable)db.SelectDataTable("SELECT Value FROM CheckValues");
        Assert(table.Rows.Count == 1 && Convert.ToInt32(table.Rows[0][0]) == 42, "Rollback failed.");
        Assert(db.GetTableName().Rows.Cast<DataRow>().Any(row => row[2].ToString() == "CheckValues"), "Table name contract changed.");
        using var transaction = db.m_TranConn.BeginTransaction();
        using var command = db.m_TranConn.CreateCommand();
        command.Transaction = transaction;
        command.CommandText = "INSERT INTO CheckValues VALUES (43)";
        command.ExecuteNonQuery();
        transaction.Rollback();
        Assert(((DataTable)db.SelectDataTable("SELECT * FROM CheckValues")).Rows.Count == 1, "External rollback failed.");
    }
    private static void Copies()
    {
        var source = new List<CTranValue> { new CTranValue { m_TranValue = "a", m_ComStringList = new[] { "x" } } };
        var copy = ModelCopies.CopyValues(source);
        copy[0].m_ComStringList[0] = "y";
        copy[0].m_TranValue = "b";
        Assert((string)source[0].m_TranValue == "a" && source[0].m_ComStringList[0] == "x", "Mutable value alias.");
        var terms = new List<CTermData> { new CTermData { m_DataName = new List<string> { "a" } } };
        ModelCopies.CopyTerminalData(terms)[0].m_DataName[0] = "b";
        Assert(terms[0].m_DataName[0] == "a", "Mutable name alias.");
    }
    private static void RealTime()
    {
        CGlobal.m_Device = new CDeviceClass();
        CGlobal.m_Device.m_YcList.Add(new CYcOption { m_YcNo = 1, m_YcName = "Test", m_YcValue = 1.25F });
        CGlobal.m_Device.m_YxList.Add(new CYxOption { m_YxNo = 1, m_YxName = "State" });
        using var screen = new CRealCtrlForm();
        StopTimers(screen);
        _ = screen.Element;
        Call(screen, "CRealCtrlForm_Load");
        var table = Field<MyListView>(screen, "LV_YC");
        Assert(table.Items.Count == 1, "Analog points not loaded.");
        var grid = (W.DataGrid)table.Element;
        Assert(grid.EnableRowVirtualization && grid.EnableColumnVirtualization, "Virtualization disabled.");
        var row = table.Items[0];
        Call(screen, "timer1_Tick");
        Assert(ReferenceEquals(table.Items[0], row), "Idle refresh rebuilt rows.");
        StopTimers(screen);
    }
    private static void ParameterReply()
    {
        using var screen = new CParaForm();
        StopTimers(screen);
        var reply = new Message { Msg = UseMessage.MY_MSG_2 };
        screen.Receive(ref reply);
        Assert(Field<ToolStripLabel>(screen, "Result").Text == "固化成功", "Commit reply lost.");
    }
    private static void CellEdit()
    {
        using var table = new MyListView();
        table.Columns.Add("Name");
        table.Columns.Add("Value");
        table.AddItem(new ListViewItem(new[] { "A", "1" }));
        table.SetCellMode(0, 1, 1, 0);
        int column = -1;
        object value = null;
        table.theProcessFun = (index, next) => { column = index; value = next; };
        table.CommitEdit(table.Items[0], 1, "2");
        Assert(column == 1 && (string)value == "2" && table.Items[0].SubItems[1].Text == "2", "Editor callback changed.");
    }
    private static void TableEdit()
    {
        var values = new DataTable();
        values.Columns.Add("选择", typeof(bool));
        values.Columns.Add("值", typeof(string));
        values.Rows.Add(false, "a");
        using var editor = new DataGridView { DataSource = values };
        _ = editor.Element;
        editor.Rows[0].Cells[0].Value = true;
        editor.Rows[0].Cells["值"].Value = "b";
        Assert((bool)values.Rows[0][0] && values.Rows[0][1].ToString() == "b", "Table values detached.");
    }
    private static void ControlReply()
    {
        CGlobal.m_Device = new CDeviceClass();
        CGlobal.m_Device.m_Ptr = new C104Ptr { theDevice = CGlobal.m_Device };
        CGlobal.m_Device.m_YkList.Add(new CYkOption { m_YkNo = 1, m_YkAddress = 0x6001, m_YkName = "Test" });
        using var screen = new CYKForm();
        StopTimers(screen);
        Call(screen, "CYKForm_Load");
        CYKForm.m_YkMsg.m_YkNo = 1;
        CYKForm.m_YkMsg.m_YkStatus = 0;
        CYKForm.m_YkMsg.m_YkValue = 0;
        var reply = new Message { Msg = UseMessage.MY_MSG_7 };
        screen.Receive(ref reply);
        Assert(Field<Button>(screen, "YK_F_Ex_btn").Enabled && Field<Button>(screen, "YK_F_C_btn").Enabled, "Execute/cancel did not unlock.");
        Assert(!Field<Button>(screen, "YK_H_Ex_btn").Enabled, "Opposite operation unlocked.");
    }
    private static void VirtualizedRows()
    {
        using var table = new MyListView();
        table.Columns.Add("Number");
        table.Columns.Add("Value");
        var grid = (W.DataGrid)table.Element;
        table.BeginUpdate();
        try
        {
            for (int i = 0; i < 5000; i++)
                table.AddItem(new ListViewItem(new[] { i.ToString(), "0" }));
        }
        finally { table.EndUpdate(); }
        using var host = Attach(grid, 900, 600);
        grid.Measure(new Size(900, 600));
        grid.Arrange(new Rect(0, 0, 900, 600));
        grid.UpdateLayout();
        int realized = Enumerable.Range(0, 5000).Count(index => grid.ItemContainerGenerator.ContainerFromIndex(index) is W.DataGridRow);
        Assert(realized > 0 && realized < 100, "Realized row count: " + realized);
        Console.WriteLine("Realized rows for 5000 records: " + realized);
    }
    private static void TemplateCopy(string source)
    {
        string directory = Path.Combine(Path.GetTempPath(), "TerminalWorkbench-template-check-" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(directory);
        string path = Path.Combine(directory, Path.GetFileName(source));
        File.Copy(source, path);
        var module = new CModuleClass { m_ModuleName = Path.GetFileNameWithoutExtension(path) };
        module.m_ModuleDB.m_Path = path;
        Assert(module.m_ModuleDB.ConnectDataBase(), "Jet provider could not open the copied template.");
        try
        {
            module.InitialzeReadOption();
            CGlobal.m_Device = new CDeviceClass { m_DeviceMsg = module };
            CGlobal.m_Device.InitialzeSetDeviceModuleOption();
            CGlobal.m_Device.InitializationReadDataList();
            Console.WriteLine($"Template points: YC={CGlobal.m_Device.m_YcList.Count}, YX={CGlobal.m_Device.m_YxList.Count}, YK={CGlobal.m_Device.m_YkList.Count}");
            if (CGlobal.m_Device.m_YcList.Count > 0)
            {
                using var main = new MainForm();
                StopTimers(main);
                _ = main.Element;
                using var realtime = new CRealCtrlForm();
                main.GetType().GetField("pLoadForm", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(main, realtime);
                main.FreshForm();
                StopTimers(realtime);
                Call(main, "Btn_Output_Click");
                var view = main.Element;
                using var host = Attach(view, 1440, 900);
                view.Measure(new Size(1440, 900));
                view.Arrange(new Rect(0, 0, 1440, 900));
                view.UpdateLayout();
                System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.ContextIdle, new Action(() => { }));
                view.UpdateLayout();
                var analogTable = Field<MyListView>(realtime, "LV_YC");
                var analogGrid = (W.DataGrid)analogTable.Element;
                int analogRows = Enumerable.Range(0, analogGrid.Items.Count).Count(index => analogGrid.ItemContainerGenerator.ContainerFromIndex(index) is W.DataGridRow);
                Console.WriteLine($"Rendered template rows: model={analogTable.Items.Count}, source={analogGrid.Items.Count}, viewport={analogRows}");
                Assert(analogTable.Items.Count == CGlobal.m_Device.m_YcList.Count && analogRows >= 10, "Template points did not reach the native viewport.");
                var bitmap = new RenderTargetBitmap(1440, 900, 96, 96, PixelFormats.Pbgra32);
                bitmap.Render(view);
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                string imagePath = Path.Combine(AppContext.BaseDirectory, "native-workbench-preview.png");
                using var image = File.Create(imagePath);
                encoder.Save(image);
                Console.WriteLine("Preview: " + imagePath);
            }
        }
        finally { module.m_ModuleDB.CloseDatabase(); }
    }
    private static HwndSource Attach(FrameworkElement element, int width, int height)
    {
        var parameters = new HwndSourceParameters("TerminalWorkbench offline renderer")
        {
            Width = width,
            Height = height,
            WindowStyle = unchecked((int)0x90000000),
            PositionX = -32000,
            PositionY = -32000
        };
        var host = new HwndSource(parameters);
        host.RootVisual = element;
        element.ApplyTemplate();
        System.Windows.Application.Current.Dispatcher.Invoke(DispatcherPriority.Render, new Action(() => { }));
        return host;
    }
    private static void GoldenFrames()
    {
        var expected = File.ReadAllLines(Path.Combine(AppContext.BaseDirectory, "ProtocolGoldenFrames.txt"));
        var current = ProtocolGoldenCases.Capture().Select(frame => "GOLDEN " + frame.Key + "=" + frame.Value).ToArray();
        Assert(expected.Length == 112 && expected.SequenceEqual(current), "A fixed protocol frame changed.");
    }
    private static void DynamicControls()
    {
        using var panel = new Panel();
        _ = panel.Element;
        panel.Controls.Add(new Label { Text = "A" });
        panel.Controls.Add(new Label { Text = "B" });
        Assert(panel.Controls.Count == 2, "Dynamic labels were lost.");
        using var tabs = new CNormalTabControl();
        _ = tabs.Element;
        tabs.TabPages.Add(new TabPage { Text = "A" });
        tabs.TabPages.Add(new TabPage { Text = "B" });
        Assert(((W.TabControl)tabs.Element).Items.Count == 2, "Dynamic tab contents were lost.");
    }
    private static void DockedClose()
    {
        using var panel = new TerminalWorkbench.Presentation.DockPanel();
        _ = panel.Element;
        using var first = new DockContent { Text = "A" };
        using var second = new DockContent { Text = "B" };
        first.Show(panel);
        second.Show(panel);
        first.Close();
        Assert(panel.Controls.Count == 1 && ((W.TabControl)panel.Element).Items.Count == 1, "Closed document remained attached.");
    }
    private static void ModalResult()
    {
        using var form = new Form();
        typeof(Form).GetMethod("GetWindow", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(form, null);
        form.DialogResult = DialogResult.OK;
        Assert(form.IsDisposed, "Setting a modal result did not close the window.");
    }
    private static void EncryptionFailure()
    {
        var encoder = new CEnCodePtr { m_SecretInter = new FailingSecretInterface() };
        Assert(!encoder.ConnectEncodeDev(), "An exception was reported as a successful connection.");
        bool threw = false;
        try
        {
            encoder.GetRand(new byte[8]);
        }
        catch (InvalidOperationException) { threw = true; }
        Assert(threw, "Native failure did not propagate to its operation boundary.");
        object sync = typeof(CEnCodePtr).GetField("m_Lock", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(encoder);
        bool entered = false;
        var worker = new System.Threading.Thread(() =>
        {
            entered = System.Threading.Monitor.TryEnter(sync, 500);
            if (entered)
                System.Threading.Monitor.Exit(sync);
        });
        worker.Start();
        worker.Join();
        Assert(entered, "Encryption failure retained its lock.");
    }
    private sealed class FailingSecretInterface : CSecretInterface
    {
        public override int Secret_ConnectHSM(string host, int port, int timeout) => throw new InvalidOperationException("Synthetic native failure");
        public override int Secret_GenRand(int connection, int length, byte[] bytes) => throw new InvalidOperationException("Synthetic native failure");
    }
    private static void EditMouseEvent()
    {
        using var table = new MyListView();
        int called = 0;
        table.DoubleClick += (_, _) => called++;
        var grid = (W.DataGrid)table.Element;
        var mouse = new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left)
        {
            RoutedEvent = UIElement.PreviewMouseLeftButtonDownEvent,
            Handled = true
        };
        typeof(MouseButtonEventArgs).GetProperty(nameof(MouseButtonEventArgs.ClickCount)).GetSetMethod(true).Invoke(mouse, new object[] { 2 });
        grid.RaiseEvent(mouse);
        Assert(called == 1, "Grid handling swallowed the business double-click event.");
    }
    private static void NativeSelection()
    {
        using var table = new MyListView();
        table.Columns.Add("A");
        table.Items.Add("1");
        table.Items.Add("2");
        var grid = (W.DataGrid)table.Element;
        table.Items[1].Selected = true;
        Assert(ReferenceEquals(grid.SelectedItem, table.Items[1]), "Single selection did not reach the grid.");
        grid.SelectedItem = table.Items[0];
        Assert(table.SelectedIndices.SequenceEqual(new[] { 0 }), "Native selection did not reach the model.");
    }
    private static void DateTimeReply()
    {
        using var picker = new DateTimePicker { Format = DateTimePickerFormat.Custom, CustomFormat = "yyyy-MM-dd HH:mm:ss" };
        var field = (W.TextBox)picker.Element;
        picker.Value = new DateTime(2020, 1, 2, 3, 4, 5);
        Assert(field.Text == "2020-01-02 03:04:05", "Date change did not update the view.");
    }
    private static void RichText()
    {
        using var model = new RichTextBox();
        var box = (W.RichTextBox)model.Element;
        model.SelectionColor = System.Drawing.Color.Red;
        model.AppendText("a");
        model.SelectionColor = System.Drawing.Color.Blue;
        model.AppendText("b");
        var runs = ((Paragraph)box.Document.Blocks.FirstBlock).Inlines.OfType<Run>().Where(run => run.Text.Length != 0).ToArray();
        Assert(model.Text == "ab" && runs.Length == 2, "Rich-text append duplicated or dropped text.");
        Assert(((SolidColorBrush)runs[0].Foreground).Color == Colors.Red && ((SolidColorBrush)runs[1].Foreground).Color == Colors.Blue, "Message colors were lost.");
    }
    private static void HexConversion()
    {
        using var form = new CDataTranForm();
        var method = typeof(CDataTranForm).GetMethod("StringToBuffer", BindingFlags.Instance | BindingFlags.NonPublic);
        var bytes = (byte[])method.Invoke(form, new object[] { "68 04\r\n43 00 00 00" });
        Assert(bytes.SequenceEqual(new byte[] { 0x68, 4, 0x43, 0, 0, 0 }), "Line breaks changed hex conversion.");
    }
    private static void DockReturn()
    {
        using var panel = new TerminalWorkbench.Presentation.DockPanel();
        _ = panel.Element;
        using var document = new DockContent();
        document.Show(panel);
        panel.Controls.Remove(document);
        typeof(Form).GetMethod("GetWindow", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(document, null);
        document.Show(panel);
        Assert(!document.IsDisposed && panel.Controls.Contains(document), "Docking disposed the live monitor.");
    }
    private static void Trend()
    {
        using var chart = new AxTChart();
        for (int i = 0; i < 200; i++)
            chart.Series(0).AddXY(i, Math.Sin(i / 10.0), i.ToString(), 0);
        var view = chart.Element;
        using var host = Attach(view, 900, 600);
        view.Measure(new Size(900, 600));
        view.Arrange(new Rect(0, 0, 900, 600));
        view.UpdateLayout();
        var wheel = new MouseWheelEventArgs(Mouse.PrimaryDevice, 0, 120) { RoutedEvent = UIElement.MouseWheelEvent };
        view.RaiseEvent(wheel);
        var bitmap = new RenderTargetBitmap(900, 600, 96, 96, PixelFormats.Pbgra32);
        bitmap.Render(view);
        double left = Field<double>(view, "left"), right = Field<double>(view, "right");
        Assert(right - left < 1, "Trend zoom did not change the viewport.");
    }
    private static void Waveform()
    {
        var wave = new CWaveFileInfo
        {
            m_ChanelCount = 2,
            m_AnCount = 2,
            m_Samp = 4000,
            m_Hz = 50,
            m_StartTime = new DateTime(2020, 1, 1).Ticks,
            m_EndTime = new DateTime(2020, 1, 1).AddSeconds(1).Ticks,
            m_strStartTime = "2020-01-01 00:00:00",
            m_strEndTime = "2020-01-01 00:00:01"
        };
        for (int channel = 0; channel < 2; channel++)
        {
            var data = new CWaveData { m_ChanelName = "I" + channel, m_ChanelType = "I" };
            for (int i = 0; i < 1600; i++)
                data.m_DataList.Add((float)Math.Sin(i / 10.0 + channel));
            wave.m_ChanelList.Add(data);
        }
        using var form = new CSoePicForm { m_File = new CFileCtrl { m_CWaveInfo = wave } };
        StopTimers(form);
        var view = form.Element;
        using var host = Attach(view, 1200, 800);
        view.Measure(new Size(1200, 800));
        view.Arrange(new Rect(0, 0, 1200, 800));
        view.UpdateLayout();
        Call(form, "CSoePicForm_Load");
        StopTimers(form);
        var bitmap = new RenderTargetBitmap(1200, 800, 96, 96, PixelFormats.Pbgra32);
        bitmap.Render(view);
        Assert(form.m_SinCurrent.Length == 2, "Wave channels were lost.");
    }
    private static void DisposedTimer()
    {
        var timer = new TerminalWorkbench.Presentation.Timer();
        timer.Dispose();
        timer.Start();
        timer.Enabled = true;
        Assert(!timer.Enabled, "A disposed page restarted its timer.");
    }
    private static void ConfigurationSave(Type tableType)
    {
        using var database = new CSQLite();
        Assert(database.ConnectDataBase(":memory:"), "Could not open fixture database.");
        var table = (CTableInterface)Activator.CreateInstance(tableType);
        table.m_SqlDataBase = database;
        table.m_TableName = "Probe";
        string columns = string.Join(",", table.m_ValueList.Select(value => value.GetCreateDesc()));
        Assert(database.ExecSql("CREATE TABLE Probe (" + columns + ")"), "Could not create the business schema.");
        table.AddNewData();
        for (int column = 0; column < table.GetColCount(); column++)
        {
            CTranValue value = table.GetTranValue(0, column);
            value.m_TranValue = value.m_ValueType == 2 ? (object)"O'Brien" : 0;
        }
        Assert(table.SaveTableContent() == 1, "Business save did not commit its row.");
        table.IniReadTableData();
        Assert(table.GetRowCount() == 1, "Saved business row did not reload.");
        for (int column = 0; column < table.GetColCount(); column++)
        {
            CTranValue value = table.GetTranValue(0, column);
            if (value.m_ValueType == 2)
                Assert((string)value.m_TranValue == "O'Brien", "Quoted configuration text did not round-trip.");
        }
    }
    private static void ConfigurationRollback()
    {
        using var database = new CSQLite();
        database.ConnectDataBase(":memory:");
        var table = new CTransSetDB { m_SqlDataBase = database, m_TableName = "Probe" };
        Assert(database.ExecSql("CREATE TABLE Probe (ID INTEGER PRIMARY KEY, DataNo INTEGER, SetVal0 TEXT, SetVal1 TEXT, ValType INTEGER)"), "Schema creation failed.");
        table.AddNewData();
        table.GetTranValue(0, 2).m_TranValue = "kept";
        Assert(table.SaveTableContent() == 1, "Initial save failed.");
        table.AddNewData();
        table.GetTranValue(0, 0).m_TranValue = 0;
        table.GetTranValue(1, 0).m_TranValue = 0;
        bool failed = false;
        try
        {
            table.SaveTableContent();
        }
        catch (Microsoft.Data.Sqlite.SqliteException) { failed = true; }
        var stored = (DataTable)database.SelectDataTable("SELECT SetVal0 FROM Probe");
        Assert(failed && stored.Rows.Count == 1 && stored.Rows[0][0].ToString() == "kept", "Failed batch changed committed values.");
    }
    private static void RouteRollback()
    {
        using var database = new CSQLite();
        database.ConnectDataBase(":memory:");
        Assert(database.ExecSql("CREATE TABLE Probe (ID INTEGER, DstNode INTEGER, SrcNode INTEGER CHECK(SrcNode>=0)); INSERT INTO Probe VALUES (1,0,3)"), "Route schema creation failed.");
        var station = new CTransStation { m_UserSqlite = database };
        var route = new CRouteTable { m_TheStation = station, m_TableName = "Probe" };
        route.m_PriData.Add(new CPriTable { SrcNode = -1 });
        bool failed = false;
        try
        {
            route.SaveDataTable();
        }
        catch (Microsoft.Data.Sqlite.SqliteException) { failed = true; }
        var stored = (DataTable)database.SelectDataTable("SELECT SrcNode FROM Probe");
        Assert(failed && stored.Rows.Count == 1 && Convert.ToInt32(stored.Rows[0][0]) == 3, "Failed route replacement deleted the existing route.");
    }
    private static void FileProgress()
    {
        using var model = new ProgressBar();
        var bar = (W.ProgressBar)model.Element;
        model.Maximum = 200;
        model.Value = 150;
        Assert(bar.Maximum == 200 && bar.Value == 150, "The view retained a stale packet-count range.");
    }
}
