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
            options.Headers.Add(Headers.Authorization);
        });

        var customersClient = services.AddHttpClient<ICustomerClient, CustomerClient>(client =>
        {
            client.BaseAddress = new Uri(settings.Services.ProfilesUrl);
            client.Timeout = TimeSpan.FromMinutes(minutes: 1, seconds: 30);
        });

        var ownersClient = services.AddHttpClient<IOwnerClient, OwnerClient>(client =>
        {
            client.BaseAddress = new Uri(settings.Services.ProfilesUrl);
            client.Timeout = TimeSpan.FromMinutes(minutes: 1, seconds: 30);
        });

        var paymentsClient = services.AddHttpClient<IPaymentClient, PaymentClient>(client =>
        {
            client.BaseAddress = new Uri(settings.Services.PaymentsUrl);
            client.Timeout = TimeSpan.FromMinutes(minutes: 1, seconds: 30);
        });

        var storesClient = services.AddHttpClient<IEstablishmentClient, EstablishmentClient>(client =>
        {
            client.BaseAddress = new Uri(settings.Services.StoresUrl);
            client.Timeout = TimeSpan.FromMinutes(minutes: 1, seconds: 30);
        });

        var productsClient = services.AddHttpClient<IProductClient, ProductClient>(client =>
        {
            client.BaseAddress = new Uri(settings.Services.StoresUrl);
            client.Timeout = TimeSpan.FromMinutes(minutes: 1, seconds: 30);
        });

        customersClient.AddHeaderPropagation();
        ownersClient.AddHeaderPropagation();
        paymentsClient.AddHeaderPropagation();

        storesClient.AddHeaderPropagation();
        productsClient.AddHeaderPropagation();
    }
}
