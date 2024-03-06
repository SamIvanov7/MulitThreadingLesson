using System;
using System.Threading;
using System.Threading.Tasks;

public class AsyncConsumer
{
    private readonly AsyncBuffer<int> buffer;
    private readonly CancellationToken cancellationToken;

    public AsyncConsumer(AsyncBuffer<int> buffer, CancellationToken cancellationToken)
    {
        this.buffer = buffer;
        this.cancellationToken = cancellationToken;
    }

    public async Task ConsumeAsync()
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            int item = await buffer.TakeAsync();
            Console.WriteLine($"Consumed: {item}");
            await Logging.LogAsync($"Consumed: {item}");
            await Task.Delay(1500, cancellationToken);
        }
    }
}