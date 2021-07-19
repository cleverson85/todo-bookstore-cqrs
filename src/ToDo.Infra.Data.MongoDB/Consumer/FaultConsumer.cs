using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;
using ToDo.Domain.Events;

namespace ToDo.Infra.Data.MongoDB.Consumer
{
    public class FaultConsumer : IConsumer<Fault<BookCreateEvent>>
    {
        private readonly ILogger<FaultConsumer> _logger;

        public FaultConsumer(ILogger<FaultConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<Fault<BookCreateEvent>> context)
        {
            _logger.LogError($"Fault Consumer Error: {JsonConvert.SerializeObject(context.Message.Message)}");
            await Task.CompletedTask;
        }
    }
}
