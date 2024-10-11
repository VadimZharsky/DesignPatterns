using System.Text;
using NUnit.Framework;

namespace Patterns.FlyweightPattern.FlyweightExercise;

public class Sentence
{
    private readonly string[] _words;
    private readonly Dictionary<int, WordToken> _tokens = [];
    
    public Sentence(string plainText)
    {
        _words = plainText.Split(' ');
    }
    
    public WordToken this[int index]
    {
        get
        {
            if (_tokens.ContainsKey(index))
            {
                return _tokens[index];
            }

            _tokens.Add(index, new WordToken());
            return _tokens[index];
        }
    }
    
    public override string ToString()
    {
        var sb = new string[_words.Length];
        for (var i = 0; i < _words.Length; i++)
        {
            if (_tokens.ContainsKey(i) && _tokens[i].Capitalize)
                sb[i] = (ProceedWord(_words[i], true));
            else
                sb[i] = (ProceedWord(_words[i], false));
        }

        return string.Join(' ', sb);
    }

    public class WordToken
    {
        public bool Capitalize { get; set; }
    }

    private string ProceedWord(string word, bool isUpper) => isUpper ? word.ToUpperInvariant() : word;
}

[TestFixture]
public class Tester
{
    [Test]
    public void Test1()
    {
        var s = "hello big unconscious world";
        var sentence = new Sentence(s);
        sentence[0].Capitalize = true;
        sentence[1].Capitalize = true;
        sentence[0].Capitalize = false;
        Console.WriteLine(sentence);
    }

    [TestCase("hello big unconscious world", "hello BIG unconscious world", 1)]
    [TestCase("hello big unconscious world", "hello big UNCONSCIOUS world", 2)]
    [TestCase("hello big unconscious world", "hello big unconscious world", 15)]
    public void Test2(string testString, string result, int indexOfCapitalizing)
    {
        var sentence = new Sentence(testString) { [indexOfCapitalizing] = { Capitalize = true } };
        Assert.That(sentence.ToString(), Is.EqualTo(result));
    }
}