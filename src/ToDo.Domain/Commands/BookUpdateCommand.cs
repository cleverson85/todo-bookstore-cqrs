using System;
using ToDo.Domain.Commands.Validations;

namespace ToDo.Domain.Commands
{
    public class BookUpdateCommand : BookCommand
    {
        public BookUpdateCommand(Guid id, string title, string author, string synopsis, string url, bool available)
        {
            Id = id;
            Title = title;
            Author = author;
            Synopsis = synopsis;
            Url = url;
            Available = available;
        }

        public override bool IsValid()
        {
            ValidationResult = new BookUpdateCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
