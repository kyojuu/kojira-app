using kojira.Application.Abstractions.Authentication;
using kojira.Application.Abstractions.Data;
using kojira.Application.Abstractions.Messaging;
using kojira.Domain.Members;
using kojira.Domain.Roles;
using kojira.Domain.Users;
using kojira.Domain.Workspaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SharedKernel;

namespace kojira.Application.Workspaces.Commands.Create;

internal sealed class CreateWorkspaceCommandHandler(
    IApplicationDbContext context,
    IUserContext userContext
    ) : ICommandHandler<CreateWorkspaceCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateWorkspaceCommand command, CancellationToken cancellationToken)
    {

        if (userContext.UserId != command.UserId)
        {
            return Result.Failure<Guid>(UserErrors.Unauthorized());
        }

        User? user = await context.Users.AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == command.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(command.UserId));
        }

        var workspace = new Workspace
        {
            UserId = user.Id,
            WorkspaceName = command.WorkspaceName,
            Members = new List<Member>
            {
                new Member
                {
                    UserId = user.Id,
                    RoleId = Role.AdminId
                }
            }
        };

        workspace.Raise(new WorkspaceCreatedDomainEvent(workspace.Id));

        context.Workspaces.Add(workspace);

        await context.SaveChangesAsync(cancellationToken);

        return workspace.Id;
    }
}
