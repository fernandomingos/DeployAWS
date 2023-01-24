using DeployAWS.Application.Dtos;
using FluentValidation;

namespace DeployAWS.Application.Validator
{
    public class ClientDtoValidator : AbstractValidator<ClientDto>
    {
        public ClientDtoValidator()
        {
            RuleFor(c => c.Nome).NotNull().NotEmpty();
            RuleFor(c => c.Nome).MaximumLength(80);

            RuleFor(c => c.Sobrenome).NotNull().NotEmpty();
            RuleFor(c => c.Sobrenome).MaximumLength(80);

            RuleFor(c => c.Email).NotNull().NotEmpty();
        }
    }
}
