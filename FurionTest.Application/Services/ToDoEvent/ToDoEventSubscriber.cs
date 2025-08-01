using Furion.EventBus;

namespace FurionTest.Application.Services.ToDoEvent;

// 实现 IEventSubscriber 接口
public class ToDoEventSubscriber : IEventSubscriber, ISingleton
{
    public ToDoEventSubscriber()
    {
    }

    [EventSubscribe("ToDo:Create")]
    public async Task CreateToDo(EventHandlerExecutingContext context)
    {
        var todo = context.Source;
        Log.Information("创建一个 ToDo：{Name}", todo.Payload);

        await Task.CompletedTask;
    }

    // 支持多个
    [EventSubscribe("ToDo:Create")]
    [EventSubscribe("ToDo:Update")]
    public async Task CreateOrUpdateToDo(EventHandlerExecutingContext context)
    {
        var todo = context.Source;
        Log.Information("创建或更新一个 ToDo：{Name}", todo.Payload);
        await Task.CompletedTask;
    }
}