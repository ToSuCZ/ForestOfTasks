using FluentValidation;

namespace ForestOfTasks.Application.Users.Queries.UserDetail;

internal sealed class UserDetailValidator : AbstractValidator<UserDetailQuery>
{
    public UserDetailValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("Id is required.");
    }
}
