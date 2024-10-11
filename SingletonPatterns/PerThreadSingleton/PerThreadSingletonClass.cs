namespace Patterns.SingletonPatterns.PerThreadSingleton;

public sealed class PerThreadSingleton
{
    public int Id;
    
    private static ThreadLocal<PerThreadSingleton> _threadInstance
        = new(() => new PerThreadSingleton());

    private PerThreadSingleton()
    {
        Id = Environment.CurrentManagedThreadId;
    }

    public static PerThreadSingleton Instance => _threadInstance.Value ?? throw new Exception();
}

public static class PerThreadSingletonClass
{
    public static void ThreadWork(string taskName)
    {
        for (int i = 0; i < 20; i++)
        {
            Console.WriteLine($"{taskName}: " + PerThreadSingleton.Instance.Id);
            Thread.Sleep(1000);
        }
    }

    public static void LocalMain()
    {
        var t1 = Task.Factory.StartNew(() => { ThreadWork("t1"); });

        var t2 = Task.Factory.StartNew(() => { ThreadWork("t2"); });

        Task.WaitAll(t1, t2);
    }
}