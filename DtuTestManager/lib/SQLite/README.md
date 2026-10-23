# SQLite 依赖

- 固定版本：System.Data.SQLite 1.0.118.0，托管程序集目标为 .NET Framework 4.6，供当前 .NET Framework 4.8 / x86 工程使用。
- 来源：本机已有历史工程 `E:\FiberHome\DLWL\srcode\EG3214P_tool\DtuTestManager\bin\Release`，未下载或安装其他组件。
- `System.Data.SQLite.dll`、`System.Data.SQLite.Linq.dll` 和 `x86/SQLite.Interop.dll` 来自同一套组件；保留配套版本。
- 工程对托管 DLL 使用相对路径引用，并将 x86 原生 `SQLite.Interop.dll` 复制到生成 EXE 的同目录。独立 32 位内存查询已验证此布局可加载。
- 原工程引用的版本为 1.0.105.2。本次仅变更依赖配置；现有厂站配置库的读写兼容性仍需专项回归，不能以编译通过替代业务验证。
