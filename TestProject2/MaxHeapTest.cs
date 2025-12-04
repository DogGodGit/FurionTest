using FurionTest.Common.Algo;

namespace TestProject2;

public class MaxHeapTest
{
    [Fact]
    public void Test_PriorityQueue()
    {
        /* 初始化堆 */
        // 初始化小顶堆
        PriorityQueue<int, int> minHeap = new();

        /* 输入列表并建堆 */
        minHeap = new PriorityQueue<int, int>([(1, 1), (3, 3), (2, 2), (5, 5), (4, 4)]);

        Assert.Equal(5, minHeap.Count);

        // 初始化大顶堆（使用 lambda 表达式修改 Comparer 即可）
        PriorityQueue<int, int> maxHeap = new(Comparer<int>.Create((x, y) => y.CompareTo(x)));

        /* 元素入堆 */
        maxHeap.Enqueue(1, 1);
        maxHeap.Enqueue(3, 3);
        maxHeap.Enqueue(2, 2);
        maxHeap.Enqueue(5, 5);
        maxHeap.Enqueue(4, 4);

        /* 获取堆顶元素 */
        int peek = maxHeap.Peek();//5

        Assert.Equal(5, peek);

        /* 堆顶元素出堆 */
        // 出堆元素会形成一个从大到小的序列
        peek = maxHeap.Dequeue();  // 5
        Assert.Equal(5, peek);
        peek = maxHeap.Dequeue();  // 4
        Assert.Equal(4, peek);
        peek = maxHeap.Dequeue();  // 3
        Assert.Equal(3, peek);
        peek = maxHeap.Dequeue();  // 2
        Assert.Equal(2, peek);
        peek = maxHeap.Dequeue();  // 1
        Assert.Equal(1, peek);

        /* 获取堆大小 */
        int size = maxHeap.Count;

        /* 判断堆是否为空 */
        bool isEmpty = maxHeap.Count == 0;

        Assert.True(isEmpty);
    }

    // 测试正常情况：数组长度大于 k
    [Fact]
    public void Test_TopKHeap_正常情况()
    {
        int[] nums = { 3, 1, 5, 12, 2, 11 };
        int k = 3;
        PriorityQueue<int, int> result = TopKHeap(nums, k);

        // 验证堆中元素为最大的 k 个
        var dequeue = result.Dequeue();
        Assert.Equal(5, dequeue);
        dequeue = result.Dequeue();
        Assert.Equal(11, dequeue);
        dequeue = result.Dequeue();
        Assert.Equal(12, dequeue);
    }

    /* 基于堆查找数组中最大的 k 个元素 */
    PriorityQueue<int, int> TopKHeap(int[] nums, int k)
    {
        // 初始化小顶堆
        PriorityQueue<int, int> heap = new();
        // 将数组的前 k 个元素入堆
        for (int i = 0; i < k; i++)
        {
            heap.Enqueue(nums[i], nums[i]);
        }
        // 从第 k+1 个元素开始，保持堆的长度为 k
        for (int i = k; i < nums.Length; i++)
        {
            // 若当前元素大于堆顶元素，则将堆顶元素出堆、当前元素入堆
            if (nums[i] > heap.Peek())
            {
                heap.Dequeue();
                heap.Enqueue(nums[i], nums[i]);
            }
        }
        return heap;
    }

    [Fact]
    public void Test_CreateMaxHeap()
    {
        // 测试顺序列表的情况，堆顶应为最大值
        var nums = new List<int> { 1, 2, 3, 4, 5, 6 };
        var heap = new MaxHeap(nums);
        Assert.Equal(6, heap.Peek());
    }

    // 测试正常情况下的 Push 和 Pop
    [Fact]
    public void TestPushPopNormal()
    {
        var heap = new MaxHeap();
        heap.Push(5);
        heap.Push(3);
        heap.Push(8);
        heap.Push(1);
        heap.Push(7);

        Assert.Equal(8, heap.Pop()); // 堆顶元素应为8
        Assert.Equal(7, heap.Pop()); // 堆顶元素应为7
        Assert.Equal(5, heap.Pop()); // 堆顶元素应为5
        Assert.Equal(3, heap.Pop()); // 堆顶元素应为3
        Assert.Equal(1, heap.Pop()); // 堆顶元素应为1
    }

    // 测试空堆的 Pop 操作
    [Fact]
    public void TestPopEmptyHeap()
    {
        var heap = new MaxHeap();
        Assert.Throws<ArgumentOutOfRangeException>(() => heap.Pop()); // 应抛出异常
    }

    // 测试单元素堆的 Pop 操作
    [Fact]
    public void TestPopSingleElementHeap()
    {
        var heap = new MaxHeap();
        heap.Push(5);
        Assert.Equal(5, heap.Pop()); // 堆顶元素应为5
        Assert.Equal(0, heap.Size()); // 堆大小应为0
    }

    // 测试 Peek 操作
    [Fact]
    public void TestPeek()
    {
        var heap = new MaxHeap();
        heap.Push(5);
        heap.Push(3);
        heap.Push(8);
        heap.Push(1);
        heap.Push(7);

        Assert.Equal(8, heap.Peek()); // 堆顶元素应为8
        Assert.Equal(5, heap.Size()); // 堆大小应为5
    }

    // 测试空堆的 Peek 操作
    [Fact]
    public void TestPeekEmptyHeap()
    {
        var heap = new MaxHeap();
        Assert.Throws<ArgumentOutOfRangeException>(() => heap.Peek()); // 应抛出异常
    }

    // 测试单元素堆的 Peek 操作
    [Fact]
    public void TestPeekSingleElementHeap()
    {
        var heap = new MaxHeap();
        heap.Push(5);
        Assert.Equal(5, heap.Peek()); // 堆顶元素应为5
        Assert.Equal(1, heap.Size()); // 堆大小应为1
    }

    // 测试 Size 操作
    [Fact]
    public void TestSize()
    {
        var heap = new MaxHeap();
        Assert.Equal(0, heap.Size()); // 初始堆大小应为0
        heap.Push(5);
        Assert.Equal(1, heap.Size()); // 堆大小应为1
        heap.Pop();
        Assert.Equal(0, heap.Size()); // 堆大小应为0
    }
}