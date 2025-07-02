using System.Net;
using System.Text.Json;

namespace DiegoSantanaCalendar.API.Template
{
    public enum ErrorType
    {
        None = 0,
        Validation = 1,
        NotFound = 2,
        Unauthorized = 3,
        Forbidden = 4,
        Conflict = 5,
        ServerError = 6,
        BadRequest = 7,
        Timeout = 8,
        ExternalService = 9,
        Unknown = 99
    }

    public class ResponseTemplate<T>
    {


        public bool Success { get; set; }

        public T? Data { get; set; }

        public string Message { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public ErrorType ErrorType { get; set; } = ErrorType.None;

        public List<string> Errors { get; set; } = new();

        public string? Trace { get; set; } = null;


        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

}
