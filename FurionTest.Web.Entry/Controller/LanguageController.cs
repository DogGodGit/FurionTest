using Furion.Localization;

namespace FurionTest.Api;

public class LanguageController : IDynamicApiController
{
    [QueryParameters]
    public dynamic GetLangStr()
    {
        // 文本多语言
        var apiInterface = L.Text["apiInterface", "https://localhost:4398/api"].Value;
        var sourceCode = L.Text["sourceCode"].Value;

        // HTML 标记多语言
        var nameL = L.Html["hellostr", "hello", "world"];

        return new
        {
            apiInterface,
            sourceCode,
            nameL
        };
    }

}