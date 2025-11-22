namespace Vinder.Comanda.Orchestrator.Infrastructure.Gateways;

public sealed class ProfilesGateway(ICustomerClient customerClient, IOwnerClient ownerClient) : IProfilesGateway
{
    public async Task<Result<CustomerScheme>> CreateCustomerAsync(
        CustomerCreationScheme parameters, CancellationToken cancellation = default)
    {
        return await customerClient.CreateCustomerAsync(parameters, cancellation);
    }

    public async Task<Result<OwnerScheme>> CreateOwnerAsync(
        OwnerCreationScheme parameters, CancellationToken cancellation = default)
    {
        return await ownerClient.CreateOwnerAsync(parameters, cancellation);
    }
}