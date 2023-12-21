using WebScraping.WebScrapers;
using WebScraping.WebScrapingParameters;

namespace WebScraping;

internal interface IWebScraperRepository
{
    IWebScraper GetScraper(IWebScrapingParams webScrapingParams);
    IEnumerable<IScrapingPackage> GetScrapers(IEnumerable<IWebScrapingParams> scraperParams);
}

internal class WebScraperRepository(IEnumerable<IWebScraper> webScrapers) : IWebScraperRepository
{
    private readonly IReadOnlyList<IWebScraper> scrapers = webScrapers.ToList();

    public IWebScraper GetScraper(IWebScrapingParams webScrapingParams)
        => this.scrapers.First(x => x.CanScrape(webScrapingParams));

    public IEnumerable<IScrapingPackage> GetScrapers(IEnumerable<IWebScrapingParams> scraperParams)
        => scraperParams.Select(x => (this.scrapers.First(s => s.CanScrape(x)), x).Package());
}
