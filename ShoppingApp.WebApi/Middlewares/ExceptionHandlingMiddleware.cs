﻿using System.Net;

namespace ShoppingApp.WebApi.Middlewares
{
    // Hata yönetimi için kullanılan middleware sınıfı
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        // Middleware'in yapıcı metodu, next middleware ve logger'ı alır
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, exception.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = new
            {
                message = "An error occurred",
                detail = exception.Message
            };

            return context.Response.WriteAsJsonAsync(result);
        }
    }
}
