using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public static class Logging
{
    private static readonly string logFilePath = "ProducerConsumerLogAsync.txt";
    private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

    public static async Task LogAsync(string message)
    {
        await semaphore.WaitAsync();
        try
        {
            var logMessage = $"{DateTime.Now}: {message}\n";
            await File.AppendAllTextAsync(logFilePath, logMessage, Encoding.UTF8);
        }
        finally
        {
            semaphore.Release();
        }
    }
}