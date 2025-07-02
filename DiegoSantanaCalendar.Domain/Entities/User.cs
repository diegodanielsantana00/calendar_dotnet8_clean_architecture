using DiegoSantanaCalendar.Domain.Entities.Base;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;



namespace DiegoSantanaCalendar.Domain.Entities
{
    public class User : IdentityUser<Guid>, IBase
    {

        [Required, EmailAddress] // isso DataAnnotations
        public string Email { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
