namespace Vinder.Comanda.Orchestrator.WebApi.Extensions;

public static class ClientsExtension
{
    public static void AddHttpClients(this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var settings = serviceProvider.GetRequiredService<ISettings>();

        // registers the header propagation service
        // essential for receiving an authenticated request and forwarding it to another service
        services.AddHeaderPropagation(options =>
        {
            options.Headers.Add("Authorization");
        });

        var profilesClient = services.AddHttpClient<ICustomerClient, CustomerClient>(client =>
        {
            client.BaseAddress = new Uri(settings.Services.ProfilesUrl);
            client.Timeout = TimeSpan.FromMinutes(minutes: 1, seconds: 30);
        });

        profilesClient.AddHeaderPropagation();
    }
}
