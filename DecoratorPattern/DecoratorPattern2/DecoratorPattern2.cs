namespace Patterns.DecoratorPattern.DecoratorPattern2;

public interface IBird
{
    void Fly();
    int Weight { get; set; }
}

public interface ILizard
{
    void Crawl();
    int Weight { get; set; }
}

public class Bird : IBird
{
    public int Weight { get; set; }
    public void Fly() => Console.WriteLine($"Flying... weight: {Weight}");
}

public class Lizard : ILizard
{
    public int Weight { get; set; }
    public void Crawl() => Console.WriteLine($"Crawling... weight: {Weight}");
}

public class Dragon : IBird, ILizard
{
    private Bird _bird = new();
    private Lizard _lizard = new();
    private int _weight;

    public int Weight
    {
        get => _weight;
        set
        {
            _weight = value;
            _bird.Weight = _weight;
            _lizard.Weight = _weight;
        }
    }
    
    public void Fly() => _bird.Fly();
    
    public void Crawl() => _lizard.Crawl();
}

public static class DecoratorPattern2
{
    public static void LocalMain()
    {
        var d = new Dragon {Weight = 325};
        d.Fly();
        d.Crawl();
    }
}