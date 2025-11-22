namespace Vinder.Comanda.Orchestrator.WebApi.Controllers;

[ApiController]
[Route("api/v1/profiles")]
public sealed class ProfilesController(IDispatcher dispatcher) : ControllerBase
{
    [HttpPost("customers")]
    public async Task<IActionResult> CreateCustomerAsync(
        [FromBody] CustomerCreationScheme request, CancellationToken cancellation)
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
            { IsSuccess: true } =>
                StatusCode(StatusCodes.Status201Created, result.Data),

            /* for tracking purposes: raise error #COMANDA-ERROR-76A71 */
            { IsFailure: true } when result.Error == ProfileErrors.ProfileAlreadyExists =>
                StatusCode(StatusCodes.Status409Conflict, result.Error),

            /* for tracking purposes: raise error #COMANDA-ERROR-61CC0 */
            { IsFailure: true } when result.Error == CommonErrors.UnauthorizedAccess =>
                StatusCode(StatusCodes.Status403Forbidden, result.Error),

            /* for tracking purposes: raise error #COMANDA-ERROR-60A10 */
            { IsFailure: true } when result.Error == CommonErrors.OperationFailed =>
                StatusCode(StatusCodes.Status500InternalServerError, result.Error),

            /* for tracking purposes: raise error #COMANDA-ERROR-B6688 */
            { IsFailure: true } when result.Error == CommonErrors.RateLimitExceeded =>
                StatusCode(StatusCodes.Status429TooManyRequests, result.Error),
        };
    }

    [HttpPost("owners")]
    public async Task<IActionResult> CreateOwnerAsync(
    [FromBody] OwnerCreationScheme request, CancellationToken cancellation)
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
            { IsSuccess: true } =>
                StatusCode(StatusCodes.Status201Created, result.Data),

            /* for tracking purposes: raise error #COMANDA-ERROR-76A71 */
            { IsFailure: true } when result.Error == ProfileErrors.ProfileAlreadyExists =>
                StatusCode(StatusCodes.Status409Conflict, result.Error),

            /* for tracking purposes: raise error #COMANDA-ERROR-61CC0 */
            { IsFailure: true } when result.Error == CommonErrors.UnauthorizedAccess =>
                StatusCode(StatusCodes.Status403Forbidden, result.Error),

            /* for tracking purposes: raise error #COMANDA-ERROR-60A10 */
            { IsFailure: true } when result.Error == CommonErrors.OperationFailed =>
                StatusCode(StatusCodes.Status500InternalServerError, result.Error),

            /* for tracking purposes: raise error #COMANDA-ERROR-B6688 */
            { IsFailure: true } when result.Error == CommonErrors.RateLimitExceeded =>
                StatusCode(StatusCodes.Status429TooManyRequests, result.Error),
        };
    }
}
