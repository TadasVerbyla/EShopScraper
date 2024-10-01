using AngleSharp;
using AngleSharp.Dom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShopScraper.Helpers;

namespace EShopScraper.Services
{
    internal static class ScrapingService
    {
        public static async Task<string> GetInfo(string url)
        {
            IDocument document = await ScrapeHtml(url);
            if (document == null)
            {
                throw new ArgumentNullException("Failed to scrape HTML...");
            }
            return document.Title;
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
