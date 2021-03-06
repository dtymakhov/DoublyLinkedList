using System.Collections;
using System.Diagnostics;

namespace DoublyLinkedListCSharp;

[DebuggerDisplay("Count = {Count}")]
public class DoublyLinkedList<T> : IEnumerable<T>
{
    public DoublyLinkedNode<T>? FirstNode { get; private set; }

    public DoublyLinkedNode<T>? LastNode { get; private set; }

    public int Count { get; private set; }

    public DoublyLinkedList()
    {
    }

    public DoublyLinkedList(IEnumerable<T> enumerable)
    {
        var array = enumerable as T[] ?? enumerable.ToArray();

        if (!array.Any()) return;

        FirstNode = new DoublyLinkedNode<T> {Value = array.First()};

        var previousNode = FirstNode;

        if (array.Length > 2)
        {
            for (var i = 1; i < array.Length - 1; i++)
            {
                var node = new DoublyLinkedNode<T> {Value = array[i], Previous = previousNode};
                previousNode.Next = node;
                previousNode = node;
            }
        }

        LastNode = new DoublyLinkedNode<T> {Value = array.Last(), Previous = previousNode};

        previousNode.Next = LastNode;
        Count = array.Length;
    }

    public void AddAfter(DoublyLinkedNode<T> node, T item)
    {
        var newNode = new DoublyLinkedNode<T> {Value = item, Previous = node};

        if (node.Next is null) return;

        newNode.Next = node.Next;
        node.Next.Previous = newNode;
        node.Next = newNode;

        Count++;
    }

    public void AddBefore(DoublyLinkedNode<T> node, T item)
    {
        var newNode = new DoublyLinkedNode<T> {Value = item, Next = node};

        if (node.Previous is null) return;

        newNode.Previous = node.Previous;
        node.Previous.Next = newNode;
        node.Previous = newNode;

        Count++;
    }

    public void AddLast(T item)
    {
        if (LastNode is null)
        {
            AddFirst(item);
            return;
        }

        AddAfter(LastNode, item);
    }

    public void AddFirst(T item)
    {
        if (FirstNode is null)
        {
            var newNode = new DoublyLinkedNode<T> {Value = item};
            FirstNode = newNode;
            LastNode = newNode;
            Count++;

            return;
        }

        AddBefore(FirstNode, item);
    }

    public void Remove(DoublyLinkedNode<T> node)
    {
        if (node.Next is not null) node.Next.Previous = node.Previous;
        if (node.Previous is not null) node.Previous.Next = node.Next;

        node.Invalidate();
    }

    public void RemoveFirst()
    {
        var node = FirstNode;
        FirstNode = FirstNode?.Next;

        if (FirstNode is not null) FirstNode.Previous = null;

        node?.Invalidate();
    }

    public void RemoveLast()
    {
        var node = LastNode;
        LastNode = LastNode?.Previous;

        if (LastNode is not null) LastNode.Next = null;

        node?.Invalidate();
    }

    public void Clear()
    {
        var currentNode = FirstNode;

        while (currentNode is not null)
        {
            var nextNode = currentNode.Next;

            currentNode.Invalidate();
            currentNode = nextNode;
        }

        FirstNode = null;
        LastNode = null;
        Count = 0;
    }

    public DoublyLinkedNode<T>? Find(T value)
    {
        var node = FirstNode;
        var equalityComparer = EqualityComparer<T>.Default;

        while (node != null)
        {
            if (equalityComparer.Equals(node.Value, value)) return node;

            node = node.Next;
        }

        return null;
    }

    public DoublyLinkedNode<T>? FindLast(T value)
    {
        var node = LastNode;
        var equalityComparer = EqualityComparer<T>.Default;

        while (node != null)
        {
            if (equalityComparer.Equals(node.Value, value)) return node;

            node = node.Previous;
        }

        return null;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var currentNode = FirstNode;

        while (currentNode is not null)
        {
            yield return currentNode.Value;
            currentNode = currentNode.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}