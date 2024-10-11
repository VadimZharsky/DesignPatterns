using NUnit.Framework;
using Patterns.ProxyPatterns.ProxyPattern6;

namespace Patterns.ProxyPatterns.ProxyExercise;

public class Person
{
    public int Age { get; set; }

    public string Drink()
    {
        return "drinking";
    }

    public string Drive()
    {
        return "driving";
    }

    public string DrinkAndDrive()
    {
        return "driving while drunk";
    }
}

public class ResponsiblePerson
{
    private const string TooYoung = "too young";
    private const string TooDead = "dead";
        
    private readonly Person _person;
    private bool _canDrive, _canDrink;

    public ResponsiblePerson(Person person)
    {
        _person = person;
    }

    public int Age
    {
        get => _person.Age;
        init
        {
            _person.Age = value;
            _canDrink = _person.Age >= 18;
            _canDrive = _person.Age >= 16;
        }
    }

    public string Drink() => _canDrink ? _person.Drink() : TooYoung;

    public string Drive() => _canDrive ? _person.Drive() : TooYoung;

    public string DrinkAndDrive() => TooDead;
}

[TestFixture]
public class TestProxyExercise
{
    [Test]
    public void TestPersonUnder16()
    {
        var respPerson = new ResponsiblePerson(new Person()) {Age = 15};
        Assert.That(respPerson.Drive(), Is.EqualTo("too young"));
        Assert.That(respPerson.Drink(), Is.EqualTo("too young"));
        Assert.That(respPerson.DrinkAndDrive(), Is.EqualTo("dead"));
    }
    
    [Test]
    public void TestPersonUnder18()
    {
        var respPerson = new ResponsiblePerson(new Person()) {Age = 17};
        Assert.That(respPerson.Drive(), Is.EqualTo("driving"));
        Assert.That(respPerson.Drink(), Is.EqualTo("too young"));
        Assert.That(respPerson.DrinkAndDrive(), Is.EqualTo("dead"));
    }
    
    [Test]
    public void TestPersonGreat()
    {
        var respPerson = new ResponsiblePerson(new Person ()) {Age = 21};
        Assert.That(respPerson.Age, Is.EqualTo(21));
        Assert.That(respPerson.Drive(), Is.EqualTo("driving"));
        Assert.That(respPerson.Drink(), Is.EqualTo("drinking"));
        Assert.That(respPerson.DrinkAndDrive(), Is.EqualTo("dead"));
    }

    [Test]
    public void NotResponsiblePerson()
    {
        var p = new Person { Age = 15 };
        Assert.That(p.Age, Is.EqualTo(15));
        Assert.That(p.Drive(), Is.EqualTo("driving"));
        Assert.That(p.Drink(), Is.EqualTo("drinking"));
        Assert.That(p.DrinkAndDrive(), Is.EqualTo("driving while drunk"));
    }
}