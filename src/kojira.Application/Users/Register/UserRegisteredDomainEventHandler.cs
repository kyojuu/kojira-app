using kojira.Domain.Users;
using MediatR;

namespace kojira.Application.Users.Register;

internal sealed class UserRegisteredDomainEventHandler : INotificationHandler<UserRegisteredDomainEvent>
{
    public Task Handle(UserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        // Todo: send an email verification link, etc.
        return Task.CompletedTask;
    }
}
