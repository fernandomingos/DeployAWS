using DeployAWS.Application.Dtos;
using FluentValidation;

namespace DeployAWS.Application.Validator
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator() 
        {
            RuleFor(c => c.UserName).NotEmpty();
            RuleFor(c => c.UserName).MaximumLength(20);

            RuleFor(p => p.Password)
                .MinimumLength(8).WithMessage("O tamanho da sua senha deve ser de pelo menos 8 dígitos.")
                .MaximumLength(16).WithMessage("O tamanho da sua senha não deve exceder 16 dígitos")
                .Matches(@"[A-Z]+").WithMessage("Sua senha deve conter pelo menos uma letra maiúscula.")
                .Matches(@"[a-z]+").WithMessage("Sua senha deve conter pelo menos uma letra minúscula.")
                .Matches(@"[0-9]+").WithMessage("Sua senha deve conter pelo menos um número.")
                .Matches(@"[\!\?\*\.]+").WithMessage("Sua senha deve conter pelo menos um carácter especial (!? *.)");
        }
    }
}
