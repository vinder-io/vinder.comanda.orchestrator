namespace Vinder.Comanda.Orchestrator.Infrastructure.IoC.Extensions;

public static class GatewaysExtension
{
    public static void AddGateways(this IServiceCollection services)
    {
        services.AddSingleton<IProfilesGateway, ProfilesGateway>();
    }
}
