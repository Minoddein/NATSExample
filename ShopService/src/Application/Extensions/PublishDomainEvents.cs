using Domain.Shared;
using MediatR;

namespace Application.Extensions;

public static class MediatrExtensions
{
    public static async Task PublishDomainEvents(
        this IPublisher publisher,
        DomainEntity entity,
        CancellationToken cancellationToken = default)
    {
        foreach (var @event in entity.Events)
        {
            await publisher.Publish(@event, cancellationToken);
        }
        
        entity.ClearEvents();
    }
}