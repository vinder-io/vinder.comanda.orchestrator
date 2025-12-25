namespace Vinder.Comanda.Orchestrator.Application.Handlers.Profiles;

public sealed class CustomerCreationHandler(IProfilesGateway profilesGateway, IUsersClient usersClient, IGroupsClient groupsClient) :
    IMessageHandler<CustomerCreationScheme, Result<CustomerScheme>>
{
    public async Task<Result<CustomerScheme>> HandleAsync(
        CustomerCreationScheme parameters, CancellationToken cancellation = default)
    {
        var filters = new UsersFetchParameters
        {
            Username = parameters.Username,
        };

        var users = await usersClient.GetUsersAsync(filters, cancellation);
        var user = users.Data?.Items.FirstOrDefault();

        if (user is null)
        {
            return Result<CustomerScheme>.Failure(users.Error);
        }

        var groups = await groupsClient.GetGroupsAsync(new() { Name = Groups.Customers }, cancellation);
        var group = groups.Data?.Items.FirstOrDefault();

        if (group is null)
        {
            return Result<CustomerScheme>.Failure(groups.Error);
        }

        var assignment = await usersClient.AssignUserGroupAsync(user.Id, group.Id, cancellation);
        if (assignment.IsFailure)
        {
            return Result<CustomerScheme>.Failure(ProfileErrors.ProfileAlreadyExists);
        }

        return await profilesGateway.CreateCustomerAsync(parameters, cancellation);
    }
}