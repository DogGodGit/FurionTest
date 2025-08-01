using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Concurrent;

public class RedisHelper : IDisposable
{
    //连接字符串
    private string _connectionString;

    //实例名称
    private string _instanceName;

    //默认数据库
    public int _defaultDB;

    private ConcurrentDictionary<string, ConnectionMultiplexer> _connections;

    public RedisHelper(string connectionString, string instanceName, int defaultDB = 0)
    {
        _connectionString = connectionString;
        _instanceName = instanceName;
        _defaultDB = defaultDB;
        _connections = new ConcurrentDictionary<string, ConnectionMultiplexer>();
    }

    /// <summary>
    /// 获取ConnectionMultiplexer
    /// </summary>
    /// <returns></returns>
    protected ConnectionMultiplexer GetConnect()
    {
        return _connections.GetOrAdd(_instanceName, p => ConnectionMultiplexer.Connect(_connectionString));
    }

    /// <summary>
    /// 订阅
    /// </summary>
    /// <param name="channel">频道</param>
    /// <param name="handle">事件</param>
    public void Subscribe(RedisChannel channel, Action<RedisChannel, RedisValue> handle)
    {
        //getSubscriber() 获取到指定服务器的发布者订阅者的连接
        var sub = GetConnect().GetSubscriber();
        //订阅执行某些操作时改变了 优先/主动 节点广播
        sub.Subscribe(channel, handle);
    }

    /// <summary>
    /// 发布
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public long Publish(RedisChannel channel, RedisValue message)
    {
        var sub = GetConnect().GetSubscriber();
        return sub.Publish(channel, message);
    }

    /// <summary>
    /// 发布
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public async Task<long> PublishAsync(RedisChannel channel, RedisValue message)
    {
        var sub = GetConnect().GetSubscriber();
        return await sub.PublishAsync(channel, message);
    }

    /// <summary>
    /// 发布（使用序列化）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="channel"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public long Publish<T>(RedisChannel channel, T message)
    {
        var sub = GetConnect().GetSubscriber();
        return sub.Publish(channel, JsonConvert.SerializeObject(message));
    }

    /// <summary>
    /// 订阅
    /// </summary>
    /// <param name="redisChannel"></param>
    /// <param name="handle"></param>
    /// <returns></returns>
    public async Task SubscribeAsync(RedisChannel redisChannel, Action<RedisChannel, RedisValue> handle)
    {
        var sub = GetConnect().GetSubscriber();
        await sub.SubscribeAsync(redisChannel, handle);
    }

    /// <summary>
    /// 取消订阅
    /// </summary>
    /// <param name="redisChannel"></param>
    /// <param name="handle"></param>
    /// <returns></returns>
    public async Task UnsubscribeAsync(RedisChannel redisChannel)
    {
        var sub = GetConnect().GetSubscriber();
        await sub.UnsubscribeAsync(redisChannel);
    }

    /// <summary>
    /// 取消订阅
    /// </summary>
    /// <param name="redisChannel"></param>
    /// <param name="handle"></param>
    /// <returns></returns>
    public void Unsubscribe(RedisChannel redisChannel)
    {
        var sub = GetConnect().GetSubscriber();
        sub.Unsubscribe(redisChannel);
    }

    public void Dispose()
    {
        _connections.Clear();
    }
}