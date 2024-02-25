var threads = new List<Thread>();

for (int i = 0; i < 10; i++)
{
    var thread = new Thread(() =>
    {
        for (int i = 0; i < 100; i++)
        {
            Console.Write($"**{i}**");
            Console.WriteLine();
        }
    });
    threads.Add(thread);
    thread.Start();
}

threads.ForEach(t => t.Join());
