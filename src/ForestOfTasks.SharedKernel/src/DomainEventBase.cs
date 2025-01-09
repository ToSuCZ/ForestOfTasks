namespace ForestOfTasks.SharedKernel;

public abstract class DomainEventBase
{
  public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
}
