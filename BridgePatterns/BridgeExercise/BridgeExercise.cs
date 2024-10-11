namespace Patterns.BridgePatterns.BridgeExercise;

public interface IRenderer
{
    string WhatToRenderAs { get; }
}

public abstract class Shape
{
    private readonly IRenderer _renderer;
    
    protected Shape(IRenderer rendering, string name)
    {
        _renderer = rendering;
        Name = name;
    }

    protected string Name { get; set; }

    public override string ToString() => $"Drawing {Name} as {_renderer.WhatToRenderAs}";
}

public class Triangle : Shape
{
    public Triangle(IRenderer renderer) : base(renderer, "Triangle") { }
}

public class Square : Shape
{
    public Square(IRenderer renderer) : base(renderer, "Square") { }
}

public class RasterRenderer : IRenderer
{
    public string WhatToRenderAs => "pixels";
}

public class VectorRenderer : IRenderer
{
    public string WhatToRenderAs => "lines";
}


public static class BridgeExercise
{
    public static void LocalMAin()
    {
        IRenderer renderer = new RasterRenderer();
        var sq = new Square(renderer);
        Console.WriteLine(sq);
    }
}