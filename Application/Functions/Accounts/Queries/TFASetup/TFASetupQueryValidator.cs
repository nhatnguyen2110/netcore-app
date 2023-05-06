using FluentValidation;

namespace Application.Functions.Accounts.Queries.TFASetup
{
    public class TFASetupCommandValidator : AbstractValidator<TFASetupQuery>
    {
        public TFASetupCommandValidator()
        {
            RuleFor(v => v.UserId)
            .NotEmpty();
        }
    }
}
