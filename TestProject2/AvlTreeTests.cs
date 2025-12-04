namespace TestProject2;
public class AvlTreeTests
{
    private AvlTree _avlTree;

    public AvlTreeTests()
    {
        _avlTree = new AvlTree();
    }

    // 测试用例：插入单个节点
    [Fact]
    public void TestAddSingleNode()
    {
        _avlTree.Add(10);
        Assert.Equal(10, _avlTree.root?.val);
        Assert.Equal(0, _avlTree.Height(_avlTree.root));
    }

    // 测试用例：插入多个节点，确保树保持平衡
    [Fact]
    public void TestAddMultipleNodes()
    {
        _avlTree.Add(10);
        _avlTree.Add(20);
        _avlTree.Add(30);
        _avlTree.Add(40);
        _avlTree.Add(50);
        _avlTree.Add(25);

        // 根节点应为 30
        Assert.Equal(30, _avlTree.root?.val);
        // 树的高度应为 2
        Assert.Equal(2, _avlTree.Height(_avlTree.root));
    }

    // 测试用例：删除单个节点
    [Fact]
    public void TestDeleteSingleNode()
    {
        _avlTree.Add(10);
        _avlTree.Delete(10);
        Assert.Null(_avlTree.root);
    }

    // 测试用例：删除叶子节点，确保树保持平衡
    [Fact]
    public void TestDeleteLeafNode()
    {
        _avlTree.Add(10);
        _avlTree.Add(5);
        _avlTree.Add(15);
        _avlTree.Delete(5);

        // 根节点应为 10
        Assert.Equal(10, _avlTree.root?.val);
        // 树的高度应为 1
        Assert.Equal(1, _avlTree.Height(_avlTree.root));
    }

    // 测试用例：删除具有单个子节点的节点
    [Fact]
    public void TestDeleteNodeWithSingleChild()
    {
        _avlTree.Add(10);
        _avlTree.Add(5);
        _avlTree.Add(15);
        _avlTree.Add(12);
        _avlTree.Delete(15);

        // 根节点应为 10
        Assert.Equal(10, _avlTree.root?.val);
        // 树的高度应为 1
        Assert.Equal(1, _avlTree.Height(_avlTree.root));
    }

    // 测试用例：删除具有两个子节点的节点
    [Fact]
    public void TestDeleteNodeWithTwoChildren()
    {
        _avlTree.Add(10);
        _avlTree.Add(5);
        _avlTree.Add(15);
        _avlTree.Add(12);
        _avlTree.Add(20);
        _avlTree.Delete(10);

        // 根节点应为 12
        Assert.Equal(12, _avlTree.root?.val);
        // 树的高度应为 2
        Assert.Equal(2, _avlTree.Height(_avlTree.root));
    }

    // 测试用例：插入重复节点
    [Fact]
    public void TestAddDuplicateNode()
    {
        _avlTree.Add(10);
        _avlTree.Add(10);
        _avlTree.Add(20);

        // 根节点应为 10
        Assert.Equal(10, _avlTree.root?.val);
        // 树的高度应为 1
        Assert.Equal(1, _avlTree.Height(_avlTree.root));
    }

    // 测试用例：删除不存在的节点
    [Fact]
    public void TestDeleteNonExistentNode()
    {
        _avlTree.Add(10);
        _avlTree.Delete(20);
        Assert.Equal(10, _avlTree.root?.val);
        Assert.Equal(0, _avlTree.Height(_avlTree.root));
    }
}
