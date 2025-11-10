using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add three elements with priorities 2, 1, and 3, then remove them.
    // Expected Result: Items should be dequeued in order of highest priority first (3 → 2 → 1).
    // Defect(s) Found: Dequeue was returning items in insertion order instead of priority order; fixed comparison logic in Enqueue.
    public void TestPriorityQueue_Order()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("Low", 1);
        pq.Enqueue("Medium", 2);
        pq.Enqueue("High", 3);

        Assert.AreEqual("High", pq.Dequeue());
        Assert.AreEqual("Medium", pq.Dequeue());
        Assert.AreEqual("Low", pq.Dequeue());
    }

    [TestMethod]
    // Scenario: Add two items with the same priority.
    // Expected Result: They should be dequeued in the order they were inserted (FIFO for same priority).
    // Defect(s) Found: Equal priorities were not maintaining insertion order; fixed by using stable ordering in Enqueue.
    public void TestPriorityQueue_SamePriorityOrder()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("First", 5);
        pq.Enqueue("Second", 5);

        Assert.AreEqual("First", pq.Dequeue());
        Assert.AreEqual("Second", pq.Dequeue());
    }

    [TestMethod]
    // Scenario: Try to dequeue from an empty queue.
    // Expected Result: Should throw InvalidOperationException with an appropriate message.
    // Defect(s) Found: Dequeue did not handle empty queue; added validation to throw an exception.
    public void TestPriorityQueue_EmptyQueue()
    {
        var pq = new PriorityQueue();
        Assert.ThrowsException<InvalidOperationException>(() => pq.Dequeue());
    }
}
