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

        foreach (var item in linkedList)
        {
            item.Should().Be(list[index]);
            index++;
        }
    }

    [Fact]
    public void CreateDoublyLinkedList_FromEmptyList()
    {
        // Act
        DoublyLinkedList<int> linkedList = new(new List<int>());

        // Assert
        linkedList.Count.Should().Be(0);
        linkedList.FirstNode.Should().BeNull();
        linkedList.LastNode.Should().BeNull();
    }

    [Fact]
    public void CreateDoublyLinkedList_FromList_WithOneValue()
    {
        // Arrange
        var value = 1;
        List<int> list = new() {value};

        // Act
        DoublyLinkedList<int> linkedList = new(list);

        linkedList.FirstNode?.Value.Should().Be(value);
        linkedList.LastNode?.Value.Should().Be(value);
    }

    [Fact]
    public void CreateDoublyLinkedList_FromList_WithTwoValue()
    {
        // Arrange
        var firstValue = 1;
        var secondValue = 2;
        List<int> list = new() {firstValue, secondValue};

        // Act
        DoublyLinkedList<int> linkedList = new(list);

        // Assert
        linkedList.FirstNode?.Value.Should().Be(firstValue);
        linkedList.LastNode?.Value.Should().Be(secondValue);
    }
}