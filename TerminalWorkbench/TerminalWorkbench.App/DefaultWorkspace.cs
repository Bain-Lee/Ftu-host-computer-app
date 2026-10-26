using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace TerminalWorkbench
{
    internal static class DefaultWorkspace
    {
        internal static string EnsureCreated(string directory)
        {
            string workspace = Path.GetFullPath(directory);
            foreach (string name in new[] { "DefaultDevModule", "DevModuleDataBase", "工程模板", "工程参数", "转发表配置", "res" })
            {
                Directory.CreateDirectory(Path.Combine(workspace, name));
            }
            const string prefix = "TerminalWorkbench.Defaults.";
            Assembly assembly = typeof(DefaultWorkspace).Assembly;
            foreach (string resource in assembly.GetManifestResourceNames())
            {
                if (!resource.StartsWith(prefix, StringComparison.Ordinal))
                {
                    continue;
                }
                string relative = resource.Substring(prefix.Length).Replace('/', Path.DirectorySeparatorChar);
                string path = Path.Combine(workspace, relative);
                if (File.Exists(path))
                {
                    continue;
                }
                using Stream source = assembly.GetManifestResourceStream(resource);
                if (relative == "PrjSet_Main.ini")
                {
                    using var reader = new StreamReader(source, Encoding.UTF8);
                    File.WriteAllText(path, reader.ReadToEnd(), Encoding.Unicode);
                }
                else
                {
                    using Stream destination = new FileStream(path, FileMode.CreateNew, FileAccess.Write);
                    source.CopyTo(destination);
                }
            }
            return Path.Combine(workspace, "PrjSet_Main.ini");
        }
    }
}
