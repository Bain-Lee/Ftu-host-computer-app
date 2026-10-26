using System;
using System.IO;
using System.Data.Common;
using System.Windows;
using System.Windows.Threading;
using DtuTestManager;

namespace TerminalWorkbench
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            DispatcherUnhandledException += ReportRecoverableOperationError;
            string workspace = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TerminalWorkbench", "Workspace");
            RuntimePaths.Configure(DefaultWorkspace.EnsureCreated(workspace));
            var screen = new MainForm { Text = "配电终端工作台" };
            screen.Show();
        }
        private static void ReportRecoverableOperationError(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            if (e.Exception is IOException or UnauthorizedAccessException or FormatException or OverflowException or DbException
                or DllNotFoundException or EntryPointNotFoundException or BadImageFormatException)
            {
                MessageBox.Show("操作失败：" + e.Exception.Message, "操作结果", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
            }
        }
    }
}
