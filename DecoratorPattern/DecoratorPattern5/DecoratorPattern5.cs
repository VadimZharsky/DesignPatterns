namespace Patterns.DecoratorPattern.DecoratorPattern5;

public abstract class Shape
{
    public abstract string AsString();
}

public class Circle : Shape
{
    private float _radius;

    public Circle() { _radius = 0f; }

    public Circle(float radius)
    {
        _radius = radius;
    }

    public void Resize(float factor) => _radius *= factor;

    public override string AsString() => $"A circle with radius {_radius}";
}

public class Square : Shape
{
    private float _side;
    
    public Square() { _side = 0f; }

    public Square(float side)
    {
        _side = side;
    }

    public override string AsString() => $"A square with side {_side}";
}

public class ColoredShape : Shape
{
    private readonly Shape _shape;
    private readonly string _color;
    public ColoredShape(Shape shape, string color)
    {
        _shape = shape;
        _color = color;
    }

    public override string AsString() => $"{_shape.AsString()} with color {_color}";
}

public class TransparentShape<T> : Shape
    where T : Shape, new()
{
    private readonly T _shape = new ();
    private float _transparency;

    public TransparentShape() { _transparency = 0f; }

    public TransparentShape(float transparency)
    {
        Transparency = transparency;
    }
    public override string AsString()  => $"{_shape.AsString()} has the transparency {_transparency * 100.0f}%";

    private float Transparency
    {
        set
        {
            _transparency = value switch
            {
                > 1 => 1,
                < 0 => 0,
                _ => value
            };
        }
    }

}

public class ColoredShape<T> : Shape where T : Shape, new()
{
    private readonly string _color;
    private readonly T _shape = new T();

    public ColoredShape() : this("black") { }

    public ColoredShape(string color)
    {
        _color = color;
    }

    public override string AsString() => $"{_shape.AsString()} with color {_color}";
}

public static class DecoratorPattern5
{
    // static decorator composition
    
    public static void LocalMain()
    {
        var redSquare = new TransparentShape<ColoredShape<Square>>(0.78f);
        Console.WriteLine(redSquare.AsString());
    }
}