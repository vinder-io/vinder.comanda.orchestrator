using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Vinder.Comanda.Orchestrator.TestSuite.Fixtures;

public sealed class WebApplicationFixture : IAsyncLifetime
{
    public HttpClient HttpClient { get; private set; } = default!;
    public IServiceProvider Services { get; private set; } = default!;

    private WebApplicationFactory<Program> _factory = default!;


    public async ValueTask InitializeAsync()
    {
        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = "vinder.internal.bypass.authentication";
                        options.DefaultChallengeScheme = "vinder.internal.bypass.authentication";
                        options.DefaultScheme = "vinder.internal.bypass.authentication";
                    })
                    .AddScheme<AuthenticationSchemeOptions, BypassAuthenticationHandler>("vinder.internal.bypass.authentication", _ => { });

                    services.AddAuthorization(options =>
                    {
                        options.DefaultPolicy = new AuthorizationPolicyBuilder("vinder.internal.bypass.authentication")
                            .RequireAuthenticatedUser()
                            .Build();
                    });
                });
            });

        HttpClient = _factory.CreateClient();
        Services = _factory.Services;
    }

    public async ValueTask DisposeAsync()
    {
        HttpClient.Dispose();

        await _factory.DisposeAsync();
    }
}
