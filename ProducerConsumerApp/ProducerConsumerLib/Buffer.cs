using System.Collections.Generic;
using System.Threading;

public class Buffer<T>
{
    private readonly Queue<T> queue = new();
    private readonly int maxSize;
    private readonly object lockObject = new();

    public Buffer(int maxSize = 10)
    {
        this.maxSize = maxSize;
    }

    public bool TryAdd(T item)
    {
        lock (lockObject)
        {
            if (queue.Count < maxSize)
            {
                queue.Enqueue(item);
                Monitor.Pulse(lockObject); // Notify a waiting consumer
                return true;
            }
            return false;
        }
    }

    public bool TryTake(out T item)
    {
        lock (lockObject)
        {
            while (queue.Count == 0)
            {
                Monitor.Wait(lockObject); // Wait for an item to be added
            }
            item = queue.Dequeue();
            return true;
        }
    }
}