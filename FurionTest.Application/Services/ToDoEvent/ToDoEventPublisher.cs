using Furion.EventBus;

namespace FurionTest.Application.Services.ToDoEvent;

public class ToDoEventPublisher : IEventPublisher
{
    private readonly IEventSourceStorer _eventSourceStorer;

    public ToDoEventPublisher(IEventSourceStorer eventSourceStorer)
    {
        _eventSourceStorer = eventSourceStorer;
    }

    public async Task PublishAsync(IEventSource eventSource)
    {
        await _eventSourceStorer.WriteAsync(eventSource, eventSource.CancellationToken);
    }

    public async Task PublishAsync(string eventId, object payload = null, CancellationToken cancellationToken = default)
    {
        await _eventSourceStorer.WriteAsync(new ChannelEventSource(eventId, payload), cancellationToken);
    }

    public async Task PublishAsync(Enum eventId, object payload = null, CancellationToken cancellationToken = default)
    {
        await _eventSourceStorer.WriteAsync(new ChannelEventSource(eventId.ToString(), payload), cancellationToken);
    }

    public async Task PublishDelayAsync(IEventSource eventSource, long delay)
    {
        await _eventSourceStorer.WriteAsync(eventSource, eventSource.CancellationToken);
    }

    public async Task PublishDelayAsync(string eventId, long delay, object payload = null, CancellationToken cancellationToken = default)
    {
        await _eventSourceStorer.WriteAsync(new ChannelEventSource(eventId, payload), cancellationToken);
    }

    public async Task PublishDelayAsync(Enum eventId, long delay, object payload = null, CancellationToken cancellationToken = default)
    {
        await _eventSourceStorer.WriteAsync(new ChannelEventSource(eventId.ToString(), payload), cancellationToken);
    }
}