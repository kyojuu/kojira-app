using kojira.Domain.Users;
using kojira.Domain.Workspaces;

namespace kojira.Application.Abstractions.Authentication;

public interface ITokenProvider
{
    Task<string> Create(User user, Workspace workspace);
}
