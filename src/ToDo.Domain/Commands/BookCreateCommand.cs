using ToDo.Domain.Commands.Validations;

namespace ToDo.Domain.Commands
{
    public class BookCreateCommand : BookCommand
    {
        public BookCreateCommand(string title, string author, string synopsis, string url, bool available)
        {
            Title = title;
            Author = author;
            Synopsis = synopsis;
            Url = url;
        }

        public override bool IsValid()
        {
            ValidationResult = new BookCreatetCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
