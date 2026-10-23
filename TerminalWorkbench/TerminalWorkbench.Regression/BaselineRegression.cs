using System;
using Microsoft.Data.Sqlite;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;

using DtuTestManager;

internal static class BaselineRegression
{
    private static int failures;

    [STAThread]
    internal static int RunCore()
    {
        Run("SQLite: x86 in-memory write and read", TestSQLite);
        Run("socket: null connection releases lock", TestNullSocket);
        Run("socket: closed connection returns false", TestClosedSocket);
        Run("buffer: empty", TestEmptyBuffer);
        Run("buffer: contiguous data and consumption", TestContiguousBuffer);
        Run("buffer: wrapped data and consumption", TestWrappedBuffer);
        Run("buffer: full capacity", TestFullBuffer);
        Run("buffer: contiguous mismatch releases lock", TestContiguousMismatch);
        Run("buffer: wrapped mismatch releases lock", TestWrappedMismatch);
        Run("buffer: copy exception releases lock", TestBufferCopyException);
        Run("104: heartbeat waits for 180 seconds", () => TestHeartbeat(179, true, false));
        Run("104: heartbeat after 181 seconds", () => TestHeartbeat(181, true, true));
        Run("104: heartbeat after 241 seconds", () => TestHeartbeat(241, true, true));
        Run("104: stopped connection sends no heartbeat", () => TestHeartbeat(181, false, false));
        Run("104: S frame waits for 15 seconds", () => TestAcknowledgement(14, 0, false));
        Run("104: S frame after 16 seconds", () => TestAcknowledgement(16, 0, true));
        Run("104: delayed S frame after 65 seconds", () => TestAcknowledgement(65, 0, true));
        Run("104: four pending frames trigger S frame", () => TestAcknowledgement(0, 4, true));
        Console.WriteLine("Failures: " + failures);
        return failures == 0 ? 0 : 1;
    }

    private static void Run(string name, Action test)
    {
        try
        {
            test();
            Console.WriteLine("PASS " + name);
        }
        catch (Exception error)
        {
            failures++;
            Console.WriteLine("FAIL " + name + ": " + error.Message);
        }
    }

    private static void Assert(bool condition, string message)
    {
        if (!condition)
        {
            throw new InvalidOperationException(message);
        }
    }

    private static FieldInfo Field(object target, string name)
    {
        return target.GetType().GetField(name, BindingFlags.Instance | BindingFlags.NonPublic);
    }

    private static void AssertLockReleased(object sync)
    {
        bool retained = Monitor.IsEntered(sync);
        if (retained)
        {
            Monitor.Exit(sync);
        }
        Assert(!retained, "The returning method retained its lock.");
    }

    private static void TestSQLite()
    {
        using (SqliteConnection connection = new SqliteConnection("Data Source=:memory:;"))
        {
            connection.Open();
            using (SqliteCommand command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE BaselineCheck (Value INTEGER NOT NULL);"
                    + "INSERT INTO BaselineCheck VALUES (42);";
                command.ExecuteNonQuery();
                command.CommandText = "SELECT Value FROM BaselineCheck;";
                Assert(Convert.ToInt32(command.ExecuteScalar()) == 42, "SQLite round trip failed.");
            }
        }
    }

    private static void TestNullSocket()
    {
        CSocketChannel channel = new CSocketChannel();
        try
        {
            Assert(!channel.SendToSocket(new byte[1], 1), "A null socket must return false.");
        }
        finally
        {
            AssertLockReleased(Field(channel, "m_Lock").GetValue(channel));
        }
    }

    private static void TestClosedSocket()
    {
        CSocketChannel channel = new CSocketChannel();
        using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
        {
            socket.Close();
            Field(channel, "SocketClient").SetValue(channel, socket);
            try
            {
                Assert(!channel.SendToSocket(new byte[1], 1), "A closed socket must return false.");
            }
            finally
            {
                AssertLockReleased(Field(channel, "m_Lock").GetValue(channel));
            }
        }
    }

    private static int ReadBuffer(CDeviceClass device, byte[] buffer)
    {
        try
        {
            return device.GetProcessBuff(buffer);
        }
        finally
        {
            AssertLockReleased(Field(device, "m_ProcessLock").GetValue(device));
        }
    }

    private static void TestEmptyBuffer()
    {
        Assert(ReadBuffer(new CDeviceClass(), new byte[10240]) == 0, "Expected an empty buffer.");
    }

    private static void TestContiguousBuffer()
    {
        CDeviceClass device = new CDeviceClass();
        device.AddProcessBuff(new byte[] { 10, 20, 30 }, 3);
        byte[] buffer = new byte[10240];
        Assert(ReadBuffer(device, buffer) == 3, "Expected three bytes.");
        Assert(buffer[0] == 10 && buffer[1] == 20 && buffer[2] == 30, "Contiguous data changed.");
        device.SetStartPlace(3);
        Assert(ReadBuffer(device, buffer) == 0, "Consumed data must be empty.");
    }

    private static void TestWrappedBuffer()
    {
        CDeviceClass device = new CDeviceClass();
        device.AddProcessBuff(new byte[10239], 10239);
        device.SetStartPlace(10239);
        device.AddProcessBuff(new byte[] { 10, 20, 30 }, 3);
        byte[] buffer = new byte[10240];
        Assert(ReadBuffer(device, buffer) == 3, "Expected three wrapped bytes.");
        Assert(buffer[0] == 10 && buffer[1] == 20 && buffer[2] == 30, "Wrapped data changed.");
        device.SetStartPlace(3);
        Assert(ReadBuffer(device, buffer) == 0, "Consumed wrapped data must be empty.");
    }

    private static void TestFullBuffer()
    {
        CDeviceClass device = new CDeviceClass();
        byte[] input = new byte[10240];
        for (int index = 0; index < input.Length; index++)
        {
            input[index] = (byte)(index % 256);
        }
        device.AddProcessBuff(input, input.Length);
        byte[] buffer = new byte[input.Length];
        Assert(ReadBuffer(device, buffer) == input.Length, "Expected a full buffer.");
        for (int index = 0; index < input.Length; index++)
        {
            Assert(buffer[index] == input[index], "Full-buffer data changed at " + index);
        }
    }

    private static void TestContiguousMismatch()
    {
        CDeviceClass device = new CDeviceClass();
        Field(device, "m_RecLeng").SetValue(device, 1);
        Field(device, "m_EndPlace").SetValue(device, 2);
        Assert(ReadBuffer(device, new byte[10240]) == 0, "A length mismatch must return zero.");
    }

    private static void TestWrappedMismatch()
    {
        CDeviceClass device = new CDeviceClass();
        Field(device, "m_RecLeng").SetValue(device, 2);
        Field(device, "m_StartPlace").SetValue(device, 10238);
        Field(device, "m_EndPlace").SetValue(device, 1);
        Assert(ReadBuffer(device, new byte[10240]) == 0, "A wrapped length mismatch must return zero.");
    }

    private static void TestBufferCopyException()
    {
        CDeviceClass device = new CDeviceClass();
        device.AddProcessBuff(new byte[] { 10, 20, 30 }, 3);
        bool threw = false;
        try
        {
            ReadBuffer(device, new byte[1]);
        }
        catch (ArgumentException)
        {
            threw = true;
        }
        Assert(threw, "A short destination must retain its existing exception behavior.");
    }

    private static C104Ptr Protocol()
    {
        C104Ptr protocol = new C104Ptr();
        protocol.theDevice = new CDeviceClass();
        Field(protocol, "m_StartFlag").SetValue(protocol, true);
        return protocol;
    }

    private static void AssertFrame(byte[] buffer, int length, byte control)
    {
        Assert(length == 6, "Expected a six-byte frame, received " + length);
        Assert(buffer[0] == 0x68 && buffer[1] == 0x04 && buffer[2] == control
            && buffer[3] == 0 && buffer[4] == 0 && buffer[5] == 0, "Unexpected frame content.");
    }

    private static void TestHeartbeat(int elapsedSeconds, bool started, bool expected)
    {
        C104Ptr protocol = Protocol();
        Field(protocol, "m_StartFlag").SetValue(protocol, started);
        Field(protocol, "m_HeartTime").SetValue(protocol, DateTime.Now.AddSeconds(-elapsedSeconds));
        byte[] buffer = new byte[256];
        int length = 0;
        protocol.GetTestOrSBuff(buffer, ref length);
        if (expected)
        {
            AssertFrame(buffer, length, 0x43);
            Assert((bool)Field(protocol, "m_bHeartOnlie").GetValue(protocol), "Heartbeat state was not set.");
            length = 0;
            protocol.GetTestOrSBuff(buffer, ref length);
            Assert(length == 0, "Heartbeat must not immediately repeat.");
        }
        else
        {
            Assert(length == 0, "Heartbeat was sent before its deadline or while stopped.");
        }
    }

    private static void TestAcknowledgement(int elapsedSeconds, int pending, bool expected)
    {
        C104Ptr protocol = Protocol();
        Field(protocol, "m_STime").SetValue(protocol, DateTime.Now.AddSeconds(-elapsedSeconds));
        Field(protocol, "m_SCount").SetValue(protocol, pending);
        byte[] buffer = new byte[256];
        int length = 0;
        protocol.GetTestOrSBuff(buffer, ref length);
        if (expected)
        {
            AssertFrame(buffer, length, 0x01);
            Assert((int)Field(protocol, "m_SCount").GetValue(protocol) == 0, "Pending count was not reset.");
        }
        else
        {
            Assert(length == 0, "S frame was sent before its deadline.");
        }
    }
}
