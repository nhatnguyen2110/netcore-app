using FluentValidation;

namespace Application.Functions.Devices.Commands.Delete
{
    public class DeviceDeleteByUserIdCommandValidator : AbstractValidator<DeviceDeleteByUserIdCommand>
    {
        public DeviceDeleteByUserIdCommandValidator()
        {
            RuleFor(v => v.UserId)
            .NotEmpty();
        }
    }
}
