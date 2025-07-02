using DiegoSantanaCalendar.Application.DTOs.Auth;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiegoSantanaCalendar.Application.Validate.Auth
{
    public class UpdateValidate : AbstractValidator<UpdateUserDTO>
    {
        public UpdateValidate()
        {
 
            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role do usuario é obrigatorio");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("O e-mail informado não é válido.")
                .MaximumLength(100).WithMessage("O e-mail deve ter no máximo 100 caracteres.");
        }
    }
}
