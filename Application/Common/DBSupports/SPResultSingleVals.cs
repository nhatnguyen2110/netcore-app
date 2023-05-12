namespace Application.Common.DBSupports
{
	public class SPSingleValueQueryResultString
	{
		public string? val { get; set; }
	}
	public class JSONStringQueryResultString
	{
		public string? JSONVal { get; set; }
	}
	public class SPSingleValueQueryResultInt
	{
		public int val { get; set; }
	}
    public class SPColumnTypes
    {
        public string? COLUMN_NAME { get; set; }
        public string? DATA_TYPE { get; set; }
        public int? CHARACTER_MAXIMUM_LENGTH { get; set; }
    }
}
