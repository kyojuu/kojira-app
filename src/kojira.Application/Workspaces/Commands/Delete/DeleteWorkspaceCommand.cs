using kojira.Application.Abstractions.Messaging;

namespace kojira.Application.Workspaces.Commands.Delete;

public sealed record DeleteWorkspaceCommand(Guid WorkspaceId) : ICommand;
