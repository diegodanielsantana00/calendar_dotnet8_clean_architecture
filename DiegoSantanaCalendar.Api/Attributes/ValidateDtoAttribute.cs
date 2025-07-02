using DiegoSantanaCalendar.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DiegoSantanaCalendar.API.Attributes
{
    public class ValidateDtoAttribute<T> : TypeFilterAttribute where T : class
    {
        public ValidateDtoAttribute() : base(typeof(ValidateFilter<T>))
        {
        }
    }
}
