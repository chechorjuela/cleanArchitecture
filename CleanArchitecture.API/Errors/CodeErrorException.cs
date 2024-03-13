using System.Net;

namespace CleanArchitecture.API.Errors
{
    public class CodeErrorException : CodeErrorResponse
    {
        public string? Details { get; set; }
        public string Apocalypse
        public CodeErrorException(int statusCode, string? message = null, string? details = null) : base(statusCode, message)
        {
            Details = details;
        }
    }
}
