using MediatR;
using Microsoft.Extensions.Logging;

namespace ForestOfTasks.SharedKernel;

public class DomainEventDispatcher(
  ILogger<DomainEventDispatcher> logger,
  IPublisher mediator
) : IDomainEventDispatcher
{
  public async Task DispatchAndClearAsync(IEnumerable<IDomainEventHolder> entities, CancellationToken ct = default)
  {
    foreach (IDomainEventHolder entity in entities)
    {
        DomainEventBase[] domainEvents = entity.DomainEvents.ToArray();
        
        logger.LogInformation(
          "Dispatching domain events for entity {Entity}. Count: {count}",
          entity.GetType().Name,
          domainEvents.Length);

        entity.ClearDomainEvents();

        foreach (DomainEventBase domainEvent in domainEvents)
        {
          await mediator.Publish(domainEvent, ct);
        }
    }
  }
}
