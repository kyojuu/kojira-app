using kojira.api.Extensions;
using kojira.Application.Workspaces.Commands.Create;
using kojira.Domain.Users;
using kojira.api.Infrastructure;
using MediatR;
using NuGet.DependencyResolver;
using SharedKernel;
using kojira.Domain.Roles;

namespace kojira.api.Endpoints.Workspaces;

internal sealed class Create : IEndpoint
{
    public sealed record Request(Guid UserId, string WorkspaceName);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("workspaces", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new CreateWorkspaceCommand
            {
                UserId = request.UserId,
                WorkspaceName = request.WorkspaceName,
            };

            Result<Guid> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Workspaces)
        .RequireAuthorization(policy => policy.RequireRole(Role.Admin));
    }
}
