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
    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    /// <summary>
    /// Abstraction to handle any result from the application layer and translate it to an HTTP request
    /// </summary>
    /// <typeparam name="T">Type generic that allows any object or variable</typeparam>
    /// <param name="result">The result(s) returned by the Application layer to the API Controller</param>
    /// <returns>Returns an Action Result with an error code or a success code with the corresponding values</returns>
    protected ActionResult HandleResult<T>(Result<T> result)
    {
        if (result == null) return NotFound();
        if (result.IsSuccess && result.Value != null)
            return Ok(result.Value);

        if (result.IsSuccess && result.Value == null)
            return NotFound();

        return BadRequest(result.Error);
    }
}
