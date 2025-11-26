using FurionTest.Common.Algo;
using System.Collections;

namespace TestProject2;

/// <summary>
/// 测试 SimpleHash 类的单元测试类。
/// </summary>
public class SimpleHashTests
{
    private readonly SimpleHash _simpleHash;

    public SimpleHashTests()
    {
        _simpleHash = new SimpleHash();
    }

    /// <summary>
    /// 测试 内置 方法。
    /// </summary>
    [Fact]
    public void Test_Built_In_Hash()
    {
        int num = 3;
        int hashNum = num.GetHashCode();
        Assert.Equal(3, hashNum);

        bool bol = true;
        int hashBol = bol.GetHashCode();
        Assert.Equal(1, hashBol);

        double dec = 3.14159;
        int hashDec = dec.GetHashCode();
        Assert.Equal(-1340954729, hashDec);

        string str = "Hello 算法";
        int hashStr = str.GetHashCode();
        // 字符串“Hello 算法”的哈希值为 -586107568;
        //Assert.Equal(-586107568, hashStr);

        object[] arr = [12836, "小哈"];
        int hashTup = arr.GetHashCode();
        // 数组 [12836, 小哈] 的哈希值为 42931033;
        //Assert.Equal(42931033, hashTup);

        ListNode obj = new(0);
        int hashObj = obj.GetHashCode();
        // 节点对象 0 的哈希值为 39053774;
        //Assert.Equal(39053774, hashObj);

        Hashtable hashtable = new Hashtable();
        hashtable.Add("name", "小哈");
        Assert.Equal("小哈", hashtable["name"]);
        hashtable.Remove("name");
        Assert.Empty(hashtable);
    }

    /// <summary>
    /// 测试 AddHash 方法。
    /// </summary>
    [Fact]
    public void Test_AddHash()
    {
        var result = InvokePrivateMethod<int>("AddHash", "test");
        Assert.Equal(448, result); // 计算 "test" 的加法哈希值。
    }

    /// <summary>
    /// 测试 MulHash 方法。
    /// </summary>
    [Fact]
    public void Test_MulHash()
    {
        var result = InvokePrivateMethod<int>("MulHash", "test");
        Assert.Equal(3556498, result); // 计算 "test" 的乘法哈希值。
    }

    /// <summary>
    /// 测试 XorHash 方法。
    /// </summary>
    [Fact]
    public void Test_XorHash()
    {
        var result = InvokePrivateMethod<int>("XorHash", "test");
        Assert.Equal(6, result); // 计算 "test" 的异或哈希值。
    }

    /// <summary>
    /// 测试 RotHash 方法。
    /// </summary>
    [Fact]
    public void Test_RotHash()
    {
        var result = InvokePrivateMethod<int>("RotHash", "test");
        Assert.Equal(467524, result); // 计算 "test" 的旋转哈希值。
    }

    /// <summary>
    /// 使用反射调用 SimpleHash 的私有方法。
    /// </summary>
    private T InvokePrivateMethod<T>(string methodName, string key)
    {
        var method = typeof(SimpleHash).GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        if (method == null)
        {
            throw new MissingMethodException($"方法 {methodName} 未找到。");
        }
        return (T)method.Invoke(_simpleHash, [key]);
    }
}