using AutoMapper;
using FluentValidation.Results;
using NetDevPack.Mediator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Application.EventSourcedNormalizers;
using ToDo.Application.Interfaces;
using ToDo.Application.ViewModels;
using ToDo.Domain.Commands;
using ToDo.Domain.Models;
using ToDo.Infra.Data.MongoDB.Interfaces;
using ToDo.Infra.Data.Repository.Repositories.EventSourcing;

namespace ToDo.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _mediator;
        private readonly IEventStoreRepository _eventStoreRepository;
        private readonly IMongoRepository _mongoRepository;

        public BookService(IMapper mapper, IMediatorHandler mediator, 
                           IEventStoreRepository eventStoreRepository,
                           IMongoRepository mongoRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _eventStoreRepository = eventStoreRepository;
            _mongoRepository = mongoRepository;
        }

        public async Task<ValidationResult> Create(BookViewModel bookViewModel)
        {
            var registerCommand = _mapper.Map<BookCreateCommand>(bookViewModel);
            return await _mediator.SendCommand(registerCommand);
        }

        public async Task<ValidationResult> Update(BookViewModel bookViewModel)
        {
            var updateCommand = _mapper.Map<BookUpdateCommand>(bookViewModel);
            return await _mediator.SendCommand(updateCommand);
        }

        public async Task<ValidationResult> Delete(Guid id)
        {
            var removeCommand = new BookDeleteCommand(id);
            return await _mediator.SendCommand(removeCommand);
        }

        public async Task<IEnumerable<BookViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<BookViewModel>>(await _mongoRepository.GetAllAsync<Book>());
        }

        public async Task<BookViewModel> GetById(Guid id)
        {
            return _mapper.Map<BookViewModel>(await _mongoRepository.GetAsync<Book>(id));
        }

        public async Task<IList<BookHistoryData>> GetAllHistory(Guid id)
        {
            return BookHistory.ToJavaScriptCustomerHistory(await _eventStoreRepository.All(id));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
