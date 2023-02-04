using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Core;

namespace API.Controllers;
/// <summary>
/// Base API controller in which other controllers inherits from for avoiding code repetition
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class BaseApiController : ControllerBase
{
    /// <summary>
    /// Property for injecting the Mediator service without initalization in every API Controller, it helps to save code writing
    /// </summary>
    private IMediator? _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;

    /// <summary>
    /// Abstraction to handle any result from the application layer and translate it to an HTTP request
    /// </summary>
    /// <typeparam name="T">Type generic that allows any object or variable</typeparam>
    /// <param name="result">The result(s) returned by the Application layer to the API Controller</param>
    /// <returns>Returns an Action Result with an error code or a success code with the corresponding values</returns>
    protected ActionResult HandleResult<T>(Result<T> result)
    {
        //if the result or the result value is null, it will be interpreted as a NotFound
        if (result is null) return NotFound();
        //if the result is successful and the value is not null, it will be interpreted as an Ok response
        if (result.IsSuccess && result.Value is not null)
            return Ok(result.Value);

        if (result.IsSuccess && result.Value is null)
            return NotFound();
        //if the result is not success, it returns a BadRequest with the corresponding error
        return BadRequest(result.Error);
    }
}
