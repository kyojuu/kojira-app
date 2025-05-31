using kojira.api.Extensions;
using kojira.Application.Workspaces.Commands.Update;
using kojira.api.Infrastructure;
using MediatR;
using SharedKernel;
using kojira.Domain.Roles;

namespace kojira.api.Endpoints.Workspaces;

internal sealed class Update : IEndpoint
{
    public sealed record Request(Guid WorkspaceId, string NewWorkspaceName);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("workspaces/{id:guid}", async (Request request, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new UpdateWorkspaceCommand(request.WorkspaceId, request.NewWorkspaceName);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Workspaces)
        .RequireAuthorization(policy => policy.RequireRole(Role.Admin));
    }
}
