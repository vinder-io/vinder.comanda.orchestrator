namespace Vinder.Comanda.Orchestrator.Infrastructure.Policies;

public static class PollyPolicies
{
    public static AsyncPolicyWrap<Result<TResult>> CreatePolicy<TResult>(ILogger logger)
    {
        var timeoutPolicy = Policy.TimeoutAsync<Result<TResult>>(TimeSpan.FromSeconds(20));
        var retryPolicy = Policy<Result<TResult>>
            .Handle<Exception>()
            .WaitAndRetryAsync(2, _ => TimeSpan.FromSeconds(2));

        var circuitBreakerPolicy = Policy.Handle<Exception>()
            .CircuitBreakerAsync(exceptionsAllowedBeforeBreaking: 3, durationOfBreak: TimeSpan.FromSeconds(20),
                onBreak: (exception, breakDelay) =>
                {
                    logger.LogInformation("Circuit Breaker opened for {Seconds}s due to: {Message}", breakDelay.TotalSeconds, exception.Message);
                },
                onReset: () =>
                {
                    logger.LogInformation("Circuit Breaker closed — back to normal");
                },
                onHalfOpen: () =>
                {
                    logger.LogInformation("Circuit Breaker half-open — testing next call");
                }
            );

        var fallbackPolicy = Policy<Result<TResult>>.Handle<Exception>()
            .FallbackAsync(
                fallbackAction: (cancellationToken) =>
                {
                    var result = Result<TResult>.Failure(CommonErrors.OperationFailed);
                    return Task.FromResult(result);
                }
            );

        return Policy.WrapAsync<Result<TResult>>(
            fallbackPolicy,
            retryPolicy,
            circuitBreakerPolicy.AsAsyncPolicy<Result<TResult>>(),
            timeoutPolicy
        );
    }
}