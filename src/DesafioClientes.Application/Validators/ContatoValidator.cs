using DesafioClientes.Application.DTOs;
using FluentValidation;

namespace DesafioClientes.Application.Validators;

public class ContatoValidator : AbstractValidator<ContatoDTO>
{
    public ContatoValidator()
    {
        RuleFor(x => x.Tipo)
            .NotEmpty().WithMessage("Tipo de contato é obrigatório")
            .Must(tipo => new[] { "Email", "Telefone", "Celular" }.Contains(tipo))
            .WithMessage("Tipo deve ser Email, Telefone ou Celular");

        RuleFor(x => x.Texto)
            .NotEmpty().WithMessage("Texto do contato é obrigatório")
            .MaximumLength(100).WithMessage("Texto deve ter no máximo 100 caracteres");

        When(x => x.Tipo == "Email", () =>
        {
            RuleFor(x => x.Texto)
                .EmailAddress().WithMessage("Email inválido");
        });

        When(x => x.Tipo == "Telefone" || x.Tipo == "Celular", () =>
        {
            RuleFor(x => x.Texto)
                .Matches(@"^\(?\d{2}\)?\s?9?\d{4}-?\d{4}$")
                .WithMessage("Telefone deve estar no formato (00) 0000-0000 ou (00) 90000-0000");
        });
    }
}
