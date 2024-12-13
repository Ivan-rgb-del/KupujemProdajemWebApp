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

        [HttpPut("update/{adId}")]
        public async Task<IActionResult> Edit([FromBody] EditAdViewModel editAdVM, int adId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Validation errors", errors = ModelState });
            }

            var result = await _advertisementService.EditAdvertisement(adId, editAdVM);
            return Ok(result);
        }

        [HttpGet("ad/city={city}/categoryId={categoryId}/groupId={groupId}/IsFixedPrice={IsFixedPrice}/IsReplacement={IsReplacement}/minPrice={minPrice}/maxPrice={maxPrice}")]
        public async Task<IActionResult> FilterAds(string city, int categoryId, int groupId, bool IsFixedPrice, bool IsReplacement, double minPrice, double maxPrice)
        {
            var ads = await _advertisementService.FilterAds(city, categoryId, groupId, IsFixedPrice, IsReplacement, minPrice, maxPrice);
            return Ok(ads);
        }
    }
}
