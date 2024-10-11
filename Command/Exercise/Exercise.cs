using NUnit.Framework;

namespace Patterns.Command.Exercise;

public class Command
{
    public enum Action
    {
        Deposit,
        Withdraw,
        NotSupportedOperation
    }

    public Action TheAction;
    public int Amount;
    public bool Success;

    public override string ToString()
        => $"{nameof(TheAction)}: {TheAction}, {nameof(Amount)}: {Amount}, {nameof(Success)}: {Success}";
}

public class Account
{
    public int Balance { get; private set; }

    public void Process(Command c) => GetAction(c.TheAction).Invoke(c);

    private void DepositSum(Command c)
    {
        Balance += c.Amount;
        c.Success = true;
    }

    private void WithDrawSum(Command c)
    {
        if (c.Amount > Balance)
        {
            c.Success = false;
            return;
        }

        Balance -= c.Amount;
        c.Success = true;
    }
    
    private Action<Command> GetAction(Command.Action action)
    {
        return action switch
        {
            Command.Action.Deposit => DepositSum,
            Command.Action.Withdraw => WithDrawSum,
            _ => NotSupported
        };
    }
    
    private static void NotSupported(Command c) => c.Success = false;
}

[TestFixture]
public class TestClass
{
    private Account? Account { get; set; }

    [TestCase(Command.Action.Deposit, true, 300, 300)]
    [TestCase(Command.Action.Withdraw, false, 300, 0)]
    [TestCase(Command.Action.NotSupportedOperation, false, 300, 0)]
    public void TestCase1(Command.Action action, bool success, int transferSum, int balance)
    {
        Account = new Account();
        var com = new Command { TheAction = action, Amount = transferSum };
        Account.Process(com);
        Assert.That(com.Success, Is.EqualTo(success));
        Assert.That(Account.Balance, Is.EqualTo(balance));
    }
    
    [TestCase(300, 299, 1, true)]
    [TestCase(300, 300, 0, true)]
    [TestCase(300, 400, 300, false)]
    public void TestCase2(int deposit, int withdraw, int balance, bool success)
    {
        Account = new Account();
        var com1 = new Command { TheAction = Command.Action.Deposit, Amount = deposit };
        var com2 = new Command { TheAction = Command.Action.Withdraw, Amount = withdraw };
        Account.Process(com1);
        Account.Process(com2);
        
        Assert.That(com2.Success, Is.EqualTo(success));
        Assert.That(Account.Balance, Is.EqualTo(balance));
        Console.WriteLine(com1.ToString()); 
    }
}