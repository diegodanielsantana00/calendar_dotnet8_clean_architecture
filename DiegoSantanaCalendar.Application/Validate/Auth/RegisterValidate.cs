using DiegoSantanaCalendar.Application.DTOs.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiegoSantanaCalendar.Application.Validate.Auth
{
    public class RegisterValidate : AbstractValidator<RegisterDTO>
    {
        public RegisterValidate()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("O nome de usuário é obrigatório.")
                .MinimumLength(3).WithMessage("O nome de usuário deve ter pelo menos 3 caracteres.")
                .MaximumLength(50).WithMessage("O nome de usuário deve ter no máximo 50 caracteres.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role do usuario é obrigatorio");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("O e-mail informado não é válido.")
                .MaximumLength(100).WithMessage("O e-mail deve ter no máximo 100 caracteres.");

            RuleFor(x => x.Password)
          .NotEmpty().WithMessage("A senha é obrigatória.")
          .MinimumLength(8).WithMessage("A senha deve ter pelo menos 8 caracteres.")
          .Matches("[A-Z]").WithMessage("A senha deve conter ao menos uma letra maiúscula.")
          .Matches("[a-z]").WithMessage("A senha deve conter ao menos uma letra minúscula.")
          .Matches("[0-9]").WithMessage("A senha deve conter ao menos um número.")
          .Matches("[^a-zA-Z0-9]").WithMessage("A senha deve conter ao menos um caractere especial.");
        }
    }
}
