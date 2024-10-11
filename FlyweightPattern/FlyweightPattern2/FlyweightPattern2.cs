using System.Text;

namespace Patterns.FlyweightPattern.FlyweightPattern2;

public class FormattedText
{
    private readonly string _plainText;
    private readonly bool[] _capitalize;

    public FormattedText(string plainText)
    {
        _plainText = plainText;
        _capitalize = new bool[plainText.Length];
    }

    public void Capitalize(int start, int end)
    {
        for (int i = start; i <= end; i++)
        {
            _capitalize[i] = true;
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        for (int i = 0; i < _plainText.Length; i++)
        {
            var c = _plainText[i];
            sb.Append(_capitalize[i] ? char.ToUpper(c) : c);
        }

        return sb.ToString();
    }
}

public class BetterFormattingText
{
    public class TextRange
    {
        public int Start, End;
        public bool Capitalize;

        public bool Covers(int position) => position >= Start && position <= End;
    }
    
    private readonly string _plainText;
    private List<TextRange> _formatting = [];

    public BetterFormattingText(string plainText)
    {
        _plainText = plainText;
    }

    public TextRange GetRange(int start, int end)
    {
        var range = new TextRange { Start = start, End = end };
        _formatting.Add(range);
        return range;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        for (int i = 0; i < _plainText.Length; i++)
        {
            char ch = _plainText[i];
            foreach (var texdtRange in _formatting.Where(textRange => textRange.Covers(i) && textRange.Capitalize))
                ch = char.ToUpper(_plainText[i]);
            
            sb.Append(ch);
        }

        return sb.ToString();
    }
}

public static class FlyweightPattern2
{
    public static void LocalMain()
    {
        var s = "qwerty quite quote";
        var form = new FormattedText(s);
        form.Capitalize(7, 11);
        Console.WriteLine(form);

        var betterForm = new BetterFormattingText(s);
        betterForm.GetRange(7, 11).Capitalize = true;
        Console.WriteLine(betterForm);
    }
}