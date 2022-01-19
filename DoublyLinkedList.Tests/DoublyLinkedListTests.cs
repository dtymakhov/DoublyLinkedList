using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using DoublyLinkedListCSharp;

namespace DoublyLinkedList.Tests;

public class DoublyLinkedListTests
{
    [Fact]
    public void CreateDoublyLinkedList_FromAnotherList()
    {
        // Arrange
        List<int> list = new() {1, 2, 3, 4, 5};
        
        // Act
        DoublyLinkedList<int> linkedList = new(list);

        // Assert
        var index = 0;

        foreach (var node in linkedList)
        {
            node.Value.Should().Be(list[index]);
            index++;
        }
    }
}