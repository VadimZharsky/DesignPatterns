namespace Patterns.ObserverPattern.Observer2;

public class Event
{
    
}

public class FallsIllEvent : Event
{
    public required string Address { get; init; }
}

public class Person : IObservable<Event>
{
    private readonly HashSet<Subscription> _subscriptions = [];

    public IDisposable Subscribe(IObserver<Event> observer)
    {
        var sub = new Subscription(this, observer);
        _subscriptions.Add(sub);

        return sub;
    }

    public void FallsIll()
    {
        foreach (var subscription in _subscriptions)
        {
            subscription.Observer.OnNext(
                new FallsIllEvent { Address = "Some Road 123" }
            );
        }
    }

    private class Subscription : IDisposable
    {
        
        private readonly Person _person;

        public Subscription(Person person, IObserver<Event> observer)
        {
            Observer = observer;
            _person = person;
        }
        
        public IObserver<Event> Observer { get; }

        public void Dispose() => _person._subscriptions.Remove(this);
    }
}

public class Observer : IObserver<Event>
{
    public void OnCompleted() { }

    public void OnError(Exception error) { }

    public void OnNext(Event value)
    {
        if (value is FallsIllEvent ev)
        {
            Console.WriteLine($"A doctor is required at {ev.Address}");
        }
    }
}

public class Observer2
{
    public static void LocalMain()
    {
        var person = new Person();

        var observer = new Observer();

        var sub = person.Subscribe(observer);
        person.FallsIll();
        
    }
}