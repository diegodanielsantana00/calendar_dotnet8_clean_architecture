using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiegoSantanaCalendar.Domain.Entities.Base
{
    public interface IBase
    {
        Guid Id { get; }
        DateTime CreatedAt { get; set; }
    }
}
