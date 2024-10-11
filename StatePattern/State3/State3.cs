using System.Text;
using Patterns.ColorPrint;

namespace Patterns.StatePattern.State3;

public enum State
{
    Locked,
    Failed,
    Unlocked
}

public class State3
{
    public static void LocalMain()
    {
        var code = "1234";
        var exit = "-1";
        var state = State.Locked;
        var entry = new StringBuilder();

        while (true)
        {
            switch (state)
            {
                case State.Locked:
                    entry.Append(Console.ReadKey().KeyChar);
                    
                    if (entry.ToString() == code)
                    {
                        state = State.Unlocked;
                        break;
                    }

                    if (!code.StartsWith(entry.ToString()))
                        state = State.Failed;

                    break;
                case State.Failed:
                    Console.CursorLeft = 0;
                    ColorConsole.Print("FAILED", Color.Fail);
                    entry.Clear();
                    state = State.Locked;
                    break;
                case State.Unlocked:
                    Console.CursorLeft = 0;
                    ColorConsole.Print("UNLOCKED", Color.Ok);
                    return;
            }
        }
    }
}