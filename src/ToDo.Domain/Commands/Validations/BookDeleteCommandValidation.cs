namespace ToDo.Domain.Commands.Validations
{
    public class BookDeleteCommandValidation : BookValidation<BookDeleteCommand>
    {
        public BookDeleteCommandValidation()
        {
            ValidateId();
        }
    }
}
