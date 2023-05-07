using FluentValidation;

namespace Application.Functions.Accounts.Commands.ChangePassword
{
    public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(v => v.CurrentPassword)
                .NotEmpty();
            RuleFor(v => v.NewPassword)
                .NotEmpty();
        }
    }
}
