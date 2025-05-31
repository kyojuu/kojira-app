using kojira.Application.Abstractions.Messaging;

namespace kojira.Application.Workspaces.Queries.Get;

public sealed record GetWorkspacesQuery(Guid UserId) : IQuery<List<WorkspaceResponse>>;
