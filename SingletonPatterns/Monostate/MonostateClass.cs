namespace Patterns.SingletonPatterns.Monostate;

public class Ceo
{
    private static string _name = string.Empty;
    private static int _age;

    public string Name
    {
        get => _name;
        set => _name = value != string.Empty ? value : "not specified";
    }

    public int Age
    {
        get => _age;
        set => _age = value;
    }

    public override string ToString() => $"{nameof(Name)}: {Name}, {nameof(Age)}: {Age}";
}

public static class MonostateClass
{
    public static void LocalMain()
    {
        var ceo = new Ceo
        {
            Name = "Adam Smith",
            Age = 45
        };

        Console.WriteLine(ceo);

        var ceo2 = new Ceo();
        
        Console.WriteLine(ceo2);
        ceo2.Name = string.Empty;
        Console.WriteLine(ceo);
    }
}