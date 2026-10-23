using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using DtuTestManager;

namespace TerminalWorkbench
{
    internal static class RuntimePaths
    {
        private static string workspace;
        internal static string WorkspaceDirectory
        {
            get
            {
                string directory = workspace ?? AppContext.BaseDirectory;
                return Path.EndsInDirectorySeparator(directory) ? directory : directory + Path.DirectorySeparatorChar;
            }
        }
        internal static void Configure(string configuration)
        {
            string path = Path.GetFullPath(configuration);
            workspace = Path.GetDirectoryName(path);
            Directory.SetCurrentDirectory(workspace);
            CGlobal.FileName = path;
            NativeLibrary.SetDllImportResolver(typeof(CGlobal).Assembly, ResolveVendorLibrary);
        }
        private static IntPtr ResolveVendorLibrary(string name, Assembly assembly, DllImportSearchPath? searchPath)
        {
            if (!string.Equals(name, "TZhuZhanDll.dll", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(name, "HsmPriDll.dll", StringComparison.OrdinalIgnoreCase))
                return IntPtr.Zero;
            return NativeLibrary.Load(Path.Combine(workspace, name));
        }
    }
}
