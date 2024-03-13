using CleanArchitecture.API.Errors;
using CleanArchitecture.Application.Exceptions;
using Newtonsoft.Json;
using System.Net;

namespace CleanArchitecture.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context) {
            try {
                await _next(context);
            }catch(Exception exp) {
                _logger.LogError(exp, exp.Message);
                context.Response.ContentType = "application/json";
                var statusCode = (int)HttpStatusCode.InternalServerError;

                var result = string.Empty;

                switch (exp) {
                    case NotFoundException notFoundException:
                        statusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case ValidationException validationException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        var validationJson = JsonConvert.SerializeObject(validationException.Errors);
                        result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, exp.Message, validationJson));
                        break;
                    case BadRequestException badRequestException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        break;
                }
                
                if (string.IsNullOrEmpty(result)) {
                    result = JsonConvert.SerializeObject(new CodeErrorException(statusCode, exp.Message, exp.StackTrace));
                }

                //var response = _env.IsDevelopment()
                //    ? new CodeErrorException((int)HttpStatusCode.InternalServerError, exp.Message, exp.StackTrace)
                //    : new CodeErrorException((int)HttpStatusCode.InternalServerError);
                //var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                //var jsonData = JsonSerializer.Serialize(response, options);

                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsync(result);
            }
        }

    }
}
