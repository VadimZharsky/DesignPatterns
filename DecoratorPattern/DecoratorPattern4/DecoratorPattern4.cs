using Patterns.BridgePatterns.BridgeExercise;

namespace Patterns.DecoratorPattern.DecoratorPattern4;

public interface IShape
{
    string AsString();
}

public class Circle : IShape
{
    private float _radius;

    public Circle(float radius)
    {
        _radius = radius;
    }

    public void Resize(float factor) => _radius *= factor;

    public string AsString() => $"A circle with radius {_radius}";
}

public class Square : IShape
{
    private float _side;

    public Square(float side)
    {
        _side = side;
    }

    public string AsString() => $"A square with side {_side}";
}

public class TransparentShape : IShape
{
    private readonly IShape _shape;
    private float _transparency;

    public TransparentShape(IShape shape, float transparency)
    {
        _shape = shape;
        Transparency = transparency;
    }
    public string AsString()  => $"{_shape.AsString()} has the transparency {_transparency * 100.0f}%";

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

public interface IShapeDecoratorCyclePolicy
{
    public abstract bool TypeAdditionAllowed(Type type, IList<Type> allTypes);
    public abstract bool ApplicationAllowed(Type type, IList<Type> allTypes);
}

public class ThrowOnCyclePolicy : IShapeDecoratorCyclePolicy
{
    public bool TypeAdditionAllowed(Type type, IList<Type> allTypes)
    {
        return Handler(type, allTypes);
    }

    public bool ApplicationAllowed(Type type, IList<Type> allTypes)
    {
        return Handler(type, allTypes);
    }
    
    private bool Handler(Type type, IList<Type> allTypes)
    {
        if (!allTypes.Contains(type))
            return true;
        throw new InvalidOperationException($"Cycle detected! Type is already a {type.FullName}");
    }
}

public abstract class ShapeDecorator : IShape
{
    protected readonly List<Type> Types = [];

    protected readonly IShape Shape;

    protected ShapeDecorator(IShape shape)
    {
        Shape = shape;
        if (shape is ShapeDecorator sd)
            Types.AddRange(sd.Types);
    }

    public abstract string AsString();
}

public abstract class ShapeDecorator<TSelf, TCyclePolicy> : ShapeDecorator
    where TCyclePolicy : IShapeDecoratorCyclePolicy, new()
{
    private readonly TCyclePolicy Policy = new();
    
    protected ShapeDecorator(IShape shape) : base(shape)
    {
        if (Policy.TypeAdditionAllowed(typeof(TSelf), Types))
            Types.Add(typeof(TSelf));
    }
}

public class ColoredShape : ShapeDecorator<ColoredShape, ThrowOnCyclePolicy>
{
    private readonly string _color;
    public ColoredShape(IShape shape, string color) : base(shape)
    {
        _color = color;
    }

    public override string AsString() => $"{Shape.AsString()} with color {_color}";
}

public static class DecoratorPattern4
{
    // Dynamic decorator
    
    public static void LocalMain()
    {
        var square = new Square(1.23f);
        Console.WriteLine(square.AsString());

        var greenSquare = new ColoredShape(square, "green");
        Console.WriteLine(greenSquare.AsString());
        
        // var blueSquare = new ColoredShape(greenSquare, "green");
        // Console.WriteLine(blueSquare.AsString());

        var greenTransparentSquare = new TransparentShape(greenSquare, 0.56f);
        Console.WriteLine(greenTransparentSquare.AsString());
    }
}