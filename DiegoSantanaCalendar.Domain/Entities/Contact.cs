using DiegoSantanaCalendar.Domain.Entities.Base;
using DiegoSantanaCalendar.Domain.Enum;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;



namespace DiegoSantanaCalendar.Domain.Entities
{
    public class Contact : BaseEntities
    {

        [Required]
        public string Name { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required, Phone]
        public string Phone { get; set; } = null!;

        [Required]
        public User User { get; set; } = null!;
        public Guid UserId { get; set; }

        [Required]
        public StatusContactEnum StatusContactEnum { get; set; } = StatusContactEnum.Active;

    }
}
