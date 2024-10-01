using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShopScraper.Helpers;
using EShopScraper.Models;

namespace EShopScraper.Services
{
    internal static class ScrapingService
    {
        public static async Task<ShopItem> GetInfo(string url)
        {
            IDocument document = await ScrapeHtml(url);
            if (document == null)
            {
                throw new ArgumentNullException("Failed to scrape HTML...");
            }

            ShopItem shopItem = new ShopItem()
            {
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

        private static List<string> FindImages(IDocument document)
        {
            return [];
        }
        private static string FindBrandName(IDocument document)
        {
            return "";
        }
        private static decimal FindPrice(IDocument document)
        {
            return 0;
        }
        private static decimal FindDiscount(IDocument document)
        {
            return 0;
        }
        private static List<string> FindRatings(IDocument document)
        {
            return [];
        }
        private static string FindDescription(IDocument document)
        {
            return "";
        }
        private static List<string> FindBulletPoints(IDocument document)
        {
            return [];
        }
        private static List<string> FindAttributes(IDocument document)
        {
            return [];
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
