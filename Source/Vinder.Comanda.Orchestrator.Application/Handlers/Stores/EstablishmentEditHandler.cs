namespace Vinder.Comanda.Orchestrator.Application.Handlers.Stores;

public sealed class EstablishmentEditHandler(IEstablishmentGateway establishmentGateway) :
    IMessageHandler<EstablishmentModificationScheme, Result<EstablishmentScheme>>
{
    public async Task<Result<EstablishmentScheme>> HandleAsync(
        EstablishmentModificationScheme parameters, CancellationToken cancellation = default)
    {
        return await establishmentGateway.UpdateEstablishmentAsync(parameters, cancellation);
    }
}
