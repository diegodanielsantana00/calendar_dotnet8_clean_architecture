using FluentValidation;
using DiegoSantanaCalendar.Application.DTOs.Auth;

namespace DiegoSantanaCalendar.Application.Validate.Auth
{
    public class LoginValidate : AbstractValidator<LoginDTO>
    {
        public LoginValidate()
        {

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("O nome de usuário é obrigatório.")
                .MinimumLength(3).WithMessage("O nome de usuário deve ter pelo menos 3 caracteres.")
                .MaximumLength(50).WithMessage("O nome de usuário deve ter no máximo 50 caracteres.");

            RuleFor(x => x.Password)
              .NotEmpty().WithMessage("A senha é obrigatória.")
              .MinimumLength(8).WithMessage("A senha deve ter pelo menos 8 caracter.")
              .Matches("[A-Z]").WithMessage("A senha deve conter ao menos uma letra maiúscula.")
              .Matches("[a-z]").WithMessage("A senha deve conter ao menos uma letra minúscula.")
              .Matches("[0-9]").WithMessage("A senha deve conter ao menos um número.")
              .Matches("[^a-zA-Z0-9]").WithMessage("A senha deve conter ao menos um caractere especial.");
        }
    }
}
