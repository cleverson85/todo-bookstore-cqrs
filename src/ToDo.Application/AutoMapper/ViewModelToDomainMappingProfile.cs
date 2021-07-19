using AutoMapper;
using ToDo.Application.ViewModels;
using ToDo.Domain.Commands;

namespace ToDo.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<BookViewModel, BookCreateCommand>()
             .ConstructUsing(c => new BookCreateCommand(c.Title, c.Author, c.Synopsis, c.Url, c.Available));

            CreateMap<BookViewModel, BookUpdateCommand>()
             .ConstructUsing(c => new BookUpdateCommand(c.Id, c.Title, c.Author, c.Synopsis, c.Url, c.Available));
        }
    }
}
