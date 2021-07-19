using AutoMapper;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using ToDo.Domain.Events;
using ToDo.Domain.Models;
using ToDo.Infra.Data.MongoDB.Interfaces;

namespace ToDo.Infra.Data.MongoDB.Consumer
{
    public class BookEventConsumer : IConsumer<BookCreateEvent>, IConsumer<BookUpdateEvent>, IConsumer<BookDeleteEvent>
    {
        private readonly ILogger<BookEventConsumer> _logger;
        private readonly IMongoRepository _repository;
        private readonly IMapper _mapper;

        public BookEventConsumer() { }

        public BookEventConsumer(ILogger<BookEventConsumer> logger, IMapper mapper, IMongoRepository repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<BookCreateEvent> context)
        {
            _logger.LogInformation($"BookCreateEvent Consumer received: {context.Message.Id}");
            var result = _mapper.Map<Book>(context.Message);
            await _repository.CreateAsync(result);
        }

        public async Task Consume(ConsumeContext<BookUpdateEvent> context)
        {
            _logger.LogInformation($"BookUpdateEvent Consumer updated: {context.Message.Id}");
            var result = _mapper.Map<Book>(context.Message);
            await _repository.UpdateAsync(result);
        }

        public async Task Consume(ConsumeContext<BookDeleteEvent> context)
        {
            _logger.LogInformation($"BookDeleteEvent Consumer deleted: {context.Message.Id}");
            var result = _mapper.Map<Book>(context.Message);
            await _repository.DeleteAsync(result);
        }
    }
}
