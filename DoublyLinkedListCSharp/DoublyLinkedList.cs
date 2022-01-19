using System.Collections;

namespace DoublyLinkedListCSharp;

public class DoublyLinkedList<T> : IEnumerable<DoublyLinkedNode<T>>
{
    public DoublyLinkedNode<T>? FirstNode { get; private set; }

    public DoublyLinkedNode<T>? LastNode { get; private set; }

    public DoublyLinkedList()
    {
    }

    public DoublyLinkedList(IEnumerable<T> enumerable)
    {
        var array = enumerable as T[] ?? enumerable.ToArray();

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
    }

    public void AddAfter(DoublyLinkedNode<T> node, T item)
    {
        var newNode = new DoublyLinkedNode<T> {Value = item, Previous = node};

        if (node.Next is null) return;

        newNode.Next = node.Next;
        node.Next.Previous = newNode;
        node.Next = newNode;
    }

    public void AddBefore(DoublyLinkedNode<T> node, T item)
    {
        var newNode = new DoublyLinkedNode<T> {Value = item, Next = node};

        if (node.Previous is null) return;

        newNode.Previous = node.Previous;
        node.Previous.Next = newNode;
        node.Previous = newNode;
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

            return;
        }

        AddBefore(FirstNode, item);
    }

    public IEnumerator<DoublyLinkedNode<T>> GetEnumerator()
    {
        var currentNode = FirstNode;

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