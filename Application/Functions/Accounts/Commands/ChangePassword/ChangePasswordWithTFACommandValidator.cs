using FluentValidation;

namespace Application.Functions.Accounts.Commands.ChangePassword
{
    public class ChangePasswordWithTFACommandValidator : AbstractValidator<ChangePasswordWithTFACommand>
    {
        public ChangePasswordWithTFACommandValidator()
        {
            RuleFor(v => v.CurrentPassword)
                .NotEmpty();
            RuleFor(v => v.NewPassword)
                .NotEmpty();
            RuleFor(v => v.Code)
                .NotEmpty();
        }
    }
}
