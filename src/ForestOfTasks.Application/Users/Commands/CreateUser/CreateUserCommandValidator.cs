using FluentValidation;

namespace ForestOfTasks.Application.Users.Commands.CreateUser;

internal sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
  public CreateUserCommandValidator()
  {
    RuleFor(x => x.Email)
      .NotEmpty().WithMessage("Email is required.")
      .EmailAddress().WithMessage("A valid email is required.");
    
    RuleFor(x => x.Username)
      .NotEmpty().WithMessage("Username is required.")
      .MinimumLength(3).WithMessage("Username must be at least 3 characters.");
    
    RuleFor(x => x.Password)
      .NotEmpty().WithMessage("Password is required.")
      .MinimumLength(8).WithMessage("Password must be at least 6 characters.");
  }
}
