namespace ForestOfTasks.SharedKernel;

public interface IDomainEventHolder
{
    IReadOnlyCollection<DomainEventBase> DomainEvents { get; }

    void RegisterDomainEvent(DomainEventBase domainEvent);

    void ClearDomainEvents();
}
