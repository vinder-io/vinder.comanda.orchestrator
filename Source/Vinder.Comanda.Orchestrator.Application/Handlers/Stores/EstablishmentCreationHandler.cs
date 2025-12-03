namespace Vinder.Comanda.Orchestrator.Application.Handlers.Stores;

public sealed class EstablishmentCreationHandler(IEstablishmentGateway establishmentGateway) :
    IMessageHandler<EstablishmentCreationScheme, Result<EstablishmentScheme>>
{
    public async Task<Result<EstablishmentScheme>> HandleAsync(
        EstablishmentCreationScheme parameters, CancellationToken cancellation = default)
    {
        return await establishmentGateway.CreateEstablishmentAsync(parameters, cancellation);
    }
}
