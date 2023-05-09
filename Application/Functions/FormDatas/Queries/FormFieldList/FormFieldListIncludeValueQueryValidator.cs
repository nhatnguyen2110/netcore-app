using FluentValidation;

namespace Application.Functions.FormDatas.Queries.FormFieldList
{
	public class FormFieldListIncludeValueQueryValidator : AbstractValidator<FormFieldListIncludeValueQuery>
	{
		public FormFieldListIncludeValueQueryValidator()
		{
			RuleFor(v => v.DBTable)
			.NotEmpty();
		}
	}
}
