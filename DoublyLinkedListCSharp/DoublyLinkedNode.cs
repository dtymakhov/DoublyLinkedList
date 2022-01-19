namespace DoublyLinkedListCSharp;

public record DoublyLinkedNode<T>
{
    public T Value { get; set; }

    public DoublyLinkedNode<T>? Previous { get; internal set; }

    public DoublyLinkedNode<T>? Next { get; internal set; }

    internal void Invalidate()
    {
        Previous = null;
        Next = null;
    }
}