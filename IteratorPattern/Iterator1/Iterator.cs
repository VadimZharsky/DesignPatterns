using NUnit.Framework;

namespace Patterns.IteratorPattern.Iterator1;

public class Node<T>
{
    public Node(T value)
    {
        Value = value;
    }

    public Node(T value, Node<T> left, Node<T> right)
    {
        Value = value;
        Left = left;
        Right = right;

        Left.Parent = Right.Parent = this;
    }

    public T Value { get; set; }
    public Node<T>? Left { get; set; }
    public Node<T>? Right { get; set; }
    public Node<T>? Parent { get; set; }
}

public class InOrderIterator<T>
{
    private readonly Node<T> _root;
    private Node<T>? _current;
    private bool _yieldedStart;

    public InOrderIterator(Node<T> root)
    {
        _root = root;
        _current = root;
        while (_current.Left != null)
            _current = _current.Left;
    }

    public T CurrentValue => _current.Value;

    public bool MoveNext()
    {
        if (!_yieldedStart)
        {
            _yieldedStart = true;
            return true;
        }

        if (_current.Right != null)
        {
            _current = _current.Right;
            while (_current.Left != null)
                _current = _current.Left;
            return true;
        }
        
        var p = _current.Parent;
        while (p != null && _current == p.Right)
        {
            _current = p;
            p = p.Parent;
        }

        _current = p;
        return _current != null;
    }

    public void Reset()
    {
        _current = _root;
        _yieldedStart = false;
        while (_current.Left != null)
            _current = _current.Left;
    }
}

[TestFixture]
public class Iterator
{
    [Test]
    public void TestCase1()
    {
        //               8
        //          /         \
        //        2             12
        //      /   \        /     \
        //    1      6       10     14
        //         /  \    /   \   /  \
        //        4    7  9    11 13  15
        //      /  \
        //     3    5
        
        // in order: 1 - 15

        var leftNode1 = new Node<int>(4, new Node<int>(3), new Node<int>(5));
        var leftNode2 = new Node<int>(6, leftNode1, new Node<int>(7));
        var leftNode3 = new Node<int>(2, new Node<int>(1), leftNode2);

        var rightNode1 = new Node<int>(10, new Node<int>(9), new Node<int>(11));
        var rightNode2 = new Node<int>(14, new Node<int>(13), new Node<int>(15));
        var rightNode3 = new Node<int>(12, rightNode1, rightNode2);

        var root = new Node<int>(8, leftNode3, rightNode3);
        
        var it = new InOrderIterator<int>(root);
        while (it.MoveNext())
        {
            Console.WriteLine(it.CurrentValue);
        }
        it.Reset();
        
        while (it.MoveNext())
        {
            Console.WriteLine(it.CurrentValue);
        }
        
    }
}