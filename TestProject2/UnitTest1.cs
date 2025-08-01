using Furion.RemoteRequest.Extensions;
using FurionTest.Application.Interfaces;
using FurionTest.Core;
using Xunit.Abstractions;

namespace TestProject2;

public class UnitTest1
{
    private readonly ITestOutputHelper Output;
    private ITestService _testService;
    private ICalcService _calcService;

    public UnitTest1(
    ITestOutputHelper tempOutput
    , ICalcService calcService
    , ITestService testService
     )
    {
        Output = tempOutput;
        _calcService = calcService;

        _testService = testService;
    }

    [Fact]
    public void Test1()
    {
        Assert.Equal(2, 1 + 1);
    }

    [Fact]
    public async Task 测试请求百度()
    {
        var rep = await "https://www.baidu.com".GetAsync();
        Assert.True(rep.IsSuccessStatusCode);
    }

    [Theory]
    [InlineData(1, 2)]
    [InlineData(3, 4)]
    [InlineData(5, 7)]
    public void 带参数测试(int i, int j)
    {
        Assert.NotEqual(0, (i + j) % 2);
    }

    [Fact]
    public void 测试两个数的和()
    {
        Assert.Equal(3, _calcService.Plus(1, 2));
    }

    [Fact]
    public void Test_String_Equal()
    {
        Output.WriteLine("哈哈哈哈，我是 Furion");
        Assert.NotEqual("Furion", "Fur");
    }

    public void Dispose()
    {
        // 释放你的对象
    }

    [Theory]
    [InlineData(false, 50)]
    [InlineData(true, 50)]
    public void Test_CreateTable(bool Backup = false, int StringDefaultLength = 50, params Type[] types)
    {
        var db = new AppDbContext(GlobalConfig.ConnectionConfigs[0].ConnectionString);
        db.CreateTable(Backup, StringDefaultLength, types);
    }

    [Fact]
    public void 测试SayHello方法()
    {
        var result = _testService.SayHello("世界");
        Output.WriteLine($"SayHello返回: {result}");
        Assert.False(string.IsNullOrWhiteSpace(result));
    }

    [Theory]
    [InlineData("1")]
    public void 测试GetOrder方法(string id)
    {
        var order = _testService.GetOrder(id);
        Output.WriteLine($"GetOrder返回: Id={order?.Id}, CustomName={order?.CustomName}");
        Assert.NotNull(order);
        Assert.Equal(id, order.Id);
    }

    [Fact]
    public void 测试QueryAllList方法()
    {
        Exception ex = Record.Exception(() => _testService.QueryAllList());
        Assert.Null(ex);
    }

    [Fact]
    public void 测试InsertStudent方法()
    {
        Exception ex = Record.Exception(() => _testService.InsertStudent());
        Assert.Null(ex);
    }

    [Fact]
    public void 测试UpdateStudent方法()
    {
        Exception ex = Record.Exception(() => _testService.UpdateStudent());
        Assert.Null(ex);
    }

    [Fact]
    public void 测试DeleteStudent方法()
    {
        Exception ex = Record.Exception(() => _testService.DeleteStudent());
        Assert.Null(ex);
    }

    [Fact]
    public void 测试GetOrderList方法()
    {
        var list = _testService.GetOrderList();
        Output.WriteLine($"GetOrderList返回数量: {list?.Count}");
        Assert.NotNull(list);
        Assert.True(list.Count >= 0);
    }

    [Fact]
    public void 测试BulkList方法()
    {
        var result = _testService.BulkList();
        Output.WriteLine($"BulkList返回: {result}");
        Assert.IsType<bool>(result);
    }

    [Fact]
    public void 测试ValidateData方法()
    {
        var result = _testService.ValidateData();
        Output.WriteLine($"ValidateData返回: {result}");
        Assert.IsType<bool>(result);
    }
}