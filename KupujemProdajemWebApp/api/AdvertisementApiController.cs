using KupujemProdajemWebApp.Models;
using KupujemProdajemWebApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace KupujemProdajemWebApp.api
{
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    [ApiController]
    public class AdvertisementApiController : ControllerBase
    {
        private readonly AdvertisementService _advertisementService;

        public AdvertisementApiController(AdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Advertisement>>> GetAllAds()
        {
            var ads = await _advertisementService.GetAllAdvertisements();
            return Ok(ads);
        }
    }
}
