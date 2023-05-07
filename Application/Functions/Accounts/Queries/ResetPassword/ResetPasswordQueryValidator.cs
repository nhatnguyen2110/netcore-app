using FluentValidation;

namespace Application.Functions.Accounts.Queries.ResetPassword
{
    public class ResetPasswordQueryValidator : AbstractValidator<ResetPasswordQuery>
    {
        public ResetPasswordQueryValidator()
        {
            RuleFor(v => v.Email)
            .NotEmpty()
            .EmailAddress();
        }
    }
}
