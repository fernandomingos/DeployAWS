using DeployAWS.Application.Dtos;
using FluentValidation;

namespace DeployAWS.Application.Validator
{
    public class CustomerDtoPUTValidator : AbstractValidator<CustomerDto>
    {
        public CustomerDtoPUTValidator()
        {
            RuleFor(c => c.Id).NotNull().NotEmpty();

            RuleFor(c => c.UserName).NotNull().NotEmpty();
            RuleFor(c => c.UserName).MaximumLength(20);

            RuleFor(c => c.FirstName).NotNull().NotEmpty();
            RuleFor(c => c.FirstName).MaximumLength(80);

            RuleFor(c => c.LastName).NotNull().NotEmpty();
            RuleFor(c => c.LastName).MaximumLength(80);

            RuleFor(c => c.EmailAddress).NotNull().NotEmpty();
            RuleFor(c => c.EmailAddress).MaximumLength(80);

            RuleFor(c => c.Profile).NotNull().NotEmpty();
            RuleFor(c => c.Profile).MaximumLength(80);

            RuleFor(c => c.CreateDate).NotNull().NotEmpty();

            RuleFor(c => c.IsActive).NotNull().NotEmpty();

            

            RuleFor(c => c.LastName).NotNull().NotEmpty();
            RuleFor(c => c.LastName).MaximumLength(80);

            RuleFor(c => c.EmailAddress).NotNull().NotEmpty();
        }
    }
}
