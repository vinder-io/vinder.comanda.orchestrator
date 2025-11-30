namespace Vinder.Comanda.Orchestrator.Application.Handlers.Profiles;

public sealed class EditCustomerAddressHandler(IProfilesGateway profilesGateway) :
    IMessageHandler<EditCustomerAddressScheme, Result<Address>>
{
    public async Task<Result<Address>> HandleAsync(
        EditCustomerAddressScheme parameters, CancellationToken cancellation = default)
    {
        return await profilesGateway.EditCustomerAddressAsync(parameters, cancellation);
    }
}
