using DiegoSantanaCalendar.Domain.Entities;
using DiegoSantanaCalendar.Domain.Interfaces;
using DiegoSantanaCalendar.Infrastructure.Persistence;
using DiegoSantanaCalendar.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DiegoSantanaCalendar.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DiegoSantanaCalendarDBContext context) : base(context) { }

        public bool CheckUsernameExists(string username)
        {
            return _context.Users.Any(u => u.UserName == username);
        }
    }
}
