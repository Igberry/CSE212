using System.Collections;
using System.Linq;

public class LinkedList : IEnumerable<int>
{
    private Node? _head;
    private Node? _tail;

    /// <summary>
    /// Insert a new node at the front (i.e. the head) of the linked list.
    /// </summary>
    public void InsertHead(int value)
    {
        // Create new node
        Node newNode = new(value);
        // If the list is empty, then point both head and tail to the new node.
        if (_head is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        // If the list is not empty, then only head will be affected.
        else
        {
            newNode.Next = _head; // Connect new node to the previous head
            _head.Prev = newNode; // Connect the previous head to the new node
            _head = newNode; // Update the head to point to the new node
        }
    }

    /// <summary>
    /// Insert a new node at the back (i.e. the tail) of the linked list.
    /// </summary>
    public void InsertTail(int value)
    {
        // Create new node
        Node newNode = new(value);
        // If the list is empty, point both head and tail to the new node
        if (_tail is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        // If the list is not empty, append the new node at the end
        else
        {
            _tail.Next = newNode; // Connect the current tail to the new node
            newNode.Prev = _tail; // Connect the new node back to the current tail
            _tail = newNode; // Update the tail to the new node
        }
    }

    /// <summary>
    /// Remove the first node (i.e. the head) of the linked list.
    /// </summary>
    public void RemoveHead()
    {
        // If the list has only one item in it, then set head and tail 
        // to null resulting in an empty list.  This condition will also
        // cover an empty list.  Its okay to set to null again.
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        // If the list has more than one item in it, then only the head
        // will be affected.
        else if (_head is not null)
        {
            _head.Next!.Prev = null; // Disconnect the second node from the first node
            _head = _head.Next; // Update the head to point to the second node
        }
    }

    /// <summary>
    /// Remove the last node (i.e. the tail) of the linked list.
    /// </summary>
    public void RemoveTail()
    {
        // If the list is empty or has only one node
        if (_tail == _head)
        {
            _head = null;
            _tail = null;
        }
        // If the list has more than one node
        else if (_tail is not null)
        {
            _tail = _tail.Prev; // Move tail back one node
            _tail!.Next = null; // Disconnect the removed node
        }
    }

    /// <summary>
    /// Insert 'newValue' after the first occurrence of 'value' in the linked list.
    /// </summary>
    public void InsertAfter(int value, int newValue)
    {
        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == value)
            {
                if (curr == _tail)
                {
                    InsertTail(newValue);
                }
                else
                {
                    Node newNode = new(newValue);
                    newNode.Prev = curr;
                    newNode.Next = curr.Next;
                    curr.Next!.Prev = newNode;
                    curr.Next = newNode;
                }
                return;
            }
            curr = curr.Next;
        }
    }

    /// <summary>
    /// Remove the first node that contains 'value'.
    /// </summary>
    public void Remove(int value)
    {
        Node? curr = _head;

        while (curr is not null)
        {
            if (curr.Data == value)
            {
                if (curr == _head)
                {
                    RemoveHead();
                }
                else if (curr == _tail)
                {
                    RemoveTail();
                }
                else
                {
                    curr.Prev!.Next = curr.Next;
                    curr.Next!.Prev = curr.Prev;
                }
                return; // Stop after removing the first match
            }
            curr = curr.Next;
        }
    }

    /// <summary>
    /// Search for all instances of 'oldValue' and replace the value to 'newValue'.
    /// </summary>
    public void Replace(int oldValue, int newValue)
    {
        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == oldValue)
            {
                curr.Data = newValue;
            }
            curr = curr.Next;
        }
    }

    /// <summary>
    /// Yields all values in the linked list
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    /// <summary>
    /// Iterate forward through the Linked List
    /// </summary>
    public IEnumerator<int> GetEnumerator()
    {
        Node? curr = _head;
        while (curr is not null)
        {
            yield return curr.Data;
            curr = curr.Next;
        }
    }

    /// <summary>
    /// Iterate backward through the Linked List
    /// </summary>
    public IEnumerable Reverse()
    {
        Node? curr = _tail;
        while (curr is not null)
        {
            yield return curr.Data;
            curr = curr.Prev;
        }
    }

    public override string ToString()
    {
        return "<LinkedList>{" + string.Join(", ", this) + "}";
    }

    // Just for testing.
    public Boolean HeadAndTailAreNull()
    {
        return _head is null && _tail is null;
    }

    // Just for testing.
    public Boolean HeadAndTailAreNotNull()
    {
        return _head is not null && _tail is not null;
    }
}

/// <summary>
/// Extension method so tests can call AsString() on IEnumerable.
/// </summary>
public static class IntArrayExtensionMethods
{
    public static string AsString(this IEnumerable array)
    {
        return "<IEnumerable>{" + string.Join(", ", array.Cast<int>()) + "}";
    }
}
