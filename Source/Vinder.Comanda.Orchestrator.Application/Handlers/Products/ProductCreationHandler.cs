namespace Vinder.Comanda.Orchestrator.Application.Handlers.Products;

public sealed class ProductCreationHandler(IProductGateway productGateway) :
    IMessageHandler<ProductCreationScheme, Result<ProductScheme>>
{
    public async Task<Result<ProductScheme>> HandleAsync(
        ProductCreationScheme parameters, CancellationToken cancellation = default)
    {
        return await productGateway.CreateProductAsync(parameters, cancellation);
    }
}
