using kojira.Application.Abstractions.Authentication;
using kojira.Application.Abstractions.Data;
using kojira.Application.Abstractions.Messaging;
using kojira.Domain.Members;
using kojira.Domain.Roles;
using kojira.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using SharedKernel;

namespace kojira.Application.Users.Register;

internal sealed class RegisterUserCommandHandler(
    IApplicationDbContext context, 
    IPasswordHasher passwordHasher) 
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
    {
        if (await context.Users.AnyAsync(u => u.Email == command.Email, cancellationToken))
        {
            return Result.Failure<Guid>(UserErrors.EmailNotUnique);
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = command.Email,
            Name = command.Name,
            PasswordHash = passwordHasher.Hash(command.Password)
        };

        user.Raise(new UserRegisteredDomainEvent(user.Id));

        context.Users.Add(user);

        var member = new Member
        {  
            UserId = user.Id,
            RoleId = Role.AdminId
        };
        context.Members.Add(member);

        await context.SaveChangesAsync(cancellationToken);

        return user.Id;

    }
}
