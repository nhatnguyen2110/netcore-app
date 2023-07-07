namespace GraphAPI
{
    public class GraphAuthDto
    {
        public string? authority { get; set; }
        public string? clientId { get; set; }
        public string? clientSecret { get; set;}
        public string? resource { get; set; }
        public string? tenantId { get; set; }
        public string? userId { get; set; }

    }
}
