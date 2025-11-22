namespace Vinder.Comanda.Orchestrator.CrossCutting.Configurations;

public interface ISettings
{
    public FederationSettings Federation { get; }
    public ServicesSettings Services { get; }
}