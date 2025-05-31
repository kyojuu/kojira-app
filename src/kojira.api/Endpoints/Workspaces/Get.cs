using kojira.api.Extensions;
using kojira.Application.Workspaces.Queries.Get;
using kojira.api.Infrastructure;
using MediatR;
using SharedKernel;
using kojira.Domain.Roles;
using kojira.Domain.Members;

namespace kojira.api.Endpoints.Workspaces;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("workspaces", async (Guid userId, ISender sender, CancellationToken cancellationToken) =>
        {
            var command = new GetWorkspacesQuery(userId);

            Result<List<WorkspaceResponse>> result = await sender.Send(command, cancellationToken);

            return result.Match(Results.Ok, CustomResults.Problem);
        })
        .WithTags(Tags.Workspaces)
        .RequireAuthorization(policy => policy.RequireRole(Role.Admin, Role.Member));
    }
}
