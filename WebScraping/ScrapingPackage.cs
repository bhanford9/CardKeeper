using WebScraping.WebScrapers;
using WebScraping.WebScrapingParameters;

namespace WebScraping;

internal static class ScraperPackageHelpers
{
    public static IScrapingPackage Package(this (IWebScraper Scraper, IWebScrapingParams Params) items)
        => new ScrapingPackage(items.Scraper, items.Params);

    public static (IWebScraper Scraper, IWebScrapingParams Params) Open(this IScrapingPackage package)
        => (package.Scraper, package.Parameters);
}

internal interface IScrapingPackage
{
    IWebScrapingParams Parameters { get; }
    IWebScraper Scraper { get; }
}

internal class ScrapingPackage(IWebScraper scraper, IWebScrapingParams parameters) : IScrapingPackage
{
    public IWebScraper Scraper { get; } = scraper;
    public IWebScrapingParams Parameters { get; } = parameters;
}
