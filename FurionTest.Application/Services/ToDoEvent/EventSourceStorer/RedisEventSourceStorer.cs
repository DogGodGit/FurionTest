using Furion.EventBus;
using Furion.JsonSerialization;
using FurionTest.Application.Services.ToDoEvent.EventSource;
using RabbitMQ.Client;
using StackExchange.Redis;
using System.Threading.Channels;

namespace FurionTest.Application.Services.ToDoEvent.EventSourceStorer;

public class RedisEventSourceStorer : IEventSourceStorer, IDisposable
{
    /// <summary>
    /// 内存通道事件源存储器
    /// </summary>
    private readonly Channel<IEventSource> _channel;

    private readonly RedisHelper _redisClient;
    private readonly RedisChannel _redisChannel;

    public RedisEventSourceStorer(RedisHelper redisClient, string routeKey = "EventHub", int capacity = 3000)
    {
        // 配置通道，设置超出默认容量后进入等待
        var boundedChannelOptions = new BoundedChannelOptions(capacity)
        {
            FullMode = BoundedChannelFullMode.Wait
        };

        // 创建有限容量通道
        _channel = Channel.CreateBounded<IEventSource>(boundedChannelOptions);
        _redisClient = redisClient;
        _redisChannel = RedisChannel.Pattern(routeKey);

        _redisClient.Subscribe(_redisChannel, (c, msg) =>
        {
            if (!msg.IsNull)
            {
                var eventSource = JSON.Deserialize<RedisEventSource>(msg);
                // 写入内存管道存储器
                _channel.Writer.WriteAsync(new ChannelEventSource(eventSource.EventId, eventSource.Payload));
            }
        });
    }

    // 往 Redis 中写入一条
    public async ValueTask WriteAsync(IEventSource eventSource, CancellationToken cancellationToken)
    {
        // 空检查
        if (eventSource == default)
        {
            throw new ArgumentNullException(nameof(eventSource));
        }

        if (eventSource is RedisEventSource source)
        {
            var json = JSON.Serialize(source);
            await _redisClient.PublishAsync(_redisChannel, json);
        }
        else
        {
            // 这里处理动态订阅问题
            await _channel.Writer.WriteAsync(eventSource, cancellationToken);
        }
    }

    // 从 Redis 中读取一条
    public async ValueTask<IEventSource> ReadAsync(CancellationToken cancellationToken)
    {
        var eventSource = await _channel.Reader.ReadAsync(cancellationToken);
        return eventSource;
    }

    public void Dispose()
    {
        _redisClient.Dispose();
    }
}
