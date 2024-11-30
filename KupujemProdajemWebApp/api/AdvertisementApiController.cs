﻿using KupujemProdajemWebApp.Models;
using KupujemProdajemWebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace KupujemProdajemWebApp.api
{
    [ApiController]
    [Route("api/[controller]")]
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