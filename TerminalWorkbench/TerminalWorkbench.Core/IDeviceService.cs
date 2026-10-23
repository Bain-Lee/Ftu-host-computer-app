using System;
using System.Threading.Tasks;

namespace TerminalWorkbench.Core
{
    public interface IDeviceService : IDisposable
    {
        event EventHandler<DeviceNoticeEventArgs> Notice;

        string RuntimeDirectory { get; }
        Task LoadAsync(string configurationFile);
        Task ConnectAsync();
        Task DisconnectAsync();
        DeviceSnapshot Refresh();
    }

    public sealed class DeviceNoticeEventArgs : EventArgs
    {
        public DeviceNoticeEventArgs(string message, bool disconnectRequired)
        {
            Message = message;
            DisconnectRequired = disconnectRequired;
        }

        public string Message { get; }
        public bool DisconnectRequired { get; }
    }
}
