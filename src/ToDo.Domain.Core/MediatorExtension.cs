using Microsoft.EntityFrameworkCore;
using NetDevPack.Domain;
using NetDevPack.Mediator;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo.Domain.Core
{
    public static class MediatorExtension
    {
        public static async Task PublishDomainEvents<TEntity>(this IMediatorHandler mediator, TEntity context) where TEntity : DbContext
        {
            var domainEntities = context.ChangeTracker
                .Entries<Entity>()
                .Where(c => c.Entity.DomainEvents != null && c.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await mediator.PublishEvent(domainEvent);
                });

            await Task.WhenAll(tasks);
        }
    }
}
