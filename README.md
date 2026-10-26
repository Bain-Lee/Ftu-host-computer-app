# FTU / DTU 配电终端上位机

用于 FTU / DTU 配电终端通信调试、实时监视、参数配置、文件传输和规约试验的 Windows 桌面程序，使用 C# 开发。

仓库保留原 WinForms 工程，并在独立分支提供 .NET 10 / WPF 迁移候选。WPF 版本仍有业务与呈现逻辑耦合，尚未完成纯 MVVM 拆分及真实设备全功能验收。

## 版本与分支

| 分支 | 技术栈 | 工程入口 | 状态 |
| --- | --- | --- | --- |
| [`main`](https://github.com/Bain-Lee/Ftu-host-computer-app/tree/main)（默认） | WinForms / .NET Framework 4.8 / x86 | `DtuTestManager.sln` | 保留原工程及已验证的构建、回归基线 |
| [`refactor/wpf-mvvm`](https://github.com/Bain-Lee/Ftu-host-computer-app/tree/refactor/wpf-mvvm) | WPF / .NET 10 / x86 自包含 | `TerminalWorkbench/TerminalWorkbench.sln` | 已有离线回归与本机通信仿真，设备验收待完成 |

`TerminalWorkbench/` 仅存在于 WPF 分支；克隆后需切换分支才能构建新版。

## 功能范围

- 串口、TCP 客户端和监听模式，支持 101 / 104 规约。
- 遥测、遥信、电度、报文、事件和 SOE 监视，以及对时、总召和遥控操作。
- 参数读取、预置、固化、撤销，模板及工程参数管理。
- 厂站、终端、主站、转发表和通信模块配置。
- 历史文件、录波、趋势、文件上下行、升级及规约故障试验。
- SFTP 文件操作和厂商加密接口。

以上为源码中的功能范围。真实终端、加密设备和 SFTP 服务的完整操作流程仍需在具备条件的测试环境中验证。

## 获取源码

```powershell
git clone https://github.com/Bain-Lee/Ftu-host-computer-app.git
cd Ftu-host-computer-app
```

以下命令均从仓库根目录执行。

## 原 WinForms 版本

### 构建

需要 Windows、Visual Studio / Build Tools 的完整 MSBuild，以及 .NET Framework 4.8 开发目标包。工程保留 ADODB、ADOX 和 stdole COM 引用，默认使用 Visual Studio Developer PowerShell 构建。

解决方案配置虽然名为 `Any CPU`，实际目标均为 **x86**；项目采用传统 MSBuild 格式。

以下示例使用临时输出目录，避免覆盖本机已有运行文件：

```powershell
git switch main
$buildDir = Join-Path $env:TEMP ('Ftu-host-check-' + [DateTime]::UtcNow.ToString('yyyyMMdd-HHmmss'))
New-Item -ItemType Directory -Path $buildDir | Out-Null
msbuild .\DtuTestManager.sln /t:Build /p:Configuration=Debug '/p:Platform=Any CPU' /warnaserror /nr:false "/p:BaseIntermediateOutputPath=$buildDir\obj\" "/p:IntermediateOutputPath=$buildDir\obj\Debug\" "/p:OutputPath=$buildDir\bin\Debug\"
```

构建成功后，执行对应输出的离线回归：

```powershell
.\tests\Run-Regression.ps1 -BuildDirectory "$buildDir\bin\Debug"
```

生成的程序集名为 `配电终端测试软件V2.0.exe`。隔离构建输出只用于编译和回归检查，不是完整运行包。

### 依赖与运行目录

- 仓库保留 System.Data.SQLite 1.0.118.0、SSH.NET、Docking 及 TeeChart 托管包装 DLL；引用路径见项目文件。
- 读取 Access 模板需要 32 位 Jet OLE DB 4.0；原版曲线界面仍需要匹配的 32 位 TeeChart ActiveX。
- 加密功能需要对应厂商原生 DLL、运行库和设备。
- 原程序从 EXE 所在目录读取 `PrjSet_Main.ini`，同时通过当前工作目录读取模板、`DataBase.mdb` 和图片。启动前应准备完整运行目录并设置正确的工作目录。

`DtuTestManager/bin/Debug` 还保存工程引用的依赖和受版本管理的模板、图片，不能将整个目录当作构建缓存清理。现场 INI、证书及完整运行环境由使用者单独准备。

## WPF 迁移版本

在 Windows x64 构建机上切换到 WPF 分支后执行：

```powershell
git switch refactor/wpf-mvvm
.\TerminalWorkbench\Build.ps1 -Configuration Release -Verify -Publish
```

脚本使用用户缓存 `%LOCALAPPDATA%/TerminalWorkbench.BuildTools/dotnet-10.0.402` 中的私有 SDK；不存在时从微软官方下载。首次构建需要联网下载 SDK 和还原 NuGet 包，不安装系统 SDK，不把 SDK 放进工程目录。

- `-Verify`：依次执行离线回归和独立进程中的自动工作流。
- `-Publish`：生成内置 x86 .NET 桌面运行时、默认模板和资源的压缩单文件 EXE。
- 发布入口：`TerminalWorkbench/.artifacts/publish/win-x86/TerminalWorkbench.exe`。只需复制这一个文件，双击即可打开。

新版解决方案只包含 `TerminalWorkbench.App` 和 `TerminalWorkbench.Regression`。`Core`、`Legacy`、`Desktop` 为未纳入有效解决方案的早期草稿。

首次启动自动在 `%LOCALAPPDATA%/TerminalWorkbench/Workspace` 准备默认配置、模板和业务目录，不再要求选择 INI。以后继续使用并保留该用户目录中的修改。连接参数在软件内设置，由用户点击“开始运行”发起初次连接；Access / Jet 和厂商加密接口的运行条件仍需满足。

详细操作见 [WPF 版本说明](https://github.com/Bain-Lee/Ftu-host-computer-app/blob/refactor/wpf-mvvm/TerminalWorkbench/README.md)。

## 验证记录与边界

原版记录来自 **2026-10-24**；WPF 记录更新至 **2026-10-26**：

| 版本 | 已有记录 | 验证范围 |
| --- | --- | --- |
| WinForms | Debug / Release 构建 0 错误、0 告警；28 项回归通过 | SQLite、锁释放、接收缓存、104 计时，以及隐藏控件的缓冲和布局等 |
| WPF | Debug / Release 构建 0 错误、0 告警；各通过 92 组离线回归和 15 组自动工作流 | 窗口与编辑流程、内置默认工程、临时数据和本机 TCP 仿真等；单文件 EXE 另做独立目录无参数启动检查 |

检查通过不能替代真实终端遥控、参数固化、升级、加密设备、SFTP 文件传输或规约全量符合性验收。设备写入及故障试验应在明确授权的测试范围内进行。

## 文档导航

- [原工程分析与修复记录](PROJECT_ANALYSIS.md)
- [原版离线回归说明](tests/README.md)
- [WPF 重构分析与范围边界](https://github.com/Bain-Lee/Ftu-host-computer-app/blob/refactor/wpf-mvvm/TerminalWorkbench/REFACTORING.md)
- [WPF 功能迁移索引](https://github.com/Bain-Lee/Ftu-host-computer-app/blob/refactor/wpf-mvvm/TerminalWorkbench/FEATURE_PARITY.md)
- [WPF 验证记录](https://github.com/Bain-Lee/Ftu-host-computer-app/blob/refactor/wpf-mvvm/TerminalWorkbench/VERIFICATION.md)

## 版本管理范围

仓库包含源码、工程文件、文档、必要依赖、现有模板数据库及图片。构建产物、项目内 SDK、Visual Studio 用户状态、日志、证书和现场配置按 [`.gitignore`](.gitignore) 排除，白名单中的既有依赖与资源保留。
