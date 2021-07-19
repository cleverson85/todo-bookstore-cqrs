using MassTransit;
using System;
using System.Threading;
using System.Threading.Tasks;
using ToDo.Domain.Interfaces;

namespace FavoDeMel.Infra.Application.Services
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public RabbitMQService(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task Send<TEntity>(TEntity obj, Uri queue, CancellationToken cancellationToken)
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(queue);
            await sendEndpoint.Send(obj, cancellationToken);
        }
    }
}