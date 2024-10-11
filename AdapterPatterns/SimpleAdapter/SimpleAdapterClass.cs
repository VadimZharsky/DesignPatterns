using System.Collections.ObjectModel;

namespace Patterns.AdapterPatterns.SimpleAdapter;

public class Point
{
    public readonly int X;
    public readonly int Y;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString() => $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";

    
    public override int GetHashCode()
    {
        unchecked
        {
            return (X * 397) ^ Y;
        }
    }
}

public class Line
{
    public Point Start, End;

    public Line(Point start, Point end)
    {
        Start = start;
        End = end;
    }

    public override string ToString() => $"{nameof(Start)}: {Start}, {nameof(End)}: {End}";
}

public class VectorObject : Collection<Line>
{
    
}

public class VectorRectangle : VectorObject
{
    public VectorRectangle(int x, int y, int width, int height)
    {
        Add(new Line(new Point(x, y), new Point(x + width, y)));
        Add(new Line(new Point(x + width, y), new Point(x + width, y + height)));
        Add(new Line(new Point(x, y), new Point(x, y + height)));
        Add(new Line(new Point(x, y + height), new Point(x + width, y + height)));
    }
}

public class LineToPointAdapter : List<Point>
{
    private static int _count;

    private static Dictionary<int, List<Point>> _cache = [];
    
    public LineToPointAdapter(Line line)
    {
        Console.WriteLine($"{++_count}: Generating points for line [{line.Start.X}, {line.Start.Y}]-" +
                          $"[{line.End.X}, {line.End.Y}]");
        AddRange([new Point(line.Start.X, line.Start.Y), new Point(line.End.X, line.End.Y)]);
    }
}

public static class SimpleAdapterClass
{
    private static readonly List<VectorObject> VectorObjects =
    [
        new VectorRectangle(1, 1, 10, 10),
        new VectorRectangle(3, 3, 6, 6)
    ];
    
    public static void DrawAPoint(Point p)
    {
        Console.Write(".");
    }

    public static void LocalMain()
    {
        foreach (var line in VectorObjects.SelectMany(vectorObject => vectorObject))
        {
            var adapter = new LineToPointAdapter(line);
            
            adapter.ForEach(DrawAPoint);
        }
    }
}