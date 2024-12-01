using FluentValidation;


namespace Core.Entities.Validators
{
    public class AutorValidator : AbstractValidator<Autor>
    {
        public AutorValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome não pode ser vazia.")
                .MaximumLength(40).WithMessage("Nome não pode ser maior que 40 caracteres.");
        }
    }
}
