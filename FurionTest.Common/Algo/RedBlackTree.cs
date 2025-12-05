public class RedBlackTree
{
    private TreeNode? root;
    private readonly TreeNode nil = new TreeNode(0, NodeColor.Black); // 哨兵节点

    // 插入操作
    public void Insert(int val)
    {
        TreeNode? node = root;
        TreeNode? parent = null;

        // 查找插入位置
        while (node != null && node != nil)
        {
            parent = node;
            if (val < node.val)
                node = node.left;
            else if (val > node.val)
                node = node.right;
            else
                return; // 重复值不插入
        }

        // 创建新节点
        TreeNode newNode = new TreeNode(val)
        {
            left = nil,
            right = nil,
            parent = parent,
            color = NodeColor.Red
        };

        // 插入新节点
        if (parent == null)
        {
            root = newNode;
        }
        else if (val < parent.val)
        {
            parent.left = newNode;
        }
        else
        {
            parent.right = newNode;
        }

        // 修复红黑树性质
        FixInsert(newNode);
    }

    // 删除操作
    public void Delete(int val)
    {
        TreeNode? node = Search(val);
        if (node == null || node == nil) return;

        TreeNode? toDelete = node;
        NodeColor originalColor = toDelete.color;
        TreeNode? replacement;

        if (node.left == nil)
        {
            replacement = node.right;
            Transplant(node, node.right);
        }
        else if (node.right == nil)
        {
            replacement = node.left;
            Transplant(node, node.left);
        }
        else
        {
            toDelete = Minimum(node.right);
            originalColor = toDelete.color;
            replacement = toDelete.right;

            if (toDelete.parent == node)
            {
                replacement.parent = toDelete;
            }
            else
            {
                Transplant(toDelete, toDelete.right);
                toDelete.right = node.right;
                toDelete.right.parent = toDelete;
            }

            Transplant(node, toDelete);
            toDelete.left = node.left;
            toDelete.left.parent = toDelete;
            toDelete.color = node.color;
        }

        if (originalColor == NodeColor.Black)
        {
            FixDelete(replacement);
        }
    }

    // 搜索操作
    public TreeNode? Search(int val)
    {
        TreeNode? node = root;
        while (node != null && node != nil)
        {
            if (val == node.val) return node;
            node = val < node.val ? node.left : node.right;
        }
        return null;
    }

    // 辅助方法：获取最小值节点
    private TreeNode Minimum(TreeNode node)
    {
        while (node.left != nil)
        {
            node = node.left;
        }
        return node;
    }

    // 辅助方法：节点替换
    private void Transplant(TreeNode? u, TreeNode? v)
    {
        if (u == null) return;

        if (u.parent == null)
        {
            root = v;
        }
        else if (u == u.parent.left)
        {
            u.parent.left = v;
        }
        else
        {
            u.parent.right = v;
        }

        if (v != null) v.parent = u.parent;
    }

    // 插入后修复红黑树性质
    private void FixInsert(TreeNode node)
    {
        while (node.parent != null && node.parent.color == NodeColor.Red)
        {
            TreeNode? parent = node.parent;
            TreeNode? grandParent = parent.parent;
            TreeNode? uncle = null;

            if (parent == grandParent?.left)
            {
                uncle = grandParent?.right;
                if (uncle != null && uncle.color == NodeColor.Red)
                {
                    // 情况1：叔叔节点是红色
                    parent.color = NodeColor.Black;
                    uncle.color = NodeColor.Black;
                    grandParent.color = NodeColor.Red;
                    node = grandParent;
                }
                else
                {
                    // 情况2和3：叔叔节点是黑色
                    if (node == parent.right)
                    {
                        node = parent;
                        LeftRotate(node);
                    }
                    parent.color = NodeColor.Black;
                    if (grandParent != null)
                    {
                        grandParent.color = NodeColor.Red;
                        RightRotate(grandParent);
                    }
                }
            }
            else
            {
                // 对称情况
                uncle = grandParent?.left;
                if (uncle != null && uncle.color == NodeColor.Red)
                {
                    parent.color = NodeColor.Black;
                    uncle.color = NodeColor.Black;
                    grandParent.color = NodeColor.Red;
                    node = grandParent;
                }
                else
                {
                    if (node == parent.left)
                    {
                        node = parent;
                        RightRotate(node);
                    }
                    parent.color = NodeColor.Black;
                    if (grandParent != null)
                    {
                        grandParent.color = NodeColor.Red;
                        LeftRotate(grandParent);
                    }
                }
            }
        }
        if (root != null) root.color = NodeColor.Black;
    }

    // 删除后修复红黑树性质
    private void FixDelete(TreeNode? node)
    {
        while (node != root && node?.color == NodeColor.Black)
        {
            if (node == node.parent?.left)
            {
                TreeNode? sibling = node.parent.right;
                if (sibling?.color == NodeColor.Red)
                {
                    // 情况1：兄弟节点是红色
                    sibling.color = NodeColor.Black;
                    node.parent.color = NodeColor.Red;
                    LeftRotate(node.parent);
                    sibling = node.parent?.right;
                }
                if (sibling != null && sibling.left?.color == NodeColor.Black && sibling.right?.color == NodeColor.Black)
                {
                    // 情况2：兄弟节点的两个子节点都是黑色
                    sibling.color = NodeColor.Red;
                    node = node.parent;
                }
                else
                {
                    if (sibling != null && sibling.right?.color == NodeColor.Black)
                    {
                        // 情况3：兄弟节点的右子节点是黑色
                        if (sibling.left != null) sibling.left.color = NodeColor.Black;
                        sibling.color = NodeColor.Red;
                        RightRotate(sibling);
                        sibling = node.parent?.right;
                    }
                    // 情况4
                    if (sibling != null)
                    {
                        sibling.color = node.parent?.color ?? NodeColor.Black;
                        if (sibling.right != null) sibling.right.color = NodeColor.Black;
                    }
                    if (node.parent != null) node.parent.color = NodeColor.Black;
                    LeftRotate(node.parent);
                    node = root;
                }
            }
            else
            {
                // 对称情况
                TreeNode? sibling = node.parent?.left;
                if (sibling?.color == NodeColor.Red)
                {
                    sibling.color = NodeColor.Black;
                    if (node.parent != null) node.parent.color = NodeColor.Red;
                    RightRotate(node.parent);
                    sibling = node.parent?.left;
                }
                if (sibling != null && sibling.right?.color == NodeColor.Black && sibling.left?.color == NodeColor.Black)
                {
                    sibling.color = NodeColor.Red;
                    node = node.parent;
                }
                else
                {
                    if (sibling != null && sibling.left?.color == NodeColor.Black)
                    {
                        if (sibling.right != null) sibling.right.color = NodeColor.Black;
                        sibling.color = NodeColor.Red;
                        LeftRotate(sibling);
                        sibling = node.parent?.left;
                    }
                    if (sibling != null)
                    {
                        sibling.color = node.parent?.color ?? NodeColor.Black;
                        if (sibling.left != null) sibling.left.color = NodeColor.Black;
                    }
                    if (node.parent != null) node.parent.color = NodeColor.Black;
                    RightRotate(node.parent);
                    node = root;
                }
            }
        }
        if (node != null) node.color = NodeColor.Black;
    }

    // 左旋操作
    private void LeftRotate(TreeNode? node)
    {
        /*左旋之前的节点结构：
         
          node                                  
         /    \
        A    rightChild
              /     \
        grandChild    B

          parent
            /
          node
         /    \
        A    rightChild
              /     \
        grandChild    B

         */
        if (node == null || node.right == nil) return;

        TreeNode? rightChild = node?.right;
        TreeNode? grandChild = rightChild?.left;
        // 以 rightChild 为原点，将 node 向左旋转
        if (grandChild != nil)
            grandChild.parent = node;
        node.right = grandChild;

        rightChild.parent = node.parent;

        if (node.parent == null)
            root = rightChild;
        else if (node == node.parent.left)
            node.parent.left = rightChild;
        else
            node.parent.right = rightChild;

        rightChild.left = node;
        node.parent = rightChild;

        /*左旋之后节点结构：

              rightChild
               /     \
            node       B
           /   \        
          A    grandChild

                parent
                  /
              rightChild
               /     \
            node       B
           /   \        
          A    grandChild

         */
    }

    // 右旋操作
    private void RightRotate(TreeNode? node)
    {
        if (node == null || node.left == nil) return;

        TreeNode leftChild = node.left;
        node.left = leftChild.right;

        if (leftChild.right != nil)
            leftChild.right.parent = node;

        leftChild.parent = node.parent;

        if (node.parent == null)
            root = leftChild;
        else if (node == node.parent.right)
            node.parent.right = leftChild;
        else
            node.parent.left = leftChild;

        leftChild.right = node;
        node.parent = leftChild;
    }

    // 层序遍历（用于测试）
    public List<int> LevelOrder()
    {
        return root?.LevelOrder();
    }
}