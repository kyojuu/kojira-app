using kojira.Application.Abstractions.Authentication;
using kojira.Application.Abstractions.Data;
using kojira.Application.Abstractions.Messaging;
using kojira.Domain.Workspaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SharedKernel;

namespace kojira.Application.Workspaces.Commands.Delete;

internal sealed class DeleteWorkspaceCommandHandler(IApplicationDbContext context, IUserContext userContext)
    : ICommandHandler<DeleteWorkspaceCommand>
{
    public async Task<Result> Handle(DeleteWorkspaceCommand command, CancellationToken cancellationToken)
    {
        Workspace? workspace = await context.Workspaces
            .AsNoTracking()
            .SingleOrDefaultAsync(w => w.Id == command.WorkspaceId && w.UserId == userContext.UserId, cancellationToken);

        if (workspace is null)
        {
            return Result.Failure(WorkspaceErrors.NotFound(command.WorkspaceId));
        }

        context.Workspaces.Remove(workspace);

        workspace.Raise(new WorkspaceDeletedDomainEvent(workspace.Id));

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
