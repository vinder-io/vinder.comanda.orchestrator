namespace Vinder.Comanda.Orchestrator.Application.Payloads.Payments;

public sealed record OnlineChargeScheme(CheckoutSessionCreationScheme Payload, string SecretKey) :
    IMessage<Result<CheckoutSession>>;
