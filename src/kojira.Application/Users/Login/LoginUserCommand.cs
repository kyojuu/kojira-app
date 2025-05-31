using kojira.Application.Abstractions.Messaging;

namespace kojira.Application.Users.Login;

public sealed record LoginUserCommand(string Email, string Password) : ICommand<string>;
