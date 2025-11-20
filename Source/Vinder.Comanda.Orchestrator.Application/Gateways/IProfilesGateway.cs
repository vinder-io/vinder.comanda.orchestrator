namespace Vinder.Comanda.Orchestrator.Application.Gateways;

public interface IProfilesGateway
{
    public Task<Result<CustomerScheme>> CreateCustomerAsync(
        CustomerCreationScheme parameters,
        CancellationToken cancellation = default
    );
}