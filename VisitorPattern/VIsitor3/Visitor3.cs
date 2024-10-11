using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Patterns.VisitorPattern.Visitor3;

public interface ITransformer<out T>
{
    T Transform(DoubleExpression de);
    T Transform(AdditionExpression ae);
}

public abstract class Expression
{
    public abstract T Reduce<T>(ITransformer<T> transformer);
}

public class DoubleExpression : Expression
{
    public DoubleExpression(double value)
    {
        Value = value;
    }

    public double Value { get; }

    public override T Reduce<T>(ITransformer<T> transformer) 
        => transformer.Transform(this);
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
    
    public override T Reduce<T>(ITransformer<T> transformer)
        => transformer.Transform(this);
}

public class EvaluationTransformer : ITransformer<double>
{
    public double Transform(DoubleExpression de) => de.Value;

    public double Transform(AdditionExpression ae)
        => ae.Left.Reduce(this) + ae.Right.Reduce(this);
}

public class PrintTransformer : ITransformer<string>
{
    public string Transform(DoubleExpression de) 
        => de.Value.ToString(CultureInfo.InvariantCulture);

    public string Transform(AdditionExpression ae)
        => $"({ae.Left.Reduce(this)} + {ae.Right.Reduce(this)})";
}

public class SquareTransformer : ITransformer<Expression>
{
    public Expression Transform(DoubleExpression de)
        => new DoubleExpression(de.Value * de.Value);

    public Expression Transform(AdditionExpression ae)
    {
        return new AdditionExpression(
            ae.Left.Reduce(this), 
            ae.Right.Reduce(this)
        );
    }
}

public static class Visitor3
{
    // reducing and transforms
    
    public static void LocalMain()
    {
        var e = new AdditionExpression(new DoubleExpression(12d), 
            new AdditionExpression(new DoubleExpression(10d), 
                new DoubleExpression(2d)));

        var square = new SquareTransformer();

        var updated = e.Reduce(square);

        var evaluateTransformer = new EvaluationTransformer();
        var printTransformer = new PrintTransformer();
        
        Console.WriteLine($"{updated.Reduce(printTransformer)} = {updated.Reduce(evaluateTransformer)}");
    }
}