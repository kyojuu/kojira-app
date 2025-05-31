namespace kojira.Application.Workspaces.Queries.Get;

public sealed class WorkspaceResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string WorkspaceName { get; set; }
}
