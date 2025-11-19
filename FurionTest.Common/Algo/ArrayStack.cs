/// <summary>
/// 基于数组实现的栈
/// </summary>
public class ArrayStack
{
    private List<int> stack;

    public ArrayStack()
    {
        // 初始化列表（动态数组）
        stack = new List<int>();
    }

    /* 获取栈的长度 */

    public int Size()
    {
        return stack.Count;
    }

    /* 判断栈是否为空 */

    public bool IsEmpty()
    {
        return Size() == 0;
    }

    /* 入栈 */

    public void Push(int num)
    {
        stack.Add(num);
    }

    /* 出栈 */

    public int Pop()
    {
        if (IsEmpty())
            throw new Exception();
        var val = Peek();
        stack.RemoveAt(Size() - 1);
        return val;
    }

    /* 访问栈顶元素 */

    public int Peek()
    {
        if (IsEmpty())
            throw new Exception();
        return stack[Size() - 1];
    }

    /* 将 List 转化为 Array 并返回 */

    public int[] ToArray()
    {
        return stack.ToArray(); // 替换集合表达式为 ToArray 方法
    }
}
