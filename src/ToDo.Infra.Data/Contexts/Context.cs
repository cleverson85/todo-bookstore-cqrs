using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using NetDevPack.Mediator;
using NetDevPack.Messaging;
using System.Threading.Tasks;
using ToDo.Domain.Core;
using ToDo.Domain.Models;
using ToDo.Infra.Data.Mappings;

namespace ToDo.Infra.Data.Contexts
{
    public class Context : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public Context(DbContextOptions<Context> options, IMediatorHandler mediatorHandler) : base(options)
        {
            _mediatorHandler = mediatorHandler;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Book> Book { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfiguration(new BookMap());

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> Commit()
        {
            await _mediatorHandler.PublishDomainEvents(this).ConfigureAwait(false);
            var success = await SaveChangesAsync() > 0;
            return success;
        }
    }
}
