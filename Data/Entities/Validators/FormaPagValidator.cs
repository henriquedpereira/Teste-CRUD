using FluentValidation;


namespace Core.Entities.Validators
{
    public class FormaPagValidator : AbstractValidator<FormaPag>
    {
        public FormaPagValidator()
        {
            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("Descrição não pode ser vazia.")
                .MaximumLength(20).WithMessage("Descrição não pode ser maior que 20 caracteres.");
        }
    }
}
