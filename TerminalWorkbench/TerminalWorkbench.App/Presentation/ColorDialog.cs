using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace TerminalWorkbench.Presentation
{
    public sealed class ColorDialog : IDisposable
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct ChooseColorInfo
        {
            public int Size;
            public IntPtr Owner;
            public IntPtr Instance;
            public int Color;
            public IntPtr CustomColors;
            public int Flags;
            public IntPtr CustomData;
            public IntPtr Hook;
            public IntPtr Template;
        }
        [DllImport("comdlg32.dll", CharSet = CharSet.Unicode, EntryPoint = "ChooseColorW")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ChooseColor(ref ChooseColorInfo info);
        public Color Color { get; set; } = Color.White;
        public DialogResult ShowDialog()
        {
            IntPtr palette = Marshal.AllocHGlobal(16 * sizeof(int));
            try
            {
                Marshal.Copy(new int[16], 0, palette, 16);
                var info = new ChooseColorInfo { Size = Marshal.SizeOf<ChooseColorInfo>(), Color = ColorTranslator.ToWin32(Color), CustomColors = palette, Flags = 3 };
                if (!ChooseColor(ref info))
                    return DialogResult.Cancel;
                Color = ColorTranslator.FromWin32(info.Color);
                return DialogResult.OK;
            }
            finally { Marshal.FreeHGlobal(palette); }
        }
        public void Dispose()
        {
        }
    }
}
