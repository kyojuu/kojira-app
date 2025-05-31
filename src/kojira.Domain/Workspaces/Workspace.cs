using kojira.Domain.Members;
using SharedKernel;

namespace kojira.Domain.Workspaces;

public sealed class Workspace : Entity
{
    public Guid Id { get; set; }
    public string WorkspaceName { get; set; }
    public Guid UserId { get; set; }

    public ICollection<Member> Members { get; set; } = [];
}
