using DiegoSantanaCalendar.Domain.Entities;
using DiegoSantanaCalendar.Domain.Interfaces;
using DiegoSantanaCalendar.Infrastructure.Persistence;
using DiegoSantanaCalendar.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DiegoSantanaCalendar.Infrastructure.Repositories
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(DiegoSantanaCalendarDBContext context) : base(context) { }
    }
}
