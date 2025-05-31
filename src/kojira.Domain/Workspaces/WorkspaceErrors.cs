using SharedKernel;

namespace kojira.Domain.Workspaces;

public static class WorkspaceErrors
{
    public static Error NotFound(Guid workspaceId) => Error.NotFound(
        "Workspaces.NotFound",
        $"The workspace with the id = '{workspaceId}' was not found");
}
