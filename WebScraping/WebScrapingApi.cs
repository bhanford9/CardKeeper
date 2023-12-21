using WebScraping.ScrapeOutputs;
using WebScraping.ScrapeOutputs.PokemonScrapeOutputs;
using WebScraping.WebScrapingParameters.PokemonScrapingParameters;

namespace WebScraping;

public interface IWebScrapingApi
{
    Task<IMavinScrapeOutput> Scrape(IMavinScrapingParams parameters);
}

internal class WebScrapingApi : IWebScrapingApi
{
    private readonly IWebScrapingExecutive scraper;

    internal WebScrapingApi(IWebScrapingExecutive executive) => this.scraper = executive;

    public async Task<IMavinScrapeOutput> Scrape(IMavinScrapingParams parameters)
    {
        return (await this.scraper.Scrape(parameters)).As<IMavinScrapeOutput>();
    }
}
