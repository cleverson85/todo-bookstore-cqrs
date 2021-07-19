using NetDevPack.Messaging;
using Newtonsoft.Json;
using ToDo.Domain.Core.Events;
using ToDo.Infra.Data.Repository.Repositories.EventSourcing;

namespace ToDo.Infra.Data.Repository.EventSourcing
{
    public class EventStore : IEventStore
    {
        private readonly IEventStoreRepository _eventStoreRepository;

        public EventStore(IEventStoreRepository eventStoreRepository)
        {
            _eventStoreRepository = eventStoreRepository;
        }

        public void Save<TEntity>(TEntity theEvent) where TEntity : Event
        {
            var serializedData = JsonConvert.SerializeObject(theEvent);

            var storedEvent = new StoredEvent(
                theEvent,
                serializedData, "cleverson85@gmail.com");

            _eventStoreRepository.Store(storedEvent);
        }
    }
}
