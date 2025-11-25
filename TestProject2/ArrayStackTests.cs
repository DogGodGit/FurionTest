namespace TestProject2;

/// <summary>
/// 包含对 <see cref="ArrayStack"/> 类的单元测试。
/// </summary>
public class ArrayStackTests
{
    /// <summary>
    /// 测试 <see cref="ArrayStack"/> 的构造函数。
    /// 验证新创建的栈是否为空且大小为 0。
    /// </summary>
    [Fact]
    public void Test_Constructor()
    {
        var stack = new ArrayStack();
        Assert.True(stack.IsEmpty());
        Assert.Equal(0, stack.Size());
    }

    /// <summary>
    /// 测试 <see cref="ArrayStack.Push"/> 方法。
    /// 验证元素是否正确压入栈中，以及栈顶元素是否正确。
    /// </summary>
    [Fact]
    public void Test_Push()
    {
        var stack = new ArrayStack();
        stack.Push(10);
        stack.Push(20);
        Assert.Equal(2, stack.Size());
        Assert.Equal(20, stack.Peek());
    }

    /// <summary>
    /// 测试 <see cref="ArrayStack.Pop"/> 方法。
    /// 验证元素是否正确弹出栈，以及栈的大小和栈顶元素是否正确更新。
    /// </summary>
    [Fact]
    public void Test_Pop()
    {
        var stack = new ArrayStack();
        stack.Push(10);
        stack.Push(20);
        var top = stack.Pop();
        Assert.Equal(20, top);
        Assert.Equal(1, stack.Size());
        Assert.Equal(10, stack.Peek());
    }

    /// <summary>
    /// 测试 <see cref="ArrayStack.Peek"/> 方法。
    /// 验证栈顶元素是否正确返回。
    /// </summary>
    [Fact]
    public void Test_Peek()
    {
        var stack = new ArrayStack();
        stack.Push(10);
        Assert.Equal(10, stack.Peek());
    }

    /// <summary>
    /// 测试 <see cref="ArrayStack.ToArray"/> 方法。
    /// 验证栈中的元素是否正确转换为数组。
    /// </summary>
    [Fact]
    public void Test_ToArray()
    {
        var stack = new ArrayStack();
        stack.Push(10);
        stack.Push(20);
        var array = stack.ToArray();
        Assert.Equal(new[] { 10, 20 }, array);
    }

    /// <summary>
    /// 测试 <see cref="ArrayStack.IsEmpty"/> 方法。
    /// 验证栈是否正确判断为空或非空。
    /// </summary>
    [Fact]
    public void Test_IsEmpty()
    {
        var stack = new ArrayStack();
        Assert.True(stack.IsEmpty());
        stack.Push(10);
        Assert.False(stack.IsEmpty());
    }

    /// <summary>
    /// 测试 <see cref="ArrayStack.Size"/> 方法。
    /// 验证栈的大小是否正确返回。
    /// </summary>
    [Fact]
    public void Test_Size()
    {
        var stack = new ArrayStack();
        Assert.Equal(0, stack.Size());
        stack.Push(10);
        Assert.Equal(1, stack.Size());
    }

    /// <summary>
    /// 测试栈的下溢行为。
    /// 验证在栈为空时调用 <see cref="ArrayStack.Peek"/> 和 <see cref="ArrayStack.Pop"/> 是否抛出异常。
    /// </summary>
    [Fact]
    public void Test_Underflow()
    {
        var stack = new ArrayStack();
        Assert.Throws<Exception>(() => stack.Peek());
        Assert.Throws<Exception>(() => stack.Pop());
    }
}