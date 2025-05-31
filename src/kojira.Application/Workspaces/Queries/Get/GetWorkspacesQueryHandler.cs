using kojira.Application.Abstractions.Authentication;
using kojira.Application.Abstractions.Data;
using kojira.Application.Abstractions.Messaging;
using kojira.Domain.Members;
using kojira.Domain.Users;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace kojira.Application.Workspaces.Queries.Get;

internal sealed class GetWorkspacesQueryHandler(
    IApplicationDbContext context, 
    IUserContext userContext) : IQueryHandler<GetWorkspacesQuery, List<WorkspaceResponse>>
{
    public async Task<Result<List<WorkspaceResponse>>> Handle(GetWorkspacesQuery query, CancellationToken cancellationToken)
    {
        if (query.UserId != userContext.UserId)
        {
            return Result.Failure<List<WorkspaceResponse>>(UserErrors.Unauthorized());
        }

        List<WorkspaceResponse> workspace = await context.Workspaces
            .Where(workspaceCard => workspaceCard.UserId == query.UserId)
            .Select(workspaceCard => new WorkspaceResponse 
            { 
                Id = workspaceCard.Id,
                UserId = workspaceCard.UserId,
                WorkspaceName = workspaceCard.WorkspaceName
            })
            .ToListAsync(cancellationToken);

        return workspace;
    }
}
