namespace TestProject2;

/// <summary>
/// 测试 HashMapOpenAddressing 类的单元测试类。
/// </summary>
public class HashMapOpenAddressingTests
{
    /// <summary>
    /// 测试构造函数，验证初始化后的哈希表是否为空。
    /// </summary>
    [Fact]
    public void Test_Constructor()
    {
        var hashMap = new HashMapOpenAddressing();
        Assert.Null(hashMap.Get(1));
    }

    /// <summary>
    /// 测试 Put 方法，验证添加键值对的功能。
    /// </summary>
    [Fact]
    public void Test_Put()
    {
        var hashMap = new HashMapOpenAddressing();
        hashMap.Put(1, "value1");
        hashMap.Put(2, "value2");
        Assert.Equal("value1", hashMap.Get(1));
        Assert.Equal("value2", hashMap.Get(2));
    }

    /// <summary>
    /// 测试 Put 方法，验证更新已有键值对的功能。
    /// </summary>
    [Fact]
    public void Test_Put_Update()
    {
        var hashMap = new HashMapOpenAddressing();
        hashMap.Put(1, "value1");
        hashMap.Put(1, "value1_updated");
        Assert.Equal("value1_updated", hashMap.Get(1));
    }

    /// <summary>
    /// 测试 Get 方法，验证获取键值对的功能。
    /// </summary>
    [Fact]
    public void Test_Get()
    {
        var hashMap = new HashMapOpenAddressing();
        hashMap.Put(1, "value1");
        Assert.Equal("value1", hashMap.Get(1));
        Assert.Null(hashMap.Get(2));
    }

    /// <summary>
    /// 测试 Remove 方法，验证删除键值对的功能。
    /// </summary>
    [Fact]
    public void Test_Remove()
    {
        var hashMap = new HashMapOpenAddressing();
        hashMap.Put(1, "value1");
        hashMap.Remove(1);
        Assert.Null(hashMap.Get(1));
    }

    /// <summary>
    /// 测试扩容功能，验证在负载因子超过阈值时是否正确扩容。
    /// </summary>
    [Fact]
    public void Test_Extend()
    {
        var hashMap = new HashMapOpenAddressing();
        hashMap.Put(1, "value1");
        hashMap.Put(2, "value2");
        hashMap.Put(3, "value3");
        hashMap.Put(4, "value4"); // 触发扩容
        Assert.Equal("value1", hashMap.Get(1));
        Assert.Equal("value2", hashMap.Get(2));
        Assert.Equal("value3", hashMap.Get(3));
        Assert.Equal("value4", hashMap.Get(4));
    }

    /// <summary>
    /// 测试哈希冲突的处理，验证开放寻址是否正确。
    /// </summary>
    [Fact]
    public void Test_HashCollision()
    {
        var hashMap = new HashMapOpenAddressing();
        hashMap.Put(1, "value1");
        hashMap.Put(5, "value5"); // 假设哈希冲突
        Assert.Equal("value1", hashMap.Get(1));
        Assert.Equal("value5", hashMap.Get(5));
    }

    /// <summary>
    /// 测试 Print 方法，验证打印哈希表的功能。
    /// </summary>
    [Fact]
    public void Test_Print()
    {
        var hashMap = new HashMapOpenAddressing();
        hashMap.Put(1, "value1");
        hashMap.Put(2, "value2");

        using var writer = new StringWriter();
        Console.SetOut(writer);

        hashMap.Print();

        var output = writer.ToString().Trim();
        Assert.Contains("1 -> value1", output);
        Assert.Contains("2 -> value2", output);
    }
}