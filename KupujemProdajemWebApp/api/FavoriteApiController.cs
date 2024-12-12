using KupujemProdajemWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace KupujemProdajemWebApp.api
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteApiController : ControllerBase
    {
        private readonly FavoriteService _favoriteService;
        private readonly ILogger<FavoriteApiController> _logger;

        public FavoriteApiController(FavoriteService favoriteService, ILogger<FavoriteApiController> logger)
        {
            _favoriteService = favoriteService;
            _logger = logger;
        }

        [HttpGet("savedAds")]
        public async Task<IActionResult> SavedAds()
        {
            var result = await _favoriteService.GetAllUserSavedAds();
            return Ok(result);
        }

        [HttpPost("saveAd/{adId}")]
        public async Task<IActionResult> SaveAd(int adId)
        {
            var result = await _favoriteService.SaveAdToFavorite(adId);

            if (!result)
            {
                return BadRequest("This ad is already in favorites.");
            }

            return Ok("Ad has been successfully added to favorites.");
        }

        [HttpDelete("remove/{adId}")]
        public async Task<IActionResult> RemoveFromFavorites(int adId)
        {
            var result = await _favoriteService.RemovedSavedAd(adId);

            if (!result)
            {
                return BadRequest("This ad does not exist!");
            }

            return Ok("Successfully deleted ad!");
        }
    }
}
