/* 二叉树节点类 */
public class ArrayBinaryTree(List<int?> arr)
{
    List<int?> tree = new(arr);

    /* 列表容量 */
    public int Size()
    {
        return tree.Count;
    }

    /* 获取索引为 i 节点的值 */
    public int? Val(int i)
    {
        // 若索引越界，则返回 null ，代表空位
        if (i < 0 || i >= Size())
            return null;
        return tree[i];
    }

    /* 获取索引为 i 节点的左子节点的索引 */
    public int Left(int i)
    {
        return 2 * i + 1;
    }

    /* 获取索引为 i 节点的右子节点的索引 */
    public int Right(int i)
    {
        return 2 * i + 2;
    }

    /* 获取索引为 i 节点的父节点的索引 */
    public int Parent(int i)
    {
        return (i - 1) / 2;
    }

    /* 层序遍历 */
    public List<int> LevelOrder()
    {
        List<int> res = [];
        // 直接遍历数组
        for (int i = 0; i < Size(); i++)
        {
            if (Val(i).HasValue)
                res.Add(Val(i)!.Value);
        }
        return res;
    }

    /* 深度优先遍历 */
    void DFS(int i, string order, List<int> res)
    {
        // 若为空位，则返回
        if (!Val(i).HasValue)
            return;
        // 前序遍历
        if (order == "pre")
            res.Add(Val(i)!.Value);
        DFS(Left(i), order, res);
        // 中序遍历
        if (order == "in")
            res.Add(Val(i)!.Value);
        DFS(Right(i), order, res);
        // 后序遍历
        if (order == "post")
            res.Add(Val(i)!.Value);
    }

    /* 前序遍历 */
    public List<int> PreOrder()
    {
        List<int> res = [];
        DFS(0, "pre", res);
        return res;
    }

    /* 中序遍历 */
    public List<int> InOrder()
    {
        List<int> res = [];
        DFS(0, "in", res);
        return res;
    }

    /* 后序遍历 */
    public List<int> PostOrder()
    {
        List<int> res = [];
        DFS(0, "post", res);
        return res;
    }
}