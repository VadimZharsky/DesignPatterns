namespace Patterns.ProxyPatterns.ProxyPattern3;

public readonly struct Percentage
{
    public Percentage(float value)
    {
        Value = value;
    }
    
    public static float operator +(int f, Percentage p) => f + p.Value;
    
    public static float operator *(float f, Percentage p) => f * p.Value;

    public static Percentage operator +(Percentage p1, Percentage p2)
        => new (p1.Value + p2.Value);

    public float Value { get; }

    public override string ToString() => $"{Value * 100}%";
}

public static class PercentageExtensions
{
    public static Percentage Percent(this int value) => new Percentage(value / 100.0f);
    
    public static Percentage Percent(this float value) => new Percentage(value / 100.0f);
}

public static class ProxyPattern3
{
    //value proxy

    public static void LocalMain()
    {
        var r = 5.Percent();
        var b = 10.Percent();
        var t = r + b;
        Console.WriteLine(t.Value);
        Console.WriteLine(t);
    }
}