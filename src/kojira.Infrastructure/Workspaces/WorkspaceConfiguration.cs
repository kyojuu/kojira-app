using System.Globalization;
using kojira.Domain.Users;
using kojira.Domain.Workspaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace kojira.Infrastructure.Workspaces;

internal sealed class WorkspaceConfiguration : IEntityTypeConfiguration<Workspace>
{
    public void Configure(EntityTypeBuilder<Workspace> builder)
    {
        builder.HasKey(w => w.Id);

        builder.HasOne<User>().WithMany().HasForeignKey(w => w.UserId);
    } 
}
