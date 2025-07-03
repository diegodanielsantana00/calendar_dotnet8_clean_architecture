using DiegoSantanaCalendar.Application.DTOs.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiegoSantanaCalendar.Application.Interfaces
{
    public interface IMessagePublisher
    {
        void PublishUpdateContactStatus(UpdateContactStatusDto message);
    }
}
