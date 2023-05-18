using FluentValidation;

namespace Application.Functions.Notifications.Commands.WebPush
{
    public class WebPushCommandValidator : AbstractValidator<WebPushCommand>
    {
        public WebPushCommandValidator() {
            RuleFor(v => v.UserId)
               .NotEmpty();
        }
    }
}
