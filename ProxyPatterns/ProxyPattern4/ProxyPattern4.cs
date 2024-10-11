namespace Patterns.ProxyPatterns.ProxyPattern4;

public class Creature
{
    public byte Age;
    public int X, Y;

}

public class Creatures
{
    private readonly int _size;
    private byte[] _age;
    private int[] _x, _y;

    public Creatures(int size)
    {
        _size = size;
        _age = new byte[_size];
        _x = new int[_size];
        _y = new int[_size];
    }
    
    public readonly struct CreatureProxy
    {
        private readonly Creatures _creatures;

        public CreatureProxy(Creatures creatures, int index)
        {
            _creatures = creatures;
            Index = index;
        }

        public int Index { get; }
        public ref byte Age => ref _creatures._age[Index];
        public ref int X => ref _creatures._x[Index];
        public ref int Y => ref _creatures._y[Index];
    }

    public IEnumerator<CreatureProxy> GetEnumerator()
    {
        for (int pos = 0; pos < _size; ++pos)
        {
            yield return new CreatureProxy(this, pos);
        }
    }
}

public static class ProxyPattern4
{
    public static void LocalMain()
    {
        var creatures = new Creatures(100);
        foreach (var proxy in creatures)
        {
            Console.WriteLine(proxy.Index);
            proxy.X++;
        }
    }
}