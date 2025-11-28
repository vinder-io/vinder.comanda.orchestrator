namespace Vinder.Comanda.Orchestrator.Application.Handlers.Profiles;

public sealed class EditCustomerHandler(IProfilesGateway profilesGateway) :
    IMessageHandler<EditCustomerScheme, Result<CustomerScheme>>
{
    public async Task<Result<CustomerScheme>> HandleAsync(
        EditCustomerScheme parameters, CancellationToken cancellation = default)
    {
        return await profilesGateway.EditCustomerAsync(parameters, cancellation);
    }
}
