using System.Collections.Generic;
using System.Threading.Tasks;

public class AsyncBuffer<T>
{
    private readonly Queue<T> queue = new();
    private readonly int maxSize;
    private readonly SemaphoreSlim semaphore = new(1, 1);

    public AsyncBuffer(int maxSize = 10)
    {
        this.maxSize = maxSize;
    }

    public async Task<bool> TryAddAsync(T item)
    {
        await semaphore.WaitAsync();
        try
        {
            if (queue.Count < maxSize)
            {
                queue.Enqueue(item);
                return true;
            }
            return false;
        }
        finally
        {
            semaphore.Release();
        }
    }

    public async Task<T> TakeAsync()
    {
        await semaphore.WaitAsync();
        try
        {
            while (queue.Count == 0)
            {
                await Task.Delay(50);
            }
            return queue.Dequeue();
        }
        finally
        {
            semaphore.Release();
        }
    }
}