using AngleSharp;
using HtmlAgilityPack;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using EShopScraper;
using EShopScraper.Services;
class Program
{
    static async Task Main(string[] args)
    {
        var url = "https://www.macys.com/shop/product/calvin-klein-mens-slim-fit-wool-infinite-stretch-suit-pants?ID=2711143&swatchColor=Navy";
        var result = await ScrapingService.GetInfo(url);
        Console.WriteLine(result);
    }
}