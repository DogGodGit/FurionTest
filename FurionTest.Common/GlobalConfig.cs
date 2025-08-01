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
    }
}

public class RabbitMQConfig
{
    public string? HostName { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
}