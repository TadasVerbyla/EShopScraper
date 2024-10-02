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
        //var url = "https://www.macys.com/shop/product/j.m.-haggar-mens-4-way-stretch-classic-fit-flat-front-dress-pant?ID=6124248&swatchColor=Black";
        var url = "https://www.macys.com/shop/product/cole-haan-mens-grand-atlantic-textured-sneaker?ID=18896651";
        //var url = "https://www.macys.com/shop/product/michael-kors-mens-classic-fit-spring-performance-pants?ID=19160243&swatchColor=Navy%20Plaid";
        var item = await ScrapingService.GetInfo(url);
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(item));
    }
}