namespace Application.Models
{
	public class RequestParameter
	{
		const int maxPageSize = 999;
		public int PageNumber { get; set; } = 1;
		private int _pageSize = 10;
		public int PageSize
		{
			get => _pageSize;
			set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
		}
		public virtual string OrderBy { get; set; } = "Id";
	}
}
