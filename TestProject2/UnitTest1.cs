using Furion.RemoteRequest.Extensions;
using FurionTest.Application.Interfaces;
using FurionTest.Core;
using Xunit.Abstractions;

namespace TestProject2;

public class UnitTest1
{
    private readonly ITestOutputHelper Output;
    private ICalcService _calcService;

    public UnitTest1(
    ITestOutputHelper tempOutput
    , ICalcService calcService
     )
    {
        Output = tempOutput;
        _calcService = calcService;
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
    [InlineData(5, 8)]
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

    [Fact]
    public void Test_PINQ()
    {
        var list = new List<int>() { 12, 21, 13, 31, 14, 41, 15, 51, 16, 61 };
        var result = list.AsParallel().Where(x => x > 30);
        foreach (var item in result)
        {
            Output.WriteLine(item.ToString());
        }
    }

    [Theory]
    [InlineData(false, 50)]
    [InlineData(true, 50)]
    public void Test_CreateTable(bool Backup = false, int StringDefaultLength = 50, params Type[] types)
    {
        var db = new AppDbContext(GlobalConfig.ConnectionConfigs[0]);
        db.CreateTable(Backup, StringDefaultLength, types);
    }
}