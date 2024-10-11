using System.Text;
using NUnit.Framework;

namespace Patterns.InterpreterPattern;


public interface IElement
{
    int Value { get; }
}

public class Integer : IElement
{
    public Integer(int value)
    {
        Value = value;
    }

    public int Value { get; }
}

public class BinaryOperation : IElement
{
    public enum Type
    {
        Addition, Subtraction
    }

    public Type ThisType { get; set; }
    public IElement? Left { get; set; }
    public IElement? Right { get; set; }

    public int Value
    {
        get
        {
            if (Left == null || Right == null) return 0;
            
            return ThisType switch
            {
                Type.Addition => Left.Value + Right.Value,
                Type.Subtraction => Left.Value - Right.Value,
                _ => 0
            };
        }
    }
}

public class Token
{
    public Token(Type thisType, string text)
    {
        ThisType = thisType;
        Text = text;
    }
    
    public enum Type
    {
        Integer, Plus, Minus, Lparen, Rparen
    }

    public Type ThisType { get; }
    public string Text { get; }

    public override string ToString() => $"`{Text}`";
}

public static class Lexer
{
    public static List<Token> Generate(string input)
    {
        List<Token> result = [];
        
        for (int i = 0; i < input.Length; i++)
        {
            if (GetToken(input[i], out Token? token) && token != null)
                result.Add(token);
            else
            {
                var sb = new StringBuilder(input[i].ToString());
                for (int j = i + 1; j < input.Length; j++)
                {
                    if (char.IsDigit(input[j]))
                    {
                        sb.Append(input[j]);
                        i++;
                    }
                    else
                        break;
                }
                result.Add(new Token(Token.Type.Integer, sb.ToString()));
            }
        }

        return result;
    }

    public static IElement Parse(IReadOnlyList<Token> tokens)
    {
        var result = new BinaryOperation();
        bool haveLhs = false;

        for (int i = 0; i < tokens.Count; i++)
        {
            var token = tokens[i];

            switch (token.ThisType)
            {
                case Token.Type.Integer:
                    var intElement = new Integer(int.Parse(token.Text));
                    if (!haveLhs)
                    {
                        result.Left = intElement;
                        haveLhs = true;
                    }
                    else
                        result.Right = intElement;
                    break;
                case Token.Type.Plus:
                    result.ThisType = BinaryOperation.Type.Addition;
                    break;
                case Token.Type.Minus:
                    result.ThisType = BinaryOperation.Type.Subtraction;
                    break;
                case Token.Type.Lparen:
                    var j = i;
                    for (; j < tokens.Count; ++j)
                        if (tokens[j].ThisType == Token.Type.Rparen)
                            break;
                    var subExpression = tokens.Skip(i + 1).Take(j - i - 1).ToList();
                    var element = Parse(subExpression);
                    if (!haveLhs)
                    {
                        result.Left = element;
                        haveLhs = true;
                    }
                    else
                        result.Right = element;

                    i = j;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return result;
    }

    private static bool GetToken(char inp, out Token? token)
    {
        token = inp switch
        {
            '+' => new Token(Token.Type.Plus, "+"),
            '-' => new Token(Token.Type.Minus, "-"),
            '(' => new Token(Token.Type.Lparen, "("),
            ')' => new Token(Token.Type.Rparen, ")"),
            _ => null
        };

        return token != null;
    }
}

[TestFixture]
public class Interpreter1
{
    [TestCase("(13+4)-(12-1)", 6)]
    [TestCase("13+4", 17)]
    public void TestCase1(string input, int result)
    {
        var tokens = Lexer.Generate(input);
        Console.WriteLine(string.Join("  ", tokens.Select(x => x.Text)));
        var elements = Lexer.Parse(tokens);
        Assert.That(elements.Value, Is.EqualTo(result));
    }
}