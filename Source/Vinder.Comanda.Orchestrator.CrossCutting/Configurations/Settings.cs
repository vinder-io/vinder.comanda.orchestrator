namespace Vinder.Comanda.Orchestrator.CrossCutting.Configurations;

public sealed record Settings : ISettings
{
    public FederationSettings Federation { get; init; } = default!;
    public ServicesSettings Services { get; init; } = default!;
}