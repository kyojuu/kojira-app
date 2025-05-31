using kojira.api.Extensions;
using kojira.api.Infrastructure;
using kojira.Application.Workspaces.Commands.Delete;
using kojira.Domain.Roles;
using MediatR;
using SharedKernel;

namespace kojira.api.Endpoints.Workspaces;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("workspaces/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new DeleteWorkspaceCommand(id);

            Result result = await sender.Send(command, cancellationToken);

            return result.Match(Results.NoContent, CustomResults.Problem);
        })
        .WithTags(Tags.Workspaces)
        .RequireAuthorization(policy => policy.RequireRole(Role.Admin));
    }
}
