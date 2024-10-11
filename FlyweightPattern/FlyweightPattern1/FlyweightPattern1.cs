using JetBrains.dotMemoryUnit;
using NUnit.Framework;

namespace Patterns.FlyweightPattern.FlyweightPattern1;

public class User
{
    private string _fullName;

    public User(string fullName)
    {
        _fullName = fullName;
    }
}

public class User2
{
    private static readonly List<string> Strings = [];
    private int[] _names;

    public User2(string fullName)
    {
        _names = fullName.Split(' ').Select(GetOrAdd).ToArray();
        return;

        int GetOrAdd(string s)
        {
            int idx = Strings.IndexOf(s);
            if (idx != -1) return idx;
            
            Strings.Add(s);
            return Strings.Count - 1;
        }
    }

    public string FullName => string.Join(" ", _names.Select(i => Strings[i]));
}

[TestFixture]
public class TestClass
{
    [Test]
    public void TestUser()
    {
        var firstNames = Enumerable.Range(0, 1000).Select(_ => RandomString()).ToList();
        var lastNames = Enumerable.Range(0, 1000).Select(_ => RandomString()).ToList();

        var users = new List<User>();
        
        foreach (var firstName in firstNames) 
            users.AddRange(lastNames.Select(lastName => new User($"{firstName} {lastName}")));
        
        ForceGc();
        dotMemory.Check(memory =>
        {
            Console.WriteLine(memory.SizeInBytes / 1024);
        });
    }
    
    [Test]
    public void TestUser2()
    {
        var firstNames = Enumerable.Range(0, 1000).Select(_ => RandomString()).ToList();
        var lastNames = Enumerable.Range(0, 1000).Select(_ => RandomString()).ToList();

        var users = new List<User2>();
        
        foreach (var firstName in firstNames) 
            users.AddRange(lastNames.Select(lastName => new User2($"{firstName} {lastName}")));

        ForceGc();
        dotMemory.Check(memory =>
        {
            Console.WriteLine(memory.SizeInBytes / 1024);
        });
    }

    private void ForceGc()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
    }

    private static string RandomString()
    {
        var rnd = new Random();
        return new string(Enumerable
            .Range(0, 10)
            .Select(i => (char)('a' + rnd.Next(26)))
            .ToArray());
    }
}

public static class FlyweightPattern1
{
    public static void LocalMain()
    {
    }
}