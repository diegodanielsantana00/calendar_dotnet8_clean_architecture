using DiegoSantanaCalendar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiegoSantanaCalendar.Application.ViewModels
{
    public class SessionViewModel
    {
        public string Token { get; set; }
        public User User { get; set; }
        public string RuleUser { get; set; }
    }
}
