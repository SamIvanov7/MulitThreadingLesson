using System;
using System.Threading;

public class Consumer
{
    private readonly Buffer<int> buffer;
    private readonly int delayMilliseconds;
    private readonly CancellationToken cancellationToken;

    public Consumer(Buffer<int> buffer, CancellationToken cancellationToken, int delayMilliseconds = 1500)
    {
        this.buffer = buffer;
        this.delayMilliseconds = delayMilliseconds;
        this.cancellationToken = cancellationToken;
    }

    public void Consume()
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            if (buffer.TryTake(out int item))
            {
                Console.WriteLine($"Consumed: {item}");
                Logging.Log($"Consumed: {item}");
                Thread.Sleep(delayMilliseconds);
            }
        }
    }
}
