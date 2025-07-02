using DiegoSantanaCalendar.Application.Interfaces;
using DiegoSantanaCalendar.Application.Services;
using DiegoSantanaCalendar.Domain.Interfaces;
using DiegoSantanaCalendar.Domain.Interfaces.Base;
using DiegoSantanaCalendar.Infrastructure.Repositories;
using DiegoSantanaCalendar.Infrastructure.Repositories.Base;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoSantanaCalendar.CrossCutting.Compromise.Setups
{
    class RepositorySetup
    {
        public static void ConfigureRepository(IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            //services.AddScoped<IUserRepository, UserRepository>();
        }

    }
}
