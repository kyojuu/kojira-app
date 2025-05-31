using kojira.Application.Abstractions.Messaging;

namespace kojira.Application.Users.Register;

public sealed record RegisterUserCommand(string Email, string Name, string Password) : ICommand<Guid>;
