namespace Patterns.BuilderPatterns.FacetedBuilder;

public class Person
{
    // address
    public string StreetAddress { get; set; } = string.Empty;
    public string PostCode { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
        
    //employment
    public string CompanyName { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;

    public int AnnualIncome { get; set; }

    public override string ToString() 
        => $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(PostCode)}: {PostCode}, {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}, " +
           $"{nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
}

public class PersonBuilder // facade
{
    // reference
    protected Person Person = new Person();

    public PersonJobBuilder Works => new PersonJobBuilder(Person);

    public PersonAddressBuilder Lives => new PersonAddressBuilder(Person);

    public Person Build() => Person;
}

public class PersonAddressBuilder : PersonBuilder
{
    public PersonAddressBuilder(Person person) => Person = person;
    public PersonAddressBuilder At(string streetAddress)
    {
        Person.StreetAddress = streetAddress;
        return this;
    }

    public PersonAddressBuilder WithPostCode(string postcode)
    {
        Person.PostCode = postcode;
        return this;
    }

    public PersonAddressBuilder In(string city)
    {
        Person.City = city;
        return this;
    }
}

public class PersonJobBuilder : PersonBuilder
{
    public PersonJobBuilder(Person person) => Person = person;
    public PersonJobBuilder At(string companyName)
    {
        Person.CompanyName = companyName;
        return this;
    }

    public PersonJobBuilder AsA(string position)
    {
        Person.Position = position;
        return this;
    }

    public PersonJobBuilder Earning(int amount)
    {
        Person.AnnualIncome = amount;
        return this;
    }
}

public static class FacetedBuilderClass
{
    public static void LocalMain()
    {
        var pb = new PersonBuilder();
        var person = pb
            .Works
                .At("IBA")
                .AsA("Developer")
                .Earning(4500)
            .Lives
                .In("Chelsey")
                .At("novagore st.")
                .WithPostCode("23334453")
            .Build();
        
        Console.WriteLine(person.ToString());
    }
}