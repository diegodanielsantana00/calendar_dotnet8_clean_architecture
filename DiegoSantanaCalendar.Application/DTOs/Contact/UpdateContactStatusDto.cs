using DiegoSantanaCalendar.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiegoSantanaCalendar.Application.DTOs.Contact
{
    public class UpdateContactStatusDto
    {
        public Guid Id { get; set; }
        public StatusContactEnum NewStatus { get; set; }
    }
}
