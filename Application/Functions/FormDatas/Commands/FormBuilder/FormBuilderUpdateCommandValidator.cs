using FluentValidation;

namespace Application.Functions.FormDatas.Commands.FormBuilder
{
    public class FormBuilderUpdateCommandValidator : AbstractValidator<FormBuilderUpdateCommand>
    {
        public FormBuilderUpdateCommandValidator()
        {
            RuleFor(v => v.ParentId)
            .NotEmpty();
            RuleFor(v => v.ParentColumnName)
            .NotEmpty();
            RuleFor(v => v.TableName)
            .NotEmpty();
            RuleFor(v => v.Fields)
            .NotEmpty();
        }
    }
}
