using FluentValidation.Results;
using MediatR;
using NetDevPack.Mediator;
using NetDevPack.Messaging;
using System;
using System.Threading;
using System.Threading.Tasks;
using ToDo.Domain.Events;
using ToDo.Domain.Interfaces;
using ToDo.Domain.Models;

namespace ToDo.Domain.Commands
{
    public class BookCommandHandler : CommandHandler,
        IRequestHandler<BookCreateCommand, ValidationResult>,
        IRequestHandler<BookUpdateCommand, ValidationResult>,
        IRequestHandler<BookDeleteCommand, ValidationResult>
    {
        private readonly IBookRepository _bookRepository;

        public BookCommandHandler(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<ValidationResult> Handle(BookCreateCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                return message.ValidationResult;
            }

            var book = new Book(Guid.NewGuid(), message.Title, message.Author, message.Synopsis, message.Url, true);
            book.AddDomainEvent(new BookCreateEvent(book.Id, book.Title, book.Author, book.Synopsis, book.Url, true));

            await _bookRepository.SaveAsync(book);
            return await Commit(_bookRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(BookUpdateCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                return message.ValidationResult;
            }

            var book = new Book(message.Id, message.Title, message.Author, message.Synopsis, message.Url, message.Available);

            var existing = await _bookRepository.GetAsync(message.Id);

            if (existing is null)
            {
                AddError("The book not exists.");
                return ValidationResult;
            }

            book.AddDomainEvent(new BookUpdateEvent(book.Id, book.Title, book.Author, book.Synopsis, book.Url, book.Available));

            await _bookRepository.UpdateAsync(book);
            return await Commit(_bookRepository.UnitOfWork);
        }

        public async Task<ValidationResult> Handle(BookDeleteCommand message, CancellationToken cancellationToken)
        {
            if (!message.IsValid())
            {
                return message.ValidationResult;
            }

            var bookExisting = await _bookRepository.GetAsync(message.Id);

            if (bookExisting is null)
            {
                AddError("The book not exists.");
                return ValidationResult;
            }

            bookExisting.AddDomainEvent(new BookDeleteEvent(message.Id));

            await _bookRepository.DeleteAsync(bookExisting);
            return await Commit(_bookRepository.UnitOfWork);
        }

        public void Dispose()
        {
            _bookRepository.Dispose();
        }
    }
}
