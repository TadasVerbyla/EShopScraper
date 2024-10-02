namespace MacysScrapperAPI.Models
{
    // Model class for the items found in the scraped stores web page
    public class ShopItem
    {
        public required string Name { get; set; }
        public required List<string> Images { get; set; }
        public string? BrandName { get; set; }
        public required decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public string? Ratings { get; set; }
        public required string Description { get; set; }
        public List<string>? BulletPoints { get; set; }
        public Attributes? Attributes { get; set; }
    }
}
