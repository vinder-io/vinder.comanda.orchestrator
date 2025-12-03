namespace Vinder.Comanda.Orchestrator.Application.Handlers.Stores;

public sealed class EstablishmentsFetchHandler(IEstablishmentGateway establishmentGateway) :
    IMessageHandler<EstablishmentsFetchParameters, Result<PaginationScheme<EstablishmentScheme>>>
{
    public async Task<Result<PaginationScheme<EstablishmentScheme>>> HandleAsync(
        EstablishmentsFetchParameters parameters, CancellationToken cancellation = default)
    {
        return await establishmentGateway.GetEstablishmentsAsync(parameters, cancellation);
    }
}
