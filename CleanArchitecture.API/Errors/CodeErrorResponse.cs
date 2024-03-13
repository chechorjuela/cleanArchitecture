using System.Net;

namespace CleanArchitecture.API.Errors
{
    public class CodeErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }

        public CodeErrorResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageStatusCode(statusCode);
        }

        private string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                (int)HttpStatusCode.BadRequest => "El request tiene errores",
                (int)HttpStatusCode.Unauthorized => "No tienes Authorizaciones para este recurso",
                (int)HttpStatusCode.NotFound => "No se encontro este recurso",
                (int)HttpStatusCode.InternalServerError => "se produjo error en el servidor",
                _ => string.Empty
            };
        }
    }
}
