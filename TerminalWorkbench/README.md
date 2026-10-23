# FTU / DTU 配电终端工作台

本目录是 `refactor/wpf-mvvm` 分支上的 .NET 10 / WPF 迁移候选。原 `DtuTestManager.sln` 和原源码保留，`main` 仍为此前验证通过的稳定版。当前没有配置远程仓库，也没有上传。

**当前不能把这个候选称为已经完成全功能等价验收的替换版本。** 全部 54 个编译窗口的控制逻辑和入口已移植，实际视图由 WPF 控件构成；设备、加密机、SFTP 服务及现场操作的完整流程还没有实测。部分旧控制器、全局状态和业务模型中的呈现耦合仍保留，尚未完成纯 MVVM 的架构拆分。

技术选择、改动边界与剩余工作见 [REFACTORING.md](REFACTORING.md)；逐窗口清单见 [FEATURE_PARITY.md](FEATURE_PARITY.md)。

## 构建和验证

在本目录运行：

```powershell
.\Build.ps1 -Configuration Release -Verify -Publish
```

脚本使用本目录 `.tools/dotnet` 下的私有 SDK 10.0.402；不存在时从微软官方下载到本目录，不安装系统 SDK，不修改全局 PATH，不注册 COM。首次还原需要联网。构建机为 Windows x64，应用与测试程序均输出为 x86，保留旧 Jet 和厂商原生接口的位数约束。

新解决方案为 `TerminalWorkbench.sln`，只包含 `TerminalWorkbench.App` 和 `TerminalWorkbench.Regression`。原 VS2022 继续用于原解决方案；.NET 10 的完整 IDE 构建和调试需要支持该框架的 Visual Studio 版本，当前环境请使用上面的脚本。没有自动升级 Visual Studio。

输出位置：

- 默认 Release：`TerminalWorkbench.App/bin/Release/win-x86/TerminalWorkbench.exe`。
- 默认 Debug：`TerminalWorkbench.App/bin/Debug/win-x86/TerminalWorkbench.exe`。
- `-Publish` 的完整运行包：`.artifacts/publish/win-x86/`。
- 独立离线回归：`TerminalWorkbench.Regression/bin/<配置>/win-x86/TerminalWorkbench.Regression.exe`。

发布包包含 x86 .NET 桌面运行时。应保留整个发布目录；只复制 EXE 不能运行。程序的数据文件由所选工程目录提供。

## 打开现有工程

启动新版后选择原运行目录中的 `PrjSet_Main.ini`，例如 `../DtuTestManager/bin/Debug/PrjSet_Main.ini`。程序按该文件所在目录读取模板、工程参数和业务文件；厂商原生 DLL 也从该目录解析。加载工程和打开通信通道是两个动作，必须由用户点击“开始运行”才进行初次连接。

依赖仍包括已有模板 / Access 数据库、32 位 Jet OLE DB，以及加密功能需要的厂商 DLL、运行库和设备。新版不再使用 TeeChart ActiveX、WinForms 停靠 DLL 或原 EXE 来提供业务界面。没有复制、改写或自动注册现场证书、口令和厂商 DLL。

有真实终端时，遥控、固化、升级、远端文件改写仍须在明确授权的测试环境中进行。首次验收不要让两个版本同时占用同一通信通道。

## 已执行的离线检查

2026-10-24：Release 全量构建 0 错误、0 告警，告警视为错误；92 组离线检查通过，并生成自包含运行包。Debug 同样完成零告警构建与离线检查，详见验证记录。

- 54 个窗口的构造和 WPF 视图创建；此项不等同于窗口全部按钮的业务验收。
- 18 个既有接收缓存、锁释放、104 心跳与 S 帧检查。
- 112 组固定报文与 `main` 稳定版 EXE 逐字节一致，包括参数类型、遥控选执撤、写文件分包和预期错误的 101 报文。动态携带当前时间的 101 时钟读请求不纳入固定字节基准。
- 参数应答、列表编辑、单选、动态控件、监视文档关闭 / 返回停靠、模态确认、日期变化、彩色文字、趋势缩放、录波绘制和定时器释放。
- 九类真实业务表执行保存 / 重读，覆盖含单引号的配置文本；另外检查配置批处理与转发表替换失败时原数据仍保留。
- 在临时副本上打开标准 / 空 Access 模板。标准模板读取 400 个 YC、930 个 YX 和 32 个 YK；400 个 YC 完整进入表格。
- 5000 行记录的测试视口创建了 19 个行控件，证明该测试路径启用了行虚拟化；这不是全程序性能基准。

原解决方案另做隔离 Debug 构建并通过原 28 项回归，未覆盖原运行文件。

## 文件说明

- `TerminalWorkbench.App/Ported`：从显式编译清单移植的业务 / 规约控制器和 WPF 视图描述，后续直接维护这些文件。
- `TerminalWorkbench.App/Presentation`：WPF 呈现模型、原生视图、表格编辑、曲线和窗口生命周期。`Form`、`ListView` 等名字用于保持控制器调用契约，不是 WinForms 控件，也不使用 WindowsFormsHost。
- `TerminalWorkbench.Regression`：独立离线检查；不会运行产品启动流程，不打开真实通信通道。
- `tools/port_sources.py`：一次性移植工具，不参与构建；已有迁移源文件时拒绝覆盖人工完善的实现。
- `FEATURE_INVENTORY.json`：原编译窗口、事件绑定和直接任务号的静态清单。

`TerminalWorkbench.Core`、`TerminalWorkbench.Legacy`、`TerminalWorkbench.Desktop` 是范围澄清前的 net48 兼容试验稿，保留供审查，未进入新解决方案和发布包，也不计为本次迁移成果。自动审批拒绝了这些草稿的删除，理由是可能丢失中间成果，因此未继续删除或移动。
