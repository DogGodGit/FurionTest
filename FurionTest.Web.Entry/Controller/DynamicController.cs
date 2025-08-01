using Furion.ClayObject;

namespace FurionTest.Api.Controller;

public class DynamicController : IDynamicApiController
{
    /// <summary>
    /// 获取动态对象
    /// </summary>
    /// <returns></returns>
    public dynamic GetDynamicObject()
    {
        // 创建一个空的粘土对象
        dynamic clay = new Clay();

        // 从现有的对象创建
        dynamic clay2 = Clay.Object(new { });

        // 从 json 字符串创建，可用于第三方 API 对接，非常有用
        dynamic clay3 = Clay.Parse(@"{""foo"":""json"", ""bar"":100, ""nest"":{ ""foobar"":true } }");

        return clay3;
    }

    public string GetDynamicObjectWithProperties()
    {
        dynamic clay = Clay.Object(new
        {
            Foo = "json",
            Bar = 100,
            Nest = new
            {
                Foobar = true
            }
        });

        var r1 = clay.Foo; // "json" - string类型
        var r2 = clay.Bar; // 100 - double类型
        var r3 = clay.Nest.Foobar; // true - bool类型
        var r4 = clay["Nest"]["Foobar"]; // 还可以和 JavaScript 一样通过索引器获取

        return $" {r1}-{r2}-{r3}-{r4}";
    }
}