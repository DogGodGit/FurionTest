using FurionTest.Application.Interfaces;
using StackExchange.Profiling.Internal;

namespace FurionTest.Api;
/// <summary>
/// 系统服务接口
/// </summary>
//[Authorize]
public class SystemController : IDynamicApiController
{
    private readonly ISystemService _systemService;
    private readonly ITestService _testService;

    public SystemController(
    ISystemService systemService,
    ITestService testService)
    {
        _systemService = systemService;
        _testService = testService;
    }

    /// <summary>
    /// 获取系统描述
    /// </summary>
    /// <returns></returns>
    public string GetDescription()
    {
        return _systemService.GetDescription();
    }

    public string SayHello(string word)
    {
        return _testService.SayHello(word);
    }

    public List<ViewOrder> GetOrderList()
    {
        return _testService.GetOrderList();
    }

    public string GetConfig()
    {
        return App.Configuration.ToJson();
    }
}