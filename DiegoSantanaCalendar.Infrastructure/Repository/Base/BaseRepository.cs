using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DiegoSantanaCalendar.Domain.Entities.Base;
using DiegoSantanaCalendar.Domain.Interfaces.Base;
using DiegoSantanaCalendar.Infrastructure.Persistence;

namespace DiegoSantanaCalendar.Infrastructure.Repositories.Base
{
    public class BaseRepository<T>  : IBaseRepository<T> where T : class, IBase
    {
        protected readonly DiegoSantanaCalendarDBContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(DiegoSantanaCalendarDBContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task AddWithoutSaveChangesAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
