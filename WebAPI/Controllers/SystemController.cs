using Application.Models;
using Application.Models.Settings;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Common;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class SystemController : ApiControllerBase
    {
        private readonly ApplicationSettings _applicationSettings;
        private readonly IConfiguration _configuration;
        public SystemController(ApplicationSettings applicationSettings, IConfiguration configuration)
        {
            _applicationSettings = applicationSettings;
            _configuration = configuration;
        }
        [HttpPost("[action]")]
        public IActionResult RSAEncryptData([FromBody] EncryptedDataRequestModel request)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            return Ok(
                Response<object>.Success(
                    new
                    {
                        EncryptedData1 = CommonHelper.RSAEncrypt(request.PlainText1, _applicationSettings.PublicKeyEncode),
                        EncryptedData2 = CommonHelper.RSAEncrypt(request.PlainText2, _applicationSettings.PublicKeyEncode)
                    }
                    )
                );
#pragma warning restore CS8604 // Possible null reference argument.
        }
        [HttpGet("[action]")]
        public IActionResult GetConfigs()
        {
            return Ok(
                Response<object>.Success(
                    new
                    {
                        EncryptPublicKeyEncode = _applicationSettings.PublicKeyEncode,
                        EnableEncryptAuthorize = _applicationSettings.EnableEncryptAuthorize,
                        EnableGoogleReCaptcha = _applicationSettings.EnableGoogleReCaptcha,
                        GoogleSiteKey = _applicationSettings.GoogleSiteKey,
                        GoogleAuthClientId = _configuration["Authentication:Google:ClientId"],
                    }
                    )
                );
        }
    }
}
