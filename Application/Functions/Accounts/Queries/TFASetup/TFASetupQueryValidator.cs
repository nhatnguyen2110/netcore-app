using FluentValidation;

namespace Application.Functions.Accounts.Queries.TFASetup
{
    public class TFASetupQueryValidator : AbstractValidator<TFASetupQuery>
    {
        public TFASetupQueryValidator()
        {
            RuleFor(v => v.UserId)
            .NotEmpty();
        }
    }
}
