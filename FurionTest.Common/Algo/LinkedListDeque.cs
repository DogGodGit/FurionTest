/* 基于双向链表实现的双向队列 */
public class LinkedListDeque
{
    private DoubleListNode? front, rear; // 头节点 front, 尾节点 rear
    private int queSize = 0;      // 双向队列的长度

    public LinkedListDeque()
    {
        front = null;
        rear = null;
    }

    /* 获取双向队列的长度 */

    public int Size()
    {
        return queSize;
    }

    /* 判断双向队列是否为空 */

    public bool IsEmpty()
    {
        return Size() == 0;
    }

    /* 入队操作 */

    private void Push(int num, bool isFront)
    {
        DoubleListNode node = new(num);
        // 若链表为空，则令 front 和 rear 都指向 node
        if (IsEmpty())
        {
            front = node;
            rear = node;
        }
        // 队首入队操作
        else if (isFront)
        {
            // 将 node 添加至链表头部
            front!.prev = node;
            node.next = front;
            front = node; // 更新头节点
        }
        // 队尾入队操作
        else
        {
            // 将 node 添加至链表尾部
            rear!.next = node;
            node.prev = rear;
            rear = node;  // 更新尾节点
        }

        queSize++; // 更新队列长度
    }

    /* 队首入队 */

    public void PushFirst(int num)
    {
        Push(num, true);
    }

    /* 队尾入队 */

    public void PushLast(int num)
    {
        Push(num, false);
    }

    /* 出队操作 */

    private int? Pop(bool isFront)
    {
        if (IsEmpty())
            throw new Exception();
        int? val;
        // 队首出队操作
        if (isFront)
        {
            val = front?.val; // 暂存头节点值
            // 删除头节点
            DoubleListNode? fNext = front?.next;
            if (fNext != null)
            {
                fNext.prev = null;
                front!.next = null;
            }
            front = fNext;   // 更新头节点
        }
        // 队尾出队操作
        else
        {
            val = rear?.val;  // 暂存尾节点值
            // 删除尾节点
            DoubleListNode? rPrev = rear?.prev;
            if (rPrev != null)
            {
                rPrev.next = null;
                rear!.prev = null;
            }
            rear = rPrev;    // 更新尾节点
        }

        queSize--; // 更新队列长度
        return val;
    }

    /* 队首出队 */

    public int? PopFirst()
    {
        return Pop(true);
    }

    /* 队尾出队 */

    public int? PopLast()
    {
        return Pop(false);
    }

    /* 访问队首元素 */

    public int? PeekFirst()
    {
        if (IsEmpty())
            throw new Exception();
        return front?.val;
    }

    /* 访问队尾元素 */

    public int? PeekLast()
    {
        if (IsEmpty())
            throw new Exception();
        return rear?.val;
    }

    /* 返回数组用于打印 */

    public int?[] ToArray()
    {
        DoubleListNode? node = front;
        int?[] res = new int?[Size()];
        for (int i = 0; i < res.Length; i++)
        {
            res[i] = node?.val;
            node = node?.next;
        }

        return res;
    }
}
