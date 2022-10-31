using System.Diagnostics;

namespace SchoolAPI.Middleware
{
    public class RequestExecutionTimeMiddleware : IMiddleware
    {
        private readonly ILogger<RequestExecutionTimeMiddleware> _logger;
        private Stopwatch _stopwatch;

        public RequestExecutionTimeMiddleware(ILogger<RequestExecutionTimeMiddleware> logger)
        {
            _logger = logger;
            _stopwatch = new Stopwatch();
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopwatch.Start();
            await next.Invoke(context);
            _stopwatch.Stop();

            var miliseconds = _stopwatch.ElapsedMilliseconds;

            if(miliseconds > 4000)
            {
                _logger.LogInformation($"Request  execution time was too long: {miliseconds}");
            }
        }

    }
}
