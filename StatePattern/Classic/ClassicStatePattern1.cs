namespace Patterns.StatePattern.Classic;

public class Switch
{
    public State State { get; set; } = new OffState();

    public void On()
    {
        State.On(this);
    }

    public void Off()
    {
        State.Off(this);
    }
}

public abstract class State
{
    public virtual void On(Switch sw)
    {
        Console.WriteLine("Light is already on.");
    }
    
    public virtual void Off(Switch sw)
    {
        Console.WriteLine("Light is already off.");
    }
}

public class OnState : State
{
    public OnState()
    {
        Console.WriteLine("Light turned on.");
    }

    public override void Off(Switch sw)
    {
        Console.WriteLine("turning light off...");
        sw.State = new OffState();
    }
}

public class OffState : State
{
    public OffState()
    {
        Console.WriteLine("Light turned off.");
    }

    public override void On(Switch sw)
    {
        Console.WriteLine("turning light on...");
        sw.State = new OnState();
    }
}

public static class ClassicStatePattern1
{
    public static void LocalMain()
    {
        var ls = new Switch();
        ls.Off();
        ls.On();
        ls.On();
        ls.Off();
    }
}