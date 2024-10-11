using Patterns.BuilderPatterns.FluentBuilder.Entities;

namespace Patterns.BuilderPatterns.FluentBuilder.Builders;

public class PersonInfoBuilder : PersonBuilder
{
    protected Person Person = new Person();

    public PersonInfoBuilder Called(string name)
    {
        Person.Name = name;
        return this;
    }
}