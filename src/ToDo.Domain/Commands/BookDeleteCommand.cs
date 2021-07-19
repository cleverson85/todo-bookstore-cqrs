using System;
using ToDo.Domain.Commands.Validations;

namespace ToDo.Domain.Commands
{
    public class BookDeleteCommand : BookCommand
    {
        public BookDeleteCommand(Guid id)
        {
            Id = id;
            AggregateId = id;
        }

        public override bool IsValid()
        {
            ValidationResult = new BookDeleteCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
