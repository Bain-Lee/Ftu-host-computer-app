using System;
using System.Collections.Generic;

namespace TerminalWorkbench.Core
{
    public sealed class DeviceSnapshot
    {
        public static readonly DeviceSnapshot Empty = new DeviceSnapshot(false, false, false,
            string.Empty, string.Empty, string.Empty, new TerminalPoint[0], new TerminalPoint[0], new TerminalPoint[0]);

        public DeviceSnapshot(bool isConfigured, bool isChannelOpen, bool isConnected,
            string deviceName, string protocol, string connection,
            TerminalPoint[] analogPoints, TerminalPoint[] digitalPoints, TerminalPoint[] energyPoints)
        {
            IsConfigured = isConfigured;
            IsChannelOpen = isChannelOpen;
            IsConnected = isConnected;
            DeviceName = deviceName;
            Protocol = protocol;
            Connection = connection;
            AnalogPoints = Array.AsReadOnly(analogPoints);
            DigitalPoints = Array.AsReadOnly(digitalPoints);
            EnergyPoints = Array.AsReadOnly(energyPoints);
        }

        public bool IsConfigured { get; }
        public bool IsChannelOpen { get; }
        public bool IsConnected { get; }
        public string DeviceName { get; }
        public string Protocol { get; }
        public string Connection { get; }
        public IReadOnlyList<TerminalPoint> AnalogPoints { get; }
        public IReadOnlyList<TerminalPoint> DigitalPoints { get; }
        public IReadOnlyList<TerminalPoint> EnergyPoints { get; }
    }
}
