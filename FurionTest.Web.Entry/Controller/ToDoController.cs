using Furion.EventBus;
using FurionTest.Application.Services.ToDoEvent.EventSource;

namespace FurionTest.Api;

public class ToDoController : IDynamicApiController
{
    // 依赖注入事件发布者 IEventPublisher
    private readonly IEventPublisher _eventPublisher;

    public ToDoController(IEventPublisher eventPublisher)
    {
        _eventPublisher = eventPublisher;
    }

    // 发布 ToDo:Create 消息
    public async Task PublishMessage(string name = "test")
    {
        await _eventPublisher.PublishAsync(new ChannelEventSource("ToDo:Create", name));
    }

    // 发布 ToDo:Create 消息
    public async Task PublishCustomMessage(string name = "test")
    {
        var source = new ToDoEventSource("ToDo:Create", name);
        source.ToDoName = Guid.NewGuid().ToString("N"); // 设置自定义属性
        await _eventPublisher.PublishAsync(source);
    }

    public async Task MQPublishMessage(string name = "test")
    {
        await _eventPublisher.PublishAsync(new RabbitMQEventSource("ToDo:Create", name));
    }

    public async Task RedisPublishMessage(string name = "test")
    {
        await _eventPublisher.PublishAsync(new RedisEventSource("ToDo:Create", name));
    }
}