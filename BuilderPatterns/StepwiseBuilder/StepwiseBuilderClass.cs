namespace Patterns.BuilderPatterns.StepwiseBuilder;

public enum CarType
{
    Sedan,
    Crossover
}

public record Car(CarType CarType, int WheelSize);

public static class StepwiseBuilderClass
{
    public static void LocalMain()
    {
        var car = new Car(CarType: CarType.Sedan, WheelSize: 16);
        Console.WriteLine(car);
    }
}