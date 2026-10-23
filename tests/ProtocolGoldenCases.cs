using System;
using System.Collections.Generic;
using DtuTestManager;

// Shared by the .NET Framework and .NET 10 runners. These fixtures only call
// frame builders with synthetic in-memory state; no channel is opened.
internal static class ProtocolGoldenCases
{
    internal static List<KeyValuePair<string, string>> Capture()
    {
        var result = new List<KeyValuePair<string, string>>();
        int[] common = { 1, 2, 3, 4, 7, 8, 9, 10, 20, 21, 28, 29, 30, 33, 34, 35, 36, 37, 38, 39, 41, 42, 60, 61, 62, 63, 64 };
        foreach (bool use104 in new[] { false, true })
        {
            foreach (int task in common)
            {
                // The 101 clock-read request carries the current clock. It is
                // not a deterministic byte fixture across separate processes.
                if (!use104 && task == 41) continue;
                Add(result, use104, task, 2, 0, 0, false);
            }
            for (int action = 0; action < 3; action++)
            {
                for (int type = 0; type < 2; type++)
                {
                    Add(result, use104, 6, 2, action, type, false);
                }
            }
            foreach (int type in new[] { 1, 4, 32, 33, 43, 45, 35, 36, 37, 38, 39 })
            {
                Add(result, use104, 9, type, 0, 0, false);
            }
            foreach (int task in new[] { 8, 9, 10, 28, 42 }) Add(result, use104, task, 2, 0, 0, true);
        }
        Add(result, false, 40, 2, 0, 0, false);
        foreach (int task in new[] { 1000, 1001, 1002, 1003, 1004, 2000, 2001, 3000, 3001, 3002, 3003, 3004, 3005, 3006 })
        {
            Add(result, false, task, 2, 0, 0, false);
        }
        return result;
    }

    private static void Add(List<KeyValuePair<string, string>> output, bool use104, int task, int parameterType, int action, int controlType, bool stationParameters)
    {
        CGlobal.m_Device = new CDeviceClass();
        var device = CGlobal.m_Device;
        device.m_DeviceMsg = new CModuleClass { m_LineCount = 1, m_DevAddress = 1 };
        device.m_ASDUAddr = 1;
        device.m_Connection.IsEncode = false;
        device.m_Connection.m_n_ProType = use104 ? 1 : 0;
        CPtrInterface protocol = use104 ? (CPtrInterface)new C104Ptr() : new C101Ptr();
        device.m_Ptr = protocol;
        protocol.theDevice = device;
        protocol.m_AddressType = 2;
        protocol.m_CotType = 2;
        protocol.m_ParaAreaNo = 1;
        protocol.Is_PriPara = stationParameters;
        protocol.pYkMsg = new CYkOption { m_YkNo = 1, m_YkAddress = 0x6001, m_YkType = controlType, m_YkStatus = action, m_YkValue = 1 };
        protocol.m_NowParaTask = new ParaTaskMsg { m_StartPalce = 0, m_ParaCount = 1, m_EndFlag = true, m_TaskNo = task };
        var parameter = new CParaCtrl { m_ParaAddress = 0x1234, m_ParaType = parameterType };
        parameter.m_ParaValue = parameterType == 4 ? (object)"TEST" : parameterType == 38 || parameterType == 39 ? 1.25 : 1;
        device.m_SelectParaList.Add(parameter);
        device.m_TotalParaDataList.Add(parameter);
        device.m_TransFile.m_FileName = "test.bin";
        device.m_TransFile.m_FileNameLength = 8;
        device.m_TransFile.m_FileID = 1;
        device.m_TransFile.m_FileSize = 3;
        device.m_TransFile.SumFrames = 1;
        device.m_TransFile.m_data.Add(new CFileData { DataLength = 3, DataBuff = new byte[] { 1, 2, 3 } });
        CGlobal.m_YxNo = 1;
        CGlobal.m_YxValue = 1;
        CGlobal.m_YcNo = 1;
        CGlobal.m_YcValue = 1.25F;
        CEnCodePtr.NormalTest();
        var bytes = new byte[4096];
        int count = protocol.GetTaskBuffer(bytes, task);
        string label = (use104 ? "104" : "101") + "/task=" + task + "/type=" + parameterType + "/action=" + action + "/control=" + controlType + "/station=" + stationParameters;
        if (count <= 0) throw new InvalidOperationException("Fixture produced an empty frame: " + label);
        output.Add(new KeyValuePair<string, string>(label, BitConverter.ToString(bytes, 0, count)));
    }
}
