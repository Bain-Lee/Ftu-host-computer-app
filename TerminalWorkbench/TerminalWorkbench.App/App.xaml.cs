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
            var picker = new Microsoft.Win32.OpenFileDialog
            {
                Title = "选择现有工程配置",
                Filter = "工程配置 (PrjSet_Main.ini)|PrjSet_Main.ini|INI 文件 (*.ini)|*.ini",
                CheckFileExists = true
            };
            if (picker.ShowDialog() != true)
            {
                Shutdown();
                return;
            }
            string config = Path.GetFullPath(picker.FileName);
            RuntimePaths.Configure(config);
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
