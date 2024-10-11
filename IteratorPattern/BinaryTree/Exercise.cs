using NUnit.Framework;

namespace Patterns.IteratorPattern.BinaryTree;

public class Node 
{
    public Node(int value)
    {
        Value = value;
        Left = Right = null;
    }

    public Node(int value, Node left, Node right)
    {
        Value = value;
        Left = left;
        Right = right;
        Left.Parent = Right.Parent = this;
    }

    public int Value { get; private set; }
    public Node? Left { get; set; }
    public Node? Right { get; set; }
    public Node? Parent { get; private set; }
}

public abstract class BinaryTree
{
    protected readonly Node Root;
    protected Node? Current;
    protected bool IsReset;
    
    protected BinaryTree(Node root)
    {
        Root = root;
    }
    
    public int Value => Current?.Value ?? 0;

    public abstract bool Next();
}


public class InOrderTree : BinaryTree
{

    public InOrderTree(Node root) : base(root)
    {
    }

    public override bool Next()
    {
        if (!IsReset)
        {
            ResetTree();
            IsReset = true;
            return true;
        }

        Iterate();
        
        return Current != null;
    }
    
    private void Iterate()
    {
        if (Current?.Right != null)
        {
            Current = Current.Right;
            MoveFarLeft();
            return;
        }
        
        var itsParent = Current?.Parent;
        while (itsParent != null && Current == itsParent.Right)
        {
            Current = itsParent;
            itsParent = itsParent.Parent;
        }
        
        Current = itsParent;
    }
    
    private void ResetTree()
    {
        Current = Root;
        MoveFarLeft();
    }
    
    private void MoveFarLeft()
    {
        while (Current?.Left != null)
            Current = Current.Left;
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

        var leftNode1 = new Node(4, new Node(3), new Node(5));
        var leftNode2 = new Node(6, leftNode1, new Node(7));
        var leftNode3 = new Node(2, new Node(1), leftNode2);

        var rightNode1 = new Node(10, new Node(9), new Node(11));
        var rightNode2 = new Node(14, new Node(13), new Node(15));
        var rightNode3 = new Node(12, rightNode1, rightNode2);

        var root = new Node(8, leftNode3, rightNode3);
        
        BinaryTree it = new InOrderTree(root);

        while (it.Next())
        {
            Console.WriteLine(it.Value);
        }
    }
}