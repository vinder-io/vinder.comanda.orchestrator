namespace Vinder.Comanda.Orchestrator.WebApi.Controllers;

[ApiController]
[Route("api/v1/profiles")]
public sealed class ProfilesController(IDispatcher dispatcher) : ControllerBase
{
    [HttpGet("customers")]
    public async Task<IActionResult> GetCustomersAsync([FromQuery] FetchCustomersParameters request, CancellationToken cancellation)
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

    [HttpPost("customers")]
    public async Task<IActionResult> CreateCustomerAsync([FromBody] CustomerCreationScheme request, CancellationToken cancellation)
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

    [HttpPut("customers/{id}")]
    public async Task<IActionResult> EditCustomerAsync([FromBody] EditCustomerScheme request, [FromRoute] string id, CancellationToken cancellation)
    {
        var result = await dispatcher.DispatchAsync(request with { CustomerId = id }, cancellation);

        return result switch
        {
            { IsSuccess: true } when result.Data is not null =>
                StatusCode(StatusCodes.Status200OK, result.Data),

            /* for tracking purposes: raise error #COMANDA-ERROR-AF04C */
            { IsFailure: true } when result.Error == CustomerErrors.CustomerDoesNotExist =>
                StatusCode(StatusCodes.Status404NotFound, result.Error),

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

    [HttpGet("owners")]
    public async Task<IActionResult> GetOwnersAsync(
        [FromQuery] FetchOwnersParameters request, CancellationToken cancellation)
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

    [HttpPut("owners/{ownerId}")]
    public async Task<IActionResult> UpdateOwnerAsync(
        [FromBody] EditOwnerScheme request, [FromRoute] string ownerId, CancellationToken cancellation)
    {
        var result = await dispatcher.DispatchAsync(request with { OwnerId = ownerId }, cancellation);

        return result switch
        {
            { IsSuccess: true } =>
                StatusCode(StatusCodes.Status200OK, result.Data),

            /* for tracking purposes: raise error #COMANDA-ERROR-0831D */
            { IsFailure: true } when result.Error == OwnerErrors.OwnerDoesNotExist =>
                StatusCode(StatusCodes.Status404NotFound, result.Error),

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

    [HttpDelete("owners/{ownerId}")]
    public async Task<IActionResult> DeleteOwnerAsync(
        [FromQuery] OwnerDeletionScheme request, [FromRoute] string ownerId, CancellationToken cancellation)
    {
        var result = await dispatcher.DispatchAsync(request with { OwnerId = ownerId }, cancellation);

        return result switch
        {
            { IsSuccess: true } => StatusCode(StatusCodes.Status204NoContent),

            /* for tracking purposes: raise error #COMANDA-ERROR-0831D */
            { IsFailure: true } when result.Error == OwnerErrors.OwnerDoesNotExist =>
                StatusCode(StatusCodes.Status404NotFound, result.Error),

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
