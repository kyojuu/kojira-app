using FluentValidation;

namespace kojira.Application.Workspaces.Commands.Delete;

internal sealed class DeleteWorkspaceCommandValidator : AbstractValidator<DeleteWorkspaceCommand>
{
    public DeleteWorkspaceCommandValidator()
    {
        RuleFor(w => w.WorkspaceId).NotEmpty();
    }
}
