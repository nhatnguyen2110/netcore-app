namespace SocialNetworkAPI
{
    public interface ISocialNetworkClient
    {
        Task<FacebookJsonWeb.Payload> VerifyFacebookTokenAsync(string token, CancellationToken cancellationToken = default(CancellationToken));
    }
}
