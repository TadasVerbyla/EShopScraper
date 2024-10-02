namespace MacysScrapperAPI.Models
{
    // A separate model for ShopItem's Attributes field, saving colors and sizes data of items
    public class Attributes
    {
        public List<string>? Colors { get; set; }
        public List<string>? Sizes { get; set; }
    }
}
