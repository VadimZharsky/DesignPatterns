namespace Patterns.ProxyPatterns.ProxyPattern5;

public class MasonrySettings
{
    private readonly bool[] _flags = new bool[3];

    public bool? All
    {
        get => _flags.Skip(1).All(f => f == _flags[0]) ? _flags[0] : null;
        set
        {
            if (!value.HasValue) return;
            for (int i = 0; i < _flags.Length; i++)
            {
                _flags[i] = value.Value;
            }
        }
    }
    

    public bool Pillars
    {
        get => _flags[0];
        set => _flags[0] = value;
    }
    
    public bool Walls
    {
        get => _flags[1];
        set => _flags[1] = value;
    }
    
    public bool Floors
    {
        get => _flags[2];
        set => _flags[2] = value;
    }
}

public static class ProxyPattern5
{
    // Array backed properties
}