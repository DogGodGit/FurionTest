[ErrorCodeType]
public enum ErrorCodes
{
    [ErrorCodeItemMetadata("Error.z1000")]
    z1000,

    [ErrorCodeItemMetadata("Error.x1000")]
    x1000,

    [ErrorCodeItemMetadata("Error.x1001")]
    x1001,

    [ErrorCodeItemMetadata("Error.SERVER_ERROR", ErrorCode = 1000)]
    SERVER_ERROR
}

[ErrorCodeType]
public enum UserErrorCodes
{
    [ErrorCodeItemMetadata("用户数据不存在")]
    u1000,

    [ErrorCodeItemMetadata("其他异常")]
    u1001
}