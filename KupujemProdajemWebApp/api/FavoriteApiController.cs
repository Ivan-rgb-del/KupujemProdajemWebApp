using KupujemProdajemWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace KupujemProdajemWebApp.api
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoriteApiController : ControllerBase
    {
        private readonly FavoriteService _favoriteService;

        public FavoriteApiController(FavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [HttpGet("savedAds")]
        public async Task<IActionResult> SavedAds()
        {
            var result = await _favoriteService.GetAllUserSavedAds();
            return Ok(result);
        }
    }
}
