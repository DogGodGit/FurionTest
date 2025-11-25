using Dm;
using FurionTest.Application.Services;
using FurionTest.Core;
using FurionTest.Core.Models;
using SqlSugar;

namespace TestProject2;

public class TestServiceTests
{
    private readonly TestService _testService;
    private readonly ISqlSugarClient _mockDb;

    public TestServiceTests(ISqlSugarClient mockDb)
    {
        // 创建一个模拟的SqlSugarClient实例
        _mockDb = mockDb;
        _mockDb.CreateTable();

        // 使用模拟的SqlSugarClient实例初始化TestService
        _testService = new TestService(_mockDb);
    }

    [Fact]
    public void SayHello_正常输入_返回正确问候()
    {
        // 测试正常输入
        var result = _testService.SayHello("世界");
        Assert.Equal("Hello 世界", result);
    }

    [Fact]
    public void SayHello_空输入_返回正确问候()
    {
        // 测试空输入
        var result = _testService.SayHello("");
        Assert.Equal("Hello ", result);
    }

    [Fact]
    public void SayHello_特殊字符输入_返回正确问候()
    {
        // 测试特殊字符输入
        var result = _testService.SayHello("!@#$%^&*()");
        Assert.Equal("Hello !@#$%^&*()", result);
    }

    [Fact]
    public void GetOrder_正常输入_返回正确订单()
    {
        // 准备测试数据
        _mockDb.Insertable(new Order { Id = 1, Name = "订单1" }).ExecuteCommand();
        var result = _testService.GetOrder("1");
        Assert.Equal("1", result.Id);
    }

    [Fact]
    public void GetOrder_不存在的订单_返回空()
    {
        // 测试不存在的订单
        var result = _testService.GetOrder("999");
        Assert.Null(result);
    }

    [Fact]
    public void QueryAllList_执行查询_不抛出异常()
    {
        Assert.Null(Record.Exception(() => _testService.QueryAllList()));
    }

    [Fact]
    public void InsertStudent_插入单个学生_不抛出异常()
    {
        // 测试插入单个学生 不抛出异常
        Assert.Null(Record.Exception(() => _testService.InsertStudent()));
    }

    [Fact]
    public void UpdateStudent_更新单个学生_不抛出异常()
    {
        // 准备测试数据
        _mockDb.Insertable(new Student { Id = 1, Name = "张三", SchoolId = 1 }).ExecuteCommand();
        // 测试更新单个学生 不抛出异常
        Assert.Null(Record.Exception(() => _testService.UpdateStudent()));
    }

    [Fact]
    public void DeleteStudent_删除单个学生_不抛出异常()
    {
        // 准备测试数据
        _mockDb.Insertable(new Student { Id = 1, Name = "张三", SchoolId = 1 }).ExecuteCommand();
        // 测试删除单个学生 不抛出异常
        Assert.Null(Record.Exception(() => _testService.DeleteStudent()));
    }

    [Fact]
    public void GetOrderList_获取订单列表_返回正确结果()
    {
        var db = DbContext.Instance.ScopedContext;
        db.CodeFirst.InitTables(typeof(Order), typeof(Custom), typeof(OrderItem));
        // 准备测试数据
        db.Insertable(new Order { Id = 1, Name = "订单1", CustomId = 1 }).ExecuteCommand();
        db.Insertable(new Custom { Id = 1, Name = "客户1" }).ExecuteCommand();
        // 测试获取订单列表 返回正确结果
        var result = _testService.GetOrderList();
        Assert.Single(result);
        Assert.Equal("1", result.First().Id);
        Assert.Equal("客户1", result.First().CustomName);
    }

    [Fact]
    public void BulkList_执行批量操作_不抛出异常()
    {
        // 测试执行批量操作 不抛出异常
        Assert.Null(Record.Exception(() => _testService.BulkList()));
    }

    [Fact]
    public void ValidateData_执行验证_返回真()
    {
        // 测试执行验证 返回真
        var result = _testService.ValidateData();
        Assert.True(result);
    }
}
