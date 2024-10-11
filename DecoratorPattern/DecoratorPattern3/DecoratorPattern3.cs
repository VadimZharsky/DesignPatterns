namespace Patterns.DecoratorPattern.DecoratorPattern3;

public interface ICreature
{
    int Age { get; set; }
}

public interface IBird : ICreature
{
    void Fly()
    {
        if (Age >= 10)
            Console.WriteLine("I am flying");
    }
}

public interface ILizard : ICreature
{
    void Crawl()
    {
        if (Age < 10)
            Console.WriteLine("I am crawling");
    }

}

public abstract class Organizm : ICreature
{
    public int Age { get; set; }
}

public class Dragon : Organizm, IBird, ILizard
{
    
}


public static class DecoratorPattern3
{
    public static void LocalMain()
    {
        var d = new Dragon { Age = 15 };
        
        if (d is IBird bird)
            bird.Fly();
        if (d is ILizard lizard)
            lizard.Crawl();
    }
}