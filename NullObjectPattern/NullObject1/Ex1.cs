using Autofac;
using Autofac.Core;
using Moq;

namespace Patterns.NullObjectPattern.NullObject1;

public interface ILog
{
    void Info(string msg);
    void Warning(string msg);
}

public class ConsoleLog : ILog
{
    public void Info(string msg)
    {
        Console.WriteLine(msg);
    }

    public void Warning(string msg)
    {
        Console.WriteLine($"WARNING: {msg}");
    }
}

public class NullLog : ILog
{
    public void Info(string msg) { }

    public void Warning(string msg) { }
}

public class BankAccount
{
    private readonly ILog _logger;
    private int _balance;

    public BankAccount(ILog logger)
    {
        _logger = logger;
    }

    public void Deposit(int amount)
    {
        _balance += amount;
        _logger.Info($"Deposited {amount}. Balance is now {_balance}");
    }
}

public static class Ex1
{
    public static void LocalMain()
    {
        var cb = new ContainerBuilder();
        cb.RegisterType<BankAccount>();
        cb.RegisterType<NullLog>().As<ILog>();
        using (var c = cb.Build())
        {
            var ba = c.Resolve<BankAccount>();
            ba.Deposit(100);
        }
    }
}