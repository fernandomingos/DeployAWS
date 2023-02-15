using DeployAWS.Application.Dtos;
using FluentValidation;

namespace DeployAWS.Application.Validator
{
    public class CustomerDtoValidator : AbstractValidator<CustomerDto>
    {
        public CustomerDtoValidator()
        {
            RuleFor(c => c.FirstName).NotNull().NotEmpty();
            RuleFor(c => c.FirstName).MaximumLength(80);

            RuleFor(c => c.LastName).NotNull().NotEmpty();
            RuleFor(c => c.LastName).MaximumLength(80);

            RuleFor(c => c.EmailAddress).NotNull().NotEmpty();
        }
    }
}
