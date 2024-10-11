namespace Patterns.TemplateMethodPattern.Template1;

public abstract class Game
{
    protected int CurrentPlayer;
    protected readonly int NumberOfPlayers;

    protected Game(int numberOfPlayers)
    {
        NumberOfPlayers = numberOfPlayers;
    }

    protected abstract bool HaveWinner { get; }
    protected abstract int WinningPlayer { get; }

    public void Run()
    {
        Start();

        while (!HaveWinner)
        {
            TakeTurn();
        }

        Console.WriteLine($"player {WinningPlayer} wins.");
    }

    protected abstract void Start();

    protected abstract void TakeTurn();
}

public class Chess : Game
{
    private const int MaxTurns = 10;
    private int _turn = 1;
    
    public Chess() : base(numberOfPlayers: 2) { }

    protected override bool HaveWinner => _turn == MaxTurns;
    protected override int WinningPlayer => CurrentPlayer;
    protected override void Start() => 
        Console.WriteLine($"starting a game of chess with {NumberOfPlayers} player(s)");

    protected override void TakeTurn()
    {
        Console.WriteLine($"turn {_turn++} taken by player {CurrentPlayer}");
        CurrentPlayer = (CurrentPlayer + 1) % NumberOfPlayers;
    }
}

public class Template1
{
    public static void LocalMain()
    {
        Game game = new Chess();
        game.Run();
    }
}