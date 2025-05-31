using kojira.Domain.Roles;
using kojira.Domain.Users;
using kojira.Domain.Workspaces;
using SharedKernel;

namespace kojira.Domain.Members;

public sealed class Member : Entity
{
    public Guid UserId { get; set; }
    public Guid WorkspaceId { get; set; }
    public int RoleId { get; set; }

    public User User { get; set; }
    public Workspace Workspace { get; set; }
    public Role Role { get; set; }
}
