using FluentValidation;

namespace ForestOfTasks.Application.Users.Queries.LoginUser;

internal sealed class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
{
    public LoginUserQueryValidator()
    {
        RuleFor(x => x.Email)
          .NotEmpty().WithMessage("Email is required.")
          .EmailAddress().WithMessage("Email is not valid.");

        RuleFor(x => x.Password)
          .NotEmpty().WithMessage("Password is required.");
    }
}
