namespace Vinder.Comanda.Orchestrator.Application.Handlers.Profiles;

public sealed class OwnerCreationHandler(IProfilesGateway profilesGateway, IUsersClient usersClient, IGroupsClient groupsClient) :
    IMessageHandler<OwnerCreationScheme, Result<OwnerScheme>>
{
    public async Task<Result<OwnerScheme>> HandleAsync(
        OwnerCreationScheme parameters, CancellationToken cancellation = default)
    {
        var filters = new UsersFetchParameters
        {
            Username = parameters.Username,
        };

        var users = await usersClient.GetUsersAsync(filters, cancellation);
        if (users.IsFailure || users.Data is null)
        {
            return Result<OwnerScheme>.Failure(users.Error);
        }

        var user = users.Data.Items.FirstOrDefault();
        if (user is null)
        {
            return Result<OwnerScheme>.Failure(users.Error);
        }

        var groups = await groupsClient.GetGroupsAsync(new() { Name = "owners" }, cancellation);
        var group = groups.Data?.Items.FirstOrDefault();

        if (group is null)
        {
            return Result<OwnerScheme>.Failure(groups.Error);
        }

        var assignment = await usersClient.AssignUserGroupAsync(user.Id, group.Name, cancellation);
        if (assignment.IsFailure)
        {
            return Result<OwnerScheme>.Failure(ProfileErrors.ProfileAlreadyExists);
        }

        return await profilesGateway.CreateOwnerAsync(parameters, cancellation);
    }
}