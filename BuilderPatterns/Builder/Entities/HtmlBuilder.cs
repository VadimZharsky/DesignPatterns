namespace Patterns.BuilderPatterns.Builder.Entities;

public class HtmlBuilder
{
    private readonly string _rootName;
    private HtmlElement _root;

    public HtmlBuilder(string rootName)
    {
        _rootName = rootName;
        _root = new HtmlElement(name: _rootName);
    }

    public HtmlBuilder AddChild(HtmlElement element)
    {
        _root.AddChild(element);
        return this;
    }
    

    public override string ToString() => _root.ToString();

    public void Clear() => _root = new HtmlElement(name: _rootName);
}