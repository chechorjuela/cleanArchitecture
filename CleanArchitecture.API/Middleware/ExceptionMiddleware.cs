﻿using CleanArchitecture.API.Errors;
using System.Net;
using System.Text.Json;

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
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment()
                    ? new CodeErrorException((int)HttpStatusCode.InternalServerError, exp.Message, exp.StackTrace)
                    : new CodeErrorException((int)HttpStatusCode.InternalServerError);
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var jsonData = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(jsonData);
            }
        }

    }
}