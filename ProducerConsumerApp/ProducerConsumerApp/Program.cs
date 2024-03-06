using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var buffer = new AsyncBuffer<int>();
        var cts = new CancellationTokenSource();

        var producer = new AsyncProducer(buffer, cts.Token);
        var consumer = new AsyncConsumer(buffer, cts.Token);

        var produceTask = producer.ProduceAsync();
        var consumeTask = consumer.ConsumeAsync();

        Console.WriteLine("Press any key to stop...");
        Console.ReadKey();

        cts.Cancel();

        await Task.WhenAll(produceTask, consumeTask);
    }
}