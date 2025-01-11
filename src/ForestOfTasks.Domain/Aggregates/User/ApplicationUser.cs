using System.ComponentModel.DataAnnotations.Schema;
using ForestOfTasks.SharedKernel;
using Microsoft.AspNetCore.Identity;

namespace ForestOfTasks.Domain.Aggregates.User;

public class ApplicationUser : IdentityUser, IDomainEventHolder, IAggregateRoot
{
  private readonly List<DomainEventBase> _domainEvents = [];
  
  [NotMapped]
  public IReadOnlyCollection<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();
  
  public void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);
  
  public void ClearDomainEvents() => _domainEvents.Clear();
}
