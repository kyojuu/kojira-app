using kojira.Application.Abstractions.Messaging;

namespace kojira.Application.Workspaces.Commands.Update;

public sealed record UpdateWorkspaceCommand(Guid WorkspaceId, string NewWorkspaceName) : ICommand;
