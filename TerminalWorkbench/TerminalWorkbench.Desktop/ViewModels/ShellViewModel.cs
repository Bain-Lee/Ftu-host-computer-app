using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Data;
using TerminalWorkbench.Core;

namespace TerminalWorkbench.Desktop.ViewModels
{
    public sealed class ShellViewModel : ObservableObject, IDisposable
    {
        private readonly IDeviceService device;
        private readonly IUserInteraction interaction;
        private readonly ICollectionView[] views;
        private DeviceSnapshot snapshot = DeviceSnapshot.Empty;
        private Task activeOperation = Task.CompletedTask;
        private bool busy;
        private bool closing;
        private string status = "载入现有工程，查看终端测点。";
        private string searchText = string.Empty;
        private int selectedPage;

        public ShellViewModel(IDeviceService device, IUserInteraction interaction)
        {
            this.device = device;
            this.interaction = interaction;
            views = new[]
            {
                CollectionViewSource.GetDefaultView(AnalogPoints),
                CollectionViewSource.GetDefaultView(DigitalPoints),
                CollectionViewSource.GetDefaultView(EnergyPoints)
            };
            foreach (ICollectionView view in views)
            {
                view.Filter = MatchesSearch;
            }
            LoadCommand = new RelayCommand(SelectWorkspace, () => !busy && !closing && !snapshot.IsChannelOpen);
            ConnectCommand = new RelayCommand(() => StartOperation(device.ConnectAsync, "正在连接终端…", "通信通道已打开。"),
                () => !busy && !closing && snapshot.IsConfigured && !snapshot.IsChannelOpen);
            DisconnectCommand = new RelayCommand(() => StartOperation(device.DisconnectAsync, "正在断开连接…", "通信通道已关闭。"),
                () => !busy && !closing && snapshot.IsChannelOpen);
            OpenOriginalCommand = new RelayCommand(OpenOriginal, () => !busy && !closing && snapshot.IsConfigured && !snapshot.IsChannelOpen);
            device.Notice += OnNotice;
        }

        public ObservableCollection<PointRow> AnalogPoints { get; } = new ObservableCollection<PointRow>();
        public ObservableCollection<PointRow> DigitalPoints { get; } = new ObservableCollection<PointRow>();
        public ObservableCollection<PointRow> EnergyPoints { get; } = new ObservableCollection<PointRow>();
        public ObservableCollection<string> Notices { get; } = new ObservableCollection<string>();
        public RelayCommand LoadCommand { get; }
        public RelayCommand ConnectCommand { get; }
        public RelayCommand DisconnectCommand { get; }
        public RelayCommand OpenOriginalCommand { get; }
        public string Status => status;
        public bool IsBusy => busy;
        public bool IsConfigured => snapshot.IsConfigured;
        public bool IsConnected => snapshot.IsConnected;
        public bool IsChannelOpen => snapshot.IsChannelOpen;
        public string DeviceName => snapshot.IsConfigured ? snapshot.DeviceName : "尚未载入工程";
        public string Protocol => snapshot.Protocol;
        public string Connection => snapshot.Connection;
        public string ConnectionState => snapshot.IsConnected ? "已连接" : snapshot.IsChannelOpen ? "通道已打开" : "未连接";
        public string AnalogCaption => "遥测  " + AnalogPoints.Count;
        public string DigitalCaption => "遥信  " + DigitalPoints.Count;
        public string EnergyCaption => "电度  " + EnergyPoints.Count;
        public string PageTitle => new[] { "遥测数据", "遥信状态", "电度数据" }[selectedPage];
        public ICollectionView SelectedPoints => views[selectedPage];
        public Task ActiveOperation => activeOperation;

        public int SelectedPage
        {
            get => selectedPage;
            set
            {
                if (SetProperty(ref selectedPage, value))
                {
                    Notify(nameof(SelectedPoints));
                    Notify(nameof(PageTitle));
                }
            }
        }

        public string SearchText
        {
            get => searchText;
            set
            {
                if (SetProperty(ref searchText, value ?? string.Empty))
                {
                    foreach (ICollectionView view in views)
                    {
                        view.Refresh();
                    }
                }
            }
        }

        private bool MatchesSearch(object item)
        {
            PointRow point = (PointRow)item;
            string query = searchText.Trim();
            return query.Length == 0 || point.Name.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0
                || point.Number.ToString().IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0
                || point.AddressText.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private void SelectWorkspace()
        {
            string file = interaction.SelectConfiguration(device.RuntimeDirectory);
            if (!string.IsNullOrEmpty(file))
            {
                StartOperation(() => device.LoadAsync(file), "正在载入终端模板…", "工程已载入，连接后可查看实时数据。");
            }
        }

        private void StartOperation(Func<Task> action, string pending, string completed)
        {
            activeOperation = RunOperationAsync(action, pending, completed);
        }

        private async Task RunOperationAsync(Func<Task> action, string pending, string completed)
        {
            busy = true;
            SetProperty(ref status, pending, nameof(Status));
            RefreshCommands();
            try
            {
                await action();
                ApplySnapshot(device.Refresh());
                Report(completed);
            }
            catch (Exception error)
            {
                ApplySnapshot(device.Refresh());
                Report(error.Message);
            }
            finally
            {
                busy = false;
                RefreshCommands();
            }
        }

        public void Refresh()
        {
            if (!busy && !closing)
            {
                ApplySnapshot(device.Refresh());
            }
        }

        private void ApplySnapshot(DeviceSnapshot next)
        {
            if (ReferenceEquals(snapshot, next))
            {
                return;
            }
            snapshot = next;
            ApplyRows(AnalogPoints, next.AnalogPoints);
            ApplyRows(DigitalPoints, next.DigitalPoints);
            ApplyRows(EnergyPoints, next.EnergyPoints);
            Notify(nameof(IsConfigured));
            Notify(nameof(IsConnected));
            Notify(nameof(IsChannelOpen));
            Notify(nameof(DeviceName));
            Notify(nameof(Protocol));
            Notify(nameof(Connection));
            Notify(nameof(ConnectionState));
            Notify(nameof(AnalogCaption));
            Notify(nameof(DigitalCaption));
            Notify(nameof(EnergyCaption));
            RefreshCommands();
        }

        private static void ApplyRows(ObservableCollection<PointRow> rows, IReadOnlyList<TerminalPoint> points)
        {
            bool rebuild = rows.Count != points.Count;
            for (int index = 0; !rebuild && index < rows.Count; index++)
            {
                rebuild = rows[index].Number != points[index].Number || rows[index].Address != points[index].Address;
            }
            if (rebuild)
            {
                rows.Clear();
                foreach (TerminalPoint point in points)
                {
                    rows.Add(new PointRow(point));
                }
                return;
            }
            for (int index = 0; index < rows.Count; index++)
            {
                rows[index].Apply(points[index]);
            }
        }

        private void OpenOriginal()
        {
            try
            {
                interaction.OpenOriginalTool(device.RuntimeDirectory);
            }
            catch (Exception error)
            {
                Report(error.Message);
            }
        }

        private void OnNotice(object sender, DeviceNoticeEventArgs notice)
        {
            Report(notice.Message);
            if (notice.DisconnectRequired && DisconnectCommand.CanExecute(null))
            {
                DisconnectCommand.Execute(null);
            }
        }

        public void Report(string message)
        {
            SetProperty(ref status, message, nameof(Status));
            Notices.Insert(0, DateTime.Now.ToString("HH:mm:ss") + "  " + message);
            if (Notices.Count > 200)
            {
                Notices.RemoveAt(Notices.Count - 1);
            }
        }

        private void RefreshCommands()
        {
            Notify(nameof(IsBusy));
            LoadCommand.Refresh();
            ConnectCommand.Refresh();
            DisconnectCommand.Refresh();
            OpenOriginalCommand.Refresh();
        }

        public async Task ShutdownAsync()
        {
            closing = true;
            RefreshCommands();
            await activeOperation;
            await device.DisconnectAsync();
        }

        public void Dispose()
        {
            device.Notice -= OnNotice;
            device.Dispose();
        }
    }
}
