using FluentValidation;

namespace Application.Functions.Devices.Commands.Create
{
    public class DeviceCreateCommandValidator: AbstractValidator<DeviceCreateCommand>
    {
        public DeviceCreateCommandValidator() {
            RuleFor(v => v.PushAuth)
            .NotEmpty();
            RuleFor(v => v.PushEndpoint)
            .NotEmpty();
            RuleFor(v => v.PushP256DH)
            .NotEmpty();
        }
    }
}
