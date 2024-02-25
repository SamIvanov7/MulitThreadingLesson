using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;

public static class Logging
{
    private static readonly string logFilePath = "ProducerConsumerLog.txt";
    private static readonly ConcurrentQueue<string> logMessages = new ConcurrentQueue<string>();
    private static readonly AutoResetEvent logSignal = new AutoResetEvent(false);
    private static readonly Thread logThread;
    private static bool isLoggingEnabled = true;

    static Logging()
    {
        logThread = new Thread(LogMessageHandler)
        {
            IsBackground = true
        };
        logThread.Start();
    }

    public static void Log(string message)
    {
        logMessages.Enqueue($"{DateTime.Now}: {message}\n");
        logSignal.Set();
    }

    private static void LogMessageHandler()
    {
        while (isLoggingEnabled)
        {
            logSignal.WaitOne();
            while (logMessages.TryDequeue(out string message))
            {
                File.AppendAllText(logFilePath, message);
            }
        }
    }

    public static void StopLogging()
    {
        isLoggingEnabled = false;
        logSignal.Set();
    }
}