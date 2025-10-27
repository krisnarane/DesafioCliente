using DesafioClientes.Application.DTOs;
using FluentValidation;

namespace DesafioClientes.Application.Validators;

public class EnderecoValidator : AbstractValidator<EnderecoDTO>
{
    public EnderecoValidator()
    {
        RuleFor(x => x.Cep)
            .NotEmpty().WithMessage("CEP é obrigatório")
            .Matches(@"^\d{5}-?\d{3}$").WithMessage("CEP deve estar no formato 00000-000 ou 00000000");

        RuleFor(x => x.Numero)
            .NotEmpty().WithMessage("Número é obrigatório")
            .MaximumLength(20).WithMessage("Número deve ter no máximo 20 caracteres");

        RuleFor(x => x.Complemento)
            .MaximumLength(200).WithMessage("Complemento deve ter no máximo 200 caracteres");
    }
}
