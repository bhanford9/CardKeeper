using CardManager.Models.Cards.PokemonCards;
using WebScraping;
using WebScraping.ScrapeOutputs.PokemonScrapeOutputs;
using WebScraping.WebScrapingParameters.PokemonScrapingParameters;
using static CardManager.Services.WebScrapingService;

namespace CardManager.Services;

public interface IWebScrapingService
{
    event ScrapeCompletedHandler? ScrapeCompleted;

    Task<IMavinScrapeOutput> Scrape(IMavinScrapingParams parameters);
}

public class WebScrapingService(IWebScrapingApi webScrapingApi) : IWebScrapingService
{
    private readonly IWebScrapingApi webScrapingApi = webScrapingApi;

    public delegate void ScrapeCompletedHandler();
    public event ScrapeCompletedHandler? ScrapeCompleted;

    public Task<IMavinScrapeOutput> Scrape(IMavinScrapingParams parameters)
    {
        return this.webScrapingApi.Scrape(parameters);
    }
}
