using kojira.Domain.Workspaces;
using SharedKernel;

namespace kojira.Domain.Users;

public sealed class User : Entity
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string PasswordHash { get; set; }
}
