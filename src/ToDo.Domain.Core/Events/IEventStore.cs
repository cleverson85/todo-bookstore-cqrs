using NetDevPack.Messaging;

namespace ToDo.Domain.Core.Events
{
    public interface IEventStore
    {
        void Save<TEntity>(TEntity theEvent) where TEntity : Event;
    }
}
