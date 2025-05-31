using SharedKernel;

namespace kojira.Domain.Roles;

public sealed class Role : Entity
{
    public const string Admin = "Admin";
    public const string Member = "Member";
    public const int AdminId = 1;
    public const int MemberId = 2;

    public int Id { get; init; }
    public string Name { get; init; }
}


