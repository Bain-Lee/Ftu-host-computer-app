# 逐窗口功能迁移与验收清单

只统计原工程显式编译的窗口。以下 54 个窗口均有移植控制器及 WPF 视图创建检查；这不等于全部按钮流程或设备行为已经验收。

所有窗口的设备验收均尚未执行。任务号列只是十进制字面量的静态索引；枚举、十六进制和动态分包任务应继续核对源码。

另有 14 组自动工作流，覆盖主窗体真实加载、11 个业务页的三轮切换、14 种独立对话框加载 / 关闭、原生表格编辑、工程参数文件重开，以及本机 101 / 104 通信仿真。它们未涵盖所有窗口的全部操作，具体证据见 [VERIFICATION.md](VERIFICATION.md)。

## 业务范围

| 功能 | 对应控制器 |
| --- | --- |
| 串口 / TCP、101 / 104、客户端 / 监听、模板选择 | CMasterConForm、MainForm |
| 实时 YC / YX / 电度、对时、召时、总召、冻结与置数 | CRealCtrlForm、CLineRealForm、CFixDataForm |
| 遥控选择 / 执行 / 撤销、单点 / 双点与试验标志 | CYKForm |
| 参数读取 / 预置 / 固化 / 撤销、区号与分包应答 | CParaForm、CGuHuaForm、CVersionForm |
| 工程参数文件、参数组、差异与模板编辑 | CEngPara、CEngParaNewForm、CEngModPara、CDiffParaForm、CModulForm、CParaSetForm |
| 4G / 网络 / 串口 / 主站 / 终端 / MAC 配置 | CommuCFG、4GForms 下四个编辑窗口 |
| 厂站、终端、主站、转发表及属性 | CTransForm、CPriForm、CTermForm、CRouteForm 及配套选项窗口 |
| 特殊参数、交流 / 直流校准、功能试验 | CSpeParaForm、CDataCheckForm、CTestFunForm |
| 规约故障试验、报文转换 | CTtestCaseForm、CDataTranForm |
| 报文、事件、SOE | CBufferViewForm、CEventMsgForm、CSOEMsgForm、CEventViewForm |
| 历史目录、历史文件、录波、趋势 | CHisRecordForm、CFileTimeForm、CSoePicForm、CCurrentForm |
| 文件下发、普通升级、FTU 快速升级 | CDownFileForm、CUpdataForm、CFTUFastUpdate |
| SFTP 登录、文件浏览、上传 / 下载 / 删除 | CSFTPLogin、CSFTPMainForm、CSFTPForm |
| 加密接口、证书 / 密钥相关操作 | CSecrteKeyForm |

设备相关的写入与破坏性试验需要明确授权的独立测试环境。离线检查只调用内存组帧、模拟接口、隐藏控件及临时数据库。

## 原编译窗口逐项索引

| 窗口类型 | 标题 | 原控件事件绑定数 | 直接十进制任务号 |
| --- | --- | ---: | --- |
| CAddCom | CAddCom | 1 | - |
| CAddGateWay | 添加网关 | 1 | - |
| CAddHost | CAddHost | 2 | - |
| CAddIPForm | 添加IP地址 | 1 | - |
| CDiffParaForm | CDiffParaForm | 3 | - |
| CEngModPara | 参数配置 | 1 | - |
| CEngPara | 工程参数 | 13 | 10 |
| CEngParaNewForm | 参数配置 | 9 | - |
| CFixDataForm | 置数窗口 | 4 | 61, 62, 63, 64 |
| CFTUFastUpdate | CFTUFastUpdate | 3 | - |
| CGuHuaForm | 固化窗口 | 2 | 10 |
| CSFTPForm | SFTP传输 | 2 | - |
| CSFTPLogin | 登录 | 2 | - |
| CSFTPMainForm | 文件上下装 | 9 | - |
| CSpeParaForm | 厂站参数 | 3 | 50, 51 |
| CTtestCaseForm | CTtestCaseForm | 3 | - |
| CommuCFG | 通信模块配置软件 | 43 | - |
| CEventViewForm | 事件信息 | 0 | - |
| CSecrteKeyForm | 加密管理 | 39 | - |
| CDataTranForm | 数据转换 | 9 | - |
| CTestFunForm | 辅助功能 | 10 | - |
| CAddTermForm | 终端信息 | 2 | - |
| CPriForm | CPriForm | 4 | - |
| CPriOptionForm | CTranStationOptionForm | 7 | - |
| CRouteForm | CTermForm | 4 | - |
| CRouteFormSetting | CRouteFormSetting | 9 | - |
| CTermForm | CTermForm | 4 | - |
| CTermOptionForm | CTranStationOptionForm | 16 | - |
| CGetSelectParaForm | 参数选择 | 10 | - |
| CFileTimeForm | 召唤目录时间选择 | 1 | - |
| CLineRealForm | CLineRealForm | 3 | - |
| CDeviceSetForm | CDeviceSetForm | 2 | - |
| CParaSetForm | CParaSetForm | 20 | - |
| CModulForm | 模板配置 | 28 | - |
| CTransForm | 终端配置 | 4 | 23 |
| CTranStationOptionForm | CTranStationOptionForm | 13 | - |
| CTranTableManageSubForm | 数据表管理 | 6 | - |
| CTranTableManageTermForm | 数据表管理 | 6 | - |
| CUpdataForm | 远程升级 | 18 | 35, 36 |
| CVersionForm | 板件类型 | 2 | - |
| CDownFileForm | 文件传输 | 4 | 33 |
| CDataCheckForm | 数据校验 | 14 | 25, 26, 27, 31, 32 |
| CSoePicForm | 录波图形分析 | 12 | - |
| CParaForm | 终端参数 | 27 | 10, 28, 37, 38, 42 |
| CWaitForm | CWaitForm | 1 | - |
| CHisRecordForm | 历史记录 | 14 | 22, 23 |
| CBufferViewForm | 实时报文 | 4 | - |
| CEventMsgForm | 事件信息 | 1 | - |
| CMasterConForm | 通讯配置 | 1 | - |
| CCurrentForm | 数据曲线 | 0 | - |
| CRealCtrlForm | 实时监控 | 11 | 1, 2, 3, 4, 5, 7, 21, 39, 41 |
| CSOEMsgForm | SOE信息 | 1 | - |
| CYKForm | 遥控 | 18 | 6 |
| MainForm | 配电终端测试软件（版本号V2.00） | 25 | 1, 4 |
