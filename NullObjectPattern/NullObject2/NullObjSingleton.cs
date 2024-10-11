namespace Patterns.NullObjectPattern.NullObject2;

public interface ILog
{
    public static ILog NullLogInstance => NullLog.Instance;
    void Info(string message);
    void Warning(string message);
    
    private sealed class NullLog : ILog
    {
        private static readonly Lazy<NullLog> NullInstance = new(() => new NullLog());
        
        private NullLog() { }

        public static ILog Instance => NullInstance.Value;
        
        public void Info(string message) { }

        public void Warning(string message) { }
    }
}

public static class NullObjSingleton
{
    public static void LocalMain()
    {
        var b = ILog.NullLogInstance;
        b.Info("dwd");
    }
}