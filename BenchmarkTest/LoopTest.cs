using BenchmarkDotNet.Attributes;
using System.Text;

[MemoryDiagnoser]//记录内存分配情况
public class LoopBenchmark
{
    public IEnumerable<object> Source => new object[] { 1 };

    public IEnumerable<object[]> Source1 => new object[][] {
        new object[] { 1, 0 }
    };

    [Benchmark]
    [ArgumentsSource(nameof(Source))]
    public int ForLoop(int n)
    {
        int res = 0;
        // 循环求和 1, 2, ..., n-1, n
        for (int i = 1; i <= n; i++)
        {
            res += i;
        }
        return res;
    }

    [Benchmark]
    [ArgumentsSource(nameof(Source))]
    public int WhileLoop(int n)
    {
        int res = 0;
        int i = 1; // 初始化条件变量
                   // 循环求和 1, 2, ..., n-1, n
        while (i <= n)
        {
            res += i;
            i += 1; // 更新条件变量
        }
        return res;
    }

    [Benchmark]
    [ArgumentsSource(nameof(Source))]
    public int WhileLoopII(int n)
    {
        int res = 0;
        int i = 1; // 初始化条件变量
                   // 循环求和 1, 4, 10, ...
        while (i <= n)
        {
            res += i;
            // 更新条件变量
            i += 1;
            i *= 2;
        }
        return res;
    }

    [Benchmark]
    [ArgumentsSource(nameof(Source))]
    public string NestedForLoop(int n)
    {
        StringBuilder res = new();
        // 循环 i = 1, 2, ..., n-1, n
        for (int i = 1; i <= n; i++)
        {
            // 循环 j = 1, 2, ..., n-1, n
            for (int j = 1; j <= n; j++)
            {
                res.Append($"({i}, {j}), ");
            }
        }
        return res.ToString();
    }

    [Benchmark]
    [ArgumentsSource(nameof(Source))]
    public int Recur(int n)
    {
        // 终止条件
        if (n == 1)
            return 1;
        // 递：递归调用
        int res = Recur(n - 1);
        // 归：返回结果
        return n + res;
    }

    [Benchmark]
    [ArgumentsSource(nameof(Source1))]
    public int TailRecur(int n, int res = 0)
    {
        // 终止条件
        if (n == 0)
            return res;
        // 尾递归调用
        return TailRecur(n - 1, res + n);
    }

    [Benchmark]
    [ArgumentsSource(nameof(Source))]
    public int Fib(int n)
    {
        // 终止条件 f(1) = 0, f(2) = 1
        if (n == 1 || n == 2)
            return n - 1;
        // 递归调用 f(n) = f(n-1) + f(n-2)
        int res = Fib(n - 1) + Fib(n - 2);
        Console.WriteLine($"Fib({n}) = {res}");
        // 返回结果 f(n)
        return res;
    }

    [Benchmark]
    public int BubbleSort(int[] nums)
    {
        int count = 0;  // 计数器
                        // 外循环：未排序区间为 [0, i]
        for (int i = nums.Length - 1; i > 0; i--)
        {
            // 内循环：将未排序区间 [0, i] 中的最大元素交换至该区间的最右端
            for (int j = 0; j < i; j++)
            {
                if (nums[j] > nums[j + 1])
                {
                    // 交换 nums[j] 与 nums[j + 1]
                    (nums[j + 1], nums[j]) = (nums[j], nums[j + 1]);
                    count += 3;  // 元素交换包含 3 个单元操作
                }
            }
        }
        return count;
    }

    [Benchmark]
    public int Exponential(int n)
    {
        int count = 0, bas = 1;
        // 细胞每轮一分为二，形成数列 1, 2, 4, 8, ..., 2^(n-1)
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < bas; j++)
            {
                count++;
            }
            bas *= 2;
        }
        // count = 1 + 2 + 4 + 8 + .. + 2^(n-1) = 2^n - 1
        return count;
    }

    [Benchmark]
    public int ExpRecur(int n)
    {
        if (n == 1) return 1;
        return ExpRecur(n - 1) + ExpRecur(n - 1) + 1;
    }

    [Benchmark]
    public int Logarithmic(int n)
    {
        int count = 0;
        while (n > 1)
        {
            n /= 2;
            count++;
        }
        return count;
    }

    [Benchmark]
    public int LogRecur(int n)
    {
        if (n <= 1) return 0;
        return LogRecur(n / 2) + 1;
    }

    [Benchmark]
    public int LinearLogRecur(int n)
    {
        if (n <= 1) return 1;
        int count = LinearLogRecur(n / 2) + LinearLogRecur(n / 2);
        for (int i = 0; i < n; i++)
        {
            count++;
        }
        return count;
    }

    [Benchmark]
    public int[] RandomNumbers(int n)
    {
        int[] nums = new int[n];
        // 生成数组 nums = { 1, 2, 3, ..., n }
        for (int i = 0; i < n; i++)
        {
            nums[i] = i + 1;
        }

        // 随机打乱数组元素
        for (int i = 0; i < nums.Length; i++)
        {
            int index = new Random().Next(i, nums.Length);
            (nums[i], nums[index]) = (nums[index], nums[i]);
        }
        return nums;
    }

    [Benchmark]
    public int FindOne(int[] nums)
    {
        for (int i = 0; i < nums.Length; i++)
        {
            // 当元素 1 在数组头部时，达到最佳时间复杂度 O(1)
            // 当元素 1 在数组尾部时，达到最差时间复杂度 O(n)
            if (nums[i] == 1)
                return i;
        }
        return -1;
    }

    /* 类 */

    internal class Node
    {
        public Node(int x)
        {
            int val = x;
            Node next;
        }
    }

    /* 函数 */

    private int Function()
    {
        // 执行某些操作...
        return 0;
    }

    public int Algorithm(int n)
    {        // 输入数据
        const int a = 0;          // 暂存数据（常量）
        int b = 0;                // 暂存数据（变量）
        Node node = new(0);       // 暂存数据（对象）
        int c = Function();       // 栈帧空间（调用函数）
        return a + b + c;         // 输出数据
    }

    public void Algorithm2(int n)
    {
        int a = 0;                   // O(1)
        int[] b = new int[10000];    // O(1)
        if (n > 10)
        {
            int[] nums = new int[n]; // O(n)
        }
    }

    /* 循环的空间复杂度为 O(1) */
    void Loop(int n)
    {
        for (int i = 0; i < n; i++)
        {
            Function();
        }
    }
    /* 递归的空间复杂度为 O(n) */
    int Recur2(int n)
    {
        if (n == 1) return 1;
        return Recur2(n - 1);
    }

    /* 常数阶 */
    void Constant(int n)
    {
        // 常量、变量、对象占用 O(1) 空间
        int a = 0;
        int b = 0;
        int[] nums = new int[10000];
        //ListNode node = new(0);
        // 循环中的变量占用 O(1) 空间
        for (int i = 0; i < n; i++)
        {
            int c = 0;
        }
        // 循环中的函数占用 O(1) 空间
        for (int i = 0; i < n; i++)
        {
            Function();
        }
    }

    /* 线性阶 */
    void Linear(int n)
    {
        // 长度为 n 的数组占用 O(n) 空间
        int[] nums = new int[n];
        // 长度为 n 的列表占用 O(n) 空间
        List<ListNode> nodes = new List<ListNode>();
        for (int i = 0; i < n; i++)
        {
            nodes.Add(new ListNode(i));
        }
        // 长度为 n 的哈希表占用 O(n) 空间
        Dictionary<int, string> map = new Dictionary<int, string>();
        for (int i = 0; i < n; i++)
        {
            map.Add(i, i.ToString());
        }
    }

    /* 线性阶（递归实现） */
    void LinearRecur(int n)
    {
        Console.WriteLine("递归 n = " + n);
        if (n == 1) return;
        LinearRecur(n - 1);
    }

    /* 平方阶 */
    void Quadratic(int n)
    {
        // 矩阵占用 O(n^2) 空间
        int[,] numMatrix = new int[n, n];
        // 二维列表占用 O(n^2) 空间
        List<List<int>> numList = new List<List<int>>();
        for (int i = 0; i < n; i++)
        {
            List<int> tmp = new List<int>();
            for (int j = 0; j < n; j++)
            {
                tmp.Add(0);
            }
            numList.Add(tmp);
        }
    }

    /* 平方阶（递归实现） */
    int QuadraticRecur(int n)
    {
        if (n <= 0) return 0;
        int[] nums = new int[n];
        Console.WriteLine("递归 n = " + n + " 中的 nums 长度 = " + nums.Length);
        return QuadraticRecur(n - 1);
    }

    /* 指数阶（建立满二叉树） */
    TreeNode? BuildTree(int n)
    {
        if (n == 0) return null;
        TreeNode root = new TreeNode(0)
        {
            left = BuildTree(n - 1),
            right = BuildTree(n - 1)
        };
        return root;
    }
}

internal class ListNode
{
    private int i;

    public ListNode(int i)
    {
        this.i = i;
    }
}

internal class TreeNode
{
    internal TreeNode left;
    internal TreeNode right;
    private int v;

    public TreeNode(int v)
    {
        this.v = v;
    }
}