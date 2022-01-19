using System.Collections;

namespace DoublyLinkedListCSharp;

public class DoublyLinkedList<T> : IEnumerable<DoublyLinkedNode<T>>
{
    private DoublyLinkedNode<T>? _firstNode;

    private DoublyLinkedNode<T>? _lastNode;

    public DoublyLinkedList()
    {
    }

    public DoublyLinkedList(IEnumerable<T> enumerable)
    {
        var array = enumerable as T[] ?? enumerable.ToArray();

        _firstNode = new DoublyLinkedNode<T> {Value = array.First()};

        var previousNode = _firstNode;

        for (var i = 1; i < array.Length - 2; i++)
        {
            var node = new DoublyLinkedNode<T> {Value = array[i], Previous = previousNode};
            previousNode.Next = node;
            previousNode = node;
        }

        _lastNode = new DoublyLinkedNode<T> {Value = array.Last(), Previous = previousNode};
        previousNode.Next = _lastNode;
    }

    public void InsertAfter(DoublyLinkedNode<T> node, T item)
    {
        var newNode = new DoublyLinkedNode<T> {Value = item, Previous = node};

        if (node.Next is null) return;

        newNode.Next = node.Next;
        node.Next.Previous = newNode;
        node.Next = newNode;
    }

    public void InsertBefore(DoublyLinkedNode<T> node, T item)
    {
        var newNode = new DoublyLinkedNode<T> {Value = item, Next = node};

        if (node.Previous is null) return;

        newNode.Previous = node.Previous;
        node.Previous.Next = newNode;
        node.Previous = newNode;
    }

    public void Insert(T item)
    {
        if (_lastNode is null)
        {
            InsertStart(item);
            return;
        }

        InsertAfter(_lastNode, item);
    }

    public void InsertStart(T item)
    {
        if (_firstNode is null)
        {
            var newNode = new DoublyLinkedNode<T> {Value = item};
            _firstNode = newNode;
            _lastNode = newNode;

            return;
        }

        InsertBefore(_firstNode, item);
    }

    public IEnumerator<DoublyLinkedNode<T>> GetEnumerator()
    {
        var currentNode = _firstNode;

        while (currentNode is not null)
        {
            yield return currentNode;
            currentNode = currentNode.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}