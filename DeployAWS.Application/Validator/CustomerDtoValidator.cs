using DeployAWS.Application.Dtos;
using FluentValidation;

namespace DeployAWS.Application.Validator
{
    public class CustomerDtoValidator : AbstractValidator<CustomerDto>
    {
        public CustomerDtoValidator()
        {
            RuleFor(c => c.UserName).NotEmpty();
            RuleFor(c => c.UserName).MaximumLength(20);

            RuleFor(c => c.FirstName).NotEmpty();
            RuleFor(c => c.FirstName).MaximumLength(20);

            RuleFor(c => c.LastName).NotEmpty();
            RuleFor(c => c.LastName).MaximumLength(20);

            RuleFor(p => p.Password).SetValidator(new PasswordValidator());

            RuleFor(c => c.EmailAddress).NotEmpty()
                .Matches(@"[\@]+").WithMessage("Formato de email inválido!");
            RuleFor(c => c.EmailAddress).MaximumLength(40);

            RuleFor(c => c.Profile).NotEmpty();
            RuleFor(c => c.Profile).MaximumLength(20);
        }
    }
}
