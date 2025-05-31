using kojira.Application.Abstractions.Authentication;
using kojira.Application.Abstractions.Data;
using kojira.Application.Abstractions.Messaging;
using kojira.Domain.Users;
using kojira.Domain.Workspaces;
using kojira.Domain.Members;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace kojira.Application.Users.Login;

internal sealed class LoginUserCommandHandler(
    IApplicationDbContext context,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider)
    : ICommandHandler<LoginUserCommand, string>
{
    public async Task<Result<string>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {
        User? user = await context.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(u => u.Email == command.Email, cancellationToken);

        if (user is null)
        {
            return Result.Failure<string>(UserErrors.NotFoundByEmail);
        }

        bool verified = passwordHasher.Verify(command.Password, user.PasswordHash);

        if (!verified)
        {
            return Result.Failure<string>(UserErrors.NotFoundByEmail);
        }

        Workspace? workspace = await context.Workspaces
            .AsNoTracking()
            .Include(w => w.Members)
            .ThenInclude(m => m.Role)
            .SingleOrDefaultAsync(w => w.UserId == user.Id, cancellationToken);

        string token = await tokenProvider.Create(user, workspace!);

        return token;
    }
}
