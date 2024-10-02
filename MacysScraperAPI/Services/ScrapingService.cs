using AngleSharp;
using AngleSharp.Dom;
using MacysScrapperAPI.Helpers;
using MacysScrapperAPI.Models;
using System.Text.RegularExpressions;

namespace MacysScrapperAPI.Services
{
    internal static class ScrapingService
    {
        // Service implementing the retrieval of needed data from the given webpage URl
        public static async Task<ShopItem> GetInfo(string url)
        {
            // Python script is called to scrape the html of the given URL, bypassing 403 error
            IDocument document = await ScrapeHtml(url);
            if (document == null)
            {
                throw new ArgumentNullException("Failed to scrape HTML");
            }
          
            // ShopItem is created, finding value of each of its fields via a dedicated method
            ShopItem shopItem = new ShopItem()
            {
                Name = FindName(document),
                Images = FindImages(document),
                BrandName = FindBrandName(document),
                Price = FindPrice(document),
                Discount = FindDiscount(document),
                Ratings = FindRatings(document),
                Description = FindDescription(document),
                BulletPoints = FindBulletPoints(document),
                Attributes = FindAttributes(document),
            };

            return shopItem;
        }

        private static string FindName(IDocument document)
        {
            // CSS selectors are used to find specific elements, hardcoded locations or ID's are avoided, as they might change in the future
            var nameSpan = document.QuerySelector(".product-title span[itemprop=\"name\"]");
            if (nameSpan != null)
            {
                return nameSpan.TextContent;
            }
            throw new ArgumentNullException("Name elemenet not found");
        }

        private static List<string> FindImages(IDocument document)
        {
            var images = new List<string>();

            // Attempts to select images from the side-gallery, if it exists or can be found
            var altImages = document.QuerySelectorAll(".alt-image-container img");
            if (altImages.Length > 0)
            {
                foreach (var altImage in altImages)
                {
                    var image = altImage.GetAttribute("src");
                    if (image != null) images.Add(image);
                    else throw new ArgumentNullException("src attribute not found");
                }
            }
            // Otherwise selects only the main image of the item
            else
            {
                var mainImage = document.QuerySelector(".stylitics-shop-similar");
                if (mainImage != null)
                {
                    var image = mainImage.GetAttribute("src");
                    if (image != null) images.Add(image);
                    else throw new ArgumentNullException("src attribute not found");
                }
                else throw new ArgumentNullException("Main image element not found");
            }
            return images;
        }
        private static string FindBrandName(IDocument document)
        {
            var brandLabel = document.QuerySelector(".product-title label[itemprop=\"brand\"]");
            if (brandLabel != null)
            {
                return brandLabel.TextContent;
            }
            throw new ArgumentNullException("Brand element not found");
        }
        private static decimal FindPrice(IDocument document)
        {
            var priceElement = document.QuerySelector(".price-wrapper .price-strike-lg");
            if (priceElement != null)
            {
                var priceText = priceElement.TextContent;
                // Price text contains currency symbol, which is removed via regex
                return Convert.ToDecimal(Regex.Match(priceText, @"\d+(\.\d{1,2})?").Value);
            }
            throw new ArgumentNullException("Price wrapper not found");
        }
        private static decimal FindDiscount(IDocument document)
        {
            var discountElement = document.QuerySelector(".price-wrapper .price-red.price-lg");
            if (discountElement != null)
            {
                var priceText = discountElement.TextContent;
                return Convert.ToDecimal(Regex.Match(priceText, @"\d+(\.\d{1,2})?").Value);
            }
            throw new ArgumentNullException("Price wrapper not found");
        }
        private static string FindRatings(IDocument document)
        {
            var ratingDiv = document.QuerySelector(".title .rating fieldset[aria-label]");
            if (ratingDiv != null)
            {
                var ratings = ratingDiv.GetAttribute("aria-label");
                if (ratings != null) { return ratings; }
                else { throw new ArgumentNullException("Rating attribute not found"); }
            }
            return "";
        }
        private static string FindDescription(IDocument document)
        {
            var descriptionElement = document.QuerySelector("div[data-testid='product-details-accordion'] .margin-bottom-xxs");
            if (descriptionElement != null)
            {
                return descriptionElement.TextContent;
            }
            throw new ArgumentNullException("Detail accordion not found");
        }
        private static List<string> FindBulletPoints(IDocument document)
        {
            var bulletpoints = new List<string>();
            foreach (var bulletpoint in document.QuerySelectorAll("div[data-testid='product-details-accordion'] .padding-left-xxs"))
            {
                bulletpoints.Add(bulletpoint.TextContent);
            }
            return bulletpoints;
        }
        private static Attributes FindAttributes(IDocument document)
        {
            var attributes = new Attributes()
            {
                Colors = new List<string>(),
                Sizes = new List<string>(),
            };
            foreach (var chip in document.QuerySelectorAll("div[data-testid=\"size-chips\"] button span"))
            {
                attributes.Sizes.Add(chip.TextContent);
            }
            foreach (var swatch in document.QuerySelectorAll(".colors li label input"))
            {
                var color = swatch.GetAttribute("aria-label");
                // Color attributes are preceded by "Color: ", which is trimmed
                if (color != null) { attributes.Colors.Add(color.Replace("Color: ", "").Trim()); }
                else { throw new ArgumentNullException("Color atttribute not found"); }
            }
            return attributes;
        }

        private static async Task<IDocument> ScrapeHtml(string url)
        {
            PythonCaller pythonCaller = new PythonCaller();
            var data = pythonCaller.RunScraper(url);
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(req => req.Content(data));
            return document;
        }
    }
}
