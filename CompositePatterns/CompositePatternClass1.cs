using System.Drawing;
using System.Text;

namespace Patterns.CompositePatterns;

public class GraphicObject
{
    private readonly Lazy<List<GraphicObject>> _graphicObjects = new();

    public virtual string Name { get; init; } = "Group";
    public string? Color { get; set; }
    public List<GraphicObject> GraphicObjects => _graphicObjects.Value;
    
    public override string ToString()
    {
        var sb = new StringBuilder();
        Print(sb, 1);
        return sb.ToString();
    }

    private void Print(StringBuilder sb, int depth)
    {
        sb.Append(new string(' ', depth))
            .Append(string.IsNullOrWhiteSpace(Color) ? string.Empty : $"{Color} ")
            .AppendLine(Name);
        GraphicObjects.ForEach(obj => obj.Print(sb, depth + 2));
    }
}

public class Circle : GraphicObject
{
    public override string Name => "Circle";
}

public class Square : GraphicObject
{
    public override string Name => "Square";
}

public class Rectangle : GraphicObject
{
    public override string Name => "Rectangle";
}

public static class CompositePatternClass1
{
    public static void LocalMain()
    {
        var drawing = new GraphicObject { Name = "My Drawing" };
        drawing.GraphicObjects.AddRange([
            new Square {Color = "Red"},
            new Circle {Color = "Blue"}
        ]);

        var group = new GraphicObject();
        var innerRectangle = new Rectangle { Color = "Green" };
        innerRectangle.GraphicObjects.Add(new Rectangle {Color = "Black"});
        group.GraphicObjects.AddRange([
            innerRectangle,
            new Square {Color = "Grey"}
        ]);
        
        drawing.GraphicObjects.AddRange([
            group,
            new Rectangle {Color = "Yellow"}
        ]);

        Console.WriteLine(drawing);
    }
}