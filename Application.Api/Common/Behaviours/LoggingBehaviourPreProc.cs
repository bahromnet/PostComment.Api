using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Api.Common.Behaviours;
public class LoggingBehaviourPreProc<TRequest> : IRequestPreProcessor<TRequest>
    where TRequest : notnull
{
    private readonly ILogger<LoggingBehaviourPreProc<TRequest>> _logger;

    public LoggingBehaviourPreProc(ILogger<LoggingBehaviourPreProc<TRequest>> logger)
    {
        _logger = logger;
    }

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        _logger.LogWarning($"Coming request PreProcessor: {typeof(TRequest).Name}");
        return Task.CompletedTask;
    }
}
