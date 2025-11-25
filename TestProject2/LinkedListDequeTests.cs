namespace TestProject2;

/// <summary>
/// 测试 LinkedListDeque 类的单元测试类。
/// </summary>
public class LinkedListDequeTests
{
    /// <summary>
    /// 测试构造函数，验证初始化后的队列是否为空。
    /// </summary>
    [Fact]
    public void Test_Constructor()
    {
        var deque = new LinkedListDeque();
        Assert.True(deque.IsEmpty());
        Assert.Equal(0, deque.Size());
    }

    /// <summary>
    /// 测试 PushFirst 方法，验证从队列头部插入元素的功能。
    /// </summary>
    [Fact]
    public void Test_PushFirst()
    {
        var deque = new LinkedListDeque();
        deque.PushFirst(10);
        deque.PushFirst(20);
        Assert.Equal(2, deque.Size());
        Assert.Equal(20, deque.PeekFirst());
    }

    /// <summary>
    /// 测试 PushLast 方法，验证从队列尾部插入元素的功能。
    /// </summary>
    [Fact]
    public void Test_PushLast()
    {
        var deque = new LinkedListDeque();
        deque.PushLast(10);
        deque.PushLast(20);
        Assert.Equal(2, deque.Size());
        Assert.Equal(20, deque.PeekLast());
    }

    /// <summary>
    /// 测试 PopFirst 方法，验证从队列头部移除元素的功能。
    /// </summary>
    [Fact]
    public void Test_PopFirst()
    {
        var deque = new LinkedListDeque();
        deque.PushLast(10);
        deque.PushLast(20);
        var first = deque.PopFirst();
        Assert.Equal(10, first);
        Assert.Equal(1, deque.Size());
    }

    /// <summary>
    /// 测试 PopLast 方法，验证从队列尾部移除元素的功能。
    /// </summary>
    [Fact]
    public void Test_PopLast()
    {
        var deque = new LinkedListDeque();
        deque.PushLast(10);
        deque.PushLast(20);
        var last = deque.PopLast();
        Assert.Equal(20, last);
        Assert.Equal(1, deque.Size());
    }

    /// <summary>
    /// 测试 PeekFirst 方法，验证获取队列头部元素的功能。
    /// </summary>
    [Fact]
    public void Test_PeekFirst()
    {
        var deque = new LinkedListDeque();
        deque.PushLast(10);
        Assert.Equal(10, deque.PeekFirst());
    }

    /// <summary>
    /// 测试 PeekLast 方法，验证获取队列尾部元素的功能。
    /// </summary>
    [Fact]
    public void Test_PeekLast()
    {
        var deque = new LinkedListDeque();
        deque.PushLast(10);
        Assert.Equal(10, deque.PeekLast());
    }

    /// <summary>
    /// 测试 ToArray 方法，验证将队列转换为数组的功能。
    /// </summary>
    [Fact]
    public void Test_ToArray()
    {
        var deque = new LinkedListDeque();
        deque.PushLast(10);
        deque.PushLast(20);
        var array = deque.ToArray();
        Assert.Equal(new int?[] { 10, 20 }, array);
    }

    /// <summary>
    /// 测试 IsEmpty 方法，验证队列是否为空的功能。
    /// </summary>
    [Fact]
    public void Test_IsEmpty()
    {
        var deque = new LinkedListDeque();
        Assert.True(deque.IsEmpty());
        deque.PushLast(10);
        Assert.False(deque.IsEmpty());
    }

    /// <summary>
    /// 测试 Size 方法，验证获取队列大小的功能。
    /// </summary>
    [Fact]
    public void Test_Size()
    {
        var deque = new LinkedListDeque();
        Assert.Equal(0, deque.Size());
        deque.PushLast(10);
        Assert.Equal(1, deque.Size());
    }

    /// <summary>
    /// 测试队列下溢时的行为，验证是否正确抛出异常。
    /// </summary>
    [Fact]
    public void Test_Underflow()
    {
        var deque = new LinkedListDeque();
        Assert.Throws<Exception>(() => deque.PopFirst());
        Assert.Throws<Exception>(() => deque.PopLast());
        Assert.Throws<Exception>(() => deque.PeekFirst());
        Assert.Throws<Exception>(() => deque.PeekLast());
    }
}