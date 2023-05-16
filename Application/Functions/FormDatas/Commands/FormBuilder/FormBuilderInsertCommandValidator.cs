using FluentValidation;

namespace Application.Functions.FormDatas.Commands.FormBuilder
{
    public class FormBuilderInsertCommandValidator : AbstractValidator<FormBuilderInsertCommand>
    {
        public FormBuilderInsertCommandValidator()
        {
            RuleFor(v => v.TableName)
            .NotEmpty();
            RuleFor(v => v.Fields)
            .NotEmpty();
        }
    }
}
