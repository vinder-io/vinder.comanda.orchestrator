global using Microsoft.Extensions.Logging;

global using Vinder.Comanda.Internal.Contracts.Errors;
global using Vinder.Comanda.Internal.Contracts.Clients.Interfaces;

global using Vinder.Comanda.Internal.Contracts.Transport.Internal.Profiles;
global using Vinder.Comanda.Internal.Contracts.Transport.Internal.Payments;
global using Vinder.Comanda.Internal.Contracts.Transport.Internal;

global using Vinder.Comanda.Orchestrator.Application.Gateways;
global using Vinder.Comanda.Orchestrator.Infrastructure.Policies;
global using Vinder.Internal.Essentials.Patterns;

global using Polly;
global using Polly.RateLimit;
global using Polly.Wrap;
