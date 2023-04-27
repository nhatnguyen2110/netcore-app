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
        private readonly ILogger<SystemController> _logger;
        public SystemController(ApplicationSettings applicationSettings, ILogger<SystemController> logger)
        {
            _applicationSettings = applicationSettings;
            _logger = logger;
        }
        [HttpPost("[action]")]
        public IActionResult RSAEncryptData([FromBody] EncryptedDataRequestModel request)
        {
            _logger.LogDebug("This is debug log");
            _logger.LogInformation("This is info log");
            _logger.LogWarning("This is warning log");
            _logger.LogError("This is error log");
            _logger.LogCritical("This is critical log");

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
