using DiegoSantanaCalendar.API.Attributes;
using DiegoSantanaCalendar.Application.DTOs;
using DiegoSantanaCalendar.Application.Interfaces;
using DiegoSantanaCalendar.Application.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador")]
public class RoleController : ControllerBase
{
    private readonly IAuthService _authService;

    public RoleController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreateRole(string roleName)
    {
        await _authService.CreateRoleAsync(roleName: roleName);
        return Created();
    }

    [HttpPost("addUser")]
    public async Task<IActionResult> AddUserToRole(string email, string roleName)
    {

        await _authService.AddUserToRoleAsync(email: email, roleName: roleName);

        return Ok();
    }

    [HttpGet("getAll")]
    public async Task<ActionResult<List<string>>> GetAll()
    {
        try
        {
            var listRules = _authService.GetAllRules();
            return Ok(listRules);
        }
        catch (Exception)
        {
            return BadRequest();
        }

        
    }

}
