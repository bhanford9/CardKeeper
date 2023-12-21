using Microsoft.Extensions.DependencyInjection;
using WebScraping.WebScrapingParameters.PokemonScrapingParameters;

namespace WebScraping.WebScrapingParameters;
internal class ScrapingParamsContainer
{
    public static void Register(IServiceCollection builder)
    {
        builder
            .AddTransient<IWebScrapingParams, MavinScrapingParams>()
            .AddTransient<IMavinScrapingParams, MavinScrapingParams>();
    }
}
