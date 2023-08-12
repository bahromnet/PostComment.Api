using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Api.Common.Behaviours;
public class LoggingBehaviourPost<TRequest, TRespons> : IRequestPostProcessor<TRequest, TRespons>
    where TRequest : IRequest<TRespons>
{
    private readonly ILogger<LoggingBehaviourPost<TRequest, TRespons>> _logger;

    public LoggingBehaviourPost(ILogger<LoggingBehaviourPost<TRequest, TRespons>> logger)
    {
        _logger = logger;
    }

    public Task Process(TRequest request, TRespons response, CancellationToken cancellationToken)
    {
        _logger.LogWarning($"Used this request after posting: {typeof(TRequest).Name}");
        return Task.CompletedTask;
    }
}
