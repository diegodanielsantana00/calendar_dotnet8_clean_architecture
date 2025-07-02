using DiegoSantanaCalendar.Domain.Entities;
using DiegoSantanaCalendar.Domain.Interfaces.Base;

namespace DiegoSantanaCalendar.Domain.Interfaces
{
   public interface IUserRepository : IBaseRepository<User>
    {
        bool CheckUsernameExists(string username);
    }
}
