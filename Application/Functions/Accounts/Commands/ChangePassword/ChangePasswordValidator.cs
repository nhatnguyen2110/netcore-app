using FluentValidation;

namespace Application.Functions.Accounts.Commands.ChangePassword
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidator()
        {
            RuleFor(v => v.CurrentPassword)
            .NotEmpty();
            RuleFor(v => v.NewPassword)
                .NotEmpty();
        }
    }
}
