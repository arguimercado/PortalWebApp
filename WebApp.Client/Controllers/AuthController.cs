using Auth.Applications.Features.Users.Queries;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Client.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public IActionResult HandleResult<TValue>(Result<TValue> result)
    {
        if (!result.IsSuccess) {
            return BadRequest(result.Errors[0].Message);
        }
        return Ok(result.Value);
    }

    public AuthController(IMediator mediator) {
        _mediator = mediator;
    }

    [HttpGet("validate")]
    public async Task<IActionResult> Validate([FromQuery]Validate.Query query) {
        var result = await _mediator.Send(query);
        return HandleResult(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(Login.Query query) {
        var result = await _mediator.Send(query);
        return HandleResult(result);
    }
}
