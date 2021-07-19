using MediatR;
using System;
using ToDo.Domain.Models;

namespace ToDo.Infra.Data.MongoDB.Query
{
    public class BookIdQuery : IRequest<Book>
    {
        public Guid Id { get; private set; }

        public BookIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
