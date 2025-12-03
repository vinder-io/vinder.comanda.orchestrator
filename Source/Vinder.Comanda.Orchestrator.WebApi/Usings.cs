global using System.Diagnostics.CodeAnalysis;

global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Authorization;

global using Vinder.Comanda.Internal.Contracts.Errors;
global using Vinder.Comanda.Internal.Contracts.Transport.Internal.Profiles;
global using Vinder.Comanda.Internal.Contracts.Transport.Internal.Payments;
global using Vinder.Comanda.Internal.Contracts.Transport.Internal.Stores;
global using Vinder.Comanda.Internal.Contracts.Transport.Internal;

global using Vinder.Comanda.Internal.Contracts.Clients;
global using Vinder.Comanda.Internal.Contracts.Clients.Interfaces;

global using Vinder.Comanda.Orchestrator.WebApi.Extensions;
global using Vinder.Comanda.Orchestrator.WebApi.Constants;

global using Vinder.Comanda.Orchestrator.Application.Payloads.Payments;
global using Vinder.Comanda.Orchestrator.Infrastructure.IoC.Extensions;
global using Vinder.Comanda.Orchestrator.CrossCutting.Configurations;

global using Vinder.Dispatcher.Contracts;
global using Vinder.IdentityProvider.Sdk.Extensions;

global using Scalar.AspNetCore;
global using FluentValidation.AspNetCore;