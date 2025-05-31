/*
using kojira.api.Extensions;
using kojira.api.Infrastructure;
using kojira.Application.Users.Login;
using kojira.Application.Users.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace kojira.api.Controllers.Users;

[Authorize]
[Route("api/users")]
[ApiController]
public sealed class UserAccountController : ControllerBase
{
    private readonly ISender _sender;

    public UserAccountController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("login")]
    public async Task<IActionResult> Login(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginUserCommand(request.Email, request.Password);

        Result<string> result = await _sender.Send(command, cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterUserCommand(
            request.Email, request.Name, request.Password);

        Result<Guid> result = await _sender.Send(command, cancellationToken);

        return result.Match(
            success => Ok(result),
            failure => CustomResults.Problem(result)
        );

    }
}
*/
