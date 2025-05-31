using kojira.Domain.Roles;
using kojira.Domain.Users;
using kojira.Domain.Workspaces;
using kojira.Domain.Members;
using Microsoft.EntityFrameworkCore;

namespace kojira.Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Workspace> Workspaces { get; }
    DbSet<Role> Roles { get; }
    DbSet<Member> Members { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
