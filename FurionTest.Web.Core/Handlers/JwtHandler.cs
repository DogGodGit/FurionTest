using Furion.Authorization;

namespace FurionTest.Core;

/// <summary>
/// JWT 授权自定义处理程序
/// </summary>
public class JwtHandler : AppAuthorizeHandler
{
    /// <summary>
    /// 请求管道
    /// </summary>
    /// <param name="context"></param>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public override Task<bool> PipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
    {
        // 此处已经自动验证 Jwt token的有效性了，无需手动验证

        // 检查权限，如果方法是异步的就不用 Task.FromResult 包裹，直接使用 async/await 即可
        return Task.FromResult(CheckAuthorzie(httpContext));
    }

    /// <summary>
    /// 检查权限
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    private static bool CheckAuthorzie(DefaultHttpContext httpContext)
    {
        // 获取权限特性
        var securityDefineAttribute = httpContext.GetMetadata<SecurityDefineAttribute>();
        if (securityDefineAttribute == null) return true;
        //"查询数据库返回是否有权限"
        return false;
    }

    /// <summary>
    /// 重写 Handler 添加自动刷新收取逻辑
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task HandleAsync(AuthorizationHandlerContext context)
    {
        // 常规授权（可以判断是不是第三方）
        var isAuthenticated = context.User.Identity.IsAuthenticated;

        // 自动刷新 token
        if (JWTEncryption.AutoRefreshToken(context, context.GetCurrentHttpContext()))
        {
            await AuthorizeHandleAsync(context);
        }
        else context.Fail();    // 授权失败

        // 第三方授权自定义
        if (true)
        {
            foreach (var requirement in context.PendingRequirements)
            {
                // 授权成功
                context.Succeed(requirement);
            }
        }
        // 授权失败
        else context.Fail();
    }
}