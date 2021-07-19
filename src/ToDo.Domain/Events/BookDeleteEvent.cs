using NetDevPack.Messaging;
using System;

namespace ToDo.Domain.Events
{
    public class BookDeleteEvent : Event
    {
        public BookDeleteEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; set; }
    }
}
