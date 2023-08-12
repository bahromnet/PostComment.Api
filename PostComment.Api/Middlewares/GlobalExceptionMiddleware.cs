using Application.Api.Common.Notification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PostComment.Api.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMediator _mediator;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        public GlobalExceptionMiddleware(RequestDelegate requestDelegate, ILogger<GlobalExceptionMiddleware> logger, IMediator mediator)
        {
            _next = requestDelegate;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await _mediator.Publish(new ExceptionNotification()
                {
                    ExceptionString = ex.Message
                });
            }
        }
        public async ValueTask<ActionResult> HandleException(HttpContext httpContext, string exMessage, HttpStatusCode httpStatusCode, string message)
        {
            _logger.LogCritical(message);
            HttpResponse response = httpContext.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)httpStatusCode;

            var error = new
            {
                Message = message,
                StatusCode = (int)httpStatusCode
            };

            return await Task.FromResult(new BadRequestObjectResult(error));

        }
    }
    public static class GlobalExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }
}
