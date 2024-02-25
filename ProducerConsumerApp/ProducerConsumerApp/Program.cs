using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        var buffer = new Buffer<int>();
        var cts = new CancellationTokenSource();
        var producer = new Producer(buffer, cts.Token);
        var consumer = new Consumer(buffer, cts.Token);

        Thread producerThread = new Thread(() => producer.Produce());
        Thread consumerThread = new Thread(() => consumer.Consume());

        Console.WriteLine("Press any key to stop...");

        producerThread.Start();
        consumerThread.Start();

        
        Console.ReadKey();

        // Signal for cancellation to Producer and Consumer
        cts.Cancel();

        // Wait for both threads to complete their current work and exit
        producerThread.Join();
        consumerThread.Join();

        // Safe-stop logging
        Logging.StopLogging();
    }
}
