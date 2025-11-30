namespace Vinder.Comanda.Orchestrator.Application.Handlers.Profiles;

public sealed class DeleteCustomerAddressHandler(IProfilesGateway profilesGateway) :
    IMessageHandler<DeleteCustomerAddressScheme, Result>
{
    public async Task<Result> HandleAsync(
        DeleteCustomerAddressScheme parameters, CancellationToken cancellation = default)
    {
        return await profilesGateway.DeleteCustomerAddressAsync(parameters, cancellation);
    }
}
