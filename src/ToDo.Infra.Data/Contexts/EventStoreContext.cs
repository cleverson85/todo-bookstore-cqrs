using Microsoft.EntityFrameworkCore;
using ToDo.Domain.Core.Events;
using ToDo.Infra.Data.Mappings;

namespace ToDo.Infra.Data.Contexts
{
    public class EventStoreContext : DbContext
    {
        public DbSet<StoredEvent> StoredEvent { get; set; }

        public EventStoreContext(DbContextOptions<EventStoreContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StoredEventMap());
            base.OnModelCreating(modelBuilder);
        }
    }
}
