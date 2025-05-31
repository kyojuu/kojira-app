using FluentValidation;

namespace kojira.Application.Workspaces.Commands.Create;

public class CreateWorkspaceCommandValidator : AbstractValidator<CreateWorkspaceCommand>
{
    public CreateWorkspaceCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.WorkspaceName).NotEmpty();
    }
}
