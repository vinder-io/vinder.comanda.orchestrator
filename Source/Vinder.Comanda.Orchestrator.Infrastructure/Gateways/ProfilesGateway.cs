using Vinder.Comanda.Internal.Contracts.Clients.Interfaces;
using Vinder.Comanda.Internal.Contracts.Transport.Internal.Profiles;
using Vinder.Comanda.Orchestrator.Application.Gateways;
using Vinder.Internal.Essentials.Patterns;

namespace Vinder.Comanda.Orchestrator.Infrastructure.Gateways;

public sealed class ProfilesGateway(ICustomerClient customerClient) : IProfilesGateway
{
    public async Task<Result<CustomerScheme>> CreateCustomerAsync(
        CustomerCreationScheme parameters, CancellationToken cancellation = default)
    {
        return await customerClient.CreateCustomerAsync(parameters, cancellation);
    }
}