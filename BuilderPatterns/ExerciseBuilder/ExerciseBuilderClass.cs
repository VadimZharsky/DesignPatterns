using System.Text;

namespace Patterns.BuilderPatterns.ExerciseBuilder;


public class CodeBuilder
{
    private readonly string _className;
    private readonly List<PropertyField> _fields;
    
    private const int Indent = 2;

    public CodeBuilder(string className)
    {
        _className = className;
        _fields = new List<PropertyField>();
    }

    public CodeBuilder AddField(string attrName, string attrType)
    {
        _fields.Add(new PropertyField {AttrType = attrType, AttrName = attrName});
        return this;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"public class {_className}");
        sb.AppendLine("{");
        _fields.ForEach(p => 
            sb.AppendLine($"{new string(' ', Indent)} public {p.AttrType} {p.AttrName};"));
        sb.AppendLine("}");
        
        return sb.ToString();
    }
}

public class PropertyField
{
    public string AttrType { get; init; } = string.Empty;
    public string AttrName { get; init; } = string.Empty;
}

public static class ExerciseBuilderClass
{
    public static void LocalMain()
    {
        var cb = new CodeBuilder("Person")
            .AddField("Name", "string")
            .AddField("Age", "int");

        Console.WriteLine(cb);
    }
}