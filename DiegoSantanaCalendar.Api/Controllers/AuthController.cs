using DiegoSantanaCalendar.API.Attributes;
using DiegoSantanaCalendar.Application.DTOs.Auth;
using DiegoSantanaCalendar.Application.Interfaces;
using DiegoSantanaCalendar.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{

    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    [ValidateDto<LoginDTO>]
    public async Task<ActionResult<SessionViewModel>> Login([FromBody] LoginDTO body)
    {
        try
        {
            var session = await _authService.Login(username: body.Username, password: body.Password);
            return Ok(session);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);

        }
    }

    [HttpPost("register")]
    [ValidateDto<RegisterDTO>]
    public async Task<ActionResult> Register([FromBody] RegisterDTO body)
    {

        try
        {
            await _authService.Register(email: body.Email, password: body.Password, username: body.Username, role: body.Role);
            return Ok("Usuário registrado com sucesso");
        }
        catch (Exception ex)
        {
            return Problem(ex.Message);

        }
            
    }

}
