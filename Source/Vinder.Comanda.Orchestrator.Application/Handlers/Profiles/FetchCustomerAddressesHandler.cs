namespace Vinder.Comanda.Orchestrator.Application.Handlers.Profiles;

public sealed class FetchCustomerAddressesHandler(IProfilesGateway profilesGateway) :
    IMessageHandler<FetchCustomerAddressesParameters, Result<IReadOnlyCollection<Address>>>
{
    public async Task<Result<IReadOnlyCollection<Address>>> HandleAsync(
        FetchCustomerAddressesParameters parameters, CancellationToken cancellation = default)
    {
        return await profilesGateway.GetCustomerAddressAsync(parameters, cancellation);
    }
}
