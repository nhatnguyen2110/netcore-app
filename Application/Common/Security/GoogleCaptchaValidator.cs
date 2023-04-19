using Application.Models.Settings;
using FluentValidation;
using FluentValidation.Validators;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Application.Common.Security
{
    public class GoogleCaptchaValidator<T, TElement> : PropertyValidator<T, TElement>
    {
        private readonly ApplicationSettings _applicationSettings;
        public GoogleCaptchaValidator(ApplicationSettings applicationSettings)
        {
            _applicationSettings = applicationSettings;
        }

        public override string Name => "GoogleCaptchaValidator";
        protected override string GetDefaultMessageTemplate(string errorCode)
            => "Google reCAPTCHA validation failed";

        public override bool IsValid(ValidationContext<T> context, TElement value)
        {
            if (_applicationSettings.EnableGoogleReCaptcha)
            {
                if (value == null || value.ToString() == String.Empty)
                {
                    return false;
                }
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                String reCaptchResponse = value.ToString();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                String reCaptchaSecret = "" + _applicationSettings.GoogleSecretKey;
                using (HttpClient httpClient = new HttpClient())
                {
                    var httpResponse = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={reCaptchaSecret}&response={reCaptchResponse}").Result;
                    if (httpResponse.StatusCode != HttpStatusCode.OK)
                    {
                        return false;
                    }
                    String jsonResponse = httpResponse.Content.ReadAsStringAsync().Result;
                    dynamic jsonData = JObject.Parse(jsonResponse);
                    if (jsonData.success != true.ToString().ToLower())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
