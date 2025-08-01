using Furion.EventBus;

namespace FurionTest.Application.Services.ToDoEvent;

public class RetryEventHandlerExecutor : IEventHandlerExecutor
{
    public async Task ExecuteAsync(EventHandlerExecutingContext context, Func<EventHandlerExecutingContext, Task> handler)
    {
        // 如果执行失败，每隔 1s 重试，最多三次
        await Retry.InvokeAsync(async () =>
        {
            await handler(context);
        }, 3, 1000);
    }
}
