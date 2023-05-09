using Application.Common.Mappings;
using Domain.Entities.Forms;

namespace Application.Models.Form
{
	public class FormFieldDto : IMapFrom<FormData>
	{
		public int Id { get; set; }
		public string? DBTable { get; set; }
		public string? DBColumn { get; set; }
		public string? EditorType { get; set; }
		public string? Name { get; set; }
		public string? DefaultValue { get; set; }
		public bool Required { get; set; }
		public int? MaxLength { get; set; }
		public int? MinValue { get; set; }
		public int? MaxValue { get; set; }
		public int SortOrder { get; set; }
		public string? Tooltip { get; set; }
		public string? Javascript { get; set; }
		public string? SourceEndpoint { get; set; }
		public string? HttpMethod { get; set; }
		public string? DataText { get; set; }
		public string? DataValue { get; set; }
		public string? Width { get; set; }
		public string? Height { get; set; }
		public string? ClassCSS { get; set; }
		public string? PermissionRoles { get; set; }
		public string? DBValue { get; set; }
	}
}
