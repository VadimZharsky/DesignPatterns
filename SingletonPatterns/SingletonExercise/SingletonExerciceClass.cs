namespace Patterns.SingletonPatterns.SingletonExercise;

public class Person
{
    public string Name { get; set; }

    public Person(string name)
    {
        Name = name;
    }
}


public class SingletonTester
{
    public static bool IsSingleton(Func<object> func)
    {
        var obj1 = func();
        var obj2 = func();
        return ReferenceEquals(obj1, obj2);
    }
}

public static class SingletonExerciseClass
{
    public static void LocalMain()
    {
        var p1 = new Person(name: "John");
        Console.WriteLine(SingletonTester.IsSingleton(() => new Person("Jane")));
    }
}