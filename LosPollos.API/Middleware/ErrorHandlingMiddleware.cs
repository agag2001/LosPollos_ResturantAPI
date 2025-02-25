
using LosPollos.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.Runtime.InteropServices;

namespace LosPollos.API.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {

                await next.Invoke(context);   

            }
            
            catch(NotFoundException notFound)
            {
                _logger.LogWarning(notFound, notFound.Message);
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                await context.Response.WriteAsync(notFound.Message);        
            }
            catch (ForbidException forbid)
            {
                _logger.LogWarning(forbid, forbid.Message);
                context.Response.StatusCode = StatusCodes.Status403Forbidden;   
                await context.Response.WriteAsync(forbid.Message);      
            }
            catch(UserException userFaild)
            {
                _logger.LogWarning(userFaild, userFaild.Message);   
                context.Response.StatusCode =  StatusCodes.Status400BadRequest;   
                await context.Response.WriteAsync(userFaild.Message);       
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("Something went wrong");
               

            }
            
        }
    }
}
