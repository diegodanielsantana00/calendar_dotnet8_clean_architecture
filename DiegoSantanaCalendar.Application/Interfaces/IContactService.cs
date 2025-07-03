

using DiegoSantanaCalendar.Application.DTOs.Contact;
using DiegoSantanaCalendar.Domain.Entities;

namespace DiegoSantanaCalendar.Application.Interfaces
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAll();
        Task<Contact> GetById(Guid id);
        Task Create(CreateContactDTO dto, Guid idUser);
        Task Update(UpdateContactDTO dto);
        Task Delete(Guid id);
        Task UpdateStatusAsync(UpdateContactStatusDto dto);

    }
}
