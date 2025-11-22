namespace Vinder.Comanda.Orchestrator.Infrastructure.Gateways;

public sealed class ProfilesGateway(ICustomerClient customerClient, IOwnerClient ownerClient, ILogger<IProfilesGateway> logger) : IProfilesGateway
{
    private readonly AsyncPolicyWrap<Result<CustomerScheme>> _customerPolicy =
        PollyPolicies.CreatePolicy<CustomerScheme>(logger);

    private readonly AsyncPolicyWrap<Result<OwnerScheme>> _ownerPolicy =
        PollyPolicies.CreatePolicy<OwnerScheme>(logger);

    public async Task<Result<CustomerScheme>> CreateCustomerAsync(
        CustomerCreationScheme parameters, CancellationToken cancellation = default)
    {
        // applies a full resiliency pattern for external service calls using
        // timeout, retry, fallback, and circuit breaker policies.

        // more details: https://learn.microsoft.com/dotnet/architecture/resilient-applications/

        return await _customerPolicy.ExecuteAsync(token =>
            customerClient.CreateCustomerAsync(parameters, token), cancellation
        );
    }

    public async Task<Result<OwnerScheme>> CreateOwnerAsync(
        OwnerCreationScheme parameters, CancellationToken cancellation = default)
    {
        // applies a full resiliency pattern for external service calls using
        // timeout, retry, fallback, and circuit breaker policies.

        // more details: https://learn.microsoft.com/dotnet/architecture/resilient-applications/

        return await _ownerPolicy.ExecuteAsync(token =>
            ownerClient.CreateOwnerAsync(parameters, token), cancellation
        );
    }
}