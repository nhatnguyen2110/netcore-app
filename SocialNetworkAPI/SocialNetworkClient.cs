using Domain.ApiClient;
using Microsoft.Extensions.Configuration;

namespace SocialNetworkAPI
{
    public class SocialNetworkClient : ApiHttpClient, ISocialNetworkClient
    {
        private readonly IConfiguration _configuration;
        public SocialNetworkClient(IConfiguration configuration, HttpClient httpClient) : base(httpClient)
        {
            _configuration = configuration;
            this.ReadResponseAsString = false;
        }
        public override Task PrepareRequestAsync(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url, object obj)
        {
            //json body
            var content_ = new System.Net.Http.StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(obj, _settings.Value));
            content_.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
            request.Content = content_;
            request.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("application/json"));
            client.DefaultRequestHeaders.Add("UserAgent", "My WebApplication");
            return Task.CompletedTask;
        }
        public async Task<FacebookJsonWeb.Payload> VerifyFacebookTokenAsync(string token, CancellationToken cancellationToken = default)
        {
            if (token == null)
                throw new System.ArgumentNullException("token");
            this.BaseUrl = "https://graph.facebook.com";
            var appVer = _configuration["Authentication:Facebook:AppVer"] ?? "v14.0";
            var relativeUrl = $"{appVer}/me?access_token={token}&fields=email,first_name,last_name,picture";
            return await this.SendRequestAsync<FacebookJsonWeb.Payload>(null, relativeUrl, new { }, Domain.Enums.HttpMethod.GET, cancellationToken);
        }
    }
}
