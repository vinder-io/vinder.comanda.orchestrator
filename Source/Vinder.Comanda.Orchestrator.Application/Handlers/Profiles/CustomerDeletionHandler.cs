namespace Vinder.Comanda.Orchestrator.Application.Handlers.Profiles;

public sealed class CustomerDeletionHandler(IProfilesGateway profilesGateway) :
    IMessageHandler<CustomerDeletionScheme, Result>
{
    public async Task<Result> HandleAsync(
        CustomerDeletionScheme parameters, CancellationToken cancellation = default)
    {
        return await profilesGateway.DeleteCustomerAsync(parameters, cancellation);
    }
}
