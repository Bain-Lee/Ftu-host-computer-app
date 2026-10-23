using System;
using System.Reflection;
using System.Windows.Forms;
using DtuTestManager;

internal static class UiRegression
{
    private sealed class TestMainForm : MainForm
    {
        protected override void OnLoad(EventArgs e)
        {
            throw new InvalidOperationException("The application load path must not run in this test.");
        }
    }

    private sealed class TestRealForm : CRealCtrlForm
    {
        protected override void OnLoad(EventArgs e)
        {
            throw new InvalidOperationException("The real-time form load path must not run in this test.");
        }
    }

    private static void Assert(bool condition, string message)
    {
        if (!condition)
        {
            throw new InvalidOperationException(message);
        }
    }

    private static int NativeListStyle(ListView list)
    {
        return MyListView.SendMessage(list.Handle, 0x1000 + 55, IntPtr.Zero, IntPtr.Zero);
    }

    private static void RecreateHandle(Control control)
    {
        typeof(Control).GetMethod("RecreateHandle", BindingFlags.Instance | BindingFlags.NonPublic)
            .Invoke(control, null);
    }

    private static void AssertNotComposited(Control control)
    {
        CreateParams parameters = (CreateParams)control.GetType()
            .GetProperty("CreateParams", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(control, null);
        Assert((parameters.ExStyle & 0x02000000) == 0, "The entire descendant tree still uses WS_EX_COMPOSITED.");
    }

    private static void InvokeReal(CRealCtrlForm form, string method, params object[] arguments)
    {
        typeof(CRealCtrlForm).GetMethod(method, BindingFlags.Instance | BindingFlags.NonPublic)
            .Invoke(form, arguments);
    }

    internal static void ListDoubleBuffer()
    {
        using (MyListView list = new MyListView())
        {
            int style = NativeListStyle(list);
            Assert((style & 0x00010000) != 0, "Native list double buffering is disabled.");
            Assert((style & 0x2F) == 0x2F, "Existing list styles changed.");
            Assert(list.OwnerDraw && list.View == View.Details, "List rendering mode changed.");
            Assert(list.Controls.Count == 2, "The cell editors must be retained.");
        }
    }

    internal static void ListDoubleBufferAfterRecreate()
    {
        using (MyListView list = new MyListView())
        {
            RecreateHandle(list);
            Assert((NativeListStyle(list) & 0x00010000) != 0, "Double buffering was lost after handle recreation.");
        }
    }

    internal static void MainWindowStyle()
    {
        using (TestMainForm form = new TestMainForm())
        {
            AssertNotComposited(form);
        }
    }

    internal static void ParameterWindowStyle()
    {
        using (CParaForm form = new CParaForm())
        {
            AssertNotComposited(form);
        }
    }

    internal static void ControlWindowStyle()
    {
        using (CYKForm form = new CYKForm())
        {
            AssertNotComposited(form);
        }
    }

    internal static void ModuleTabStyle()
    {
        using (CNormalTabControl control = new CNormalTabControl())
        {
            AssertNotComposited(control);
        }
    }

    internal static void ControlListDoubleBuffer()
    {
        using (CYKForm form = new CYKForm())
        {
            ListView list = (ListView)typeof(CYKForm)
                .GetField("LV_YK", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(form);
            Assert((NativeListStyle(list) & 0x00010000) != 0, "The control list has no native double buffering.");
            RecreateHandle(list);
            Assert((NativeListStyle(list) & 0x00010000) != 0, "Control-list buffering was lost after handle recreation.");
        }
    }

    internal static void OfflineIdleRepaint()
    {
        CDeviceClass previousDevice = CGlobal.m_Device;
        try
        {
            CGlobal.m_Device = new CDeviceClass();
            using (TestRealForm form = new TestRealForm())
            {
                form.timer1.Stop();
                IntPtr handle = form.Handle;
                int invalidations = 0;
                form.Invalidated += (sender, arguments) => invalidations++;
                for (int index = 0; index < 20; index++)
                {
                    InvokeReal(form, "timer1_Tick", null, EventArgs.Empty);
                }
                Assert(invalidations == 0, "Offline idle ticks invalidated the form " + invalidations + " times.");
            }
        }
        finally
        {
            CGlobal.m_Device = previousDevice;
        }
    }

    internal static void EmbeddedWindowLayout()
    {
        using (TestMainForm main = new TestMainForm())
        using (Form child = new Form())
        {
            Control panel = (Control)typeof(MainForm)
                .GetField("myPanelForm", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(main);
            bool dockedBeforeAdding = false;
            panel.ControlAdded += (sender, arguments) => dockedBeforeAdding = arguments.Control.Dock == DockStyle.Fill;
            typeof(MainForm).GetField("pLoadForm", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(main, child);
            main.FreshForm();
            Assert(dockedBeforeAdding, "The child window was inserted before its final docking layout was set.");
            Assert(child.Parent == panel && !child.TopLevel, "The embedded window host changed.");
        }
    }

    internal static void RealListInitialization()
    {
        CDeviceClass previousDevice = CGlobal.m_Device;
        try
        {
            CGlobal.m_Device = new CDeviceClass();
            for (int index = 0; index < 32; index++)
            {
                CGlobal.m_Device.m_YcList.Add(new CYcOption { m_YcNo = index + 1, m_YcName = "YC", m_YcValue = index });
                CGlobal.m_Device.m_YxList.Add(new CYxOption { m_YxNo = index + 1, m_YxName = "YX", m_YxValue = 0 });
                CGlobal.m_Device.m_PowerList.Add(new CPowerOption { m_PowerNo = index + 1, m_PowerName = "PW", m_PowerValue = index });
            }
            using (TestRealForm form = new TestRealForm())
            {
                form.timer1.Stop();
                InvokeReal(form, "Init_YcLV");
                InvokeReal(form, "Init_YxLV");
                InvokeReal(form, "Init_DDLV");
                foreach (string name in new string[] { "LV_YC", "LV_YX", "LV_DD" })
                {
                    ListView list = (ListView)typeof(CRealCtrlForm)
                        .GetField(name, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(form);
                    Assert(list.Items.Count == 32, name + " lost rows during initialization.");
                    Assert(list.Items[0].SubItems[1].Text.Length > 0, name + " lost its row text.");
                    Assert((NativeListStyle(list) & 0x00010000) != 0, name + " lost native double buffering.");
                }
                ListView yxList = (ListView)typeof(CRealCtrlForm)
                    .GetField("LV_YX", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(form);
                MyListView.LV_ITEM image = new MyListView.LV_ITEM();
                image.iItem = 31;
                image.iSubItem = 2;
                image.mask = MyListView.LVIF_IMAGE;
                image.iImage = -1;
                Assert(MyListView.SendMessage(yxList.Handle, MyListView.LVM_GETITEM, 0, ref image)
                    && image.iImage == 1, "Batch initialization lost a signal-state icon.");
            }
        }
        finally
        {
            CGlobal.m_Device = previousDevice;
        }
    }
}
