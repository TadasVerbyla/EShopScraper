using EShopScraper.Models;
using EShopScraper.Services;
using Microsoft.AspNetCore.Mvc;

namespace EShopScraper.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScraperController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<ShopItem>> GetItemInfo([FromBody] string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return BadRequest();
            }
            var itemInfo = await ScrapingService.GetInfo(url);
            if (itemInfo == null)
            {
                return NotFound();
            }
            return Ok(itemInfo);
        }
    }
}
