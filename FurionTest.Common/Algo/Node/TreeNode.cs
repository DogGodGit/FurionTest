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

/* 红黑树节点类 */
public enum NodeColor
{
    Red,
    Black
}

public partial class TreeNode
{
    public TreeNode? parent;
    public NodeColor color = NodeColor.Red;

    public TreeNode(int? x, NodeColor color) : this(x)
    {
        this.color = color;
    }
}