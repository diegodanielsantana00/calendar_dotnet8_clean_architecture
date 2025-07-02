using DiegoSantanaCalendar.Application.Interfaces;
using DiegoSantanaCalendar.Application.Services;
using DiegoSantanaCalendar.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace DiegoSantanaCalendar.CrossCutting.Compromise.Setups
{
    public static class JwtSetup 
    {
        public static void ConfigureJwt(IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "Bearer";
                options.DefaultChallengeScheme = "Bearer";
            })
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Key"])
                    ),
                    RoleClaimType = ClaimTypes.Role
                };
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy =>
                    policy.RequireRole("Administrador"));

                options.AddPolicy("RequireFinancialAnalystRole", policy =>
                    policy.RequireRole("Analista Financeiro"));

                options.AddPolicy("RequireCollaboratorRole", policy =>
                    policy.RequireRole("Colaborador"));

                //options.AddPolicy("FinancialOrAdmin", policy =>
                //    policy.RequireRole("Analista Financeiro", "Administrador"));

                options.AddPolicy("AuthenticatedUser", policy =>
                    policy.RequireAuthenticatedUser());

                options.AddPolicy("HasUserIdClaim", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("idUser");
                });

            });
        }
    }
}