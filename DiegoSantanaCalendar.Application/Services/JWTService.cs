using DiegoSantanaCalendar.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace DiegoSantanaCalendar.Application.Services
{
    public class JWTService : IJWTService 
    {
        private readonly IConfiguration _configuration;
        public JWTService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<string> GenerateAccessToken(Guid idUser, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var securityKey = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, idUser.ToString()),
                    new Claim(ClaimTypes.Role, role)
                }),
                Issuer = issuer,
                Audience = audience,
                Expires = DateTime.Now.AddHours(8),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(securityKey),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var stringToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

            return stringToken;
        }

    }
}
