using kojira.Domain.Members;
using kojira.Domain.Users;
using kojira.Domain.Workspaces;
using kojira.Domain.Roles;  
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace kojira.Infrastructure.Members;

internal sealed class MemberConfiguration : IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.HasKey(m => new { m.UserId, m.WorkspaceId, m.RoleId });

        builder.HasOne(m => m.User).WithMany().HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(m => m.Workspace).WithMany().HasForeignKey(m => m.WorkspaceId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(m => m.Role).WithMany().HasForeignKey(m => m.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
