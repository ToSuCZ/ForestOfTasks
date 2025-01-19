using Ardalis.GuardClauses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ForestOfTasks.SharedKernel;

public class DomainEventDispatcher(
  ILogger<DomainEventDispatcher> logger,
  IPublisher mediator
) : IDomainEventDispatcher
{
    private static readonly Action<ILogger, string, int, Exception?> _logDispatchingEvent =
      LoggerMessage.Define<string, int>(
        LogLevel.Information,
        new EventId(1, nameof(DispatchAndClearAsync)),
        "Dispatching domain events for entity {Entity}. Count: {Count}");

    public async Task DispatchAndClearAsync(IEnumerable<IDomainEventHolder> entities, CancellationToken cancellationToken = default)
    {
        Guard.Against.Null(entities);

        foreach (IDomainEventHolder entity in entities)
        {
            DomainEventBase[] domainEvents = entity.DomainEvents.ToArray();

            _logDispatchingEvent(
              logger,
              entity.GetType().Name,
              domainEvents.Length,
              null);

            entity.ClearDomainEvents();

            foreach (DomainEventBase domainEvent in domainEvents)
            {
                await mediator.Publish(domainEvent, cancellationToken);
            }
        }
    }
}
