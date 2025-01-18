namespace ForestOfTasks.SharedKernel;

public abstract class EntityBase(int id, Guid publicId) : DomainEventHolder
{
  public int Id { get; set; } = id;
  
  public Guid PublicId { get; set; } = publicId;
}
