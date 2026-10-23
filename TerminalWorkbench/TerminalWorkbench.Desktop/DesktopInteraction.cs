using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using TerminalWorkbench.Desktop.ViewModels;

namespace TerminalWorkbench.Desktop
{
    public sealed class DesktopInteraction : IUserInteraction
    {
        public string SelectConfiguration(string runtimeDirectory)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "选择现有运行目录中的通信配置";
            dialog.Filter = "通信配置 (PrjSet_Main.ini)|PrjSet_Main.ini|INI 配置 (*.ini)|*.ini";
            dialog.CheckFileExists = true;
            if (!string.IsNullOrEmpty(runtimeDirectory))
            {
                dialog.InitialDirectory = runtimeDirectory;
            }
            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }

        public void OpenOriginalTool(string runtimeDirectory)
        {
            string executable = Path.Combine(runtimeDirectory, "配电终端测试软件V2.0.exe");
            if (!File.Exists(executable))
            {
                throw new FileNotFoundException("所选运行目录中没有原版程序。", executable);
            }
            Process.Start(new ProcessStartInfo(executable)
            {
                WorkingDirectory = runtimeDirectory,
                UseShellExecute = true
            });
        }
    }
}
