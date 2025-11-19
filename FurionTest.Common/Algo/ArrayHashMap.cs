/* 基于数组实现的哈希表 */
public class ArrayHashMap
{
    private List<Pair?> buckets;

    public ArrayHashMap()
    {
        // 初始化数组，包含 100 个桶
        buckets = new();
        for (int i = 0; i < 100; i++)
        {
            buckets.Add(null);
        }
    }

    /* 哈希函数 */

    private int HashFunc(int key)
    {
        int index = key % 100;
        return index;
    }

    /* 查询操作 */

    public string? Get(int key)
    {
        int index = HashFunc(key);
        Pair? pair = buckets[index];
        if (pair == null) return null;
        return pair.val;
    }

    /* 添加操作 */

    public void Put(int key, string val)
    {
        Pair pair = new(key, val);
        int index = HashFunc(key);
        buckets[index] = pair;
    }

    /* 删除操作 */

    public void Remove(int key)
    {
        int index = HashFunc(key);
        // 置为 null ，代表删除
        buckets[index] = null;
    }

    /* 获取所有键值对 */

    public List<Pair> PairSet()
    {
        List<Pair> pairSet = new();
        foreach (Pair? pair in buckets)
        {
            if (pair != null)
                pairSet.Add(pair);
        }
        return pairSet;
    }

    /* 获取所有键 */

    public List<int> KeySet()
    {
        List<int> keySet = new();
        foreach (Pair? pair in buckets)
        {
            if (pair != null)
                keySet.Add(pair.key);
        }
        return keySet;
    }

    /* 获取所有值 */

    public List<string> ValueSet()
    {
        List<string> valueSet = new();
        foreach (Pair? pair in buckets)
        {
            if (pair != null)
                valueSet.Add(pair.val);
        }
        return valueSet;
    }

    /* 打印哈希表 */

    public void Print()
    {
        foreach (Pair kv in PairSet())
        {
            Console.WriteLine(kv.key + " -> " + kv.val);
        }
    }
}
