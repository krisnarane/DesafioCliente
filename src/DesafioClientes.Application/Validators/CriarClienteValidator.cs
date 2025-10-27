using DesafioClientes.Application.DTOs;
using FluentValidation;

namespace DesafioClientes.Application.Validators;

public class CriarClienteValidator : AbstractValidator<CriarClienteDTO>
{
    public CriarClienteValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório")
            .MinimumLength(3).WithMessage("Nome deve ter no mínimo 3 caracteres")
            .MaximumLength(150).WithMessage("Nome deve ter no máximo 150 caracteres");

        RuleFor(x => x.Endereco)
            .SetValidator(new EnderecoValidator()!)
            .When(x => x.Endereco != null);

        RuleForEach(x => x.Contatos)
            .SetValidator(new ContatoValidator());
    }
}
