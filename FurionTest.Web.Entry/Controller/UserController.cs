using Furion.JsonSerialization;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace FurionTest.Api;

[AllowAnonymous]
public class UserController : IDynamicApiController
{
    public string PostLogin(string username, string password)
    {
        // 生成 token
        var accessToken = JWTEncryption.Encrypt(new Dictionary<string, object>()
        {
            { "username", username },
            { "password", password },
        });
        return accessToken;
    }

    public string RefreshToken(string accessToken)
    {
        // 获取刷新 token
        var refreshToken = JWTEncryption.GenerateRefreshToken(accessToken, 43200); // 第二个参数是刷新 token 的有效期（分钟），默认三十天
                                                                                   // 获取 `Jwt` 存储的信息
        return refreshToken;
    }

    public JsonWebToken JWTDecrypt(string accessToken)
    {
        return JWTEncryption.ReadJwtToken(accessToken);  // 解密
    }

    public bool JWTValidate(string accessToken)
    {
        var (isValid, tokenData, result) = JWTEncryption.Validate(accessToken); // 验证token有效期

        return isValid;
    }

    [Authorize]
    public string GetName()
    {
        // 获取 `Jwt` 存储的信息
        return JSON.Serialize(new { username = App.User?.FindFirstValue("username"), password = App.User?.FindFirstValue("password") });
    }
}