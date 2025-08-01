namespace FurionTest.Common;

// <summary>
/// 服务过滤器
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class MultiServiceFilterAttribute : Attribute
{
    public string _serviceName { get; }
    /// <summary>
    /// 服务过滤器
    /// </summary>
    /// <param name="serviceName">字符串</param>
    /// <exception cref="Exception"></exception>
    public MultiServiceFilterAttribute(string serviceName)
    {
        if (string.IsNullOrEmpty(serviceName))
            throw new Exception("serviceName不能为空");


        _serviceName = serviceName;
    }

    /// <summary>
    /// 服务过滤器
    /// </summary>
    /// <param name="serviceName">枚举</param>
    /// <exception cref="Exception"></exception>
    public MultiServiceFilterAttribute(RegionType serviceEnum)
    {
        if (serviceEnum == null)
        {
            throw new Exception("serviceEnum不能为空");
        }
        if (string.IsNullOrEmpty(serviceEnum.ToString()))
            throw new Exception("serviceName不能为空");

        _serviceName = serviceEnum.ToString();
    }
}