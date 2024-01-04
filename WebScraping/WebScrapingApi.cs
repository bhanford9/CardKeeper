using WebScraping.ScrapeOutputs;
using WebScraping.ScrapeOutputs.PokemonScrapeOutputs;
using WebScraping.WebScrapingParameters.PokemonScrapingParameters;

namespace WebScraping;

public interface IWebScrapingApi
{
    Task<IMavinScrapeOutput> Scrape(IMavinScrapingParams parameters);

    //Task Scrape(IMavinScrapingParams parameters, Action<IMavinScrapeOutput> onComplete);
}

internal class WebScrapingApi(IWebScrapingExecutive executive) : IWebScrapingApi
{
    private readonly IWebScrapingExecutive scraper = executive;

    public async Task<IMavinScrapeOutput> Scrape(IMavinScrapingParams parameters)
    {
        return (await this.scraper.Scrape(parameters)).As<IMavinScrapeOutput>();
    }    

    //public async Task Scrape(IMavinScrapingParams parameters, Action<IMavinScrapeOutput> onComplete)
    //{
    //    onComplete(await this.Scrape(parameters));
    //}
}
