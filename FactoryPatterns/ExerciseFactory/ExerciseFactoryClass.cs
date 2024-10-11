namespace Patterns.FactoryPatterns.ExerciseFactory;

public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }

    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}";
    }
}

public class PersonFactory
{
    private int _personCounter;

    public Person CreatePerson(string name) => new() { Id = _personCounter++, Name = name };
}

public static class ExerciseFactoryClass
{
    public static void LocalMain()
    {
        var pf = new PersonFactory();

        var person1 = pf.CreatePerson("Joe");
        var person2 = pf.CreatePerson("Chris");
        var person3 = pf.CreatePerson("Chris");
        Console.WriteLine(person1);
        Console.WriteLine(person2);
        Console.WriteLine(person3);
    }
}