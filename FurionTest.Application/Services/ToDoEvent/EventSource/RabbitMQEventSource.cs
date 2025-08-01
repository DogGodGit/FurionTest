using Furion.EventBus;

namespace FurionTest.Application.Services.ToDoEvent.EventSource;

public class RabbitMQEventSource : IEventSource
{
    public RabbitMQEventSource()
    {
    }

    public RabbitMQEventSource(string eventId, object payload)
    {
        EventId = eventId;
        Payload = payload;
    }

    /// <summary>
    /// 事件 Id
    /// </summary>
    public string EventId { get; set; }

    /// <summary>
    /// 事件承载(携带)数据
    /// </summary>
    public object Payload { get; set; }

    /// <summary>
    /// 事件创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// 取消任务 Token
    /// </summary>
    /// <remarks>用于取消本次消息处理</remarks>
    [Newtonsoft.Json.JsonIgnore]
    [System.Text.Json.Serialization.JsonIgnore]
    public CancellationToken CancellationToken { get; set; }
}
