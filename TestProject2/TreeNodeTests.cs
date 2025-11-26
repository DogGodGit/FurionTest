namespace TestProject2;

/// <summary>
/// 测试 TreeNode 类的单元测试类。
/// </summary>
public class TreeNodeTests
{
    /// <summary>
    /// 测试 TreeNode 构造函数，验证节点值是否正确初始化。
    /// </summary>
    [Fact]
    public void Test_Constructor()
    {
        /* 初始化二叉树 */
        // 初始化节点
        TreeNode n1 = new(1);
        TreeNode n2 = new(2);
        TreeNode n3 = new(3);
        TreeNode n4 = new(4);
        TreeNode n5 = new(5);
        TreeNode n6 = new(6);
        TreeNode n7 = new(7);
        // 构建节点之间的引用（指针）
        n1.left = n2;
        n1.right = n3;
        n2.left = n4;
        n2.right = n5;
        n3.left = n6;
        n3.right = n7;

        Assert.Equal(2, n1.left.val);
        Assert.NotNull(n1.left.left);
        Assert.NotNull(n1.right.right);

        /* 插入与删除节点 */
        TreeNode P = new(0);
        // 在 n1 -> n2 中间插入节点 P
        n1.left = P;
        P.left = n2;
        // 删除节点 P
        n1.left = n2;

        var result = LevelOrder(n1);
        Assert.Equal(result, [1, 2, 3, 4, 5, 6, 7]);

        list.Clear();
        PreOrder(n1);
        Assert.Equal(list, [1, 2, 4, 5, 3, 6, 7]);

        list.Clear();
        InOrder(n1);
        Assert.Equal(list, [4, 2, 5, 1, 6, 3, 7]);

        list.Clear();
        PostOrder(n1);
        Assert.Equal(list, [4, 5, 2, 6, 7, 3, 1]);
    }

    /* 层序遍历 */

    private List<int> LevelOrder(TreeNode root)
    {
        // 初始化队列，加入根节点
        Queue<TreeNode> queue = new();
        queue.Enqueue(root);
        // 初始化一个列表，用于保存遍历序列
        List<int> list = [];
        while (queue.Count != 0)
        {
            TreeNode node = queue.Dequeue(); // 队列出队
            list.Add(node.val!.Value);       // 保存节点值
            if (node.left != null)
                queue.Enqueue(node.left);    // 左子节点入队
            if (node.right != null)
                queue.Enqueue(node.right);   // 右子节点入队
        }
        return list;
    }

    private List<int> list = new();

    /* 前序遍历 */

    private void PreOrder(TreeNode? root)
    {
        if (root == null) return;
        // 访问优先级：根节点 -> 左子树 -> 右子树
        list.Add(root.val!.Value);
        PreOrder(root.left);
        PreOrder(root.right);
    }

    /* 中序遍历 */

    private void InOrder(TreeNode? root)
    {
        if (root == null) return;
        // 访问优先级：左子树 -> 根节点 -> 右子树
        InOrder(root.left);
        list.Add(root.val!.Value);
        InOrder(root.right);
    }

    /* 后序遍历 */

    private void PostOrder(TreeNode? root)
    {
        if (root == null) return;
        // 访问优先级：左子树 -> 右子树 -> 根节点
        PostOrder(root.left);
        PostOrder(root.right);
        list.Add(root.val!.Value);
    }
}