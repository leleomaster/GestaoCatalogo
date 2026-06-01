using FluentValidation;
using GestaoCatalogo.Application.Dtos;

namespace GestaoCatalogo.Application.Validations;

public class ProdutoDtoValidator : AbstractValidator<ProdutoDto>
{
    public ProdutoDtoValidator()
    {
        RuleFor(p => p.Nome)
            .NotEmpty().WithMessage("O campo Nome é obrigatório.")
            .MinimumLength(5).WithMessage("O campo Nome deve ter pelo menos 5 caracteres.");
    }
}