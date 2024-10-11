namespace Patterns.ObserverPattern.Observer1;

public class Person
{
    public event EventHandler<FallsEventArgs>? Falls;

    public Person(string name)
    {
        Name = name;
    }

    public string Name { get; }

    public void CatchACold()
    {
        Falls?.Invoke(this, 
            new FallsEventArgs(address: "123 London Road")
        );
    }
    
    public class FallsEventArgs : EventArgs
    {
        public string Address { get; }

        public FallsEventArgs(string address)
        {
            Address = address;
        }
    }
}

public static class Observer1
{
    public static void LocalMain()
    {
        var person = new Person(name: "Somebody");
        
        person.Falls += PersonOnFalls;
        
        person.CatchACold();
    }

    private static void PersonOnFalls(object? sender, Person.FallsEventArgs e)
    {
        if (sender == null) return;
        var personSender = (Person)sender;
        Console.WriteLine($"person: {personSender.Name} calls to {e.Address}");
    }
}