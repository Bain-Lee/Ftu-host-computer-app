using System.Windows;
using TerminalWorkbench.Presentation;

namespace DtuTestManager
{
    partial class CSFTPMainForm
    {
        private void ArrangeFileTransferLayout()
        {
            var localPath = new TableLayoutPanel
            {
                Name = "LocalFilePathLayout",
                ColumnCount = 3,
                RowCount = 1
            };
            localPath.ColumnWidths.Add(GridLength.Auto);
            localPath.ColumnWidths.Add(new GridLength(1, GridUnitType.Star));
            localPath.ColumnWidths.Add(GridLength.Auto);
            localPath.RowHeights.Add(GridLength.Auto);
            localPath.Controls.Add(label1, 0, 0);
            localPath.Controls.Add(FilePath, 1, 0);
            localPath.Controls.Add(button1, 2, 0);

            var remotePath = new TableLayoutPanel
            {
                Name = "RemoteFilePathLayout",
                ColumnCount = 2,
                RowCount = 1
            };
            remotePath.ColumnWidths.Add(GridLength.Auto);
            remotePath.ColumnWidths.Add(new GridLength(1, GridUnitType.Star));
            remotePath.RowHeights.Add(GridLength.Auto);
            remotePath.Controls.Add(label2, 0, 0);
            remotePath.Controls.Add(RemoteFilePath, 1, 0);

            var localFiles = new TableLayoutPanel
            {
                Name = "LocalFilesLayout",
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };
            localFiles.RowHeights.Add(GridLength.Auto);
            localFiles.RowHeights.Add(new GridLength(1, GridUnitType.Star));
            localFiles.Controls.Add(localPath, 0, 0);
            localFiles.Controls.Add(LV1, 0, 1);

            var remoteFiles = new TableLayoutPanel
            {
                Name = "RemoteFilesLayout",
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };
            remoteFiles.RowHeights.Add(GridLength.Auto);
            remoteFiles.RowHeights.Add(new GridLength(1, GridUnitType.Star));
            remoteFiles.Controls.Add(remotePath, 0, 0);
            remoteFiles.Controls.Add(LV2, 0, 1);

            var actions = new TableLayoutPanel
            {
                Name = "FileTransferActions",
                ColumnCount = 1,
                RowCount = 6
            };
            actions.RowHeights.Add(new GridLength(1, GridUnitType.Star));
            actions.RowHeights.Add(GridLength.Auto);
            actions.RowHeights.Add(GridLength.Auto);
            actions.RowHeights.Add(GridLength.Auto);
            actions.RowHeights.Add(GridLength.Auto);
            actions.RowHeights.Add(new GridLength(1, GridUnitType.Star));
            actions.Controls.Add(button2, 0, 1);
            actions.Controls.Add(button3, 0, 2);
            actions.Controls.Add(button5, 0, 3);
            actions.Controls.Add(button4, 0, 4);

            var localGroup = new GroupBox { Name = "LocalFilesGroup", Text = "本地文件" };
            localGroup.Controls.Add(localFiles);
            var remoteGroup = new GroupBox { Name = "RemoteFilesGroup", Text = "装置文件" };
            remoteGroup.Controls.Add(remoteFiles);

            var browsers = new TableLayoutPanel
            {
                Name = "FileBrowsersLayout",
                ColumnCount = 5,
                RowCount = 1
            };
            browsers.ColumnWidths.Add(new GridLength(1, GridUnitType.Star));
            browsers.ColumnWidths.Add(new GridLength(8));
            browsers.ColumnWidths.Add(GridLength.Auto);
            browsers.ColumnWidths.Add(new GridLength(8));
            browsers.ColumnWidths.Add(new GridLength(1, GridUnitType.Star));
            browsers.Controls.Add(localGroup, 0, 0);
            browsers.Controls.Add(actions, 2, 0);
            browsers.Controls.Add(remoteGroup, 4, 0);

            LV1.Dock = DockStyle.Fill;
            LV2.Dock = DockStyle.Fill;
            LV3.Dock = DockStyle.Fill;
            var transfers = new GroupBox { Name = "TransferHistoryGroup", Text = "传输记录" };
            transfers.Controls.Add(LV3);

            var layout = new TableLayoutPanel
            {
                Name = "FileTransferLayout",
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2
            };
            layout.RowHeights.Add(new GridLength(2, GridUnitType.Star));
            layout.RowHeights.Add(new GridLength(1, GridUnitType.Star));
            layout.Controls.Add(browsers, 0, 0);
            layout.Controls.Add(transfers, 0, 1);
            Controls.Clear();
            Controls.Add(layout);
        }
    }
}
