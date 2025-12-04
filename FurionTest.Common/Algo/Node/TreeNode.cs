/* 二叉树节点类 */

public partial class TreeNode(int? x)
{
    public int? val = x;    // 节点值
    public TreeNode? left;  // 左子节点引用
    public TreeNode? right; // 右子节点引用
}

/* AVL 树节点类 */

public partial class TreeNode
{
    public int height;      // 节点高度
}

public static class TreeNodeExtend
{
    /* 层序遍历 */

    public static List<int> LevelOrder(this TreeNode root)
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

    /* 前序遍历 */

    public static List<int> PreOrder(this TreeNode? root)
    {
        List<int> list = new();
        PreOrderHelper(root, list);
        return list;
    }

    private static void PreOrderHelper(TreeNode? root, List<int> list)
    {
        if (root == null) return;
        // 访问优先级：根节点 -> 左子树 -> 右子树
        list.Add(root.val!.Value);
        PreOrderHelper(root.left, list);
        PreOrderHelper(root.right, list);
    }

    /* 中序遍历 */

    public static List<int> InOrder(this TreeNode? root)
    {
        List<int> list = new();
        InOrderHelper(root, list);
        return list;
    }

    private static void InOrderHelper(TreeNode? root, List<int> list)
    {
        if (root == null) return;
        // 访问优先级：左子树 -> 根节点 -> 右子树
        InOrderHelper(root.left, list);
        list.Add(root.val!.Value);
        InOrderHelper(root.right, list);
    }

    /* 后序遍历 */

    public static List<int> PostOrder(this TreeNode? root)
    {
        List<int> list = new();
        PostOrderHelper(root, list);
        return list;
    }

    private static void PostOrderHelper(TreeNode? root, List<int> list)
    {
        if (root == null) return;
        // 访问优先级：左子树 -> 右子树 -> 根节点
        PostOrderHelper(root.left, list);
        PostOrderHelper(root.right, list);
        list.Add(root.val!.Value);
    }

    /* 查找节点 */

    public static TreeNode? Search(this TreeNode? root, int num)
    {
        TreeNode? cur = root;
        // 循环查找，越过叶节点后跳出
        while (cur != null)
        {
            // 目标节点在 cur 的右子树中
            if (cur.val < num) cur =
                cur.right;
            // 目标节点在 cur 的左子树中
            else if (cur.val > num)
                cur = cur.left;
            // 找到目标节点，跳出循环
            else
                break;
        }
        // 返回目标节点
        return cur;
    }

    /* 插入节点 */

    public static void Insert(this TreeNode? root, int num)
    {
        // 若树为空，则初始化根节点
        if (root == null)
        {
            root = new TreeNode(num);
            return;
        }
        TreeNode? cur = root, pre = null;
        // 循环查找，越过叶节点后跳出
        while (cur != null)
        {
            // 找到重复节点，直接返回
            if (cur.val == num)
                return;
            pre = cur;
            // 插入位置在 cur 的右子树中
            if (cur.val < num)
                cur = cur.right;
            // 插入位置在 cur 的左子树中
            else
                cur = cur.left;
        }

        // 插入节点
        TreeNode node = new(num);
        if (pre != null)
        {
            if (pre.val < num)
                pre.right = node;
            else
                pre.left = node;
        }
    }

    /* 删除节点 */

    public static void Remove(this TreeNode? root, int num)
    {
        // 若树为空，直接提前返回
        if (root == null)
            return;
        TreeNode? cur = root, pre = null;
        // 循环查找，越过叶节点后跳出
        while (cur != null)
        {
            // 找到待删除节点，跳出循环
            if (cur.val == num)
                break;
            pre = cur;
            // 待删除节点在 cur 的右子树中
            if (cur.val < num)
                cur = cur.right;
            // 待删除节点在 cur 的左子树中
            else
                cur = cur.left;
        }
        // 若无待删除节点，则直接返回
        if (cur == null)
            return;
        // 子节点数量 = 0 or 1
        if (cur.left == null || cur.right == null)
        {
            // 当子节点数量 = 0 / 1 时， child = null / 该子节点
            TreeNode? child = cur.left ?? cur.right;
            // 删除节点 cur
            if (cur != root)
            {
                if (pre!.left == cur)
                    pre.left = child;
                else
                    pre.right = child;
            }
            else
            {
                // 若删除节点为根节点，则重新指定根节点
                root = child;
            }
        }
        // 子节点数量 = 2
        else
        {
            // 获取中序遍历中 cur 的下一个节点
            TreeNode? tmp = cur.right;
            while (tmp.left != null)
            {
                tmp = tmp.left;
            }
            // 递归删除节点 tmp
            Remove(root, tmp.val!.Value);
            // 用 tmp 覆盖 cur
            cur.val = tmp.val;
        }
    }
}