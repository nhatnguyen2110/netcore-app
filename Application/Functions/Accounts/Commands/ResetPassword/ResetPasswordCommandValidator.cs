using FluentValidation;

namespace Application.Functions.Accounts.Commands.ResetPassword
{
    public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordCommandValidator()
        {
            RuleFor(v => v.UserId)
                .NotEmpty();
            RuleFor(v => v.Token)
                .NotEmpty();
            RuleFor(v => v.newPassword)
                .NotEmpty();
        }
    }
}
