using Patterns.BuilderPatterns.Builder.Entities;

namespace Patterns.BuilderPatterns.Builder;

public static class BuilderClass
{
    public static void LocalMain()
    {
        var builder = new HtmlBuilder("ul");
        var li1 = new HtmlElement("li", "product1");
        var li2 = new HtmlElement("li", "product2");
        li1.AddChild(new HtmlElement("p", "description")).AddChild(new HtmlElement("p", "price"));
        li2.AddChild(new HtmlElement("p", "description")).AddChild(new HtmlElement("p", "price"));
        builder.AddChild(li1).AddChild(li2);
        Console.WriteLine(builder.ToString());
    }
}