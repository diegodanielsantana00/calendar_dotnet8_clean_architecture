using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DiegoSantanaCalendar.Domain.Entities.Base
{
    public abstract class BaseEntities : IBase
    {
        public Guid Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
