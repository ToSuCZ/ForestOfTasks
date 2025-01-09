using System.Reflection;
using ForestOfTasks.Domain.Aggregates.User;
using ForestOfTasks.SharedKernel;
using Microsoft.EntityFrameworkCore;

namespace ForestOfTasks.Infrastructure.Data;

public class ForestOfTasksDbContext(
  DbContextOptions options,
  IDomainEventDispatcher? dispatcher
  ) : DbContext(options)
{
  public DbSet<User> Users { get; set; } = null!;
  
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.HasDefaultSchema("ForestOfTasks");
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    
    base.OnModelCreating(modelBuilder);
  }
  
  protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
  {
    configurationBuilder.Properties<decimal>().HavePrecision(18,6);
  }
  
  public override async Task<int> SaveChangesAsync(CancellationToken ct = default)
  {
    int result = await base.SaveChangesAsync(ct).ConfigureAwait(false);

    if (dispatcher is null)
    {
      return result;
    }
    
    var entities = ChangeTracker
      .Entries<IDomainEventHolder>()
      .Select(e => e.Entity)
      .Where(e => e.DomainEvents.Any())
      .ToArray();
    
    await dispatcher.DispatchAndClearAsync(entities, ct);
    
    return result;
  }
}
