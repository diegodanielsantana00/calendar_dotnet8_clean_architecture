using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using DiegoSantanaCalendar.Application.Validate.Auth;
using DiegoSantanaCalendar.Application.Validate.Contact;

namespace DiegoSantanaCalendar.CrossCutting.Compromise.Setups
{
    class ValidateSetup
    {
        public static void ConfigureValidate(IServiceCollection services, IConfiguration configuration)
        {
            services.AddValidatorsFromAssemblyContaining<LoginValidate>();
            services.AddValidatorsFromAssemblyContaining<RegisterValidate>();
            services.AddValidatorsFromAssemblyContaining<CreateContactDTOValidate>();
            services.AddValidatorsFromAssemblyContaining<UpdateContactDTOValidate>();
            services.AddValidatorsFromAssemblyContaining<UpdateContactStatusDtoValidate>();

        }
    }
}
