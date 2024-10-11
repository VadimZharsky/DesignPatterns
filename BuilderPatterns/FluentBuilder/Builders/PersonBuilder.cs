using Patterns.BuilderPatterns.FluentBuilder.Entities;

namespace Patterns.BuilderPatterns.FluentBuilder.Builders;

public abstract class PersonBuilder
{
    protected Person Person = new Person();

    public Person Build() => Person;
}