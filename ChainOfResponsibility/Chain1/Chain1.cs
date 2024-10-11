namespace Patterns.ChainOfResponsibility.Chain1;

public class Creature
{
    public Creature(string name, int attack, int defence)
    {
        Name = name;
        Attack = attack;
        Defence = defence;
    }
    
    public string Name { get; init; }
    public int Attack { get; set; }
    public int Defence { get; set; }

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Attack)}: {Attack}, {nameof(Defence)}: {Defence}";
    }
}

public class CreatureModifier
{
    private CreatureModifier? _next;
    
    public void Add(CreatureModifier modifier)
    {
        if (_next != null)
            _next.Add(modifier);
        else
            _next = modifier;
    }

    public virtual void Handle(Creature creature)
    {
        Thread.Sleep(500);
        _next?.Handle(creature);
    }
}

public class DoubleAttackModifier : CreatureModifier
{
    public override void Handle(Creature creature)
    {
        Console.WriteLine($"doubling {creature.Name}'s attack");
        creature.Attack *= 2;
        base.Handle(creature);
    }
}

public class IncreaseDefenseModifier : CreatureModifier
{
    public override void Handle(Creature creature)
    {
        Console.WriteLine($"increasing {creature.Name}'s defense");
        creature.Defence += 3;
        base.Handle(creature);
    }
}

public class NoBonusesModifier : CreatureModifier
{
    public override void Handle(Creature creature) => Console.WriteLine("do nothing");
}

public class ModifierBuilder
{
    private readonly CreatureModifier _modifier = new();

    public ModifierBuilder AddDoubleAttack()
    {
        _modifier.Add(new DoubleAttackModifier());
        return this;
    }

    public ModifierBuilder AddIncreaseDefense()
    {
        _modifier.Add(new IncreaseDefenseModifier());
        return this;
    }
    
    public ModifierBuilder AddNoBonuses()
    {
        _modifier.Add(new NoBonusesModifier());
        return this;
    }

    public CreatureModifier Build() => _modifier;
}

public static class Chain1
{
    // method chain
    
    public static void LocalMain()
    {
        var goblin = new Creature("goblin", 3, 3);
        Console.WriteLine(goblin);
        
        var modifier = new ModifierBuilder()
            .AddDoubleAttack()
            .AddIncreaseDefense()
            .AddNoBonuses()
            .AddDoubleAttack()
            .AddIncreaseDefense()
            .Build();
        
        modifier.Handle(goblin);
        Console.WriteLine(goblin);
    }
}