// using mutex to lock a resource

var threads = new List<Thread>();

var mutex = new Mutex();

for (var i = 0; i < 10; i++)
{
    var thread = new Thread(() =>
    {
        for (var i = 0; i < 100; i++)
        {
            mutex.WaitOne();
            Console.Write($"**{i}**");
            Console.WriteLine();
            mutex.ReleaseMutex();
        }
    });
    threads.Add(thread);
    thread.Start();
}

threads.ForEach(t => t.Join());