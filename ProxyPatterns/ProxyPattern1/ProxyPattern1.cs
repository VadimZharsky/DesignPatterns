namespace Patterns.ProxyPatterns.ProxyPattern1;

public interface ICar
{
    void Drive();
}

public class Car : ICar
{
    public void Drive() => Console.WriteLine("The car is being driven");
}

public class Driver
{
    public int Age { get; set; }
}

public class CarProxy : ICar
{
    private Driver _driver;
    private Car _car = new Car();

    public CarProxy(Driver driver)
    {
        _driver = driver;
    }

    public void Drive()
    {
        if (_driver.Age >= 16)
            _car.Drive();
        else
            Console.WriteLine("Driving impossible. Too young");
    }
}

public static class ProxyPattern1
{
    // protection proxy

    public static void TryDrive(ICar car) => car.Drive();

    public static void LocalMain()
    {
        var car = new Car();
        var carProxy = new CarProxy(new Driver {Age = 12});
        
        TryDrive(car);
        TryDrive(carProxy);
    }
}