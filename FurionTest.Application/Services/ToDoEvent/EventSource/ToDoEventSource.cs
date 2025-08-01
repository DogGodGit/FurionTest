using Furion.EventBus;

namespace FurionTest.Application.Services.ToDoEvent.EventSource;

public class ToDoEventSource : IEventSource
{
    public ToDoEventSource(string eventId, string payload)
    {
        EventId = eventId;
        Payload = payload;
    }

    // 自定义属性
    public string ToDoName { get; set; }

    /// <summary>
    /// 事件 Id
    /// </summary>
    public string EventId { get; }

    /// <summary>
    /// 事件承载（携带）数据
    /// </summary>
    public object Payload { get; }


    /// <summary>
    /// 事件创建时间
    /// </summary>
    public DateTime CreatedTime { get; } = DateTime.UtcNow;

    /// <summary>
    /// 取消任务 Token
    /// </summary>
    /// <remarks>用于取消本次消息处理</remarks>
    public CancellationToken CancellationToken { get; }
}
