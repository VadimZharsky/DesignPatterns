using NUnit.Framework;

namespace Patterns.ChainOfResponsibility.Exercise;

public abstract class Creature
{
    public int Attack { get; set; }
    public int Defense { get; set; }

    public void Increase(int attack, int defense)
    {
        Attack += attack;
        Defense += defense;
    }

}

public class Goblin : Creature
{
    public Goblin(Game game)
    {
        Attack = 1;
        Defense = 1;
    }
}

public class GoblinKing : Goblin
{
    public GoblinKing(Game game) : base(game)
    {
        Attack = 3;
        Defense = 3;
    }
}

public class Creatures
{
    private List<Creature> _creatures = new List<Creature>();

    public void Add(Creature creature)
    {
        _creatures.Add(creature);
        if (_creatures.Count <= 1) return;
        
        var increaseDefense = 0;
        var increaseAttack = 0;
        var type = creature.GetType();
        if (type == typeof(Goblin))
            increaseDefense += 1;
        else if (type == typeof(GoblinKing))
        {
            increaseDefense += 1;
            increaseAttack += 1;
        }
        foreach (var thisCreature in _creatures)
            thisCreature.Increase(increaseAttack, increaseDefense);
    }
}

public class Game
{
    public readonly Creatures Creatures;
    
    public Game()
    {
        Creatures = new Creatures();
    }
}

[TestFixture]
public class TestExercise
{
    [Test]
    public void ManyGoblinsTest()
    {
        var game = new Game();
        var goblin = new Goblin(game);
        game.Creatures.Add(goblin);
        
        Assert.That(goblin.Attack, Is.EqualTo(1));
        Assert.That(goblin.Defense, Is.EqualTo(1));
        
        var goblin2 = new Goblin(game);
        game.Creatures.Add(goblin2);

        Assert.That(goblin.Attack, Is.EqualTo(1));
        Assert.That(goblin.Defense, Is.EqualTo(2));
        
        var goblin3 = new GoblinKing(game);
        game.Creatures.Add(goblin3);

        Assert.That(goblin.Attack, Is.EqualTo(2));
        Assert.That(goblin.Defense, Is.EqualTo(3));
    }
}

public static class ChainExercise
{
    public static void LocalMain()
    {
        var game = new Game();
        var goblin = new GoblinKing(game);
        Console.WriteLine(goblin.GetType() == typeof(Goblin));
    }
}