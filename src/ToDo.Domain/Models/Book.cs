using NetDevPack.Domain;
using System;

namespace ToDo.Domain.Models
{
    public class Book : Entity, IAggregateRoot
    {
        public Book(Guid id, string title, string author, string synopsis, string url, bool available)
        {
            Id = id;
            Title = title;
            Author = author;
            Synopsis = synopsis;
            Url = url;
            Available = available;
        }

        public Book() { }

        public string Title { get; set; }
        public string Author { get; set; }
        public string Synopsis { get; set; }
        public string Url { get; set; }
        public bool Available { get; set; } = true;
    }
}
