// create lock example using two threads

var threads = new List<Thread>();

var lockObj = new object();

for (var i = 0; i < 10; i++)
{
    var thread = new Thread(() =>
    {
        for (var i = 0; i < 100; i++)
        {
            lock (lockObj)
            {
                Console.Write($"**{i}**");
                Console.WriteLine();
            }
        }
    });
    threads.Add(thread);
    thread.Start();
}

threads.ForEach(t => t.Join());

