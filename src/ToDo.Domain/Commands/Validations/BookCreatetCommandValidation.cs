namespace ToDo.Domain.Commands.Validations
{
    public class BookCreatetCommandValidation : BookValidation<BookCreateCommand>
    {
        public BookCreatetCommandValidation()
        {
            ValidateAuthor();
            ValidateTitle();
        }
    }
}
