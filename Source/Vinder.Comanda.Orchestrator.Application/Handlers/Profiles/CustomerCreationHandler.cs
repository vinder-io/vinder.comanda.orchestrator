namespace Vinder.Comanda.Orchestrator.Application.Handlers.Profiles;

public sealed class CustomerCreationHandler(IProfilesGateway profilesGateway) :
    IMessageHandler<CustomerCreationScheme, Result<CustomerScheme>>
{
    public async Task<Result<CustomerScheme>> HandleAsync(
        CustomerCreationScheme parameters, CancellationToken cancellation = default)
    {
        return await profilesGateway.CreateCustomerAsync(parameters, cancellation);
    }
}