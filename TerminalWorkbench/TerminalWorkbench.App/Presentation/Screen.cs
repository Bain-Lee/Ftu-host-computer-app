using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Threading;

namespace TerminalWorkbench.Presentation
{
    public class Form : Control
    {
        private Window window;
        private bool loaded;
        private bool closing;
        private bool relocatingWindow;
        private DialogResult dialogResult;
        private readonly List<IDisposable> owned = new List<IDisposable>();
        public event EventHandler Load;
        public event FormClosingEventHandler FormClosing;
        public event FormClosedEventHandler FormClosed;
        public event EventHandler Resize;
        public System.Drawing.Size ClientSize { get; set; } = new System.Drawing.Size(1000, 700);
        public bool TopLevel { get; set; } = true;
        public DialogResult DialogResult
        {
            get => dialogResult;
            set
            {
                dialogResult = value;
                if (value != DialogResult.None && window != null && !closing)
                    Close();
            }
        }
        public bool DoubleBuffered
        {
            get; set;
        }
        public bool MinimizeBox { get; set; } = true;
        public bool MaximizeBox { get; set; } = true;
        public FormStartPosition StartPosition
        {
            get; set;
        }
        internal void Own(IDisposable item) => owned.Add(item);

        public Form()
        {
            Size = new System.Drawing.Size(1000, 700);
        }

        internal void ActivateScreen()
        {
            if (loaded)
                return;
            loaded = true;
            Load?.Invoke(this, EventArgs.Empty);
        }

        public void Show()
        {
            if (Parent != null || !TopLevel)
            {
                Visible = true;
                ActivateScreen();
                return;
            }
            GetWindow().Show();
        }

        public DialogResult ShowDialog()
        {
            GetWindow().ShowDialog();
            return DialogResult;
        }

        public void Hide()
        {
            Visible = false;
            window?.Hide();
        }

        protected Window GetWindow()
        {
            if (window != null)
                return window;
            window = new Window
            {
                Title = Text,
                Width = Math.Min(Math.Max(480, ClientSize.Width + 32), SystemParameters.WorkArea.Width),
                Height = Math.Min(Math.Max(240, ClientSize.Height + 64), SystemParameters.WorkArea.Height),
                MinWidth = 480,
                MinHeight = 240,
                Content = Element,
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                FontFamily = new System.Windows.Media.FontFamily("Microsoft YaHei UI"),
                FontSize = 13,
                Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(241, 245, 248))
            };
            Window main = System.Windows.Application.Current.MainWindow;
            if (main != null && main != window && main.IsVisible)
                window.Owner = main;
            window.Loaded += (_, _) => ActivateScreen();
            window.SizeChanged += (_, _) => { if (loaded) Resize?.Invoke(this, EventArgs.Empty); };
            window.Closing += (_, e) =>
            {
                if (closing || relocatingWindow)
                    return;
                var args = new FormClosingEventArgs();
                FormClosing?.Invoke(this, args);
                e.Cancel = args.Cancel;
                if (!e.Cancel)
                    FinishClose();
            };
            window.Closed += (_, _) => { if (!relocatingWindow) Dispose(); };
            return window;
        }
        protected void ReleaseWindowForDocking()
        {
            if (window == null)
                return;
            relocatingWindow = true;
            try
            {
                Window previous = window;
                window = null;
                previous.Content = null;
                previous.Close();
            }
            finally { relocatingWindow = false; }
        }

        public void Close()
        {
            if (closing || IsDisposed)
                return;
            var args = new FormClosingEventArgs();
            FormClosing?.Invoke(this, args);
            if (args.Cancel)
                return;
            FinishClose();
            window?.Close();
            Parent?.Controls.Remove(this);
            Dispose();
        }

        private void FinishClose()
        {
            closing = true;
            foreach (IDisposable item in owned)
                item.Dispose();
            var message = new Message { Msg = 0x18 };
            WndProc(ref message);
            FormClosed?.Invoke(this, new FormClosedEventArgs());
        }

        protected override void Dispose(bool disposing)
        {
            if (loaded && !closing)
            {
                Close();
                return;
            }
            foreach (IDisposable item in owned)
                item.Dispose();
            base.Dispose(disposing);
        }

        protected virtual void WndProc(ref Message message)
        {
        }
        internal void Receive(ref Message message) => WndProc(ref message);
    }

    public struct Message
    {
        public int Msg;
        public IntPtr WParam;
        public IntPtr LParam;
        public IntPtr Result;
    }

    public static class UiMessages
    {
        private static readonly ConcurrentDictionary<int, WeakReference<Control>> targets = new ConcurrentDictionary<int, WeakReference<Control>>();
        internal static void Register(Control control) => targets[control.Handle.ToInt32()] = new WeakReference<Control>(control);
        internal static void Unregister(Control control) => targets.TryRemove(control.Handle.ToInt32(), out _);
        internal static Control Find(IntPtr id) => targets.TryGetValue(id.ToInt32(), out var entry) && entry.TryGetTarget(out var target) ? target : null;
        public static int Post(int id, int message, int wParam, int lParam)
        {
            if (System.Windows.Application.Current == null)
                return 0;
            System.Windows.Application.Current.Dispatcher.BeginInvoke(() => Deliver(id, message, wParam, lParam));
            return 1;
        }
        public static int Send(int id, int message, int wParam, int lParam)
        {
            if (System.Windows.Application.Current == null)
                return 0;
            System.Windows.Application.Current.Dispatcher.Invoke(() => Deliver(id, message, wParam, lParam));
            return 1;
        }
        private static void Deliver(int id, int number, int wParam, int lParam)
        {
            if (Find(new IntPtr(id)) is not Form screen || screen.IsDisposed)
                return;
            var message = new Message { Msg = number, WParam = new IntPtr(wParam), LParam = new IntPtr(lParam) };
            screen.Receive(ref message);
        }
    }

    public sealed class Timer : IDisposable
    {
        private bool disposed;
        private readonly DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Background);
        public Timer()
        {
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += (sender, e) => { if (!disposed) Tick?.Invoke(this, e); };
        }
        public event EventHandler Tick;
        public bool Enabled
        {
            get => timer.IsEnabled; set
            {
                if (value)
                    Start();
                else
                    Stop();
            }
        }
        public int Interval
        {
            get => (int)timer.Interval.TotalMilliseconds; set => timer.Interval = TimeSpan.FromMilliseconds(value);
        }
        public void Start()
        {
            if (!disposed)
                timer.Start();
        }
        public void Stop() => timer.Stop();
        public void Dispose()
        {
            disposed = true;
            timer.Stop();
        }
    }

    public delegate void FormClosingEventHandler(object sender, FormClosingEventArgs e);
    public delegate void FormClosedEventHandler(object sender, FormClosedEventArgs e);
    public class FormClosingEventArgs : CancelEventArgs
    {
    }
    public class FormClosedEventArgs : EventArgs
    {
    }
    public enum DialogResult
    {
        None, OK, Cancel, Yes, No
    }
    public enum MessageBoxButtons
    {
        OK, OKCancel, YesNo, YesNoCancel, RetryCancel, AbortRetryIgnore
    }
    public enum MessageBoxIcon
    {
        None, Error, Question, Warning, Information
    }
    public enum MessageBoxDefaultButton
    {
        Button1, Button2, Button3
    }
    public enum MessageBoxOptions
    {
        DefaultDesktopOnly, ServiceNotification
    }
    public enum FormStartPosition
    {
        Manual, CenterScreen, CenterParent
    }
    public static class MessageBox
    {
        internal static Func<string, string, DialogResult> DialogHandler
        {
            get; set;
        }
        public static DialogResult Show(string text, string caption = "提示", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.None, MessageBoxDefaultButton defaultButton = MessageBoxDefaultButton.Button1, MessageBoxOptions options = MessageBoxOptions.DefaultDesktopOnly)
        {
            if (DialogHandler != null)
                return DialogHandler(text, caption);
            MessageBoxButton wpfButtons = buttons == MessageBoxButtons.YesNo ? MessageBoxButton.YesNo :
                buttons == MessageBoxButtons.YesNoCancel ? MessageBoxButton.YesNoCancel :
                buttons == MessageBoxButtons.OKCancel ? MessageBoxButton.OKCancel : MessageBoxButton.OK;
            MessageBoxImage wpfIcon = icon == MessageBoxIcon.Error ? MessageBoxImage.Error : icon == MessageBoxIcon.Warning ? MessageBoxImage.Warning :
                icon == MessageBoxIcon.Information ? MessageBoxImage.Information : icon == MessageBoxIcon.Question ? MessageBoxImage.Question : MessageBoxImage.None;
            Func<MessageBoxResult> show = () => System.Windows.MessageBox.Show(text, caption, wpfButtons, wpfIcon);
            var result = System.Windows.Application.Current.Dispatcher.Invoke(show);
            return result == MessageBoxResult.Yes ? DialogResult.Yes : result == MessageBoxResult.No ? DialogResult.No :
                result == MessageBoxResult.OK ? DialogResult.OK : DialogResult.Cancel;
        }
    }

    public class OpenFileDialog : IDisposable
    {
        protected readonly Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
        public string FileName
        {
            get => dialog.FileName; set => dialog.FileName = value;
        }
        public string Filter
        {
            get => dialog.Filter; set => dialog.Filter = value;
        }
        public string InitialDirectory
        {
            get => dialog.InitialDirectory; set => dialog.InitialDirectory = value;
        }
        public string Title
        {
            get => dialog.Title; set => dialog.Title = value;
        }
        public bool ValidateNames
        {
            get => dialog.ValidateNames; set => dialog.ValidateNames = value;
        }
        public bool CheckFileExists
        {
            get => dialog.CheckFileExists; set => dialog.CheckFileExists = value;
        }
        public bool CheckPathExists
        {
            get => dialog.CheckPathExists; set => dialog.CheckPathExists = value;
        }
        public bool Multiselect
        {
            get => dialog.Multiselect; set => dialog.Multiselect = value;
        }
        public DialogResult ShowDialog() => dialog.ShowDialog() == true ? DialogResult.OK : DialogResult.Cancel;
        public void Dispose()
        {
        }
    }
    public sealed class SaveFileDialog : IDisposable
    {
        private readonly Microsoft.Win32.SaveFileDialog dialog = new Microsoft.Win32.SaveFileDialog();
        public string FileName
        {
            get => dialog.FileName; set => dialog.FileName = value;
        }
        public string Filter
        {
            get => dialog.Filter; set => dialog.Filter = value;
        }
        public string InitialDirectory
        {
            get => dialog.InitialDirectory; set => dialog.InitialDirectory = value;
        }
        public string Title
        {
            get => dialog.Title; set => dialog.Title = value;
        }
        public DialogResult ShowDialog() => dialog.ShowDialog() == true ? DialogResult.OK : DialogResult.Cancel;
        public void Dispose()
        {
        }
    }
    public sealed class FolderBrowserDialog : IDisposable
    {
        private readonly Microsoft.Win32.OpenFolderDialog dialog = new Microsoft.Win32.OpenFolderDialog();
        public string SelectedPath
        {
            get => dialog.FolderName; set => dialog.FolderName = value;
        }
        public string Description
        {
            get => dialog.Title; set => dialog.Title = value;
        }
        public DialogResult ShowDialog() => dialog.ShowDialog() == true ? DialogResult.OK : DialogResult.Cancel;
        public void Dispose()
        {
        }
    }
    public static class Application
    {
        public static string StartupPath => AppContext.BaseDirectory;
        public static string ExecutablePath => Environment.ProcessPath;
        public static void Exit() => System.Windows.Application.Current.Shutdown();
        public static void Restart()
        {
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(Environment.ProcessPath) { UseShellExecute = true });
            Exit();
        }
        public static void DoEvents()
        {
            // Pump only the current dispatcher's queued work during the legacy
            // synchronous progress sequence; the device thread is independent.
            var frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() => frame.Continue = false));
            Dispatcher.PushFrame(frame);
        }
    }
}
