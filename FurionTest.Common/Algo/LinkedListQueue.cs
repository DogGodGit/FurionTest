/* 基于链表实现的队列 */
public class LinkedListQueue
{
    private ListNode? front, rear;  // 头节点 front ，尾节点 rear
    private int queSize = 0;

    public LinkedListQueue()
    {
        front = null;
        rear = null;
    }

    /* 获取队列的长度 */

    public int Size()
    {
        return queSize;
    }

    /* 判断队列是否为空 */

    public bool IsEmpty()
    {
        return Size() == 0;
    }

    /* 入队 */

    public void Push(int num)
    {
        // 在尾节点后添加 num
        ListNode node = new(num);
        // 如果队列为空，则令头、尾节点都指向该节点
        if (front == null)
        {
            front = node;
            rear = node;
            // 如果队列不为空，则将该节点添加到尾节点后
        }
        else if (rear != null)
        {
            rear.next = node;
            rear = node;
        }
        queSize++;
    }

    /* 出队 */

    public int Pop()
    {
        int num = Peek();
        // 删除头节点
        front = front?.next;
        queSize--;
        return num;
    }

    /* 访问队首元素 */

    public int Peek()
    {
        if (IsEmpty())
            throw new Exception();
        return front!.val;
    }

    /* 将集合表达式替换为传统数组初始化方式 */

    public int[] ToArray()
    {
        if (front == null)
            return Array.Empty<int>(); // 替换集合表达式为传统数组初始化

        ListNode? node = front;
        int[] res = new int[Size()];
        for (int i = 0; i < res.Length; i++)
        {
            res[i] = node!.val;
            node = node.next;
        }
        return res;
    }
}
