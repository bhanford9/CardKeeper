using WebScraping.ScrapingResults;
using WebScraping.WebScrapingParameters;

namespace WebScraping;

internal interface IWebScrapingExecutive
{
    Task<IEnumerable<IScrapingResult>> Scrape(IEnumerable<IWebScrapingParams> scrapingParams);
    Task<IScrapingResult> Scrape(IWebScrapingParams scrapingParams);
}

internal class WebScrapingExecutive(IWebScraperRepository repository) : IWebScrapingExecutive
{
    private readonly IWebScraperRepository repository = repository;

    public async Task<IScrapingResult> Scrape(IWebScrapingParams scrapingParams)
    {
        var package = this.repository.GetScraper(scrapingParams);
        return await package.Scrape(scrapingParams);
    }

    public async Task<IEnumerable<IScrapingResult>> Scrape(IEnumerable<IWebScrapingParams> scrapingParams)
    {
        return await Task.WhenAll(this.repository
            .GetScrapers(scrapingParams)
            .Select(async package => await package.Scraper.Scrape(package.Parameters)));
    }
}
