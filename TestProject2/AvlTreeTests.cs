namespace TestProject2;

public class AvlTreeTests
{
    // 测试用例：插入节点后的高度更新
    [Fact]
    public void TestInsertHelper_更新高度()
    {
        // 创建一个空的 AVL 树节点
        TreeNode? root = null;
        // 插入一个节点
        root = root.Add(10);
        // 根节点的高度应为 0
        Assert.Equal(0, root.Height());
    }

    // 测试用例：插入节点后的右旋操作
    [Fact]
    public void TestInsertHelper_右旋()
    {
        // 创建一个 AVL 树节点
        TreeNode root = new TreeNode(20);
        // 插入节点使其成为左偏树
        root = root.Add(10);
        root = root.Add(5);
        // 插入后应执行右旋操作，根节点的值应为 10
        Assert.Equal(10, root.val);
    }

    // 测试用例：插入节点后的左旋操作
    [Fact]
    public void TestInsertHelper_左旋()
    {
        // 创建一个 AVL 树节点
        TreeNode root = new TreeNode(10);
        // 插入节点使其成为右偏树
        root = root.Add(20);
        root = root.Add(30);
        // 插入后应执行左旋操作，根节点的值应为 20
        Assert.Equal(20, root.val);
    }

    // 测试用例：插入节点后的先左旋后右旋操作
    [Fact]
    public void TestInsertHelper_先左旋后右旋()
    {
        // 创建一个 AVL 树节点
        TreeNode root = new TreeNode(30);
        // 插入节点使其成为左偏树但需要先左旋后右旋
        root = root.Add(10);
        root = root.Add(20);
        // 插入后应执行先左旋后右旋操作，根节点的值应为 20
        Assert.Equal(20, root.val);
    }

    // 测试用例：插入节点后的先右旋后左旋操作
    [Fact]
    public void TestInsertHelper_先右旋后左旋()
    {
        // 创建一个 AVL 树节点
        TreeNode root = new TreeNode(10);
        // 插入节点使其成为右偏树但需要先右旋后左旋
        root = root.Add(30);
        root = root.Add(20);
        // 插入后应执行先右旋后左旋操作，根节点的值应为 20
        Assert.Equal(20, root.val);
    }

    // 测试用例：删除节点后的高度更新
    [Fact]
    public void TestRemoveHelper_更新高度()
    {
        // 创建一个 AVL 树节点
        TreeNode root = new(20)
        {
            left = new TreeNode(10),
            right = new TreeNode(30),
            height = 1
        };
        // 删除一个节点
        root = root.Delete(10);
        // 删除后根节点的高度应为 1
        Assert.Equal(1, root.Height());
    }

    // 测试用例：删除节点后的右旋操作
    [Fact]
    public void TestRemoveHelper_右旋()
    {
        // 创建一个 AVL 树节点
        TreeNode root = new(30)
        {
            left = new TreeNode(20)
            {
                left = new TreeNode(10)
            }
        };

        root.left.UpdateHeight();
        root.UpdateHeight();

        // 删除一个节点使其成为右偏树
        root = root.Delete(30);
        // 删除后应执行右旋操作，根节点的值应为 20
        Assert.Equal(20, root.val);
    }

    // 测试用例：删除节点后的左旋操作
    [Fact]
    public void TestRemoveHelper_左旋()
    {
        // 创建一个 AVL 树节点
        TreeNode root = new TreeNode(10)
        {
            right = new TreeNode(20)
            {
                right = new TreeNode(30)
            }
        };
        root.UpdateHeight();
        root.right.UpdateHeight();
        // 删除一个节点使其成为左偏树
        root = root.Delete(10);
        // 删除后应执行左旋操作，根节点的值应为 20
        Assert.Equal(20, root.val);
    }

    // 测试用例：删除节点后的先左旋后右旋操作
    [Fact]
    public void TestRemoveHelper_先左旋后右旋()
    {
        // 创建一个 AVL 树节点
        TreeNode root = new TreeNode(30)
        {
            left = new TreeNode(10)
            {
                right = new TreeNode(20)
            }
        };
        root.UpdateHeight();
        root.left.UpdateHeight();
        // 删除一个节点使其成为左偏树但需要先左旋后右旋
        root = root.Delete(30);
        // 删除后应执行先左旋后右旋操作，根节点的值应为 10
        Assert.Equal(10, root.val);
    }

    // 测试用例：删除节点后的先右旋后左旋操作
    [Fact]
    public void TestRemoveHelper_先右旋后左旋()
    {
        // 创建一个 AVL 树节点
        TreeNode root = new TreeNode(10)
        {
            right = new TreeNode(30)
            {
                left = new TreeNode(20)
            }
        };
        root.UpdateHeight();
        root.right.UpdateHeight();
        // 删除一个节点使其成为右偏树但需要先右旋后左旋
        root = root.Delete(10);
        // 删除后应执行先右旋后左旋操作，根节点的值应为 30
        Assert.Equal(30, root.val);
    }

    // 测试用例：删除不存在的节点
    [Fact]
    public void TestRemoveHelper_删除不存在的节点()
    {
        // 创建一个 AVL 树节点
        TreeNode root = new TreeNode(20);
        root.left = new TreeNode(10);
        root.right = new TreeNode(30);
        root.UpdateHeight();
        root.left.UpdateHeight();
        root.right.UpdateHeight();
        // 删除一个不存在的节点，树结构应保持不变
        var oldRoot = root;
        root = root.Delete(40);
        Assert.Same(oldRoot, root);
    }

    // 测试用例：删除叶节点
    [Fact]
    public void TestRemoveHelper_删除叶节点()
    {
        // 创建一个 AVL 树节点
        TreeNode root = new TreeNode(20);
        root.left = new TreeNode(10);
        root.right = new TreeNode(30);
        root.UpdateHeight();
        root.left.UpdateHeight();
        root.right.UpdateHeight();
        // 删除一个叶节点，树结构应保持平衡且节点被删除
        root = root.Delete(10);
        Assert.Null(root.left);
    }

    // 测试用例：删除只有一个子节点的节点
    [Fact]
    public void TestRemoveHelper_删除只有一个子节点的节点()
    {
        // 创建一个 AVL 树节点
        TreeNode root = new TreeNode(20)
        {
            left = new TreeNode(10)
        };
        root.left.right = new TreeNode(15);
        root.UpdateHeight();
        root.left.UpdateHeight();
        root.left.right.UpdateHeight();
        // 删除一个只有一个子节点的节点，树结构应保持平衡且节点被删除
        root = root.Delete(10);
        Assert.Equal(15, root.left.val);
    }

    // 测试用例：删除有两个子节点的节点
    [Fact]
    public void TestRemoveHelper_删除有两个子节点的节点()
    {
        // 创建一个 AVL 树节点
        TreeNode root = new TreeNode(20)
        {
            left = new TreeNode(10),
            right = new TreeNode(30)
        };
        root.left.left = new TreeNode(5);
        root.left.right = new TreeNode(15);
        root.right.left = new TreeNode(25);
        root.right.right = new TreeNode(35);
        root.UpdateHeight();
        root.left.UpdateHeight();
        root.left.left.UpdateHeight();
        root.left.right.UpdateHeight();
        root.right.UpdateHeight();
        root.right.left.UpdateHeight();
        root.right.right.UpdateHeight();
        // 删除一个有两个子节点的节点，树结构应保持平衡且节点被替换为中序后继
        root = root.Delete(20);
        Assert.Equal(25, root.val);
    }

    // 测试用例：删除根节点
    [Fact]
    public void TestRemoveHelper_删除根节点()
    {
        // 创建一个 AVL 树节点
        TreeNode root = new TreeNode(20)
        {
            left = new TreeNode(10),
            right = new TreeNode(30)
        };
        root.UpdateHeight();
        root.left.UpdateHeight();
        root.right.UpdateHeight();
        // 删除根节点，树结构应保持平衡且根节点被替换为中序后继
        root = root.Delete(20);
        Assert.Equal(30, root.val);
    }

    // 测试用例：插入重复节点
    [Fact]
    public void TestInsertHelper_插入重复节点()
    {
        // 创建一个 AVL 树节点
        TreeNode root = new TreeNode(20);
        // 插入一个重复节点
        root = root.Add(20);
        // 插入重复节点后，树结构应保持不变
        Assert.Equal(20, root.val);
        Assert.Null(root.left);
        Assert.Null(root.right);
    }


    [Fact]
    public void Test_Insert_And_Search()
    {
        TreeNode? root = new(8);

        root.Add(4);
        root.Add(12);
        root.Add(2);
        root.Add(6);
        root.Add(10);
        root.Add(14);
        root.Add(1);
        root.Add(3);
        root.Add(5);
        root.Add(7);
        root.Add(9);
        root.Add(11);
        root.Add(13);
        root.Add(15);

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

        root.Add(4);
        root.Add(12);
        root.Add(2);
        root.Add(6);
        root.Add(10);
        root.Add(14);
        root.Add(1);
        root.Add(3);
        root.Add(5);
        root.Add(7);
        root.Add(9);
        root.Add(11);
        root.Add(13);
        root.Add(15);

        root.Delete(1);
        var result = root.LevelOrder();
        Assert.Equal([8, 4, 12, 2, 6, 10, 14, 3, 5, 7, 9, 11, 13, 15], result);

        root.Delete(2);
        result = root.LevelOrder();
        Assert.Equal([8, 4, 12, 3, 6, 10, 14, 5, 7, 9, 11, 13, 15], result);

        root.Delete(4);
        result = root.LevelOrder();
        Assert.Equal([8, 5, 12, 3, 6, 10, 14, 7, 9, 11, 13, 15], result);
    }
}
