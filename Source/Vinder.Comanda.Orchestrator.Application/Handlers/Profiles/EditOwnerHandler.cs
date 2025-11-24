namespace Vinder.Comanda.Orchestrator.Application.Handlers.Profiles;

public sealed class EditOwnerHandler(IProfilesGateway profilesGateway) :
    IMessageHandler<EditOwnerScheme, Result<OwnerScheme>>
{
    public async Task<Result<OwnerScheme>> HandleAsync(
        EditOwnerScheme parameters, CancellationToken cancellation = default)
    {
        return await profilesGateway.UpdateOwnerAsync(parameters, cancellation);
    }
}
