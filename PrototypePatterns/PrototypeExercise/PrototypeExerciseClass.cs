namespace Patterns.PrototypePatterns.PrototypeExercise;

public interface ICloneable<out T> where T : class
{
    T DeepCopy();
}

public class Point : ICloneable<Point>
{
    public int X, Y;

    public Point DeepCopy()
    {
        return new Point { X = X, Y = Y };
    }

    public override string ToString() => $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";
}

public class Line : ICloneable<Line>
{
    public Point Start, End;

    public Line DeepCopy()
    {
        return new Line { Start = Start.DeepCopy(), End = End.DeepCopy() };
    }

    public override string ToString() => $"{nameof(Start)}: {Start}, {nameof(End)}: {End}";
}

public static class PrototypeExerciseClass
{
    public static void LocalMain()
    {
        var l = new Line {Start = new Point { X = 13, Y = 15 }, End = new Point { X = 13, Y = 15 }};
        var r = l.DeepCopy();
        
        Console.WriteLine(l);
        Console.WriteLine(r);

        l.Start.X = 2;
        l.Start.Y = 3;
        
        Console.WriteLine(l);
        Console.WriteLine(r);
    }
}