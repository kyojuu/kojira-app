using kojira.api.Extensions;
using kojira.api.Infrastructure;
using kojira.Application.Users.Register;
using MediatR;
using SharedKernel;

namespace kojira.api.Endpoints.Users;

internal sealed class Register : IEndpoint
{
    public sealed record Request(string Email, string Name, string Password);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/register", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new RegisterUserCommand(
                request.Email,
                request.Name,
                request.Password);

            Result<Guid> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Users);
    }
}
