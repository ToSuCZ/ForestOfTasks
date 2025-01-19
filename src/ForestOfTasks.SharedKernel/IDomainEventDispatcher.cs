namespace ForestOfTasks.SharedKernel;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearAsync(IEnumerable<IDomainEventHolder> entities, CancellationToken cancellationToken = default);
}
