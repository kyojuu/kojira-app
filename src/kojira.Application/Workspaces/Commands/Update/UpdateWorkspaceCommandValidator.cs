using FluentValidation;

namespace kojira.Application.Workspaces.Commands.Update;

internal sealed class UpdateWorkspaceCommandValidator : AbstractValidator<UpdateWorkspaceCommand>
{
    public UpdateWorkspaceCommandValidator()
    {
        RuleFor(w => w.WorkspaceId).NotEmpty();
    }
}
