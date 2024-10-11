namespace Patterns.AdapterPatterns;

public class Square
{
    public double Side;
}

public class Circle
{
    public double Radius;
}

public interface IRectangle
{
    double Width { get; }
    double Height { get; }
}

public static class ExtensionMethods
{
    public static double Area(this IRectangle rc)
    {
        return rc.Width * rc.Height;
    }
}

public class SquareToRectangleAdapter : IRectangle
{
    private readonly Square _square;
    
    public SquareToRectangleAdapter(Square square)
    {
        _square = square;
    }
    public double Width => _square.Side;
    public double Height => _square.Side;
}

public class CircleToRectangleAdapter : IRectangle
{
    private readonly Circle _circle;
    
    public CircleToRectangleAdapter(Circle circle)
    {
        _circle = circle;
    }

    public double Width => _circle.Radius * _circle.Radius;
    public double Height => Math.PI;
}

public static class AdapterExercise
{
    public static void LocalMain()
    {
        var sq = new Square {Side = 5};

        var adapter = new SquareToRectangleAdapter(sq);
        Console.WriteLine(adapter.Area());

        var circle = new Circle {Radius = 24};
        var circleAdapter = new CircleToRectangleAdapter(circle);
        Console.WriteLine(circleAdapter.Area());
    }
}