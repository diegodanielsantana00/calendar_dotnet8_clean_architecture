using DiegoSantanaCalendar.Domain.Entities;
using DiegoSantanaCalendar.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoSantanaCalendar.CrossCutting.Compromise.Setups
{
    class IdentitySetup
    {
        public static void ConfigureIdentity(IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<DiegoSantanaCalendarDBContext>()
            .AddDefaultTokenProviders();
        }

    }
}
