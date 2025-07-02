using FluentValidation;
using DiegoSantanaCalendar.Application.DTOs.Auth;
using DiegoSantanaCalendar.Application.DTOs.Contact;

namespace DiegoSantanaCalendar.Application.Validate.Auth
{
    public class CreateContactDTOValidate : AbstractValidator<CreateContactDTO>
    {
        public CreateContactDTOValidate()
        {

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .Length(3, 150).WithMessage("O nome deve ter entre 3 e 150 caracteres.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("O formato do e-mail é inválido.")
                .MaximumLength(150).WithMessage("O e-mail não pode exceder 150 caracteres.");

            RuleFor(c => c.Phone)
                .NotEmpty().WithMessage("O telefone é obrigatório.")
                .Matches(@"^(\(?[1-9]{2}\)?\s?)?(9?[2-9][0-9]{3}\-?[0-9]{4})$")
                .WithMessage("O formato do telefone é inválido. Use (XX) 9XXXX-XXXX ou formatos similares.");

        }
    }
}
