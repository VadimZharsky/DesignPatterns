using System.Security.Cryptography;
using System.Text;

namespace Patterns.VisitorPattern.Visitor2;

public interface IExpressionVisitor
{
    void Visit(DoubleExpression de);
    void Visit(AdditionExpression ae);
}

public abstract class Expression
{
    public abstract void Accept(IExpressionVisitor visitor);
}

public class DoubleExpression : Expression
{
    public DoubleExpression(double value)
    {
        Value = value;
    }
    
    public double Value { get; }

    public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
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

    public override void Accept(IExpressionVisitor visitor) => visitor.Visit(this);
}

public class ExpressionPrinter : IExpressionVisitor
{
    private readonly StringBuilder _sb = new();
    
    public void Clear() => _sb.Clear();
    
    public void Visit(DoubleExpression de) => _sb.Append(de.Value);
    

    public void Visit(AdditionExpression ae)
    {
        _sb.Append('(');
        ae.Left.Accept(this);
        _sb.Append('+');
        ae.Right.Accept(this);
        _sb.Append(')');
    }
    
    public override string ToString()
    {
        return _sb.ToString();
    }
}

public class ExpressionCalculator : IExpressionVisitor
{
    public double Result { get; private set; }
    
    public void Visit(DoubleExpression de) => Result = de.Value;

    public void Visit(AdditionExpression ae)
    {
        ae.Left.Accept(this);
        var a = Result;
        ae.Right.Accept(this);
        var b = Result;
        Result = a + b;
    }
}

public static class Visitor2
{
    // ClassicVisitor
    
    public static void LocalMain()
    {
        var e = new AdditionExpression(new DoubleExpression(12d), 
            new AdditionExpression(new DoubleExpression(10d), 
                new DoubleExpression(2d)));

        var visitor = new ExpressionPrinter();
        var calculator = new ExpressionCalculator();
        
        visitor.Visit(e);
        calculator.Visit(e);

        Console.WriteLine(visitor);
        Console.WriteLine(calculator.Result);
    }
}