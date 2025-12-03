namespace Vinder.Comanda.Orchestrator.WebApi.Controllers;

[ApiController]
[Route("api/v1/stores")]
public sealed class StoresController(IDispatcher dispatcher) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetEstablishmentsAsync(
        [FromQuery] EstablishmentsFetchParameters request, CancellationToken cancellation)
    {
        var result = await dispatcher.DispatchAsync(request, cancellation);

        /* applies pagination navigation links according to RFC 8288 (web linking) */
        /* https://datatracker.ietf.org/doc/html/rfc8288 */
        if (result.IsSuccess && result.Data is not null)
        {
            Response.WithPagination(result.Data);
            Response.WithWebLinking(result.Data, Request);
        }

        return result switch
        {
            { IsSuccess: true } when result.Data is not null =>
                StatusCode(StatusCodes.Status200OK, result.Data.Items),

            { IsFailure: true } when result.Error == CommonErrors.OperationFailed =>
                StatusCode(StatusCodes.Status500InternalServerError, result.Error),

            { IsFailure: true } when result.Error == CommonErrors.RateLimitExceeded =>
                StatusCode(StatusCodes.Status429TooManyRequests, result.Error),
        };
    }

    [HttpPost]
    public async Task<IActionResult> CreateEstablishmentAsync(
        [FromBody] EstablishmentCreationScheme request, CancellationToken cancellation)
    {
        var result = await dispatcher.DispatchAsync(request, cancellation);

        /* appends resource location header according to RFC 9110 (HTTP Semantics) */
        /* https://www.rfc-editor.org/rfc/rfc9110.html */
        if (result.IsSuccess && result.Data is not null)
        {
            Response.WithResourceLocation(Request, result.Data.Identifier);
        }

        return result switch
        {
            { IsSuccess: true } when result.Data is not null =>
                StatusCode(StatusCodes.Status201Created, result.Data),

            { IsFailure: true } when result.Error == EstablishmentErrors.OwnerAlreadyHasEstablishment =>
                StatusCode(StatusCodes.Status409Conflict, result.Error),

            { IsFailure: true } when result.Error == CommonErrors.OperationFailed =>
                StatusCode(StatusCodes.Status500InternalServerError, result.Error),

            { IsFailure: true } when result.Error == CommonErrors.RateLimitExceeded =>
                StatusCode(StatusCodes.Status429TooManyRequests, result.Error),
        };
    }

    [HttpPut("{establishmentId}")]
    public async Task<IActionResult> UpdateEstablishmentAsync(
        [FromBody] EstablishmentModificationScheme request, [FromRoute] string establishmentId, CancellationToken cancellation)
    {
        var result = await dispatcher.DispatchAsync(request with { EstablishmentId = establishmentId }, cancellation);

        return result switch
        {
            { IsSuccess: true } when result.Data is not null =>
                StatusCode(StatusCodes.Status200OK, result.Data),

            { IsFailure: true } when result.Error == EstablishmentErrors.EstablishmentDoesNotExist =>
                StatusCode(StatusCodes.Status404NotFound, result.Error),

            { IsFailure: true } when result.Error == CommonErrors.OperationFailed =>
                StatusCode(StatusCodes.Status500InternalServerError, result.Error),

            { IsFailure: true } when result.Error == CommonErrors.RateLimitExceeded =>
                StatusCode(StatusCodes.Status429TooManyRequests, result.Error),
        };
    }

    [HttpDelete("{establishmentId}")]
    public async Task<IActionResult> DeleteEstablishmentAsync(
        [FromQuery] EstablishmentDeletionScheme request, [FromRoute] string establishmentId, CancellationToken cancellation)
    {
        var result = await dispatcher.DispatchAsync(request with { EstablishmentId = establishmentId }, cancellation);

        return result switch
        {
            { IsSuccess: true } => StatusCode(StatusCodes.Status204NoContent),

            { IsFailure: true } when result.Error == EstablishmentErrors.EstablishmentDoesNotExist =>
                StatusCode(StatusCodes.Status404NotFound, result.Error),

            { IsFailure: true } when result.Error == CommonErrors.OperationFailed =>
                StatusCode(StatusCodes.Status500InternalServerError, result.Error),

            { IsFailure: true } when result.Error == CommonErrors.RateLimitExceeded =>
                StatusCode(StatusCodes.Status429TooManyRequests, result.Error),
        };
    }

    [HttpGet("{establishmentId}/products")]
    public async Task<IActionResult> GetProductsAsync(
        [FromQuery] ProductsFetchParameters request, [FromRoute] string establishmentId, CancellationToken cancellation)
    {
        var result = await dispatcher.DispatchAsync(request with { EstablishmentId = establishmentId }, cancellation);

        /* applies pagination navigation links according to RFC 8288 (web linking) */
        /* https://datatracker.ietf.org/doc/html/rfc8288 */
        if (result.IsSuccess && result.Data is not null)
        {
            Response.WithPagination(result.Data);
            Response.WithWebLinking(result.Data, Request);
        }

        return result switch
        {
            { IsSuccess: true } when result.Data is not null =>
                StatusCode(StatusCodes.Status200OK, result.Data.Items),

            { IsFailure: true } when result.Error == CommonErrors.OperationFailed =>
                StatusCode(StatusCodes.Status500InternalServerError, result.Error),

            { IsFailure: true } when result.Error == CommonErrors.RateLimitExceeded =>
                StatusCode(StatusCodes.Status429TooManyRequests, result.Error),
        };
    }

    [HttpPost("{establishmentId}/products")]
    public async Task<IActionResult> CreateProductAsync(
        [FromBody] ProductCreationScheme request, [FromRoute] string establishmentId, CancellationToken cancellation)
    {
        var result = await dispatcher.DispatchAsync(request with { EstablishmentId = establishmentId }, cancellation);

        /* appends resource location header according to RFC 9110 (HTTP Semantics) */
        /* https://www.rfc-editor.org/rfc/rfc9110.html */
        if (result.IsSuccess && result.Data is not null)
        {
            Response.WithResourceLocation(Request, result.Data.Identifier);
        }

        return result switch
        {
            { IsSuccess: true } when result.Data is not null =>
                StatusCode(StatusCodes.Status201Created, result.Data),

            { IsFailure: true } when result.Error == CommonErrors.OperationFailed =>
                StatusCode(StatusCodes.Status500InternalServerError, result.Error),

            { IsFailure: true } when result.Error == CommonErrors.RateLimitExceeded =>
                StatusCode(StatusCodes.Status429TooManyRequests, result.Error),
        };
    }

    [HttpPut("{establishmentId}/products/{productId}")]
    public async Task<IActionResult> UpdateProductAsync(
        [FromBody] ProductModificationScheme request, [FromRoute] string establishmentId, [FromRoute] string productId, CancellationToken cancellation)
    {
        var result = await dispatcher.DispatchAsync(request with { EstablishmentId = establishmentId, ProductId = productId }, cancellation);

        return result switch
        {
            { IsSuccess: true } when result.Data is not null =>
                StatusCode(StatusCodes.Status200OK, result.Data),

            { IsFailure: true } when result.Error == EstablishmentErrors.EstablishmentDoesNotExist =>
                StatusCode(StatusCodes.Status404NotFound, result.Error),

            { IsFailure: true } when result.Error == ProductErrors.ProductDoesNotExist =>
                StatusCode(StatusCodes.Status404NotFound, result.Error),

            { IsFailure: true } when result.Error == CommonErrors.OperationFailed =>
                StatusCode(StatusCodes.Status500InternalServerError, result.Error),

            { IsFailure: true } when result.Error == CommonErrors.RateLimitExceeded =>
                StatusCode(StatusCodes.Status429TooManyRequests, result.Error),
        };
    }

    [HttpPut("{establishmentId}/products/{productId}/image")]
    public async Task<IActionResult> UploadProductImageAsync(
        [FromRoute] string establishmentId, [FromRoute] string productId, [FromForm] IFormFile file, CancellationToken cancellation)
    {
        var stream = file.OpenReadStream();
        var result = await dispatcher.DispatchAsync(stream.AsImage(productId, establishmentId), cancellation);

        return result switch
        {
            { IsSuccess: true } => StatusCode(StatusCodes.Status204NoContent),

            { IsFailure: true } when result.Error == EstablishmentErrors.EstablishmentDoesNotExist =>
                StatusCode(StatusCodes.Status404NotFound, result.Error),

            { IsFailure: true } when result.Error == ProductErrors.ProductDoesNotExist =>
                StatusCode(StatusCodes.Status404NotFound, result.Error),

            { IsFailure: true } when result.Error == CommonErrors.OperationFailed =>
                StatusCode(StatusCodes.Status500InternalServerError, result.Error),

            { IsFailure: true } when result.Error == CommonErrors.RateLimitExceeded =>
                StatusCode(StatusCodes.Status429TooManyRequests, result.Error),
        };
    }

    [HttpDelete("{establishmentId}/products/{productId}")]
    public async Task<IActionResult> DeleteProductAsync(
        [FromQuery] ProductDeletionScheme request, [FromRoute] string establishmentId, [FromRoute] string productId, CancellationToken cancellation)
    {
        var result = await dispatcher.DispatchAsync(request with { EstablishmentId = establishmentId, ProductId = productId }, cancellation);

        return result switch
        {
            { IsSuccess: true } => StatusCode(StatusCodes.Status204NoContent),

            { IsFailure: true } when result.Error == EstablishmentErrors.EstablishmentDoesNotExist =>
                StatusCode(StatusCodes.Status404NotFound, result.Error),

            { IsFailure: true } when result.Error == ProductErrors.ProductDoesNotExist =>
                StatusCode(StatusCodes.Status404NotFound, result.Error),

            { IsFailure: true } when result.Error == ProductErrors.ProductBelongsToAnotherEstablishment =>
                StatusCode(StatusCodes.Status422UnprocessableEntity, result.Error),

            { IsFailure: true } when result.Error == CommonErrors.OperationFailed =>
                StatusCode(StatusCodes.Status500InternalServerError, result.Error),

            { IsFailure: true } when result.Error == CommonErrors.RateLimitExceeded =>
                StatusCode(StatusCodes.Status429TooManyRequests, result.Error),
        };
    }
}
