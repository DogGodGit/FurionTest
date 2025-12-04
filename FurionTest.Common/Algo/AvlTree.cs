/* 二叉树节点类 */

public static class AvlTreeExtensions
{
    /* 获取节点高度 */

    public static int Height(this TreeNode? node)
    {
        // 空节点高度为 -1 ，叶节点高度为 0
        return node == null ? -1 : node.height;
    }

    /* 更新节点高度 */

    public static void UpdateHeight(this TreeNode node)
    {
        // 节点高度等于最高子树高度 + 1
        node.height = Math.Max(Height(node.left), Height(node.right)) + 1;
    }

    /* 获取平衡因子 */

    public static int BalanceFactor(this TreeNode? node)
    {
        // 空节点平衡因子为 0
        if (node == null) return 0;
        // 节点平衡因子 = 左子树高度 - 右子树高度
        return Height(node.left) - Height(node.right);
    }

    /* 右旋操作 */

    public static TreeNode? RightRotate(this TreeNode? node)
    {
        TreeNode? child = node?.left;
        TreeNode? grandChild = child?.right;
        // 以 child 为原点，将 node 向右旋转
        child.right = node;
        node.left = grandChild;
        // 更新节点高度
        UpdateHeight(node);
        UpdateHeight(child);
        // 返回旋转后子树的根节点
        return child;
    }

    /* 左旋操作 */

    public static TreeNode? LeftRotate(this TreeNode? node)
    {
        TreeNode? child = node?.right;
        TreeNode? grandChild = child?.left;
        // 以 child 为原点，将 node 向左旋转
        child.left = node;
        node.right = grandChild;
        // 更新节点高度
        UpdateHeight(node);
        UpdateHeight(child);
        // 返回旋转后子树的根节点
        return child;
    }

    /* 执行旋转操作，使该子树重新恢复平衡 */

    public static TreeNode? Rotate(this TreeNode? node)
    {
        // 获取节点 node 的平衡因子
        int balanceFactorInt = BalanceFactor(node);
        // 左偏树
        if (balanceFactorInt > 1)
        {
            if (BalanceFactor(node?.left) >= 0)
            {
                // 右旋
                return RightRotate(node);
            }
            else
            {
                // 先左旋后右旋
                node!.left = LeftRotate(node!.left);
                return RightRotate(node);
            }
        }
        // 右偏树
        if (balanceFactorInt < -1)
        {
            if (BalanceFactor(node?.right) <= 0)
            {
                // 左旋
                return LeftRotate(node);
            }
            else
            {
                // 先右旋后左旋
                node!.right = RightRotate(node!.right);
                return LeftRotate(node);
            }
        }
        // 平衡树，无须旋转，直接返回
        return node;
    }

    /* 递归插入节点（辅助方法） */

    public static TreeNode? Add(this TreeNode? node, int val)
    {
        if (node == null) return new TreeNode(val);
        /* 1. 查找插入位置并插入节点 */
        if (val < node.val)
            node.left = Add(node.left, val);
        else if (val > node.val)
            node.right = Add(node.right, val);
        else
            return node;     // 重复节点不插入，直接返回
        UpdateHeight(node);  // 更新节点高度
        /* 2. 执行旋转操作，使该子树重新恢复平衡 */
        node = Rotate(node);
        // 返回子树的根节点
        return node;
    }

    /* 递归删除节点（辅助方法） */

    public static TreeNode? Delete(this TreeNode? node, int val)
    {
        if (node == null) return null;
        /* 1. 查找节点并删除 */
        if (val < node.val)
            node.left = Delete(node.left, val);
        else if (val > node.val)
            node.right = Delete(node.right, val);
        else
        {
            if (node.left == null || node.right == null)
            {
                TreeNode? child = node.left ?? node.right;
                // 子节点数量 = 0 ，直接删除 node 并返回
                if (child == null)
                    return null;
                // 子节点数量 = 1 ，直接删除 node
                else
                    node = child;
            }
            else
            {
                // 子节点数量 = 2 ，则将中序遍历的下个节点删除，并用该节点替换当前节点
                TreeNode? temp = node.right;
                while (temp.left != null)
                {
                    temp = temp.left;
                }
                node.right = Delete(node.right, temp.val!.Value);
                node.val = temp.val;
            }
        }
        UpdateHeight(node);  // 更新节点高度
        /* 2. 执行旋转操作，使该子树重新恢复平衡 */
        node = Rotate(node);
        // 返回子树的根节点
        return node;
    }
}