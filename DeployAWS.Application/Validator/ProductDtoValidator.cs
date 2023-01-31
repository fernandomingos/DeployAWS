using DeployAWS.Application.Dtos;
using FluentValidation;

namespace DeployAWS.Application.Validator
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>
    {
        public ProductDtoValidator()
        {
            RuleFor(c => c.Id).NotNull().NotEmpty();

            RuleFor(c => c.Name).NotNull().NotEmpty();
            RuleFor(c => c.Name).MaximumLength(80);

            RuleFor(c => c.Value).NotNull().NotEmpty();
        }
    }
}
