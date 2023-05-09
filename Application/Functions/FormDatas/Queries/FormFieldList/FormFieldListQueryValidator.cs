using FluentValidation;

namespace Application.Functions.FormDatas.Queries.FormFieldList
{
	public class FormFieldListQueryValidator : AbstractValidator<FormFieldListQuery>
	{
		public FormFieldListQueryValidator()
		{
			RuleFor(v => v.DBTable)
			.NotEmpty();
		}
	}
}
