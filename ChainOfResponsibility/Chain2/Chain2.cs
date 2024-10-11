
namespace Patterns.ChainOfResponsibility.Chain2;

public class Game
{
    public event EventHandler<Query>? Queries;

    public void PerformQuery(object sender, Query q) => Queries?.Invoke(sender, q);
}

public class Query
{
    public enum Argument { Attack, Defense }

    public Query(string creatureName, Argument whatToQuery, int value)
    {
        CreatureName = creatureName;
        WhatToQuery = whatToQuery;
        Value = value;
    }
    
    public string CreatureName { get; }
    public Argument WhatToQuery { get; }
    public int Value { get; set; }
}

public class Creature
{
    public Game Game { get; set; }
    public string Name { get; set; }
    private int _attack;
    private int _defense;

    public Creature(Game game, string name, int attack, int defense)
    {
        Game = game;
        Name = name;
        _attack = attack;
        _defense = defense;
    }

    public int Attack
    {
        get
        {
            var q = new Query(Name, Query.Argument.Attack, _attack);
            Game.PerformQuery(this, q);
            return q.Value;
        }
    }
    
    public int Defense
    {
        get
        {
            var q = new Query(Name, Query.Argument.Defense, _defense);
            Game.PerformQuery(this, q);
            return q.Value;
        }
    }

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defense)}: {Defense}";
    }
}

public abstract class CreatureModifier : IDisposable
{
    protected Game Game;
    protected Creature Creature;

    protected CreatureModifier(Game game, Creature creature)
    {
        Game = game;
        Creature = creature;
        Game.Queries += Handle;
    }

    protected abstract void Handle(object? sender, Query query);

    public void Dispose() => Game.Queries -= Handle;
}

public class DoubleAttackModifier : CreatureModifier
{
    public DoubleAttackModifier(Game game, Creature creature) : base(game, creature) { }

    protected override void Handle(object? sender, Query query)
    {
        if (query.CreatureName == Creature.Name
            && query.WhatToQuery == Query.Argument.Attack)
            query.Value *= 2;
    }
}

public class IncreaseDefenseModifier : CreatureModifier
{
    public IncreaseDefenseModifier(Game game, Creature creature) : base(game, creature) { }

    protected override void Handle(object? sender, Query query)
    {
        if (query.CreatureName == Creature.Name
            && query.WhatToQuery == Query.Argument.Defense)
            query.Value += 2;
    }
}

public static class Chain2
{
    // broker chain
    
    public static void LocalMain()
    {
        var game = new Game();
        var goblin = new Creature(game, "goblin", 3, 3);
        Console.WriteLine(goblin);

        using (new DoubleAttackModifier(game, goblin))
        {
            Console.WriteLine(goblin);
            using (new IncreaseDefenseModifier(game, goblin))
            {
                Console.WriteLine(goblin);
            }
        }

        Console.WriteLine(goblin);
    }
}