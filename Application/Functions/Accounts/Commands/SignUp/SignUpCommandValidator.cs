using FluentValidation;
using System.Text.RegularExpressions;

namespace Application.Functions.Accounts.Commands.SignUp
{
    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(v => v.Email)
            .NotEmpty()
            .Must(x => !Regex.IsMatch(x ?? "", "<([^>]+)>", RegexOptions.Multiline))
            .EmailAddress();
            RuleFor(v => v.Password)
                .NotEmpty()
                .Matches(@"^(?=.*\d)(?=.*[A-Za-z])(?=.*\W).{9,}$");
            RuleFor(v => v.FirstName)
                .NotEmpty()
                .Must(x => !Regex.IsMatch(x ?? "", "<([^>]+)>", RegexOptions.Multiline));
            RuleFor(v => v.LastName)
                .NotEmpty()
                .Must(x => !Regex.IsMatch(x ?? "", "<([^>]+)>", RegexOptions.Multiline));
        }
    }
}
