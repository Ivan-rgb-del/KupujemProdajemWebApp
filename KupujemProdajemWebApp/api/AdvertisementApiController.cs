using KupujemProdajemWebApp.Models;
using KupujemProdajemWebApp.Services;
using KupujemProdajemWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KupujemProdajemWebApp.api
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvertisementApiController : ControllerBase
    {
        private readonly AdvertisementService _advertisementService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AdvertisementApiController(AdvertisementService advertisementService, IHttpContextAccessor httpContextAccessor)
        {
            _advertisementService = advertisementService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Advertisement>>> GetAllAds()
        {
            var ads = await _advertisementService.GetAllAdvertisements();
            return Ok(ads);
        }

        [HttpGet("adId={adId}")]
        public async Task<ActionResult<Advertisement>> GetAd(int adId)
        {
            var ad = await _advertisementService.GetAdById(adId);

            if (ad == null)
            {
                return NotFound();
            }

            return Ok(ad);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateAdViewModel createAdVM)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Validation errors", errors = ModelState });
            }

            var result = await _advertisementService.CreateNewAdvertisement(createAdVM);

            return Ok(result);
        }

        [HttpDelete("delete={adId}")]
        public async Task Delete(int adId)
        {
            await _advertisementService.DeleteAdvertisement(adId);
        }
    }
}
