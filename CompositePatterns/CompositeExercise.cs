using System.Collections;

namespace Patterns.CompositePatterns;

public interface IValueContainer : IEnumerable<int>
{
      
}

public class SingleValue : IValueContainer
{
    public int Value;
    public IEnumerator<int> GetEnumerator()
    {
        yield return Value;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class ManyValues : List<int>, IValueContainer
{
      
}

public static class ExtensionMethods
{
    public static int Sum(this List<IValueContainer> containers)
    {
        int result = 0;
        foreach (var c in containers)
        foreach (var i in c)
            result += i;
        return result;
    }
}

public static class CompositeExercise
{
    public static void LocalMain()
    {
        var singleValue = new SingleValue {Value = 11};
        var otherValues = new ManyValues
        {
            22,
            33
        };
        var nextCollection = new List<IValueContainer> { singleValue, otherValues};
        Console.WriteLine(nextCollection.Sum());
    }
}