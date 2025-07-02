using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace DiegoSantanaCalendar.Application.Interfaces
{
    public interface IJWTService
    {
        Task<string> GenerateAccessToken(Guid idUser, string role);

    }
}
