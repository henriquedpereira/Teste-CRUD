using FluentValidation;


namespace Core.Entities.Validators
{
    public class LivroValidator : AbstractValidator<Livro>
    {
        public LivroValidator()
        {
            RuleFor(x => x.Titulo)
                .NotEmpty().WithMessage("Titulo não pode ser vazia.")
                .MaximumLength(40).WithMessage("Titulo não pode ser maior que 40 caracteres.");

            RuleFor(x => x.Editora)
                .NotEmpty().WithMessage("Editora não pode ser vazia.")
                .MaximumLength(40).WithMessage("Editora não pode ser maior que 40 caracteres.");

            RuleFor(x => x.Edicao)
               .NotEmpty().WithMessage("Edicao não pode ser vazia.");

            RuleFor(x => x.AnoPublicacao)
              .NotEmpty().WithMessage("Ano de Publicacao não pode ser vazia.");

            RuleForEach(l => l.ListaFormasPag)
          .ChildRules(fp =>
          {
              fp.RuleFor(f => f.text)
                  .NotEmpty().WithMessage("A descrição da forma de pagamento é obrigatória.");
              fp.RuleFor(f => f.value)
                  .NotEmpty().WithMessage("O valor da forma de pagamento é obrigatório.");
              fp.RuleFor(f => f.selected)
                  .Must((formapag, selected) => !selected || formapag.value2 != 0)
                  .WithMessage("O valor é obrigatório quando a forma de pagamento está selecionada.");
          });

        }
    }
}
