namespace Patterns.BuilderPatterns.FunctionalBuilder;

public class Person
{
    public string Name { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;

    public override string ToString() => $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
}

public abstract class FunctionalBuilder<TSubject, TSelf> 
    where TSelf : FunctionalBuilder<TSubject, TSelf>
    where TSubject : new()
{
    private readonly List<Func<TSubject, TSubject>> _actions = [];
    
    public TSelf Do(Action<TSubject> action) => AddAction(action);

    public TSubject Build() => _actions.Aggregate(new TSubject(), (p, f) => f(p));

    private TSelf AddAction(Action<TSubject> action)
    {
        _actions.Add(p => { action(p);
            return p;
        });
        return (TSelf) this;
    }
}

public sealed class PersonBuilder : FunctionalBuilder<Person, PersonBuilder>
{
    private readonly List<Func<Person, Person>> _actions = [];

    public PersonBuilder Called(string name) => Do(p => p.Name = name);

    public new PersonBuilder Do(Action<Person> action) => AddAction(action);

    public new Person Build() => _actions.Aggregate(new Person(), (p, f) => f(p));

    private PersonBuilder AddAction(Action<Person> action)
    {
        _actions.Add(p => { action(p);
            return p;
        });
        return this;
    }
}

public static class PersonBuilderExtensions
{
    public static PersonBuilder WorksAs(this PersonBuilder builder, string position) =>
        builder.Do(p => p.Position = position);
}

public static class FunctionalBuilderClass
{
    public static void LocalMain()
    {
        var person = new PersonBuilder().Called("Sarah").WorksAs("Specialist").Build();
        Console.WriteLine(person);
    }
}