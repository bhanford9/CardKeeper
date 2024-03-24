using WebScraping.ScrapingResults;
using WebScraping.WebScrapingParameters;

namespace WebScraping.WebScrapers;

public interface IWebScraper
{
    bool CanScrape(IWebScrapingParams scrapingParams);

    Task<IScrapingResult> Scrape(IWebScrapingParams scrapingParams);
}

internal abstract class WebScraper<TScraperParams> : IWebScraper
    where TScraperParams : IWebScrapingParams
{
    public bool CanScrape(IWebScrapingParams scrapingParams) =>
        scrapingParams is TScraperParams && InternalCanScrape((TScraperParams)scrapingParams);

    public async Task<IScrapingResult> Scrape(IWebScrapingParams scrapingParams) =>
        await Task.Run(() => InternalScrape((TScraperParams)scrapingParams));

    protected abstract bool InternalCanScrape(TScraperParams scraperParams);

    protected abstract IScrapingResult InternalScrape(TScraperParams scraperParams, int attempt = 0);
}
