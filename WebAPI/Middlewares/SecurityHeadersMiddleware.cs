namespace WebAPI.Middlewares
{
    public class SecurityHeadersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<SecurityHeadersMiddleware> _logger;

        public SecurityHeadersMiddleware(RequestDelegate next, ILogger<SecurityHeadersMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                string defaultSrc = "*.google-analytics.com " +
                                    "*.doubleclick.net " +
                                    "*.adsrvr.org " +
                                    "*.cloudfront.net " +
                                    "*.google.com " +
                                    "*.youtube.com " +
                                    "*.braintree-api.com " +
                                    "*.braintreegateway.com " +
                                    "*.paypal.com " +
                                    "*.cardinalcommerce.com " +
                                    "*.amazonaws.com " +
                                    "*.onemap.sg " +
                                    "*.facebook.com "
                                    ;

                string scriptSrcElem = "*.googletagmanager.com " +
                                        "*.google-analytics.com " +
                                        "*.adsrvr.org " +
                                        "acdn.adnxs.com " +
                                        "*.facebook.net " +
                                        "login.dotomi.com " +
                                        "unpkg.com " +
                                        "cdnjs.cloudflare.com " +
                                        "cdn.jsdelivr.net " +
                                        "fonts.googleapis.com " +
                                        "*.googleadservices.com " +
                                        "sg-ma.sam4m.com " +
                                        "*.doubleclick.net " +
                                        "*.google.com " +
                                        "*.gstatic.com " +
                                        "code.jquery.com " +
                                        "js.braintreegateway.com " +
                                        "maps.googleapis.com " +
                                        "*.paypalobjects.com " +
                                        "*.google.com " +
                                        "songbirdstag.cardinalcommerce.com " +
                                        "*.dotomi.com " +
                                        "*.paypal.com "
                                        ;

                string scriptSrc = "www.googletagmanager.com " +
                                    "www.google-analytics.com " +
                                    "google-analytics.com " +
                                    "*.adsrvr.org " +
                                    "acdn.adnxs.com " +
                                    "login.dotomi.com " +
                                    "*.dotomi.com " +
                                    "unpkg.com " +
                                    "cdnjs.cloudflare.com " +
                                    "cdn.jsdelivr.net " +
                                    "www.googleadservices.com " +
                                    "connect.facebook.net " +
                                    "*.braintree-api.com " +
                                    "*.braintreegateway.com " +
                                    "*.fls.doubleclick.net " +
                                    "googleads.g.doubleclick.net " +
                                    "www.paypalobjects.com " +
                                    "*.paypal.com " +
                                    "pay.google.com" +
                                    "songbirdstag.cardinalcommerce.com " +
                                    "songbird.cardinalcommerce.com " +
                                    "www.google.com " +
                                    "code.jquery.com " +
                                    "maps.googleapis.com " +
                                    "js.braintreegateway.com " +
                                    "assets.braintreegateway.com "
                                    ;

                string styleSrc = "cdnjs.cloudflare.com " +
                                    "fonts.googleapis.com " +
                                    "*.braintreegateway.com " +
                                    "cdn.jsdelivr.net "
                                    ;

                string imageSrc = "assets.braintreegateway.com " +
                                    "checkout.paypal.com "
                                    ;

                string mediaSrc = "*.ascentismedia.com "
                                    ;

                string childSrc = "assets.braintreegateway.com " +
                                    "*.paypal.com "
                                    ;

                string frameSrc = "assets.braintreegateway.com " +
                                    "*.braintreegateway.com " +
                                    "*.paypal.com " +
                                    "*.cardinalcommerce.com " +
                                    "*.google.com " +
                                    "*.youtube.com " +
                                    "*.adsrvr.org " +
                                    "*.doubleclick.net "
                                    ;

                string connectSrc = "*.braintreegateway.com " +
                                    "*.doubleclick.net " +
                                    "*.cardinalcommerce.com " +
                                    "*.paypal.com " +
                                    "*.onemap.sg " +
                                    "*.google-analytics.com " +
                                    "*.facebook.com " +
                                    "*.braintree-api.com "
                                    ;

                string frameAncestors = "dmp.truoptik.com "
                                        ;

                string fontSrc = "fonts.gstatic.com " +
                                    "kit-free.fontawesome.com " +
                                    "cdnjs.cloudflare.com "
                                    ;

                string objectSrc = "";

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                string userAgent = httpContext.Request.Headers["User-Agent"];
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

                bool isBrowserSafari = !string.IsNullOrEmpty(userAgent) && userAgent.ToLower().Contains("safari");

                if (!httpContext.Response.Headers.ContainsKey("X-Frame-Options"))
                {
                    httpContext.Response.Headers.Add("X-Frame-Options", "SAMEORIGIN");
                };
                if (!httpContext.Response.Headers.ContainsKey("X-Xss-Protection"))
                {
                    httpContext.Response.Headers.Add("X-Xss-Protection", "1; mode=block");
                };
                if (!httpContext.Response.Headers.ContainsKey("X-Content-Type-Options"))
                {
                    httpContext.Response.Headers.Add("X-Content-Type-Options", "nosniff");
                };
                if (!httpContext.Response.Headers.ContainsKey("Referrer-Policy"))
                {
                    httpContext.Response.Headers.Add("Referrer-Policy", "strict-origin-when-cross-origin");
                };
                if (!httpContext.Response.Headers.ContainsKey("Cache-Control"))
                {
                    httpContext.Response.Headers.Add("Cache-Control", "no-cache; no-store");
                };
                if (!httpContext.Response.Headers.ContainsKey("Content-Security-Policy"))
                {
                    if (isBrowserSafari)
                    {
                        httpContext.Response.Headers.Add("Content-Security-Policy",
                             $"default-src 'self' {defaultSrc} https:; " +
                             $"script-src 'self' {scriptSrc} 'unsafe-inline' https:;" +
                             $"style-src 'self' {styleSrc} 'unsafe-inline' https:; " +
                             $"img-src 'self' {imageSrc} data: https:; " +
                             $"media-src 'self' {mediaSrc} data: https:; " +
                             $"child-src 'self' {childSrc} 'unsafe-inline' 'unsafe-eval' https:; " +
                             $"frame-src 'self' {frameSrc} https:; " +
                             $"connect-src 'self' {connectSrc} https:; " +
                             $"frame-ancestors 'self' {frameAncestors} https:; " +
                             $"font-src 'self' {fontSrc} https:; " +
                             $"object-src 'self' {objectSrc} https:; ")
                            ;
                    }
                    else
                    {
                        httpContext.Response.Headers.Add("Content-Security-Policy",
                             $"default-src 'self' {defaultSrc} https:; " +
                             $"script-src-elem 'self' {scriptSrcElem} 'unsafe-inline' https:; " +
                             $"script-src 'self' {scriptSrc} 'unsafe-inline' https:;" +
                             $"style-src 'self' {styleSrc} 'unsafe-inline' https:; " +
                             $"img-src 'self' {imageSrc} data: https:; " +
                             $"media-src 'self' {mediaSrc} data: https:; " +
                             $"child-src 'self' {childSrc} 'unsafe-inline' 'unsafe-eval' https:; " +
                             $"frame-src 'self' {frameSrc} https:; " +
                             $"connect-src 'self' {connectSrc} https:; " +
                             $"frame-ancestors 'self' {frameAncestors} https:; " +
                             $"font-src 'self' {fontSrc} https:; " +
                             $"object-src 'self' {objectSrc} https:; ")
                            ;
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Add Security Header Exception: {ex.Message}");
            }
            finally
            {
                if (!httpContext.Response.HasStarted)
                {
                    await _next(httpContext);
                }
            }
        }
    }

    public static class AddSecurityHeadersMiddlewareExtension
    {
        public static IApplicationBuilder UseAddSecurityHeaders(this IApplicationBuilder app, IConfiguration configuration)
        {
            bool.TryParse(configuration["ApplicationSettings:IsAddSecurityHeaders"], out bool isAddSecurityHeaders);
            if (!isAddSecurityHeaders)
                return app;
            return app.UseMiddleware<SecurityHeadersMiddleware>();
        }
    }
}
