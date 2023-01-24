using DeployAWS.Application.Dtos;
using FluentValidation;

namespace DeployAWS.Application.Validator
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(c => c.Id).NotNull().NotEmpty();

            RuleFor(c => c.Nome).NotNull().NotEmpty();
            RuleFor(c => c.Nome).MaximumLength(80);

            RuleFor(c => c.Valor).NotNull().NotEmpty();
        }
    }
}
