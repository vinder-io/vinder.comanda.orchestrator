namespace Vinder.Comanda.Orchestrator.Application.Handlers.Stores;

public sealed class EstablishmentDeletionHandler(IEstablishmentGateway establishmentGateway) :
    IMessageHandler<EstablishmentDeletionScheme, Result>
{
    public async Task<Result> HandleAsync(
        EstablishmentDeletionScheme parameters, CancellationToken cancellation = default)
    {
        return await establishmentGateway.DeleteEstablishmentAsync(parameters, cancellation);
    }
}
