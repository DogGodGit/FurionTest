namespace FurionTest.Common.Algo;

public static class ArrayExtend
{
    /* 二分查找（双闭区间） */
    public static int BinarySearch<T>(this T[] nums, T target) where T : IComparable<T>
    {
        // 初始化双闭区间 [0, n-1] ，即 i, j 分别指向数组首元素、尾元素
        int i = 0, j = nums.Length - 1;
        // 循环，当搜索区间为空时跳出（当 i > j 时为空）
        while (i <= j)
        {
            int m = i + (j - i) / 2;   // 计算中点索引 m
            if (nums[m].CompareTo(target) < 0) // 此情况说明 target 在区间 [m+1, j] 中
                i = m + 1;
            else if (nums[m].CompareTo(target) > 0) // 此情况说明 target 在区间 [i, m-1] 中
                j = m - 1;
            else                       // 找到目标元素，返回其索引
                return m;
        }
        // 未找到目标元素，返回 -1
        return -1;
    }

    /* 二分查找（左闭右开区间） */
    public static int BinarySearchLCRO<T>(this T[] nums, T target) where T : IComparable<T>
    {
        // 初始化左闭右开区间 [0, n) ，即 i, j 分别指向数组首元素、尾元素+1
        int i = 0, j = nums.Length;
        // 循环，当搜索区间为空时跳出（当 i = j 时为空）
        while (i < j)
        {
            int m = i + (j - i) / 2;   // 计算中点索引 m
            if (nums[m].CompareTo(target) < 0)      // 此情况说明 target 在区间 [m+1, j) 中
                i = m + 1;
            else if (nums[m].CompareTo(target) > 0) // 此情况说明 target 在区间 [i, m) 中
                j = m;
            else                       // 找到目标元素，返回其索引
                return m;
        }
        // 未找到目标元素，返回 -1
        return -1;
    }

    /* 二分查找插入点（无重复元素） */
    public static int BinarySearchInsertionSimple<T>(this T[] nums, T target) where T : IComparable<T>
    {
        int i = 0, j = nums.Length - 1; // 初始化双闭区间 [0, n-1]
        while (i <= j)
        {
            int m = i + (j - i) / 2; // 计算中点索引 m
            if (nums[m].CompareTo(target) < 0)      // 此情况说明 target 在区间 [m+1, j) 中
                i = m + 1;
            else if (nums[m].CompareTo(target) > 0) // 此情况说明 target 在区间 [i, m-1) 中
                j = m - 1;
            else
            {
                return m; // 找到 target ，返回插入点 m
            }
        }
        // 未找到 target ，返回插入点 i
        return i;
    }

    /* 二分查找插入点（存在重复元素） */
    public static int BinarySearchInsertion<T>(this T[] nums, T target) where T : IComparable<T>
    {
        int i = 0, j = nums.Length - 1; // 初始化双闭区间 [0, n-1]
        while (i <= j)
        {
            int m = i + (j - i) / 2; // 计算中点索引 m
            if (nums[m].CompareTo(target) < 0)      // 此情况说明 target 在区间 [m+1, j) 中
                i = m + 1;
            else if (nums[m].CompareTo(target) > 0) // 此情况说明 target 在区间 [i, m-1) 中
                j = m - 1;
            else
            {
                j = m - 1; // 首个小于 target 的元素在区间 [i, m-1] 中
            }
        }
        // 返回插入点 i
        return i;
    }

    /* 二分查找最左一个 target */
    public static int BinarySearchLeftEdge<T>(this T[] nums, T target) where T : IComparable<T>
    {
        // 等价于查找 target 的插入点
        int i = BinarySearchInsertion(nums, target);
        // 未找到 target ，返回 -1
        if (i == nums.Length || nums[i].CompareTo(target) != 0)
        {
            return -1;
        }
        // 找到 target ，返回索引 i
        return i;
    }

    /* 二分查找最右一个 target */
    public static int BinarySearchRightEdge(this int[] nums, int target)
    {
        // 转化为查找最左一个 target + 1
        int i = BinarySearchInsertion(nums, target + 1);
        // j 指向最右一个 target ，i 指向首个大于 target 的元素
        int j = i - 1;
        // 未找到 target ，返回 -1
        if (j == -1 || nums[j] != target)
        {
            return -1;
        }
        // 找到 target ，返回索引 j
        return j;
    }

    /* 方法一：暴力枚举 */
    public static int[] TwoSumBruteForce(this int[] nums, int target)
    {
        int size = nums.Length;
        // 两层循环，时间复杂度为 O(n^2)
        for (int i = 0; i < size - 1; i++)
        {
            for (int j = i + 1; j < size; j++)
            {
                if (nums[i] + nums[j] == target)
                    return [i, j];
            }
        }
        return [];
    }

    /* 方法二：辅助哈希表 */
    public static int[] TwoSumHashTable(this int[] nums, int target)
    {
        // 辅助哈希表，空间复杂度为 O(n)
        Dictionary<int, int> dic = [];
        // 单层循环，时间复杂度为 O(n)
        for (int i = 0; i < nums.Length; i++)
        {
            var m = target - nums[i];
            if (dic.TryGetValue(m, out int v))
            {
                return [v, i];
            }
            dic.Add(nums[i], i);
        }
        return [];
    }
}
