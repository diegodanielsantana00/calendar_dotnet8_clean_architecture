using DiegoSantanaCalendar.API.Attributes;
using DiegoSantanaCalendar.Application.DTOs.Contact;
using DiegoSantanaCalendar.Application.Interfaces;
using DiegoSantanaCalendar.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrador, Membro")]
public class ContactController : ControllerBase
{
    private readonly IContactService _contactService;

    public ContactController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpPost("create")]
    [ValidateDto<CreateContactDTO>]
    public async Task<ActionResult> Create([FromBody] CreateContactDTO dto)
    {
        var idUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _contactService.Create(dto, new Guid(idUser));
        return Ok();
    }

    [HttpPut("update")]
    [ValidateDto<UpdateContactDTO>]
    public async Task<ActionResult> Update([FromBody] UpdateContactDTO dto)
    {
        await _contactService.Update(dto);
        return Ok();
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _contactService.Delete(id);
        return Ok();
    }

    [HttpGet("getById/{id}")]
    public async Task<ActionResult<Contact>> GetById(Guid id)
    {
        var jobFunction = await _contactService.GetById(id);
        if (jobFunction == null)
            return NotFound();
        return Ok(jobFunction);
    }

    [HttpGet("getAll")]
    public async Task<ActionResult<IEnumerable<Contact>>> GetAll()
    {
        var jobFunctions = await _contactService.GetAll();
        return Ok(jobFunctions);
    }
}