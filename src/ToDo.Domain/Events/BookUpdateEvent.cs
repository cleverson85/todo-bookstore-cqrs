using NetDevPack.Messaging;
using System;

namespace ToDo.Domain.Events
{
    public class BookUpdateEvent : Event
    {
        public BookUpdateEvent(Guid id, string title, string author, string synopsis, string url, bool available)
        {
            Id = id;
            Title = title;
            Author = author;
            Synopsis = synopsis;
            Url = url;
            Available = available;
            AggregateId = id;
        }

        public Guid Id { get; set; }
        public string Title { get; private set; }
        public string Author { get; private set; }
        public string Synopsis { get; private set; }
        public string Url { get; private set; }
        public bool Available { get; private set; } = true;
    }
}
