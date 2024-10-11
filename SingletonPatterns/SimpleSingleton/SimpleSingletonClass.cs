using System.Text;
using Autofac;
using NUnit.Framework;

namespace Patterns.SingletonPatterns.SimpleSingleton;

public interface IDatabase
{
    string GetBrandSpecification(string brandName);
}

public class CarBrand
{
    public string BrandName { get; init; }
    public int Amount { get; private set; }

    public CarBrand(string brandName, int amount)
    {
        BrandName = brandName;
        Amount = amount;
    }

    public void AppendCar() => Amount++;
    
    public void SellCar() => Amount--;

    public override string ToString() => $"{nameof(BrandName)}: {BrandName}, {nameof(Amount)}: {Amount}";
}

public class SingletonDatabase : IDatabase
{
    private static readonly Lazy<SingletonDatabase> Instance = new(() => new SingletonDatabase());
    private readonly List<CarBrand> _carBrands = [];

    private SingletonDatabase()
    {
        Count++;
        Console.WriteLine("db initializing...");
        _carBrands.Add(new CarBrand("BMW", 30));
        _carBrands.Add(new CarBrand("Volvo", 45));
        _carBrands.Add(new CarBrand("Mazda", 27));
        Thread.Sleep(500);
    }

    public static SingletonDatabase Database => Instance.Value;

    public static int Count { get; private set; }

    public string GetBrandSpecification(string brandName)
    {
        var tempBrand = _carBrands.FirstOrDefault(carBrand => carBrand.BrandName == brandName);
        return tempBrand != null ? tempBrand.ToString() : string.Empty;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        _carBrands.ForEach(x => sb.AppendLine(x.ToString()));
        return sb.ToString();
    }
}

public class OrdinaryDatabase : IDatabase
{
    private readonly List<CarBrand> _carBrands = [];

    private OrdinaryDatabase()
    {
        Console.WriteLine("db initializing...");
        _carBrands.Add(new CarBrand("BMW", 30));
        _carBrands.Add(new CarBrand("Volvo", 45));
        _carBrands.Add(new CarBrand("Mazda", 27));
        Thread.Sleep(500);
    }

    public string GetBrandSpecification(string brandName)
    {
        var tempBrand = _carBrands.FirstOrDefault(carBrand => carBrand.BrandName == brandName);
        return tempBrand != null ? tempBrand.ToString() : string.Empty;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        _carBrands.ForEach(x => sb.AppendLine(x.ToString()));
        return sb.ToString();
    }
}

public class DummyDataBase : IDatabase
{
    public string GetBrandSpecification(string brandName)
    {
        return new CarBrand("Renault", 56).ToString();
    }
}

public class ConfigurableRecordFinder
{
    private readonly IDatabase _database;

    public ConfigurableRecordFinder(IDatabase database) => 
        _database = database ?? throw new ArgumentNullException(nameof(database));

    public string GetBrandSpecification(string brandName) => _database.GetBrandSpecification(brandName);
}

[TestFixture]
public class SingletonTests
{
    [Test]
    public void IsSingletonTest()
    {
        var d = SingletonDatabase.Database;
        var s = SingletonDatabase.Database;
        Assert.That(d, expression: Is.SameAs(s));
        Assert.That(SingletonDatabase.Count, expression: Is.EqualTo(expected: 1));
    } 
}

public static class SimpleSingletonClass
{
    public static void LocalMain()
    {
        var builder = new ContainerBuilder();
        builder
            .RegisterType<OrdinaryDatabase>()
            .As<IDatabase>()
            .SingleInstance();
        
        builder.RegisterType<ConfigurableRecordFinder>();

        using (var container = builder.Build())
        {
            var rf = container.Resolve<ConfigurableRecordFinder>();
            Console.WriteLine(rf.GetBrandSpecification("BMW"));
        }
        
    }
}