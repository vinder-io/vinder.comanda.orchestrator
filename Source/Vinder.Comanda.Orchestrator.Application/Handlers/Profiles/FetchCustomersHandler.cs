namespace Vinder.Comanda.Orchestrator.Application.Handlers.Profiles;

public sealed class FetchCustomersHandler(IProfilesGateway profilesGateway) :
    IMessageHandler<FetchCustomersParameters, Result<PaginationScheme<CustomerScheme>>>
{
    public async Task<Result<PaginationScheme<CustomerScheme>>> HandleAsync(
        FetchCustomersParameters parameters, CancellationToken cancellation = default)
    {
        return await profilesGateway.GetCustomersAsync(parameters, cancellation);
    }
}
