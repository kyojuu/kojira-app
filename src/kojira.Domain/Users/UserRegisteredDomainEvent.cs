using SharedKernel;

namespace kojira.Domain.Users;

public sealed record UserRegisteredDomainEvent(Guid UserId) : IDomainEvents;
