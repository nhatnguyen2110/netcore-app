namespace SocialNetworkAPI.FacebookJsonWeb
{
    public class Payload
    {
        [Newtonsoft.Json.JsonProperty("id")]
        public string? Id { get; set; }
        [Newtonsoft.Json.JsonProperty("first_name")]
        public string? FirstName { get; set; }
        [Newtonsoft.Json.JsonProperty("last_name")]
        public string? LastName { get; set; }
        [Newtonsoft.Json.JsonProperty("email")]
        public string? Email { get; set; }
        [Newtonsoft.Json.JsonProperty("picture")]
        public FBPicture? Picture { get; set; }
    }
    public class FBPicture
    {
        [Newtonsoft.Json.JsonProperty("data")]
        public FBPictureDetail? Data { get; set; }
    }
    public class FBPictureDetail
    {
        [Newtonsoft.Json.JsonProperty("height")]
        public double Height { get; set; }
        [Newtonsoft.Json.JsonProperty("width")]
        public double Width { get; set; }
        [Newtonsoft.Json.JsonProperty("url")]
        public string? Url { get; set; }
    }
}
