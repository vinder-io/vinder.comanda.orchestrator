namespace Vinder.Comanda.Orchestrator.Application.Handlers.Profiles;

public sealed class AssignCustomerAddressHandler(IProfilesGateway profilesGateway) :
    IMessageHandler<AssignCustomerAddressScheme, Result<Address>>
{
    public async Task<Result<Address>> HandleAsync(
        AssignCustomerAddressScheme parameters, CancellationToken cancellation = default)
    {
        return await profilesGateway.AssignCustomerAddressAsync(parameters, cancellation);
    }
}
