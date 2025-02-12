
using System.Diagnostics;

namespace LosPollos.API.Middleware
{
    public class RequestTimeLoggingMiddleware : IMiddleware
    {
        private readonly ILogger<RequestTimeLoggingMiddleware> _logger;
        public RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stopwatch =  Stopwatch.StartNew();
            await next.Invoke(context);   
            stopwatch.Stop();   
            if(stopwatch.ElapsedMilliseconds/1000>4)
            {
                _logger.LogInformation("Request [{verb}] at path {Path} take time {Time} ms",
                    context.Request.Method,
                    context.Request.Path,
                    stopwatch.ElapsedMilliseconds
                );
                

            }
        }
    }
}
