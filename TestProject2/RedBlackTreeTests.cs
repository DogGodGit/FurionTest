namespace TestProject2;

public class RedBlackTreeTests
{
    private RedBlackTree tree;

    public RedBlackTreeTests()
    {
        tree = new RedBlackTree();
    }

    // 测试正常插入
    [Fact]
    public void TestInsert_HappyPath()
    {
        tree.Insert(10);
        tree.Insert(20);
        tree.Insert(30);
        tree.Insert(15);
        tree.Insert(25);
        tree.Insert(35);

        // 层序遍历检查插入结果
        List<int> result = tree.LevelOrder();
        Assert.Equal([20, 10, 30, 15, 25, 35], result);
    }

    // 测试插入重复值
    [Fact]
    public void TestInsert_DuplicateValue()
    {
        tree.Insert(10);
        tree.Insert(10);
        tree.Insert(10);

        // 层序遍历检查插入结果
        List<int> result = tree.LevelOrder();
        Assert.Equal(new List<int> { 10 }, result);
    }

    // 测试删除叶子节点
    [Fact]
    public void TestDelete_LeafNode()
    {
        tree.Insert(10);
        tree.Insert(20);
        tree.Delete(20);

        // 层序遍历检查删除结果
        List<int> result = tree.LevelOrder();
        Assert.Equal([10], result);
    }

    // 测试删除只有一个子节点的节点
    [Fact]
    public void TestDelete_OneChildNode()
    {
        tree.Insert(10);
        tree.Insert(20);
        tree.Insert(30);
        tree.Delete(20);

        // 层序遍历检查删除结果
        List<int> result = tree.LevelOrder();
        Assert.Equal([30, 10], result);
    }

    // 测试删除有两个子节点的节点
    [Fact]
    public void TestDelete_TwoChildrenNode()
    {
        tree.Insert(10);
        tree.Insert(20);
        tree.Insert(30);
        tree.Insert(15);
        tree.Insert(25);
        tree.Insert(35);
        tree.Delete(20);

        // 层序遍历检查删除结果
        List<int> result = tree.LevelOrder();
        Assert.Equal([25, 10, 30, 15, 35], result);
    }

    // 测试删除根节点
    [Fact]
    public void TestDelete_RootNode()
    {
        tree.Insert(20);
        tree.Insert(10);
        tree.Insert(30);
        tree.Delete(20);

        // 层序遍历检查删除结果
        List<int> result = tree.LevelOrder();
        Assert.Equal([30, 10], result);
    }

    // 测试删除空树
    [Fact]
    public void TestDelete_EmptyTree()
    {
        tree.Delete(10);

        // 层序遍历检查删除结果
        List<int> result = tree.LevelOrder();
        Assert.Equal(new List<int>(), result);
    }

    // 测试搜索存在的值
    [Fact]
    public void TestSearch_ValueExists()
    {
        tree.Insert(10);
        tree.Insert(20);
        TreeNode? node = tree.Search(20);
        Assert.NotNull(node);
        Assert.Equal(20, node.val);
    }

    // 测试搜索不存在的值
    [Fact]
    public void TestSearch_ValueNotExists()
    {
        tree.Insert(10);
        tree.Insert(20);
        TreeNode? node = tree.Search(30);
        Assert.Null(node);
    }

    // 测试搜索空树
    [Fact]
    public void TestSearch_EmptyTree()
    {
        TreeNode? node = tree.Search(10);
        Assert.Null(node);
    }
}
