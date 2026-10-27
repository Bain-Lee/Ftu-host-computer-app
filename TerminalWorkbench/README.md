# FTU / DTU 配电终端工作台

本目录是 `refactor/wpf-mvvm` 分支上的 .NET 10 / WPF 迁移候选。原 `DtuTestManager.sln` 和原源码保留，`main` 仍为此前验证通过的稳定版。源码已上传至 [Ftu-host-computer-app](https://github.com/Bain-Lee/Ftu-host-computer-app)，仓库默认分支为 `main`。

**当前不能把这个候选称为已经完成全功能等价验收的替换版本。** 全部 54 个编译窗口的控制逻辑和入口已移植，实际视图由 WPF 控件构成；设备、加密机、SFTP 服务及现场操作的完整流程还没有实测。部分旧控制器、全局状态和业务模型中的呈现耦合仍保留，尚未完成纯 MVVM 的架构拆分。

技术选择、改动边界与剩余工作见 [REFACTORING.md](REFACTORING.md)；逐窗口清单见 [FEATURE_PARITY.md](FEATURE_PARITY.md)。

## 构建和验证

在本目录运行：

```powershell
.\Build.ps1 -Configuration Release -Verify -Publish
```

脚本使用 `%LOCALAPPDATA%/TerminalWorkbench.BuildTools/dotnet-10.0.402` 下的私有 SDK；不存在时从微软官方下载到该用户缓存，不安装系统 SDK，不修改全局 PATH，不注册 COM。首次还原需要联网。构建机为 Windows x64，应用与测试程序均输出为 x86，保留旧 Jet 和厂商原生接口的位数约束。

新解决方案为 `TerminalWorkbench.sln`，只包含 `TerminalWorkbench.App` 和 `TerminalWorkbench.Regression`。原 VS2022 继续用于原解决方案；.NET 10 的完整 IDE 构建和调试需要支持该框架的 Visual Studio 版本，当前环境请使用上面的脚本。没有自动升级 Visual Studio。

输出位置：

- 默认 Release：`TerminalWorkbench.App/bin/Release/win-x86/TerminalWorkbench.exe`。
- 默认 Debug：`TerminalWorkbench.App/bin/Debug/win-x86/TerminalWorkbench.exe`。
- `-Publish` 的运行程序：`.artifacts/publish/win-x86/TerminalWorkbench.exe`，发布目录只有这一个文件。
- 独立离线回归：`TerminalWorkbench.Regression/bin/<配置>/win-x86/TerminalWorkbench.Regression.exe`。

发布 EXE 已内置并压缩 x86 .NET 桌面运行时、默认配置、模板和必要图片，只需复制这一个文件。WPF 使用反射与动态界面，本次没有启用不受支持的运行时裁剪。原生运行库由 .NET 在运行时自动解包到用户临时缓存，无需用户逐个整理 DLL。

## 双击启动

直接双击发布 EXE，首次启动自动创建 `%LOCALAPPDATA%/TerminalWorkbench/Workspace`，并进入标准 DTU 模板的工作台。该目录存放 `PrjSet_Main.ini`、Access 模板、工程参数、转发表等业务数据；重新打开时只补齐缺少的默认资源，保留已有配置和模板修改。默认关闭自动连接和加密，连接参数在“通信配置”中设置，由用户点击“开始运行”发起初次连接。

需要沿用原工程数据时，先关闭程序并备份该用户目录，再按原目录结构复制需要的配置和模板；厂商原生 DLL 也从该用户目录解析。不要把证书、密钥或现场配置作为默认发布资产。

依赖仍包括已有模板 / Access 数据库、32 位 Jet OLE DB，以及加密功能需要的厂商 DLL、运行库和设备。新版不再使用 TeeChart ActiveX、WinForms 停靠 DLL 或原 EXE 来提供业务界面。没有复制、改写或自动注册现场证书、口令和厂商 DLL。

有真实终端时，遥控、固化、升级、远端文件改写仍须在明确授权的测试环境中进行。首次验收不要让两个版本同时占用同一通信通道。

## 已执行的离线检查

2026-10-27：修复模板配置打开时因树首节点为空而闪退的问题。Debug / Release 均为 0 错误、0 告警，告警视为错误；各通过 92 组离线回归和 17 组自动工作流，其中包含 56 个布局渲染场景。单文件程序另在独立目录启动并正常关闭，检查范围和界面验证边界见 [VERIFICATION.md](VERIFICATION.md)。

- 54 个窗口的构造和 WPF 视图创建；此项不等同于窗口全部按钮的业务验收。
- 18 个既有接收缓存、锁释放、104 心跳与 S 帧检查。
- 112 组固定报文与 `main` 稳定版 EXE 逐字节一致，包括参数类型、遥控选执撤、写文件分包和预期错误的 101 报文。动态携带当前时间的 101 时钟读请求不纳入固定字节基准。
- 参数应答、列表编辑、单选、动态控件、监视文档关闭 / 返回停靠、模态确认、日期变化、彩色文字、趋势缩放、录波绘制和定时器释放。
- 九类真实业务表执行保存 / 重读，覆盖含单引号的配置文本；另外检查配置批处理与转发表替换失败时原数据仍保留。
- 在临时副本上打开标准 / 空 Access 模板。标准模板读取 400 个 YC、930 个 YX 和 32 个 YK；400 个 YC 完整进入表格。
- 5000 行记录的测试视口创建了 19 个行控件，证明该测试路径启用了行虚拟化；这不是全程序性能基准。
- 自动工作流在临时工程中执行主窗体真实加载、11 个业务页的 33 次切换和退出，以及 14 种独立对话框的加载 / 关闭。
- 模板配置真实加载、30 个装置分组及四类子模板浏览，另从主界面菜单连续三次打开 / 关闭；没有改写用户模板。
- 内置资源初始化可独立运行；二次初始化保留已有通信设置和模板文件时间。
- 本机 TCP 仿真覆盖 101 / 104 的分片接收、104 连续报文与遥测 / 遥信更新、链路测试应答、监听端口释放和五次重连；SFTP 只验证连接失败期间界面响应和关闭，未验证认证与文件操作。
- 原生通信表格的选择 / 编辑，以及工程参数子集保存、逗号文本和长中文字符串保存重开均通过。修复了这些工作流实际发现的列丢失、递归关闭、重复参数累积、保存条数和文本截断问题。
- 通信配置、历史、校验、模块、文本区等重点页在两种视口及 14 种小对话框的默认尺寸下检查文字、边界和表格面积，保存离屏截图；校验页重开不再累积条目并保留会话值。布局设计见 [DESIGN.md](DESIGN.md)，截图在回归输出的 `layout-previews`。

原解决方案另做隔离 Debug 构建并通过原 28 项回归，未覆盖原运行文件。

## 文件说明

- `TerminalWorkbench.App/Ported`：从显式编译清单移植的业务 / 规约控制器和 WPF 视图描述，后续直接维护这些文件。
- `TerminalWorkbench.App/Presentation`：WPF 呈现模型、原生视图、表格编辑、曲线和窗口生命周期。`Form`、`ListView` 等名字用于保持控制器调用契约，不是 WinForms 控件，也不使用 WindowsFormsHost。
- `TerminalWorkbench.Regression`：独立离线检查；工作流使用内置资源生成临时工程，运行主窗体实际加载，不打开真实设备通道。
- `tools/port_sources.py`：一次性移植工具，不参与构建；已有迁移源文件时拒绝覆盖人工完善的实现。
- `FEATURE_INVENTORY.json`：原编译窗口、事件绑定和直接任务号的静态清单。

构建会重新生成各项目 `bin/obj`，它们属于开发输出，不是需要分发的运行包。2026-10-26 已按用户本次明确授权清理两个 WPF 项目的四个 `bin/obj` 目录；SDK 移出工程目录，默认数据在用户目录，发布目录只保留一个 EXE。保留的离线预览位于 `.artifacts/preview/native-workbench-preview.png`。

`TerminalWorkbench.Core`、`TerminalWorkbench.Legacy`、`TerminalWorkbench.Desktop` 是范围澄清前的 net48 兼容试验稿，保留供审查，未进入新解决方案和发布包，也不计为本次迁移成果。自动审批拒绝了这些草稿的删除，理由是可能丢失中间成果，因此未继续删除或移动。
