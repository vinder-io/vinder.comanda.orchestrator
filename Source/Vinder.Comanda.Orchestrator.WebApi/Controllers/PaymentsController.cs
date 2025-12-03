namespace Vinder.Comanda.Orchestrator.WebApi.Controllers;

[ApiController]
[Route("api/v1/payments")]
public sealed class PaymentsController(IDispatcher dispatcher) : ControllerBase
{
    [HttpPost("online")]
    public async Task<IActionResult> CreateOnlineChargeAsync(
        [FromBody] CheckoutSessionCreationScheme request, [FromHeader(Name = Headers.Credential)] string credential, CancellationToken cancellation)
    {
        var charge = new OnlineChargeScheme(request, credential);
        var result = await dispatcher.DispatchAsync(charge, cancellation);

        return result switch
        {
            { IsSuccess: true } when result.Data is not null =>
                StatusCode(StatusCodes.Status200OK, result.Data),

            /* returning 502 Bad Gateway because an unexpected or invalid response was received from the external provider */
            /* https://developer.mozilla.org/en-US/docs/Web/HTTP/Reference/Status/502 */
            { IsFailure: true } when result.Error == AbacatePayErrors.OperationFailed =>
                StatusCode(StatusCodes.Status502BadGateway, result.Error),

            { IsFailure: true } when result.Error == CommonErrors.OperationFailed =>
                StatusCode(StatusCodes.Status500InternalServerError, result.Error),

            { IsFailure: true } when result.Error == CommonErrors.RateLimitExceeded =>
                StatusCode(StatusCodes.Status429TooManyRequests, result.Error)
        };
    }

    [HttpPost("offline")]
    public async Task<IActionResult> CreateOfflinePaymentAsync([FromBody] OfflinePaymentScheme request, CancellationToken cancellation)
    {
        var result = await dispatcher.DispatchAsync(request, cancellation);

        return result switch
        {
            { IsSuccess: true } when result.Data is not null =>
                StatusCode(StatusCodes.Status200OK, result.Data),

            { IsFailure: true } when result.Error == CommonErrors.OperationFailed =>
                StatusCode(StatusCodes.Status500InternalServerError, result.Error),

            { IsFailure: true } when result.Error == CommonErrors.RateLimitExceeded =>
                StatusCode(StatusCodes.Status429TooManyRequests, result.Error)
        };
    }
}
