using FluentValidation;

namespace Application.Functions.Accounts.Commands.TFASetup
{
    public class TFAEnableCommandValidator : AbstractValidator<TFAEnableCommand>
    {
        public TFAEnableCommandValidator()
        {
            RuleFor(v => v.Code)
           .NotEmpty();
        }
    }
}
