namespace FurionTest.Api;
public class RemoteController : IDynamicApiController
{
    private readonly IHttp _http;

    public RemoteController(IHttp http)
    {
        _http = http;
    }

    public async Task<dynamic> GetData()
    {
        var data = await _http.GetTestAsync();

        // HttpResponseMessage
        var res = await "#(Furion:Address)/test".GetAsync();

        // Stream
        var stream = await "#(Furion:Address)/test".GetAsStreamAsync();

        // T
        var user = await "#(Furion:Address)/test".GetAsAsync<User>();

        // String
        var str = await "#(Furion:Address)/test".GetAsStringAsync();

        return data;
    }

    public async Task<Result> PostUser(User user)
    {
        return await _http.PostTestAsync(user);
    }

    public async Task<Result> PutData(User user)
    {
        var data = await _http.PutTestAsync(user);
        return data;
    }

    public async Task<Result> DeleteData(int id)
    {
        var data = await _http.DeleteTestAsync(id);
        return data;
    }

    public async Task<Result> PatchData(User user)
    {
        var data = await _http.PatchTestAsync(user);
        return data;
    }

    public async Task<Result> HeadData(User user)
    {
        var data = await _http.HeadTestAsync(user);
        return data;
    }

    public async Task<Result> GetXXX(int id, string name)
    {
        return await _http.GetXXXAsync(id, name);
    }

    public async Task<Result> GetXXX([FromQuery] User user)
    {
        return await _http.GetXXXAsync(user);
    }

    public async Task<Result> GetXXXWithHead(int id, string name)
    {
        return await _http.GetXXXAsync(id, name);
    }

    public async Task<Result> GetXXX2(int id, string token = default)
    {
        return await _http.GetXXX2Async(id, token);
    }

    public async Task<Result> GetGithub()
    {
        return await _http.GetGithubAsync();
    }

    public async Task<string> PostFile(IFormFile file)
    {
        HttpFile httpfile = HttpFile.Create("file", file.ToByteArray(), file.FileName);

        return await "#(Furion:Address)/test/upload".SetContentType("multipart/form-data").SetFiles(httpfile).PostAsStringAsync();
    }

    public async Task<string> PostFile(string token, IFormFile file)
    {
        var apiUrl = $"https://api.weixin.qq.com/wxa/img_sec_check?access_token={token}";

        HttpFile httpfile = HttpFile.Create("media", file.ToByteArray(), file.FileName);

        return await apiUrl.SetContentType("application/octet-stream").SetFiles(httpfile).PostAsStringAsync();
    }

    public async Task<string> PostFacebook(string token, string filePath)
    {
        //请求并且序列化请求结果
        var result = await "https://api.facebook.com/"
            .OnRequesting((client, req) =>
            {
                req.AppendQueries(new Dictionary<string, object> {
                    { "access_token", "xxxx"}
                });
            })
            .OnClientCreating(client =>
            {
                client.Timeout = new TimeSpan(30000); // 设置超时时间
            })
            .OnResponsing((client, req) =>
            {
                // res 为 HttpResponseMessage 对象
            })
            //如果不加OnException，则会直接截断
            .OnException((client, req, errors) =>
            {
                //激活异步拦截 此处可以做记录日志操作 也可保持现状
            })
            .PostAsAsync<string>();

        return result;
    }
}