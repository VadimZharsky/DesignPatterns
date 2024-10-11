namespace Patterns.ColorPrint;

public enum Color
{
    Ok, Info, Debug, Warning, Fail, Default
}

public static class ColorConsole
{
    public static void Print(string text, Color color = Color.Default)
    {
        if (color == Color.Default)
        {
            Console.WriteLine(text);
            return;
        }

        Console.ForegroundColor = GetColor(color);
        Console.WriteLine(text);
        Console.ForegroundColor = GetColor(Color.Default);
    }

    private static ConsoleColor GetColor(Color color) =>
        color switch
        {
            Color.Ok => ConsoleColor.Green,
            Color.Info => ConsoleColor.Blue,
            Color.Debug => ConsoleColor.Cyan,
            Color.Warning => ConsoleColor.Yellow,
            Color.Fail => ConsoleColor.Red,
            _ => ConsoleColor.White
        };
}