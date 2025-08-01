using Furion.DistributedIDGenerator;
using Furion.SensitiveDetection;

namespace FurionTest.Api;

[ApiDescriptionSettings("Test", Tag = "Hello")]
public class HelloController : IDynamicApiController
{
    private readonly IDistributedIDGenerator _idGenerator;
    private readonly ISensitiveDetectionProvider _sensitiveDetectionProvider;

    public HelloController(IDistributedIDGenerator idGenerator, ISensitiveDetectionProvider sensitiveDetectionProvider)
    {
        _idGenerator = idGenerator;
        _sensitiveDetectionProvider = sensitiveDetectionProvider;
    }

    public int Get(int id)
    {
        switch (id)
        {
            case 1: throw Oops.Oh(1000);
            case 2: throw Oops.Oh(ErrorCodes.x1000);
            case 3: throw Oops.Oh("哈哈哈哈");
            case 4: throw Oops.Oh(errorCode: "x1001");
            case 5: throw Oops.Oh(1000, typeof(InvalidOperationException));
            case 6: throw Oops.Oh(1000).StatusCode(400);    // 设置错误码
            case 7: throw Oops.Bah("用户名或密码错误"); // 抛出业务异常，状态码为 400
            case 8: throw Oops.Bah(1000);
        }

        return id;
    }

    public void WriteLog()
    {
        Log.Information("Hello World");
        "简单日志".LogInformation();

        "百小僧 新增了一条记录".LogInformation<HelloController>();

        "程序出现异常啦".LogError<HelloController>();

        "这是自定义类别日志".SetCategory<HelloController>().LogInformation();

        TP.Wrapper("填充日志记录", "有数据进入", "##时间##" + DateTime.Now).LogInformation();
    }

    public TestDto GetValidateData([FromQuery] TestDto testDto)
    {
        return testDto;
    }

    [NonValidation] // 跳过全局验证
    public DataValidationResult PostValidateData(TestDto2 testDto)
    {
        return testDto.TryValidate();
    }

    public string RouteSeat(
        [ApiSeat(ApiSeats.ControllerStart)] int id, // 控制器名称之前
        [ApiSeat(ApiSeats.ControllerEnd)] string name, // 控制器名称之后
        [ApiSeat(ApiSeats.ControllerEnd)] int age, // 控制器名称之后
        [ApiSeat(ApiSeats.ActionStart)] decimal weight, // 动作方法名称之前
        [ApiSeat(ApiSeats.ActionStart)] float height, // 动作方法名称之前
        [ApiSeat(ApiSeats.ActionEnd)] DateTime birthday) // 动作方法名称之后（默认值）
    {
        return "配置路由参数位置";
    }

    public string GetGUID()
    {
        var guid = IDGen.NextID();

        // 还可以配置更多参数
        var guid2 = IDGen.NextID(new SequentialGuidSettings { LittleEndianBinary16Format = true });    // SequentialGuidSettings 参数取决于你的分布式ID的实现

        return guid.ToString();
    }

    public string GetGUID2()
    {
        var guidObject = _idGenerator.Create();
        var idGen = new SequentialGuidIDGenerator();
        var guid = idGen.Create();

        // 更多参数
        var idGen2 = new SequentialGuidIDGenerator();
        var guid2 = idGen2.Create(new SequentialGuidSettings { LittleEndianBinary16Format = true });
        return guidObject.ToString();
    }

    public void ShortID()
    {
        var shortid = ShortIDGen.NextID(); // 生成一个包含数字，字母，不包含特殊符号的 8 位短id

        // 添加更多配置
        var shortid2 = ShortIDGen.NextID(new GenerationOptions
        {
            UseNumbers = false, // 不包含数字
            UseSpecialCharacters = true, // 包含特殊符号
            Length = 8// 设置长度，注意：不设置次长度是随机长度！！！！！！！
        });

        // 自定义生成短 ID 参与运算字符
        string characters = "ⒶⒷⒸⒹⒺⒻⒼⒽⒾⒿⓀⓁⓂⓃⓄⓅⓆⓇⓈⓉⓊⓋⓌⓍⓎⓏⓐⓑⓒⓓⓔⓕⓖⓗⓘⓙⓚⓛⓜⓝⓞⓟⓠⓡⓢⓣⓤⓥⓦⓧⓨⓩ①②③④⑤⑥⑦⑧⑨⑩⑪⑫"; //whatever you want;
        ShortIDGen.SetCharacters(characters);
        shortid = ShortIDGen.NextID();

        // 自定义随机数（for）步长
        int seed = 1939048828;
        ShortIDGen.SetSeed(seed);
        shortid = ShortIDGen.NextID();

        // 重载所有自定义配置
        ShortIDGen.Reset();
    }

    /// <summary>
    /// 获取所有脱敏词汇
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<string>> GetWordsAsync()
    {
        return await _sensitiveDetectionProvider.GetWordsAsync();
    }

    /// <summary>
    /// 判断是否是正常的词汇
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public async Task<bool> VaildedAsync(string text)
    {
        return await _sensitiveDetectionProvider.VaildedAsync(text);
    }

    /// <summary>
    /// 替换非正常词汇
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public async Task<string> ReplaceAsync(string text)
    {
        return await _sensitiveDetectionProvider.ReplaceAsync(text, '*');
    }
}