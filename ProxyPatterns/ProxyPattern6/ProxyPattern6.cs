using System.ComponentModel;
using System.Runtime.CompilerServices;
using NUnit.Framework;

namespace Patterns.ProxyPatterns.ProxyPattern6;

//model
public class Person
{
    public string FirstName, LastName;

    public Person(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}


//viewmodel
public class PersonViewModel
{
    private readonly Person _person;

    public PersonViewModel(Person person)
    {
        _person = person;
    }

    public bool OnChanged { get; private set; }

    public string FirstName
    {
        get => _person.FirstName;
        set
        {
            if (_person.FirstName == value) return;
            OnChanged = true;
            _person.FirstName = value;
        }
    }
    
    public string LastName
    {
        get => _person.LastName;
        set
        {
            if (_person.LastName == value) return;
            OnChanged = true;
            _person.LastName = value;
        }
    }
}

[TestFixture]
public class TestClass
{
    [TestCase("John", "Smith", "John", false)]
    [TestCase("John", "Smith", "Smoothie", true)]
    public void TryChangePersonName(string name, string secondName, string alteredName, bool changed)
    {
        var personView = new PersonViewModel(new Person(name, secondName));
        Assert.That(personView.FirstName, Is.EqualTo(name));
        personView.FirstName = alteredName;
        Assert.That(personView.OnChanged, Is.EqualTo(changed));
    }
    
    [TestCase("John", "Smith", "Smith", false)]
    [TestCase("John", "Smith", "Smoothie", true)]
    public void TryChangePersonLastName(string name, string secondName, string alteredName, bool changed)
    {
        var personView = new PersonViewModel(new Person(name, secondName));
        Assert.That(personView.LastName, Is.EqualTo(secondName));
        personView.LastName = alteredName;
        Assert.That(personView.OnChanged, Is.EqualTo(changed));
    }
}


public static class ProxyPattern6
{
    //View Model
    
    public static void LocalMain()
    {
        
    }
}