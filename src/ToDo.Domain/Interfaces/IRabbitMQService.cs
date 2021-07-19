using System;
using System.Threading;
using System.Threading.Tasks;

namespace ToDo.Domain.Interfaces
{
    public interface IRabbitMQService
    {
        Task Send<TEntity>(TEntity obj, Uri queue, CancellationToken cancellationToken);
    }
}
