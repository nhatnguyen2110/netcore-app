using FluentValidation;

namespace Application.Functions.Accounts.Commands.RefreshToken
{
	public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
	{
		public RefreshTokenCommandValidator()
		{
			RuleFor(v => v.RefreshToken)
				.NotEmpty();
			RuleFor(v => v.AccessToken)
				.NotEmpty();
		}
	}
}
