using MassTransit;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ToDo.Domain.Events
{
    public class BookEventHandler :
        INotificationHandler<BookCreateEvent>,
        INotificationHandler<BookUpdateEvent>,
        INotificationHandler<BookDeleteEvent>
    {
        private readonly IBus _bus;

        public BookEventHandler(IBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(BookDeleteEvent notification, CancellationToken cancellationToken)
        {
            await _bus.Publish(notification, cancellationToken);
        }

        public async Task Handle(BookUpdateEvent notification, CancellationToken cancellationToken)
        {
            await _bus.Publish(notification, cancellationToken);
        }

        public async Task Handle(BookCreateEvent notification, CancellationToken cancellationToken)
        {
            await _bus.Publish(notification, cancellationToken);
        }
    }
}
