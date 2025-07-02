using DiegoSantanaCalendar.Application.Interfaces;
using DiegoSantanaCalendar.Application.Services;
using DiegoSantanaCalendar.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiegoSantanaCalendar.CrossCutting.Compromise.Setups
{
    class ConnectionStringDBSetup
    {
        public static void ConfigureConnectionStringDB(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DatabaseDiegoSantanaCalendar");
            services.AddDbContext<DiegoSantanaCalendarDBContext>(options =>
            {
                options.UseNpgsql(connectionString);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
        }
    }
}
