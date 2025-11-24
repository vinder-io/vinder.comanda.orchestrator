namespace Vinder.Comanda.Orchestrator.Infrastructure.Gateways;

public sealed class ProfilesGateway(ICustomerClient customerClient, IOwnerClient ownerClient, ILogger<IProfilesGateway> logger) : IProfilesGateway
{
    private readonly AsyncPolicyWrap<Result<CustomerScheme>> _customerPolicy =
        PollyPolicies.CreatePolicy<CustomerScheme>(logger);

    private readonly AsyncPolicyWrap<Result<OwnerScheme>> _ownerPolicy =
        PollyPolicies.CreatePolicy<OwnerScheme>(logger);

    private readonly AsyncPolicyWrap<Result<PaginationScheme<OwnerScheme>>> _fetchOwnersPolicy =
        PollyPolicies.CreatePolicy<PaginationScheme<OwnerScheme>>(logger);

    private readonly AsyncPolicyWrap<Result<OwnerScheme>> _editOwnerPolicy =
        PollyPolicies.CreatePolicy<OwnerScheme>(logger);

    private readonly AsyncPolicyWrap<Result> _deleteOwnerPolicy =
        PollyPolicies.CreatePolicy(logger);

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

    public async Task<Result<PaginationScheme<OwnerScheme>>> GetOwnersAsync(
        FetchOwnersParameters parameters, CancellationToken cancellation = default)
    {
        // applies a full resiliency pattern for external service calls using
        // timeout, retry, fallback, and circuit breaker policies.

        // more details: https://learn.microsoft.com/dotnet/architecture/resilient-applications/

        return await _fetchOwnersPolicy.ExecuteAsync(token =>
            ownerClient.GetOwnersAsync(parameters, token), cancellation
        );
    }

    public async Task<Result<OwnerScheme>> UpdateOwnerAsync(
        EditOwnerScheme parameters, CancellationToken cancellation = default)
    {
        // applies a full resiliency pattern for external service calls using
        // timeout, retry, fallback, and circuit breaker policies.

        // more details: https://learn.microsoft.com/dotnet/architecture/resilient-applications/

        return await _editOwnerPolicy.ExecuteAsync(token =>
            ownerClient.UpdateOwnerAsync(parameters, token), cancellation
        );
    }

    public async Task<Result> DeleteOwnerAsync(
        OwnerDeletionScheme parameters, CancellationToken cancellation = default)
    {
        // applies a full resiliency pattern for external service calls using
        // timeout, retry, fallback, and circuit breaker policies.

        // more details: https://learn.microsoft.com/dotnet/architecture/resilient-applications/
        return await _deleteOwnerPolicy.ExecuteAsync(token =>
            ownerClient.DeleteOwnerAsync(parameters, token), cancellation
        );
    }
}