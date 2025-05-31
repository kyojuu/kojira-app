using System.Xml.XPath;
using kojira.Application.Abstractions.Authentication;
using kojira.Application.Abstractions.Data;
using kojira.Application.Abstractions.Messaging;
using kojira.Domain.Users;
using kojira.Domain.Workspaces;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace kojira.Application.Workspaces.Commands.Update;

internal sealed class UpdateWorkspaceCommandHandler(
    IApplicationDbContext context,
    IUserContext userContext)
    : ICommandHandler<UpdateWorkspaceCommand>
{
    public async Task<Result> Handle(UpdateWorkspaceCommand command, CancellationToken cancellationToken)
    {
        Workspace? workspace = await context.Workspaces
            .AsNoTracking()
            .SingleOrDefaultAsync(w => w.Id == command.WorkspaceId && w.UserId == userContext.UserId, cancellationToken);

        if (workspace is null)
        {
            return Result.Failure(WorkspaceErrors.NotFound(command.WorkspaceId));
        }

        workspace.WorkspaceName = command.NewWorkspaceName;

        context.Workspaces.Update(workspace);

        workspace.Raise(new WorkspaceUpdatedDomainEvent(workspace.Id));

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
