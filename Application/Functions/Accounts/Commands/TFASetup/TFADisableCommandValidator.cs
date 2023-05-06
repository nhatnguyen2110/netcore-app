using FluentValidation;

namespace Application.Functions.Accounts.Commands.TFASetup
{
    public class TFADisableCommandValidator : AbstractValidator<TFADisableCommand>
    {
        public TFADisableCommandValidator()
        {
            RuleFor(v => v.Email)
            .NotEmpty();
        }
    }
}
