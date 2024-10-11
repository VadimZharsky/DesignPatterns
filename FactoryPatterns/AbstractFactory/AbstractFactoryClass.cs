namespace Patterns.FactoryPatterns.AbstractFactory;

public interface IHotDrink
{
    void Consume();
}

public interface IHotDrinkFactory
{
    IHotDrink Prepare(int amount);
}

internal class Tea : IHotDrink
{
    public void Consume() => Console.WriteLine("such a good tea");
}

internal class Coffee : IHotDrink
{
    public void Consume() => Console.WriteLine("such a good coffee");
}

internal class TeaFactory : IHotDrinkFactory
{
    public IHotDrink Prepare(int amount)
    {
        Console.WriteLine($"preparing a {amount} cup(s) of tea");
        return new Tea();
    }
}

internal class CoffeeFactory : IHotDrinkFactory
{
    public IHotDrink Prepare(int amount)
    {
        Console.WriteLine($"preparing a {amount} cup(s) of coffee");
        return new Coffee();
    }
}

public class HotDrinkMachine
{
    // region violating OSP
    // public enum AvailableDrink
    // {
    //     Coffee, Tea
    // }
    //
    // private Dictionary<AvailableDrink, IHotDrinkFactory> _factories = [];
    //
    // public HotDrinkMachine()
    // {
    //     foreach (AvailableDrink available in Enum.GetValues(typeof(AvailableDrink)))
    //     {
    //         var thisType = Type.GetType("Patterns.FactoryPatterns.AbstractFactory." + Enum.GetName(typeof(AvailableDrink), available) + "Factory");
    //         
    //         if (thisType == null) continue;
    //         
    //         var factoryObject = Activator.CreateInstance(thisType);
    //         
    //         if (factoryObject == null) continue;
    //         
    //         _factories.Add(available, (IHotDrinkFactory)factoryObject);
    //     }
    // }
    //
    // public IHotDrink MakeDrink(AvailableDrink drink, int amount)
    // {
    //     return _factories[drink].Prepare(amount);
    // }

    private List<Tuple<string, IHotDrinkFactory>> _factories = [];
    
    public HotDrinkMachine()
    {
        foreach (var t in typeof(HotDrinkMachine).Assembly.GetTypes())
        {
            if (typeof(IHotDrinkFactory).IsAssignableFrom(t) && !t.IsInterface)
                _factories.Add(Tuple.Create(
                    t.Name.Replace("Factory", string.Empty), 
                    (IHotDrinkFactory)Activator.CreateInstance(t)
                    ));
        }
    }

    public IHotDrink MakeDrink(int type, int amount)
    {
        Console.WriteLine("available:");
        _factories.ForEach(t => Console.WriteLine($"{t.Item1}"));
        return _factories[type].Item2.Prepare(amount);
    }
}

public static class AbstractFactoryClass
{
    public static void LocalMain()
    {
        var machine = new HotDrinkMachine();
        var drink = machine.MakeDrink(0, 10);
        drink.Consume();
    }
}