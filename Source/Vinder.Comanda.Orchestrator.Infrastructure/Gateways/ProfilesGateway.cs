namespace Vinder.Comanda.Orchestrator.Infrastructure.Gateways;

public sealed class ProfilesGateway(ICustomerClient customerClient, IOwnerClient ownerClient, ILogger<IProfilesGateway> logger) : IProfilesGateway
{
    public async Task<Result<CustomerScheme>> CreateCustomerAsync(
        CustomerCreationScheme parameters, CancellationToken cancellation = default)
    {
        // applies a full resiliency pattern for external service calls using
        // timeout, retry, fallback, and circuit breaker policies.

        // more details: https://learn.microsoft.com/dotnet/architecture/resilient-applications/
        var policy = PollyPolicies.CreatePolicy<CustomerScheme>(logger);

        return await policy.ExecuteAsync(token =>
            customerClient.CreateCustomerAsync(parameters, token), cancellation
        );
    }

    public async Task<Result<OwnerScheme>> CreateOwnerAsync(
        OwnerCreationScheme parameters, CancellationToken cancellation = default)
    {
        // applies a full resiliency pattern for external service calls using
        // timeout, retry, fallback, and circuit breaker policies.

        // more details: https://learn.microsoft.com/dotnet/architecture/resilient-applications/
        var policy = PollyPolicies.CreatePolicy<OwnerScheme>(logger);

        return await policy.ExecuteAsync(token =>
            ownerClient.CreateOwnerAsync(parameters, token), cancellation
        );
    }
}