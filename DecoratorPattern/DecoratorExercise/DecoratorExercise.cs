namespace Patterns.DecoratorPattern.DecoratorExercise;

public class Bird
{
    public int Age { get; set; }
      
    public string Fly() => Age < 10 ? "flying" : "too old";
}

public class Lizard
{
    public int Age { get; set; }
      
    public string Crawl() => Age > 1 ? "crawling" : "too young";
}

public class Dragon // no need for interfaces
{
    private readonly Bird _bird = new();
    private readonly Lizard _lizard = new();

    public int Age
    {
        get => _bird.Age;
        set
        {
            _bird.Age = value;
            _lizard.Age = value;
        }
    }

    public string Fly() => _bird.Fly();

    public string Crawl() => _lizard.Crawl();
}

public static class DecoratorExercise
{
    public static void LocalMain()
    {
        var dragon = new Dragon();

        Console.WriteLine(dragon.Fly());
        Console.WriteLine(dragon.Crawl());

        dragon.Age = 2;
        
        Console.WriteLine(dragon.Fly());
        Console.WriteLine(dragon.Crawl());

        dragon.Age = 12;
        
        Console.WriteLine(dragon.Fly());
        Console.WriteLine(dragon.Crawl());

        Console.WriteLine(dragon.Age);
    }
}