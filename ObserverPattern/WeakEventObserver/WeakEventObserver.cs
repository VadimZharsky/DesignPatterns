namespace Patterns.ObserverPattern.WeakEventObserver;

public delegate void ButtonHandler(string? message);

public class Button
{
    public event EventHandler? Clicked;

    public event ButtonHandler? Clicked2;

    public void Fire() => Fire(string.Empty);
    
    public void Fire(string message)
    {
        Clicked?.Invoke(this, EventArgs.Empty);
        Clicked2?.Invoke(message);
    }
}

public class Window
{
    public Window(Button button)
    {
        button.Clicked += ButtonOnClicked;
        button.Clicked2 += ButtonOnClicked2;
    }

    private void ButtonOnClicked2(string? message)
    {
        Console.WriteLine($"call ButtonOnClicked2 with message: {message}");
    }

    private void ButtonOnClicked(object? sender, EventArgs e)
    {
        Console.WriteLine("call ButtonOnClicked");
    }
    
    public void Dispose(Button button)
    {
        button.Clicked -= ButtonOnClicked;
        button.Clicked2 -= ButtonOnClicked2;
    }

    ~Window()
    {
        Console.WriteLine("Window Finalized");
    }
}

public class WeakEventObserver
{
    public static void LocalMain()
    {
        var btn = new Button();

        var window = new Window(btn);
        var windowRef = new WeakReference(window);

        btn.Fire();
        
        btn.Fire("pushed");
        window.Dispose(btn);
        window = null;
        ForceGc();
        Console.WriteLine($"window alive after GC? {windowRef.IsAlive}");
        
        btn?.Fire();
    }

    public static void ForceGc()
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
    }
}