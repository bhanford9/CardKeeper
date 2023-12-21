using WebScraping;
using WebScraping.ScrapeOutputs.PokemonScrapeOutputs;
using WebScraping.WebScrapingParameters.PokemonScrapingParameters;

namespace CardManager.Services;

public interface IWebScrapingService
{
    Task<IMavinScrapeOutput> Scrape(IMavinScrapingParams parameters);
}

public class WebScrapingService(IWebScrapingApi webScrapingApi) : IWebScrapingService
{
    private readonly IWebScrapingApi webScrapingApi = webScrapingApi;

    public async Task<IMavinScrapeOutput> Scrape(IMavinScrapingParams parameters)
    {
        return await this.webScrapingApi.Scrape(parameters);
    }
}
