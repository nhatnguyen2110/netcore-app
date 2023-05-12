using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SocialNetworkAPI
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddSocialNetworkAPI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<ISocialNetworkClient, SocialNetworkClient>();
            return services;
        }
    }
}
