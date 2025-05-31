using SharedKernel;

namespace kojira.Domain.Workspaces;

public sealed record WorkspaceDeletedDomainEvent(Guid WorkspaceId) : IDomainEvents;
