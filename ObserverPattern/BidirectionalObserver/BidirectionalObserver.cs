using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Patterns.ObserverPattern.BidirectionalObserver;

public class Product : INotifyPropertyChanged
{
    private string? _name;
    public event PropertyChangedEventHandler? PropertyChanged;

    public string? Name
    {
        get => _name;
        set
        {
            if (value == _name) return;
            _name = value;
            OnPropertyChanged();
        }
    }

    private void OnPropertyChanged(
        [CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, 
            new PropertyChangedEventArgs(propertyName)
        );
    }
}

public class Window : INotifyPropertyChanged
{
    private string? _productName;
    public event PropertyChangedEventHandler? PropertyChanged;

    public string? ProductName
    {
        get => _productName;
        set
        {
            if (value == _productName) return;
            _productName = value;
            OnPropertyChanged();
        }
    }

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, 
            new PropertyChangedEventArgs(propertyName)
        );
    }
}

public class BidirectionalBinding : IDisposable
{
    
    public void Dispose()
    {
        Disposed = true;
    }

    public BidirectionalBinding(
        INotifyPropertyChanged first, 
        Expression<Func<object>> firstProperty,
        INotifyPropertyChanged second,
        Expression<Func<object>> secondProperty
    )
    {
        if (firstProperty.Body is not MemberExpression firstExpression
            || secondProperty.Body is not MemberExpression secondExpression) return;

        if (firstExpression.Member is not PropertyInfo firstProp
            || secondExpression.Member is not PropertyInfo secondProp) return;
        
        first.PropertyChanged += (_, _) =>
        {
            if (!Disposed)
                secondProp.SetValue(second, firstProp.GetValue(first));
        };
            
        second.PropertyChanged += (_, _) =>
        {
            if (!Disposed)
                firstProp.SetValue(first, secondProp.GetValue(second));
        };
    }

    public bool Disposed { get; private set; }
}

public static class BidirectionalObserver
{
    public static void LocalMain()
    {
        var product = new Product { Name = "Book" };
        var window = new Window { ProductName = "Book" };
        //
        // product.PropertyChanged += (_, args) =>
        // {
        //     if (args.PropertyName == "Name")
        //         window.ProductName = product.Name;
        // };
        //
        // window.PropertyChanged += (_, args) =>
        // {
        //     if (args.PropertyName == "ProductName")
        //         product.Name = window.ProductName;
        // };
        using (var binding = new BidirectionalBinding(
                first: product,
                firstProperty: () => product.Name,
                second: window,
                secondProperty: () => window.ProductName
            ))
        {
            
            product.Name = "Note";

            window.ProductName = "Bag";
        }
    }
}