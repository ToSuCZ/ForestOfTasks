using System.Reflection;
using ForestOfTasks.Domain.Aggregates.UserAggregate;
using ForestOfTasks.SharedKernel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ForestOfTasks.Infrastructure.Data;

public class ApplicationDbContext(
  DbContextOptions options,
  IDomainEventDispatcher? dispatcher
  ) : IdentityDbContext<ApplicationUser>(options)
{

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.HasDefaultSchema("ForestOfTasks.ServiceDefaults");
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<decimal>().HavePrecision(18, 6);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        if (dispatcher is null)
        {
            return result;
        }

        var entities = ChangeTracker
          .Entries<IDomainEventHolder>()
          .Select(e => e.Entity)
          .Where(e => e.DomainEvents.Count > 0)
          .ToArray();

        await dispatcher.DispatchAndClearAsync(entities, cancellationToken);

        return result;
    }
}
