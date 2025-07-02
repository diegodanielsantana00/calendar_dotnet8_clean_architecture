using DiegoSantanaCalendar.Application.Interfaces;
using DiegoSantanaCalendar.Application.Utils;
using DiegoSantanaCalendar.Application.ViewModels;
using DiegoSantanaCalendar.Domain.Entities;
using DiegoSantanaCalendar.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace DiegoSantanaCalendar.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IJWTService _jwtService;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public AuthService(
            IJWTService jwtService,
            UserManager<User> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            IUserRepository userRepository)
        {
            _jwtService = jwtService;
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
        }


        public async Task<SessionViewModel> Login(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user is null)
                throw new KeyNotFoundException("Usuário não encontrado");

            var isValidPassword = await _userManager.CheckPasswordAsync(user, password);
            if (!isValidPassword) throw new UnauthorizedAccessException("Credenciais inválidas.");

            var userRoles = await _userManager.GetRolesAsync(user);
            await _userManager.UpdateAsync(user);
            var result = new SessionViewModel()
            {
                Token = await _jwtService.GenerateAccessToken(user.Id, userRoles.FirstOrDefault()),
                User = user,
                RuleUser = userRoles.FirstOrDefault()
            };
            return result;

        }

        public async Task Register(string username, string email, string password, string role)
        {
            var userExists = await _userManager.FindByEmailAsync(email);
            if (userExists != null)
                throw new InvalidOperationException("Usuário já existe");

            User userSystem = new User()
            {
                Email = email,
                NormalizedEmail = email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = username,
            };

            await this.CreateRoleAsync("Membro");
            await this.CreateRoleAsync("Administrador");
            var result = await _userManager.CreateAsync(userSystem, password);
            

            if (!result.Succeeded)
                throw new InvalidOperationException("Falha na criação do usuário.");
            else
                await this.AddUserToRoleAsync(userSystem.Id.ToString(), role);
        }

        public async Task CreateRoleAsync(string roleName)
        {
            var roleExist = await _roleManager.RoleExistsAsync(roleName);
            if (roleExist) return;

            var roleResult = await _roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
            if (!roleResult.Succeeded)
                throw new ApplicationException($"Erro ao criar a role '{roleName}'.");
        }


        public async Task AddUserToRoleAsync(string uid, string roleName)
        {
            var user = await _userManager.FindByIdAsync(uid);
            if (user == null)
                throw new KeyNotFoundException($"Usuário com o ID '{uid}' não encontrado.");

            var result = await _userManager.AddToRoleAsync(user, roleName);
            if (!result.Succeeded)
                throw new ApplicationException($"Falha ao adicionar o usuário '{uid}' à role '{roleName}'.");
        }

        public async Task RemoveUserFromRoleAsync(string email, string roleName)
        {

            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw new InvalidOperationException($"Usuário com o email '{email}' não encontrado.");

            var result = await _userManager.RemoveFromRoleAsync(user, roleName);
            if (!result.Succeeded)
                throw new ApplicationException($"Falha ao remover o usuário '{email}' da role '{roleName}'");

        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetById(Guid uid)
        {
            return await _userRepository.GetByIdAsync(uid);
        }

        public async Task<IEnumerable<string>> GetAllRules()
        {
            return await Task.FromResult(_roleManager.Roles.Select(r => r.Name).ToList());
        }

        public async Task<string> GetRuleByUserId(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                throw new KeyNotFoundException($"Usuário com o ID '{id}' não encontrado.");
            var userRoles = await _userManager.GetRolesAsync(user);

            return userRoles.SingleOrDefault();
        }

        public async Task Delete(Guid uid)
        {
            var user = await _userManager.FindByIdAsync(uid.ToString());
            if (user == null)
                throw new KeyNotFoundException($"Usuário com o ID '{uid}' não encontrado.");
            await _userManager.DeleteAsync(user);
        }

        public async Task Update(Guid id, string email, string role)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                throw new KeyNotFoundException($"Usuário com o ID '{id}' não encontrado.");
            user.Email = email;
            user.NormalizedEmail = email.ToUpper();

            var currentRoles = await _userManager.GetRolesAsync(user);
            if (currentRoles.Any())
                await _userManager.RemoveFromRolesAsync(user, currentRoles);

            await _userManager.AddToRoleAsync(user, role);
            
        }
    }
}
