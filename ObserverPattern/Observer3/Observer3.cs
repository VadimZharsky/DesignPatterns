using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Patterns.ObserverPattern.Observer3;

public class Market
{
    private List<float> _prices = [];

    public event EventHandler<float>? PriceAdded; 
    
    public void AddPrice(float price)
    {
        _prices.Add(price);
        PriceAdded?.Invoke(this, price);
    }
}

public static class Observer3
{
    public static void LocalMain()
    {
        var market = new Market();

        market.PriceAdded += (_, f) =>
        {
            Console.WriteLine($"price added: {f}");
        };
        
        market.AddPrice(34);
        market.AddPrice(23);
    }
}