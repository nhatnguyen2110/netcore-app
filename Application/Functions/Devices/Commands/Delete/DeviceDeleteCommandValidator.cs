using FluentValidation;

namespace Application.Functions.Devices.Commands.Delete
{
    public class DeviceDeleteCommandValidator : AbstractValidator<DeviceDeleteCommand>
    {
        public DeviceDeleteCommandValidator()
        {
            RuleFor(v => v.Id)
            .NotEmpty();
        }
    }
}
