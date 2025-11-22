namespace Vinder.Comanda.Orchestrator.Application.Handlers.Profiles;

public sealed class OwnerCreationHandler(IProfilesGateway profilesGateway) :
    IMessageHandler<OwnerCreationScheme, Result<OwnerScheme>>
{
    public async Task<Result<OwnerScheme>> HandleAsync(
        OwnerCreationScheme parameters, CancellationToken cancellation = default)
    {
        return await profilesGateway.CreateOwnerAsync(parameters, cancellation);
    }
}