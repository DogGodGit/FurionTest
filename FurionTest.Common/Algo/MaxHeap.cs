namespace FurionTest.Common.Algo;

public class MaxHeap
{
    private List<int> maxHeap;

    public MaxHeap()
    {
        maxHeap = [];
    }

    /* 构造函数，根据输入列表建堆 */

    public MaxHeap(IEnumerable<int> nums)
    {
        // 将列表元素原封不动添加进堆
        maxHeap = [.. nums];
        // 堆化除叶节点以外的其他所有节点
        var size = Parent(this.Size() - 1);
        for (int i = size; i >= 0; i--)
        {
            SiftDown(i);
        }
    }

    /* 获取左子节点的索引 */

    private int Left(int i)
    {
        return 2 * i + 1;
    }

    /* 获取右子节点的索引 */

    private int Right(int i)
    {
        return 2 * i + 2;
    }

    /* 获取父节点的索引 */

    private int Parent(int i)
    {
        return (i - 1) / 2; // 向下整除
    }

    /* 访问堆顶元素 */

    public int Peek()
    {
        return maxHeap[0];
    }

    /* 元素入堆 */

    public void Push(int val)
    {
        // 添加节点
        maxHeap.Add(val);
        // 从底至顶堆化
        SiftUp(Size() - 1);
    }

    /* 从节点 i 开始，从底至顶堆化 */

    private void SiftUp(int i)
    {
        while (true)
        {
            // 获取节点 i 的父节点
            int p = Parent(i);
            // 若“越过根节点”或“节点无须修复”，则结束堆化
            if (p < 0 || maxHeap[i] <= maxHeap[p])
                break;
            // 交换两节点
            Swap(i, p);
            // 循环向上堆化
            i = p;
        }
    }

    /* 元素出堆 */

    public int Pop()
    {
        // 判空处理
        if (IsEmpty())
            throw new ArgumentOutOfRangeException();
        // 交换根节点与最右叶节点（交换首元素与尾元素）
        Swap(0, Size() - 1);
        // 删除节点
        int val = maxHeap.Last();
        maxHeap.RemoveAt(Size() - 1);
        // 从顶至底堆化
        SiftDown(0);
        // 返回堆顶元素
        return val;
    }

    private bool IsEmpty()
    {
        return maxHeap == null || maxHeap.Count == 0;
    }

    /* 从节点 i 开始，从顶至底堆化 */

    private void SiftDown(int i)
    {
        while (true)
        {
            // 判断节点 i, l, r 中值最大的节点，记为 ma
            int l = Left(i), r = Right(i), max = i;
            if (l < Size() && maxHeap[l] > maxHeap[max])
                max = l;
            if (r < Size() && maxHeap[r] > maxHeap[max])
                max = r;
            // 若“节点 i 最大”或“越过叶节点”，则结束堆化
            if (max == i) break;
            // 交换两节点
            Swap(i, max);
            // 循环向下堆化
            i = max;
        }
    }

    private void Swap(int i, int max)
    {
        (maxHeap[i], maxHeap[max]) = (maxHeap[max], maxHeap[i]);
    }

    public int Size()
    {
        return maxHeap.Count;
    }
}