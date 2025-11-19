/* 基于链表实现的栈 */
public class LinkedListStack
{
    private ListNode? stackPeek;  // 将头节点作为栈顶
    private int stkSize = 0;   // 栈的长度

    public LinkedListStack()
    {
        stackPeek = null;
    }

    /* 获取栈的长度 */

    public int Size()
    {
        return stkSize;
    }

    /* 判断栈是否为空 */

    public bool IsEmpty()
    {
        return Size() == 0;
    }

    /* 入栈 */

    public void Push(int num)
    {
        ListNode node = new(num)
        {
            next = stackPeek
        };
        stackPeek = node;
        stkSize++;
    }

    /* 出栈 */

    public int Pop()
    {
        int num = Peek();
        stackPeek = stackPeek!.next;
        stkSize--;
        return num;
    }

    /* 访问栈顶元素 */

    public int Peek()
    {
        if (IsEmpty())
            throw new Exception();
        return stackPeek!.val;
    }

    /* 将 List 转化为 Array 并返回 */

    public int[] ToArray()
    {
        if (stackPeek == null)
            return Array.Empty<int>();

        ListNode? node = stackPeek;
        int[] res = new int[Size()];
        for (int i = res.Length - 1; i >= 0; i--)
        {
            res[i] = node!.val;
            node = node.next;
        }
        return res;
    }
}
