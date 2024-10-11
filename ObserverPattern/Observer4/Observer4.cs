using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Patterns.ObserverPattern.Observer4;

public class PropertyNotificationSupport : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private readonly Dictionary<string, HashSet<string>> _affectedByDict = [];

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        if (propertyName == null) return;
        
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        foreach (var key in _affectedByDict.Keys
                     .Where(key => _affectedByDict[key].Contains(propertyName)))
        {
            OnPropertyChanged(key);
        }
    }
}

public class Person : PropertyNotificationSupport
{
    private int _age;

    public int Age
    {
        get => _age;
        set
        {
            if (value == _age) return;
            _age = value;
            OnPropertyChanged();
        }
    }

    public bool CanVote => Age >= 16;
    
}

public static class Observer4
{
    public static void LocalMain()
    {
        Console.WriteLine(123);
    }
}