using JetBrains.Annotations;

namespace Patterns.MementoPattern.Memento1;

public class Memento
{
    public Memento(int balance)
    {
        Balance = balance;
    }

    public int Balance { get; }
}

public class BankAccount
{
    private int _balance;
    private List<Memento> _changes;
    private int _currentMemento;
    
    public BankAccount(int balance)
    {
        _balance = balance;
        _changes =
        [
            new Memento(_balance)
        ];
    }

    public Memento Deposit(int amount)
    {
        _balance += amount;
        var memento = new Memento(_balance);
        _changes.Add(memento);
        _currentMemento++;
        
        return memento;
    }
    
    public Memento Restore(Memento memento)
    {
        _balance = memento.Balance;
        _changes.Add(memento);
        return memento;
    }

    public override string ToString()
    {
        return $"{nameof(_balance)}: {_balance}";
    }

    public Memento? Undo()
    {
        if (_currentMemento == 0) return null;
        
        var m = _changes[--_currentMemento];
        _balance = m.Balance;
        return m;
    }

    public Memento? Redo()
    {
        if (_currentMemento + 1 >= _changes.Count) return null;
        
        var m = _changes[++_currentMemento];
        _balance = m.Balance;
        return m;
    }
}

public static class Memento1
{
    public static void LocalMain()
    {
        var ba = new BankAccount(100);
        ba.Deposit(50);
        ba.Deposit(25);
        Console.WriteLine(ba);

        ba.Undo();
        Console.WriteLine(ba);
        ba.Undo();
        Console.WriteLine(ba);
        ba.Undo();
        Console.WriteLine(ba);
        
        ba.Redo();
        Console.WriteLine(ba);
        ba.Redo();
        Console.WriteLine(ba);
        ba.Redo();
        Console.WriteLine(ba);
    }
}