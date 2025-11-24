namespace Vinder.Comanda.Orchestrator.Application.Gateways;

public interface IProfilesGateway
{
    public Task<Result<CustomerScheme>> CreateCustomerAsync(
        CustomerCreationScheme parameters,
        CancellationToken cancellation = default
    );

    public Task<Result<PaginationScheme<OwnerScheme>>> GetOwnersAsync(
        FetchOwnersParameters parameters,
        CancellationToken cancellation = default
    );

    public Task<Result<OwnerScheme>> CreateOwnerAsync(
        OwnerCreationScheme parameters,
        CancellationToken cancellation = default
    );

    public Task<Result<OwnerScheme>> UpdateOwnerAsync(
        EditOwnerScheme parameters,
        CancellationToken cancellation = default
    );

    public Task<Result> DeleteOwnerAsync(
        OwnerDeletionScheme parameters,
        CancellationToken cancellation = default
    );
}