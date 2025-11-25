namespace TestProject2;

public class ArrayQueueTests
{
    [Fact]
    public void Test_Constructor()
    {
        var queue = new ArrayQueue(5);
        Assert.True(queue.IsEmpty());
        Assert.Equal(0, queue.Size());
    }

    [Fact]
    public void Test_Push()
    {
        var queue = new ArrayQueue(3);
        queue.Push(10);
        queue.Push(20);
        Assert.Equal(2, queue.Size());
        Assert.Equal(10, queue.Peek());
    }

    [Fact]
    public void Test_Pop()
    {
        var queue = new ArrayQueue(3);
        queue.Push(10);
        queue.Push(20);
        var first = queue.Pop();
        Assert.Equal(10, first);
        Assert.Equal(1, queue.Size());
        Assert.Equal(20, queue.Peek());
    }

    [Fact]
    public void Test_Peek()
    {
        var queue = new ArrayQueue(3);
        queue.Push(10);
        Assert.Equal(10, queue.Peek());
    }

    [Fact]
    public void Test_ToArray()
    {
        var queue = new ArrayQueue(3);
        queue.Push(10);
        queue.Push(20);
        var array = queue.ToArray();
        Assert.Equal(new[] { 10, 20 }, array);
    }

    [Fact]
    public void Test_IsEmpty()
    {
        var queue = new ArrayQueue(3);
        Assert.True(queue.IsEmpty());
        queue.Push(10);
        Assert.False(queue.IsEmpty());
    }

    [Fact]
    public void Test_Size()
    {
        var queue = new ArrayQueue(3);
        Assert.Equal(0, queue.Size());
        queue.Push(10);
        Assert.Equal(1, queue.Size());
    }

    [Fact]
    public void Test_Overflow()
    {
        var queue = new ArrayQueue(2);
        queue.Push(10);
        queue.Push(20);
        queue.Push(30); // Should not add
        Assert.Equal(2, queue.Size());
        Assert.Equal(new[] { 10, 20 }, queue.ToArray());
    }

    [Fact]
    public void Test_Underflow()
    {
        var queue = new ArrayQueue(2);
        Assert.Throws<Exception>(() => queue.Peek());
        Assert.Throws<Exception>(() => queue.Pop());
    }
}