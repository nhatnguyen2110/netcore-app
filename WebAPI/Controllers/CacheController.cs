using Application.Common.Interfaces;
using Application.Models;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Common;

namespace WebAPI.Controllers
{
    public class CacheController : ApiControllerBase
    {
        private readonly ILogger<CacheController> _logger;
        private readonly ICacheService _cacheService;
        public CacheController(
        ILogger<CacheController> logger,
        ICacheService cacheService
        )
        {
            _logger = logger;
            _cacheService = cacheService;
        }
        [Authorize(Roles = "Administrator")]
        [HttpDelete("[action]")]
        public ActionResult ClearAll()
        {
            try
            {
                _logger.LogTrace("This is trace log");
                _logger.LogDebug("This is debug log");
                _logger.LogInformation("This is info log");
                _logger.LogWarning("This is warning log");
                _logger.LogError("This is error log");
                _logger.LogCritical("This is critical log");

                var total = _cacheService.RemoveAllCache();
                return Ok(Response<int>.Success(total));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get clear all cache. Message: {ex.Message}");
                return BadRequest(new Response(false, Constants.GeneralErrorMessage, ex.Message, "Failed to clear all cache"));
            }
        }
        [Authorize(Roles = "Administrator")]
        [HttpDelete("[action]")]
        public ActionResult ClearByKey([FromQuery] string key)
        {
            try
            {
                _cacheService.Remove(key);
                return Ok(Response<Unit>.Success());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to get clear cache by  key: {key}. Message: {ex.Message}");
                return BadRequest(new Response(false, Constants.GeneralErrorMessage, ex.Message, "Failed to clear cache"));
            }
        }
    }
}
