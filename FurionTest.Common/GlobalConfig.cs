using System.Reflection;

namespace FurionTest.Core;

public enum CacheType
{
    None, SqlServer, Redis
}

public enum EventHubType
{
    None, RabbitMQ, Redis
}

public class GlobalConfig
{
    public static bool IsSqlProfiler;

    public static List<ConnectionConfig> ConnectionConfigs;

    public static CacheType CacheType { get; set; }
    public static EventHubType EventHubType { get; set; }
    public static ConnectionConfig Redis { get; set; }
    public static bool EnableGlobalAuthorize { get; set; }
    public static RabbitMQConfig RabbitMQ { get; set; }
    public static RegionType RegionType { get; set; }

    static GlobalConfig()
    {
        IsSqlProfiler = App.GetConfig<bool?>("Logging:Database:SqlProfiler") ?? true;
        ConnectionConfigs = App.GetConfig<List<ConnectionConfig>>("ConnectionConfigs") ?? new List<ConnectionConfig>();
        Redis = App.GetConfig<ConnectionConfig>("Redis") ?? new ConnectionConfig();
        CacheType = App.GetConfig<CacheType>("CacheType");
        EnableGlobalAuthorize = App.GetConfig<bool>("EnableGlobalAuthorize", true);

        RabbitMQ = App.GetConfig<RabbitMQConfig>("RabbitMQ") ?? new RabbitMQConfig();
        EventHubType = App.GetConfig<EventHubType>("EventHubType");

        RegionType = App.GetConfig<RegionType>("RegionType");

        foreach (var connectionConfig in ConnectionConfigs)
        {
            connectionConfig.ConfigureExternalServices = new ConfigureExternalServices
            {
                //注意:  这儿AOP设置不能少
                EntityService = (c, p) =>
                {
                    /***低版本C#写法***/
                    // int?  decimal?这种 isnullable=true 不支持string(下面.NET 7支持)
                    if (p.IsPrimarykey == false && c.PropertyType.IsGenericType &&
                    c.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        p.IsNullable = true;
                    }

                    /***高版C#写法***/
                    //支持string?和string
                    if (p.IsPrimarykey == false && new NullabilityInfoContext()
                     .Create(c).WriteState is NullabilityState.Nullable)
                    {
                        p.IsNullable = true;
                    }

                    //p.DbColumnName = UtilMethods.ToUnderLine(p.DbColumnName);//ToUnderLine驼峰转下划线方法
                },

                EntityNameService = (x, p) => //处理表名
                {
                    //p.DbTableName = UtilMethods.ToUnderLine(p.DbTableName);//ToUnderLine驼峰转下划线方法
                }
            };
        }
    }
}

public class RabbitMQConfig
{
    public string? HostName { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
}