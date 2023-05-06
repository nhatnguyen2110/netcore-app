using FluentValidation;

namespace Application.Functions.Accounts.Commands.SignIn
{
    public class SignInWithTFACommandValidator : AbstractValidator<SignInWithTFACommand>
    {
        public SignInWithTFACommandValidator()
        {
            RuleFor(v => v.Email)
                .NotEmpty();
            RuleFor(v => v.Code)
                .NotEmpty();
        }
    }
}
