using Patterns.ColorPrint;

namespace Patterns.StatePattern.State2;

public enum State
{
    OffHook,
    Connecting,
    Connected,
    OnHold
}

public enum Trigger
{
    CallDialed,
    HungUp,
    CallConnected,
    PlacedOnHold,
    TakenOffHold,
    LeftMessage
}

public class State2
{
    // handmade state machine

    private static readonly Dictionary<State, List<(Trigger, State)>> Rules =
        new()
        {
            [State.OffHook] = [(Trigger.CallDialed, State.Connecting)],
            [State.Connecting] = 
            [
                (Trigger.HungUp, State.OffHook), 
                (Trigger.CallConnected, State.Connected)
            ],
            [State.Connected] = 
            [
                (Trigger.LeftMessage, State.OffHook),
                (Trigger.HungUp, State.OffHook),
                (Trigger.PlacedOnHold, State.OnHold)
            ],
            [State.OnHold] = 
            [
                (Trigger.TakenOffHold, State.Connected),
                (Trigger.HungUp, State.OffHook)
            ]
        };
    
    public static void LocalMain()
    {
        var state = State.OffHook;
        while (true)
        {
            ColorConsole.Print($"The phone is currently {state}", Color.Ok);
            ColorConsole.Print("Select -1 to exit", Color.Info);
            ColorConsole.Print("Select a trigger:", Color.Debug);
            

            for (var i = 0; i < Rules[state].Count; i++)
            {
                var (t, _) = Rules[state][i];
                ColorConsole.Print($"{i}. {t}");
            }
            
            if (!int.TryParse(Console.ReadLine(), out int inp))
                continue;
            if (inp == -1)
                break;
            if (inp > Rules[state].Count - 1)
                continue;
            
            var (_, s) = Rules[state][inp];
            state = s;
        }
    }
}