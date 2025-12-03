namespace Vinder.Comanda.Orchestrator.CrossCutting.Configurations;

public sealed class ServicesSettings
{
    public string ProfilesUrl { get; init; } = default!;
    public string StoresUrl { get; init; } = default!;
    public string PaymentsUrl { get; init; } = default!;
}
