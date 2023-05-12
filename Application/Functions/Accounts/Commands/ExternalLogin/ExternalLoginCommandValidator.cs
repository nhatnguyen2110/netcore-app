using FluentValidation;

namespace Application.Functions.Accounts.Commands.ExternalLogin
{
    public class ExternalLoginCommandValidator : AbstractValidator<ExternalLoginCommand>
    {
        public ExternalLoginCommandValidator()
        {
            RuleFor(v => v.IdToken)
                .NotEmpty();
            RuleFor(v => v.Provider)
                .NotEmpty();
        }
    }
}
