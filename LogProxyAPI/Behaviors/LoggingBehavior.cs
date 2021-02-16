using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LogProxyAPI.Behaviors
{
    internal class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation("Handling {Request}", typeof(TRequest).Name);
            try
            {
                var response = await next();
                _logger.LogInformation("Handled {Request}", typeof(TRequest).Name);
                return response;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Request} Failed", typeof(TRequest).Name);
                throw;
            }
        }
    }
}
