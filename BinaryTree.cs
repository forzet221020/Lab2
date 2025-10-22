using System;
using System.Collections;
using System.Collections.Generic;

public class BinaryTree<T> : IEnumerable<T> where T : class
{
    private class Node
    {
        public T Value { get; set; }
        public Node? Left { get; set; }
        public Node? Right { get; set; }

        public Node(T value)
        {
            this.Value = value;
        }
    }

    private Node? root;
    private readonly IComparer<T>? comparer;

    public BinaryTree(IComparer<T>? comparer = null)
    {
        this.comparer = comparer;
    }

    public void Add(T value)
    {
        if (root == null)
        {
            root = new Node(value);
        }
        else
        {
            AddRecursive(root, value);
        }
    }

    private void AddRecursive(Node currentNode, T value)
    {
        int cmp = Compare(value, currentNode.Value);
        if (cmp < 0)
        {
            if (currentNode.Left == null)
            {
                currentNode.Left = new Node(value);
            }
            else
            {
                AddRecursive(currentNode.Left, value);
            }
        }
        else
        {
            if (currentNode.Right == null)
            {
                currentNode.Right = new Node(value);
            }
            else
            {
                AddRecursive(currentNode.Right, value);
            }
        }
    }

    private int Compare(T x, T y)
    {
        if (comparer != null)
            return comparer.Compare(x, y);

        if (x is IComparable<T> ic)
            return ic.CompareTo(y);

        throw new InvalidOperationException($"No comparer provided and type {typeof(T).FullName} does not implement IComparable<{typeof(T).Name}>");
    }

    public bool Contains(T value)
    {
        return ContainsRecursive(root, value);
    }

    private bool ContainsRecursive(Node? node, T value)
    {
        if (node == null) return false;

        int cmp = Compare(value, node.Value);
        if (cmp == 0) return true;
        if (cmp < 0) return ContainsRecursive(node.Left, value);
        return ContainsRecursive(node.Right, value);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return PreOrderTraversal(root).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private IEnumerable<T> PreOrderTraversal(Node? node)
    {
        if (node is not null)
        {
            yield return node.Value;

            foreach (T value in PreOrderTraversal(node.Left))
            {
                yield return value;
            }

            foreach (T value in PreOrderTraversal(node.Right))
            {
                yield return value;
            }
        }
    }

    // Дополнительные обходы
    public IEnumerable<T> InOrder() => InOrderTraversal(root);
    public IEnumerable<T> PostOrder() => PostOrderTraversal(root);
    public IEnumerable<T> LevelOrder() => LevelOrderTraversal(root);

    private IEnumerable<T> InOrderTraversal(Node? node)
    {
        if (node is not null)
        {
            foreach (var v in InOrderTraversal(node.Left)) yield return v;
            yield return node.Value;
            foreach (var v in InOrderTraversal(node.Right)) yield return v;
        }
    }

    private IEnumerable<T> PostOrderTraversal(Node? node)
    {
        if (node is not null)
        {
            foreach (var v in PostOrderTraversal(node.Left)) yield return v;
            foreach (var v in PostOrderTraversal(node.Right)) yield return v;
            yield return node.Value;
        }
    }

    private IEnumerable<T> LevelOrderTraversal(Node? start)
    {
        if (start is null) yield break;
        var q = new Queue<Node>();
        q.Enqueue(start);
        while (q.Count > 0)
        {
            var n = q.Dequeue();
            yield return n.Value;
            if (n.Left != null) q.Enqueue(n.Left);
            if (n.Right != null) q.Enqueue(n.Right);
        }
    }

    public int Count => GetCount(root);

    private int GetCount(Node? node)
    {
        if (node is null) return 0;
        return 1 + GetCount(node.Left) + GetCount(node.Right);
    }
}