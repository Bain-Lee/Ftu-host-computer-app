# 基线离线回归

这是一组独立 x86 控制台检查，不依赖测试框架，不加入 WinForms 工程的编译清单。覆盖：

- SQLite 托管和原生依赖配合使用的内存建表、写入和查询。
- 空 Socket、已关闭 Socket 的返回值及锁释放。
- 接收缓存的空、连续、跨尾、满容量、消费、长度不一致及复制异常路径。
- 104 心跳在 180 秒前后、跨多个分钟及未启动状态下的报文行为。
- S 帧的 15 秒等待、跨分钟等待和累计四帧触发。

共 18 项。异常和长度不一致路径通过反射设置内部状态；锁检查使用 `Monitor.IsEntered`，会在报告失败前释放测试线程遗留的锁。时间检查直接设置计时起点，无须等待真实超时。没有进行并发压力测试。

## 执行

在仓库根目录的 Visual Studio Developer PowerShell 中，先构建，再检查对应输出：

```powershell
msbuild .\DtuTestManager.sln /t:Build /p:Configuration=Debug '/p:Platform=Any CPU' /warnaserror /nr:false
.\tests\Run-Regression.ps1 -BuildDirectory .\DtuTestManager\bin\Debug

msbuild .\DtuTestManager.sln /t:Build /p:Configuration=Release '/p:Platform=Any CPU' /warnaserror /nr:false
.\tests\Run-Regression.ps1 -BuildDirectory .\DtuTestManager\bin\Release
```

脚本使用 Windows 自带的 .NET Framework x86 编译器，按告警级别 4 编译检查程序，并将告警作为错误。应用 EXE 和同目录 DLL 复制到一个新建的临时目录，测试在该目录运行；不会启动 `Program.Main` 或任何窗体，不打开串口、连接网络、读取现场 INI / 数据库或调用加密设备。SQLite 只操作内存数据库。

成功时显示 18 条 `PASS` 和 `Failures: 0`；编译失败或任一检查失败时脚本抛出错误。生成的测试程序保留在输出提示的临时目录。

需要强制验证完整编译时，使用 `AGENTS.md` 中的隔离构建方法。不要整体删除 `bin`，其中包含工程直接引用的依赖及运行资源。

## 验证边界

这些检查验证具体修复与依赖装载，不能替代 TeeChart ActiveX、实际配置库、完整界面和真实终端的回归。
