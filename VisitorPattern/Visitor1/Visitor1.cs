using System.Text;

namespace Patterns.VisitorPattern.Visitor1;

public abstract class Expression
{
}

public class DoubleExpression : Expression
{
    public DoubleExpression(double value)
    {
        Value = value;
    }

    public double Value { get; }
}

public class AdditionExpression : Expression
{
    public AdditionExpression(Expression left, Expression right)
    {
        Left = left;
        Right = right;
    }

    public Expression Left { get; }
    public Expression Right { get; }
}

public static class ExpressionPrinter
{
    public static void Print(Expression e, StringBuilder sb)
    {
        switch (e)
        {
            case DoubleExpression de:
                sb.Append(de.Value);
                break;
            case AdditionExpression ae:
                sb.Append('(');
                Print(ae.Left, sb);
                sb.Append('+');
                Print(ae.Right, sb);
                sb.Append(')');
                break;
        }
    }
}

public static class Visitor1
{
    //Reflective visitor
    
    public static void LocalMain()
    {
        var e = new AdditionExpression(new DoubleExpression(23.45d), 
            new AdditionExpression(new DoubleExpression(12.2d), 
                new DoubleExpression(2.314d)));

        var sb = new StringBuilder();
        
        ExpressionPrinter.Print(e, sb);
        
        Console.WriteLine(sb.ToString());
    }
}