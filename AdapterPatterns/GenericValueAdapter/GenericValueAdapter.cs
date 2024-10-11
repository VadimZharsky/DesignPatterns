namespace Patterns.AdapterPatterns.GenericValueAdapter;

public interface IInteger
{
    int Value { get; }
}

public static class Dimensions
{
    public class Two : IInteger
    {
        public int Value => 2;
    }

    public class Three : IInteger
    {
        public int Value => 3;
    }
}

public class Vector<T, TDimension> where TDimension : IInteger, new()
{
    protected T[] Data;

    public Vector(params T[] values)
    {
        var requiredSize = new TDimension().Value;
        Data = new T[requiredSize];

        var providedSize = values.Length;

        for (int i = 0; i < Math.Min(requiredSize, providedSize); i++)
        {
            Data[i] = values[i];
        }
    }

    protected Vector()
    {
        Data = new T[new TDimension().Value];
    }

    public int DataSize => Data.Length;

    public override string ToString()
    {
        return string.Join(',', Data);
    }

    public T this[int index]
    {
        get => Data[index];
        set => Data[index] = value;
    }

    public T X
    {
        get => Data[0];
        set => Data[0] = value;
    }
}

public class Vector2I : Vector<int, Dimensions.Two>
{
    public Vector2I(params int[] values) : base(values)
    {
    }

    public Vector2I()
    {
    }
}

public static class GenericValueAdapter
{
    public static void LocalMain()
    {
        var v = new Vector2I(4, 3, 6);
        Console.WriteLine($"Size: {v.DataSize}\nContent: {v}");

        var vv = new Vector2I();
    }
}