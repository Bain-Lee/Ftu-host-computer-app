# 项目协作指南

本文件适用于当前目录及所有子目录。使用中文沟通；结论应区分源码事实、静态推断与运行验证。项目分析见 `PROJECT_ANALYSIS.md`。

## 项目定位

- 这是 FTU / DTU 配电终端的 Windows 上位机程序，包含通信调试、实时监视、遥控、参数配置、文件传输、升级和规约试验功能。
- 解决方案：`DtuTestManager.sln`；唯一项目：`DtuTestManager/DtuTestManager.csproj`。
- 技术栈为 C#、WinForms、.NET Framework 4.8；项目采用传统 MSBuild 格式，不是 SDK 风格的现代 .NET 项目。
- 解决方案配置名为 `Debug|Any CPU`、`Release|Any CPU`，但两种配置的实际 `PlatformTarget` 均为 `x86`。未经专项要求，不改为 x64 / AnyCPU。
- 输出程序集名为 `配电终端测试软件V2.0`。不要根据目录日期或解决方案的 Visual Studio 2012 注释推断目标框架版本。
- 已按用户要求建立本地 Git 仓库，默认分支为 `main`；尚未配置远程仓库。

## 版本管理

- 跟踪源码、工程文件、文档、图片、现有模板数据库、`lib/SQLite` 依赖，以及工程引用的 SSH.NET / Docking DLL 和现有 TeeChart Interop DLL。`bin/Debug` 中这些受版本管理的文件通过 `.gitignore` 白名单保留，不移动或重命名。
- 忽略 Visual Studio 用户状态、`obj`、可执行运行包、日志、证书、现场 INI / 工程参数 / 转发表配置，以及未被当前工程引用的本地 `CDataBase.sdf`。忽略不代表删除本地文件。
- `bin/Debug/工程模板/.gitkeep` 保留启动代码依赖的空目录；不要当作无用文件移除。
- 本仓库设置 `core.autocrlf=false`，首个提交保留现有文件换行方式；不要借提交统一源文件换行或格式。
- 提交前检查暂存范围；不要用 `git add -f` 把忽略的现场数据或证书加入仓库。上传远程仓库另按用户明确要求进行。

## 修改前先确认

1. 明确目标、假设及可验证的成功条件；存在影响正确性的歧义时先澄清，轻微歧义按最小可逆方案推进。
2. 先查 `.csproj` 的显式 `Compile` / `EmbeddedResource` 清单，再定位窗体或业务实现。磁盘上的同名文件未必参与构建。
3. 沿现有调用链修改最小范围，不顺手重构、统一格式、改名或升级依赖。
4. 编写代码采用 Allman 风格：类型、方法及控制块的左大括号独占一行。保留原文件编码、中文内容和换行方式，不整文件重排。

## 代码导航

以下路径均相对于项目根目录。

| 位置 | 作用 |
| --- | --- |
| `DtuTestManager/Program.cs` | STA 入口，启动 `MainForm` |
| `DtuTestManager/MyForms/MainForm.cs` | 实际参与编译的主窗体；启动、模板装载、连接及功能入口 |
| `DtuTestManager/MyForms/`、`DtuTestManager/4GForms/` | 业务窗体及通信配置编辑界面 |
| `DtuTestManager/MyControls/` | 自定义 WinForms 控件 |
| `DtuTestManager/MyClass/CGlobal.cs` | 全局设备、模板、INI 配置、任务发送及 Windows 消息接口 |
| `DtuTestManager/MyClass/CDeviceClass.cs` | 当前设备、共享数据、接收环形缓存及处理线程；选择规约和通信通道 |
| `DtuTestManager/MyClass/CPtrInterface.cs`、同目录的 `C101Ptr.cs` / `C104Ptr.cs` | 公共规约行为、101 / 104 组帧、解析和任务分派 |
| `DtuTestManager/MyClass/CSerialPort.cs`、同目录的 `CSocketClient.cs` | 串口以及 TCP 客户端 / 监听模式 |
| `DtuTestManager/MyClass/CDataBase.cs`、同目录的 `CParaDefine.cs` | Access 模板数据库与参数模型；实际 `CParaCtrl` 定义在 `CParaDefine.cs` |
| `DtuTestManager/MyClass/CTrans*.cs`、同目录的 `CRouteTable.cs` / `CSQLite.cs` | 厂站、终端、主站及转发表配置，使用 SQLite / INI |
| `DtuTestManager/MyClass/CFileCtrl.cs`、同目录的 `CTransFile.cs` / `CWaveFileInfo.cs` | 文件内容、传输分包及录波数据 |
| `DtuTestManager/MyClass/CEnCodePtr.cs`、同目录的 `CSecretInterface.cs` | 认证、加密及厂商原生 DLL 接口 |
| `DtuTestManager/MyClass/CTetstCasePro.cs` | 101 规约试验，包含 `C101Ptr` 的另一部分实现及故意构造的错误报文 |
| `DtuTestManager/MyClass/SFTP.cs` | SSH.NET 文件操作；主界面入口为 `CSFTPLogin` → `CSFTPMainForm` |

特别注意：`DtuTestManager/MainForm.cs` 未参与编译。`MyForms/CCommModeSetForm.cs`、`CHisRecordForm1.cs`、`CMasterConForm1.cs`、`MyClass/CParaCtrl.cs` 等也不在当前编译清单中。正在使用的通信配置窗体 `CommuCFG` 位于 `MyForms/CCommModeSet.cs`。不删除这些旧文件，不把它们加入构建来修复同名类型问题。

## 调用约定与修改边界

- 常规命令沿 `CGlobal.SendTaskOrder` → `CDeviceClass.SendTaskOrder` → `m_Ptr.GetTaskBuffer` → `SendToDevice` 发送。接收线程先调用 `AddProcessBuff`，再由设备处理线程抽帧、解析、应答并更新共享数据。
- `SystemConnection` 定义在 `CGlobalData.cs`：连接类型 `0=串口 / 1=网络`，规约类型 `0=101 / 1=104`，TCP 模式 `0=客户端 / 1=监听`。规约与传输通道是分开选择的。
- 数字任务号、信息体地址、ASDU 地址、参数类型和长度属于现有通信契约。修改时核对窗体调用方、101 / 104 实现及模板数据，不自行重新编号。
- 参数读取、预置、固化和撤销是不同操作；窗体定时器通过 `ParaTaskMsg` 与应答标志推进分包任务，不把“已发送”视为“设备已持久化”。
- 规约试验中的错误控制域、地址、长度、校验和、结束符和时间字段是预期测试输入。除非需求明确改变试验，不修正这些故意错误。
- 保留现有 `CGlobal`、线程和窗体交互模式。涉及锁或线程退出的修复应针对具体可验证路径，不借机迁移整个通信架构。
- `*.Designer.cs`、`*.resx`、`Properties` 中的生成文件与对应窗体配套维护；非界面任务不修改它们。新增源码或资源必须同步显式项目清单。
- 列表使用控件自身的双缓冲，原生扩展样式中保留 `LVS_EX_DOUBLEBUFFER`。不以全窗体 `WS_EX_COMPOSITED` 叠加到已有缓冲的列表及其父容器；无数据变化时不定时重绘整个实时监测窗体。界面性能修复记录见 `PROJECT_ANALYSIS.md` 第 10 节。
- 不新增与当前需求无关的框架、配置层、重试、回退、完整性扫描或通用校验层。不用哈希检测普通文件变化；新增哈希须满足用户约定的特定用途并先说明。

## 构建与验证

使用 Windows 下的 Visual Studio / Build Tools 完整 MSBuild，并具备 .NET Framework 4.8 开发目标包。项目有 COM 引用，不把 `dotnet build` 作为默认构建方法。

主要外部依赖：

| 依赖 | 当前项目约束 |
| --- | --- |
| `System.Data.SQLite`、`System.Data.SQLite.Linq` | 固定版本 1.0.118.0，HintPath 指向项目内 `lib/SQLite`；配套 x86 原生 `SQLite.Interop.dll` 自动复制到 EXE 同目录 |
| `Renci.SshNet` | 引用版本 2014.4.6.0；HintPath 位于 `bin/Debug` |
| `WeifenLuo.WinFormsUI.Docking` | 引用版本 2.3.1.24483；HintPath 位于 `bin/Debug` |
| TeeChart / AxTeeChart | 编译直接引用 `bin/Debug` 中已有的 Interop / AxInterop DLL，不在构建时重新导入类型库；曲线界面运行仍需匹配的 32 位 ActiveX 控件 |
| ADODB / ADOX / stdole | 工程中的其他 COM 引用 |
| Jet OLE DB 4.0 | 运行时读取 Access `.mdb` 模板，需匹配 x86 进程 |
| `TZhuZhanDll.dll`、`HsmPriDll.dll` | 按加密版本选择的厂商原生接口；相关功能需匹配的运行库及设备条件 |

建议在 Visual Studio Developer PowerShell 中隔离构建，避免覆盖已有运行文件：

```powershell
$validationDir = Join-Path $env:TEMP ('DtuTestManager-check-' + [DateTime]::UtcNow.ToString('yyyyMMdd-HHmmss'))
New-Item -ItemType Directory -Path $validationDir | Out-Null
msbuild .\DtuTestManager.sln /t:Build /p:Configuration=Debug '/p:Platform=Any CPU' "/p:BaseIntermediateOutputPath=$validationDir\obj\" "/p:IntermediateOutputPath=$validationDir\obj\Debug\" "/p:OutputPath=$validationDir\bin\Debug\" /nr:false
```

Release 验证使用对应的 `Configuration=Release` 及隔离输出路径。临时输出只是编译产物，不是完整运行包。

2026-10-23 已建立干净构建基线：Visual Studio 2022 的完整 64 位 MSBuild 对 Debug / Release 分别完成全新中间目录构建和默认输出目录构建，均为 0 错误、0 告警；验证命令使用 `/warnaserror`，工程告警级别保持 4，没有新增告警抑制。SQLite 及 TeeChart 依赖配置已落实到 `.csproj`；经确认当前源码和资源未使用的两个历史 ReportViewer 引用已移除。详细记录见 `PROJECT_ANALYSIS.md` 第 9 节。

编译通过不代表 TeeChart ActiveX、加密设备或现场数据库业务已经验证。不要通过删除引用、注释业务代码或自动安装 / 注册组件来掩盖环境问题；本次 TeeChart 仅改为引用现有托管包装 DLL，控件运行依赖仍须保留。

没有独立测试项目或外部测试框架。`tests/BaselineRegression.cs` 与 `tests/UiRegression.cs` 为独立 x86 控制台回归，共 28 项，覆盖 SQLite 内存读写、具体锁释放路径、接收缓存、104 心跳 / S 帧计时，以及隐藏控件的缓冲、布局、空闲重绘和列表初始化。2026-10-24 的 Debug / Release 完整构建与默认输出均保持 0 错误、0 告警，28 项检查全部通过。构建后使用 `tests/Run-Regression.ps1 -BuildDirectory <输出目录>` 执行，说明见 `tests/README.md`。检查仅创建隐藏控件，不显示主程序，不执行主窗体启动加载，不连接终端或读写现场配置。

`CTetstCasePro` / `CTtestCaseForm` 是程序内部的终端规约试验功能。普通修改先做最小相关编译或离线检查；涉及规约时再用明确的报文样例或获准的测试终端验证，不为文档修改新增测试框架。

## 运行文件与设备操作

- `bin/Debug` 含外部依赖、Access 模板、工程配置、图片、证书及历史记录，部分依赖直接从此目录引用。不得将整个 `bin` 当作可清理的构建缓存。
- 启动同时使用应用基目录和当前工作目录：INI 为基目录下的 `PrjSet_Main.ini`，模板目录、`DataBase.mdb` 和 `res/Log.png` 等使用相对路径。运行前确认工作目录和完整资源布局，不能只复制 EXE。
- 不在分析或初始化时自动启动程序、连接真实终端 / 加密机、执行遥控、下装参数、固化、升级或远端文件写入 / 删除。设备操作须在用户明确授权的测试范围内进行。
- 不改现有 INI、数据库、证书、密钥、原生 DLL、历史记录、`.suo`、`.vs` 或用户配置；不输出证书、口令和现场配置的敏感内容。
- 删除文件、注册 COM、安装全局组件、升级依赖、改变生产配置或 Git 历史等高风险操作须按用户要求先获得明确授权。

## 交付要求

- 说明修改内容、原因、验证方法和结果；构建因环境依赖失败时列出具体缺项，不能声称验证通过。
- 新发现但不在任务范围的问题单独说明，不擅自修复。静态代码发现不能写成已在设备上复现的故障，也不能据此声称规约全面符合标准。
- 验证与改动风险相称，不默认扫描、格式化、哈希或重构整个工程。

## WPF 重构分支

- `refactor/wpf-mvvm` 分支新增 `TerminalWorkbench/TerminalWorkbench.sln`，其有效项目为 `TerminalWorkbench.App` 和 `TerminalWorkbench.Regression`；原解决方案仍独立保留。
- 新项目使用 .NET 10、WPF、自包含 x86 输出，按 `TerminalWorkbench/Build.ps1` 构建，不使用原 COM 工程的构建命令来构建新版。
- 新版分析与范围边界见 `TerminalWorkbench/REFACTORING.md`，功能索引见 `TerminalWorkbench/FEATURE_PARITY.md`。不得把窗口创建、固定报文对照或零告警构建写成设备全功能验收。
- `Ported` 是经过人工修复的迁移源码，直接维护；一次性迁移脚本不参与构建且不得覆盖已有实现。保留原任务号及原本故意错误的试验报文。
- `TerminalWorkbench.Core` / `Legacy` / `Desktop` 为未采用的早期兼容草稿，未纳入有效解决方案或发布包；不以它们代替新版功能，也不擅自删除。
- 不手工修改、清理或暂存 `.tools`、`.artifacts`、各项目 `bin` / `obj` 中的生成物；正常构建可更新生成物，不把 SDK 或运行时加入 Git。
- `Build.ps1 -Verify` 先执行 92 组离线回归，再在独立进程执行 14 组自动工作流。工作流可运行主窗体的真实加载，但只使用生成的临时 INI、模板副本和本机 TCP 仿真，不使用现场连接参数；测试成功不代表实机或 SFTP 传输验收。
