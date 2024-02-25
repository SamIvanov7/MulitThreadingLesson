// create example of semaphore with 3 threads and 3 available slots in semaphore

var semaphore = new Semaphore(3, 3);

var threads = new List<Thread>();

for (var i = 0; i < 10; i++)
{
    var thread = new Thread(() =>
    {
        semaphore.WaitOne();
        Console.WriteLine("Thread entered semaphore");
        Thread.Sleep(1000);
        semaphore.Release();
        Console.WriteLine("Thread left semaphore");
    });
    threads.Add(thread);
    thread.Start();
}

threads.ForEach(t => t.Join());