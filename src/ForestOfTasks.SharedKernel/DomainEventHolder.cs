using System.ComponentModel.DataAnnotations.Schema;

namespace ForestOfTasks.SharedKernel;

public abstract class DomainEventHolder : IDomainEventHolder
{
    private readonly List<DomainEventBase> _domainEvents = [];

    [NotMapped]
    public IReadOnlyCollection<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

    public void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();
}
