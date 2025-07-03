using DiegoSantanaCalendar.Application.DTOs.Contact;
using FluentValidation;

namespace DiegoSantanaCalendar.Application.Validate.Contact
{
    public class UpdateContactStatusDtoValidate : AbstractValidator<UpdateContactStatusDto>
    {
        public UpdateContactStatusDtoValidate()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("O ID do contato é obrigatório.");

            RuleFor(x => x.NewStatus)
                .IsInEnum()
                .WithMessage("O novo status fornecido não é válido.");
        }
    }
}
