using DiegoSantanaCalendar.Application.Interfaces;
using DiegoSantanaCalendar.Application.Services;
using DiegoSantanaCalendar.Application.Utils;
using DiegoSantanaCalendar.Domain.Interfaces.Base;
using DiegoSantanaCalendar.Infrastructure.Repositories.Base;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoSantanaCalendar.CrossCutting.Compromise.Setups
{
    class ServicesSetup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<MapperDtoToEntities>();
            services.AddSingleton<IMessagePublisher, RabbitMqPublisher>();
            services.AddHostedService<ContactStatusUpdateConsumer>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IJWTService, JWTService>();
            services.AddScoped<IContactService, ContactService>();
        }

    }
}
