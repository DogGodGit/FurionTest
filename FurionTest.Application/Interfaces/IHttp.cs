using Furion.RemoteRequest;
using FurionTest.Application.Dtos;

[RetryPolicy(3, 1000)] // 支持全局
[Headers("Authorization", "Bearer ")]
[Headers("X-Authorization", "Bearer ")] // 设置多个请求头
public interface IHttp : IHttpDispatchProxy
{
    [Get("#(Furion:Address)/test")]
    Task<dynamic> GetTestAsync();

    [Post("#(Furion:Address)/post")]
    Task<Result> PostTestAsync([Body] User user);

    [Put("#(Furion:Address)/test")]
    Task<Result> PutTestAsync([Body] User user);

    [Delete("#(Furion:Address)/test")]
    Task<Result> DeleteTestAsync(int id);

    [Patch("#(Furion:Address)/test")]
    Task<Result> PatchTestAsync([Body] User user);

    [Head("#(Furion:Address)/test")]
    Task<Result> HeadTestAsync([Body] User user);

    [Get("#(Furion:Address)/test/{id}/{name}"), Headers("Authorization", "value2")]
    Task<Result> GetXXXAsync(int id, string name);

    [Get("#(Furion:Address)/test/user?Id={u.Id}&Name={u.Name}")]
    Task<Result> GetXXXAsync(User u);

    [Get("#(Furion:Address)/test")]
    Task<Result> GetXXX2Async(int id, [Headers] string token = default);

    [Get("get"), Client("github"), RetryPolicy(3, 1000)]
    Task<Result> GetGithubAsync();
}