namespace ToDo.Domain.Commands.Validations
{
    public class BookUpdateCommandValidation : BookValidation<BookUpdateCommand>
    {
        public BookUpdateCommandValidation()
        {
            ValidateId();
            ValidateAuthor();
        }
    }
}
