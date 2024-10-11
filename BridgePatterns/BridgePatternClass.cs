namespace Patterns.BridgePatterns;

public interface IRenderer
{
    void RenderCircle(float radius);
}

public class VectorRenderer : IRenderer
{
    public void RenderCircle(float radius)
    {
        Console.WriteLine($"Drawing a circle with radius {radius}");
    }
}

public class RasterRenderer : IRenderer
{
    public void RenderCircle(float radius)
    {
        Console.WriteLine($"Drawing pixels for circle with radius {radius}");
    }
}

public abstract class Shape
{
    protected IRenderer Renderer;

    protected Shape(IRenderer renderer)
    {
        Renderer = renderer;
    }

    public abstract void Draw();

    public abstract void Resize(float factor);
}

public class Circle : Shape
{
    private float _radius;

    public Circle(IRenderer renderer, float radius) : base(renderer)
    {
        _radius = radius;
    }

    public override void Draw() => Renderer.RenderCircle(_radius);

    public override void Resize(float factor) => _radius *= factor;
}

public static class BridgePatternClass
{
    public static void LocalMain()
    {
        IRenderer renderer = new VectorRenderer();
        var circle = new Circle(renderer, 5);
        circle.Draw();
        circle.Resize(3.15f);
        circle.Draw();
    }
}