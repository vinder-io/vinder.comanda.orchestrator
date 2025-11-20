namespace Vinder.Comanda.Orchestrator.WebApi.Extensions;

[ExcludeFromCodeCoverage(Justification = "contains only dependency injection registration with no business logic.")]
public static class SpecificationsExtension
{
    public static void UseSpecification(this IEndpointRouteBuilder app)
    {
        app.MapScalarApiReference(options =>
        {
            options.DarkMode = false;
            options.HideDarkModeToggle = true;
            options.HideClientButton = true;

            options.WithTitle("Vinder Comanda Orchestrator API");
            options.WithClassicLayout();
        });
    }
}