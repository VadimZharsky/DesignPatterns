namespace Patterns.Command.Command1;

public interface ICommand
{
    bool Success { get; }
    void Call();
    void Undo();
}

public class BankAccountCommand : ICommand
{
    private readonly BankAccount _account;
    private readonly Action _action;
    private readonly int _amount;

    public BankAccountCommand(BankAccount account, Action action, int amount)
    {
        _account = account;
        _action = action;
        _amount = amount;
    }

    public bool Success { get; private set; }

    public enum Action { Deposit, Withdraw }
    
    private Func<int, bool> ThisAction(bool reverse)
        => _action switch
        {
            Action.Deposit => reverse ? _account.Withdraw : _account.Deposit,
            Action.Withdraw => reverse ? _account.Deposit : _account.Withdraw,
            _ => throw new ArgumentOutOfRangeException(_action.ToString())
        };
    
    public void Call() => Success = ThisAction(false).Invoke(_amount);

    public void Undo()
    {
        if (!Success) return;
        Success =  ThisAction(true).Invoke(_amount);
    }
}

public class BankAccount
{
    private int _balance;
    private int _overdraftLimit = -500;

    public bool Deposit(int amount)
    {
        _balance += amount;
        Console.WriteLine($"Deposited ${amount}, balance: {_balance}");
        return true;
    }

    public bool Withdraw(int amount)
    {
        if (_balance - amount < _overdraftLimit) return false;
        _balance -= amount;
        Console.WriteLine($"Withdrew ${amount}, balance: {_balance}");
        return true;
    }
}

public static class Command1
{
    public static void LocalMain()
    {
        var account = new BankAccount();
        var commands = new List<ICommand>
        {
            new BankAccountCommand(account, BankAccountCommand.Action.Deposit, 1000),
            new BankAccountCommand(account, BankAccountCommand.Action.Withdraw, 900),
            new BankAccountCommand(account, BankAccountCommand.Action.Withdraw, 800),
            new BankAccountCommand(account, BankAccountCommand.Action.Withdraw, 700),
            new BankAccountCommand(account, BankAccountCommand.Action.Withdraw, 600)
        };
        foreach (var command in commands)
            command.Call();
        Console.WriteLine("reversing");
        foreach (var command in Enumerable.Reverse(commands)
                     .Where(x => x.Success))
            command.Undo();
    }
}