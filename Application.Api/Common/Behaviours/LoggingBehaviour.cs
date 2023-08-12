using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Api.Common.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogDebug("Request before Executing");
            _logger.LogWarning($"Request before Executing: {typeof(TRequest).Name}");
            return await next();
        }
        finally
        {
            _logger.LogWarning($"Request before Executing: {typeof(TRequest).Name}");
            _logger.LogDebug("Request after Executing");
        }
    }
}
