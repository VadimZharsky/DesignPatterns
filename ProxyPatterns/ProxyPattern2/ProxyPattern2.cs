namespace Patterns.ProxyPatterns.ProxyPattern2;

public class Property<T> where T : new()
{
    private T _value;

    public T Value
    {
        get => _value;
        set
        {
            if (Equals(_value, value)) return;
            Console.WriteLine($"Assigning value to {value}");
            _value = value;
        }
    }

    public Property() : this(Activator.CreateInstance<T>()) { }

    public Property(T value)
    {
        _value = value;
    }

    public static implicit operator T(Property<T> property) => property._value; // int n = p_int;

    public static implicit operator Property<T>(T value) => new Property<T>(value);  // Property<int> p = 123;

    protected bool Equals(Property<T> other)
    {
        return EqualityComparer<T>.Default.Equals(_value, other._value);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Property<T>)obj);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<T>.Default.GetHashCode(_value);
    }
}

public class Creature
{
    private Property<int> _agility = new ();

    public int Agility
    {
        get => _agility.Value;
        set => _agility.Value = value;
    }
}

public static class ProxyPattern2
{
    public static void LocalMain()
    {
        var cr = new Creature { Agility = 15 };
        cr.Agility = 12;
    }
}