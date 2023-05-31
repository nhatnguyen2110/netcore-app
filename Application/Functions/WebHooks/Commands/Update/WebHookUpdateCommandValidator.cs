using FluentValidation;

namespace Application.Functions.WebHooks.Commands.Update
{
    public class WebHookUpdateCommandValidator : AbstractValidator<WebHookUpdateCommand>
    {
        public WebHookUpdateCommandValidator()
        {
            RuleFor(v => v.WebHookUrl)
            .NotEmpty();
            RuleFor(v=>v.Id)
            .NotEmpty();
        }
    }
}
