using DiegoSantanaCalendar.Domain.Entities;
using DiegoSantanaCalendar.Domain.Interfaces.Base;

namespace DiegoSantanaCalendar.Domain.Interfaces
{
   public interface IContactRepository : IBaseRepository<Contact>
    {
        Task<IEnumerable<Contact>> GetAllByIdUser(Guid idUser);

    }
}
