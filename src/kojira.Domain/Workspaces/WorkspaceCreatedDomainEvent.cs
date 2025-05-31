using SharedKernel;

namespace kojira.Domain.Workspaces;

public sealed record WorkspaceCreatedDomainEvent(Guid WorkspaceId) : IDomainEvents;
