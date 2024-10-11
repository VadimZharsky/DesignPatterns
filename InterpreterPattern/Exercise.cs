using System.Text;
using NUnit.Framework;
// ReSharper disable MergeIntoLogicalPattern
// ReSharper disable MergeConditionalExpression

namespace Patterns.InterpreterPattern;

public class ExpressionProcessor
{
    public Dictionary<char, int> Variables = new ();
    private Element? _element;

    public class Element
    {
        private Element? _next;
        
        public Element(int value, ThisType type)
        {
            Value = value;
            ActionToNext = type;
        }

        public enum ThisType
        {
            Addition,
            Subtraction,
            Default
        }

        public int Value { get; }

        public int Result() => Calculate(Value);
        public ThisType ActionToNext { get; }

        public void AddNext(Element element)
        {
            if (_next != null)
                _next.AddNext(element);
            else
                _next = element;
        }

        public void Print()
        {
            Console.WriteLine($"value: {Value}, type: {ActionToNext.ToString()}");
            _next?.Print();
        }

        private int Calculate(int value)
        {
            if (_next == null) return value;
            
            switch (ActionToNext)
            {
                case ThisType.Addition:
                    return _next.Calculate(value + _next.Value);
                case ThisType.Subtraction:
                    return _next.Calculate(value - _next.Value);
                default:
                    return value;
            }
        }
    }
    
    private bool IsValid { get; set; } = true;

    public int Calculate(string expression)
    {
        var sb = new StringBuilder();
        foreach (var ch in expression)
        {
            if (ch == '+' || ch == '-')
            {
                var type = ch == '+' ? Element.ThisType.Addition : Element.ThisType.Subtraction;
                ApplyElement(sb.ToString(), type);
                sb.Clear();
            }
            else
            {
                if (char.IsDigit(ch))
                {
                    sb.Append(ch);
                }
                else
                {
                    if (Variables.TryGetValue(ch, out int value))
                    {
                        sb.Append(value);
                    }

                    else
                        IsValid = false;
                }
            }
        }
        if (sb.Length > 0)
            ApplyElement(sb.ToString(), Element.ThisType.Default);

        return _element != null && IsValid ? _element.Result() : 0;
    }

    public void PrintElements() => _element?.Print();

    private void ApplyElement(string value, Element.ThisType operand)
    {
        if (_element == null)
            _element = new Element(int.Parse(value), operand);
        else
            _element.AddNext(new Element(int.Parse(value), operand));
    }
}

[TestFixture]
public class ExerciseTest
{
    [TestCase("1+2", 3)]
    [TestCase("1+2+3", 6)]
    [TestCase("1+2+xy", 0)]
    public void TestCase1(string input, int result)
    {
        var calc = new ExpressionProcessor();
        var res = calc.Calculate(input);
        calc.PrintElements();
        Assert.That(res, Is.EqualTo(result));
    }
    
    [TestCase("10-2-x", 'x', 3, 5)]
    public void TestCase2(string input, char letter, int integer, int result)
    {
        var calc = new ExpressionProcessor
        {
            Variables =
            {
                [letter] = integer
            }
        };
        var res = calc.Calculate(input);
        calc.PrintElements();
        Assert.That(res, Is.EqualTo(result));
    }

}