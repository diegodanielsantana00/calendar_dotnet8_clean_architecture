using DiegoSantanaCalendar.API.Template;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace DiegoSantanaCalendar.API.Filters
{
    public class ValidateFilter<T> : IActionFilter where T : class
    {
        private readonly IValidator<T> _validator;

        public ValidateFilter(IValidator<T> validator)
        {
            _validator = validator;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var argument = context.ActionArguments
                .FirstOrDefault(x => x.Value is T).Value as T;

            if (argument is not null)
            {
                var validationResult = _validator.Validate(argument);
                if (!validationResult.IsValid)
                {

                    var errors = validationResult.Errors
                      .Select(e => $"{e.PropertyName}: {e.ErrorMessage}")
                      .ToList();

                    var response = new ResponseTemplate<object>
                    {
                        Success = false,
                        StatusCode = HttpStatusCode.BadRequest,
                        ErrorType = ErrorType.Validation,
                        Message = "Um ou mais erros de validação ocorreram.",
                        Errors = errors,
                        Trace = null
                    };

                    context.Result = new JsonResult(response)
                    {
                        StatusCode = (int)HttpStatusCode.BadRequest
                    };



                }
            }


        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }

}
