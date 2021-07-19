using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Application.ViewModels;
using FluentValidation.Results;
using ToDo.Application.EventSourcedNormalizers;

namespace ToDo.Application.Interfaces
{
    public interface IBookService : IDisposable
    {
        Task<IEnumerable<BookViewModel>> GetAll();
        Task<BookViewModel> GetById(Guid id);
        Task<ValidationResult> Create(BookViewModel bookViewModel);
        Task<ValidationResult> Update(BookViewModel bookViewModel);
        Task<ValidationResult> Delete(Guid id);
        Task<IList<BookHistoryData>> GetAllHistory(Guid id);
    }
}
