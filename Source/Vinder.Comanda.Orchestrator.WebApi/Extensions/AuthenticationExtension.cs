namespace Vinder.Comanda.Orchestrator.WebApi.Extensions;

[ExcludeFromCodeCoverage(Justification = "contains only dependency injection registration with no business logic")]
public static class AuthenticationExtension
{
    public static void AddIdentityServer(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var settings = serviceProvider.GetRequiredService<ISettings>();

        services.AddIdentityProvider(options =>
        {
            options.BaseUrl = settings.Federation.BaseUrl;
            options.ClientId = settings.Federation.ClientId;
            options.Tenant = settings.Federation.Tenant;
            options.ClientSecret = settings.Federation.ClientSecret;
        });
    }
}