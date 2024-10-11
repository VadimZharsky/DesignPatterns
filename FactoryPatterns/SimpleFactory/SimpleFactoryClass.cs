namespace Patterns.FactoryPatterns.SimpleFactory;

public class Point
{
    private double _x, _y;
    
    public static class Factory
    {
        public static Point NewCartesianPoint(double x, double y) => new (x, y);
    
        public static Point NewPolarPoint(double rho, double theta) => new (rho*Math.Cos(theta), rho*Math.Sin(theta));
    }
    
    private Point(double x, double y)
    {
        _x = x;
        _y = y;
    }
    
    public override string ToString() => $"{nameof(_x)}: {_x}, {nameof(_y)}: {_y}";
}

public static class SimpleFactoryClass
{
    public static void LocalMain()
    {
        var point1 = Point.Factory.NewCartesianPoint(4, 5);
        var point2 = Point.Factory.NewPolarPoint(4, 5);

        Console.WriteLine(point1);
        Console.WriteLine(point2);
    }
}