using System;
using System.Threading;
using System.Threading.Tasks;

public class AsyncProducer
{
    private readonly AsyncBuffer<int> buffer;
    private readonly CancellationToken cancellationToken;

    public AsyncProducer(AsyncBuffer<int> buffer, CancellationToken cancellationToken)
    {
        this.buffer = buffer;
        this.cancellationToken = cancellationToken;
    }

    public async Task ProduceAsync()
    {
        var random = new Random();
        while (!cancellationToken.IsCancellationRequested)
        {
            int newItem = random.Next(100);
            if (await buffer.TryAddAsync(newItem))
            {
                Console.WriteLine($"Produced: {newItem}");
                await Logging.LogAsync($"Produced: {newItem}");
            }
            await Task.Delay(1000, cancellationToken);
        }
    }


}