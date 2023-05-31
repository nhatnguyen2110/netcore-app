using FluentValidation;

namespace Application.Functions.WebHooks.Commands.Create
{
    public class WebHookCreateCommandValidator : AbstractValidator<WebHookCreateCommand>
    {
        public WebHookCreateCommandValidator()
        {
            RuleFor(v => v.WebHookUrl)
            .NotEmpty();
        }
    }
}
