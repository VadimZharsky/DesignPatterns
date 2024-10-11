using System.Collections;
using Patterns.ColorPrint;

namespace Patterns.Command.Command2;

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
    private const int OverdraftLimit = -500;
    
    private int _balance;

    public bool Deposit(int amount)
    {
        _balance += amount;
        ColorConsole.Print($"Deposited ${amount}, balance: {_balance}", Color.Ok);
        return true;
    }

    public bool Withdraw(int amount)
    {
        if (_balance - amount < OverdraftLimit)
        {
            ColorConsole.Print($"Can not withdraw ${amount}", Color.Fail);
            return false;
        }

        _balance -= amount;
        ColorConsole.Print($"Withdrew ${amount}, balance: {_balance}", Color.Ok);
        return true;
    }

    public override string ToString() => $"balance: {_balance}";
}

public class CompositeBankAccountCommand : List<BankAccountCommand>, ICommand
{
    protected CompositeBankAccountCommand() { }

    public CompositeBankAccountCommand(IEnumerable<BankAccountCommand> collection) 
        : base(collection) { }
    
    public bool Success => this.All(cmd => cmd.Success);

    public virtual void Call() => ForEach(command => command.Call());

    public virtual void Undo()
    {
        foreach (var command in ((IEnumerable<BankAccountCommand>)this).Reverse())
            if (command.Success) command.Undo();
    }
}

public class MoneyTransferCommand : CompositeBankAccountCommand
{
    public MoneyTransferCommand(BankAccount from, BankAccount to, int amount)
    {
        AddRange(new []
        {
            new BankAccountCommand(from, BankAccountCommand.Action.Withdraw, amount),
            new BankAccountCommand(to, BankAccountCommand.Action.Deposit, amount)
        });
    }

    public override void Call()
    {
        BankAccountCommand? last = null;

        foreach (var cmd in this)
        {
            if (last == null || last.Success)
            {
                cmd.Call();
                last = cmd;
            }
            else
            {
                cmd.Undo();
                break;
            }
        }
    }

    public override void Undo()
    {
        base.Undo();
    }
}

public static class Command2
{
    public static void AsComposite()
    {
        var account = new BankAccount();
        var deposit = new BankAccountCommand(account, BankAccountCommand.Action.Deposit, 100);
        var withdraw = new BankAccountCommand(account, BankAccountCommand.Action.Withdraw, 50);
        var withdraw2 = new BankAccountCommand(account, BankAccountCommand.Action.Withdraw, 600);
        var composite = new CompositeBankAccountCommand(
            new []
            {
                deposit, 
                withdraw,
                withdraw2
            });
        composite.Call();
        composite.Undo();
    }

    public static void AsMoneyTransfer()
    {
        var from = new BankAccount();
        var initFromAccount = new BankAccountCommand(from, BankAccountCommand.Action.Deposit, 500);
        initFromAccount.Call();
        var to = new BankAccount();
        Console.WriteLine(from.ToString());
        Console.WriteLine(to.ToString());
        var transfer = new MoneyTransferCommand(from, to, 300);
        transfer.Call();
        Console.WriteLine(from.ToString());
        Console.WriteLine(to.ToString());
    }

    public static void LocalMain()
    {
        AsMoneyTransfer();
    }
}