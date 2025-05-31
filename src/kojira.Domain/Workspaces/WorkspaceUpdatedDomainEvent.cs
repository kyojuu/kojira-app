using SharedKernel;

namespace kojira.Domain.Workspaces;

public sealed record WorkspaceUpdatedDomainEvent(Guid WorkspaceId) : IDomainEvents;
