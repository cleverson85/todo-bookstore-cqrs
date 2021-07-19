using NetDevPack.Messaging;
using System;

namespace ToDo.Domain.Commands
{
    public abstract class BookCommand : Command
    {
        public Guid Id { get; protected set; }
        public string Title { get; protected set; }
        public string Author { get; protected set; }
        public string Synopsis { get; protected set; }
        public string Url { get; protected set; }
        public bool Available { get; protected set; }
    }
}
