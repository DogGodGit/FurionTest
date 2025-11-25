namespace TestProject2;

/// <summary>
/// 测试 LinkedListQueue 类的单元测试类。
/// </summary>
public class LinkedListQueueTests
{
    /// <summary>
    /// 测试构造函数，验证初始化后的队列是否为空。
    /// </summary>
    [Fact]
    public void Test_Constructor()
    {
        var queue = new LinkedListQueue();
        Assert.True(queue.IsEmpty());
        Assert.Equal(0, queue.Size());
    }

    /// <summary>
    /// 测试 Push 方法，验证入队操作的功能。
    /// </summary>
    [Fact]
    public void Test_Push()
    {
        var queue = new LinkedListQueue();
        queue.Push(10);
        queue.Push(20);
        Assert.Equal(2, queue.Size());
        Assert.Equal(10, queue.Peek());
    }

    /// <summary>
    /// 测试 Pop 方法，验证出队操作的功能。
    /// </summary>
    [Fact]
    public void Test_Pop()
    {
        var queue = new LinkedListQueue();
        queue.Push(10);
        queue.Push(20);
        var first = queue.Pop();
        Assert.Equal(10, first);
        Assert.Equal(1, queue.Size());
        Assert.Equal(20, queue.Peek());
    }

    /// <summary>
    /// 测试 Peek 方法，验证获取队首元素的功能。
    /// </summary>
    [Fact]
    public void Test_Peek()
    {
        var queue = new LinkedListQueue();
        queue.Push(10);
        Assert.Equal(10, queue.Peek());
    }

    /// <summary>
    /// 测试 ToArray 方法，验证将队列转换为数组的功能。
    /// </summary>
    [Fact]
    public void Test_ToArray()
    {
        var queue = new LinkedListQueue();
        queue.Push(10);
        queue.Push(20);
        var array = queue.ToArray();
        Assert.Equal(new[] { 10, 20 }, array);
    }

    /// <summary>
    /// 测试 IsEmpty 方法，验证队列是否为空的功能。
    /// </summary>
    [Fact]
    public void Test_IsEmpty()
    {
        var queue = new LinkedListQueue();
        Assert.True(queue.IsEmpty());
        queue.Push(10);
        Assert.False(queue.IsEmpty());
    }

    /// <summary>
    /// 测试 Size 方法，验证获取队列大小的功能。
    /// </summary>
    [Fact]
    public void Test_Size()
    {
        var queue = new LinkedListQueue();
        Assert.Equal(0, queue.Size());
        queue.Push(10);
        Assert.Equal(1, queue.Size());
    }

    /// <summary>
    /// 测试队列下溢时的行为，验证是否正确抛出异常。
    /// </summary>
    [Fact]
    public void Test_Underflow()
    {
        var queue = new LinkedListQueue();
        Assert.Throws<Exception>(() => queue.Pop());
        Assert.Throws<Exception>(() => queue.Peek());
    }
}