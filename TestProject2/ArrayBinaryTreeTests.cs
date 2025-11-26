namespace TestProject2;

/// <summary>
/// 测试 ArrayBinaryTree 类的单元测试类。
/// </summary>
public class ArrayBinaryTreeTests
{
    /// <summary>
    /// 测试构造函数，验证树的初始化。
    /// </summary>
    [Fact]
    public void Test_Constructor()
    {
        var tree = new ArrayBinaryTree(new List<int?> { 1, 2, 3 });
        Assert.Equal(3, tree.Size());
        Assert.Equal(1, tree.Val(0));
        Assert.Equal(2, tree.Val(1));
        Assert.Equal(3, tree.Val(2));
    }

    /// <summary>
    /// 测试 Val 方法，验证索引越界时返回 null。
    /// </summary>
    [Fact]
    public void Test_Val_OutOfBounds()
    {
        var tree = new ArrayBinaryTree(new List<int?> { 1, 2, 3 });
        Assert.Null(tree.Val(-1));
        Assert.Null(tree.Val(3));
    }

    /// <summary>
    /// 测试 Left 和 Right 方法，验证子节点索引计算。
    /// </summary>
    [Fact]
    public void Test_LeftAndRight()
    {
        var tree = new ArrayBinaryTree(new List<int?> { 1, 2, 3 });
        Assert.Equal(1, tree.Left(0));
        Assert.Equal(2, tree.Right(0));
    }

    /// <summary>
    /// 测试 Parent 方法，验证父节点索引计算。
    /// </summary>
    [Fact]
    public void Test_Parent()
    {
        var tree = new ArrayBinaryTree(new List<int?> { 1, 2, 3 });
        Assert.Equal(0, tree.Parent(1));
        Assert.Equal(0, tree.Parent(2));
    }

    /// <summary>
    /// 测试 LevelOrder 方法，验证层序遍历结果。
    /// </summary>
    [Fact]
    public void Test_LevelOrder()
    {
        var tree = new ArrayBinaryTree(new List<int?> { 1, 2, 3, null, 5 });
        var result = tree.LevelOrder();
        Assert.Equal(new List<int> { 1, 2, 3, 5 }, result);
    }

    /// <summary>
    /// 测试 PreOrder 方法，验证前序遍历结果。
    /// </summary>
    [Fact]
    public void Test_PreOrder()
    {
        var tree = new ArrayBinaryTree(new List<int?> { 1, 2, 3, null, 5 });
        var result = tree.PreOrder();
        Assert.Equal(new List<int> { 1, 2, 5, 3 }, result);
    }

    /// <summary>
    /// 测试 InOrder 方法，验证中序遍历结果。
    /// </summary>
    [Fact]
    public void Test_InOrder()
    {
        var tree = new ArrayBinaryTree(new List<int?> { 1, 2, 3, null, 5 });
        var result = tree.InOrder();
        Assert.Equal(new List<int> { 2, 5, 1, 3 }, result);
    }

    /// <summary>
    /// 测试 PostOrder 方法，验证后序遍历结果。
    /// </summary>
    [Fact]
    public void Test_PostOrder()
    {
        var tree = new ArrayBinaryTree(new List<int?> { 1, 2, 3, null, 5 });
        var result = tree.PostOrder();
        Assert.Equal(new List<int> { 5, 2, 3, 1 }, result);
    }
}