using FluentValidation;
using System;

namespace ToDo.Domain.Commands.Validations
{
    public abstract class BookValidation<TEntity> : AbstractValidator<TEntity> where TEntity : BookCommand
    {
        protected void ValidateAuthor()
        {
            RuleFor(c => c.Author)
                .NotEmpty()
                .WithMessage("Please ensure you have entered the Author Name.")
                .Length(2, 150)
                .WithMessage("The Author Name must have between 2 and 150 characters");
        }

        protected void ValidateTitle()
        {
            RuleFor(c => c.Title)
                .NotEmpty()
                .WithMessage("Please ensure you have entered the Title.");
        }

        protected void ValidateImage()
        {
            RuleFor(c => c.Url)
                .NotEmpty()
                .WithMessage("Imagem deve ser informada.");
        }

        protected void ValidateSinopsis()
        {
            RuleFor(c => c.Synopsis)
                .NotEmpty()
                .WithMessage("Sinopse deve ser informada.");
        }

        protected void ValidateId()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    }
}
