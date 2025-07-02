using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiegoSantanaCalendar.CrossCutting.Compromise.Setups
{
    class CorsSetup
    {
        public static void ConfigureCORS(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options => {
                options.AddPolicy("CorsDev", builder => {
                    builder.SetIsOriginAllowed(_ => true)
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();


                });

                string ValidUrl = configuration["Jwt:Audience"] ?? throw new Exception("ValidAudience invalida");

                options.AddPolicy("CorsProd", builder => {
                    builder.WithOrigins(ValidUrl)
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
            });
        }
    }
}
