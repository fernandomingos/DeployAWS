﻿using DeployAWS.Application.Dtos;
using FluentValidation;

namespace DeployAWS.Application.Validator
{
    public class ProdutoDtoValidator : AbstractValidator<ProdutoDto>
    {
        public ProdutoDtoValidator()
        {
            RuleFor(c => c.Id).NotNull().NotEmpty();

            RuleFor(c => c.Nome).NotNull().NotEmpty();
            RuleFor(c => c.Nome).MaximumLength(80);

            RuleFor(c => c.Valor).NotNull().NotEmpty();
        }
    }
}
