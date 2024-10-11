namespace Patterns.ObserverPattern.ClassicObserver;

public abstract class StateEvent
{
    public TypeEvent ThisType { get; private set; } = TypeEvent.Initial;
    public string Message { get; init; }

    protected StateEvent(string message)
    {
        Message = message;
    }

    public void SetOnlyLog() => ThisType = TypeEvent.OnlyLog;

    public void SetStatusAndLog() => ThisType = TypeEvent.StatusAndLog;
    
    public enum TypeEvent
    {
        Initial,
        OnlyLog,
        StatusAndLog
    }
}

public class OnCreationEvent : StateEvent
{
    public OnCreationEvent(string message) 
        : base(message)
    {
    }
}

public class OnRemovingEvent : StateEvent
{
    public OnRemovingEvent(string message) 
        : base(message)
    {
    }
}

public interface IObservable
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void Notify(StateEvent stateEvent);
}

public interface IObserver
{
    void Update(StateEvent stateEvent);
}

public class StateHandler : IObserver
{
    public string Name { get; init; }

    public StateHandler(string name)
    {
        Name = name;
    }

    public void Update(StateEvent stateEvent)
    {
        Console.WriteLine($"handler: {Name} Got new event with type: {stateEvent.ThisType} " +
                          $"and message: {stateEvent.Message}");
    }
}

public class StateRepository : IObservable
{
    private readonly List<IObserver> _observers = [];
    
    public void Attach(IObserver observer) => _observers.Add(observer);

    public void Detach(IObserver observer) => _observers.Remove(observer);

    public void Notify(StateEvent stateEvent)
    {
        _observers.ForEach(x => x.Update(stateEvent));
    }
}

public class ServerHandler
{
    public IObservable StateKeeper { get; } = new StateRepository();

    public ServerHandler()
    {
    }

    public void Attach(IObserver observer) => StateKeeper.Attach(observer);

    public void Detach(IObserver observer) => StateKeeper.Detach(observer);
    
    public void AddEvent(StateEvent stateEvent) => StateKeeper.Notify(stateEvent);
}


public static class ClassicObserver
{
    public static void LocalMain()
    {
        var observers = new IObserver[]
        {
            new StateHandler("1"),
            new StateHandler("2"),
            new StateHandler("3")
        };

        var handler = new ServerHandler();
        AttachObservers(observers, handler);

        var ev1 = new OnCreationEvent("On creationEvent");
        ev1.SetStatusAndLog();

        var ev2 = new OnRemovingEvent("Removing");
        ev2.SetOnlyLog();
        
        handler.AddEvent(ev1);
        
        handler.Detach(observers[1]);
        
        handler.AddEvent(ev2);
    }

    public static void AttachObservers(IObserver[] observers, ServerHandler handler)
    {
        foreach (var observer in observers)
            handler.Attach(observer);
    }
}