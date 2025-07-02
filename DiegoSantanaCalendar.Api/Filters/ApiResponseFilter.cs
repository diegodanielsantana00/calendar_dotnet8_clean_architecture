using DiegoSantanaCalendar.API.Template;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace DiegoSantanaCalendar.API.Filters
{
    public class ApiResponseFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var executedContext = await next();

            if (executedContext.Result is not ObjectResult objectResult)
                return;

            if (IsAlreadyWrapped(objectResult.Value))
                return;

            var response = CreateResponse(objectResult);

            executedContext.Result = new JsonResult(response)
            {
                StatusCode = objectResult.StatusCode
            };
        }

        private static bool IsAlreadyWrapped(object? value)
        {
            var valueType = value?.GetType();
            return valueType is not null &&
                   valueType.IsGenericType &&
                   valueType.GetGenericTypeDefinition() == typeof(ResponseTemplate<>);
        }

        private static ResponseTemplate<object> CreateResponse(ObjectResult result)
        {
            var statusCode = result.StatusCode ?? 200;
            var isSuccess = statusCode is >= 200 and < 300;

            var response = new ResponseTemplate<object>
            {
                Success = isSuccess,
                StatusCode = (HttpStatusCode)statusCode,
                ErrorType = isSuccess ? ErrorType.None : ErrorType.BadRequest,
                Message = isSuccess ? "Operação realizada com sucesso." : "Erro na operação.",
                Errors = new()
            };

            if (isSuccess)
            {
                response.Data = result.Value;
                return response;
            }

            response = HandleError(result.Value, response);
            return response;
        }

        private static ResponseTemplate<object> HandleError(object? value, ResponseTemplate<object> response)
        {
            switch (value)
            {
                case string message:
                    response.Message = message;
                    response.Errors.Add(message);
                    break;

                case ValidationProblemDetails validation:
                    response.Message = validation.Title ?? "Erro de validação.";
                    response.ErrorType = ErrorType.Validation;
                    response.Errors = validation.Errors.SelectMany(e => e.Value).ToList();
                    break;

                default:
                    response.Data = value;
                    response.Errors.Add("Erro desconhecido.");
                    break;
            }

            return response;
        }

    }
}
