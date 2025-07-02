

using DiegoSantanaCalendar.CrossCutting.Compromise.Setups;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DiegoSantanaCalendar.CrossCutting.Compromise
{
    public static class DependencyInjectionControl
    {
        public static IServiceCollection AddConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            ValidateSetup.ConfigureValidate(services, configuration);
            ConnectionStringDBSetup.ConfigureConnectionStringDB(services, configuration);
            RepositorySetup.ConfigureRepository(services);
            JwtSetup.ConfigureJwt(services, configuration);
            IdentitySetup.ConfigureIdentity(services);
            CorsSetup.ConfigureCORS(services, configuration);
            ServicesSetup.ConfigureServices(services);

            return services;
        }
    }
}
