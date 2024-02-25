using System;
using System.Threading;

public class Producer
{
    private readonly Buffer<int> buffer;
    private readonly int delayMilliseconds;
    private readonly Random random = new();
    private readonly CancellationToken cancellationToken;

    public Producer(Buffer<int> buffer, CancellationToken cancellationToken, int delayMilliseconds = 500)
    {
        this.buffer = buffer;
        this.delayMilliseconds = delayMilliseconds;
        this.cancellationToken = cancellationToken;
    }

    public void Produce()
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            int newItem = random.Next(1000);
            if (buffer.TryAdd(newItem))
            {
                Console.WriteLine($"Produced: {newItem}");
                Logging.Log($"Produced: {newItem}");
                Thread.Sleep(delayMilliseconds);
            }
        }
    }
}