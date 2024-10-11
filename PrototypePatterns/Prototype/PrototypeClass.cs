namespace Patterns.PrototypePatterns.Prototype;

public interface IPrototype<out TObject>
{
    /// <summary>
    /// returns a deep copy of an object
    /// </summary>
    /// <returns></returns>
    TObject DeepCopy();
}

public class Person : IPrototype<Person>
{
    public string Name { get; set; }
    public Address Address { get; set; }

    public Person(string name, Address address)
    {
        Name = name;
        Address = address;
    }

    public Person(Person otherPerson)
    {
        Name = otherPerson.Name;
        Address = new Address(otherPerson.Address);
    }

    public Person DeepCopy() => new(Name, Address.DeepCopy());

    public override string ToString() => $"{nameof(Name)}: {Name}, {nameof(Address)}: {Address}";
}

public class Address : IPrototype<Address>
{
    public string StreetName { get; set; }
    public int HouseNumber { get; set; }

    public Address(string streetName, int houseNumber)
    {
        StreetName = streetName;
        HouseNumber = houseNumber;
    }

    public Address(Address otherAddress)
    {
        StreetName = otherAddress.StreetName;
        HouseNumber = otherAddress.HouseNumber;
    }

    public Address DeepCopy() => new(StreetName, HouseNumber);

    public override string ToString() => $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
}

public static class PrototypeClass
{
    public static void LocalMain()
    {
        var p1 = new Person("Joe", new Address("Baker Street", 34));
        var p2 = p1.DeepCopy();
        Console.WriteLine(p1);
        Console.WriteLine(p2);
        p2.Name = "Jane";
        p2.Address.StreetName = "SummerFall avenue";
        Console.WriteLine(p1);
        Console.WriteLine(p2);
    }
}