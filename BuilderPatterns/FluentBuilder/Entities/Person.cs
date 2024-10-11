namespace Patterns.BuilderPatterns.FluentBuilder.Entities;

public class Person
{
    public string Name { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
    }
}