using MediatR;
using System.Threading;
using System.Threading.Tasks;
using ToDo.Domain.Models;
using ToDo.Infra.Data.MongoDB.Interfaces;

namespace ToDo.Infra.Data.MongoDB.Query
{
    public class BookQueryHandler : IRequestHandler<BookIdQuery, Book>
    {
        private readonly IMongoRepository _mongoRepository;

        public BookQueryHandler(IMongoRepository mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public Task<Book> Handle(BookIdQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
