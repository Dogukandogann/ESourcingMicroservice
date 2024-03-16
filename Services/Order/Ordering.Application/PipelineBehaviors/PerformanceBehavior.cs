using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.PipelineBehaviors
{
    public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull, IRequest<TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TResponse> _logger;
        public PerformanceBehavior(ILogger<TResponse> logger)
        {
            _logger = logger;
            _timer = new Stopwatch();
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();
            var response = await next();
            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;
            if (elapsedMilliseconds > 500) 
            {
                var requestName = typeof(TRequest).Name;

                _logger.LogWarning("Long running request :{requestName} ({ElapsedMilliseconds} miliseconds {@Request})", requestName, elapsedMilliseconds, request);
            }
            return response;
        }

        
    }
}
