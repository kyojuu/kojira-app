using kojira.Application.Abstractions.Messaging;

namespace kojira.Application.Workspaces.Commands.Create;

public sealed class CreateWorkspaceCommand : ICommand<Guid>
{
    public Guid UserId { get; set; }
    public string WorkspaceName { get; set; }
}
