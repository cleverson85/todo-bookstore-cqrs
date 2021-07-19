using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Domain.Core.Events;

namespace ToDo.Infra.Data.Repository.Repositories.EventSourcing
{
    public interface IEventStoreRepository : IDisposable
    {
        void Store(StoredEvent theEvent);
        Task<IList<StoredEvent>> All(Guid aggregateId);
    }
}
