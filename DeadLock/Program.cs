// create deadlock example using two threads

var lock1 = new object();
var lock2 = new object();

var t1 = new Thread(() =>
{
    lock (lock1)
    {
        Console.WriteLine("Thread 1: locked lock1");
        Thread.Sleep(1000);
        Console.WriteLine("Thread 1: waiting for lock2");

        lock (lock2)
        {
            Console.WriteLine("Thread 1: locked lock2");
        }
    }
});

var t2 = new Thread(() =>
{
    lock (lock2)
    {
        Console.WriteLine("Thread 2: locked lock2");
        Thread.Sleep(1000);
        Console.WriteLine("Thread 2: waiting for lock1");

        lock (lock1)
        {
            Console.WriteLine("Thread 2: locked lock1");
        }
    }
});

t1.Start();
t2.Start();

t1.Join();
t2.Join();

Console.WriteLine("Done");

