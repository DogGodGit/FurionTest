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
    }

    [Fact]
    public void Test_LevelOrder()
    {
        TreeNode root = new(1)
        {
            left = new(2)
            {
                left = new(4),
                right = new(5)
            },
            right = new(3)
            {
                left = new(6),
                right = new(7)
            }
        };

        var result = root.LevelOrder();
        Assert.Equal([1, 2, 3, 4, 5, 6, 7], result);
    }

    [Fact]
    public void Test_PreOrder()
    {
        TreeNode root = new(1)
        {
            left = new(2)
            {
                left = new(4),
                right = new(5)
            },
            right = new(3)
            {
                left = new(6),
                right = new(7)
            }
        };

        var result = root.PreOrder();
        Assert.Equal([1, 2, 4, 5, 3, 6, 7], result);
    }

    [Fact]
    public void Test_InOrder()
    {
        TreeNode root = new(1)
        {
            left = new(2)
            {
                left = new(4),
                right = new(5)
            },
            right = new(3)
            {
                left = new(6),
                right = new(7)
            }
        };

        var result = root.InOrder();
        Assert.Equal(new List<int> { 4, 2, 5, 1, 6, 3, 7 }, result);
    }

    [Fact]
    public void Test_PostOrder()
    {
        TreeNode root = new(1)
        {
            left = new(2)
            {
                left = new(4),
                right = new(5)
            },
            right = new(3)
            {
                left = new(6),
                right = new(7)
            }
        };

        var result = root.PostOrder();
        Assert.Equal(new List<int> { 4, 5, 2, 6, 7, 3, 1 }, result);
    }

    [Fact]
    public void Test_Insert_And_Search()
    {
        TreeNode? root = new(8);

        root.Insert(4);
        root.Insert(12);
        root.Insert(2);
        root.Insert(6);
        root.Insert(10);
        root.Insert(14);
        root.Insert(1);
        root.Insert(3);
        root.Insert(5);
        root.Insert(7);
        root.Insert(9);
        root.Insert(11);
        root.Insert(13);
        root.Insert(15);

        var result = root.LevelOrder();
        Assert.Equal([8, 4, 12, 2, 6, 10, 14, 1, 3, 5, 7, 9, 11, 13, 15], result);

        var searchResult = root.Search(6);
        Assert.NotNull(searchResult);
        Assert.Equal(6, searchResult.val);
    }

    [Fact]
    public void Test_Remove()
    {
        TreeNode? root = new(8);

        root.Insert(4);
        root.Insert(12);
        root.Insert(2);
        root.Insert(6);
        root.Insert(10);
        root.Insert(14);
        root.Insert(1);
        root.Insert(3);
        root.Insert(5);
        root.Insert(7);
        root.Insert(9);
        root.Insert(11);
        root.Insert(13);
        root.Insert(15);

        root.Remove(1);
        var result = root.LevelOrder();
        Assert.Equal([8, 4, 12, 2, 6, 10, 14, 3, 5, 7, 9, 11, 13, 15], result);

        root.Remove(2);
        result = root.LevelOrder();
        Assert.Equal([8, 4, 12, 3, 6, 10, 14, 5, 7, 9, 11, 13, 15], result);

        root.Remove(4);
        result = root.LevelOrder();
        Assert.Equal([8, 5, 12, 3, 6, 10, 14, 7, 9, 11, 13, 15], result);
    }
}