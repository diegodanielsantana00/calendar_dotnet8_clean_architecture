using DiegoSantanaCalendar.API.Attributes;
using DiegoSantanaCalendar.Application.DTOs.Contact;
using DiegoSantanaCalendar.Application.Interfaces;
using DiegoSantanaCalendar.Application.ViewModels;
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
        return Ok("Criado com sucesso.");
    }

    [HttpPut("update")]
    [ValidateDto<UpdateContactDTO>]
    public async Task<ActionResult> Update([FromBody] UpdateContactDTO dto)
    {
        await _contactService.Update(dto);
        return Ok("Atualizado com sucesso");
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        await _contactService.Delete(id);
        return Ok("Deletado com sucesso.");
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
        var idUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var jobFunctions = await _contactService.GetAllByIdUser(new Guid(idUser));
        return Ok(jobFunctions);
    }

    [HttpGet("getStats")]
    public ActionResult<IEnumerable<ContactStats>> getStats()
    {
        var mockStats = new List<ContactStats>
    {
        new ContactStats { Month = "Janeiro", Count = 10 },
        new ContactStats { Month = "Fevereiro", Count = 15 },
        new ContactStats { Month = "Março", Count = 8 },
        new ContactStats { Month = "Abril", Count = 20 },
        new ContactStats { Month = "Maio", Count = 12 }
    };

        return Ok(mockStats);
    }

    [HttpPatch("updateStatus")]
    [ValidateDto<UpdateContactStatusDto>]
    public async Task<ActionResult> UpdateStatus([FromBody] UpdateContactStatusDto dto)
    {
        await _contactService.UpdateStatusAsync(dto);
        return Ok("Solicitação de atualização de status recebida e em processamento.");
    }




}