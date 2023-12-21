using Microsoft.Extensions.DependencyInjection;
using WebScraping.WebScrapingParameters.PokemonScrapingParameters;

namespace WebScraping.WebScrapingParameters;
internal static class ScrapingParamsContainer
{
    public static IServiceCollection RegisterScrapingParams(this IServiceCollection builder)
        => builder
            .AddTransient<IWebScrapingParams, MavinScrapingParams>()
            .AddTransient<IMavinScrapingParams, MavinScrapingParams>();
}
