using Patterns.ColorPrint;
using Patterns.StatePattern.Classic;

namespace Patterns.StatePattern.State4;

public class Chest
{
    public ChestState State { get; private set; } = ChestState.Locked;

    public void Manipulate(Action action, bool haveKey = false)
    {
        switch (action)
        {
            case Action.Open:
                Open(haveKey);
                break;
            case Action.Close:
                Close(haveKey);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(action), action, null);
        }
    }

    private void Close(bool haveKey)
    {
        if (State != ChestState.Open) return;

        State = haveKey ? ChestState.Locked : ChestState.Closed;
    }

    private void Open(bool haveKey)
    {
        State = State == ChestState.Closed 
                || (State == ChestState.Locked && haveKey) ? 
            ChestState.Open : State;
    }

    public enum ChestState
    {
        Open,
        Closed,
        Locked
    }
}

public enum Action
{
    Open,
    Close
}

public class State4
{
    public static void PrintChestState(Chest chest)
    {
        var color = chest.State switch
        {
            Chest.ChestState.Open => Color.Ok,
            Chest.ChestState.Closed => Color.Warning,
            _ => Color.Fail
        };
        ColorConsole.Print($"chest is {chest.State}", color);
    }

    public static void LocalMain()
    {
        var chest = new Chest();
        PrintChestState(chest);
        
        chest.Manipulate(Action.Open);
        PrintChestState(chest);

        chest.Manipulate(Action.Open, true);
        PrintChestState(chest);
        
        chest.Manipulate(Action.Close);
        PrintChestState(chest);
        
        chest.Manipulate(Action.Open);
        PrintChestState(chest);
    }
}