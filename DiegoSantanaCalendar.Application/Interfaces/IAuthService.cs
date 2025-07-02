using DiegoSantanaCalendar.Application.DTOs.Auth;
using DiegoSantanaCalendar.Application.ViewModels;
using DiegoSantanaCalendar.Domain.Entities;

namespace DiegoSantanaCalendar.Application.Interfaces
{
    public interface IAuthService
    {
        Task<SessionViewModel> Login(string username, string password);
        Task Register(string username, string email, string password, string role);
        Task Update(Guid id, string email, string role);
        Task CreateRoleAsync(string roleName);
        Task AddUserToRoleAsync(string email, string roleName);
        Task RemoveUserFromRoleAsync(string email, string roleName);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(Guid uid);
        Task<IEnumerable<string>> GetAllRules();
        Task<string> GetRuleByUserId(Guid uid);
        Task Delete(Guid uid);
    }
}
