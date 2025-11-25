namespace TestProject2;

/// <summary>
/// 测试 LinkedListStack 类的单元测试类。
/// </summary>
public class LinkedListStackTests
{
    /// <summary>
    /// 测试构造函数，验证初始化后的栈是否为空。
    /// </summary>
    [Fact]
    public void Test_Constructor()
    {
        var stack = new LinkedListStack();
        Assert.True(stack.IsEmpty());
        Assert.Equal(0, stack.Size());
    }

    /// <summary>
    /// 测试 Push 方法，验证入栈操作的功能。
    /// </summary>
    [Fact]
    public void Test_Push()
    {
        var stack = new LinkedListStack();
        stack.Push(10);
        stack.Push(20);
        Assert.Equal(2, stack.Size());
        Assert.Equal(20, stack.Peek());
    }

    /// <summary>
    /// 测试 Pop 方法，验证出栈操作的功能。
    /// </summary>
    [Fact]
    public void Test_Pop()
    {
        var stack = new LinkedListStack();
        stack.Push(10);
        stack.Push(20);
        var top = stack.Pop();
        Assert.Equal(20, top);
        Assert.Equal(1, stack.Size());
        Assert.Equal(10, stack.Peek());
    }

    /// <summary>
    /// 测试 Peek 方法，验证获取栈顶元素的功能。
    /// </summary>
    [Fact]
    public void Test_Peek()
    {
        var stack = new LinkedListStack();
        stack.Push(10);
        Assert.Equal(10, stack.Peek());
    }

    /// <summary>
    /// 测试 ToArray 方法，验证将栈转换为数组的功能。
    /// </summary>
    [Fact]
    public void Test_ToArray()
    {
        var stack = new LinkedListStack();
        stack.Push(10);
        stack.Push(20);
        var array = stack.ToArray();
        Assert.Equal(new[] { 10, 20 }, array);
    }

    /// <summary>
    /// 测试 IsEmpty 方法，验证栈是否为空的功能。
    /// </summary>
    [Fact]
    public void Test_IsEmpty()
    {
        var stack = new LinkedListStack();
        Assert.True(stack.IsEmpty());
        stack.Push(10);
        Assert.False(stack.IsEmpty());
    }

    /// <summary>
    /// 测试 Size 方法，验证获取栈大小的功能。
    /// </summary>
    [Fact]
    public void Test_Size()
    {
        var stack = new LinkedListStack();
        Assert.Equal(0, stack.Size());
        stack.Push(10);
        Assert.Equal(1, stack.Size());
    }

    /// <summary>
    /// 测试栈下溢时的行为，验证是否正确抛出异常。
    /// </summary>
    [Fact]
    public void Test_Underflow()
    {
        var stack = new LinkedListStack();
        Assert.Throws<Exception>(() => stack.Pop());
        Assert.Throws<Exception>(() => stack.Peek());
    }
}