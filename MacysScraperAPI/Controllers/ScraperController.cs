using MacysScrapperAPI.Models;
using MacysScrapperAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace MacysScrapperAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ScraperController : ControllerBase
    {
        // API endpoint to retrieve data about Macys store items, requiring item URL and returning a serialized json of ShopItem model
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
