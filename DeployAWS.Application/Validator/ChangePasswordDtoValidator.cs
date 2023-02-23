using DeployAWS.Application.Dtos;
using FluentValidation;

namespace DeployAWS.Application.Validator
{
    public class ChangePasswordDtoValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordDtoValidator() 
        {
            RuleFor(c => c.UserName).NotEmpty();
            RuleFor(c => c.UserName).MaximumLength(20);

            RuleFor(p => p.ActualPassword).SetValidator(new PasswordValidator());

            RuleFor(p => p.NewPassword).SetValidator(new PasswordValidator());

            RuleFor(p => p.ConfirmNewPassword).SetValidator(new PasswordValidator());

            // Adicionar validação que compara as duas senhas novas.
        }
    }
}
