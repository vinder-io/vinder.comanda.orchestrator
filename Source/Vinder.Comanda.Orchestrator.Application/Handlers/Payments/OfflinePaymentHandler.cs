namespace Vinder.Comanda.Orchestrator.Application.Handlers.Payments;

public sealed class OfflinePaymentHandler(IPaymentGateway paymentGateway) :
    IMessageHandler<OfflinePaymentScheme, Result<PaymentScheme>>
{
    public async Task<Result<PaymentScheme>> HandleAsync(
        OfflinePaymentScheme parameters, CancellationToken cancellation = default)
    {
        return await paymentGateway.CreateLocalPaymentAsync(parameters, cancellation);
    }
}
