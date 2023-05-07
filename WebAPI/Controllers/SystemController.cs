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
        public SystemController(ApplicationSettings applicationSettings)
        {
            _applicationSettings = applicationSettings;
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
    }
}
