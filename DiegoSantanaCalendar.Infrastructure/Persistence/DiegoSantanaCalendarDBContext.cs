using DiegoSantanaCalendar.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DiegoSantanaCalendar.Infrastructure.Persistence
{
    public class DiegoSantanaCalendarDBContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DiegoSantanaCalendarDBContext() { }

        public DiegoSantanaCalendarDBContext(DbContextOptions<DiegoSantanaCalendarDBContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

    }
}
