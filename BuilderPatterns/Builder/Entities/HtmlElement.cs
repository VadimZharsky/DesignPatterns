using System.Runtime.CompilerServices;
using System.Text;

namespace Patterns.BuilderPatterns.Builder.Entities;

public class HtmlElement
{
    public string Name;
    public string? Text;
    private readonly List<HtmlElement> _elemens = [];
    private const int IndentSize = 2;

    public HtmlElement(string name) => Name = name;

    public HtmlElement(string name, string text)
    {
        Name = name;
        Text = text;
    }

    public HtmlElement AddChild(HtmlElement element)
    {
        _elemens.Add(element);
        return this;
    }

    public override string ToString() => ToStringImpl(0);

    private string ToStringImpl(int indent)
    {
        var sb = new StringBuilder();
        var i = new string(' ', IndentSize * indent);
        sb.AppendLine($"{i}<{Name}>");

        if (!string.IsNullOrEmpty(Text))
        {
            sb.Append(new string(' ', IndentSize * (indent + 1)));
            sb.AppendLine(Text);
        }

        foreach (var element in _elemens)
        {
            sb.Append(element.ToStringImpl(indent + 1));
        }

        sb.AppendLine($"{i}</{Name}>");

        return sb.ToString();
    }
    
}