using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Patterns.PrototypePatterns.PrototypeInheritance;

public interface IDeepCopyable<T> where T : new()
{
    void CopyTo(T target);
    
    public T DeepCopy()
    {
        T t = new T();
        CopyTo(t);
        return t;
    }
}

public class Person : IDeepCopyable<Person>
{
    public string Name { get; set; } = string.Empty; 
    public Address? Address { get; set; }

    public Person()
    {
        
    }
    public Person(string name, Address address)
    {
        Name = name;
        Address = address;
    }

    public void CopyTo(Person target)
    {
        target.Name = Name;
        // ReSharper disable once MergeConditionalExpression
        target.Address = Address?.DeepCopy();
    }

    public override string ToString() => $"{nameof(Name)}: {Name}, {nameof(Address)}: {Address}";
}

public class Employee : Person, IDeepCopyable<Employee>
{
    public decimal Salary { get; set; }

    public Employee() { }
    
    public Employee(string name, Address address, decimal salary) : base(name, address)
    {
        Salary = salary;
    }
    
    public override string ToString() => $"{base.ToString()}, {nameof(Salary)}: {Salary}";
    public void CopyTo(Employee target)
    {
        base.CopyTo(target);
        target.Salary = Salary;
    }
}

public class Address : IDeepCopyable<Address>
{
    public string StreetName { get; set; } = string.Empty;
    public int HouseNumber { get; set; }
    
    public Address()
    {
      
    }

    public Address(string streetName, int houseNumber)
    {
        StreetName = streetName;
        HouseNumber = houseNumber;
    }

    public void CopyTo(Address target)
    {
        target.StreetName = StreetName;
        target.HouseNumber = HouseNumber;
    }

    public override string ToString() => $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
}

public static class ExtensionMethods
{
    public static T DeepCopy<T>(this IDeepCopyable<T> item) where T : new() => item.DeepCopy();

    public static T DeepCopy<T>(this T person) where T : Person, new() => ((IDeepCopyable<T>)person).DeepCopy();

    public static T? DeepCopyViaSerialisation<T>(this T self)
    {
        try
        {
            var temp = JsonSerializer.Serialize(self);
            return JsonSerializer.Deserialize<T>(temp);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}

public static class PrototypeInheritanceClass
{

    private static T[] CopyArr<T>(T[] collection) => collection.Select(x => x).ToArray();

    public static void LocalMain()
    {
        var p1 = new Employee(
            name: "Joe", 
            address: new Address(
                streetName: "LowRider st.", 
                houseNumber: 22),
            salary: 4700);
        var p2 = p1.DeepCopyViaSerialisation();
        Console.WriteLine(p1);
        Console.WriteLine(p2);
        p1.Name = "Sara";
        p1.Salary = 3200;
        if (p1.Address != null)
        {
            p1.Address.StreetName = "MindPulse av.";
            p1.Address.HouseNumber = 23;
        }
        
        Console.WriteLine(p1);
        Console.WriteLine(p2);
        
    }
}